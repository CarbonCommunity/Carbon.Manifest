using UnityEngine;

public class TriggerNoSpray : TriggerBase
{
	public BoxCollider TriggerCollider = null;

	private OBB cachedBounds;

	private Transform cachedTransform = null;

	private void OnEnable ()
	{
		cachedTransform = base.transform;
		cachedBounds = new OBB (cachedTransform, new Bounds (TriggerCollider.center, TriggerCollider.size));
	}

	internal override GameObject InterestedInObject (GameObject obj)
	{
		BaseEntity baseEntity = obj.ToBaseEntity ();
		if (baseEntity == null) {
			return null;
		}
		if (baseEntity.ToPlayer () == null) {
			return null;
		}
		return baseEntity.gameObject;
	}

	public bool IsPositionValid (Vector3 worldPosition)
	{
		return !cachedBounds.Contains (worldPosition);
	}
}
