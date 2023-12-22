using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ConVar;
using Facepunch.Rust;
using Network;
using ProtoBuf;
using UnityEngine;
using UnityEngine.Assertions;

public class Planner : HeldEntity
{
	public BaseEntity[] buildableList;

	public bool isTypeDeployable => (Object)(object)GetModDeployable () != (Object)null;

	public override bool OnRpcMessage (BasePlayer player, uint rpc, Message msg)
	{
		TimeWarning val = TimeWarning.New ("Planner.OnRpcMessage", 0);
		try {
			if (rpc == 1872774636 && (Object)(object)player != (Object)null) {
				Assert.IsTrue (player.isServer, "SV_RPC Message is using a clientside player!");
				if (Global.developer > 2) {
					Debug.Log ((object)string.Concat ("SV_RPCMessage: ", player, " - DoPlace "));
				}
				TimeWarning val2 = TimeWarning.New ("DoPlace", 0);
				try {
					TimeWarning val3 = TimeWarning.New ("Conditions", 0);
					try {
						if (!RPC_Server.IsActiveItem.Test (1872774636u, "DoPlace", this, player)) {
							return true;
						}
					} finally {
						((IDisposable)val3)?.Dispose ();
					}
					try {
						TimeWarning val4 = TimeWarning.New ("Call", 0);
						try {
							RPCMessage rPCMessage = default(RPCMessage);
							rPCMessage.connection = msg.connection;
							rPCMessage.player = player;
							rPCMessage.read = msg.read;
							RPCMessage msg2 = rPCMessage;
							DoPlace (msg2);
						} finally {
							((IDisposable)val4)?.Dispose ();
						}
					} catch (Exception ex) {
						Debug.LogException (ex);
						player.Kick ("RPC Error in DoPlace");
					}
				} finally {
					((IDisposable)val2)?.Dispose ();
				}
				return true;
			}
		} finally {
			((IDisposable)val)?.Dispose ();
		}
		return base.OnRpcMessage (player, rpc, msg);
	}

	[RPC_Server]
	[RPC_Server.IsActiveItem]
	private void DoPlace (RPCMessage msg)
	{
		if (!msg.player.CanInteract ()) {
			return;
		}
		CreateBuilding val = CreateBuilding.Deserialize ((Stream)(object)msg.read);
		try {
			DoBuild (val);
		} finally {
			((IDisposable)val)?.Dispose ();
		}
	}

	public Socket_Base FindSocket (string name, uint prefabIDToFind)
	{
		Socket_Base[] source = PrefabAttribute.server.FindAll<Socket_Base> (prefabIDToFind);
		return source.FirstOrDefault ((Socket_Base s) => s.socketName == name);
	}

	public void DoBuild (CreateBuilding msg)
	{
		//IL_034e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0353: Unknown result type (might be due to invalid IL or missing references)
		//IL_0368: Unknown result type (might be due to invalid IL or missing references)
		//IL_036d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0375: Unknown result type (might be due to invalid IL or missing references)
		//IL_037a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0382: Unknown result type (might be due to invalid IL or missing references)
		//IL_0387: Unknown result type (might be due to invalid IL or missing references)
		//IL_01be: Unknown result type (might be due to invalid IL or missing references)
		//IL_0218: Unknown result type (might be due to invalid IL or missing references)
		//IL_021d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0233: Unknown result type (might be due to invalid IL or missing references)
		//IL_0238: Unknown result type (might be due to invalid IL or missing references)
		//IL_023d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0242: Unknown result type (might be due to invalid IL or missing references)
		//IL_0254: Unknown result type (might be due to invalid IL or missing references)
		//IL_0259: Unknown result type (might be due to invalid IL or missing references)
		//IL_025e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0270: Unknown result type (might be due to invalid IL or missing references)
		//IL_0275: Unknown result type (might be due to invalid IL or missing references)
		//IL_027a: Unknown result type (might be due to invalid IL or missing references)
		//IL_028b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0291: Unknown result type (might be due to invalid IL or missing references)
		//IL_0296: Unknown result type (might be due to invalid IL or missing references)
		//IL_029b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d5: Unknown result type (might be due to invalid IL or missing references)
		BasePlayer ownerPlayer = GetOwnerPlayer ();
		if (!Object.op_Implicit ((Object)(object)ownerPlayer)) {
			return;
		}
		if (ConVar.AntiHack.objectplacement && ownerPlayer.TriggeredAntiHack ()) {
			ownerPlayer.ChatMessage ("AntiHack!");
			return;
		}
		Construction construction = PrefabAttribute.server.Find<Construction> (msg.blockID);
		if (construction == null) {
			ownerPlayer.ChatMessage ("Couldn't find Construction " + msg.blockID);
			return;
		}
		if (!CanAffordToPlace (construction)) {
			ownerPlayer.ChatMessage ("Can't afford to place!");
			return;
		}
		if (!ownerPlayer.CanBuild () && !construction.canBypassBuildingPermission) {
			ownerPlayer.ChatMessage ("Building is blocked!");
			return;
		}
		Deployable deployable = GetDeployable ();
		if (construction.deployable != deployable) {
			ownerPlayer.ChatMessage ("Deployable mismatch!");
			AntiHack.NoteAdminHack (ownerPlayer);
			return;
		}
		if (Server.max_sleeping_bags > 0) {
			SleepingBag.CanBuildResult? canBuildResult = SleepingBag.CanBuildBed (ownerPlayer, construction);
			if (canBuildResult.HasValue) {
				if (canBuildResult.Value.Phrase != null) {
					ownerPlayer.ShowToast ((!canBuildResult.Value.Result) ? GameTip.Styles.Red_Normal : GameTip.Styles.Blue_Long, canBuildResult.Value.Phrase, canBuildResult.Value.Arguments);
				}
				if (!canBuildResult.Value.Result) {
					return;
				}
			}
		}
		Construction.Target target = default(Construction.Target);
		if (((NetworkableId)(ref msg.entity)).IsValid) {
			target.entity = BaseNetworkable.serverEntities.Find (msg.entity) as BaseEntity;
			if ((Object)(object)target.entity == (Object)null) {
				ownerPlayer.ChatMessage ("Couldn't find entity " + msg.entity);
				return;
			}
			msg.ray = new Ray (((Component)target.entity).transform.TransformPoint (((Ray)(ref msg.ray)).origin), ((Component)target.entity).transform.TransformDirection (((Ray)(ref msg.ray)).direction));
			msg.position = ((Component)target.entity).transform.TransformPoint (msg.position);
			msg.normal = ((Component)target.entity).transform.TransformDirection (msg.normal);
			msg.rotation = ((Component)target.entity).transform.rotation * msg.rotation;
			if (msg.socket != 0) {
				string text = StringPool.Get (msg.socket);
				if (text != "") {
					target.socket = FindSocket (text, target.entity.prefabID);
				}
				if (target.socket == null) {
					ownerPlayer.ChatMessage ("Couldn't find socket " + msg.socket);
					return;
				}
			} else if (target.entity is Door) {
				ownerPlayer.ChatMessage ("Can't deploy on door");
				return;
			}
		}
		target.ray = msg.ray;
		target.onTerrain = msg.onterrain;
		target.position = msg.position;
		target.normal = msg.normal;
		target.rotation = msg.rotation;
		target.player = ownerPlayer;
		target.valid = true;
		if (ShouldParent (target.entity, deployable)) {
			Vector3 position = ((target.socket != null) ? target.GetWorldPosition () : target.position);
			float num = target.entity.Distance (position);
			if (num > 1f) {
				ownerPlayer.ChatMessage ("Parent too far away: " + num);
				return;
			}
		}
		DoBuild (target, construction);
	}

	public void DoBuild (Construction.Target target, Construction component)
	{
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_0196: Unknown result type (might be due to invalid IL or missing references)
		//IL_019b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0254: Unknown result type (might be due to invalid IL or missing references)
		//IL_025d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0264: Unknown result type (might be due to invalid IL or missing references)
		//IL_0269: Unknown result type (might be due to invalid IL or missing references)
		//IL_026e: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_02dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02df: Unknown result type (might be due to invalid IL or missing references)
		//IL_02aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d5: Unknown result type (might be due to invalid IL or missing references)
		BasePlayer ownerPlayer = GetOwnerPlayer ();
		if (!Object.op_Implicit ((Object)(object)ownerPlayer) || target.ray.IsNaNOrInfinity () || Vector3Ex.IsNaNOrInfinity (target.position) || Vector3Ex.IsNaNOrInfinity (target.normal)) {
			return;
		}
		if (target.socket != null) {
			if (!target.socket.female) {
				ownerPlayer.ChatMessage ("Target socket is not female. (" + target.socket.socketName + ")");
				return;
			}
			if ((Object)(object)target.entity != (Object)null && target.entity.IsOccupied (target.socket)) {
				ownerPlayer.ChatMessage ("Target socket is occupied. (" + target.socket.socketName + ")");
				return;
			}
			if (target.onTerrain) {
				ownerPlayer.ChatMessage ("Target on terrain is not allowed when attaching to socket. (" + target.socket.socketName + ")");
				return;
			}
		}
		Vector3 val = (((Object)(object)target.entity != (Object)null && target.socket != null) ? target.GetWorldPosition () : target.position);
		if (AntiHack.TestIsBuildingInsideSomething (target, val)) {
			ownerPlayer.ChatMessage ("Can't deploy inside objects");
			return;
		}
		if (ConVar.AntiHack.eye_protection >= 2) {
			Vector3 center = ownerPlayer.eyes.center;
			Vector3 position = ownerPlayer.eyes.position;
			Vector3 origin = ((Ray)(ref target.ray)).origin;
			Vector3 val2 = val;
			int num = 2097152;
			int num2 = 2162688;
			if (ConVar.AntiHack.build_terraincheck) {
				num2 |= 0x800000;
			}
			float num3 = ConVar.AntiHack.build_losradius;
			float padding = ConVar.AntiHack.build_losradius + 0.01f;
			int layerMask = num2;
			if (target.socket != null) {
				num3 = 0f;
				padding = 0.5f;
				layerMask = num;
			}
			if (component.isSleepingBag) {
				num3 = ConVar.AntiHack.build_losradius_sleepingbag;
				padding = ConVar.AntiHack.build_losradius_sleepingbag + 0.01f;
				layerMask = num2;
			}
			if (num3 > 0f) {
				val2 += ((Vector3)(ref target.normal)).normalized * num3;
			}
			if ((Object)(object)target.entity != (Object)null) {
				DeployShell deployShell = PrefabAttribute.server.Find<DeployShell> (target.entity.prefabID);
				if (deployShell != null) {
					val2 += ((Vector3)(ref target.normal)).normalized * deployShell.LineOfSightPadding ();
				}
			}
			if (!GamePhysics.LineOfSightRadius (center, position, layerMask, num3) || !GamePhysics.LineOfSightRadius (position, origin, layerMask, num3) || !GamePhysics.LineOfSightRadius (origin, val2, layerMask, num3, 0f, padding)) {
				ownerPlayer.ChatMessage ("Line of sight blocked.");
				return;
			}
		}
		Construction.lastPlacementError = "No Error";
		GameObject val3 = DoPlacement (target, component);
		if ((Object)(object)val3 == (Object)null) {
			ownerPlayer.ChatMessage ("Can't place: " + Construction.lastPlacementError);
		}
		if (!((Object)(object)val3 != (Object)null)) {
			return;
		}
		Deployable deployable = GetDeployable ();
		BaseEntity baseEntity = val3.ToBaseEntity ();
		if ((Object)(object)baseEntity != (Object)null && deployable != null) {
			if (ShouldParent (target.entity, deployable)) {
				baseEntity.SetParent (target.entity, worldPositionStays: true);
			}
			if (deployable.wantsInstanceData && GetOwnerItem ().instanceData != null) {
				(baseEntity as IInstanceDataReceiver).ReceiveInstanceData (GetOwnerItem ().instanceData);
			}
			if (deployable.copyInventoryFromItem) {
				StorageContainer component2 = ((Component)baseEntity).GetComponent<StorageContainer> ();
				if (Object.op_Implicit ((Object)(object)component2)) {
					component2.ReceiveInventoryFromItem (GetOwnerItem ());
				}
			}
			ItemModDeployable modDeployable = GetModDeployable ();
			if ((Object)(object)modDeployable != (Object)null) {
				modDeployable.OnDeployed (baseEntity, ownerPlayer);
			}
			baseEntity.OnDeployed (baseEntity.GetParentEntity (), ownerPlayer, GetOwnerItem ());
			if (deployable.placeEffect.isValid) {
				if (Object.op_Implicit ((Object)(object)target.entity) && target.socket != null) {
					Effect.server.Run (deployable.placeEffect.resourcePath, ((Component)target.entity).transform.TransformPoint (target.socket.worldPosition), ((Component)target.entity).transform.up);
				} else {
					Effect.server.Run (deployable.placeEffect.resourcePath, target.position, target.normal);
				}
			}
		}
		if ((Object)(object)baseEntity != (Object)null) {
			Analytics.Azure.OnEntityBuilt (baseEntity, ownerPlayer);
		}
		PayForPlacement (ownerPlayer, component);
	}

	public GameObject DoPlacement (Construction.Target placement, Construction component)
	{
		//IL_019a: Unknown result type (might be due to invalid IL or missing references)
		//IL_019f: Unknown result type (might be due to invalid IL or missing references)
		BasePlayer ownerPlayer = GetOwnerPlayer ();
		if (!Object.op_Implicit ((Object)(object)ownerPlayer)) {
			return null;
		}
		BaseEntity baseEntity = component.CreateConstruction (placement, bNeedsValidPlacement: true);
		if (!Object.op_Implicit ((Object)(object)baseEntity)) {
			return null;
		}
		float num = 1f;
		float num2 = 0f;
		Item ownerItem = GetOwnerItem ();
		if (ownerItem != null) {
			baseEntity.skinID = ownerItem.skin;
			if (ownerItem.hasCondition) {
				num = ownerItem.conditionNormalized;
			}
		}
		((Component)baseEntity).gameObject.AwakeFromInstantiate ();
		BuildingBlock buildingBlock = baseEntity as BuildingBlock;
		if (Object.op_Implicit ((Object)(object)buildingBlock)) {
			buildingBlock.blockDefinition = PrefabAttribute.server.Find<Construction> (buildingBlock.prefabID);
			if (!buildingBlock.blockDefinition) {
				Debug.LogError ((object)"Placing a building block that has no block definition!");
				return null;
			}
			buildingBlock.SetGrade (buildingBlock.blockDefinition.defaultGrade.gradeBase.type);
			num2 = buildingBlock.currentGrade.maxHealth;
		}
		BaseCombatEntity baseCombatEntity = baseEntity as BaseCombatEntity;
		if (Object.op_Implicit ((Object)(object)baseCombatEntity)) {
			num2 = (((Object)(object)buildingBlock != (Object)null) ? buildingBlock.currentGrade.maxHealth : baseCombatEntity.startHealth);
			baseCombatEntity.ResetLifeStateOnSpawn = false;
			baseCombatEntity.InitializeHealth (num2 * num, num2);
		}
		((Component)baseEntity).gameObject.SendMessage ("SetDeployedBy", (object)ownerPlayer, (SendMessageOptions)1);
		baseEntity.OwnerID = ownerPlayer.userID;
		baseEntity.Spawn ();
		if (Object.op_Implicit ((Object)(object)buildingBlock)) {
			Effect.server.Run ("assets/bundled/prefabs/fx/build/frame_place.prefab", baseEntity, 0u, Vector3.zero, Vector3.zero);
		}
		StabilityEntity stabilityEntity = baseEntity as StabilityEntity;
		if (Object.op_Implicit ((Object)(object)stabilityEntity)) {
			stabilityEntity.UpdateSurroundingEntities ();
		}
		return ((Component)baseEntity).gameObject;
	}

	public void PayForPlacement (BasePlayer player, Construction component)
	{
		if (isTypeDeployable) {
			GetItem ().UseItem ();
			return;
		}
		List<Item> list = new List<Item> ();
		foreach (ItemAmount item in component.defaultGrade.CostToBuild ()) {
			player.inventory.Take (list, item.itemDef.itemid, (int)item.amount);
			player.Command ("note.inv", item.itemDef.itemid, item.amount * -1f);
		}
		foreach (Item item2 in list) {
			item2.Remove ();
		}
	}

	public bool CanAffordToPlace (Construction component)
	{
		if (isTypeDeployable) {
			return true;
		}
		BasePlayer ownerPlayer = GetOwnerPlayer ();
		if (!Object.op_Implicit ((Object)(object)ownerPlayer)) {
			return false;
		}
		foreach (ItemAmount item in component.defaultGrade.CostToBuild ()) {
			if ((float)ownerPlayer.inventory.GetAmount (item.itemDef.itemid) < item.amount) {
				return false;
			}
		}
		return true;
	}

	private bool ShouldParent (BaseEntity targetEntity, Deployable deployable)
	{
		if ((Object)(object)targetEntity != (Object)null && targetEntity.SupportsChildDeployables () && (targetEntity.ForceDeployableSetParent () || (deployable != null && deployable.setSocketParent))) {
			return true;
		}
		return false;
	}

	public ItemModDeployable GetModDeployable ()
	{
		ItemDefinition ownerItemDefinition = GetOwnerItemDefinition ();
		if ((Object)(object)ownerItemDefinition == (Object)null) {
			return null;
		}
		return ((Component)ownerItemDefinition).GetComponent<ItemModDeployable> ();
	}

	public Deployable GetDeployable ()
	{
		ItemModDeployable modDeployable = GetModDeployable ();
		if ((Object)(object)modDeployable == (Object)null) {
			return null;
		}
		return modDeployable.GetDeployable (this);
	}
}
