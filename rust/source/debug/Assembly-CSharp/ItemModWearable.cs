using Rust;
using UnityEngine;

public class ItemModWearable : ItemMod
{
	public GameObjectRef entityPrefab = new GameObjectRef ();

	public GameObjectRef entityPrefabFemale = new GameObjectRef ();

	public ProtectionProperties protectionProperties = null;

	public ArmorProperties armorProperties = null;

	public ClothingMovementProperties movementProperties = null;

	public UIBlackoutOverlay.blackoutType occlusionType = UIBlackoutOverlay.blackoutType.NONE;

	public bool blocksAiming = false;

	public bool emissive = false;

	public float accuracyBonus = 0f;

	public bool blocksEquipping = false;

	public float eggVision = 0f;

	public float weight = 0f;

	public bool equipOnRightClick = true;

	public bool npcOnly = false;

	public GameObjectRef breakEffect = new GameObjectRef ();

	public GameObjectRef viewmodelAddition;

	public Wearable targetWearable => (!entityPrefab.isValid) ? null : entityPrefab.Get ().GetComponent<Wearable> ();

	private void DoPrepare ()
	{
		if (!entityPrefab.isValid) {
			Debug.LogWarning ("ItemModWearable: entityPrefab is null! " + base.gameObject, base.gameObject);
		}
		if (entityPrefab.isValid && targetWearable == null) {
			Debug.LogWarning ("ItemModWearable: entityPrefab doesn't have a Wearable component! " + base.gameObject, entityPrefab.Get ());
		}
	}

	public override void ModInit ()
	{
		string resourcePath = entityPrefab.resourcePath;
		if (string.IsNullOrEmpty (resourcePath)) {
			Debug.LogWarning (string.Concat (this, " - entityPrefab is null or something.. - ", entityPrefab.guid));
		}
	}

	public bool ProtectsArea (HitArea area)
	{
		if (armorProperties == null) {
			return false;
		}
		return armorProperties.Contains (area);
	}

	public bool HasProtections ()
	{
		return protectionProperties != null;
	}

	internal float GetProtection (Item item, DamageType damageType)
	{
		if (protectionProperties == null) {
			return 0f;
		}
		return protectionProperties.Get (damageType) * ConditionProtectionScale (item);
	}

	public float ConditionProtectionScale (Item item)
	{
		return item.isBroken ? 0.25f : 1f;
	}

	public void CollectProtection (Item item, ProtectionProperties protection)
	{
		if (!(protectionProperties == null)) {
			protection.Add (protectionProperties, ConditionProtectionScale (item));
		}
	}

	private bool IsHeadgear ()
	{
		Wearable component = entityPrefab.Get ().GetComponent<Wearable> ();
		if (component != null && (component.occupationOver & (Wearable.OccupationSlots.HeadTop | Wearable.OccupationSlots.Face | Wearable.OccupationSlots.HeadBack)) != 0) {
			return true;
		}
		return false;
	}

	public bool IsFootwear ()
	{
		Wearable component = entityPrefab.Get ().GetComponent<Wearable> ();
		if (component != null && (component.occupationOver & (Wearable.OccupationSlots.LeftFoot | Wearable.OccupationSlots.RightFoot)) != 0) {
			return true;
		}
		return false;
	}

	public override void OnAttacked (Item item, HitInfo info)
	{
		if (!item.hasCondition) {
			return;
		}
		float num = 0f;
		for (int i = 0; i < 25; i++) {
			DamageType damageType = (DamageType)i;
			if (info.damageTypes.Has (damageType)) {
				num += Mathf.Clamp (info.damageTypes.types [i] * GetProtection (item, damageType), 0f, item.condition);
				if (num >= item.condition) {
					break;
				}
			}
		}
		item.LoseCondition (num);
		if (item != null && item.isBroken && (bool)item.GetOwnerPlayer () && IsHeadgear () && info.damageTypes.Total () >= item.GetOwnerPlayer ().health) {
			BaseEntity baseEntity = item.Drop (item.GetOwnerPlayer ().transform.position + new Vector3 (0f, 1.8f, 0f), item.GetOwnerPlayer ().GetInheritedDropVelocity () + Vector3.up * 3f);
			baseEntity.SetAngularVelocity (Random.rotation.eulerAngles * 5f);
		}
	}

	public bool CanExistWith (ItemModWearable wearable)
	{
		if (wearable == null) {
			return true;
		}
		Wearable wearable2 = targetWearable;
		Wearable wearable3 = wearable.targetWearable;
		if ((wearable2.occupationOver & wearable3.occupationOver) != 0) {
			return false;
		}
		if ((wearable2.occupationUnder & wearable3.occupationUnder) != 0) {
			return false;
		}
		return true;
	}
}
