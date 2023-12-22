#define UNITY_ASSERTIONS
#define ENABLE_PROFILER
using System.Collections.Generic;
using Facepunch;
using Network;
using ProtoBuf;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Profiling;

public class PlayerLoot : EntityComponent<BasePlayer>
{
	public BaseEntity entitySource = null;

	public Item itemSource = null;

	public List<ItemContainer> containers = new List<ItemContainer> ();

	internal bool PositionChecks = true;

	private bool isInvokingSendUpdate;

	public override bool OnRpcMessage (BasePlayer player, uint rpc, Message msg)
	{
		using (TimeWarning.New ("PlayerLoot.OnRpcMessage")) {
		}
		return base.OnRpcMessage (player, rpc, msg);
	}

	public bool IsLooting ()
	{
		return containers.Count > 0;
	}

	public void Clear ()
	{
		if (!IsLooting ()) {
			return;
		}
		Profiler.BeginSample ("PlayerLoot.Clear");
		MarkDirty ();
		if ((bool)entitySource) {
			entitySource.SendMessage ("PlayerStoppedLooting", base.baseEntity, SendMessageOptions.DontRequireReceiver);
		}
		foreach (ItemContainer container in containers) {
			if (container != null) {
				container.onDirty -= MarkDirty;
			}
		}
		containers.Clear ();
		entitySource = null;
		itemSource = null;
		Profiler.EndSample ();
	}

	public ItemContainer FindContainer (ItemContainerId id)
	{
		Check ();
		if (!IsLooting ()) {
			return null;
		}
		foreach (ItemContainer container in containers) {
			ItemContainer itemContainer = container.FindContainer (id);
			if (itemContainer != null) {
				return itemContainer;
			}
		}
		return null;
	}

	public Item FindItem (ItemId id)
	{
		Check ();
		if (!IsLooting ()) {
			return null;
		}
		foreach (ItemContainer container in containers) {
			Item item = container.FindItemByUID (id);
			if (item != null && item.IsValid ()) {
				return item;
			}
		}
		return null;
	}

	public void Check ()
	{
		if (!IsLooting () || !base.baseEntity.isServer) {
			return;
		}
		if (entitySource == null) {
			base.baseEntity.ChatMessage ("Stopping Looting because lootable doesn't exist!");
			Clear ();
		} else if (!entitySource.CanBeLooted (base.baseEntity)) {
			Clear ();
		} else {
			if (!PositionChecks) {
				return;
			}
			float num = entitySource.Distance (base.baseEntity.eyes.position);
			if (num > 3f) {
				LootDistanceOverride component = entitySource.GetComponent<LootDistanceOverride> ();
				if (component == null || num > component.amount) {
					Clear ();
				}
			}
		}
	}

	private void MarkDirty ()
	{
		Profiler.BeginSample ("MarkDirty");
		if (!isInvokingSendUpdate) {
			isInvokingSendUpdate = true;
			Invoke (SendUpdate, 0.1f);
		}
		Profiler.EndSample ();
	}

	public void SendImmediate ()
	{
		Profiler.BeginSample ("SendImmediate");
		if (isInvokingSendUpdate) {
			isInvokingSendUpdate = false;
			CancelInvoke (SendUpdate);
		}
		SendUpdate ();
		Profiler.EndSample ();
	}

	private void SendUpdate ()
	{
		isInvokingSendUpdate = false;
		if (!base.baseEntity.IsValid ()) {
			return;
		}
		Profiler.BeginSample ("PlayerLoot.SendUpdate");
		using (PlayerUpdateLoot playerUpdateLoot = Pool.Get<PlayerUpdateLoot> ()) {
			if ((bool)entitySource && entitySource.net != null) {
				playerUpdateLoot.entityID = entitySource.net.ID;
			}
			if (itemSource != null) {
				playerUpdateLoot.itemID = itemSource.uid;
			}
			if (containers.Count > 0) {
				playerUpdateLoot.containers = Pool.Get<List<ProtoBuf.ItemContainer>> ();
				foreach (ItemContainer container in containers) {
					playerUpdateLoot.containers.Add (container.Save ());
				}
			}
			base.baseEntity.ClientRPCPlayer (null, base.baseEntity, "UpdateLoot", playerUpdateLoot);
		}
		Profiler.EndSample ();
	}

	public bool StartLootingEntity (BaseEntity targetEntity, bool doPositionChecks = true)
	{
		Clear ();
		if (!targetEntity) {
			return false;
		}
		if (!targetEntity.OnStartBeingLooted (base.baseEntity)) {
			return false;
		}
		Assert.IsTrue (targetEntity.isServer, "Assure is server");
		Profiler.BeginSample ("PlayerLoot.StartLootingEntity");
		PositionChecks = doPositionChecks;
		entitySource = targetEntity;
		itemSource = null;
		MarkDirty ();
		Profiler.EndSample ();
		if (targetEntity is ILootableEntity lootableEntity) {
			lootableEntity.LastLootedBy = base.baseEntity.userID;
		}
		return true;
	}

	public void AddContainer (ItemContainer container)
	{
		if (container != null) {
			Profiler.BeginSample ("PlayerLoot.AddContainer");
			containers.Add (container);
			container.onDirty += MarkDirty;
			Profiler.EndSample ();
		}
	}

	public void RemoveContainer (ItemContainer container)
	{
		if (container != null) {
			container.onDirty -= MarkDirty;
			containers.Remove (container);
		}
	}

	public bool RemoveContainerAt (int index)
	{
		if (index < 0 || index >= containers.Count) {
			return false;
		}
		if (containers [index] != null) {
			containers [index].onDirty -= MarkDirty;
		}
		containers.RemoveAt (index);
		return true;
	}

	public void StartLootingItem (Item item)
	{
		Clear ();
		if (item != null && item.contents != null) {
			Profiler.BeginSample ("PlayerLoot.StartLootingItem");
			PositionChecks = true;
			containers.Add (item.contents);
			item.contents.onDirty += MarkDirty;
			itemSource = item;
			entitySource = item.GetWorldEntity ();
			MarkDirty ();
			Profiler.EndSample ();
		}
	}
}
