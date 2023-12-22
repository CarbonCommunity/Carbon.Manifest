#define ENABLE_PROFILER
using System;
using System.Collections;
using System.Collections.Generic;
using ProtoBuf;
using UnityEngine;
using UnityEngine.Profiling;

public class HumanNPC : NPCPlayer, IAISenses, IAIAttack, IThinker
{
	[Header ("LOS")]
	public int AdditionalLosBlockingLayer = 0;

	[Header ("Loot")]
	public LootContainer.LootSpawnSlot[] LootSpawnSlots;

	[Header ("Damage")]
	public float aimConeScale = 2f;

	public float lastDismountTime = 0f;

	[NonSerialized]
	protected bool lightsOn = false;

	private float nextZoneSearchTime = 0f;

	private AIInformationZone cachedInfoZone = null;

	private float targetAimedDuration = 0f;

	private float lastAimSetTime = 0f;

	private Vector3 aimOverridePosition = Vector3.zero;

	public ScientistBrain Brain { get; private set; }

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

	public override bool IsLoadBalanced ()
	{
		return true;
	}

	public override void ServerInit ()
	{
		base.ServerInit ();
		Brain = GetComponent<ScientistBrain> ();
		if (!base.isClient) {
			AIThinkManager.Add (this);
		}
	}

	internal override void DoServerDestroy ()
	{
		AIThinkManager.Remove (this);
		base.DoServerDestroy ();
	}

	public void LightCheck ()
	{
		if ((TOD_Sky.Instance.IsNight && !lightsOn) || (TOD_Sky.Instance.IsDay && lightsOn)) {
			LightToggle ();
			lightsOn = !lightsOn;
		}
	}

	public override float GetAimConeScale ()
	{
		return aimConeScale;
	}

	public override void EquipWeapon (bool skipDeployDelay = false)
	{
		base.EquipWeapon (skipDeployDelay);
	}

	public override void DismountObject ()
	{
		base.DismountObject ();
		lastDismountTime = Time.time;
	}

	public bool RecentlyDismounted ()
	{
		return Time.time < lastDismountTime + 10f;
	}

	public virtual float GetIdealDistanceFromTarget ()
	{
		return Mathf.Max (5f, EngagementRange () * 0.75f);
	}

	public AIInformationZone GetInformationZone (Vector3 pos)
	{
		if (VirtualInfoZone != null) {
			return VirtualInfoZone;
		}
		if (cachedInfoZone == null || Time.time > nextZoneSearchTime) {
			cachedInfoZone = AIInformationZone.GetForPoint (pos);
			nextZoneSearchTime = Time.time + 5f;
		}
		return cachedInfoZone;
	}

	public float EngagementRange ()
	{
		AttackEntity attackEntity = GetAttackEntity ();
		if ((bool)attackEntity) {
			return attackEntity.effectiveRange * (attackEntity.aiOnlyInRange ? 1f : 2f) * Brain.AttackRangeMultiplier;
		}
		return Brain.SenseRange;
	}

	public void SetDucked (bool flag)
	{
		modelState.ducked = flag;
		SendNetworkUpdate ();
	}

	public virtual void TryThink ()
	{
		Profiler.BeginSample ("HumanNPCNew.TryThink");
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

	public void TickAttack (float delta, BaseCombatEntity target, bool targetIsLOS)
	{
		if (target == null) {
			return;
		}
		float num = Vector3.Dot (eyes.BodyForward (), (target.CenterPoint () - eyes.position).normalized);
		if (targetIsLOS) {
			if (num > 0.2f) {
				targetAimedDuration += delta;
			}
		} else {
			if (num < 0.5f) {
				targetAimedDuration = 0f;
			}
			CancelBurst ();
		}
		if (targetAimedDuration >= 0.2f && targetIsLOS) {
			bool flag = false;
			float dist = 0f;
			if ((object)this != null) {
				flag = ((IAIAttack)this).IsTargetInRange ((BaseEntity)target, out dist);
			} else {
				AttackEntity attackEntity = GetAttackEntity ();
				if ((bool)attackEntity) {
					dist = ((target != null) ? Vector3.Distance (base.transform.position, target.transform.position) : (-1f));
					flag = dist < attackEntity.effectiveRange * (attackEntity.aiOnlyInRange ? 1f : 2f);
				}
			}
			if (flag) {
				ShotTest (dist);
			}
		} else {
			CancelBurst ();
		}
	}

	public override void Hurt (HitInfo info)
	{
		if (base.isMounted) {
			info.damageTypes.ScaleAll (0.1f);
		}
		base.Hurt (info);
		BaseEntity initiator = info.Initiator;
		if (initiator != null && !initiator.EqualNetID (this)) {
			Brain.Senses.Memory.SetKnown (initiator, this, null);
		}
	}

	public float GetAimSwayScalar ()
	{
		return 1f - Mathf.InverseLerp (1f, 3f, Time.time - lastGunShotTime);
	}

	public override Vector3 GetAimDirection ()
	{
		if (Brain != null && Brain.Navigator != null && Brain.Navigator.IsOverridingFacingDirection) {
			return Brain.Navigator.FacingDirectionOverride;
		}
		return base.GetAimDirection ();
	}

	public override void SetAimDirection (Vector3 newAim)
	{
		if (newAim == Vector3.zero) {
			return;
		}
		float num = Time.time - lastAimSetTime;
		lastAimSetTime = Time.time;
		AttackEntity attackEntity = GetAttackEntity ();
		if ((bool)attackEntity) {
			newAim = attackEntity.ModifyAIAim (newAim, GetAimSwayScalar ());
		}
		if (base.isMounted) {
			BaseMountable baseMountable = GetMounted ();
			Vector3 eulerAngles = baseMountable.transform.eulerAngles;
			Quaternion quaternion = Quaternion.Euler (Quaternion.LookRotation (newAim, baseMountable.transform.up).eulerAngles);
			Vector3 forward = base.transform.InverseTransformDirection (quaternion * Vector3.forward);
			Vector3 eulerAngles2 = Quaternion.LookRotation (forward, base.transform.up).eulerAngles;
			eulerAngles2 = BaseMountable.ConvertVector (eulerAngles2);
			Quaternion quaternion2 = Quaternion.Euler (Mathf.Clamp (eulerAngles2.x, baseMountable.pitchClamp.x, baseMountable.pitchClamp.y), Mathf.Clamp (eulerAngles2.y, baseMountable.yawClamp.x, baseMountable.yawClamp.y), eulerAngles.z);
			Vector3 forward2 = base.transform.TransformDirection (quaternion2 * Vector3.forward);
			Vector3 eulerAngles3 = Quaternion.LookRotation (forward2, base.transform.up).eulerAngles;
			newAim = BaseMountable.ConvertVector (eulerAngles3);
		} else {
			BaseEntity baseEntity = GetParentEntity ();
			if ((bool)baseEntity) {
				Vector3 vector = baseEntity.transform.InverseTransformDirection (newAim);
				Vector3 forward3 = new Vector3 (newAim.x, vector.y, newAim.z);
				eyes.rotation = Quaternion.Lerp (eyes.rotation, Quaternion.LookRotation (forward3, baseEntity.transform.up), num * 25f);
				viewAngles = eyes.bodyRotation.eulerAngles;
				ServerRotation = eyes.bodyRotation;
				return;
			}
		}
		eyes.rotation = (base.isMounted ? Quaternion.Slerp (eyes.rotation, Quaternion.Euler (newAim), num * 70f) : Quaternion.Lerp (eyes.rotation, Quaternion.LookRotation (newAim, base.transform.up), num * 25f));
		viewAngles = eyes.rotation.eulerAngles;
		ServerRotation = eyes.rotation;
	}

	public void SetStationaryAimPoint (Vector3 aimAt)
	{
		aimOverridePosition = aimAt;
	}

	public void ClearStationaryAimPoint ()
	{
		aimOverridePosition = Vector3.zero;
	}

	public override bool ShouldDropActiveItem ()
	{
		return false;
	}

	public override BaseCorpse CreateCorpse ()
	{
		using (TimeWarning.New ("Create corpse")) {
			NPCPlayerCorpse nPCPlayerCorpse = DropCorpse ("assets/prefabs/npc/scientist/scientist_corpse.prefab") as NPCPlayerCorpse;
			if ((bool)nPCPlayerCorpse) {
				nPCPlayerCorpse.transform.position = nPCPlayerCorpse.transform.position + Vector3.down * NavAgent.baseOffset;
				nPCPlayerCorpse.SetLootableIn (2f);
				nPCPlayerCorpse.SetFlag (Flags.Reserved5, HasPlayerFlag (PlayerFlags.DisplaySash));
				nPCPlayerCorpse.SetFlag (Flags.Reserved2, b: true);
				nPCPlayerCorpse.TakeFrom (inventory.containerMain, inventory.containerWear, inventory.containerBelt);
				nPCPlayerCorpse.playerName = OverrideCorpseName ();
				nPCPlayerCorpse.playerSteamID = userID;
				nPCPlayerCorpse.Spawn ();
				nPCPlayerCorpse.TakeChildren (this);
				for (int i = 0; i < nPCPlayerCorpse.containers.Length; i++) {
					ItemContainer itemContainer = nPCPlayerCorpse.containers [i];
					if (i != 1) {
						itemContainer.Clear ();
					}
				}
				if (LootSpawnSlots.Length != 0) {
					LootContainer.LootSpawnSlot[] lootSpawnSlots = LootSpawnSlots;
					for (int j = 0; j < lootSpawnSlots.Length; j++) {
						LootContainer.LootSpawnSlot lootSpawnSlot = lootSpawnSlots [j];
						for (int k = 0; k < lootSpawnSlot.numberToSpawn; k++) {
							float num = UnityEngine.Random.Range (0f, 1f);
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

	protected virtual string OverrideCorpseName ()
	{
		return base.displayName;
	}

	public override void AttackerInfo (PlayerLifeStory.DeathInfo info)
	{
		base.AttackerInfo (info);
		info.inflictorName = inventory.containerBelt.GetSlot (0).info.shortname;
		info.attackerName = base.ShortPrefabName;
	}

	public bool IsThreat (BaseEntity entity)
	{
		return IsTarget (entity);
	}

	public bool IsTarget (BaseEntity entity)
	{
		if (entity is BasePlayer && !entity.IsNpc) {
			return true;
		}
		if (entity is BasePet) {
			return true;
		}
		if (entity is ScarecrowNPC) {
			return true;
		}
		return false;
	}

	public bool IsFriendly (BaseEntity entity)
	{
		if (entity == null) {
			return false;
		}
		return entity.prefabID == prefabID;
	}

	public bool CanAttack (BaseEntity entity)
	{
		return true;
	}

	public bool IsTargetInRange (BaseEntity entity, out float dist)
	{
		dist = Vector3.Distance (entity.transform.position, base.transform.position);
		return dist <= EngagementRange ();
	}

	public bool CanSeeTarget (BaseEntity entity)
	{
		BasePlayer basePlayer = entity as BasePlayer;
		if (basePlayer == null) {
			return true;
		}
		if (AdditionalLosBlockingLayer == 0) {
			return IsPlayerVisibleToUs (basePlayer, 1218519041);
		}
		return IsPlayerVisibleToUs (basePlayer, 0x48A12001 | (1 << AdditionalLosBlockingLayer));
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
		return 5f;
	}

	public bool IsOnCooldown ()
	{
		return false;
	}

	public bool StartAttacking (BaseEntity entity)
	{
		return true;
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
		Profiler.BeginSample ("HumanNPC.GetBestTarget");
		BaseEntity result = null;
		float num = -1f;
		List<BaseEntity> players = Brain.Senses.Players;
		foreach (BaseEntity item in players) {
			if (!(item == null) && !(item.Health () <= 0f)) {
				float value = Vector3.Distance (item.transform.position, base.transform.position);
				float num2 = 1f - Mathf.InverseLerp (1f, Brain.SenseRange, value);
				float value2 = Vector3.Dot ((item.transform.position - eyes.position).normalized, eyes.BodyForward ());
				num2 += Mathf.InverseLerp (Brain.VisionCone, 1f, value2) / 2f;
				num2 += (Brain.Senses.Memory.IsLOS (item) ? 2f : 0f);
				if (num2 > num) {
					result = item;
					num = num2;
				}
			}
		}
		Profiler.EndSample ();
		return result;
	}

	public void AttackTick (float delta, BaseEntity target, bool targetIsLOS)
	{
		BaseCombatEntity target2 = target as BaseCombatEntity;
		TickAttack (delta, target2, targetIsLOS);
	}

	public void UseHealingItem (Item item)
	{
		StartCoroutine (Heal (item));
	}

	private IEnumerator Heal (Item item)
	{
		UpdateActiveItem (item.uid);
		Item activeItem = GetActiveItem ();
		MedicalTool heldItem = activeItem.GetHeldEntity () as MedicalTool;
		if (!(heldItem == null)) {
			yield return new WaitForSeconds (1f);
			heldItem.ServerUse ();
			Heal (MaxHealth ());
			yield return new WaitForSeconds (2f);
			EquipWeapon ();
		}
	}

	public Item FindHealingItem ()
	{
		if (Brain == null) {
			return null;
		}
		if (!Brain.CanUseHealingItems) {
			return null;
		}
		if (inventory == null || inventory.containerBelt == null) {
			return null;
		}
		for (int i = 0; i < inventory.containerBelt.capacity; i++) {
			Item slot = inventory.containerBelt.GetSlot (i);
			if (slot != null && slot.amount > 1) {
				MedicalTool medicalTool = slot.GetHeldEntity () as MedicalTool;
				if (medicalTool != null) {
					return slot;
				}
			}
		}
		return null;
	}

	public override bool IsOnGround ()
	{
		return true;
	}
}
