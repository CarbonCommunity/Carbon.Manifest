#define ENABLE_PROFILER
#define UNITY_ASSERTIONS
using System;
using System.Collections.Generic;
using System.Linq;
using ConVar;
using Facepunch;
using Facepunch.Rust;
using Network;
using ProtoBuf;
using Rust;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Profiling;

public class PlayerInventory : EntityComponent<BasePlayer>
{
	public enum Type
	{
		Main,
		Belt,
		Wear
	}

	public interface ICanMoveFrom
	{
		bool CanMoveFrom (BasePlayer player, Item item);
	}

	public ItemContainer containerMain;

	public ItemContainer containerBelt;

	public ItemContainer containerWear;

	public ItemCrafter crafting;

	public PlayerLoot loot;

	[ServerVar]
	public static bool forceBirthday = false;

	private static float nextCheckTime = 0f;

	private static bool wasBirthday = false;

	public override bool OnRpcMessage (BasePlayer player, uint rpc, Message msg)
	{
		using (TimeWarning.New ("PlayerInventory.OnRpcMessage")) {
			if (rpc == 3482449460u && player != null) {
				Assert.IsTrue (player.isServer, "SV_RPC Message is using a clientside player!");
				if (ConVar.Global.developer > 2) {
					Debug.Log (string.Concat ("SV_RPCMessage: ", player, " - ItemCmd "));
				}
				using (TimeWarning.New ("ItemCmd")) {
					using (TimeWarning.New ("Conditions")) {
						if (!BaseEntity.RPC_Server.FromOwner.Test (3482449460u, "ItemCmd", GetBaseEntity (), player)) {
							return true;
						}
					}
					try {
						using (TimeWarning.New ("Call")) {
							BaseEntity.RPCMessage rPCMessage = default(BaseEntity.RPCMessage);
							rPCMessage.connection = msg.connection;
							rPCMessage.player = player;
							rPCMessage.read = msg.read;
							BaseEntity.RPCMessage msg2 = rPCMessage;
							ItemCmd (msg2);
						}
					} catch (Exception exception) {
						Debug.LogException (exception);
						player.Kick ("RPC Error in ItemCmd");
					}
				}
				return true;
			}
			if (rpc == 3041092525u && player != null) {
				Assert.IsTrue (player.isServer, "SV_RPC Message is using a clientside player!");
				if (ConVar.Global.developer > 2) {
					Debug.Log (string.Concat ("SV_RPCMessage: ", player, " - MoveItem "));
				}
				using (TimeWarning.New ("MoveItem")) {
					using (TimeWarning.New ("Conditions")) {
						if (!BaseEntity.RPC_Server.FromOwner.Test (3041092525u, "MoveItem", GetBaseEntity (), player)) {
							return true;
						}
					}
					try {
						using (TimeWarning.New ("Call")) {
							BaseEntity.RPCMessage rPCMessage = default(BaseEntity.RPCMessage);
							rPCMessage.connection = msg.connection;
							rPCMessage.player = player;
							rPCMessage.read = msg.read;
							BaseEntity.RPCMessage msg3 = rPCMessage;
							MoveItem (msg3);
						}
					} catch (Exception exception2) {
						Debug.LogException (exception2);
						player.Kick ("RPC Error in MoveItem");
					}
				}
				return true;
			}
		}
		return base.OnRpcMessage (player, rpc, msg);
	}

	protected void Initialize ()
	{
		containerMain = new ItemContainer ();
		containerMain.SetFlag (ItemContainer.Flag.IsPlayer, b: true);
		containerBelt = new ItemContainer ();
		containerBelt.SetFlag (ItemContainer.Flag.IsPlayer, b: true);
		containerBelt.SetFlag (ItemContainer.Flag.Belt, b: true);
		containerWear = new ItemContainer ();
		containerWear.SetFlag (ItemContainer.Flag.IsPlayer, b: true);
		containerWear.SetFlag (ItemContainer.Flag.Clothing, b: true);
		crafting = GetComponent<ItemCrafter> ();
		if (crafting != null) {
			crafting.AddContainer (containerMain);
			crafting.AddContainer (containerBelt);
		}
		loot = GetComponent<PlayerLoot> ();
		if (!loot) {
			loot = base.gameObject.AddComponent<PlayerLoot> ();
		}
	}

	public void DoDestroy ()
	{
		if (containerMain != null) {
			containerMain.Kill ();
			containerMain = null;
		}
		if (containerBelt != null) {
			containerBelt.Kill ();
			containerBelt = null;
		}
		if (containerWear != null) {
			containerWear.Kill ();
			containerWear = null;
		}
	}

	public void ServerInit (BasePlayer owner)
	{
		Initialize ();
		containerMain.ServerInitialize (null, 24);
		if (!containerMain.uid.IsValid) {
			containerMain.GiveUID ();
		}
		containerBelt.ServerInitialize (null, 6);
		if (!containerBelt.uid.IsValid) {
			containerBelt.GiveUID ();
		}
		containerWear.ServerInitialize (null, 7);
		if (!containerWear.uid.IsValid) {
			containerWear.GiveUID ();
		}
		containerMain.playerOwner = owner;
		containerBelt.playerOwner = owner;
		containerWear.playerOwner = owner;
		containerWear.onItemAddedRemoved = OnClothingChanged;
		containerWear.canAcceptItem = CanWearItem;
		containerBelt.canAcceptItem = CanEquipItem;
		containerMain.onPreItemRemove = OnItemRemoved;
		containerWear.onPreItemRemove = OnItemRemoved;
		containerBelt.onPreItemRemove = OnItemRemoved;
		containerMain.onDirty += OnContentsDirty;
		containerBelt.onDirty += OnContentsDirty;
		containerWear.onDirty += OnContentsDirty;
		containerBelt.onItemAddedRemoved = OnItemAddedOrRemoved;
		containerMain.onItemAddedRemoved = OnItemAddedOrRemoved;
	}

	public void OnItemAddedOrRemoved (Item item, bool bAdded)
	{
		if (item.info.isHoldable) {
			Invoke (UpdatedVisibleHolsteredItems, 0.1f);
		}
		if (bAdded) {
			Profiler.BeginSample ("PlayerInventory.OnItemAddedOrRemoved");
			BasePlayer basePlayer = base.baseEntity;
			if (!basePlayer.HasPlayerFlag (BasePlayer.PlayerFlags.DisplaySash) && basePlayer.IsHostileItem (item)) {
				base.baseEntity.SetPlayerFlag (BasePlayer.PlayerFlags.DisplaySash, b: true);
			}
			if (bAdded) {
				basePlayer.ProcessMissionEvent (BaseMission.MissionEventType.ACQUIRE_ITEM, item.info.shortname, item.amount);
			}
			Profiler.EndSample ();
		}
	}

	public void UpdatedVisibleHolsteredItems ()
	{
		Profiler.BeginSample ("PlayerInventory.UpdateVisibleHolsteredItems");
		List<HeldEntity> obj = Facepunch.Pool.GetList<HeldEntity> ();
		List<Item> items = Facepunch.Pool.GetList<Item> ();
		AllItemsNoAlloc (ref items);
		foreach (Item item in items) {
			if (!item.info.isHoldable) {
				continue;
			}
			BaseEntity heldEntity = item.GetHeldEntity ();
			if (!(heldEntity == null)) {
				HeldEntity component = item.GetHeldEntity ().GetComponent<HeldEntity> ();
				if (!(component == null)) {
					obj.Add (component);
				}
			}
		}
		Facepunch.Pool.FreeList (ref items);
		IOrderedEnumerable<HeldEntity> orderedEnumerable = obj.OrderByDescending ((HeldEntity x) => x.hostileScore);
		bool flag = true;
		bool flag2 = true;
		bool flag3 = true;
		foreach (HeldEntity item2 in orderedEnumerable) {
			if (!(item2 == null) && item2.holsterInfo.displayWhenHolstered) {
				if (flag3 && !item2.IsDeployed () && item2.holsterInfo.slot == HeldEntity.HolsterInfo.HolsterSlot.BACK) {
					item2.SetVisibleWhileHolstered (visible: true);
					flag3 = false;
				} else if (flag2 && !item2.IsDeployed () && item2.holsterInfo.slot == HeldEntity.HolsterInfo.HolsterSlot.RIGHT_THIGH) {
					item2.SetVisibleWhileHolstered (visible: true);
					flag2 = false;
				} else if (flag && !item2.IsDeployed () && item2.holsterInfo.slot == HeldEntity.HolsterInfo.HolsterSlot.LEFT_THIGH) {
					item2.SetVisibleWhileHolstered (visible: true);
					flag = false;
				} else {
					item2.SetVisibleWhileHolstered (visible: false);
				}
			}
		}
		Profiler.EndSample ();
		Facepunch.Pool.FreeList (ref obj);
	}

	private void OnContentsDirty ()
	{
		if (base.baseEntity != null) {
			base.baseEntity.InvalidateNetworkCache ();
		}
	}

	private bool CanMoveItemsFrom (BaseEntity entity, Item item)
	{
		if (entity is ICanMoveFrom canMoveFrom && !canMoveFrom.CanMoveFrom (base.baseEntity, item)) {
			return false;
		}
		if ((bool)BaseGameMode.GetActiveGameMode (serverside: true)) {
			return BaseGameMode.GetActiveGameMode (serverside: true).CanMoveItemsFrom (this, entity, item);
		}
		return true;
	}

	[BaseEntity.RPC_Server]
	[BaseEntity.RPC_Server.FromOwner]
	private void ItemCmd (BaseEntity.RPCMessage msg)
	{
		if (msg.player != null && msg.player.IsWounded ()) {
			return;
		}
		ItemId id = msg.read.ItemID ();
		string text = msg.read.String ();
		Item item = FindItemUID (id);
		if (item == null || item.IsLocked () || (item.parent != null && item.parent.IsLocked ()) || !CanMoveItemsFrom (item.GetEntityOwner (), item)) {
			return;
		}
		if (text == "drop") {
			int num = item.amount;
			if (msg.read.Unread >= 4) {
				num = msg.read.Int32 ();
			}
			base.baseEntity.stats.Add ("item_drop", 1, (Stats)5);
			if (num < item.amount) {
				Item item2 = item.SplitItem (num);
				if (item2 != null) {
					DroppedItem droppedItem = item2.Drop (base.baseEntity.GetDropPosition (), base.baseEntity.GetDropVelocity ()) as DroppedItem;
					if (droppedItem != null) {
						droppedItem.DropReason = DroppedItem.DropReasonEnum.Player;
						droppedItem.DroppedBy = base.baseEntity.userID;
						Analytics.Azure.OnItemDropped (base.baseEntity, droppedItem, DroppedItem.DropReasonEnum.Player);
					}
				}
			} else {
				DroppedItem droppedItem2 = item.Drop (base.baseEntity.GetDropPosition (), base.baseEntity.GetDropVelocity ()) as DroppedItem;
				if (droppedItem2 != null) {
					droppedItem2.DropReason = DroppedItem.DropReasonEnum.Player;
					droppedItem2.DroppedBy = base.baseEntity.userID;
					Analytics.Azure.OnItemDropped (base.baseEntity, droppedItem2, DroppedItem.DropReasonEnum.Player);
				}
			}
			base.baseEntity.SignalBroadcast (BaseEntity.Signal.Gesture, "drop_item");
		} else {
			item.ServerCommand (text, base.baseEntity);
			ItemManager.DoRemoves ();
			ServerUpdate (0f);
		}
	}

	[BaseEntity.RPC_Server]
	[BaseEntity.RPC_Server.FromOwner]
	public void MoveItem (BaseEntity.RPCMessage msg)
	{
		ItemId itemId = msg.read.ItemID ();
		ItemContainerId itemContainerId = msg.read.ItemContainerID ();
		int iTargetPos = msg.read.Int8 ();
		int num = (int)msg.read.UInt32 ();
		bool flag = msg.read.Bit ();
		Item item = FindItemUID (itemId);
		if (item == null) {
			msg.player.ChatMessage (string.Concat ("Invalid item (", itemId, ")"));
			return;
		}
		BaseEntity entityOwner = item.GetEntityOwner ();
		if (!CanMoveItemsFrom (entityOwner, item)) {
			msg.player.ChatMessage ("Cannot move item!");
			return;
		}
		if (num <= 0) {
			num = item.amount;
		}
		num = Mathf.Clamp (num, 1, item.MaxStackable ());
		Item activeItem = msg.player.GetActiveItem ();
		if (activeItem == item) {
			msg.player.UpdateActiveItem (default(ItemId));
		}
		if (!itemContainerId.IsValid) {
			BaseEntity baseEntity = entityOwner;
			if (loot.containers.Count > 0) {
				if (entityOwner == base.baseEntity) {
					if (!flag) {
						baseEntity = loot.entitySource;
					}
				} else {
					baseEntity = base.baseEntity;
				}
			}
			if (baseEntity is IIdealSlotEntity idealSlotEntity) {
				itemContainerId = idealSlotEntity.GetIdealContainer (base.baseEntity, item, flag);
			}
			ItemContainer parent = item.parent;
			if (parent != null && parent.IsLocked ()) {
				msg.player.ChatMessage ("Container is locked!");
				return;
			}
			if (!itemContainerId.IsValid) {
				if (baseEntity == loot.entitySource) {
					foreach (ItemContainer container in loot.containers) {
						if (!container.PlayerItemInputBlocked () && !container.IsLocked () && item.MoveToContainer (container, -1, allowStack: true, ignoreStackLimit: false, base.baseEntity)) {
							break;
						}
					}
					return;
				}
				if (!GiveItem (item, null, flag)) {
					msg.player.ChatMessage ("GiveItem failed!");
				}
				return;
			}
		}
		ItemContainer itemContainer = FindContainer (itemContainerId);
		if (itemContainer == null) {
			msg.player.ChatMessage (string.Concat ("Invalid container (", itemContainerId, ")"));
			return;
		}
		if (itemContainer.IsLocked ()) {
			msg.player.ChatMessage ("Container is locked!");
			return;
		}
		if (itemContainer.PlayerItemInputBlocked ()) {
			msg.player.ChatMessage ("Container does not accept player items!");
			return;
		}
		using (TimeWarning.New ("Split")) {
			if (item.amount > num) {
				int split_Amount = num;
				if (itemContainer.maxStackSize > 0) {
					split_Amount = Mathf.Min (num, itemContainer.maxStackSize);
				}
				Item item2 = item.SplitItem (split_Amount);
				if (!item2.MoveToContainer (itemContainer, iTargetPos, allowStack: true, ignoreStackLimit: false, base.baseEntity)) {
					item.amount += item2.amount;
					item2.Remove ();
				}
				ItemManager.DoRemoves ();
				ServerUpdate (0f);
				return;
			}
		}
		if (item.MoveToContainer (itemContainer, iTargetPos, allowStack: true, ignoreStackLimit: false, base.baseEntity)) {
			ItemManager.DoRemoves ();
			ServerUpdate (0f);
		}
	}

	private void OnClothingChanged (Item item, bool bAdded)
	{
		base.baseEntity.SV_ClothingChanged ();
		ItemManager.DoRemoves ();
		ServerUpdate (0f);
	}

	private void OnItemRemoved (Item item)
	{
		base.baseEntity.InvalidateNetworkCache ();
	}

	private bool CanEquipItem (Item item, int targetSlot)
	{
		ItemModContainerRestriction component = item.info.GetComponent<ItemModContainerRestriction> ();
		if (component == null) {
			return true;
		}
		Item[] array = containerBelt.itemList.ToArray ();
		foreach (Item item2 in array) {
			if (item2 != item) {
				ItemModContainerRestriction component2 = item2.info.GetComponent<ItemModContainerRestriction> ();
				if (!(component2 == null) && !component.CanExistWith (component2) && !item2.MoveToContainer (containerMain)) {
					item2.Drop (base.baseEntity.GetDropPosition (), base.baseEntity.GetDropVelocity ());
				}
			}
		}
		return true;
	}

	private bool CanWearItem (Item item, int targetSlot)
	{
		return CanWearItem (item, canAdjustClothing: true);
	}

	private bool CanWearItem (Item item, bool canAdjustClothing)
	{
		ItemModWearable component = item.info.GetComponent<ItemModWearable> ();
		if (component == null) {
			return false;
		}
		if (component.npcOnly && !Inventory.disableAttireLimitations) {
			BasePlayer basePlayer = base.baseEntity;
			if (basePlayer != null && !basePlayer.IsNpc) {
				return false;
			}
		}
		Item[] array = containerWear.itemList.ToArray ();
		Item[] array2 = array;
		foreach (Item item2 in array2) {
			if (item2 == item) {
				continue;
			}
			ItemModWearable component2 = item2.info.GetComponent<ItemModWearable> ();
			if (!(component2 == null) && !Inventory.disableAttireLimitations && !component.CanExistWith (component2)) {
				if (!canAdjustClothing) {
					return false;
				}
				bool flag = false;
				if (item.parent == containerBelt) {
					flag = item2.MoveToContainer (containerBelt);
				}
				if (!flag && !item2.MoveToContainer (containerMain)) {
					item2.Drop (base.baseEntity.GetDropPosition (), base.baseEntity.GetDropVelocity ());
				}
			}
		}
		return true;
	}

	public void ServerUpdate (float delta)
	{
		Profiler.BeginSample ("loot.Check");
		loot.Check ();
		Profiler.EndSample ();
		if (delta > 0f) {
			Profiler.BeginSample ("crafting.ServerUpdate");
			crafting.ServerUpdate (delta);
			Profiler.EndSample ();
		}
		float currentTemperature = base.baseEntity.currentTemperature;
		UpdateContainer (delta, Type.Main, containerMain, bSendInventoryToEveryone: false, currentTemperature);
		UpdateContainer (delta, Type.Belt, containerBelt, bSendInventoryToEveryone: true, currentTemperature);
		UpdateContainer (delta, Type.Wear, containerWear, bSendInventoryToEveryone: true, currentTemperature);
	}

	public void UpdateContainer (float delta, Type type, ItemContainer container, bool bSendInventoryToEveryone, float temperature)
	{
		if (container != null) {
			container.temperature = temperature;
			if (delta > 0f) {
				container.OnCycle (delta);
			}
			if (container.dirty) {
				SendUpdatedInventory (type, container, bSendInventoryToEveryone);
				base.baseEntity.InvalidateNetworkCache ();
			}
		}
	}

	public void SendSnapshot ()
	{
		using (TimeWarning.New ("PlayerInventory.SendSnapshot")) {
			SendUpdatedInventory (Type.Main, containerMain);
			SendUpdatedInventory (Type.Belt, containerBelt, bSendInventoryToEveryone: true);
			SendUpdatedInventory (Type.Wear, containerWear, bSendInventoryToEveryone: true);
		}
	}

	public void SendUpdatedInventory (Type type, ItemContainer container, bool bSendInventoryToEveryone = false)
	{
		using UpdateItemContainer updateItemContainer = Facepunch.Pool.Get<UpdateItemContainer> ();
		updateItemContainer.type = (int)type;
		if (container != null) {
			container.dirty = false;
			updateItemContainer.container = Facepunch.Pool.Get<List<ProtoBuf.ItemContainer>> ();
			updateItemContainer.container.Add (container.Save ());
		}
		if (bSendInventoryToEveryone) {
			base.baseEntity.ClientRPC (null, "UpdatedItemContainer", updateItemContainer);
		} else {
			base.baseEntity.ClientRPCPlayer (null, base.baseEntity, "UpdatedItemContainer", updateItemContainer);
		}
	}

	public Item FindItemUID (ItemId id)
	{
		if (!id.IsValid) {
			return null;
		}
		if (containerMain != null) {
			Item item = containerMain.FindItemByUID (id);
			if (item != null && item.IsValid ()) {
				return item;
			}
		}
		if (containerBelt != null) {
			Item item2 = containerBelt.FindItemByUID (id);
			if (item2 != null && item2.IsValid ()) {
				return item2;
			}
		}
		if (containerWear != null) {
			Item item3 = containerWear.FindItemByUID (id);
			if (item3 != null && item3.IsValid ()) {
				return item3;
			}
		}
		return loot.FindItem (id);
	}

	public Item FindItemID (string itemName)
	{
		ItemDefinition itemDefinition = ItemManager.FindItemDefinition (itemName);
		if (itemDefinition == null) {
			return null;
		}
		return FindItemID (itemDefinition.itemid);
	}

	public Item FindItemID (int id)
	{
		if (containerMain != null) {
			Item item = containerMain.FindItemByItemID (id);
			if (item != null && item.IsValid ()) {
				return item;
			}
		}
		if (containerBelt != null) {
			Item item2 = containerBelt.FindItemByItemID (id);
			if (item2 != null && item2.IsValid ()) {
				return item2;
			}
		}
		if (containerWear != null) {
			Item item3 = containerWear.FindItemByItemID (id);
			if (item3 != null && item3.IsValid ()) {
				return item3;
			}
		}
		return null;
	}

	public Item FindBySubEntityID (NetworkableId subEntityID)
	{
		if (containerMain != null) {
			Item item = containerMain.FindBySubEntityID (subEntityID);
			if (item != null && item.IsValid ()) {
				return item;
			}
		}
		if (containerBelt != null) {
			Item item2 = containerBelt.FindBySubEntityID (subEntityID);
			if (item2 != null && item2.IsValid ()) {
				return item2;
			}
		}
		if (containerWear != null) {
			Item item3 = containerWear.FindBySubEntityID (subEntityID);
			if (item3 != null && item3.IsValid ()) {
				return item3;
			}
		}
		return null;
	}

	public List<Item> FindItemIDs (int id)
	{
		List<Item> list = new List<Item> ();
		if (containerMain != null) {
			list.AddRange (containerMain.FindItemsByItemID (id));
		}
		if (containerBelt != null) {
			list.AddRange (containerBelt.FindItemsByItemID (id));
		}
		if (containerWear != null) {
			list.AddRange (containerWear.FindItemsByItemID (id));
		}
		return list;
	}

	public ItemContainer FindContainer (ItemContainerId id)
	{
		using (TimeWarning.New ("FindContainer")) {
			ItemContainer itemContainer = containerMain.FindContainer (id);
			if (itemContainer != null) {
				return itemContainer;
			}
			itemContainer = containerBelt.FindContainer (id);
			if (itemContainer != null) {
				return itemContainer;
			}
			itemContainer = containerWear.FindContainer (id);
			if (itemContainer != null) {
				return itemContainer;
			}
			return loot.FindContainer (id);
		}
	}

	public ItemContainer GetContainer (Type id)
	{
		if (id == Type.Main) {
			return containerMain;
		}
		if (Type.Belt == id) {
			return containerBelt;
		}
		if (Type.Wear == id) {
			return containerWear;
		}
		return null;
	}

	public bool GiveItem (Item item, ItemContainer container = null, bool tryWearClothing = false)
	{
		if (item == null) {
			return false;
		}
		if (container == null) {
			GetIdealPickupContainer (item, ref container, tryWearClothing);
		}
		if (container != null && item.MoveToContainer (container)) {
			return true;
		}
		if (item.MoveToContainer (containerMain)) {
			return true;
		}
		if (item.MoveToContainer (containerBelt)) {
			return true;
		}
		return false;
	}

	protected void GetIdealPickupContainer (Item item, ref ItemContainer container, bool tryWearClothing)
	{
		if (item.MaxStackable () > 1) {
			if (containerBelt != null && containerBelt.FindItemByItemID (item.info.itemid) != null) {
				container = containerBelt;
				return;
			}
			if (containerMain != null && containerMain.FindItemByItemID (item.info.itemid) != null) {
				container = containerMain;
				return;
			}
		}
		if (tryWearClothing && item.info.isWearable && CanWearItem (item, canAdjustClothing: false)) {
			container = containerWear;
		} else if (item.info.isUsable && !item.info.HasFlag (ItemDefinition.Flag.NotStraightToBelt)) {
			container = containerBelt;
		}
	}

	public void Strip ()
	{
		containerMain.Clear ();
		containerBelt.Clear ();
		containerWear.Clear ();
		ItemManager.DoRemoves ();
	}

	public static bool IsBirthday ()
	{
		if (forceBirthday) {
			return true;
		}
		if (UnityEngine.Time.time < nextCheckTime) {
			return wasBirthday;
		}
		nextCheckTime = UnityEngine.Time.time + 60f;
		DateTime now = DateTime.Now;
		wasBirthday = now.Day == 11 && now.Month == 12;
		return wasBirthday;
	}

	public static bool IsChristmas ()
	{
		return XMas.enabled;
	}

	public void GiveDefaultItems ()
	{
		Strip ();
		BaseGameMode activeGameMode = BaseGameMode.GetActiveGameMode (serverside: true);
		if (activeGameMode != null && activeGameMode.HasLoadouts ()) {
			BaseGameMode.GetActiveGameMode (serverside: true).LoadoutPlayer (base.baseEntity);
			return;
		}
		GiveDefaultItemWithSkin ("client.rockskin", "rock");
		GiveDefaultItemWithSkin ("client.torchskin", "torch");
		if (IsBirthday ()) {
			GiveItem (ItemManager.CreateByName ("cakefiveyear", 1, 0uL), containerBelt);
			GiveItem (ItemManager.CreateByName ("partyhat", 1, 0uL), containerWear);
		}
		if (IsChristmas ()) {
			GiveItem (ItemManager.CreateByName ("snowball", 1, 0uL), containerBelt);
			GiveItem (ItemManager.CreateByName ("snowball", 1, 0uL), containerBelt);
			GiveItem (ItemManager.CreateByName ("snowball", 1, 0uL), containerBelt);
		}
		void GiveDefaultItemWithSkin (string convarSkinName, string itemShortName)
		{
			ulong num = 0uL;
			int infoInt = base.baseEntity.GetInfoInt (convarSkinName, 0);
			bool flag = false;
			bool flag2 = false;
			flag2 = base.baseEntity?.UnlockAllSkins ?? false;
			if (infoInt > 0 && (base.baseEntity.blueprints.CheckSkinOwnership (infoInt, base.baseEntity.userID) || flag2)) {
				ItemDefinition itemDefinition = ItemManager.FindItemDefinition (itemShortName);
				if (itemDefinition != null && ItemDefinition.FindSkin (itemDefinition.itemid, infoInt) != 0) {
					IPlayerItemDefinition itemDefinition2 = PlatformService.Instance.GetItemDefinition (infoInt);
					if (itemDefinition2 != null) {
						num = itemDefinition2.WorkshopDownload;
					}
					if (num == 0L && itemDefinition.skins != null) {
						ItemSkinDirectory.Skin[] skins = itemDefinition.skins;
						for (int i = 0; i < skins.Length; i++) {
							ItemSkinDirectory.Skin skin = skins [i];
							if (skin.id == infoInt && skin.invItem != null && skin.invItem is ItemSkin itemSkin && itemSkin.Redirect != null) {
								GiveItem (ItemManager.CreateByName (itemSkin.Redirect.shortname, 1, 0uL), containerBelt);
								flag = true;
								break;
							}
						}
					}
				}
			}
			if (!flag) {
				GiveItem (ItemManager.CreateByName (itemShortName, 1, num), containerBelt);
			}
		}
	}

	public ProtoBuf.PlayerInventory Save (bool bForDisk)
	{
		Profiler.BeginSample ("PlayerInventory.Save");
		ProtoBuf.PlayerInventory playerInventory = Facepunch.Pool.Get<ProtoBuf.PlayerInventory> ();
		if (bForDisk) {
			playerInventory.invMain = containerMain.Save ();
		}
		playerInventory.invBelt = containerBelt.Save ();
		playerInventory.invWear = containerWear.Save ();
		Profiler.EndSample ();
		return playerInventory;
	}

	public void Load (ProtoBuf.PlayerInventory msg)
	{
		if (msg.invMain != null) {
			containerMain.Load (msg.invMain);
		}
		if (msg.invBelt != null) {
			containerBelt.Load (msg.invBelt);
		}
		if (msg.invWear != null) {
			containerWear.Load (msg.invWear);
		}
	}

	public int Take (List<Item> collect, int itemid, int amount)
	{
		int num = 0;
		if (containerMain != null) {
			int num2 = containerMain.Take (collect, itemid, amount);
			num += num2;
			amount -= num2;
		}
		if (amount <= 0) {
			return num;
		}
		if (containerBelt != null) {
			int num3 = containerBelt.Take (collect, itemid, amount);
			num += num3;
			amount -= num3;
		}
		if (amount <= 0) {
			return num;
		}
		if (containerWear != null) {
			int num4 = containerWear.Take (collect, itemid, amount);
			num += num4;
			amount -= num4;
		}
		return num;
	}

	public int GetAmount (int itemid)
	{
		if (itemid == 0) {
			return 0;
		}
		int num = 0;
		if (containerMain != null) {
			num += containerMain.GetAmount (itemid, onlyUsableAmounts: true);
		}
		if (containerBelt != null) {
			num += containerBelt.GetAmount (itemid, onlyUsableAmounts: true);
		}
		if (containerWear != null) {
			num += containerWear.GetAmount (itemid, onlyUsableAmounts: true);
		}
		return num;
	}

	public Item[] AllItems ()
	{
		List<Item> list = new List<Item> ();
		if (containerMain != null) {
			list.AddRange (containerMain.itemList);
		}
		if (containerBelt != null) {
			list.AddRange (containerBelt.itemList);
		}
		if (containerWear != null) {
			list.AddRange (containerWear.itemList);
		}
		return list.ToArray ();
	}

	public int AllItemsNoAlloc (ref List<Item> items)
	{
		items.Clear ();
		if (containerMain != null) {
			items.AddRange (containerMain.itemList);
		}
		if (containerBelt != null) {
			items.AddRange (containerBelt.itemList);
		}
		if (containerWear != null) {
			items.AddRange (containerWear.itemList);
		}
		return items.Count;
	}

	public void FindAmmo (List<Item> list, AmmoTypes ammoType)
	{
		if (containerMain != null) {
			containerMain.FindAmmo (list, ammoType);
		}
		if (containerBelt != null) {
			containerBelt.FindAmmo (list, ammoType);
		}
	}

	public bool HasAmmo (AmmoTypes ammoType)
	{
		return containerMain.HasAmmo (ammoType) || containerBelt.HasAmmo (ammoType);
	}
}
