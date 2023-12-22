#define UNITY_ASSERTIONS
#define ENABLE_PROFILER
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ConVar;
using Facepunch;
using Facepunch.Rust;
using Network;
using Network.Visibility;
using ProtoBuf;
using Rust;
using Rust.Registry;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Profiling;

public abstract class BaseNetworkable : BaseMonoBehaviour, IPrefabPostProcess, IEntity, NetworkHandler
{
	public struct SaveInfo
	{
		public ProtoBuf.Entity msg;

		public bool forDisk;

		public Connection forConnection;

		internal bool SendingTo (Connection ownerConnection)
		{
			if (ownerConnection == null) {
				return false;
			}
			if (forConnection == null) {
				return false;
			}
			return forConnection == ownerConnection;
		}
	}

	public struct LoadInfo
	{
		public ProtoBuf.Entity msg;

		public bool fromDisk;
	}

	public class EntityRealmServer : EntityRealm
	{
		protected override Manager visibilityManager => (Network.Net.sv != null) ? Network.Net.sv.visibility : null;
	}

	public abstract class EntityRealm : IEnumerable<BaseNetworkable>, IEnumerable
	{
		private ListDictionary<NetworkableId, BaseNetworkable> entityList = new ListDictionary<NetworkableId, BaseNetworkable> ();

		public int Count => entityList.Count;

		protected abstract Manager visibilityManager { get; }

		public bool Contains (NetworkableId uid)
		{
			Profiler.BeginSample ("BaseNetworkable.Contains");
			bool result = entityList.Contains (uid);
			Profiler.EndSample ();
			return result;
		}

		public BaseNetworkable Find (NetworkableId uid)
		{
			Profiler.BeginSample ("BaseNetworkable.Find");
			BaseNetworkable val = null;
			if (!entityList.TryGetValue (uid, out val)) {
				Profiler.EndSample ();
				return null;
			}
			Profiler.EndSample ();
			return val;
		}

		public void RegisterID (BaseNetworkable ent)
		{
			Profiler.BeginSample ("RegisterID");
			if (ent.net != null) {
				if (entityList.Contains (ent.net.ID)) {
					entityList [ent.net.ID] = ent;
				} else {
					entityList.Add (ent.net.ID, ent);
				}
			}
			Profiler.EndSample ();
		}

		public void UnregisterID (BaseNetworkable ent)
		{
			Profiler.BeginSample ("UnregisterID");
			if (ent.net != null) {
				entityList.Remove (ent.net.ID);
			}
			Profiler.EndSample ();
		}

		public Group FindGroup (uint uid)
		{
			Profiler.BeginSample ("FindGroup");
			Group result = visibilityManager?.Get (uid);
			Profiler.EndSample ();
			return result;
		}

		public Group TryFindGroup (uint uid)
		{
			Profiler.BeginSample ("TryFindGroup");
			Group result = visibilityManager?.TryGet (uid);
			Profiler.EndSample ();
			return result;
		}

		public void FindInGroup (uint uid, List<BaseNetworkable> list)
		{
			Group group = TryFindGroup (uid);
			if (group == null) {
				return;
			}
			Profiler.BeginSample ("FindInGroup");
			int count = group.networkables.Values.Count;
			Networkable[] buffer = group.networkables.Values.Buffer;
			for (int i = 0; i < count; i++) {
				Networkable networkable = buffer [i];
				BaseNetworkable baseNetworkable = Find (networkable.ID);
				if (!(baseNetworkable == null) && baseNetworkable.net != null && baseNetworkable.net.group != null) {
					if (baseNetworkable.net.group.ID != uid) {
						Debug.LogWarning ("Group ID mismatch: " + baseNetworkable.ToString ());
					} else {
						list.Add (baseNetworkable);
					}
				}
			}
			Profiler.EndSample ();
		}

		public IEnumerator<BaseNetworkable> GetEnumerator ()
		{
			return entityList.Values.GetEnumerator ();
		}

		IEnumerator IEnumerable.GetEnumerator ()
		{
			return GetEnumerator ();
		}

		public void Clear ()
		{
			entityList.Clear ();
		}
	}

	public enum DestroyMode : byte
	{
		None,
		Gib
	}

	public List<Component> postNetworkUpdateComponents = new List<Component> ();

	private bool _limitedNetworking = false;

	[NonSerialized]
	public EntityRef parentEntity;

	[NonSerialized]
	public readonly List<BaseEntity> children = new List<BaseEntity> ();

	[NonSerialized]
	public bool canTriggerParent = true;

	private int creationFrame;

	protected bool isSpawned;

	private MemoryStream _NetworkCache;

	public static Queue<MemoryStream> EntityMemoryStreamPool = new Queue<MemoryStream> ();

	private MemoryStream _SaveCache;

	[Header ("BaseNetworkable")]
	[ReadOnly]
	public uint prefabID = 0u;

	[Tooltip ("If enabled the entity will send to everyone on the server - regardless of position")]
	public bool globalBroadcast = false;

	[NonSerialized]
	public Networkable net;

	private string _prefabName = null;

	private string _prefabNameWithoutExtension = null;

	public static EntityRealm serverEntities = new EntityRealmServer ();

	private const bool isServersideEntity = true;

	private static List<Connection> connectionsInSphereList = new List<Connection> ();

	public bool limitNetworking {
		get {
			return _limitedNetworking;
		}
		set {
			if (value != _limitedNetworking) {
				_limitedNetworking = value;
				if (_limitedNetworking) {
					OnNetworkLimitStart ();
				} else {
					OnNetworkLimitEnd ();
				}
				UpdateNetworkGroup ();
			}
		}
	}

	public GameManager gameManager {
		get {
			if (isServer) {
				return GameManager.server;
			}
			throw new NotImplementedException ("Missing gameManager path");
		}
	}

	public PrefabAttribute.Library prefabAttribute {
		get {
			if (isServer) {
				return PrefabAttribute.server;
			}
			throw new NotImplementedException ("Missing prefabAttribute path");
		}
	}

	public static Group GlobalNetworkGroup => Network.Net.sv.visibility.Get (0u);

	public static Group LimboNetworkGroup => Network.Net.sv.visibility.Get (1u);

	public bool IsDestroyed { get; private set; }

	public string PrefabName {
		get {
			if (_prefabName == null) {
				_prefabName = StringPool.Get (prefabID);
			}
			return _prefabName;
		}
	}

	public string ShortPrefabName {
		get {
			if (_prefabNameWithoutExtension == null) {
				_prefabNameWithoutExtension = Path.GetFileNameWithoutExtension (PrefabName);
			}
			return _prefabNameWithoutExtension;
		}
	}

	public bool isServer => true;

	public bool isClient => false;

	public void BroadcastOnPostNetworkUpdate (BaseEntity entity)
	{
		Profiler.BeginSample ("BaseNetworkable.OnPostNetworkUpdate");
		foreach (Component postNetworkUpdateComponent in postNetworkUpdateComponents) {
			(postNetworkUpdateComponent as IOnPostNetworkUpdate)?.OnPostNetworkUpdate (entity);
		}
		foreach (BaseEntity child in children) {
			child.BroadcastOnPostNetworkUpdate (entity);
		}
		Profiler.EndSample ();
	}

	public virtual void PostProcess (IPrefabProcessor preProcess, GameObject rootObj, string name, bool serverside, bool clientside, bool bundling)
	{
		if (!serverside) {
			postNetworkUpdateComponents = GetComponentsInChildren<IOnPostNetworkUpdate> (includeInactive: true).Cast<Component> ().ToList ();
		}
	}

	private void OnNetworkLimitStart ()
	{
		LogEntry (LogEntryType.Network, 2, "OnNetworkLimitStart");
		List<Connection> subscribers = GetSubscribers ();
		if (subscribers == null) {
			return;
		}
		subscribers = subscribers.ToList ();
		subscribers.RemoveAll ((Connection x) => ShouldNetworkTo (x.player as BasePlayer));
		OnNetworkSubscribersLeave (subscribers);
		if (children == null) {
			return;
		}
		foreach (BaseEntity child in children) {
			child.OnNetworkLimitStart ();
		}
	}

	private void OnNetworkLimitEnd ()
	{
		LogEntry (LogEntryType.Network, 2, "OnNetworkLimitEnd");
		List<Connection> subscribers = GetSubscribers ();
		if (subscribers == null) {
			return;
		}
		OnNetworkSubscribersEnter (subscribers);
		if (children == null) {
			return;
		}
		foreach (BaseEntity child in children) {
			child.OnNetworkLimitEnd ();
		}
	}

	public BaseEntity GetParentEntity ()
	{
		return parentEntity.Get (isServer);
	}

	public bool HasParent ()
	{
		return parentEntity.IsValid (isServer);
	}

	public void AddChild (BaseEntity child)
	{
		if (!children.Contains (child)) {
			children.Add (child);
			OnChildAdded (child);
		}
	}

	protected virtual void OnChildAdded (BaseEntity child)
	{
	}

	public void RemoveChild (BaseEntity child)
	{
		children.Remove (child);
		OnChildRemoved (child);
	}

	protected virtual void OnChildRemoved (BaseEntity child)
	{
	}

	public virtual float GetNetworkTime ()
	{
		return UnityEngine.Time.time;
	}

	public virtual void Spawn ()
	{
		SpawnShared ();
		if (net == null) {
			net = Network.Net.sv.CreateNetworkable ();
		}
		creationFrame = UnityEngine.Time.frameCount;
		PreInitShared ();
		InitShared ();
		ServerInit ();
		PostInitShared ();
		UpdateNetworkGroup ();
		isSpawned = true;
		SendNetworkUpdateImmediate (justCreated: true);
		if (Rust.Application.isLoading && !Rust.Application.isLoadingSave) {
			base.gameObject.SendOnSendNetworkUpdate (this as BaseEntity);
		}
	}

	public bool IsFullySpawned ()
	{
		return isSpawned;
	}

	public virtual void ServerInit ()
	{
		serverEntities.RegisterID (this);
		if (net != null) {
			net.handler = this;
		}
	}

	protected List<Connection> GetSubscribers ()
	{
		if (net == null) {
			return null;
		}
		if (net.group == null) {
			return null;
		}
		return net.group.subscribers;
	}

	public void KillMessage ()
	{
		Kill ();
	}

	public virtual void AdminKill ()
	{
		Kill (DestroyMode.Gib);
	}

	public void Kill (DestroyMode mode = DestroyMode.None)
	{
		if (IsDestroyed) {
			Debug.LogWarning ("Calling kill - but already IsDestroyed!? " + this);
			return;
		}
		base.gameObject.BroadcastOnParentDestroying ();
		Profiler.BeginSample ("DoEntityDestroy");
		DoEntityDestroy ();
		Profiler.EndSample ();
		TerminateOnClient (mode);
		TerminateOnServer ();
		EntityDestroy ();
	}

	private void TerminateOnClient (DestroyMode mode)
	{
		if (net != null && net.group != null && Network.Net.sv.IsConnected ()) {
			LogEntry (LogEntryType.Network, 2, "Term {0}", mode);
			NetWrite netWrite = Network.Net.sv.StartWrite ();
			netWrite.PacketID (Message.Type.EntityDestroy);
			netWrite.EntityID (net.ID);
			netWrite.UInt8 ((byte)mode);
			netWrite.Send (new SendInfo (net.group.subscribers));
		}
	}

	private void TerminateOnServer ()
	{
		if (net != null) {
			InvalidateNetworkCache ();
			serverEntities.UnregisterID (this);
			Network.Net.sv.DestroyNetworkable (ref net);
			StopAllCoroutines ();
			base.gameObject.SetActive (value: false);
		}
	}

	internal virtual void DoServerDestroy ()
	{
		isSpawned = false;
		Analytics.Azure.OnEntityDestroyed (this);
	}

	public virtual bool ShouldNetworkTo (BasePlayer player)
	{
		if (net.group == null) {
			return true;
		}
		return player.net.subscriber.IsSubscribed (net.group);
	}

	protected void SendNetworkGroupChange ()
	{
		if (isSpawned && Network.Net.sv.IsConnected ()) {
			if (net.group == null) {
				Debug.LogWarning (ToString () + " changed its network group to null");
				return;
			}
			NetWrite netWrite = Network.Net.sv.StartWrite ();
			netWrite.PacketID (Message.Type.GroupChange);
			netWrite.EntityID (net.ID);
			netWrite.GroupID (net.group.ID);
			netWrite.Send (new SendInfo (net.group.subscribers));
		}
	}

	protected void SendAsSnapshot (Connection connection, bool justCreated = false)
	{
		NetWrite netWrite = Network.Net.sv.StartWrite ();
		connection.validate.entityUpdates++;
		SaveInfo saveInfo = default(SaveInfo);
		saveInfo.forConnection = connection;
		saveInfo.forDisk = false;
		SaveInfo saveInfo2 = saveInfo;
		netWrite.PacketID (Message.Type.Entities);
		netWrite.UInt32 (connection.validate.entityUpdates);
		ToStreamForNetwork (netWrite, saveInfo2);
		netWrite.Send (new SendInfo (connection));
	}

	public void SendNetworkUpdate (BasePlayer.NetworkQueue queue = BasePlayer.NetworkQueue.Update)
	{
		if (Rust.Application.isLoading || Rust.Application.isLoadingSave || IsDestroyed || net == null || !isSpawned) {
			return;
		}
		using (TimeWarning.New ("SendNetworkUpdate")) {
			LogEntry (LogEntryType.Network, 2, "SendNetworkUpdate");
			InvalidateNetworkCache ();
			List<Connection> subscribers = GetSubscribers ();
			if (subscribers != null && subscribers.Count > 0) {
				Profiler.BeginSample ("SubscriberQueue");
				for (int i = 0; i < subscribers.Count; i++) {
					Connection connection = subscribers [i];
					BasePlayer basePlayer = connection.player as BasePlayer;
					if (!(basePlayer == null) && ShouldNetworkTo (basePlayer)) {
						basePlayer.QueueUpdate (queue, this);
					}
				}
				Profiler.EndSample ();
			}
		}
		base.gameObject.SendOnSendNetworkUpdate (this as BaseEntity);
	}

	public void SendNetworkUpdateImmediate (bool justCreated = false)
	{
		if (Rust.Application.isLoading || Rust.Application.isLoadingSave || IsDestroyed || net == null || !isSpawned) {
			return;
		}
		using (TimeWarning.New ("SendNetworkUpdateImmediate")) {
			LogEntry (LogEntryType.Network, 2, "SendNetworkUpdateImmediate");
			InvalidateNetworkCache ();
			List<Connection> subscribers = GetSubscribers ();
			if (subscribers != null && subscribers.Count > 0) {
				for (int i = 0; i < subscribers.Count; i++) {
					Connection connection = subscribers [i];
					BasePlayer basePlayer = connection.player as BasePlayer;
					if (!(basePlayer == null) && ShouldNetworkTo (basePlayer)) {
						SendAsSnapshot (connection, justCreated);
					}
				}
			}
		}
		base.gameObject.SendOnSendNetworkUpdate (this as BaseEntity);
	}

	protected void SendNetworkUpdate_Position ()
	{
		if (Rust.Application.isLoading || Rust.Application.isLoadingSave || IsDestroyed || net == null || !isSpawned) {
			return;
		}
		using (TimeWarning.New ("SendNetworkUpdate_Position")) {
			LogEntry (LogEntryType.Network, 2, "SendNetworkUpdate_Position");
			List<Connection> subscribers = GetSubscribers ();
			if (subscribers != null && subscribers.Count > 0) {
				NetWrite netWrite = Network.Net.sv.StartWrite ();
				Profiler.BeginSample ("Write");
				netWrite.PacketID (Message.Type.EntityPosition);
				netWrite.EntityID (net.ID);
				Vector3 obj = GetNetworkPosition ();
				netWrite.Vector3 (in obj);
				obj = GetNetworkRotation ().eulerAngles;
				netWrite.Vector3 (in obj);
				netWrite.Float (GetNetworkTime ());
				NetworkableId uid = parentEntity.uid;
				if (uid.IsValid) {
					netWrite.EntityID (uid);
				}
				Profiler.EndSample ();
				Profiler.BeginSample ("SendInfo");
				SendInfo sendInfo = new SendInfo (subscribers);
				sendInfo.method = SendMethod.ReliableUnordered;
				sendInfo.priority = Priority.Immediate;
				SendInfo info = sendInfo;
				Profiler.EndSample ();
				Profiler.BeginSample ("Send");
				netWrite.Send (info);
				Profiler.EndSample ();
			}
		}
	}

	private void ToStream (Stream stream, SaveInfo saveInfo)
	{
		using (saveInfo.msg = Facepunch.Pool.Get<ProtoBuf.Entity> ()) {
			Profiler.BeginSample ("BaseNetworkable.ToStream");
			Profiler.BeginSample ("Save( saveInfo )");
			Save (saveInfo);
			Profiler.EndSample ();
			if (saveInfo.msg.baseEntity == null) {
				Debug.LogError (string.Concat (this, ": ToStream - no BaseEntity!?"));
			}
			if (saveInfo.msg.baseNetworkable == null) {
				Debug.LogError (string.Concat (this, ": ToStream - no baseNetworkable!?"));
			}
			Profiler.BeginSample ("saveInfo.msg.ToProto");
			saveInfo.msg.ToProto (stream);
			Profiler.EndSample ();
			Profiler.BeginSample ("PostSave");
			PostSave (saveInfo);
			Profiler.EndSample ();
			Profiler.EndSample ();
		}
	}

	public virtual bool CanUseNetworkCache (Connection connection)
	{
		return ConVar.Server.netcache;
	}

	public void ToStreamForNetwork (Stream stream, SaveInfo saveInfo)
	{
		if (!CanUseNetworkCache (saveInfo.forConnection)) {
			ToStream (stream, saveInfo);
			return;
		}
		Profiler.BeginSample ("ToStreamForNetwork");
		if (_NetworkCache == null) {
			_NetworkCache = ((EntityMemoryStreamPool.Count > 0) ? (_NetworkCache = EntityMemoryStreamPool.Dequeue ()) : new MemoryStream (8));
			ToStream (_NetworkCache, saveInfo);
			ConVar.Server.netcachesize += (int)_NetworkCache.Length;
		}
		Profiler.EndSample ();
		Profiler.BeginSample ("WriteCachedNetwork");
		_NetworkCache.WriteTo (stream);
		Profiler.EndSample ();
	}

	public void InvalidateNetworkCache ()
	{
		Profiler.BeginSample ("InvalidateNetworkCache");
		using (TimeWarning.New ("InvalidateNetworkCache")) {
			if (_SaveCache != null) {
				ConVar.Server.savecachesize -= (int)_SaveCache.Length;
				_SaveCache.SetLength (0L);
				_SaveCache.Position = 0L;
				EntityMemoryStreamPool.Enqueue (_SaveCache);
				_SaveCache = null;
			}
			if (_NetworkCache != null) {
				ConVar.Server.netcachesize -= (int)_NetworkCache.Length;
				_NetworkCache.SetLength (0L);
				_NetworkCache.Position = 0L;
				EntityMemoryStreamPool.Enqueue (_NetworkCache);
				_NetworkCache = null;
			}
			LogEntry (LogEntryType.Network, 3, "InvalidateNetworkCache");
		}
		Profiler.EndSample ();
	}

	public MemoryStream GetSaveCache ()
	{
		if (_SaveCache == null) {
			if (EntityMemoryStreamPool.Count > 0) {
				_SaveCache = EntityMemoryStreamPool.Dequeue ();
			} else {
				_SaveCache = new MemoryStream (8);
			}
			SaveInfo saveInfo = default(SaveInfo);
			saveInfo.forDisk = true;
			SaveInfo saveInfo2 = saveInfo;
			ToStream (_SaveCache, saveInfo2);
			ConVar.Server.savecachesize += (int)_SaveCache.Length;
		}
		return _SaveCache;
	}

	public virtual void UpdateNetworkGroup ()
	{
		Assert.IsTrue (isServer, "UpdateNetworkGroup called on clientside entity!");
		if (net == null) {
			return;
		}
		using (TimeWarning.New ("UpdateGroups")) {
			if (net.UpdateGroups (base.transform.position)) {
				SendNetworkGroupChange ();
			}
		}
	}

	public virtual Vector3 GetNetworkPosition ()
	{
		return base.transform.localPosition;
	}

	public virtual Quaternion GetNetworkRotation ()
	{
		return base.transform.localRotation;
	}

	public string InvokeString ()
	{
		StringBuilder stringBuilder = new StringBuilder ();
		List<InvokeAction> obj = Facepunch.Pool.GetList<InvokeAction> ();
		InvokeHandler.FindInvokes (this, obj);
		foreach (InvokeAction item in obj) {
			if (stringBuilder.Length > 0) {
				stringBuilder.Append (", ");
			}
			stringBuilder.Append (item.action.Method.Name);
		}
		Facepunch.Pool.FreeList (ref obj);
		return stringBuilder.ToString ();
	}

	public BaseEntity LookupPrefab ()
	{
		return gameManager.FindPrefab (PrefabName).ToBaseEntity ();
	}

	public bool EqualNetID (BaseNetworkable other)
	{
		return !other.IsRealNull () && other.net != null && net != null && other.net.ID == net.ID;
	}

	public bool EqualNetID (NetworkableId otherID)
	{
		return net != null && otherID == net.ID;
	}

	public virtual void ResetState ()
	{
		if (children.Count > 0) {
			children.Clear ();
		}
		if (this is ILootableEntity lootableEntity) {
			lootableEntity.LastLootedBy = 0uL;
		}
	}

	public virtual void InitShared ()
	{
	}

	public virtual void PreInitShared ()
	{
	}

	public virtual void PostInitShared ()
	{
	}

	public virtual void DestroyShared ()
	{
	}

	public virtual void OnNetworkGroupEnter (Group group)
	{
	}

	public virtual void OnNetworkGroupLeave (Group group)
	{
	}

	public void OnNetworkGroupChange ()
	{
		if (children == null) {
			return;
		}
		foreach (BaseEntity child in children) {
			if (child.ShouldInheritNetworkGroup ()) {
				child.net.SwitchGroup (net.group);
			} else if (isServer) {
				child.UpdateNetworkGroup ();
			}
		}
	}

	public void OnNetworkSubscribersEnter (List<Connection> connections)
	{
		if (!Network.Net.sv.IsConnected ()) {
			return;
		}
		foreach (Connection connection in connections) {
			BasePlayer basePlayer = connection.player as BasePlayer;
			if (!(basePlayer == null)) {
				basePlayer.QueueUpdate (BasePlayer.NetworkQueue.Update, this as BaseEntity);
			}
		}
	}

	public void OnNetworkSubscribersLeave (List<Connection> connections)
	{
		if (Network.Net.sv.IsConnected ()) {
			LogEntry (LogEntryType.Network, 2, "LeaveVisibility");
			NetWrite netWrite = Network.Net.sv.StartWrite ();
			netWrite.PacketID (Message.Type.EntityDestroy);
			netWrite.EntityID (net.ID);
			netWrite.UInt8 (0);
			netWrite.Send (new SendInfo (connections));
		}
	}

	private void EntityDestroy ()
	{
		if ((bool)base.gameObject) {
			Profiler.BeginSample ("EntityDestroy");
			ResetState ();
			gameManager.Retire (base.gameObject);
			Profiler.EndSample ();
		}
	}

	private void DoEntityDestroy ()
	{
		if (IsDestroyed) {
			return;
		}
		IsDestroyed = true;
		if (Rust.Application.isQuitting) {
			return;
		}
		Profiler.BeginSample ("DestroyShared");
		DestroyShared ();
		Profiler.EndSample ();
		if (isServer) {
			Profiler.BeginSample ("DoServerDestroy");
			DoServerDestroy ();
			Profiler.EndSample ();
		}
		using (TimeWarning.New ("Registry.Entity.Unregister")) {
			Rust.Registry.Entity.Unregister (base.gameObject);
		}
	}

	private void SpawnShared ()
	{
		IsDestroyed = false;
		using (TimeWarning.New ("Registry.Entity.Register")) {
			Rust.Registry.Entity.Register (base.gameObject, this);
		}
	}

	public virtual void Save (SaveInfo info)
	{
		Profiler.BeginSample ("BaseNetworkable.Save");
		if (prefabID == 0) {
			Debug.LogError ("PrefabID is 0! " + base.transform.GetRecursiveName (), base.gameObject);
		}
		info.msg.baseNetworkable = Facepunch.Pool.Get<ProtoBuf.BaseNetworkable> ();
		info.msg.baseNetworkable.uid = net.ID;
		info.msg.baseNetworkable.prefabID = prefabID;
		if (net.group != null) {
			info.msg.baseNetworkable.group = net.group.ID;
		}
		if (!info.forDisk) {
			info.msg.createdThisFrame = creationFrame == UnityEngine.Time.frameCount;
		}
		Profiler.EndSample ();
	}

	public virtual void PostSave (SaveInfo info)
	{
	}

	public void InitLoad (NetworkableId entityID)
	{
		net = Network.Net.sv.CreateNetworkable (entityID);
		serverEntities.RegisterID (this);
		PreServerLoad ();
	}

	public virtual void PreServerLoad ()
	{
	}

	public virtual void Load (LoadInfo info)
	{
		if (info.msg.baseNetworkable != null) {
			ProtoBuf.BaseNetworkable baseNetworkable = info.msg.baseNetworkable;
			if (prefabID != baseNetworkable.prefabID) {
				Debug.LogError ("Prefab IDs don't match! " + prefabID + "/" + baseNetworkable.prefabID + " -> " + base.gameObject, base.gameObject);
			}
		}
	}

	public virtual void PostServerLoad ()
	{
		base.gameObject.SendOnSendNetworkUpdate (this as BaseEntity);
	}

	public T ToServer<T> () where T : BaseNetworkable
	{
		if (isServer) {
			return this as T;
		}
		return null;
	}

	public virtual bool OnRpcMessage (BasePlayer player, uint rpc, Message msg)
	{
		return false;
	}

	public static List<Connection> GetConnectionsWithin (Vector3 position, float distance)
	{
		connectionsInSphereList.Clear ();
		float num = distance * distance;
		List<Connection> subscribers = GlobalNetworkGroup.subscribers;
		for (int i = 0; i < subscribers.Count; i++) {
			Connection connection = subscribers [i];
			if (connection.active) {
				BasePlayer basePlayer = connection.player as BasePlayer;
				if (!(basePlayer == null) && !(basePlayer.SqrDistance (position) > num)) {
					connectionsInSphereList.Add (connection);
				}
			}
		}
		return connectionsInSphereList;
	}

	public static void GetCloseConnections (Vector3 position, float distance, List<BasePlayer> players)
	{
		if (Network.Net.sv == null || Network.Net.sv.visibility == null) {
			return;
		}
		float num = distance * distance;
		Group group = Network.Net.sv.visibility.GetGroup (position);
		if (group == null) {
			return;
		}
		List<Connection> subscribers = group.subscribers;
		for (int i = 0; i < subscribers.Count; i++) {
			Connection connection = subscribers [i];
			if (connection.active) {
				BasePlayer basePlayer = connection.player as BasePlayer;
				if (!(basePlayer == null) && !(basePlayer.SqrDistance (position) > num)) {
					players.Add (basePlayer);
				}
			}
		}
	}

	public static bool HasCloseConnections (Vector3 position, float distance)
	{
		if (Network.Net.sv == null) {
			return false;
		}
		if (Network.Net.sv.visibility == null) {
			return false;
		}
		float num = distance * distance;
		Group group = Network.Net.sv.visibility.GetGroup (position);
		if (group == null) {
			return false;
		}
		List<Connection> subscribers = group.subscribers;
		for (int i = 0; i < subscribers.Count; i++) {
			Connection connection = subscribers [i];
			if (connection.active) {
				BasePlayer basePlayer = connection.player as BasePlayer;
				if (!(basePlayer == null) && !(basePlayer.SqrDistance (position) > num)) {
					return true;
				}
			}
		}
		return false;
	}
}
