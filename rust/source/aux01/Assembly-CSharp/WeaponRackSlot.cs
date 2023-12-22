using ProtoBuf;
using Rust;

public class WeaponRackSlot
{
	public bool Used;

	public ItemDefinition ItemDef;

	public int ClientItemID;

	public ulong ClientItemSkinID;

	public ItemDefinition AmmoItemDef;

	public int AmmoItemID;

	public int AmmoCount;

	public int AmmoMax;

	public float Condition;

	public int InventoryIndex;

	public int GridSlotIndex;

	public int Rotation;

	[InspectorFlags]
	public AmmoTypes AmmoTypes;

	public WeaponRackItem SaveToProto (Item item, WeaponRackItem proto)
	{
		proto.itemID = item?.info.itemid ?? 0;
		proto.skinid = item?.skin ?? 0;
		proto.inventorySlot = InventoryIndex;
		proto.gridSlotIndex = GridSlotIndex;
		proto.rotation = Rotation;
		proto.ammoCount = AmmoCount;
		proto.ammoMax = AmmoMax;
		proto.ammoID = AmmoItemID;
		proto.condition = Condition;
		proto.ammoTypes = (int)AmmoTypes;
		return proto;
	}

	public void InitFromProto (WeaponRackItem item)
	{
		ClientItemID = item.itemID;
		ClientItemSkinID = item.skinid;
		ItemDef = ItemManager.FindItemDefinition (ClientItemID);
		InventoryIndex = item.inventorySlot;
		GridSlotIndex = item.gridSlotIndex;
		AmmoCount = item.ammoCount;
		AmmoMax = item.ammoMax;
		AmmoItemID = item.ammoID;
		AmmoItemDef = ((AmmoItemID != 0) ? ItemManager.FindItemDefinition (AmmoItemID) : null);
		Condition = item.condition;
		Rotation = item.rotation;
		AmmoTypes = (AmmoTypes)item.ammoTypes;
	}

	public void SetItem (Item item, ItemDefinition updatedItemDef, int gridCellIndex, int rotation)
	{
		InventoryIndex = item.position;
		GridSlotIndex = gridCellIndex;
		Condition = item.conditionNormalized;
		Rotation = rotation;
		SetAmmoDetails (item);
		ItemDef = updatedItemDef;
	}

	public void SetAmmoDetails (Item item)
	{
		BaseEntity heldEntity = item.GetHeldEntity ();
		if (!(heldEntity == null)) {
			BaseProjectile component = heldEntity.GetComponent<BaseProjectile> ();
			if (!(component == null)) {
				AmmoItemDef = component.primaryMagazine.ammoType;
				AmmoItemID = ((AmmoItemDef != null) ? AmmoItemDef.itemid : 0);
				AmmoCount = component.primaryMagazine.contents;
				AmmoMax = component.primaryMagazine.capacity;
				AmmoTypes = component.primaryMagazine.definition.ammoTypes;
			}
		}
	}
}
