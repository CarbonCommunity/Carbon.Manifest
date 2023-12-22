#define ENABLE_PROFILER
using ConVar;
using ProtoBuf;
using UnityEngine;
using UnityEngine.Profiling;

public class ScarecrowNPC : NPCPlayer, IAISenses, IAIAttack, IThinker
{
	public float BaseAttackRate = 2f;

	[Header ("Loot")]
	public LootContainer.LootSpawnSlot[] LootSpawnSlots;

	public static float NextBeanCanAllowedTime;

	public bool BlockClothingOnCorpse;

	public bool RoamAroundHomePoint = false;

	public ScarecrowBrain Brain { get; protected set; }

	public override BaseNpc.AiStatistics.FamilyEnum Family => BaseNpc.AiStatistics.FamilyEnum.Murderer;

	public override float StartHealth ()
	{
		return startHealth;
	}

	public override float StartMaxHealth ()
	{
		return startHealth;
	}

	public override float MaxHealth ()
	{
		return startHealth;
	}

	public override void ServerInit ()
	{
		base.ServerInit ();
		Brain = GetComponent<ScarecrowBrain> ();
		if (!base.isClient) {
			AIThinkManager.Add (this);
		}
	}

	internal override void DoServerDestroy ()
	{
		AIThinkManager.Remove (this);
		base.DoServerDestroy ();
	}

	public virtual void TryThink ()
	{
		Profiler.BeginSample ("ScarecrowNPC.TryThink");
		ServerThink_Internal ();
		Profiler.EndSample ();
	}

	public override void ServerThink (float delta)
	{
		base.ServerThink (delta);
		if (Brain.ShouldServerThink ()) {
			Brain.DoThink ();
		}
	}

	public override string Categorize ()
	{
		return "Scarecrow";
	}

	public override void EquipWeapon (bool skipDeployDelay = false)
	{
		base.EquipWeapon (skipDeployDelay);
		HeldEntity heldEntity = GetHeldEntity ();
		if (heldEntity != null && heldEntity is Chainsaw chainsaw) {
			chainsaw.ServerNPCStart ();
		}
	}

	public float EngagementRange ()
	{
		AttackEntity attackEntity = GetAttackEntity ();
		if ((bool)attackEntity) {
			return attackEntity.effectiveRange * (attackEntity.aiOnlyInRange ? 1f : 2f) * Brain.AttackRangeMultiplier;
		}
		return Brain.SenseRange;
	}

	public bool IsThreat (BaseEntity entity)
	{
		return IsTarget (entity);
	}

	public bool IsTarget (BaseEntity entity)
	{
		return entity is BasePlayer && !entity.IsNpc;
	}

	public bool IsFriendly (BaseEntity entity)
	{
		return false;
	}

	public bool CanAttack (BaseEntity entity)
	{
		if (entity == null) {
			return false;
		}
		if (NeedsToReload ()) {
			return false;
		}
		if (IsOnCooldown ()) {
			return false;
		}
		if (!IsTargetInRange (entity, out var _)) {
			return false;
		}
		if (InSafeZone () || (entity is BasePlayer basePlayer && basePlayer.InSafeZone ())) {
			return false;
		}
		if (!CanSeeTarget (entity)) {
			return false;
		}
		return true;
	}

	public bool IsTargetInRange (BaseEntity entity, out float dist)
	{
		dist = Vector3.Distance (entity.transform.position, base.transform.position);
		return dist <= EngagementRange ();
	}

	public bool CanSeeTarget (BaseEntity entity)
	{
		if (entity == null) {
			return false;
		}
		Profiler.BeginSample ("ScarecrowNPC.CanSeeTarget");
		bool result = entity.IsVisible (GetEntity ().CenterPoint (), entity.CenterPoint ());
		Profiler.EndSample ();
		return result;
	}

	public bool NeedsToReload ()
	{
		return false;
	}

	public bool Reload ()
	{
		return true;
	}

	public float CooldownDuration ()
	{
		return BaseAttackRate;
	}

	public bool IsOnCooldown ()
	{
		AttackEntity attackEntity = GetAttackEntity ();
		if ((bool)attackEntity) {
			return attackEntity.HasAttackCooldown ();
		}
		return true;
	}

	public bool StartAttacking (BaseEntity target)
	{
		BaseCombatEntity baseCombatEntity = target as BaseCombatEntity;
		if (baseCombatEntity == null) {
			return false;
		}
		Attack (baseCombatEntity);
		return true;
	}

	private void Attack (BaseCombatEntity target)
	{
		if (!(target == null)) {
			Vector3 vector = target.ServerPosition - ServerPosition;
			float magnitude = vector.magnitude;
			if (magnitude > 0.001f) {
				ServerRotation = Quaternion.LookRotation (vector.normalized);
			}
			AttackEntity attackEntity = GetAttackEntity ();
			if ((bool)attackEntity) {
				attackEntity.ServerUse ();
			}
		}
	}

	public void StopAttacking ()
	{
	}

	public float GetAmmoFraction ()
	{
		return AmmoFractionRemaining ();
	}

	public BaseEntity GetBestTarget ()
	{
		Profiler.BeginSample ("ScarecrowNPC.GetBestTarget");
		Profiler.EndSample ();
		return null;
	}

	public void AttackTick (float delta, BaseEntity target, bool targetIsLOS)
	{
	}

	public override bool ShouldDropActiveItem ()
	{
		return false;
	}

	public override BaseCorpse CreateCorpse ()
	{
		using (TimeWarning.New ("Create corpse")) {
			string strCorpsePrefab = "assets/prefabs/npc/murderer/murderer_corpse.prefab";
			NPCPlayerCorpse nPCPlayerCorpse = DropCorpse (strCorpsePrefab) as NPCPlayerCorpse;
			if ((bool)nPCPlayerCorpse) {
				nPCPlayerCorpse.transform.position = nPCPlayerCorpse.transform.position + Vector3.down * NavAgent.baseOffset;
				nPCPlayerCorpse.SetLootableIn (2f);
				nPCPlayerCorpse.SetFlag (Flags.Reserved5, HasPlayerFlag (PlayerFlags.DisplaySash));
				nPCPlayerCorpse.SetFlag (Flags.Reserved2, b: true);
				nPCPlayerCorpse.TakeFrom (inventory.containerMain, inventory.containerWear, inventory.containerBelt);
				nPCPlayerCorpse.playerName = "Scarecrow";
				nPCPlayerCorpse.playerSteamID = userID;
				nPCPlayerCorpse.Spawn ();
				ItemContainer[] containers = nPCPlayerCorpse.containers;
				foreach (ItemContainer itemContainer in containers) {
					itemContainer.Clear ();
				}
				if (LootSpawnSlots.Length != 0) {
					LootContainer.LootSpawnSlot[] lootSpawnSlots = LootSpawnSlots;
					for (int j = 0; j < lootSpawnSlots.Length; j++) {
						LootContainer.LootSpawnSlot lootSpawnSlot = lootSpawnSlots [j];
						for (int k = 0; k < lootSpawnSlot.numberToSpawn; k++) {
							float num = Random.Range (0f, 1f);
							if (num <= lootSpawnSlot.probability) {
								lootSpawnSlot.definition.SpawnIntoContainer (nPCPlayerCorpse.containers [0]);
							}
						}
					}
				}
			}
			return nPCPlayerCorpse;
		}
	}

	public override void Hurt (HitInfo info)
	{
		if (!info.isHeadshot) {
			if ((info.InitiatorPlayer != null && !info.InitiatorPlayer.IsNpc) || (info.InitiatorPlayer == null && info.Initiator != null && info.Initiator.IsNpc)) {
				info.damageTypes.ScaleAll (Halloween.scarecrow_body_dmg_modifier);
			} else {
				info.damageTypes.ScaleAll (2f);
			}
		}
		base.Hurt (info);
	}

	public override void AttackerInfo (PlayerLifeStory.DeathInfo info)
	{
		base.AttackerInfo (info);
		info.inflictorName = inventory.containerBelt.GetSlot (0).info.shortname;
		info.attackerName = base.ShortPrefabName;
	}
}
