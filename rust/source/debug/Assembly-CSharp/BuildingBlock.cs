#define ENABLE_PROFILER
#define UNITY_ASSERTIONS
using System;
using System.Collections.Generic;
using ConVar;
using Facepunch;
using Facepunch.Rust;
using Network;
using ProtoBuf;
using Rust;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Profiling;

public class BuildingBlock : StabilityEntity
{
	public static class BlockFlags
	{
		public const Flags CanRotate = Flags.Reserved1;

		public const Flags CanDemolish = Flags.Reserved2;
	}

	public class UpdateSkinWorkQueue : ObjectWorkQueue<BuildingBlock>
	{
		protected override void RunJob (BuildingBlock entity)
		{
			if (ShouldAdd (entity)) {
				entity.UpdateSkin (force: true);
			}
		}

		protected override bool ShouldAdd (BuildingBlock entity)
		{
			return entity.IsValid ();
		}
	}

	private bool forceSkinRefresh = false;

	private ulong lastSkinID = 0uL;

	private int modelState = 0;

	private int lastModelState = 0;

	private uint lastCustomColour = 0u;

	private uint playerCustomColourToApply = 0u;

	public BuildingGrade.Enum grade = BuildingGrade.Enum.Twigs;

	private BuildingGrade.Enum lastGrade = BuildingGrade.Enum.None;

	private ConstructionSkin currentSkin;

	private DeferredAction skinChange;

	private MeshRenderer placeholderRenderer = null;

	private MeshCollider placeholderCollider = null;

	public static UpdateSkinWorkQueue updateSkinQueueServer = new UpdateSkinWorkQueue ();

	public bool CullBushes = false;

	public bool CheckForPipesOnModelChange;

	[NonSerialized]
	public Construction blockDefinition = null;

	private static Vector3[] outsideLookupOffsets = new Vector3[5] {
		new Vector3 (0f, 1f, 0f).normalized,
		new Vector3 (1f, 1f, 0f).normalized,
		new Vector3 (-1f, 1f, 0f).normalized,
		new Vector3 (0f, 1f, 1f).normalized,
		new Vector3 (0f, 1f, -1f).normalized
	};

	public uint customColour { get; private set; } = 0u;


	public ConstructionGrade currentGrade => blockDefinition.GetGrade (grade, skinID);

	public override bool OnRpcMessage (BasePlayer player, uint rpc, Message msg)
	{
		using (TimeWarning.New ("BuildingBlock.OnRpcMessage")) {
			if (rpc == 2858062413u && player != null) {
				Assert.IsTrue (player.isServer, "SV_RPC Message is using a clientside player!");
				if (ConVar.Global.developer > 2) {
					Debug.Log (string.Concat ("SV_RPCMessage: ", player, " - DoDemolish "));
				}
				using (TimeWarning.New ("DoDemolish")) {
					using (TimeWarning.New ("Conditions")) {
						if (!RPC_Server.MaxDistance.Test (2858062413u, "DoDemolish", this, player, 3f)) {
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
							DoDemolish (msg2);
						}
					} catch (Exception exception) {
						Debug.LogException (exception);
						player.Kick ("RPC Error in DoDemolish");
					}
				}
				return true;
			}
			if (rpc == 216608990 && player != null) {
				Assert.IsTrue (player.isServer, "SV_RPC Message is using a clientside player!");
				if (ConVar.Global.developer > 2) {
					Debug.Log (string.Concat ("SV_RPCMessage: ", player, " - DoImmediateDemolish "));
				}
				using (TimeWarning.New ("DoImmediateDemolish")) {
					using (TimeWarning.New ("Conditions")) {
						if (!RPC_Server.MaxDistance.Test (216608990u, "DoImmediateDemolish", this, player, 3f)) {
							return true;
						}
					}
					try {
						using (TimeWarning.New ("Call")) {
							RPCMessage rPCMessage = default(RPCMessage);
							rPCMessage.connection = msg.connection;
							rPCMessage.player = player;
							rPCMessage.read = msg.read;
							RPCMessage msg3 = rPCMessage;
							DoImmediateDemolish (msg3);
						}
					} catch (Exception exception2) {
						Debug.LogException (exception2);
						player.Kick ("RPC Error in DoImmediateDemolish");
					}
				}
				return true;
			}
			if (rpc == 1956645865 && player != null) {
				Assert.IsTrue (player.isServer, "SV_RPC Message is using a clientside player!");
				if (ConVar.Global.developer > 2) {
					Debug.Log (string.Concat ("SV_RPCMessage: ", player, " - DoRotation "));
				}
				using (TimeWarning.New ("DoRotation")) {
					using (TimeWarning.New ("Conditions")) {
						if (!RPC_Server.MaxDistance.Test (1956645865u, "DoRotation", this, player, 3f)) {
							return true;
						}
					}
					try {
						using (TimeWarning.New ("Call")) {
							RPCMessage rPCMessage = default(RPCMessage);
							rPCMessage.connection = msg.connection;
							rPCMessage.player = player;
							rPCMessage.read = msg.read;
							RPCMessage msg4 = rPCMessage;
							DoRotation (msg4);
						}
					} catch (Exception exception3) {
						Debug.LogException (exception3);
						player.Kick ("RPC Error in DoRotation");
					}
				}
				return true;
			}
			if (rpc == 3746288057u && player != null) {
				Assert.IsTrue (player.isServer, "SV_RPC Message is using a clientside player!");
				if (ConVar.Global.developer > 2) {
					Debug.Log (string.Concat ("SV_RPCMessage: ", player, " - DoUpgradeToGrade "));
				}
				using (TimeWarning.New ("DoUpgradeToGrade")) {
					using (TimeWarning.New ("Conditions")) {
						if (!RPC_Server.MaxDistance.Test (3746288057u, "DoUpgradeToGrade", this, player, 3f)) {
							return true;
						}
					}
					try {
						using (TimeWarning.New ("Call")) {
							RPCMessage rPCMessage = default(RPCMessage);
							rPCMessage.connection = msg.connection;
							rPCMessage.player = player;
							rPCMessage.read = msg.read;
							RPCMessage msg5 = rPCMessage;
							DoUpgradeToGrade (msg5);
						}
					} catch (Exception exception4) {
						Debug.LogException (exception4);
						player.Kick ("RPC Error in DoUpgradeToGrade");
					}
				}
				return true;
			}
			if (rpc == 4081052216u && player != null) {
				Assert.IsTrue (player.isServer, "SV_RPC Message is using a clientside player!");
				if (ConVar.Global.developer > 2) {
					Debug.Log (string.Concat ("SV_RPCMessage: ", player, " - DoUpgradeToGrade_Delayed "));
				}
				using (TimeWarning.New ("DoUpgradeToGrade_Delayed")) {
					using (TimeWarning.New ("Conditions")) {
						if (!RPC_Server.MaxDistance.Test (4081052216u, "DoUpgradeToGrade_Delayed", this, player, 3f)) {
							return true;
						}
					}
					try {
						using (TimeWarning.New ("Call")) {
							RPCMessage rPCMessage = default(RPCMessage);
							rPCMessage.connection = msg.connection;
							rPCMessage.player = player;
							rPCMessage.read = msg.read;
							RPCMessage msg6 = rPCMessage;
							DoUpgradeToGrade_Delayed (msg6);
						}
					} catch (Exception exception5) {
						Debug.LogException (exception5);
						player.Kick ("RPC Error in DoUpgradeToGrade_Delayed");
					}
				}
				return true;
			}
		}
		return base.OnRpcMessage (player, rpc, msg);
	}

	private bool CanDemolish (BasePlayer player)
	{
		return IsDemolishable () && HasDemolishPrivilege (player);
	}

	private bool IsDemolishable ()
	{
		if (!ConVar.Server.pve && !HasFlag (Flags.Reserved2)) {
			return false;
		}
		return true;
	}

	private bool HasDemolishPrivilege (BasePlayer player)
	{
		return player.IsBuildingAuthed (base.transform.position, base.transform.rotation, bounds);
	}

	[RPC_Server]
	[RPC_Server.MaxDistance (3f)]
	private void DoDemolish (RPCMessage msg)
	{
		if (msg.player.CanInteract () && CanDemolish (msg.player)) {
			Analytics.Azure.OnBuildingBlockDemolished (msg.player, this);
			Kill (DestroyMode.Gib);
		}
	}

	[RPC_Server]
	[RPC_Server.MaxDistance (3f)]
	private void DoImmediateDemolish (RPCMessage msg)
	{
		if (msg.player.CanInteract () && msg.player.IsAdmin) {
			Analytics.Azure.OnBuildingBlockDemolished (msg.player, this);
			Kill (DestroyMode.Gib);
		}
	}

	private void StopBeingDemolishable ()
	{
		SetFlag (Flags.Reserved2, b: false);
		SendNetworkUpdate ();
	}

	private void StartBeingDemolishable ()
	{
		SetFlag (Flags.Reserved2, b: true);
		Invoke (StopBeingDemolishable, 600f);
	}

	public void SetConditionalModel (int state)
	{
		modelState = state;
	}

	public bool GetConditionalModel (int index)
	{
		return (modelState & (1 << index)) != 0;
	}

	private bool CanChangeToGrade (BuildingGrade.Enum iGrade, ulong iSkin, BasePlayer player)
	{
		return HasUpgradePrivilege (iGrade, iSkin, player) && !IsUpgradeBlocked ();
	}

	private bool HasUpgradePrivilege (BuildingGrade.Enum iGrade, ulong iSkin, BasePlayer player)
	{
		if (iGrade < grade) {
			return false;
		}
		if (iGrade == grade && iSkin == skinID) {
			return false;
		}
		if (iGrade <= BuildingGrade.Enum.None) {
			return false;
		}
		if (iGrade >= BuildingGrade.Enum.Count) {
			return false;
		}
		return !player.IsBuildingBlocked (base.transform.position, base.transform.rotation, bounds);
	}

	private bool IsUpgradeBlocked ()
	{
		if (!blockDefinition.checkVolumeOnUpgrade) {
			return false;
		}
		DeployVolume[] volumes = PrefabAttribute.server.FindAll<DeployVolume> (prefabID);
		return DeployVolume.Check (base.transform.position, base.transform.rotation, volumes, ~(1 << base.gameObject.layer));
	}

	private bool CanAffordUpgrade (BuildingGrade.Enum iGrade, ulong iSkin, BasePlayer player)
	{
		ConstructionGrade constructionGrade = blockDefinition.GetGrade (iGrade, iSkin);
		foreach (ItemAmount item in constructionGrade.CostToBuild (grade)) {
			if ((float)player.inventory.GetAmount (item.itemid) < item.amount) {
				return false;
			}
		}
		return true;
	}

	public void SetGrade (BuildingGrade.Enum iGrade)
	{
		if (blockDefinition.grades == null || iGrade <= BuildingGrade.Enum.None || iGrade >= BuildingGrade.Enum.Count) {
			Debug.LogError ("Tried to set to undefined grade! " + blockDefinition.fullName, base.gameObject);
			return;
		}
		grade = iGrade;
		grade = currentGrade.gradeBase.type;
		UpdateGrade ();
	}

	private void UpdateGrade ()
	{
		baseProtection = currentGrade.gradeBase.damageProtecton;
	}

	protected override void OnSkinChanged (ulong oldSkinID, ulong newSkinID)
	{
		if (oldSkinID != newSkinID) {
			skinID = newSkinID;
		}
	}

	protected override void OnSkinPreProcess (IPrefabProcessor preProcess, GameObject rootObj, string name, bool serverside, bool clientside, bool bundling)
	{
	}

	public void SetHealthToMax ()
	{
		base.health = MaxHealth ();
	}

	[RPC_Server]
	[RPC_Server.MaxDistance (3f)]
	private void DoUpgradeToGrade_Delayed (RPCMessage msg)
	{
		if (!msg.player.CanInteract ()) {
			return;
		}
		BuildingGrade.Enum @enum = (BuildingGrade.Enum)msg.read.Int32 ();
		ulong num = msg.read.UInt64 ();
		ConstructionGrade constructionGrade = blockDefinition.GetGrade (@enum, num);
		if (!(constructionGrade == null) && CanChangeToGrade (@enum, num, msg.player) && CanAffordUpgrade (@enum, num, msg.player) && !(base.SecondsSinceAttacked < 30f) && (num == 0L || msg.player.blueprints.steamInventory.HasItem ((int)num))) {
			PayForUpgrade (constructionGrade, msg.player);
			if (msg.player != null) {
				playerCustomColourToApply = msg.player.LastBlockColourChangeId;
			}
			ClientRPC (null, "DoUpgradeEffect", (int)@enum, num);
			Analytics.Azure.OnBuildingBlockUpgraded (msg.player, this, @enum, playerCustomColourToApply);
			OnSkinChanged (skinID, num);
			ChangeGrade (@enum, playEffect: true);
		}
	}

	[RPC_Server]
	[RPC_Server.MaxDistance (3f)]
	private void DoUpgradeToGrade (RPCMessage msg)
	{
		if (!msg.player.CanInteract ()) {
			return;
		}
		BuildingGrade.Enum @enum = (BuildingGrade.Enum)msg.read.Int32 ();
		ulong num = msg.read.UInt64 ();
		ConstructionGrade constructionGrade = blockDefinition.GetGrade (@enum, num);
		if (!(constructionGrade == null) && CanChangeToGrade (@enum, num, msg.player) && CanAffordUpgrade (@enum, num, msg.player) && !(base.SecondsSinceAttacked < 30f) && (num == 0L || msg.player.blueprints.steamInventory.HasItem ((int)num))) {
			PayForUpgrade (constructionGrade, msg.player);
			if (msg.player != null) {
				playerCustomColourToApply = msg.player.LastBlockColourChangeId;
			}
			ClientRPC (null, "DoUpgradeEffect", (int)@enum, num);
			Analytics.Azure.OnBuildingBlockUpgraded (msg.player, this, @enum, playerCustomColourToApply);
			OnSkinChanged (skinID, num);
			ChangeGrade (@enum, playEffect: true);
		}
	}

	public void ChangeGradeAndSkin (BuildingGrade.Enum targetGrade, ulong skin, bool playEffect = false, bool updateSkin = true)
	{
		OnSkinChanged (skinID, skin);
		ChangeGrade (targetGrade, playEffect, updateSkin);
	}

	public void ChangeGrade (BuildingGrade.Enum targetGrade, bool playEffect = false, bool updateSkin = true)
	{
		SetGrade (targetGrade);
		if (grade != lastGrade) {
			SetHealthToMax ();
			StartBeingRotatable ();
		}
		if (updateSkin) {
			UpdateSkin ();
		}
		SendNetworkUpdate ();
		ResetUpkeepTime ();
		UpdateSurroundingEntities ();
		BuildingManager.server.GetBuilding (buildingID)?.Dirty ();
		if (!playEffect) {
		}
	}

	private void PayForUpgrade (ConstructionGrade g, BasePlayer player)
	{
		List<Item> list = new List<Item> ();
		foreach (ItemAmount item in g.CostToBuild (grade)) {
			player.inventory.Take (list, item.itemid, (int)item.amount);
			ItemDefinition itemDefinition = ItemManager.FindItemDefinition (item.itemid);
			Analytics.Azure.LogResource (Analytics.Azure.ResourceMode.Consumed, "upgrade_block", itemDefinition.shortname, (int)item.amount, this, null, safezone: false, null, player.userID);
			player.Command ("note.inv " + item.itemid + " " + item.amount * -1f);
		}
		foreach (Item item2 in list) {
			item2.Remove ();
		}
	}

	public void SetCustomColour (uint newColour)
	{
		if (newColour != customColour) {
			customColour = newColour;
			SendNetworkUpdateImmediate ();
			ClientRPC (null, "RefreshSkin");
		}
	}

	private bool NeedsSkinChange ()
	{
		return currentSkin == null || forceSkinRefresh || lastGrade != grade || lastModelState != modelState || lastSkinID != skinID;
	}

	public void UpdateSkin (bool force = false)
	{
		if (force) {
			forceSkinRefresh = true;
		}
		if (!NeedsSkinChange ()) {
			return;
		}
		if (cachedStability <= 0f || base.isServer) {
			ChangeSkin ();
			return;
		}
		if (!skinChange) {
			skinChange = new DeferredAction (this, ChangeSkin);
		}
		if (skinChange.Idle) {
			skinChange.Invoke ();
		}
	}

	private void DestroySkin ()
	{
		if (currentSkin != null) {
			currentSkin.Destroy (this);
			currentSkin = null;
		}
	}

	private void RefreshNeighbours (bool linkToNeighbours)
	{
		List<EntityLink> entityLinks = GetEntityLinks (linkToNeighbours);
		for (int i = 0; i < entityLinks.Count; i++) {
			EntityLink entityLink = entityLinks [i];
			for (int j = 0; j < entityLink.connections.Count; j++) {
				BuildingBlock buildingBlock = entityLink.connections [j].owner as BuildingBlock;
				if (!(buildingBlock == null)) {
					if (Rust.Application.isLoading) {
						buildingBlock.UpdateSkin (force: true);
					} else {
						updateSkinQueueServer.Add (buildingBlock);
					}
				}
			}
		}
	}

	private void UpdatePlaceholder (bool state)
	{
		if ((bool)placeholderRenderer) {
			placeholderRenderer.enabled = state;
		}
		if ((bool)placeholderCollider) {
			placeholderCollider.enabled = state;
		}
	}

	private void ChangeSkin ()
	{
		if (base.IsDestroyed) {
			return;
		}
		ConstructionGrade constructionGrade = currentGrade;
		if (constructionGrade.skinObject.isValid) {
			ChangeSkin (constructionGrade.skinObject);
			return;
		}
		ConstructionGrade defaultGrade = blockDefinition.defaultGrade;
		if (defaultGrade.skinObject.isValid) {
			ChangeSkin (defaultGrade.skinObject);
		} else {
			Debug.LogWarning ("No skins found for " + base.gameObject);
		}
	}

	private void ChangeSkin (GameObjectRef prefab)
	{
		Profiler.BeginSample ("ChangeSkin");
		bool flag = lastGrade != grade || lastSkinID != skinID;
		lastGrade = grade;
		lastSkinID = skinID;
		if (flag) {
			if (currentSkin == null) {
				UpdatePlaceholder (state: false);
			} else {
				DestroySkin ();
			}
			GameObject gameObject = base.gameManager.CreatePrefab (prefab.resourcePath, base.transform);
			currentSkin = gameObject.GetComponent<ConstructionSkin> ();
			if (currentSkin != null && base.isServer && !Rust.Application.isLoading) {
				customColour = currentSkin.GetStartingDetailColour (playerCustomColourToApply);
			}
			Model component = currentSkin.GetComponent<Model> ();
			SetModel (component);
			Assert.IsTrue (model == component, "Didn't manage to set model successfully!");
		}
		if (base.isServer) {
			modelState = currentSkin.DetermineConditionalModelState (this);
		}
		bool flag2 = lastModelState != modelState;
		lastModelState = modelState;
		bool flag3 = lastCustomColour != customColour;
		lastCustomColour = customColour;
		if (flag || flag2 || forceSkinRefresh || flag3) {
			currentSkin.Refresh (this);
			if (base.isServer && flag2) {
				CheckForPipes ();
			}
			forceSkinRefresh = false;
		}
		if (base.isServer) {
			if (flag) {
				RefreshNeighbours (linkToNeighbours: true);
			}
			if (flag2) {
				SendNetworkUpdate ();
			}
		}
		Profiler.EndSample ();
	}

	public override bool ShouldBlockProjectiles ()
	{
		return grade != BuildingGrade.Enum.Twigs;
	}

	public void CheckForPipes ()
	{
		if (!CheckForPipesOnModelChange || !ConVar.Server.enforcePipeChecksOnBuildingBlockChanges || Rust.Application.isLoading) {
			return;
		}
		List<ColliderInfo_Pipe> obj = Facepunch.Pool.GetList<ColliderInfo_Pipe> ();
		OBB oBB = new OBB (base.transform, bounds);
		Vis.Components (oBB, obj, 536870912);
		foreach (ColliderInfo_Pipe item in obj) {
			if (!(item == null) && item.gameObject.activeInHierarchy && item.HasFlag (ColliderInfo.Flags.OnlyBlockBuildingBlock) && item.ParentEntity != null && item.ParentEntity.isServer) {
				WireTool.AttemptClearSlot (item.ParentEntity, null, item.OutputSlotIndex, isInput: false);
			}
		}
		Facepunch.Pool.FreeList (ref obj);
	}

	private void OnHammered ()
	{
	}

	public override float MaxHealth ()
	{
		return currentGrade.maxHealth;
	}

	public override List<ItemAmount> BuildCost ()
	{
		return currentGrade.CostToBuild ();
	}

	public override void OnHealthChanged (float oldvalue, float newvalue)
	{
		base.OnHealthChanged (oldvalue, newvalue);
		if (base.isServer && Mathf.RoundToInt (oldvalue) != Mathf.RoundToInt (newvalue)) {
			SendNetworkUpdate (BasePlayer.NetworkQueue.UpdateDistance);
		}
	}

	public override float RepairCostFraction ()
	{
		return 1f;
	}

	private bool CanRotate (BasePlayer player)
	{
		return IsRotatable () && HasRotationPrivilege (player) && !IsRotationBlocked ();
	}

	private bool IsRotatable ()
	{
		if (blockDefinition.grades == null) {
			return false;
		}
		if (!blockDefinition.canRotateAfterPlacement) {
			return false;
		}
		if (!HasFlag (Flags.Reserved1)) {
			return false;
		}
		return true;
	}

	private bool IsRotationBlocked ()
	{
		if (!blockDefinition.checkVolumeOnRotate) {
			return false;
		}
		DeployVolume[] volumes = PrefabAttribute.server.FindAll<DeployVolume> (prefabID);
		return DeployVolume.Check (base.transform.position, base.transform.rotation, volumes, ~(1 << base.gameObject.layer));
	}

	private bool HasRotationPrivilege (BasePlayer player)
	{
		return !player.IsBuildingBlocked (base.transform.position, base.transform.rotation, bounds);
	}

	[RPC_Server]
	[RPC_Server.MaxDistance (3f)]
	private void DoRotation (RPCMessage msg)
	{
		if (msg.player.CanInteract () && CanRotate (msg.player) && blockDefinition.canRotateAfterPlacement) {
			base.transform.localRotation *= Quaternion.Euler (blockDefinition.rotationAmount);
			RefreshEntityLinks ();
			UpdateSurroundingEntities ();
			UpdateSkin (force: true);
			RefreshNeighbours (linkToNeighbours: false);
			SendNetworkUpdateImmediate ();
			ClientRPC (null, "RefreshSkin");
		}
	}

	private void StopBeingRotatable ()
	{
		SetFlag (Flags.Reserved1, b: false);
		SendNetworkUpdate ();
	}

	private void StartBeingRotatable ()
	{
		if (blockDefinition.grades != null && blockDefinition.canRotateAfterPlacement) {
			SetFlag (Flags.Reserved1, b: true);
			Invoke (StopBeingRotatable, 600f);
		}
	}

	public override void Save (SaveInfo info)
	{
		base.Save (info);
		Profiler.BeginSample ("BuildingBlock.Save");
		info.msg.buildingBlock = Facepunch.Pool.Get<ProtoBuf.BuildingBlock> ();
		info.msg.buildingBlock.model = modelState;
		info.msg.buildingBlock.grade = (int)grade;
		if (customColour != 0) {
			info.msg.simpleUint = Facepunch.Pool.Get<SimpleUInt> ();
			info.msg.simpleUint.value = customColour;
		}
		Profiler.EndSample ();
	}

	public override void Load (LoadInfo info)
	{
		base.Load (info);
		customColour = 0u;
		if (info.msg.simpleUint != null) {
			customColour = info.msg.simpleUint.value;
		}
		if (info.msg.buildingBlock != null) {
			SetConditionalModel (info.msg.buildingBlock.model);
			SetGrade ((BuildingGrade.Enum)info.msg.buildingBlock.grade);
		}
		if (info.fromDisk) {
			SetFlag (Flags.Reserved2, b: false);
			SetFlag (Flags.Reserved1, b: false);
			UpdateSkin ();
		}
	}

	public override void AttachToBuilding (DecayEntity other)
	{
		if (other != null && other is BuildingBlock) {
			AttachToBuilding (other.buildingID);
			BuildingManager.server.CheckMerge (this);
		} else {
			AttachToBuilding (BuildingManager.server.NewBuildingID ());
		}
	}

	public override void ServerInit ()
	{
		blockDefinition = PrefabAttribute.server.Find<Construction> (prefabID);
		if (blockDefinition == null) {
			Debug.LogError ("Couldn't find Construction for prefab " + prefabID);
		}
		base.ServerInit ();
		UpdateSkin ();
		if (HasFlag (Flags.Reserved1) || !Rust.Application.isLoadingSave) {
			StartBeingRotatable ();
		}
		if (HasFlag (Flags.Reserved2) || !Rust.Application.isLoadingSave) {
			StartBeingDemolishable ();
		}
		if (!CullBushes || Rust.Application.isLoadingSave) {
			return;
		}
		List<BushEntity> obj = Facepunch.Pool.GetList<BushEntity> ();
		Vis.Entities (WorldSpaceBounds (), obj, 67108864);
		foreach (BushEntity item in obj) {
			if (item.isServer) {
				item.Kill ();
			}
		}
		Facepunch.Pool.FreeList (ref obj);
	}

	public override void Hurt (HitInfo info)
	{
		if (ConVar.Server.pve && (bool)info.Initiator && info.Initiator is BasePlayer) {
			BasePlayer basePlayer = info.Initiator as BasePlayer;
			basePlayer.Hurt (info.damageTypes.Total (), DamageType.Generic);
		} else {
			base.Hurt (info);
		}
	}

	public override void ResetState ()
	{
		base.ResetState ();
		blockDefinition = null;
		forceSkinRefresh = false;
		modelState = 0;
		lastModelState = 0;
		grade = BuildingGrade.Enum.Twigs;
		lastGrade = BuildingGrade.Enum.None;
		DestroySkin ();
		UpdatePlaceholder (state: true);
	}

	public override void InitShared ()
	{
		base.InitShared ();
		Profiler.BeginSample ("GetComponent");
		placeholderRenderer = GetComponent<MeshRenderer> ();
		placeholderCollider = GetComponent<MeshCollider> ();
		Profiler.EndSample ();
	}

	public override void PostInitShared ()
	{
		baseProtection = currentGrade.gradeBase.damageProtecton;
		grade = currentGrade.gradeBase.type;
		base.PostInitShared ();
	}

	public override void DestroyShared ()
	{
		if (base.isServer) {
			RefreshNeighbours (linkToNeighbours: false);
		}
		base.DestroyShared ();
	}

	public override string Categorize ()
	{
		return "building";
	}

	public override float BoundsPadding ()
	{
		return 1f;
	}

	public override bool IsOutside ()
	{
		float outside_test_range = ConVar.Decay.outside_test_range;
		Vector3 vector = PivotPoint ();
		for (int i = 0; i < outsideLookupOffsets.Length; i++) {
			Vector3 vector2 = outsideLookupOffsets [i];
			Vector3 origin = vector + vector2 * outside_test_range;
			Ray ray = new Ray (origin, -vector2);
			if (!UnityEngine.Physics.Raycast (ray, outside_test_range - 0.5f, 2097152)) {
				return true;
			}
		}
		return false;
	}

	public override bool SupportsChildDeployables ()
	{
		return true;
	}
}
