using System;
using System.Collections.Generic;
using Facepunch;
using Facepunch.Rust;
using Rust;
using UnityEngine;
using UnityEngine.Profiling;

[CreateAssetMenu (menuName = "Rust/Missions/BaseMission")]
public class BaseMission : BaseScriptableObject
{
	[Serializable]
	public class MissionDependancy
	{
		public string targetMissionShortname;

		public MissionStatus targetMissionDesiredStatus;

		public bool everAttempted;

		public uint targetMissionID => StringEx.ManifestHash (targetMissionShortname);
	}

	public enum MissionStatus
	{
		Default,
		Active,
		Accomplished,
		Failed,
		Completed
	}

	public enum MissionEventType
	{
		CUSTOM,
		HARVEST,
		CONVERSATION,
		KILL_ENTITY,
		ACQUIRE_ITEM,
		FREE_CRATE
	}

	[Serializable]
	public class MissionObjectiveEntry
	{
		public Phrase description;

		public int[] startAfterCompletedObjectives;

		public int[] autoCompleteOtherObjectives;

		public bool onlyProgressIfStarted = true;

		public MissionObjective objective;

		public MissionObjective Get ()
		{
			return objective;
		}
	}

	public class MissionInstance : IPooled
	{
		[Serializable]
		public class ObjectiveStatus
		{
			public bool started;

			public bool completed;

			public bool failed;

			public int genericInt1;

			public float genericFloat1;
		}

		public enum ObjectiveType
		{
			MOVE,
			KILL
		}

		private BaseEntity _cachedProviderEntity;

		private BaseMission _cachedMission = null;

		public NetworkableId providerID;

		public uint missionID;

		public MissionStatus status;

		public float completionScale;

		public float startTime;

		public float endTime;

		public Vector3 missionLocation;

		public float timePassed = 0f;

		public Dictionary<string, Vector3> missionPoints = new Dictionary<string, Vector3> ();

		public ObjectiveStatus[] objectiveStatuses;

		public List<MissionEntity> createdEntities;

		public ItemAmount[] rewards;

		public BaseEntity ProviderEntity ()
		{
			//IL_0018: Unknown result type (might be due to invalid IL or missing references)
			if ((Object)(object)_cachedProviderEntity == (Object)null) {
				_cachedProviderEntity = BaseNetworkable.serverEntities.Find (providerID) as BaseEntity;
			}
			return _cachedProviderEntity;
		}

		public BaseMission GetMission ()
		{
			Profiler.BeginSample ("GetMission");
			if (_cachedMission == null) {
				_cachedMission = MissionManifest.GetFromID (missionID);
			}
			Profiler.EndSample ();
			return _cachedMission;
		}

		public bool ShouldShowOnMap ()
		{
			//IL_0014: Unknown result type (might be due to invalid IL or missing references)
			//IL_0019: Unknown result type (might be due to invalid IL or missing references)
			return (status == MissionStatus.Active || status == MissionStatus.Accomplished) && missionLocation != Vector3.zero;
		}

		public bool ShouldShowOnCompass ()
		{
			return ShouldShowOnMap ();
		}

		public virtual void ProcessMissionEvent (BasePlayer playerFor, MissionEventType type, string identifier, float amount)
		{
			if (status == MissionStatus.Active) {
				BaseMission mission = GetMission ();
				for (int i = 0; i < mission.objectives.Length; i++) {
					MissionObjectiveEntry missionObjectiveEntry = mission.objectives [i];
					missionObjectiveEntry.objective.ProcessMissionEvent (playerFor, this, i, type, identifier, amount);
				}
			}
		}

		public void Think (BasePlayer assignee, float delta)
		{
			if (status != MissionStatus.Failed && status != MissionStatus.Completed) {
				BaseMission mission = GetMission ();
				timePassed += delta;
				mission.Think (this, assignee, delta);
				if (mission.timeLimitSeconds > 0f && timePassed >= mission.timeLimitSeconds) {
					mission.MissionFailed (this, assignee, MissionFailReason.TimeOut);
				}
			}
		}

		public Vector3 GetMissionPoint (string identifier, BasePlayer playerFor)
		{
			//IL_0018: Unknown result type (might be due to invalid IL or missing references)
			//IL_001d: Unknown result type (might be due to invalid IL or missing references)
			//IL_008c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0091: Unknown result type (might be due to invalid IL or missing references)
			//IL_0094: Unknown result type (might be due to invalid IL or missing references)
			//IL_0068: Unknown result type (might be due to invalid IL or missing references)
			//IL_006d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0060: Unknown result type (might be due to invalid IL or missing references)
			//IL_0065: Unknown result type (might be due to invalid IL or missing references)
			if (missionPoints.ContainsKey (identifier)) {
				return missionPoints [identifier];
			}
			if (Object.op_Implicit ((Object)(object)playerFor)) {
				BaseMission mission = GetMission ();
				mission.SetupPositions (this, playerFor);
				Debug.Log ((object)"Mission point not found, regenerating");
				if (missionPoints.ContainsKey (identifier)) {
					return missionPoints [identifier];
				}
				return Vector3.zero;
			}
			Debug.Log ((object)("Massive mission failure to get point, correct mission definition of : " + GetMission ().shortname));
			return Vector3.zero;
		}

		public void EnterPool ()
		{
			//IL_0007: Unknown result type (might be due to invalid IL or missing references)
			//IL_003d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0042: Unknown result type (might be due to invalid IL or missing references)
			providerID = default(NetworkableId);
			missionID = 0u;
			status = MissionStatus.Default;
			completionScale = 0f;
			startTime = -1f;
			endTime = -1f;
			missionLocation = Vector3.zero;
			_cachedMission = null;
			timePassed = 0f;
			rewards = null;
			missionPoints.Clear ();
			if (createdEntities != null) {
				Pool.FreeList<MissionEntity> (ref createdEntities);
			}
		}

		public void LeavePool ()
		{
			createdEntities = Pool.GetList<MissionEntity> ();
		}
	}

	[Serializable]
	public class PositionGenerator
	{
		public enum PositionType
		{
			MissionPoint,
			WorldPositionGenerator,
			DungeonPoint
		}

		public string identifier;

		public float minDistForMovePoint = 0f;

		public float maxDistForMovePoint = 25f;

		public bool centerOnProvider = false;

		public bool centerOnPlayer = false;

		public string centerOnPositionIdentifier = "";

		public PositionType positionType;

		[Header ("MissionPoint")]
		[InspectorFlags]
		public MissionPoint.MissionPointEnum Flags = (MissionPoint.MissionPointEnum)(-1);

		[InspectorFlags]
		public MissionPoint.MissionPointEnum ExclusionFlags = (MissionPoint.MissionPointEnum)0;

		[Header ("WorldPositionGenerator")]
		public WorldPositionGenerator worldPositionGenerator;

		public bool IsDependant ()
		{
			return !string.IsNullOrEmpty (centerOnPositionIdentifier);
		}

		public string GetIdentifier ()
		{
			return identifier;
		}

		public bool Validate (BasePlayer assignee, BaseMission missionDef)
		{
			//IL_001d: Unknown result type (might be due to invalid IL or missing references)
			//IL_008f: Unknown result type (might be due to invalid IL or missing references)
			Vector3 position;
			if (positionType == PositionType.MissionPoint) {
				List<MissionPoint> points = Pool.GetList<MissionPoint> ();
				bool missionPoints = MissionPoint.GetMissionPoints (ref points, ((Component)assignee).transform.position, minDistForMovePoint, maxDistForMovePoint, (int)Flags, (int)ExclusionFlags);
				Pool.FreeList<MissionPoint> (ref points);
				if (!missionPoints) {
					Debug.Log ((object)"FAILED TO FIND MISSION POINTS");
					return false;
				}
			} else if (positionType == PositionType.WorldPositionGenerator && (Object)(object)worldPositionGenerator != (Object)null && !worldPositionGenerator.TrySample (((Component)assignee).transform.position, minDistForMovePoint, maxDistForMovePoint, out position, blockedPoints)) {
				Debug.Log ((object)"FAILED TO GENERATE WORLD POSITION!!!!!");
				return false;
			}
			return true;
		}

		public Vector3 GetPosition (BasePlayer assignee)
		{
			//IL_001d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0075: Unknown result type (might be due to invalid IL or missing references)
			//IL_007a: Unknown result type (might be due to invalid IL or missing references)
			//IL_005a: Unknown result type (might be due to invalid IL or missing references)
			//IL_005f: Unknown result type (might be due to invalid IL or missing references)
			//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
			//IL_01d1: Unknown result type (might be due to invalid IL or missing references)
			//IL_01d2: Unknown result type (might be due to invalid IL or missing references)
			//IL_0117: Unknown result type (might be due to invalid IL or missing references)
			//IL_011c: Unknown result type (might be due to invalid IL or missing references)
			//IL_010a: Unknown result type (might be due to invalid IL or missing references)
			//IL_010f: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
			//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
			//IL_01d6: Unknown result type (might be due to invalid IL or missing references)
			//IL_014d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0140: Unknown result type (might be due to invalid IL or missing references)
			//IL_0152: Unknown result type (might be due to invalid IL or missing references)
			//IL_0154: Unknown result type (might be due to invalid IL or missing references)
			//IL_0156: Unknown result type (might be due to invalid IL or missing references)
			//IL_0169: Unknown result type (might be due to invalid IL or missing references)
			//IL_016e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0173: Unknown result type (might be due to invalid IL or missing references)
			//IL_0174: Unknown result type (might be due to invalid IL or missing references)
			//IL_017c: Unknown result type (might be due to invalid IL or missing references)
			//IL_019a: Unknown result type (might be due to invalid IL or missing references)
			//IL_01b8: Unknown result type (might be due to invalid IL or missing references)
			Vector3 position;
			if (positionType == PositionType.MissionPoint) {
				List<MissionPoint> points = Pool.GetList<MissionPoint> ();
				if (MissionPoint.GetMissionPoints (ref points, ((Component)assignee).transform.position, minDistForMovePoint, maxDistForMovePoint, (int)Flags, (int)ExclusionFlags)) {
					position = points [Random.Range (0, points.Count)].GetPosition ();
				} else {
					Debug.LogError ((object)"UNABLE TO FIND MISSIONPOINT FOR MISSION!");
					position = ((Component)assignee).transform.position;
				}
				Pool.FreeList<MissionPoint> (ref points);
			} else if (positionType == PositionType.WorldPositionGenerator && (Object)(object)worldPositionGenerator != (Object)null) {
				if (!worldPositionGenerator.TrySample (((Component)assignee).transform.position, minDistForMovePoint, maxDistForMovePoint, out position, blockedPoints)) {
					Debug.LogError ((object)"UNABLE TO FIND WORLD POINT FOR MISSION!");
					position = ((Component)assignee).transform.position;
				}
			} else if (positionType == PositionType.DungeonPoint) {
				position = DynamicDungeon.GetNextDungeonPoint ();
			} else {
				Vector3 onUnitSphere = Random.onUnitSphere;
				onUnitSphere.y = 0f;
				((Vector3)(ref onUnitSphere)).Normalize ();
				Vector3 val = (centerOnPlayer ? ((Component)assignee).transform.position : ((Component)assignee).transform.position);
				position = val + onUnitSphere * Random.Range (minDistForMovePoint, maxDistForMovePoint);
				float num = position.y;
				float num2 = position.y;
				if ((Object)(object)TerrainMeta.WaterMap != (Object)null) {
					num2 = TerrainMeta.WaterMap.GetHeight (position);
				}
				if ((Object)(object)TerrainMeta.HeightMap != (Object)null) {
					num = TerrainMeta.HeightMap.GetHeight (position);
				}
				position.y = Mathf.Max (num2, num);
			}
			return position;
		}
	}

	[Serializable]
	public class MissionEntityEntry
	{
		public GameObjectRef entityRef;

		public string spawnPositionToUse;

		public bool cleanupOnMissionFailed;

		public bool cleanupOnMissionSuccess;

		public string entityIdentifier;
	}

	public enum MissionFailReason
	{
		TimeOut,
		Disconnect,
		ResetPlayerState,
		Abandon
	}

	[ServerVar]
	public static bool missionsenabled = true;

	public string shortname;

	public Phrase missionName;

	public Phrase missionDesc;

	public MissionObjectiveEntry[] objectives;

	public static List<Vector3> blockedPoints = new List<Vector3> ();

	public const string MISSION_COMPLETE_STAT = "missions_completed";

	public GameObjectRef acceptEffect;

	public GameObjectRef failedEffect;

	public GameObjectRef victoryEffect;

	public int repeatDelaySecondsSuccess = -1;

	public int repeatDelaySecondsFailed = -1;

	public float timeLimitSeconds;

	public Sprite icon;

	public Sprite providerIcon;

	public MissionDependancy[] acceptDependancies;

	public MissionDependancy[] completionDependancies;

	public MissionEntityEntry[] missionEntities;

	public PositionGenerator[] positionGenerators;

	public ItemAmount[] baseRewards;

	public uint id => StringEx.ManifestHash (shortname);

	public bool isRepeatable => repeatDelaySecondsSuccess != -1 || repeatDelaySecondsFailed != -1;

	public static void PlayerDisconnected (BasePlayer player)
	{
		if (player.IsNpc) {
			return;
		}
		int activeMission = player.GetActiveMission ();
		if (activeMission != -1 && activeMission < player.missions.Count) {
			MissionInstance missionInstance = player.missions [activeMission];
			BaseMission mission = missionInstance.GetMission ();
			if (mission.missionEntities.Length != 0) {
				mission.MissionFailed (missionInstance, player, MissionFailReason.Disconnect);
			}
		}
	}

	public static void PlayerKilled (BasePlayer player)
	{
	}

	public virtual Sprite GetIcon (MissionInstance instance)
	{
		return icon;
	}

	public virtual void SetupPositions (MissionInstance instance, BasePlayer assignee)
	{
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		Profiler.BeginSample ("BaseMission.SetupPositions");
		PositionGenerator[] array = positionGenerators;
		foreach (PositionGenerator positionGenerator in array) {
			if (!positionGenerator.IsDependant ()) {
				instance.missionPoints.Add (positionGenerator.GetIdentifier (), positionGenerator.GetPosition (assignee));
			}
		}
		PositionGenerator[] array2 = positionGenerators;
		foreach (PositionGenerator positionGenerator2 in array2) {
			if (positionGenerator2.IsDependant ()) {
				instance.missionPoints.Add (positionGenerator2.GetIdentifier (), positionGenerator2.GetPosition (assignee));
			}
		}
		Profiler.EndSample ();
	}

	public void AddBlockers (MissionInstance instance)
	{
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		foreach (KeyValuePair<string, Vector3> missionPoint in instance.missionPoints) {
			if (!blockedPoints.Contains (missionPoint.Value)) {
				blockedPoints.Add (missionPoint.Value);
			}
		}
	}

	public void RemoveBlockers (MissionInstance instance)
	{
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		foreach (KeyValuePair<string, Vector3> missionPoint in instance.missionPoints) {
			if (blockedPoints.Contains (missionPoint.Value)) {
				blockedPoints.Remove (missionPoint.Value);
			}
		}
	}

	public virtual void SetupRewards (MissionInstance instance, BasePlayer assignee)
	{
		if (baseRewards.Length != 0) {
			instance.rewards = new ItemAmount[baseRewards.Length];
			for (int i = 0; i < baseRewards.Length; i++) {
				instance.rewards [i] = new ItemAmount (baseRewards [i].itemDef, baseRewards [i].amount);
			}
		}
	}

	public static void DoMissionEffect (string effectString, BasePlayer assignee)
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		Effect effect = new Effect ();
		effect.Init (Effect.Type.Generic, assignee, StringPool.Get ("head"), Vector3.zero, Vector3.forward);
		effect.pooledString = effectString;
		EffectNetwork.Send (effect, assignee.net.connection);
	}

	public virtual void MissionStart (MissionInstance instance, BasePlayer assignee)
	{
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		SetupRewards (instance, assignee);
		SetupPositions (instance, assignee);
		AddBlockers (instance);
		for (int i = 0; i < objectives.Length; i++) {
			MissionObjectiveEntry missionObjectiveEntry = objectives [i];
			missionObjectiveEntry.Get ().MissionStarted (i, instance);
		}
		if (acceptEffect.isValid) {
			DoMissionEffect (acceptEffect.resourcePath, assignee);
		}
		MissionEntityEntry[] array = missionEntities;
		foreach (MissionEntityEntry missionEntityEntry in array) {
			if (missionEntityEntry.entityRef.isValid) {
				Vector3 missionPoint = instance.GetMissionPoint (missionEntityEntry.spawnPositionToUse, assignee);
				BaseEntity baseEntity = GameManager.server.CreateEntity (missionEntityEntry.entityRef.resourcePath, missionPoint, Quaternion.identity);
				MissionEntity missionEntity = ((Component)baseEntity).gameObject.AddComponent<MissionEntity> ();
				missionEntity.Setup (assignee, instance, missionEntityEntry.cleanupOnMissionSuccess, missionEntityEntry.cleanupOnMissionFailed);
				instance.createdEntities.Add (missionEntity);
				baseEntity.Spawn ();
			}
		}
		foreach (MissionEntity createdEntity in instance.createdEntities) {
			createdEntity.MissionStarted (assignee, instance);
		}
	}

	public void CheckObjectives (MissionInstance instance, BasePlayer assignee)
	{
		bool flag = true;
		for (int i = 0; i < objectives.Length; i++) {
			if (!instance.objectiveStatuses [i].completed || instance.objectiveStatuses [i].failed) {
				flag = false;
			}
		}
		if (flag && instance.status == MissionStatus.Active) {
			MissionSuccess (instance, assignee);
		}
	}

	public virtual void Think (MissionInstance instance, BasePlayer assignee, float delta)
	{
		Profiler.BeginSample ("BaseMission.Think");
		for (int i = 0; i < objectives.Length; i++) {
			MissionObjective missionObjective = objectives [i].Get ();
			missionObjective.Think (i, instance, assignee, delta);
		}
		CheckObjectives (instance, assignee);
		Profiler.EndSample ();
	}

	public virtual void MissionComplete (MissionInstance instance, BasePlayer assignee)
	{
		DoMissionEffect (victoryEffect.resourcePath, assignee);
		assignee.ChatMessage ("You have completed the mission : " + missionName.english);
		if (instance.rewards != null && instance.rewards.Length != 0) {
			ItemAmount[] rewards = instance.rewards;
			foreach (ItemAmount itemAmount in rewards) {
				if ((Object)(object)itemAmount.itemDef == (Object)null || itemAmount.amount == 0f) {
					Debug.LogError ((object)"BIG REWARD SCREWUP, NULL ITEM DEF");
				}
				Item item = ItemManager.Create (itemAmount.itemDef, Mathf.CeilToInt (itemAmount.amount), 0uL);
				if (item != null) {
					assignee.GiveItem (item, BaseEntity.GiveItemReason.PickedUp);
				}
			}
		}
		Analytics.Server.MissionComplete (this);
		Analytics.Azure.OnMissionComplete (assignee, this);
		instance.status = MissionStatus.Completed;
		assignee.SetActiveMission (-1);
		assignee.MissionDirty ();
		if (GameInfo.HasAchievements) {
			assignee.stats.Add ("missions_completed", 1, Stats.All);
			assignee.stats.Save (forceSteamSave: true);
		}
	}

	public virtual void MissionSuccess (MissionInstance instance, BasePlayer assignee)
	{
		instance.status = MissionStatus.Accomplished;
		MissionEnded (instance, assignee);
		MissionComplete (instance, assignee);
	}

	public virtual void MissionFailed (MissionInstance instance, BasePlayer assignee, MissionFailReason failReason)
	{
		assignee.ChatMessage ("You have failed the mission : " + missionName.english);
		DoMissionEffect (failedEffect.resourcePath, assignee);
		Analytics.Server.MissionFailed (this, failReason);
		Analytics.Azure.OnMissionComplete (assignee, this, failReason);
		instance.status = MissionStatus.Failed;
		MissionEnded (instance, assignee);
	}

	public virtual void MissionEnded (MissionInstance instance, BasePlayer assignee)
	{
		if (instance.createdEntities != null) {
			for (int num = instance.createdEntities.Count - 1; num >= 0; num--) {
				MissionEntity missionEntity = instance.createdEntities [num];
				if (!((Object)(object)missionEntity == (Object)null)) {
					missionEntity.MissionEnded (assignee, instance);
				}
			}
		}
		RemoveBlockers (instance);
		instance.endTime = Time.time;
		assignee.SetActiveMission (-1);
		assignee.MissionDirty ();
	}

	public void OnObjectiveCompleted (int objectiveIndex, MissionInstance instance, BasePlayer playerFor)
	{
		MissionObjectiveEntry missionObjectiveEntry = objectives [objectiveIndex];
		if (missionObjectiveEntry.autoCompleteOtherObjectives.Length != 0) {
			int[] autoCompleteOtherObjectives = missionObjectiveEntry.autoCompleteOtherObjectives;
			foreach (int num in autoCompleteOtherObjectives) {
				MissionObjectiveEntry missionObjectiveEntry2 = objectives [num];
				if (!instance.objectiveStatuses [num].completed) {
					missionObjectiveEntry2.objective.CompleteObjective (num, instance, playerFor);
				}
			}
		}
		CheckObjectives (instance, playerFor);
	}

	public static bool AssignMission (BasePlayer assignee, IMissionProvider provider, BaseMission mission)
	{
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		if (!missionsenabled) {
			return false;
		}
		if (!mission.IsEligableForMission (assignee, provider)) {
			return false;
		}
		MissionInstance missionInstance = Pool.Get<MissionInstance> ();
		missionInstance.missionID = mission.id;
		missionInstance.startTime = Time.time;
		missionInstance.providerID = provider.ProviderID ();
		missionInstance.status = MissionStatus.Active;
		missionInstance.createdEntities = Pool.GetList<MissionEntity> ();
		missionInstance.objectiveStatuses = new MissionInstance.ObjectiveStatus[mission.objectives.Length];
		for (int i = 0; i < mission.objectives.Length; i++) {
			missionInstance.objectiveStatuses [i] = new MissionInstance.ObjectiveStatus ();
		}
		assignee.AddMission (missionInstance);
		mission.MissionStart (missionInstance, assignee);
		assignee.SetActiveMission (assignee.missions.Count - 1);
		assignee.MissionDirty ();
		return true;
	}

	public bool IsEligableForMission (BasePlayer player, IMissionProvider provider)
	{
		if (!missionsenabled) {
			return false;
		}
		foreach (MissionInstance mission in player.missions) {
			if (mission.status == MissionStatus.Accomplished || mission.status == MissionStatus.Active) {
				return false;
			}
		}
		return true;
	}
}
