#define UNITY_ASSERTIONS
#define ENABLE_PROFILER
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using ConVar;
using Facepunch;
using Facepunch.Extend;
using Network;
using ProtoBuf;
using Rust;
using Rust.Ai;
using Rust.Workshop;
using Spatial;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Profiling;
using UnityEngine.SceneManagement;

public class BaseEntity : BaseNetworkable, IOnParentSpawning, IPrefabPreProcess
{
	public class Menu : Attribute
	{
		[Serializable]
		public struct Option
		{
			public Translate.Phrase name;

			public Translate.Phrase description;

			public Sprite icon;

			public int order;

			public bool usableWhileWounded;
		}

		public class Description : Attribute
		{
			public string token;

			public string english;

			public Description (string t, string e)
			{
				token = t;
				english = e;
			}
		}

		public class Icon : Attribute
		{
			public string icon;

			public Icon (string i)
			{
				icon = i;
			}
		}

		public class ShowIf : Attribute
		{
			public string functionName;

			public ShowIf (string testFunc)
			{
				functionName = testFunc;
			}
		}

		public class Priority : Attribute
		{
			public string functionName;

			public Priority (string priorityFunc)
			{
				functionName = priorityFunc;
			}
		}

		public class UsableWhileWounded : Attribute
		{
		}

		public string TitleToken;

		public string TitleEnglish;

		public string UseVariable;

		public int Order;

		public string ProxyFunction;

		public float Time;

		public string OnStart;

		public string OnProgress;

		public bool LongUseOnly;

		public bool PrioritizeIfNotWhitelisted = false;

		public bool PrioritizeIfUnlocked = false;

		public Menu ()
		{
		}

		public Menu (string menuTitleToken, string menuTitleEnglish)
		{
			TitleToken = menuTitleToken;
			TitleEnglish = menuTitleEnglish;
		}
	}

	[Serializable]
	public struct MovementModify
	{
		public float drag;
	}

	[Flags]
	public enum Flags
	{
		Placeholder = 1,
		On = 2,
		OnFire = 4,
		Open = 8,
		Locked = 0x10,
		Debugging = 0x20,
		Disabled = 0x40,
		Reserved1 = 0x80,
		Reserved2 = 0x100,
		Reserved3 = 0x200,
		Reserved4 = 0x400,
		Reserved5 = 0x800,
		Broken = 0x1000,
		Busy = 0x2000,
		Reserved6 = 0x4000,
		Reserved7 = 0x8000,
		Reserved8 = 0x10000,
		Reserved9 = 0x20000,
		Reserved10 = 0x40000,
		Reserved11 = 0x80000,
		InUse = 0x100000
	}

	private readonly struct QueuedFileRequest : IEquatable<QueuedFileRequest>
	{
		public readonly BaseEntity Entity;

		public readonly FileStorage.Type Type;

		public readonly uint Part;

		public readonly uint Crc;

		public readonly uint ResponseFunction;

		public readonly bool? RespondIfNotFound;

		public QueuedFileRequest (BaseEntity entity, FileStorage.Type type, uint part, uint crc, uint responseFunction, bool? respondIfNotFound)
		{
			Entity = entity;
			Type = type;
			Part = part;
			Crc = crc;
			ResponseFunction = responseFunction;
			RespondIfNotFound = respondIfNotFound;
		}

		public bool Equals (QueuedFileRequest other)
		{
			return object.Equals (Entity, other.Entity) && Type == other.Type && Part == other.Part && Crc == other.Crc && ResponseFunction == other.ResponseFunction && RespondIfNotFound == other.RespondIfNotFound;
		}

		public override bool Equals (object obj)
		{
			return obj is QueuedFileRequest other && Equals (other);
		}

		public override int GetHashCode ()
		{
			int num = ((Entity != null) ? Entity.GetHashCode () : 0);
			num = (num * 397) ^ (int)Type;
			num = (num * 397) ^ (int)Part;
			num = (num * 397) ^ (int)Crc;
			num = (num * 397) ^ (int)ResponseFunction;
			return (num * 397) ^ RespondIfNotFound.GetHashCode ();
		}
	}

	private readonly struct PendingFileRequest : IEquatable<PendingFileRequest>
	{
		public readonly FileStorage.Type Type;

		public readonly uint NumId;

		public readonly uint Crc;

		public readonly IServerFileReceiver Receiver;

		public readonly float Time;

		public PendingFileRequest (FileStorage.Type type, uint numId, uint crc, IServerFileReceiver receiver)
		{
			Type = type;
			NumId = numId;
			Crc = crc;
			Receiver = receiver;
			Time = UnityEngine.Time.realtimeSinceStartup;
		}

		public bool Equals (PendingFileRequest other)
		{
			return Type == other.Type && NumId == other.NumId && Crc == other.Crc && object.Equals (Receiver, other.Receiver);
		}

		public override bool Equals (object obj)
		{
			return obj is PendingFileRequest other && Equals (other);
		}

		public override int GetHashCode ()
		{
			int type = (int)Type;
			type = (type * 397) ^ (int)NumId;
			type = (type * 397) ^ (int)Crc;
			return (type * 397) ^ ((Receiver != null) ? Receiver.GetHashCode () : 0);
		}
	}

	public static class Query
	{
		public class EntityTree
		{
			private Grid<BaseEntity> Grid;

			private Grid<BasePlayer> PlayerGrid;

			private Grid<BaseEntity> BrainGrid;

			public EntityTree (float worldSize)
			{
				Grid = new Grid<BaseEntity> (32, worldSize);
				PlayerGrid = new Grid<BasePlayer> (32, worldSize);
				BrainGrid = new Grid<BaseEntity> (32, worldSize);
			}

			public void Add (BaseEntity ent)
			{
				Vector3 position = ent.transform.position;
				Grid.Add (ent, position.x, position.z);
			}

			public void AddPlayer (BasePlayer player)
			{
				Vector3 position = player.transform.position;
				PlayerGrid.Add (player, position.x, position.z);
			}

			public void AddBrain (BaseEntity entity)
			{
				Vector3 position = entity.transform.position;
				BrainGrid.Add (entity, position.x, position.z);
			}

			public void Remove (BaseEntity ent, bool isPlayer = false)
			{
				Grid.Remove (ent);
				if (isPlayer) {
					BasePlayer basePlayer = ent as BasePlayer;
					if (basePlayer != null) {
						PlayerGrid.Remove (basePlayer);
					}
				}
			}

			public void RemovePlayer (BasePlayer player)
			{
				PlayerGrid.Remove (player);
			}

			public void RemoveBrain (BaseEntity entity)
			{
				if (!(entity == null)) {
					BrainGrid.Remove (entity);
				}
			}

			public void Move (BaseEntity ent)
			{
				Vector3 position = ent.transform.position;
				Grid.Move (ent, position.x, position.z);
				BasePlayer basePlayer = ent as BasePlayer;
				if (basePlayer != null) {
					MovePlayer (basePlayer);
				}
				if (ent.HasBrain) {
					MoveBrain (ent);
				}
			}

			public void MovePlayer (BasePlayer player)
			{
				Vector3 position = player.transform.position;
				PlayerGrid.Move (player, position.x, position.z);
			}

			public void MoveBrain (BaseEntity entity)
			{
				Vector3 position = entity.transform.position;
				BrainGrid.Move (entity, position.x, position.z);
			}

			public int GetInSphere (Vector3 position, float distance, BaseEntity[] results, Func<BaseEntity, bool> filter = null)
			{
				Profiler.BeginSample ("GetInSphere");
				int result = Grid.Query (position.x, position.z, distance, results, filter);
				Profiler.EndSample ();
				return result;
			}

			public int GetPlayersInSphere (Vector3 position, float distance, BasePlayer[] results, Func<BasePlayer, bool> filter = null)
			{
				Profiler.BeginSample ("GetPlayersInSphere");
				int result = PlayerGrid.Query (position.x, position.z, distance, results, filter);
				Profiler.EndSample ();
				return result;
			}

			public int GetBrainsInSphere (Vector3 position, float distance, BaseEntity[] results, Func<BaseEntity, bool> filter = null)
			{
				Profiler.BeginSample ("GetBrainsInSphere");
				int result = BrainGrid.Query (position.x, position.z, distance, results, filter);
				Profiler.EndSample ();
				return result;
			}
		}

		public static EntityTree Server;
	}

	public class RPC_Shared : Attribute
	{
	}

	public struct RPCMessage
	{
		public Connection connection;

		public BasePlayer player;

		public NetRead read;
	}

	public class RPC_Server : RPC_Shared
	{
		public abstract class Conditional : Attribute
		{
			public virtual string GetArgs ()
			{
				return null;
			}
		}

		public class MaxDistance : Conditional
		{
			private float maximumDistance;

			public MaxDistance (float maxDist)
			{
				maximumDistance = maxDist;
			}

			public override string GetArgs ()
			{
				return maximumDistance.ToString ("0.00f");
			}

			public static bool Test (uint id, string debugName, BaseEntity ent, BasePlayer player, float maximumDistance)
			{
				if (ent == null || player == null) {
					return false;
				}
				return ent.Distance (player.eyes.position) <= maximumDistance;
			}
		}

		public class IsVisible : Conditional
		{
			private float maximumDistance;

			public IsVisible (float maxDist)
			{
				maximumDistance = maxDist;
			}

			public override string GetArgs ()
			{
				return maximumDistance.ToString ("0.00f");
			}

			public static bool Test (uint id, string debugName, BaseEntity ent, BasePlayer player, float maximumDistance)
			{
				if (ent == null || player == null) {
					return false;
				}
				return GamePhysics.LineOfSight (player.eyes.center, player.eyes.position, 2162688) && (ent.IsVisible (player.eyes.HeadRay (), 1218519041, maximumDistance) || ent.IsVisible (player.eyes.position, maximumDistance));
			}
		}

		public class FromOwner : Conditional
		{
			public static bool Test (uint id, string debugName, BaseEntity ent, BasePlayer player)
			{
				if (ent == null || player == null) {
					return false;
				}
				if (ent.net == null || player.net == null) {
					return false;
				}
				if (ent.net.ID == player.net.ID) {
					return true;
				}
				if (ent.parentEntity.uid != player.net.ID) {
					return false;
				}
				return true;
			}
		}

		public class IsActiveItem : Conditional
		{
			public static bool Test (uint id, string debugName, BaseEntity ent, BasePlayer player)
			{
				if (ent == null || player == null) {
					return false;
				}
				if (ent.net == null || player.net == null) {
					return false;
				}
				if (ent.net.ID == player.net.ID) {
					return true;
				}
				if (ent.parentEntity.uid != player.net.ID) {
					return false;
				}
				Item activeItem = player.GetActiveItem ();
				if (activeItem == null) {
					return false;
				}
				if (activeItem.GetHeldEntity () != ent) {
					return false;
				}
				return true;
			}
		}

		public class CallsPerSecond : Conditional
		{
			private ulong callsPerSecond;

			public CallsPerSecond (ulong limit)
			{
				callsPerSecond = limit;
			}

			public override string GetArgs ()
			{
				return callsPerSecond.ToString ();
			}

			public static bool Test (uint id, string debugName, BaseEntity ent, BasePlayer player, ulong callsPerSecond)
			{
				if (ent == null || player == null) {
					return false;
				}
				return player.rpcHistory.TryIncrement (id, callsPerSecond);
			}
		}
	}

	public enum Signal
	{
		Attack,
		Alt_Attack,
		DryFire,
		Reload,
		Deploy,
		Flinch_Head,
		Flinch_Chest,
		Flinch_Stomach,
		Flinch_RearHead,
		Flinch_RearTorso,
		Throw,
		Relax,
		Gesture,
		PhysImpact,
		Eat,
		Startled,
		Admire
	}

	public enum Slot
	{
		Lock,
		FireMod,
		UpperModifier,
		MiddleModifier,
		LowerModifier,
		CenterDecoration,
		LowerCenterDecoration,
		StorageMonitor,
		Count
	}

	[Flags]
	public enum TraitFlag
	{
		None = 0,
		Alive = 1,
		Animal = 2,
		Human = 4,
		Interesting = 8,
		Food = 0x10,
		Meat = 0x20,
		Water = 0x20
	}

	public static class Util
	{
		public static BaseEntity[] FindTargets (string strFilter, bool onlyPlayers)
		{
			return (from x in BaseNetworkable.serverEntities.Where (delegate(BaseNetworkable x) {
					if (x is BasePlayer) {
						BasePlayer basePlayer = x as BasePlayer;
						if (string.IsNullOrEmpty (strFilter)) {
							return true;
						}
						if (strFilter == "!alive" && basePlayer.IsAlive ()) {
							return true;
						}
						if (strFilter == "!sleeping" && basePlayer.IsSleeping ()) {
							return true;
						}
						if (strFilter [0] != '!' && !basePlayer.displayName.Contains (strFilter, CompareOptions.IgnoreCase) && !basePlayer.UserIDString.Contains (strFilter)) {
							return false;
						}
						return true;
					}
					if (onlyPlayers) {
						return false;
					}
					if (string.IsNullOrEmpty (strFilter)) {
						return false;
					}
					return x.ShortPrefabName.Contains (strFilter) ? true : false;
				})
				select x as BaseEntity).ToArray ();
		}

		public static BaseEntity[] FindTargetsOwnedBy (ulong ownedBy, string strFilter)
		{
			bool hasFilter = !string.IsNullOrEmpty (strFilter);
			return (from x in BaseNetworkable.serverEntities.Where (delegate(BaseNetworkable x) {
					if (x is BaseEntity baseEntity) {
						if (baseEntity.OwnerID != ownedBy) {
							return false;
						}
						if (!hasFilter || baseEntity.ShortPrefabName.Contains (strFilter)) {
							return true;
						}
					}
					return false;
				})
				select x as BaseEntity).ToArray ();
		}

		public static BaseEntity[] FindTargetsAuthedTo (ulong authId, string strFilter)
		{
			bool hasFilter = !string.IsNullOrEmpty (strFilter);
			return (from x in BaseNetworkable.serverEntities.Where (delegate(BaseNetworkable x) {
					if (x is BuildingPrivlidge buildingPrivlidge) {
						if (!buildingPrivlidge.IsAuthed (authId)) {
							return false;
						}
						if (!hasFilter || x.ShortPrefabName.Contains (strFilter)) {
							return true;
						}
					} else if (x is AutoTurret autoTurret) {
						if (!autoTurret.IsAuthed (authId)) {
							return false;
						}
						if (!hasFilter || x.ShortPrefabName.Contains (strFilter)) {
							return true;
						}
					} else if (x is CodeLock codeLock) {
						if (!codeLock.whitelistPlayers.Contains (authId)) {
							return false;
						}
						if (!hasFilter || x.ShortPrefabName.Contains (strFilter)) {
							return true;
						}
					}
					return false;
				})
				select x as BaseEntity).ToArray ();
		}

		public static T[] FindAll<T> () where T : BaseEntity
		{
			return BaseNetworkable.serverEntities.OfType<T> ().ToArray ();
		}
	}

	public enum GiveItemReason
	{
		Generic,
		ResourceHarvested,
		PickedUp,
		Crafted
	}

	private static Queue<BaseEntity> globalBroadcastQueue = new Queue<BaseEntity> ();

	private static uint globalBroadcastProtocol = 0u;

	private uint broadcastProtocol = 0u;

	private List<EntityLink> links = new List<EntityLink> ();

	private bool linkedToNeighbours = false;

	[NonSerialized]
	public BaseEntity creatorEntity;

	private int ticksSinceStopped = 0;

	private int doneMovingWithoutARigidBodyCheck = 1;

	private bool isCallingUpdateNetworkGroup;

	private EntityRef[] entitySlots = new EntityRef[8];

	protected List<TriggerBase> triggers = null;

	protected bool isVisible = true;

	protected bool isAnimatorVisible = true;

	protected bool isShadowVisible = true;

	protected OccludeeSphere localOccludee = new OccludeeSphere (-1);

	[Header ("BaseEntity")]
	public Bounds bounds;

	public GameObjectRef impactEffect;

	public bool enableSaving = true;

	public bool syncPosition;

	public Model model;

	[InspectorFlags]
	public Flags flags = (Flags)0;

	[NonSerialized]
	public uint parentBone;

	[NonSerialized]
	public ulong skinID;

	private EntityComponentBase[] _components;

	[HideInInspector]
	public bool HasBrain = false;

	[NonSerialized]
	protected string _name = null;

	private Spawnable _spawnable;

	public static HashSet<BaseEntity> saveList = new HashSet<BaseEntity> ();

	public virtual float RealisticMass => 100f;

	public float radiationLevel {
		get {
			if (triggers == null) {
				return 0f;
			}
			float num = 0f;
			for (int i = 0; i < triggers.Count; i++) {
				TriggerRadiation triggerRadiation = triggers [i] as TriggerRadiation;
				if (!(triggerRadiation == null)) {
					Vector3 position = GetNetworkPosition ();
					BaseEntity baseEntity = GetParentEntity ();
					if (baseEntity != null) {
						position = baseEntity.transform.TransformPoint (position);
					}
					num = Mathf.Max (num, triggerRadiation.GetRadiation (position, RadiationProtection ()));
				}
			}
			return num;
		}
	}

	public float currentTemperature {
		get {
			float num = Climate.GetTemperature (base.transform.position);
			if (triggers == null) {
				return num;
			}
			for (int i = 0; i < triggers.Count; i++) {
				TriggerTemperature triggerTemperature = triggers [i] as TriggerTemperature;
				if (!(triggerTemperature == null)) {
					num = triggerTemperature.WorkoutTemperature (GetNetworkPosition (), num);
				}
			}
			return num;
		}
	}

	public float currentEnvironmentalWetness {
		get {
			if (triggers == null) {
				return 0f;
			}
			float num = 0f;
			Vector3 networkPosition = GetNetworkPosition ();
			foreach (TriggerBase trigger in triggers) {
				if (trigger is TriggerWetness triggerWetness) {
					num += triggerWetness.WorkoutWetness (networkPosition);
				}
			}
			return Mathf.Clamp01 (num);
		}
	}

	protected virtual float PositionTickRate => 0.1f;

	protected virtual bool PositionTickFixedTime => false;

	public virtual Vector3 ServerPosition {
		get {
			return base.transform.localPosition;
		}
		set {
			if (!(base.transform.localPosition == value)) {
				base.transform.localPosition = value;
				base.transform.hasChanged = true;
			}
		}
	}

	public virtual Quaternion ServerRotation {
		get {
			return base.transform.localRotation;
		}
		set {
			if (!(base.transform.localRotation == value)) {
				base.transform.localRotation = value;
				base.transform.hasChanged = true;
			}
		}
	}

	public virtual TraitFlag Traits => TraitFlag.None;

	public float Weight { get; protected set; }

	public EntityComponentBase[] Components => _components ?? (_components = GetComponentsInChildren<EntityComponentBase> (includeInactive: true));

	public virtual bool IsNpc => false;

	public ulong OwnerID { get; set; }

	public override bool OnRpcMessage (BasePlayer player, uint rpc, Message msg)
	{
		using (TimeWarning.New ("BaseEntity.OnRpcMessage")) {
			if (rpc == 1552640099 && player != null) {
				Assert.IsTrue (player.isServer, "SV_RPC Message is using a clientside player!");
				if (ConVar.Global.developer > 2) {
					Debug.Log (string.Concat ("SV_RPCMessage: ", player, " - BroadcastSignalFromClient "));
				}
				using (TimeWarning.New ("BroadcastSignalFromClient")) {
					using (TimeWarning.New ("Conditions")) {
						if (!RPC_Server.FromOwner.Test (1552640099u, "BroadcastSignalFromClient", this, player)) {
							return true;
						}
					}
					try {
						using (TimeWarning.New ("Call")) {
							RPCMessage rPCMessage = default(RPCMessage);
							rPCMessage.connection = msg.connection;
							rPCMessage.player = player;
							rPCMessage.read = msg.read;
							RPCMessage msg2 = rPCMessage;
							BroadcastSignalFromClient (msg2);
						}
					} catch (Exception exception) {
						Debug.LogException (exception);
						player.Kick ("RPC Error in BroadcastSignalFromClient");
					}
				}
				return true;
			}
			if (rpc == 3645147041u && player != null) {
				Assert.IsTrue (player.isServer, "SV_RPC Message is using a clientside player!");
				if (ConVar.Global.developer > 2) {
					Debug.Log (string.Concat ("SV_RPCMessage: ", player, " - SV_RequestFile "));
				}
				using (TimeWarning.New ("SV_RequestFile")) {
					try {
						using (TimeWarning.New ("Call")) {
							RPCMessage rPCMessage = default(RPCMessage);
							rPCMessage.connection = msg.connection;
							rPCMessage.player = player;
							rPCMessage.read = msg.read;
							RPCMessage msg3 = rPCMessage;
							SV_RequestFile (msg3);
						}
					} catch (Exception exception2) {
						Debug.LogException (exception2);
						player.Kick ("RPC Error in SV_RequestFile");
					}
				}
				return true;
			}
		}
		return base.OnRpcMessage (player, rpc, msg);
	}

	public virtual void OnCollision (Collision collision, BaseEntity hitEntity)
	{
		throw new NotImplementedException ();
	}

	protected void ReceiveCollisionMessages (bool b)
	{
		if (b) {
			base.gameObject.transform.GetOrAddComponent<EntityCollisionMessage> ();
		} else {
			base.gameObject.transform.RemoveComponent<EntityCollisionMessage> ();
		}
	}

	public virtual void DebugServer (int rep, float time)
	{
		DebugText (base.transform.position + Vector3.up * 1f, $"{net?.ID.Value ?? 0}: {base.name}\n{DebugText ()}", Color.white, time);
	}

	public virtual string DebugText ()
	{
		return "";
	}

	public void OnDebugStart ()
	{
		EntityDebug entityDebug = base.gameObject.GetComponent<EntityDebug> ();
		if (entityDebug == null) {
			entityDebug = base.gameObject.AddComponent<EntityDebug> ();
		}
		entityDebug.enabled = true;
	}

	protected void DebugText (Vector3 pos, string str, Color color, float time)
	{
		if (base.isServer) {
			ConsoleNetwork.BroadcastToAllClients ("ddraw.text", time, color, pos, str);
		}
	}

	public bool HasFlag (Flags f)
	{
		return (flags & f) == f;
	}

	public bool HasAny (Flags f)
	{
		return (flags & f) > (Flags)0;
	}

	public bool ParentHasFlag (Flags f)
	{
		BaseEntity baseEntity = GetParentEntity ();
		if (baseEntity == null) {
			return false;
		}
		return baseEntity.HasFlag (f);
	}

	public void SetFlag (Flags f, bool b, bool recursive = false, bool networkupdate = true)
	{
		Flags old = flags;
		if (b) {
			if (HasFlag (f)) {
				return;
			}
			flags |= f;
		} else {
			if (!HasFlag (f)) {
				return;
			}
			flags &= ~f;
		}
		OnFlagsChanged (old, flags);
		if (networkupdate) {
			SendNetworkUpdate ();
		} else {
			InvalidateNetworkCache ();
		}
		if (recursive && children != null) {
			for (int i = 0; i < children.Count; i++) {
				children [i].SetFlag (f, b, recursive: true);
			}
		}
	}

	public bool IsOn ()
	{
		return HasFlag (Flags.On);
	}

	public bool IsOpen ()
	{
		return HasFlag (Flags.Open);
	}

	public bool IsOnFire ()
	{
		return HasFlag (Flags.OnFire);
	}

	public bool IsLocked ()
	{
		return HasFlag (Flags.Locked);
	}

	public override bool IsDebugging ()
	{
		return HasFlag (Flags.Debugging);
	}

	public bool IsDisabled ()
	{
		return HasFlag (Flags.Disabled) || ParentHasFlag (Flags.Disabled);
	}

	public bool IsBroken ()
	{
		return HasFlag (Flags.Broken);
	}

	public bool IsBusy ()
	{
		return HasFlag (Flags.Busy);
	}

	public override string GetLogColor ()
	{
		if (base.isServer) {
			return "cyan";
		}
		return "yellow";
	}

	public virtual void OnFlagsChanged (Flags old, Flags next)
	{
		if (IsDebugging () && (old & Flags.Debugging) != (next & Flags.Debugging)) {
			OnDebugStart ();
		}
	}

	protected void SendNetworkUpdate_Flags ()
	{
		if (Rust.Application.isLoading || Rust.Application.isLoadingSave || base.IsDestroyed || net == null || !isSpawned) {
			return;
		}
		using (TimeWarning.New ("SendNetworkUpdate_Flags")) {
			LogEntry (LogEntryType.Network, 2, "SendNetworkUpdate_Flags");
			List<Connection> subscribers = GetSubscribers ();
			if (subscribers != null && subscribers.Count > 0) {
				NetWrite netWrite = Network.Net.sv.StartWrite ();
				Profiler.BeginSample ("Write");
				netWrite.PacketID (Message.Type.EntityFlags);
				netWrite.EntityID (net.ID);
				netWrite.Int32 ((int)flags);
				Profiler.EndSample ();
				Profiler.BeginSample ("SendInfo");
				SendInfo info = new SendInfo (subscribers);
				Profiler.EndSample ();
				Profiler.BeginSample ("Send");
				netWrite.Send (info);
				Profiler.EndSample ();
			}
			base.gameObject.SendOnSendNetworkUpdate (this);
		}
	}

	public bool IsOccupied (Socket_Base socket)
	{
		return FindLink (socket)?.IsOccupied () ?? false;
	}

	public bool IsOccupied (string socketName)
	{
		return FindLink (socketName)?.IsOccupied () ?? false;
	}

	public EntityLink FindLink (Socket_Base socket)
	{
		List<EntityLink> entityLinks = GetEntityLinks ();
		for (int i = 0; i < entityLinks.Count; i++) {
			if (entityLinks [i].socket == socket) {
				return entityLinks [i];
			}
		}
		return null;
	}

	public EntityLink FindLink (string socketName)
	{
		List<EntityLink> entityLinks = GetEntityLinks ();
		for (int i = 0; i < entityLinks.Count; i++) {
			if (entityLinks [i].socket.socketName == socketName) {
				return entityLinks [i];
			}
		}
		return null;
	}

	public EntityLink FindLink (string[] socketNames)
	{
		List<EntityLink> entityLinks = GetEntityLinks ();
		for (int i = 0; i < entityLinks.Count; i++) {
			for (int j = 0; j < socketNames.Length; j++) {
				if (entityLinks [i].socket.socketName == socketNames [j]) {
					return entityLinks [i];
				}
			}
		}
		return null;
	}

	public T FindLinkedEntity<T> () where T : BaseEntity
	{
		List<EntityLink> entityLinks = GetEntityLinks ();
		for (int i = 0; i < entityLinks.Count; i++) {
			EntityLink entityLink = entityLinks [i];
			for (int j = 0; j < entityLink.connections.Count; j++) {
				EntityLink entityLink2 = entityLink.connections [j];
				if (entityLink2.owner is T) {
					return entityLink2.owner as T;
				}
			}
		}
		return null;
	}

	public void EntityLinkMessage<T> (Action<T> action) where T : BaseEntity
	{
		List<EntityLink> entityLinks = GetEntityLinks ();
		for (int i = 0; i < entityLinks.Count; i++) {
			EntityLink entityLink = entityLinks [i];
			for (int j = 0; j < entityLink.connections.Count; j++) {
				EntityLink entityLink2 = entityLink.connections [j];
				if (entityLink2.owner is T) {
					action (entityLink2.owner as T);
				}
			}
		}
	}

	public void EntityLinkBroadcast<T, S> (Action<T> action, Func<S, bool> canTraverseSocket) where T : BaseEntity where S : Socket_Base
	{
		globalBroadcastProtocol++;
		globalBroadcastQueue.Clear ();
		broadcastProtocol = globalBroadcastProtocol;
		globalBroadcastQueue.Enqueue (this);
		if (this is T) {
			action (this as T);
		}
		while (globalBroadcastQueue.Count > 0) {
			BaseEntity baseEntity = globalBroadcastQueue.Dequeue ();
			List<EntityLink> entityLinks = baseEntity.GetEntityLinks ();
			for (int i = 0; i < entityLinks.Count; i++) {
				EntityLink entityLink = entityLinks [i];
				if (!(entityLink.socket is S) || !canTraverseSocket (entityLink.socket as S)) {
					continue;
				}
				for (int j = 0; j < entityLink.connections.Count; j++) {
					BaseEntity owner = entityLink.connections [j].owner;
					if (owner.broadcastProtocol != globalBroadcastProtocol) {
						owner.broadcastProtocol = globalBroadcastProtocol;
						globalBroadcastQueue.Enqueue (owner);
						if (owner is T) {
							action (owner as T);
						}
					}
				}
			}
		}
	}

	public void EntityLinkBroadcast<T> (Action<T> action) where T : BaseEntity
	{
		globalBroadcastProtocol++;
		globalBroadcastQueue.Clear ();
		broadcastProtocol = globalBroadcastProtocol;
		globalBroadcastQueue.Enqueue (this);
		if (this is T) {
			action (this as T);
		}
		while (globalBroadcastQueue.Count > 0) {
			BaseEntity baseEntity = globalBroadcastQueue.Dequeue ();
			List<EntityLink> entityLinks = baseEntity.GetEntityLinks ();
			for (int i = 0; i < entityLinks.Count; i++) {
				EntityLink entityLink = entityLinks [i];
				for (int j = 0; j < entityLink.connections.Count; j++) {
					BaseEntity owner = entityLink.connections [j].owner;
					if (owner.broadcastProtocol != globalBroadcastProtocol) {
						owner.broadcastProtocol = globalBroadcastProtocol;
						globalBroadcastQueue.Enqueue (owner);
						if (owner is T) {
							action (owner as T);
						}
					}
				}
			}
		}
	}

	public void EntityLinkBroadcast ()
	{
		globalBroadcastProtocol++;
		globalBroadcastQueue.Clear ();
		broadcastProtocol = globalBroadcastProtocol;
		globalBroadcastQueue.Enqueue (this);
		while (globalBroadcastQueue.Count > 0) {
			BaseEntity baseEntity = globalBroadcastQueue.Dequeue ();
			List<EntityLink> entityLinks = baseEntity.GetEntityLinks ();
			for (int i = 0; i < entityLinks.Count; i++) {
				EntityLink entityLink = entityLinks [i];
				for (int j = 0; j < entityLink.connections.Count; j++) {
					BaseEntity owner = entityLink.connections [j].owner;
					if (owner.broadcastProtocol != globalBroadcastProtocol) {
						owner.broadcastProtocol = globalBroadcastProtocol;
						globalBroadcastQueue.Enqueue (owner);
					}
				}
			}
		}
	}

	public bool ReceivedEntityLinkBroadcast ()
	{
		return broadcastProtocol == globalBroadcastProtocol;
	}

	public List<EntityLink> GetEntityLinks (bool linkToNeighbours = true)
	{
		if (Rust.Application.isLoadingSave) {
			return links;
		}
		if (!linkedToNeighbours && linkToNeighbours) {
			LinkToNeighbours ();
		}
		return links;
	}

	private void LinkToEntity (BaseEntity other)
	{
		if (this == other || links.Count == 0 || other.links.Count == 0) {
			return;
		}
		using (TimeWarning.New ("LinkToEntity")) {
			for (int i = 0; i < links.Count; i++) {
				EntityLink entityLink = links [i];
				for (int j = 0; j < other.links.Count; j++) {
					EntityLink entityLink2 = other.links [j];
					if (entityLink.CanConnect (entityLink2)) {
						if (!entityLink.Contains (entityLink2)) {
							entityLink.Add (entityLink2);
						}
						if (!entityLink2.Contains (entityLink)) {
							entityLink2.Add (entityLink);
						}
					}
				}
			}
		}
	}

	private void LinkToNeighbours ()
	{
		if (links.Count == 0) {
			return;
		}
		linkedToNeighbours = true;
		using (TimeWarning.New ("LinkToNeighbours")) {
			List<BaseEntity> obj = Facepunch.Pool.GetList<BaseEntity> ();
			OBB oBB = WorldSpaceBounds ();
			Vis.Entities (oBB.position, oBB.extents.magnitude + 1f, obj);
			for (int i = 0; i < obj.Count; i++) {
				BaseEntity baseEntity = obj [i];
				if (baseEntity.isServer == base.isServer) {
					LinkToEntity (baseEntity);
				}
			}
			Facepunch.Pool.FreeList (ref obj);
		}
	}

	private void InitEntityLinks ()
	{
		using (TimeWarning.New ("InitEntityLinks")) {
			if (base.isServer) {
				links.AddLinks (this, PrefabAttribute.server.FindAll<Socket_Base> (prefabID));
			}
		}
	}

	private void FreeEntityLinks ()
	{
		using (TimeWarning.New ("FreeEntityLinks")) {
			links.FreeLinks ();
			linkedToNeighbours = false;
		}
	}

	public void RefreshEntityLinks ()
	{
		using (TimeWarning.New ("RefreshEntityLinks")) {
			links.ClearLinks ();
			LinkToNeighbours ();
		}
	}

	[RPC_Server]
	public void SV_RequestFile (RPCMessage msg)
	{
		uint num = msg.read.UInt32 ();
		FileStorage.Type type = (FileStorage.Type)msg.read.UInt8 ();
		string funcName = StringPool.Get (msg.read.UInt32 ());
		uint num2 = ((msg.read.Unread > 0) ? msg.read.UInt32 () : 0u);
		bool flag = msg.read.Unread > 0 && msg.read.Bit ();
		byte[] array = FileStorage.server.Get (num, type, net.ID, num2);
		if (array == null) {
			if (!flag) {
				return;
			}
			array = Array.Empty<byte> ();
		}
		SendInfo sendInfo = new SendInfo (msg.connection);
		sendInfo.channel = 2;
		sendInfo.method = SendMethod.Reliable;
		SendInfo sendInfo2 = sendInfo;
		ClientRPCEx (sendInfo2, null, funcName, num, (uint)array.Length, array, num2, (byte)type);
	}

	public void SetParent (BaseEntity entity, bool worldPositionStays = false, bool sendImmediate = false)
	{
		SetParent (entity, 0u, worldPositionStays, sendImmediate);
	}

	public void SetParent (BaseEntity entity, string strBone, bool worldPositionStays = false, bool sendImmediate = false)
	{
		SetParent (entity, (!string.IsNullOrEmpty (strBone)) ? StringPool.Get (strBone) : 0u, worldPositionStays, sendImmediate);
	}

	public bool HasChild (BaseEntity c)
	{
		if (c == this) {
			return true;
		}
		BaseEntity baseEntity = c.GetParentEntity ();
		if (baseEntity != null) {
			return HasChild (baseEntity);
		}
		return false;
	}

	public void SetParent (BaseEntity entity, uint boneID, bool worldPositionStays = false, bool sendImmediate = false)
	{
		if (entity != null) {
			if (entity == this) {
				Debug.LogError ("Trying to parent to self " + this, base.gameObject);
				return;
			}
			if (HasChild (entity)) {
				Debug.LogError ("Trying to parent to child " + this, base.gameObject);
				return;
			}
		}
		LogEntry (LogEntryType.Hierarchy, 2, "SetParent {0} {1}", entity, boneID);
		BaseEntity baseEntity = GetParentEntity ();
		if ((bool)baseEntity) {
			baseEntity.RemoveChild (this);
		}
		if (base.limitNetworking && baseEntity != null && baseEntity != entity) {
			BasePlayer basePlayer = baseEntity as BasePlayer;
			if (basePlayer.IsValid ()) {
				DestroyOnClient (basePlayer.net.connection);
			}
		}
		if (entity == null) {
			OnParentChanging (baseEntity, null);
			parentEntity.Set (null);
			base.transform.SetParent (null, worldPositionStays);
			parentBone = 0u;
			UpdateNetworkGroup ();
			if (sendImmediate) {
				SendNetworkUpdateImmediate ();
				SendChildrenNetworkUpdateImmediate ();
			} else {
				SendNetworkUpdate ();
				SendChildrenNetworkUpdate ();
			}
			return;
		}
		Debug.Assert (entity.isServer, "SetParent - child should be a SERVER entity");
		Debug.Assert (entity.net != null, "Setting parent to entity that hasn't spawned yet! (net is null)");
		Debug.Assert (entity.net.ID.IsValid, "Setting parent to entity that hasn't spawned yet! (id = 0)");
		entity.AddChild (this);
		OnParentChanging (baseEntity, entity);
		parentEntity.Set (entity);
		if (boneID != 0 && boneID != StringPool.closest) {
			base.transform.SetParent (entity.FindBone (StringPool.Get (boneID)), worldPositionStays);
		} else {
			base.transform.SetParent (entity.transform, worldPositionStays);
		}
		parentBone = boneID;
		UpdateNetworkGroup ();
		if (sendImmediate) {
			SendNetworkUpdateImmediate ();
			SendChildrenNetworkUpdateImmediate ();
		} else {
			SendNetworkUpdate ();
			SendChildrenNetworkUpdate ();
		}
	}

	private void DestroyOnClient (Connection connection)
	{
		if (children != null) {
			foreach (BaseEntity child in children) {
				child.DestroyOnClient (connection);
			}
		}
		if (Network.Net.sv.IsConnected ()) {
			NetWrite netWrite = Network.Net.sv.StartWrite ();
			netWrite.PacketID (Message.Type.EntityDestroy);
			netWrite.EntityID (net.ID);
			netWrite.UInt8 (0);
			netWrite.Send (new SendInfo (connection));
			LogEntry (LogEntryType.Network, 2, "EntityDestroy");
		}
	}

	private void SendChildrenNetworkUpdate ()
	{
		if (children == null) {
			return;
		}
		foreach (BaseEntity child in children) {
			child.UpdateNetworkGroup ();
			child.SendNetworkUpdate ();
		}
	}

	private void SendChildrenNetworkUpdateImmediate ()
	{
		if (children == null) {
			return;
		}
		foreach (BaseEntity child in children) {
			child.UpdateNetworkGroup ();
			child.SendNetworkUpdateImmediate ();
		}
	}

	public virtual void SwitchParent (BaseEntity ent)
	{
		Log ("SwitchParent Missed " + ent);
	}

	public virtual void OnParentChanging (BaseEntity oldParent, BaseEntity newParent)
	{
		Rigidbody component = GetComponent<Rigidbody> ();
		if ((bool)component) {
			if (oldParent != null && oldParent.GetComponent<Rigidbody> () == null) {
				component.velocity += oldParent.GetWorldVelocity ();
			}
			if (newParent != null && newParent.GetComponent<Rigidbody> () == null) {
				component.velocity -= newParent.GetWorldVelocity ();
			}
		}
	}

	public virtual BuildingPrivlidge GetBuildingPrivilege ()
	{
		return GetBuildingPrivilege (WorldSpaceBounds ());
	}

	public BuildingPrivlidge GetBuildingPrivilege (OBB obb)
	{
		Profiler.BeginSample ("GetBuildingPrivilege");
		BuildingBlock other = null;
		BuildingPrivlidge result = null;
		List<BuildingBlock> obj = Facepunch.Pool.GetList<BuildingBlock> ();
		Vis.Entities (obb.position, 16f + obb.extents.magnitude, obj, 2097152);
		for (int i = 0; i < obj.Count; i++) {
			BuildingBlock buildingBlock = obj [i];
			if (buildingBlock.isServer != base.isServer || !buildingBlock.IsOlderThan (other) || obb.Distance (buildingBlock.WorldSpaceBounds ()) > 16f) {
				continue;
			}
			BuildingManager.Building building = buildingBlock.GetBuilding ();
			if (building != null) {
				BuildingPrivlidge dominatingBuildingPrivilege = building.GetDominatingBuildingPrivilege ();
				if (!(dominatingBuildingPrivilege == null)) {
					other = buildingBlock;
					result = dominatingBuildingPrivilege;
				}
			}
		}
		Facepunch.Pool.FreeList (ref obj);
		Profiler.EndSample ();
		return result;
	}

	public void SV_RPCMessage (uint nameID, Message message)
	{
		Assert.IsTrue (base.isServer, "Should be server!");
		BasePlayer basePlayer = message.Player ();
		if (!basePlayer.IsValid ()) {
			if (ConVar.Global.developer > 0) {
				Debug.Log ("SV_RPCMessage: From invalid player " + basePlayer);
			}
		} else if (basePlayer.isStalled) {
			if (ConVar.Global.developer > 0) {
				Debug.Log ("SV_RPCMessage: player is stalled " + basePlayer);
			}
		} else if (!OnRpcMessage (basePlayer, nameID, message)) {
			for (int i = 0; i < Components.Length && !Components [i].OnRpcMessage (basePlayer, nameID, message); i++) {
			}
		}
	}

	public void ClientRPCPlayer<T1, T2, T3, T4, T5> (Connection sourceConnection, BasePlayer player, string funcName, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
	{
		if (Network.Net.sv.IsConnected () && net != null && player.net.connection != null) {
			ClientRPCEx (new SendInfo (player.net.connection), sourceConnection, funcName, arg1, arg2, arg3, arg4, arg5);
		}
	}

	public void ClientRPCPlayer<T1, T2, T3, T4> (Connection sourceConnection, BasePlayer player, string funcName, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
	{
		if (Network.Net.sv.IsConnected () && net != null && player.net.connection != null) {
			ClientRPCEx (new SendInfo (player.net.connection), sourceConnection, funcName, arg1, arg2, arg3, arg4);
		}
	}

	public void ClientRPCPlayer<T1, T2, T3> (Connection sourceConnection, BasePlayer player, string funcName, T1 arg1, T2 arg2, T3 arg3)
	{
		if (Network.Net.sv.IsConnected () && net != null && player.net.connection != null) {
			ClientRPCEx (new SendInfo (player.net.connection), sourceConnection, funcName, arg1, arg2, arg3);
		}
	}

	public void ClientRPCPlayer<T1, T2> (Connection sourceConnection, BasePlayer player, string funcName, T1 arg1, T2 arg2)
	{
		if (Network.Net.sv.IsConnected () && net != null && player.net.connection != null) {
			ClientRPCEx (new SendInfo (player.net.connection), sourceConnection, funcName, arg1, arg2);
		}
	}

	public void ClientRPCPlayer<T1> (Connection sourceConnection, BasePlayer player, string funcName, T1 arg1)
	{
		if (Network.Net.sv.IsConnected () && net != null && player.net.connection != null) {
			ClientRPCEx (new SendInfo (player.net.connection), sourceConnection, funcName, arg1);
		}
	}

	public void ClientRPCPlayer (Connection sourceConnection, BasePlayer player, string funcName)
	{
		if (Network.Net.sv.IsConnected () && net != null && player.net.connection != null) {
			ClientRPCEx (new SendInfo (player.net.connection), sourceConnection, funcName);
		}
	}

	public void ClientRPC<T1, T2, T3, T4, T5> (Connection sourceConnection, string funcName, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
	{
		if (Network.Net.sv.IsConnected () && net != null && net.group != null) {
			ClientRPCEx (new SendInfo (net.group.subscribers), sourceConnection, funcName, arg1, arg2, arg3, arg4, arg5);
		}
	}

	public void ClientRPC<T1, T2, T3, T4> (Connection sourceConnection, string funcName, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
	{
		if (Network.Net.sv.IsConnected () && net != null && net.group != null) {
			ClientRPCEx (new SendInfo (net.group.subscribers), sourceConnection, funcName, arg1, arg2, arg3, arg4);
		}
	}

	public void ClientRPC<T1, T2, T3> (Connection sourceConnection, string funcName, T1 arg1, T2 arg2, T3 arg3)
	{
		if (Network.Net.sv.IsConnected () && net != null && net.group != null) {
			ClientRPCEx (new SendInfo (net.group.subscribers), sourceConnection, funcName, arg1, arg2, arg3);
		}
	}

	public void ClientRPC<T1, T2> (Connection sourceConnection, string funcName, T1 arg1, T2 arg2)
	{
		if (Network.Net.sv.IsConnected () && net != null && net.group != null) {
			ClientRPCEx (new SendInfo (net.group.subscribers), sourceConnection, funcName, arg1, arg2);
		}
	}

	public void ClientRPC<T1> (Connection sourceConnection, string funcName, T1 arg1)
	{
		if (Network.Net.sv.IsConnected () && net != null && net.group != null) {
			ClientRPCEx (new SendInfo (net.group.subscribers), sourceConnection, funcName, arg1);
		}
	}

	public void ClientRPC (Connection sourceConnection, string funcName)
	{
		if (Network.Net.sv.IsConnected () && net != null && net.group != null) {
			ClientRPCEx (new SendInfo (net.group.subscribers), sourceConnection, funcName);
		}
	}

	public void ClientRPCEx<T1, T2, T3, T4, T5> (SendInfo sendInfo, Connection sourceConnection, string funcName, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
	{
		if (Network.Net.sv.IsConnected () && net != null) {
			NetWrite write = ClientRPCStart (sourceConnection, funcName);
			ClientRPCWrite (write, arg1);
			ClientRPCWrite (write, arg2);
			ClientRPCWrite (write, arg3);
			ClientRPCWrite (write, arg4);
			ClientRPCWrite (write, arg5);
			ClientRPCSend (write, sendInfo);
		}
	}

	public void ClientRPCEx<T1, T2, T3, T4> (SendInfo sendInfo, Connection sourceConnection, string funcName, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
	{
		if (Network.Net.sv.IsConnected () && net != null) {
			NetWrite write = ClientRPCStart (sourceConnection, funcName);
			ClientRPCWrite (write, arg1);
			ClientRPCWrite (write, arg2);
			ClientRPCWrite (write, arg3);
			ClientRPCWrite (write, arg4);
			ClientRPCSend (write, sendInfo);
		}
	}

	public void ClientRPCEx<T1, T2, T3> (SendInfo sendInfo, Connection sourceConnection, string funcName, T1 arg1, T2 arg2, T3 arg3)
	{
		if (Network.Net.sv.IsConnected () && net != null) {
			NetWrite write = ClientRPCStart (sourceConnection, funcName);
			ClientRPCWrite (write, arg1);
			ClientRPCWrite (write, arg2);
			ClientRPCWrite (write, arg3);
			ClientRPCSend (write, sendInfo);
		}
	}

	public void ClientRPCEx<T1, T2> (SendInfo sendInfo, Connection sourceConnection, string funcName, T1 arg1, T2 arg2)
	{
		if (Network.Net.sv.IsConnected () && net != null) {
			NetWrite write = ClientRPCStart (sourceConnection, funcName);
			ClientRPCWrite (write, arg1);
			ClientRPCWrite (write, arg2);
			ClientRPCSend (write, sendInfo);
		}
	}

	public void ClientRPCEx<T1> (SendInfo sendInfo, Connection sourceConnection, string funcName, T1 arg1)
	{
		if (Network.Net.sv.IsConnected () && net != null) {
			NetWrite write = ClientRPCStart (sourceConnection, funcName);
			ClientRPCWrite (write, arg1);
			ClientRPCSend (write, sendInfo);
		}
	}

	public void ClientRPCEx (SendInfo sendInfo, Connection sourceConnection, string funcName)
	{
		if (Network.Net.sv.IsConnected () && net != null) {
			NetWrite write = ClientRPCStart (sourceConnection, funcName);
			ClientRPCSend (write, sendInfo);
		}
	}

	public void ClientRPCPlayerAndSpectators (Connection sourceConnection, BasePlayer player, string funcName)
	{
		if (!Network.Net.sv.IsConnected () || player.net == null || player.net.connection == null) {
			return;
		}
		ClientRPCEx (new SendInfo (player.net.connection), sourceConnection, funcName);
		if (!player.IsBeingSpectated || player.children == null) {
			return;
		}
		foreach (BaseEntity child in player.children) {
			if (child is BasePlayer player2) {
				ClientRPCPlayer (sourceConnection, player2, funcName);
			}
		}
	}

	public void ClientRPCPlayerAndSpectators<T1> (Connection sourceConnection, BasePlayer player, string funcName, T1 arg1)
	{
		if (!Network.Net.sv.IsConnected () || player.net == null || player.net.connection == null) {
			return;
		}
		ClientRPCEx (new SendInfo (player.net.connection), sourceConnection, funcName, arg1);
		if (!player.IsBeingSpectated || player.children == null) {
			return;
		}
		foreach (BaseEntity child in player.children) {
			if (child is BasePlayer player2) {
				ClientRPCPlayer (sourceConnection, player2, funcName, arg1);
			}
		}
	}

	public void ClientRPCPlayerAndSpectators<T1, T2> (Connection sourceConnection, BasePlayer player, string funcName, T1 arg1, T2 arg2)
	{
		if (!Network.Net.sv.IsConnected () || player.net == null || player.net.connection == null) {
			return;
		}
		ClientRPCPlayer (sourceConnection, player, funcName, arg1, arg2);
		if (!player.IsBeingSpectated || player.children == null) {
			return;
		}
		foreach (BaseEntity child in children) {
			if (child is BasePlayer player2) {
				ClientRPCPlayer (sourceConnection, player2, funcName, arg1, arg2);
			}
		}
	}

	public void ClientRPCPlayerAndSpectators<T1, T2, T3> (Connection sourceConnection, BasePlayer player, string funcName, T1 arg1, T2 arg2, T3 arg3)
	{
		if (!Network.Net.sv.IsConnected () || player.net == null || player.net.connection == null) {
			return;
		}
		ClientRPCPlayer (sourceConnection, player, funcName, arg1, arg2, arg3);
		if (!player.IsBeingSpectated || player.children == null) {
			return;
		}
		foreach (BaseEntity child in player.children) {
			if (child is BasePlayer player2) {
				ClientRPCPlayer (sourceConnection, player2, funcName, arg1, arg2, arg3);
			}
		}
	}

	private NetWrite ClientRPCStart (Connection sourceConnection, string funcName)
	{
		Profiler.BeginSample ("ClientRPC");
		Profiler.BeginSample (funcName);
		NetWrite netWrite = Network.Net.sv.StartWrite ();
		Profiler.BeginSample ("Headers");
		netWrite.PacketID (Message.Type.RPCMessage);
		netWrite.EntityID (net.ID);
		netWrite.UInt32 (StringPool.Get (funcName));
		netWrite.UInt64 (sourceConnection?.userid ?? 0);
		Profiler.EndSample ();
		return netWrite;
	}

	private void ClientRPCWrite<T> (NetWrite write, T arg)
	{
		Profiler.BeginSample ("Objects");
		write.WriteObject (arg);
		Profiler.EndSample ();
	}

	private void ClientRPCSend (NetWrite write, SendInfo sendInfo)
	{
		write.Send (sendInfo);
		Profiler.EndSample ();
		Profiler.EndSample ();
	}

	public void ClientRPCPlayerList<T1> (Connection sourceConnection, BasePlayer player, string funcName, List<T1> list)
	{
		if (!Network.Net.sv.IsConnected () || net == null || player.net.connection == null) {
			return;
		}
		NetWrite write = ClientRPCStart (sourceConnection, funcName);
		ClientRPCWrite (write, list.Count);
		foreach (T1 item in list) {
			ClientRPCWrite (write, item);
		}
		ClientRPCSend (write, new SendInfo (player.net.connection) {
			priority = Priority.Immediate
		});
	}

	public virtual float RadiationProtection ()
	{
		return 0f;
	}

	public virtual float RadiationExposureFraction ()
	{
		return 1f;
	}

	public virtual void SetCreatorEntity (BaseEntity newCreatorEntity)
	{
		creatorEntity = newCreatorEntity;
	}

	public virtual Vector3 GetLocalVelocityServer ()
	{
		return Vector3.zero;
	}

	public virtual Quaternion GetAngularVelocityServer ()
	{
		return Quaternion.identity;
	}

	public void EnableGlobalBroadcast (bool wants)
	{
		if (globalBroadcast != wants) {
			globalBroadcast = wants;
			UpdateNetworkGroup ();
		}
	}

	public void EnableSaving (bool wants)
	{
		if (enableSaving == wants) {
			return;
		}
		enableSaving = wants;
		if (enableSaving) {
			if (!saveList.Contains (this)) {
				saveList.Add (this);
			}
		} else {
			saveList.Remove (this);
		}
	}

	public override void ServerInit ()
	{
		_spawnable = GetComponent<Spawnable> ();
		base.ServerInit ();
		if (enableSaving && !saveList.Contains (this)) {
			saveList.Add (this);
		}
		if (flags != 0) {
			OnFlagsChanged ((Flags)0, flags);
		}
		if (syncPosition && PositionTickRate >= 0f) {
			if (PositionTickFixedTime) {
				InvokeRepeatingFixedTime (NetworkPositionTick);
			} else {
				InvokeRandomized (NetworkPositionTick, PositionTickRate, PositionTickRate - PositionTickRate * 0.05f, PositionTickRate * 0.05f);
			}
		}
		Profiler.BeginSample ("Query.Server.Add");
		Query.Server.Add (this);
		Profiler.EndSample ();
	}

	public virtual void OnSensation (Sensation sensation)
	{
	}

	protected void NetworkPositionTick ()
	{
		if (!base.transform.hasChanged) {
			if (ticksSinceStopped >= 6) {
				return;
			}
			ticksSinceStopped++;
		} else {
			ticksSinceStopped = 0;
		}
		TransformChanged ();
		base.transform.hasChanged = false;
	}

	private void TransformChanged ()
	{
		if (Query.Server != null) {
			Profiler.BeginSample ("Query.Server.Update");
			Query.Server.Move (this);
			Profiler.EndSample ();
		}
		if (net == null) {
			return;
		}
		Profiler.BeginSample ("InvalidateNetworkCache");
		InvalidateNetworkCache ();
		Profiler.EndSample ();
		Profiler.BeginSample ("ValidBounds.Test");
		if (!globalBroadcast && !ValidBounds.Test (base.transform.position)) {
			OnInvalidPosition ();
			Profiler.EndSample ();
			return;
		}
		Profiler.EndSample ();
		if (syncPosition) {
			if (!isCallingUpdateNetworkGroup) {
				Invoke (UpdateNetworkGroup, 5f);
				isCallingUpdateNetworkGroup = true;
			}
			Profiler.BeginSample ("SendNetworkUpdate_Position");
			SendNetworkUpdate_Position ();
			OnPositionalNetworkUpdate ();
			Profiler.EndSample ();
		}
	}

	public virtual void OnPositionalNetworkUpdate ()
	{
	}

	public void DoMovingWithoutARigidBodyCheck ()
	{
		if (doneMovingWithoutARigidBodyCheck <= 10) {
			doneMovingWithoutARigidBodyCheck++;
			if (doneMovingWithoutARigidBodyCheck >= 10 && !(GetComponent<Collider> () == null) && GetComponent<Rigidbody> () == null) {
				Debug.LogWarning (string.Concat ("Entity moving without a rigid body! (", base.gameObject, ")"), this);
			}
		}
	}

	public override void Spawn ()
	{
		base.Spawn ();
		if (base.isServer) {
			base.gameObject.BroadcastOnParentSpawning ();
		}
	}

	public void OnParentSpawning ()
	{
		if (net != null || base.IsDestroyed) {
			return;
		}
		if (Rust.Application.isLoadingSave) {
			UnityEngine.Object.Destroy (base.gameObject);
			return;
		}
		if (GameManager.server.preProcessed.NeedsProcessing (base.gameObject)) {
			GameManager.server.preProcessed.ProcessObject (null, base.gameObject, resetLocalTransform: false);
		}
		BaseEntity baseEntity = ((base.transform.parent != null) ? base.transform.parent.GetComponentInParent<BaseEntity> () : null);
		Spawn ();
		if (baseEntity != null) {
			SetParent (baseEntity, worldPositionStays: true);
		}
	}

	public void SpawnAsMapEntity ()
	{
		if (net != null || base.IsDestroyed) {
			return;
		}
		BaseEntity baseEntity = ((base.transform.parent != null) ? base.transform.parent.GetComponentInParent<BaseEntity> () : null);
		if (baseEntity == null) {
			if (GameManager.server.preProcessed.NeedsProcessing (base.gameObject)) {
				GameManager.server.preProcessed.ProcessObject (null, base.gameObject, resetLocalTransform: false);
			}
			base.transform.parent = null;
			SceneManager.MoveGameObjectToScene (base.gameObject, Rust.Server.EntityScene);
			base.gameObject.SetActive (value: true);
			Spawn ();
		}
	}

	public virtual void PostMapEntitySpawn ()
	{
	}

	internal override void DoServerDestroy ()
	{
		CancelInvoke (NetworkPositionTick);
		saveList.Remove (this);
		RemoveFromTriggers ();
		if (children != null) {
			BaseEntity[] array = children.ToArray ();
			foreach (BaseEntity baseEntity in array) {
				baseEntity.OnParentRemoved ();
			}
		}
		SetParent (null, worldPositionStays: true);
		Profiler.BeginSample ("Query.Server.Remove");
		Query.Server.Remove (this);
		Profiler.EndSample ();
		base.DoServerDestroy ();
	}

	internal virtual void OnParentRemoved ()
	{
		Kill ();
	}

	public virtual void OnInvalidPosition ()
	{
		Debug.Log (string.Concat ("Invalid Position: ", this, " ", base.transform.position, " (destroying)"));
		Kill ();
	}

	public BaseCorpse DropCorpse (string strCorpsePrefab)
	{
		Assert.IsTrue (base.isServer, "DropCorpse called on client!");
		if (!ConVar.Server.corpses) {
			return null;
		}
		if (string.IsNullOrEmpty (strCorpsePrefab)) {
			return null;
		}
		BaseCorpse baseCorpse = GameManager.server.CreateEntity (strCorpsePrefab) as BaseCorpse;
		if (baseCorpse == null) {
			Debug.LogWarning (string.Concat ("Error creating corpse: ", base.gameObject, " - ", strCorpsePrefab));
			return null;
		}
		baseCorpse.InitCorpse (this);
		return baseCorpse;
	}

	public override void UpdateNetworkGroup ()
	{
		Assert.IsTrue (base.isServer, "UpdateNetworkGroup called on clientside entity!");
		isCallingUpdateNetworkGroup = false;
		if (net == null || Network.Net.sv == null || Network.Net.sv.visibility == null) {
			return;
		}
		using (TimeWarning.New ("UpdateNetworkGroup")) {
			if (globalBroadcast) {
				if (net.SwitchGroup (BaseNetworkable.GlobalNetworkGroup)) {
					SendNetworkGroupChange ();
				}
			} else if (ShouldInheritNetworkGroup () && parentEntity.IsSet ()) {
				BaseEntity baseEntity = GetParentEntity ();
				if (!baseEntity.IsValid ()) {
					if (!Rust.Application.isLoadingSave) {
						Debug.LogWarning ("UpdateNetworkGroup: Missing parent entity " + parentEntity.uid);
						Invoke (UpdateNetworkGroup, 2f);
						isCallingUpdateNetworkGroup = true;
					}
				} else if (baseEntity != null) {
					if (net.SwitchGroup (baseEntity.net.group)) {
						SendNetworkGroupChange ();
					}
				} else {
					Debug.LogWarning (string.Concat (base.gameObject, ": has parent id - but couldn't find parent! ", parentEntity));
				}
			} else if (base.limitNetworking) {
				if (net.SwitchGroup (BaseNetworkable.LimboNetworkGroup)) {
					SendNetworkGroupChange ();
				}
			} else {
				base.UpdateNetworkGroup ();
			}
		}
	}

	public virtual void Eat (BaseNpc baseNpc, float timeSpent)
	{
		baseNpc.AddCalories (100f);
	}

	public virtual void OnDeployed (BaseEntity parent, BasePlayer deployedBy, Item fromItem)
	{
	}

	public override bool ShouldNetworkTo (BasePlayer player)
	{
		if (player == this) {
			return true;
		}
		BaseEntity baseEntity = GetParentEntity ();
		if (base.limitNetworking) {
			if (baseEntity == null) {
				return false;
			}
			if (baseEntity != player) {
				return false;
			}
		}
		if (baseEntity != null) {
			return baseEntity.ShouldNetworkTo (player);
		}
		return base.ShouldNetworkTo (player);
	}

	public virtual void AttackerInfo (PlayerLifeStory.DeathInfo info)
	{
		info.attackerName = base.ShortPrefabName;
		info.attackerSteamID = 0uL;
		info.inflictorName = "";
	}

	public virtual void Push (Vector3 velocity)
	{
		SetVelocity (velocity);
	}

	public virtual void ApplyInheritedVelocity (Vector3 velocity)
	{
		Rigidbody component = GetComponent<Rigidbody> ();
		if ((bool)component) {
			component.velocity = Vector3.Lerp (component.velocity, velocity, 10f * UnityEngine.Time.fixedDeltaTime);
			component.angularVelocity *= Mathf.Clamp01 (1f - 10f * UnityEngine.Time.fixedDeltaTime);
			component.AddForce (-UnityEngine.Physics.gravity * Mathf.Clamp01 (0.9f), ForceMode.Acceleration);
		}
	}

	public virtual void SetVelocity (Vector3 velocity)
	{
		Rigidbody component = GetComponent<Rigidbody> ();
		if ((bool)component) {
			component.velocity = velocity;
		}
	}

	public virtual void SetAngularVelocity (Vector3 velocity)
	{
		Rigidbody component = GetComponent<Rigidbody> ();
		if ((bool)component) {
			component.angularVelocity = velocity;
		}
	}

	public virtual Vector3 GetDropPosition ()
	{
		return base.transform.position;
	}

	public virtual Vector3 GetDropVelocity ()
	{
		return GetInheritedDropVelocity () + Vector3.up;
	}

	public virtual bool OnStartBeingLooted (BasePlayer baseEntity)
	{
		return true;
	}

	public virtual string Admin_Who ()
	{
		return $"Owner ID: {OwnerID}";
	}

	[RPC_Server]
	[RPC_Server.FromOwner]
	private void BroadcastSignalFromClient (RPCMessage msg)
	{
		uint num = StringPool.Get ("BroadcastSignalFromClient");
		if (num != 0) {
			BasePlayer player = msg.player;
			if (!(player == null) && player.rpcHistory.TryIncrement (num, (ulong)ConVar.Server.maxpacketspersecond_rpc_signal)) {
				Signal signal = (Signal)msg.read.Int32 ();
				string arg = msg.read.String ();
				SignalBroadcast (signal, arg, msg.connection);
			}
		}
	}

	public void SignalBroadcast (Signal signal, string arg, Connection sourceConnection = null)
	{
		if (net != null && net.group != null) {
			ClientRPCEx (new SendInfo (net.group.subscribers) {
				method = SendMethod.Unreliable,
				priority = Priority.Immediate
			}, sourceConnection, "SignalFromServerEx", (int)signal, arg);
		}
	}

	public void SignalBroadcast (Signal signal, Connection sourceConnection = null)
	{
		if (net != null && net.group != null) {
			ClientRPCEx (new SendInfo (net.group.subscribers) {
				method = SendMethod.Unreliable,
				priority = Priority.Immediate
			}, sourceConnection, "SignalFromServer", (int)signal);
		}
	}

	protected virtual void OnSkinChanged (ulong oldSkinID, ulong newSkinID)
	{
		if (oldSkinID != newSkinID) {
			skinID = newSkinID;
		}
	}

	protected virtual void OnSkinPreProcess (IPrefabProcessor preProcess, GameObject rootObj, string name, bool serverside, bool clientside, bool bundling)
	{
		if (clientside && Skinnable.All != null && Skinnable.FindForEntity (name) != null) {
			Rust.Workshop.WorkshopSkin.Prepare (rootObj);
			MaterialReplacement.Prepare (rootObj);
		}
	}

	public virtual void PreProcess (IPrefabProcessor preProcess, GameObject rootObj, string name, bool serverside, bool clientside, bool bundling)
	{
		OnSkinPreProcess (preProcess, rootObj, name, serverside, clientside, bundling);
	}

	public bool HasAnySlot ()
	{
		for (int i = 0; i < entitySlots.Length; i++) {
			if (entitySlots [i].IsValid (base.isServer)) {
				return true;
			}
		}
		return false;
	}

	public BaseEntity GetSlot (Slot slot)
	{
		return entitySlots [(int)slot].Get (base.isServer);
	}

	public string GetSlotAnchorName (Slot slot)
	{
		return slot.ToString ().ToLower ();
	}

	public void SetSlot (Slot slot, BaseEntity ent)
	{
		entitySlots [(int)slot].Set (ent);
		SendNetworkUpdate ();
	}

	public EntityRef[] GetSlots ()
	{
		return entitySlots;
	}

	public void SetSlots (EntityRef[] newSlots)
	{
		entitySlots = newSlots;
	}

	public virtual bool HasSlot (Slot slot)
	{
		return false;
	}

	public bool HasTrait (TraitFlag f)
	{
		return (Traits & f) == f;
	}

	public bool HasAnyTrait (TraitFlag f)
	{
		return (Traits & f) != 0;
	}

	public virtual bool EnterTrigger (TriggerBase trigger)
	{
		if (triggers == null) {
			triggers = Facepunch.Pool.Get<List<TriggerBase>> ();
		}
		triggers.Add (trigger);
		return true;
	}

	public virtual void LeaveTrigger (TriggerBase trigger)
	{
		if (triggers != null) {
			triggers.Remove (trigger);
			if (triggers.Count == 0) {
				Facepunch.Pool.FreeList (ref triggers);
			}
		}
	}

	public void RemoveFromTriggers ()
	{
		if (triggers == null) {
			return;
		}
		using (TimeWarning.New ("RemoveFromTriggers")) {
			TriggerBase[] array = triggers.ToArray ();
			foreach (TriggerBase triggerBase in array) {
				if ((bool)triggerBase) {
					triggerBase.RemoveEntity (this);
				}
			}
			if (triggers != null && triggers.Count == 0) {
				Facepunch.Pool.FreeList (ref triggers);
			}
		}
	}

	public T FindTrigger<T> () where T : TriggerBase
	{
		if (triggers == null) {
			return null;
		}
		foreach (TriggerBase trigger in triggers) {
			if (trigger as T == null) {
				continue;
			}
			return trigger as T;
		}
		return null;
	}

	public bool FindTrigger<T> (out T result) where T : TriggerBase
	{
		result = FindTrigger<T> ();
		return result != null;
	}

	private void ForceUpdateTriggersAction ()
	{
		if (!base.IsDestroyed) {
			ForceUpdateTriggers (enter: false, exit: true, invoke: false);
		}
	}

	public void ForceUpdateTriggers (bool enter = true, bool exit = true, bool invoke = true)
	{
		List<TriggerBase> obj = Facepunch.Pool.GetList<TriggerBase> ();
		List<TriggerBase> obj2 = Facepunch.Pool.GetList<TriggerBase> ();
		if (triggers != null) {
			obj.AddRange (triggers);
		}
		Collider componentInChildren = GetComponentInChildren<Collider> ();
		if (componentInChildren is CapsuleCollider) {
			CapsuleCollider capsuleCollider = componentInChildren as CapsuleCollider;
			Vector3 point = base.transform.position + new Vector3 (0f, capsuleCollider.radius, 0f);
			Vector3 point2 = base.transform.position + new Vector3 (0f, capsuleCollider.height - capsuleCollider.radius, 0f);
			GamePhysics.OverlapCapsule (point, point2, capsuleCollider.radius, obj2, 262144, QueryTriggerInteraction.Collide);
		} else if (componentInChildren is BoxCollider) {
			BoxCollider boxCollider = componentInChildren as BoxCollider;
			OBB obb = new OBB (base.transform.position, base.transform.lossyScale, base.transform.rotation, new Bounds (boxCollider.center, boxCollider.size));
			GamePhysics.OverlapOBB (obb, obj2, 262144, QueryTriggerInteraction.Collide);
		} else if (componentInChildren is SphereCollider) {
			SphereCollider sphereCollider = componentInChildren as SphereCollider;
			GamePhysics.OverlapSphere (base.transform.TransformPoint (sphereCollider.center), sphereCollider.radius, obj2, 262144, QueryTriggerInteraction.Collide);
		} else {
			obj2.AddRange (obj);
		}
		if (exit) {
			foreach (TriggerBase item in obj) {
				if (!obj2.Contains (item)) {
					item.OnTriggerExit (componentInChildren);
				}
			}
		}
		if (enter) {
			foreach (TriggerBase item2 in obj2) {
				if (!obj.Contains (item2)) {
					item2.OnTriggerEnter (componentInChildren);
				}
			}
		}
		Facepunch.Pool.FreeList (ref obj);
		Facepunch.Pool.FreeList (ref obj2);
		if (invoke) {
			Invoke (ForceUpdateTriggersAction, UnityEngine.Time.time - UnityEngine.Time.fixedTime + UnityEngine.Time.fixedDeltaTime * 1.5f);
		}
	}

	public TriggerParent FindSuitableParent ()
	{
		if (triggers == null) {
			return null;
		}
		foreach (TriggerBase trigger in triggers) {
			if (trigger is TriggerParent triggerParent && triggerParent.ShouldParent (this, bypassOtherTriggerCheck: true)) {
				return triggerParent;
			}
		}
		return null;
	}

	public virtual BasePlayer ToPlayer ()
	{
		return null;
	}

	public override void InitShared ()
	{
		base.InitShared ();
		InitEntityLinks ();
	}

	public override void DestroyShared ()
	{
		base.DestroyShared ();
		FreeEntityLinks ();
	}

	public override void ResetState ()
	{
		base.ResetState ();
		parentBone = 0u;
		OwnerID = 0uL;
		flags = (Flags)0;
		parentEntity = default(EntityRef);
		if (base.isServer) {
			_spawnable = null;
		}
	}

	public virtual float InheritedVelocityScale ()
	{
		return 0f;
	}

	public virtual bool InheritedVelocityDirection ()
	{
		return true;
	}

	public virtual Vector3 GetInheritedProjectileVelocity (Vector3 direction)
	{
		BaseEntity baseEntity = parentEntity.Get (base.isServer);
		if (baseEntity == null) {
			return Vector3.zero;
		}
		if (baseEntity.InheritedVelocityDirection ()) {
			return GetParentVelocity () * baseEntity.InheritedVelocityScale ();
		}
		return Mathf.Max (Vector3.Dot (GetParentVelocity () * baseEntity.InheritedVelocityScale (), direction), 0f) * direction;
	}

	public virtual Vector3 GetInheritedThrowVelocity (Vector3 direction)
	{
		return GetParentVelocity ();
	}

	public virtual Vector3 GetInheritedDropVelocity ()
	{
		BaseEntity baseEntity = parentEntity.Get (base.isServer);
		return (baseEntity != null) ? baseEntity.GetWorldVelocity () : Vector3.zero;
	}

	public Vector3 GetParentVelocity ()
	{
		BaseEntity baseEntity = parentEntity.Get (base.isServer);
		return (baseEntity != null) ? (baseEntity.GetWorldVelocity () + (baseEntity.GetAngularVelocity () * base.transform.localPosition - base.transform.localPosition)) : Vector3.zero;
	}

	public Vector3 GetWorldVelocity ()
	{
		BaseEntity baseEntity = parentEntity.Get (base.isServer);
		return (baseEntity != null) ? (baseEntity.GetWorldVelocity () + (baseEntity.GetAngularVelocity () * base.transform.localPosition - base.transform.localPosition) + baseEntity.transform.TransformDirection (GetLocalVelocity ())) : GetLocalVelocity ();
	}

	public Vector3 GetLocalVelocity ()
	{
		if (base.isServer) {
			return GetLocalVelocityServer ();
		}
		return Vector3.zero;
	}

	public Quaternion GetAngularVelocity ()
	{
		if (base.isServer) {
			return GetAngularVelocityServer ();
		}
		return Quaternion.identity;
	}

	public virtual OBB WorldSpaceBounds ()
	{
		return new OBB (base.transform.position, base.transform.lossyScale, base.transform.rotation, bounds);
	}

	public Vector3 PivotPoint ()
	{
		return base.transform.position;
	}

	public Vector3 CenterPoint ()
	{
		return WorldSpaceBounds ().position;
	}

	public Vector3 ClosestPoint (Vector3 position)
	{
		return WorldSpaceBounds ().ClosestPoint (position);
	}

	public virtual Vector3 TriggerPoint ()
	{
		return CenterPoint ();
	}

	public float Distance (Vector3 position)
	{
		return (ClosestPoint (position) - position).magnitude;
	}

	public float SqrDistance (Vector3 position)
	{
		return (ClosestPoint (position) - position).sqrMagnitude;
	}

	public float Distance (BaseEntity other)
	{
		return Distance (other.transform.position);
	}

	public float SqrDistance (BaseEntity other)
	{
		return SqrDistance (other.transform.position);
	}

	public float Distance2D (Vector3 position)
	{
		return (ClosestPoint (position) - position).Magnitude2D ();
	}

	public float SqrDistance2D (Vector3 position)
	{
		return (ClosestPoint (position) - position).SqrMagnitude2D ();
	}

	public float Distance2D (BaseEntity other)
	{
		return Distance (other.transform.position);
	}

	public float SqrDistance2D (BaseEntity other)
	{
		return SqrDistance (other.transform.position);
	}

	public bool IsVisible (Ray ray, int layerMask, float maxDistance)
	{
		if (ray.origin.IsNaNOrInfinity ()) {
			return false;
		}
		if (ray.direction.IsNaNOrInfinity ()) {
			return false;
		}
		if (ray.direction == Vector3.zero) {
			return false;
		}
		if (!WorldSpaceBounds ().Trace (ray, out var hit, maxDistance)) {
			return false;
		}
		if (GamePhysics.Trace (ray, 0f, out var hitInfo, maxDistance, layerMask)) {
			BaseEntity entity = hitInfo.GetEntity ();
			if (entity == this) {
				return true;
			}
			if (entity != null && (bool)GetParentEntity () && GetParentEntity ().EqualNetID (entity) && hitInfo.IsOnLayer (Rust.Layer.Vehicle_Detailed)) {
				return true;
			}
			if (hitInfo.distance <= hit.distance) {
				return false;
			}
		}
		return true;
	}

	public bool IsVisibleSpecificLayers (Vector3 position, Vector3 target, int layerMask, float maxDistance = float.PositiveInfinity)
	{
		Vector3 vector = target - position;
		float magnitude = vector.magnitude;
		if (magnitude < Mathf.Epsilon) {
			return true;
		}
		Vector3 vector2 = vector / magnitude;
		Vector3 vector3 = vector2 * Mathf.Min (magnitude, 0.01f);
		return IsVisible (new Ray (position + vector3, vector2), layerMask, maxDistance);
	}

	public bool IsVisible (Vector3 position, Vector3 target, float maxDistance = float.PositiveInfinity)
	{
		Vector3 vector = target - position;
		float magnitude = vector.magnitude;
		if (magnitude < Mathf.Epsilon) {
			return true;
		}
		Vector3 vector2 = vector / magnitude;
		Vector3 vector3 = vector2 * Mathf.Min (magnitude, 0.01f);
		return IsVisible (new Ray (position + vector3, vector2), 1218519041, maxDistance);
	}

	public bool IsVisible (Vector3 position, float maxDistance = float.PositiveInfinity)
	{
		Vector3 target = CenterPoint ();
		if (IsVisible (position, target, maxDistance)) {
			return true;
		}
		Vector3 target2 = ClosestPoint (position);
		if (IsVisible (position, target2, maxDistance)) {
			return true;
		}
		return false;
	}

	public bool IsVisibleAndCanSee (Vector3 position, float maxDistance = float.PositiveInfinity)
	{
		Vector3 vector = CenterPoint ();
		if (IsVisible (position, vector, maxDistance) && IsVisible (vector, position, maxDistance)) {
			return true;
		}
		Vector3 vector2 = ClosestPoint (position);
		if (IsVisible (position, vector2, maxDistance) && IsVisible (vector2, position, maxDistance)) {
			return true;
		}
		return false;
	}

	public bool IsOlderThan (BaseEntity other)
	{
		if (other == null) {
			return true;
		}
		NetworkableId networkableId = net?.ID ?? default(NetworkableId);
		NetworkableId networkableId2 = other.net?.ID ?? default(NetworkableId);
		return networkableId.Value < networkableId2.Value;
	}

	public virtual bool IsOutside ()
	{
		return IsOutside (WorldSpaceBounds ().position);
	}

	public bool IsOutside (Vector3 position)
	{
		Profiler.BeginSample ("BaseEntity.IsOutside");
		bool result = true;
		if (UnityEngine.Physics.Raycast (position + Vector3.up * 100f, Vector3.down, out var hitInfo, 100f, 161546513)) {
			BaseEntity baseEntity = hitInfo.collider.ToBaseEntity ();
			if (baseEntity == null || !baseEntity.HasEntityInParents (this)) {
				result = false;
			}
		}
		Profiler.EndSample ();
		return result;
	}

	public virtual float WaterFactor ()
	{
		return WaterLevel.Factor (WorldSpaceBounds ().ToBounds (), waves: true, volumes: true, this);
	}

	public virtual float AirFactor ()
	{
		return (WaterFactor () > 0.85f) ? 0f : 1f;
	}

	public bool WaterTestFromVolumes (Vector3 pos, out WaterLevel.WaterInfo info)
	{
		if (triggers == null) {
			info = default(WaterLevel.WaterInfo);
			return false;
		}
		for (int i = 0; i < triggers.Count; i++) {
			if (triggers [i] is WaterVolume waterVolume && waterVolume.Test (pos, out info)) {
				return true;
			}
		}
		info = default(WaterLevel.WaterInfo);
		return false;
	}

	public bool IsInWaterVolume (Vector3 pos)
	{
		if (triggers == null) {
			return false;
		}
		WaterLevel.WaterInfo info = default(WaterLevel.WaterInfo);
		for (int i = 0; i < triggers.Count; i++) {
			if (triggers [i] is WaterVolume waterVolume && waterVolume.Test (pos, out info)) {
				return true;
			}
		}
		return false;
	}

	public bool WaterTestFromVolumes (Bounds bounds, out WaterLevel.WaterInfo info)
	{
		if (triggers == null) {
			info = default(WaterLevel.WaterInfo);
			return false;
		}
		for (int i = 0; i < triggers.Count; i++) {
			if (triggers [i] is WaterVolume waterVolume && waterVolume.Test (bounds, out info)) {
				return true;
			}
		}
		info = default(WaterLevel.WaterInfo);
		return false;
	}

	public bool WaterTestFromVolumes (Vector3 start, Vector3 end, float radius, out WaterLevel.WaterInfo info)
	{
		if (triggers == null) {
			info = default(WaterLevel.WaterInfo);
			return false;
		}
		for (int i = 0; i < triggers.Count; i++) {
			if (triggers [i] is WaterVolume waterVolume && waterVolume.Test (start, end, radius, out info)) {
				return true;
			}
		}
		info = default(WaterLevel.WaterInfo);
		return false;
	}

	public virtual bool BlocksWaterFor (BasePlayer player)
	{
		return false;
	}

	public virtual float Health ()
	{
		return 0f;
	}

	public virtual float MaxHealth ()
	{
		return 0f;
	}

	public virtual float MaxVelocity ()
	{
		return 0f;
	}

	public virtual float BoundsPadding ()
	{
		return 0.1f;
	}

	public virtual float PenetrationResistance (HitInfo info)
	{
		return 100f;
	}

	public virtual GameObjectRef GetImpactEffect (HitInfo info)
	{
		return impactEffect;
	}

	public virtual void OnAttacked (HitInfo info)
	{
	}

	public virtual Item GetItem ()
	{
		return null;
	}

	public virtual Item GetItem (ItemId itemId)
	{
		return null;
	}

	public virtual void GiveItem (Item item, GiveItemReason reason = GiveItemReason.Generic)
	{
		item.Remove ();
	}

	public virtual bool CanBeLooted (BasePlayer player)
	{
		return true;
	}

	public virtual BaseEntity GetEntity ()
	{
		return this;
	}

	public override string ToString ()
	{
		if (_name == null) {
			if (base.isServer) {
				_name = string.Format ("{1}[{0}]", net?.ID ?? default(NetworkableId), base.ShortPrefabName);
			} else {
				_name = base.ShortPrefabName;
			}
		}
		return _name;
	}

	public virtual string Categorize ()
	{
		return "entity";
	}

	public void Log (string str)
	{
		if (base.isClient) {
			Debug.Log ("<color=#ffa>[" + ToString () + "] " + str + "</color>", base.gameObject);
		} else {
			Debug.Log ("<color=#aff>[" + ToString () + "] " + str + "</color>", base.gameObject);
		}
	}

	public void SetModel (Model mdl)
	{
		if (!(model == mdl)) {
			model = mdl;
		}
	}

	public Model GetModel ()
	{
		return model;
	}

	public virtual Transform[] GetBones ()
	{
		if ((bool)model) {
			return model.GetBones ();
		}
		return null;
	}

	public virtual Transform FindBone (string strName)
	{
		if ((bool)model) {
			return model.FindBone (strName);
		}
		return base.transform;
	}

	public virtual uint FindBoneID (Transform boneTransform)
	{
		if ((bool)model) {
			return model.FindBoneID (boneTransform);
		}
		return StringPool.closest;
	}

	public virtual Transform FindClosestBone (Vector3 worldPos)
	{
		if ((bool)model) {
			return model.FindClosestBone (worldPos);
		}
		return base.transform;
	}

	public virtual bool ShouldBlockProjectiles ()
	{
		return true;
	}

	public virtual bool ShouldInheritNetworkGroup ()
	{
		return true;
	}

	public virtual bool SupportsChildDeployables ()
	{
		BaseEntity baseEntity = GetParentEntity ();
		return baseEntity != null && baseEntity.ForceDeployableSetParent ();
	}

	public virtual bool ForceDeployableSetParent ()
	{
		BaseEntity baseEntity = GetParentEntity ();
		return baseEntity != null && baseEntity.ForceDeployableSetParent ();
	}

	public void BroadcastEntityMessage (string msg, float radius = 20f, int layerMask = 1218652417)
	{
		if (base.isClient) {
			return;
		}
		List<BaseEntity> obj = Facepunch.Pool.GetList<BaseEntity> ();
		Vis.Entities (base.transform.position, radius, obj, layerMask);
		foreach (BaseEntity item in obj) {
			if (item.isServer) {
				item.OnEntityMessage (this, msg);
			}
		}
		Facepunch.Pool.FreeList (ref obj);
	}

	public virtual void OnEntityMessage (BaseEntity from, string msg)
	{
	}

	public override void Save (SaveInfo info)
	{
		base.Save (info);
		Profiler.BeginSample ("BaseEntity.Save");
		BaseEntity baseEntity = parentEntity.Get (base.isServer);
		info.msg.baseEntity = Facepunch.Pool.Get<ProtoBuf.BaseEntity> ();
		if (info.forDisk) {
			if (this is BasePlayer) {
				if (baseEntity == null || baseEntity.enableSaving) {
					info.msg.baseEntity.pos = base.transform.localPosition;
					info.msg.baseEntity.rot = base.transform.localRotation.eulerAngles;
				} else {
					info.msg.baseEntity.pos = base.transform.position;
					info.msg.baseEntity.rot = base.transform.rotation.eulerAngles;
				}
			} else {
				info.msg.baseEntity.pos = base.transform.localPosition;
				info.msg.baseEntity.rot = base.transform.localRotation.eulerAngles;
			}
		} else {
			info.msg.baseEntity.pos = GetNetworkPosition ();
			info.msg.baseEntity.rot = GetNetworkRotation ().eulerAngles;
			info.msg.baseEntity.time = GetNetworkTime ();
		}
		info.msg.baseEntity.flags = (int)flags;
		info.msg.baseEntity.skinid = skinID;
		if (info.forDisk && this is BasePlayer) {
			if (baseEntity != null && baseEntity.enableSaving) {
				info.msg.parent = Facepunch.Pool.Get<ParentInfo> ();
				info.msg.parent.uid = parentEntity.uid;
				info.msg.parent.bone = parentBone;
			}
		} else if (baseEntity != null) {
			info.msg.parent = Facepunch.Pool.Get<ParentInfo> ();
			info.msg.parent.uid = parentEntity.uid;
			info.msg.parent.bone = parentBone;
		}
		if (HasAnySlot ()) {
			info.msg.entitySlots = Facepunch.Pool.Get<EntitySlots> ();
			info.msg.entitySlots.slotLock = entitySlots [0].uid;
			info.msg.entitySlots.slotFireMod = entitySlots [1].uid;
			info.msg.entitySlots.slotUpperModification = entitySlots [2].uid;
			info.msg.entitySlots.centerDecoration = entitySlots [5].uid;
			info.msg.entitySlots.lowerCenterDecoration = entitySlots [6].uid;
			info.msg.entitySlots.storageMonitor = entitySlots [7].uid;
		}
		if (info.forDisk && (bool)_spawnable) {
			_spawnable.Save (info);
		}
		if (OwnerID != 0L && (info.forDisk || ShouldNetworkOwnerInfo ())) {
			info.msg.ownerInfo = Facepunch.Pool.Get<OwnerInfo> ();
			info.msg.ownerInfo.steamid = OwnerID;
		}
		if (Components != null) {
			for (int i = 0; i < Components.Length; i++) {
				if (!(Components [i] == null)) {
					Components [i].SaveComponent (info);
				}
			}
		}
		Profiler.EndSample ();
	}

	public virtual bool ShouldNetworkOwnerInfo ()
	{
		return false;
	}

	public override void Load (LoadInfo info)
	{
		base.Load (info);
		if (info.msg.baseEntity != null) {
			ProtoBuf.BaseEntity baseEntity = info.msg.baseEntity;
			Flags old = flags;
			flags = (Flags)baseEntity.flags;
			OnFlagsChanged (old, flags);
			OnSkinChanged (skinID, info.msg.baseEntity.skinid);
			if (info.fromDisk) {
				if (baseEntity.pos.IsNaNOrInfinity ()) {
					Debug.LogWarning (ToString () + " has broken position - " + baseEntity.pos);
					baseEntity.pos = Vector3.zero;
				}
				base.transform.localPosition = baseEntity.pos;
				base.transform.localRotation = Quaternion.Euler (baseEntity.rot);
			}
		}
		if (info.msg.entitySlots != null) {
			entitySlots [0].uid = info.msg.entitySlots.slotLock;
			entitySlots [1].uid = info.msg.entitySlots.slotFireMod;
			entitySlots [2].uid = info.msg.entitySlots.slotUpperModification;
			entitySlots [5].uid = info.msg.entitySlots.centerDecoration;
			entitySlots [6].uid = info.msg.entitySlots.lowerCenterDecoration;
			entitySlots [7].uid = info.msg.entitySlots.storageMonitor;
		}
		if (info.msg.parent != null) {
			if (base.isServer) {
				BaseEntity entity = BaseNetworkable.serverEntities.Find (info.msg.parent.uid) as BaseEntity;
				SetParent (entity, info.msg.parent.bone);
			}
			parentEntity.uid = info.msg.parent.uid;
			parentBone = info.msg.parent.bone;
		} else {
			parentEntity.uid = default(NetworkableId);
			parentBone = 0u;
		}
		if (info.msg.ownerInfo != null) {
			OwnerID = info.msg.ownerInfo.steamid;
		}
		if ((bool)_spawnable) {
			_spawnable.Load (info);
		}
		if (Components == null) {
			return;
		}
		for (int i = 0; i < Components.Length; i++) {
			if (!(Components [i] == null)) {
				Components [i].LoadComponent (info);
			}
		}
	}
}