using System;
using ConVar;
using Facepunch;
using Network;
using ProtoBuf;
using UnityEngine;
using UnityEngine.Assertions;

public class IndustrialCrafter : IndustrialEntity, IItemContainerEntity, IIdealSlotEntity, ILootableEntity, LootPanel.IHasLootPanel, IContainerSounds, IIndustrialStorage
{
	public string LootPanelName = "generic";

	public bool NeedsBuildingPrivilegeToUse = false;

	public bool OnlyOneUser = false;

	public SoundDefinition ContainerOpenSound = null;

	public SoundDefinition ContainerCloseSound = null;

	public AnimationCurve MaterialOffsetCurve = AnimationCurve.Linear (0f, 0f, 1f, 1f);

	public const Flags Crafting = Flags.Reserved1;

	public const Flags FullOutput = Flags.Reserved2;

	public Renderer[] MeshRenderers;

	public ParticleSystemContainer JobCompleteFx;

	public SoundDefinition JobCompleteSoundDef;

	public const int BlueprintSlotStart = 0;

	public const int BlueprintSlotEnd = 3;

	private ItemDefinition currentlyCrafting = null;

	private int currentlyCraftingAmount = 0;

	private const int StorageSize = 12;

	private const int InputSlotStart = 4;

	private const int InputSlotEnd = 7;

	private const int OutputSlotStart = 8;

	private const int OutputSlotEnd = 11;

	public TimeUntilWithDuration jobFinishes { get; private set; }

	public ItemContainer inventory { get; set; }

	public Transform Transform => ((Component)this).transform;

	public bool DropsLoot => true;

	public float DestroyLootPercent => 0f;

	public bool DropFloats { get; }

	public ulong LastLootedBy { get; set; }

	public ItemContainer Container => inventory;

	public BaseEntity IndustrialEntity => this;

	public Phrase LootPanelTitle => new Phrase ("industrial.crafter.loot", "Industrial Crafter");

	public SoundDefinition OpenSound => ContainerOpenSound;

	public SoundDefinition CloseSound => ContainerCloseSound;

	public override bool OnRpcMessage (BasePlayer player, uint rpc, Message msg)
	{
		TimeWarning val = TimeWarning.New ("IndustrialCrafter.OnRpcMessage", 0);
		try {
			if (rpc == 331989034 && (Object)(object)player != (Object)null) {
				Assert.IsTrue (player.isServer, "SV_RPC Message is using a clientside player!");
				if (Global.developer > 2) {
					Debug.Log ((object)string.Concat ("SV_RPCMessage: ", player, " - RPC_OpenLoot "));
				}
				TimeWarning val2 = TimeWarning.New ("RPC_OpenLoot", 0);
				try {
					TimeWarning val3 = TimeWarning.New ("Conditions", 0);
					try {
						if (!RPC_Server.IsVisible.Test (331989034u, "RPC_OpenLoot", this, player, 3f)) {
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
							RPCMessage rpc2 = rPCMessage;
							RPC_OpenLoot (rpc2);
						} finally {
							((IDisposable)val4)?.Dispose ();
						}
					} catch (Exception ex) {
						Debug.LogException (ex);
						player.Kick ("RPC Error in RPC_OpenLoot");
					}
				} finally {
					((IDisposable)val2)?.Dispose ();
				}
				return true;
			}
			if (rpc == 4167839872u && (Object)(object)player != (Object)null) {
				Assert.IsTrue (player.isServer, "SV_RPC Message is using a clientside player!");
				if (Global.developer > 2) {
					Debug.Log ((object)string.Concat ("SV_RPCMessage: ", player, " - SvSwitch "));
				}
				TimeWarning val5 = TimeWarning.New ("SvSwitch", 0);
				try {
					TimeWarning val6 = TimeWarning.New ("Conditions", 0);
					try {
						if (!RPC_Server.CallsPerSecond.Test (4167839872u, "SvSwitch", this, player, 2uL)) {
							return true;
						}
						if (!RPC_Server.IsVisible.Test (4167839872u, "SvSwitch", this, player, 3f)) {
							return true;
						}
					} finally {
						((IDisposable)val6)?.Dispose ();
					}
					try {
						TimeWarning val7 = TimeWarning.New ("Call", 0);
						try {
							RPCMessage rPCMessage = default(RPCMessage);
							rPCMessage.connection = msg.connection;
							rPCMessage.player = player;
							rPCMessage.read = msg.read;
							RPCMessage msg2 = rPCMessage;
							SvSwitch (msg2);
						} finally {
							((IDisposable)val7)?.Dispose ();
						}
					} catch (Exception ex2) {
						Debug.LogException (ex2);
						player.Kick ("RPC Error in SvSwitch");
					}
				} finally {
					((IDisposable)val5)?.Dispose ();
				}
				return true;
			}
		} finally {
			((IDisposable)val)?.Dispose ();
		}
		return base.OnRpcMessage (player, rpc, msg);
	}

	public override void OnFlagsChanged (Flags old, Flags next)
	{
		base.OnFlagsChanged (old, next);
		bool flag = next.HasFlag (Flags.On);
		if (old.HasFlag (Flags.On) != flag && base.isServer) {
			float industrialCrafterFrequency = Server.industrialCrafterFrequency;
			if (flag && industrialCrafterFrequency > 0f) {
				((FacepunchBehaviour)this).InvokeRandomized ((Action)CheckCraft, industrialCrafterFrequency, industrialCrafterFrequency, industrialCrafterFrequency * 0.5f);
			} else {
				((FacepunchBehaviour)this).CancelInvoke ((Action)CheckCraft);
			}
		}
	}

	public override void OnKilled (HitInfo info)
	{
		base.OnKilled (info);
		DropItems (info?.Initiator);
	}

	public void DropItems (BaseEntity initiator = null)
	{
		StorageContainer.DropItems (this, initiator);
	}

	public bool ShouldDropItemsIndividually ()
	{
		return false;
	}

	public void DropBonusItems (BaseEntity initiator, ItemContainer container)
	{
	}

	[RPC_Server]
	[RPC_Server.IsVisible (3f)]
	private void RPC_OpenLoot (RPCMessage rpc)
	{
		if (inventory != null) {
			BasePlayer player = rpc.player;
			if (Object.op_Implicit ((Object)(object)player) && player.CanInteract ()) {
				PlayerOpenLoot (player);
			}
		}
	}

	public virtual bool PlayerOpenLoot (BasePlayer player, string panelToOpen = "", bool doPositionChecks = true)
	{
		if (NeedsBuildingPrivilegeToUse && !player.CanBuild ()) {
			return false;
		}
		if (OnlyOneUser && IsOpen ()) {
			player.ChatMessage ("Already in use");
			return false;
		}
		if (player.inventory.loot.StartLootingEntity (this, doPositionChecks)) {
			SetFlag (Flags.Open, b: true);
			player.inventory.loot.AddContainer (inventory);
			player.inventory.loot.SendImmediate ();
			player.ClientRPCPlayer (null, player, "RPC_OpenLootPanel", LootPanelName);
			SendNetworkUpdate ();
			return true;
		}
		return false;
	}

	public virtual void PlayerStoppedLooting (BasePlayer player)
	{
		SetFlag (Flags.Open, b: false);
		SendNetworkUpdate ();
	}

	public override void ServerInit ()
	{
		base.ServerInit ();
		if (inventory == null) {
			CreateInventory (giveUID: true);
		}
	}

	public void CreateInventory (bool giveUID)
	{
		inventory = new ItemContainer ();
		inventory.entityOwner = this;
		inventory.canAcceptItem = CanAcceptItem;
		inventory.ServerInitialize (null, 12);
		if (giveUID) {
			inventory.GiveUID ();
		}
	}

	private bool CanAcceptItem (Item item, int index)
	{
		if (index >= 0 && index <= 3 && !item.IsBlueprint ()) {
			return false;
		}
		return true;
	}

	private void CheckCraft ()
	{
		((ObjectWorkQueue<IndustrialEntity>)global::IndustrialEntity.Queue).Add ((IndustrialEntity)this);
	}

	private Item GetTargetBlueprint (int index)
	{
		if (inventory == null) {
			return null;
		}
		if (index < 0 || index > 3) {
			return null;
		}
		Item slot = inventory.GetSlot (index);
		if (slot == null || !slot.IsBlueprint ()) {
			return null;
		}
		return slot;
	}

	protected override void RunJob ()
	{
		//IL_0238: Unknown result type (might be due to invalid IL or missing references)
		//IL_025a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0266: Unknown result type (might be due to invalid IL or missing references)
		base.RunJob ();
		if (Server.industrialCrafterFrequency <= 0f || HasFlag (Flags.Reserved1) || (Object)(object)currentlyCrafting != (Object)null) {
			return;
		}
		for (int i = 0; i <= 3; i++) {
			Item targetBlueprint = GetTargetBlueprint (i);
			if (targetBlueprint == null || (Object)(object)GetWorkbench () == (Object)null || GetWorkbench ().Workbenchlevel < targetBlueprint.blueprintTargetDef.Blueprint.workbenchLevelRequired) {
				continue;
			}
			ItemBlueprint blueprint = targetBlueprint.blueprintTargetDef.Blueprint;
			bool flag = true;
			foreach (ItemAmount ingredient in blueprint.ingredients) {
				int inputAmount = GetInputAmount (ingredient.itemDef);
				if ((float)inputAmount < ingredient.amount) {
					flag = false;
					break;
				}
			}
			if (!flag) {
				continue;
			}
			flag = false;
			for (int j = 8; j <= 11; j++) {
				Item slot = inventory.GetSlot (j);
				if (slot == null || ((Object)(object)slot.info == (Object)(object)targetBlueprint.blueprintTargetDef && slot.amount + blueprint.amountToCreate <= slot.MaxStackable ())) {
					flag = true;
					break;
				}
			}
			if (!flag) {
				SetFlag (Flags.Reserved2, b: true);
				continue;
			}
			SetFlag (Flags.Reserved2, b: false);
			foreach (ItemAmount ingredient2 in blueprint.ingredients) {
				ConsumeInputIngredient (ingredient2);
			}
			currentlyCrafting = targetBlueprint.blueprintTargetDef;
			currentlyCraftingAmount = blueprint.amountToCreate;
			float time = blueprint.time;
			((FacepunchBehaviour)this).Invoke ((Action)CompleteCraft, time);
			jobFinishes = TimeUntilWithDuration.op_Implicit (time);
			SetFlag (Flags.Reserved1, b: true);
			ClientRPC (null, "ClientUpdateCraftTimeRemaining", TimeUntilWithDuration.op_Implicit (jobFinishes), jobFinishes.Duration);
			break;
		}
	}

	private void CompleteCraft ()
	{
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		bool flag = false;
		for (int i = 8; i <= 11; i++) {
			Item slot = inventory.GetSlot (i);
			if (slot == null) {
				Item item = ItemManager.Create (currentlyCrafting, currentlyCraftingAmount, 0uL);
				item.position = i;
				inventory.Insert (item);
				flag = true;
				break;
			}
			if ((Object)(object)slot.info == (Object)(object)currentlyCrafting && slot.amount + currentlyCraftingAmount <= slot.MaxStackable ()) {
				slot.amount += currentlyCraftingAmount;
				slot.MarkDirty ();
				flag = true;
				break;
			}
		}
		if (!flag) {
			Item item2 = ItemManager.Create (currentlyCrafting, currentlyCraftingAmount, 0uL);
			item2.Drop (((Component)this).transform.position + ((Component)this).transform.forward * 0.5f, Vector3.zero);
		}
		currentlyCrafting = null;
		currentlyCraftingAmount = 0;
		SetFlag (Flags.Reserved1, b: false);
	}

	private int GetInputAmount (ItemDefinition def)
	{
		if ((Object)(object)def == (Object)null) {
			return 0;
		}
		int num = 0;
		for (int i = 4; i <= 7; i++) {
			Item slot = inventory.GetSlot (i);
			if (slot != null && (Object)(object)def == (Object)(object)slot.info) {
				num += slot.amount;
			}
		}
		return num;
	}

	private bool ConsumeInputIngredient (ItemAmount am)
	{
		if ((Object)(object)am.itemDef == (Object)null) {
			return false;
		}
		float num = am.amount;
		for (int i = 4; i <= 7; i++) {
			Item slot = inventory.GetSlot (i);
			if (slot != null && (Object)(object)am.itemDef == (Object)(object)slot.info) {
				float num2 = Mathf.Min (num, (float)slot.amount);
				slot.UseItem ((int)num2);
				num -= num2;
				if (num2 <= 0f) {
					return true;
				}
			}
		}
		return false;
	}

	public override void Save (SaveInfo info)
	{
		base.Save (info);
		if (info.forDisk) {
			if ((Object)(object)currentlyCrafting != (Object)null) {
				info.msg.industrialCrafter = Pool.Get<IndustrialCrafter> ();
				info.msg.industrialCrafter.currentlyCrafting = currentlyCrafting.itemid;
				info.msg.industrialCrafter.currentlyCraftingAmount = currentlyCraftingAmount;
			}
			if (inventory != null) {
				info.msg.storageBox = Pool.Get<StorageBox> ();
				info.msg.storageBox.contents = inventory.Save ();
			}
		}
	}

	public override void Load (LoadInfo info)
	{
		base.Load (info);
		if (info.msg.storageBox != null && inventory != null) {
			inventory.Load (info.msg.storageBox.contents);
			inventory.capacity = 12;
		}
		if (base.isServer && info.fromDisk && info.msg.industrialCrafter != null) {
			currentlyCrafting = ItemManager.FindItemDefinition (info.msg.industrialCrafter.currentlyCrafting);
			currentlyCraftingAmount = info.msg.industrialCrafter.currentlyCraftingAmount;
			CompleteCraft ();
		}
	}

	public Vector2i InputSlotRange (int slotIndex)
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		if (slotIndex == 3) {
			return new Vector2i (0, 3);
		}
		return new Vector2i (4, 7);
	}

	public Vector2i OutputSlotRange (int slotIndex)
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		if (slotIndex == 1) {
			return new Vector2i (0, 3);
		}
		return new Vector2i (8, 11);
	}

	public void OnStorageItemTransferBegin ()
	{
	}

	public void OnStorageItemTransferEnd ()
	{
	}

	public override void UpdateHasPower (int inputAmount, int inputSlot)
	{
	}

	public override void IOStateChanged (int inputAmount, int inputSlot)
	{
		base.IOStateChanged (inputAmount, inputSlot);
		if (inputSlot == 1) {
			SetFlag (Flags.Reserved8, inputAmount >= ConsumptionAmount () && inputAmount > 0);
			currentEnergy = inputAmount;
			ensureOutputsUpdated = true;
			MarkDirty ();
		}
		if (inputSlot == 1 && inputAmount <= 0 && IsOn ()) {
			SetSwitch (wantsOn: false);
		}
		if (inputSlot == 2) {
			if (IsOn () && inputAmount == 0) {
				SetSwitch (wantsOn: false);
			} else if (!IsOn () && inputAmount > 0 && HasFlag (Flags.Reserved8)) {
				SetSwitch (wantsOn: true);
			}
		}
		if (inputSlot == 4 && inputAmount > 0 && HasFlag (Flags.Reserved8)) {
			SetSwitch (wantsOn: true);
		}
		if (inputSlot == 5 && inputAmount > 0 && HasFlag (Flags.Reserved8)) {
			SetSwitch (wantsOn: false);
		}
	}

	public virtual void SetSwitch (bool wantsOn)
	{
		if (wantsOn != IsOn ()) {
			SetFlag (Flags.On, wantsOn, recursive: false, networkupdate: false);
			SetFlag (Flags.Busy, b: true, recursive: false, networkupdate: false);
			if (!wantsOn) {
				SetFlag (Flags.Reserved2, b: false, recursive: false, networkupdate: false);
			}
			((FacepunchBehaviour)this).Invoke ((Action)Unbusy, 0.5f);
			SendNetworkUpdateImmediate ();
			MarkDirty ();
		}
	}

	public void Unbusy ()
	{
		SetFlag (Flags.Busy, b: false);
	}

	[RPC_Server]
	[RPC_Server.IsVisible (3f)]
	[RPC_Server.CallsPerSecond (2uL)]
	private void SvSwitch (RPCMessage msg)
	{
		SetSwitch (!IsOn ());
	}

	public override bool CanPickup (BasePlayer player)
	{
		if (base.isServer) {
			return inventory != null && inventory.IsEmpty () && base.CanPickup (player);
		}
		return base.CanPickup (player);
	}

	public int GetIdealSlot (BasePlayer player, Item item)
	{
		return -1;
	}

	public ItemContainerId GetIdealContainer (BasePlayer player, Item item, bool altMove)
	{
		//IL_0003: Unknown result type (might be due to invalid IL or missing references)
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		return default(ItemContainerId);
	}

	public Workbench GetWorkbench ()
	{
		return GetParentEntity () as Workbench;
	}
}
