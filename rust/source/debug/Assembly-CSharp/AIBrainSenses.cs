#define ENABLE_PROFILER
using System;
using System.Collections.Generic;
using ConVar;
using Rust.AI;
using UnityEngine;
using UnityEngine.Profiling;

public class AIBrainSenses
{
	[ServerVar]
	public static float UpdateInterval = 0.5f;

	[ServerVar]
	public static float HumanKnownPlayersLOSUpdateInterval = 0.2f;

	[ServerVar]
	public static float KnownPlayersLOSUpdateInterval = 0.5f;

	private float knownPlayersLOSUpdateInterval = 0.2f;

	public float MemoryDuration = 10f;

	public float LastThreatTimestamp;

	public float TimeInAgressiveState;

	private static BaseEntity[] queryResults = new BaseEntity[64];

	private static BasePlayer[] playerQueryResults = new BasePlayer[64];

	private float nextUpdateTime;

	private float nextKnownPlayersLOSUpdateTime;

	private BaseEntity owner;

	private BasePlayer playerOwner;

	private IAISenses ownerSenses;

	private float maxRange;

	private float targetLostRange;

	private float visionCone;

	private bool checkVision;

	private bool checkLOS;

	private bool ignoreNonVisionSneakers;

	private float listenRange;

	private bool hostileTargetsOnly = false;

	private bool senseFriendlies = false;

	private bool refreshKnownLOS = false;

	private EntityType senseTypes;

	private IAIAttack ownerAttack;

	public BaseAIBrain brain;

	private Func<BaseEntity, bool> aiCaresAbout;

	public float TimeSinceThreat => UnityEngine.Time.realtimeSinceStartup - LastThreatTimestamp;

	public SimpleAIMemory Memory { get; private set; } = new SimpleAIMemory ();


	public float TargetLostRange => targetLostRange;

	public bool ignoreSafeZonePlayers { get; private set; }

	public List<BaseEntity> Players => Memory.Players;

	public void Init (BaseEntity owner, BaseAIBrain brain, float memoryDuration, float range, float targetLostRange, float visionCone, bool checkVision, bool checkLOS, bool ignoreNonVisionSneakers, float listenRange, bool hostileTargetsOnly, bool senseFriendlies, bool ignoreSafeZonePlayers, EntityType senseTypes, bool refreshKnownLOS)
	{
		aiCaresAbout = AiCaresAbout;
		this.owner = owner;
		this.brain = brain;
		MemoryDuration = memoryDuration;
		ownerAttack = owner as IAIAttack;
		playerOwner = owner as BasePlayer;
		maxRange = range;
		this.targetLostRange = targetLostRange;
		this.visionCone = visionCone;
		this.checkVision = checkVision;
		this.checkLOS = checkLOS;
		this.ignoreNonVisionSneakers = ignoreNonVisionSneakers;
		this.listenRange = listenRange;
		this.hostileTargetsOnly = hostileTargetsOnly;
		this.senseFriendlies = senseFriendlies;
		this.ignoreSafeZonePlayers = ignoreSafeZonePlayers;
		this.senseTypes = senseTypes;
		LastThreatTimestamp = UnityEngine.Time.realtimeSinceStartup;
		this.refreshKnownLOS = refreshKnownLOS;
		ownerSenses = owner as IAISenses;
		knownPlayersLOSUpdateInterval = ((owner is HumanNPC) ? HumanKnownPlayersLOSUpdateInterval : KnownPlayersLOSUpdateInterval);
	}

	public void DelaySenseUpdate (float delay)
	{
		nextUpdateTime = UnityEngine.Time.time + delay;
	}

	public void Update ()
	{
		if (!(owner == null)) {
			UpdateSenses ();
			UpdateKnownPlayersLOS ();
		}
	}

	private void UpdateSenses ()
	{
		if (UnityEngine.Time.time < nextUpdateTime) {
			return;
		}
		Profiler.BeginSample ("AIBrainSenses.UpdateSenses");
		nextUpdateTime = UnityEngine.Time.time + UpdateInterval;
		if (senseTypes != 0) {
			if (senseTypes == EntityType.Player) {
				SensePlayers ();
			} else {
				SenseBrains ();
				if (senseTypes.HasFlag (EntityType.Player)) {
					SensePlayers ();
				}
			}
		}
		Memory.Forget (MemoryDuration);
		Profiler.EndSample ();
	}

	public void UpdateKnownPlayersLOS ()
	{
		if (UnityEngine.Time.time < nextKnownPlayersLOSUpdateTime) {
			return;
		}
		Profiler.BeginSample ("AIBrainSenses.UpdateKnownPlayersLOS");
		nextKnownPlayersLOSUpdateTime = UnityEngine.Time.time + knownPlayersLOSUpdateInterval;
		foreach (BaseEntity player in Memory.Players) {
			if (!(player == null) && !player.IsNpc) {
				bool flag = ownerAttack.CanSeeTarget (player);
				Memory.SetLOS (player, flag);
				if (refreshKnownLOS && owner != null && flag && Vector3.Distance (player.transform.position, owner.transform.position) <= TargetLostRange) {
					Memory.SetKnown (player, owner, this);
				}
			}
		}
		Profiler.EndSample ();
	}

	private void SensePlayers ()
	{
		Profiler.BeginSample ("AIBrainSenses.SensePlayers.GetPlayersInSphereAndAiCaresAbout");
		int playersInSphere = BaseEntity.Query.Server.GetPlayersInSphere (owner.transform.position, maxRange, playerQueryResults, aiCaresAbout);
		Profiler.EndSample ();
		Profiler.BeginSample ("AIBrainSenses.SensePlayers.SetKnown");
		for (int i = 0; i < playersInSphere; i++) {
			BasePlayer ent = playerQueryResults [i];
			Memory.SetKnown (ent, owner, this);
		}
		Profiler.EndSample ();
	}

	private void SenseBrains ()
	{
		Profiler.BeginSample ("AIBrainSenses.SenseBrains.GetBrainsInSphereAndAiCaresAbout");
		int brainsInSphere = BaseEntity.Query.Server.GetBrainsInSphere (owner.transform.position, maxRange, queryResults, aiCaresAbout);
		Profiler.EndSample ();
		Profiler.BeginSample ("AIBrainSenses.SenseBrains.SetKnown");
		for (int i = 0; i < brainsInSphere; i++) {
			BaseEntity ent = queryResults [i];
			Memory.SetKnown (ent, owner, this);
		}
		Profiler.EndSample ();
	}

	private bool AiCaresAbout (BaseEntity entity)
	{
		if (entity == null) {
			return false;
		}
		if (!entity.isServer) {
			return false;
		}
		if (entity.EqualNetID (owner)) {
			return false;
		}
		if (entity.Health () <= 0f) {
			return false;
		}
		Profiler.BeginSample ("AIBrainSenses.IsValidSenseType");
		if (!IsValidSenseType (entity)) {
			Profiler.EndSample ();
			return false;
		}
		Profiler.EndSample ();
		BaseCombatEntity baseCombatEntity = entity as BaseCombatEntity;
		BasePlayer basePlayer = entity as BasePlayer;
		if (basePlayer != null && basePlayer.IsDead ()) {
			return false;
		}
		if (ignoreSafeZonePlayers && basePlayer != null && basePlayer.InSafeZone ()) {
			return false;
		}
		Profiler.BeginSample ("AIBrainSenses.CanLastNoiseBeHeard");
		if (listenRange > 0f && baseCombatEntity != null && baseCombatEntity.TimeSinceLastNoise <= 1f && baseCombatEntity.CanLastNoiseBeHeard (owner.transform.position, listenRange)) {
			Profiler.EndSample ();
			return true;
		}
		Profiler.EndSample ();
		Profiler.BeginSample ("AIBrainSenses.ShouldSenseFriendlies");
		if (senseFriendlies && ownerSenses != null && ownerSenses.IsFriendly (entity)) {
			Profiler.EndSample ();
			return true;
		}
		Profiler.EndSample ();
		float num = float.PositiveInfinity;
		Profiler.BeginSample ("AIBrainSenses.DistanceCheck");
		if (baseCombatEntity != null && AI.accuratevisiondistance) {
			num = Vector3.Distance (owner.transform.position, baseCombatEntity.transform.position);
			if (num > maxRange) {
				Profiler.EndSample ();
				return false;
			}
		}
		Profiler.EndSample ();
		Profiler.BeginSample ("AIBrainSenses.Vision");
		if (checkVision && !IsTargetInVision (entity)) {
			if (!ignoreNonVisionSneakers) {
				Profiler.EndSample ();
				return false;
			}
			if (basePlayer != null && !basePlayer.IsNpc) {
				if (!AI.accuratevisiondistance) {
					num = Vector3.Distance (owner.transform.position, basePlayer.transform.position);
				}
				if ((basePlayer.IsDucked () && num >= brain.IgnoreSneakersMaxDistance) || num >= brain.IgnoreNonVisionMaxDistance) {
					Profiler.EndSample ();
					return false;
				}
			}
		}
		Profiler.EndSample ();
		Profiler.BeginSample ("AIBrainSenses.HostileTargetsOnly");
		if (hostileTargetsOnly && baseCombatEntity != null && !baseCombatEntity.IsHostile () && !(baseCombatEntity is ScarecrowNPC)) {
			Profiler.EndSample ();
			return false;
		}
		Profiler.EndSample ();
		if (checkLOS && ownerAttack != null) {
			Profiler.BeginSample ("AIBrainSenses.AiCaresAbout.CheckLOS");
			bool flag = ownerAttack.CanSeeTarget (entity);
			Memory.SetLOS (entity, flag);
			Profiler.EndSample ();
			if (!flag) {
				return false;
			}
		}
		return true;
	}

	private bool IsValidSenseType (BaseEntity ent)
	{
		BasePlayer basePlayer = ent as BasePlayer;
		if (basePlayer != null) {
			if (basePlayer.IsNpc) {
				if (ent is BasePet) {
					return true;
				}
				if (ent is ScarecrowNPC) {
					return true;
				}
				if (senseTypes.HasFlag (EntityType.BasePlayerNPC)) {
					return true;
				}
			} else if (senseTypes.HasFlag (EntityType.Player)) {
				return true;
			}
		}
		if (senseTypes.HasFlag (EntityType.NPC) && ent is BaseNpc) {
			return true;
		}
		if (senseTypes.HasFlag (EntityType.WorldItem) && ent is WorldItem) {
			return true;
		}
		if (senseTypes.HasFlag (EntityType.Corpse) && ent is BaseCorpse) {
			return true;
		}
		if (senseTypes.HasFlag (EntityType.TimedExplosive) && ent is TimedExplosive) {
			return true;
		}
		if (senseTypes.HasFlag (EntityType.Chair) && ent is BaseChair) {
			return true;
		}
		return false;
	}

	private bool IsTargetInVision (BaseEntity target)
	{
		Profiler.BeginSample ("AIBrainSenses.IsTargetInVision");
		Vector3 rhs = Vector3Ex.Direction (target.transform.position, owner.transform.position);
		Vector3 lhs = ((playerOwner != null) ? playerOwner.eyes.BodyForward () : owner.transform.forward);
		float num = Vector3.Dot (lhs, rhs);
		bool result = num >= visionCone;
		Profiler.EndSample ();
		return result;
	}

	public BaseEntity GetNearestPlayer (float rangeFraction)
	{
		return GetNearest (Memory.Players, rangeFraction);
	}

	public BaseEntity GetNearestThreat (float rangeFraction)
	{
		return GetNearest (Memory.Threats, rangeFraction);
	}

	public BaseEntity GetNearestTarget (float rangeFraction)
	{
		return GetNearest (Memory.Targets, rangeFraction);
	}

	private BaseEntity GetNearest (List<BaseEntity> entities, float rangeFraction)
	{
		if (entities == null || entities.Count == 0) {
			return null;
		}
		Profiler.BeginSample ("AIBrainSenses.GetNearest");
		float num = float.PositiveInfinity;
		BaseEntity result = null;
		foreach (BaseEntity entity in entities) {
			if (!(entity == null) && !(entity.Health () <= 0f)) {
				float num2 = Vector3.Distance (entity.transform.position, owner.transform.position);
				if (num2 <= rangeFraction * maxRange && num2 < num) {
					result = entity;
				}
			}
		}
		Profiler.EndSample ();
		return result;
	}
}
