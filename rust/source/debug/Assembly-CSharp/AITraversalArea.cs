using UnityEngine;

public class AITraversalArea : TriggerBase
{
	public Transform entryPoint1;

	public Transform entryPoint2;

	public AITraversalWaitPoint[] waitPoints;

	public Bounds movementArea;

	public Transform activeEntryPoint;

	public float nextFreeTime = 0f;

	public void OnValidate ()
	{
		movementArea.center = base.transform.position;
	}

	internal override GameObject InterestedInObject (GameObject obj)
	{
		obj = base.InterestedInObject (obj);
		if (obj == null) {
			return null;
		}
		BaseEntity baseEntity = obj.ToBaseEntity ();
		if (baseEntity == null) {
			return null;
		}
		if (baseEntity.isClient) {
			return null;
		}
		if (!baseEntity.IsNpc) {
			return null;
		}
		return baseEntity.gameObject;
	}

	public bool CanTraverse (BaseEntity ent)
	{
		return Time.time > nextFreeTime;
	}

	public Transform GetClosestEntry (Vector3 position)
	{
		float num = Vector3.Distance (position, entryPoint1.position);
		float num2 = Vector3.Distance (position, entryPoint2.position);
		if (num < num2) {
			return entryPoint1;
		}
		return entryPoint2;
	}

	public Transform GetFarthestEntry (Vector3 position)
	{
		float num = Vector3.Distance (position, entryPoint1.position);
		float num2 = Vector3.Distance (position, entryPoint2.position);
		if (num > num2) {
			return entryPoint1;
		}
		return entryPoint2;
	}

	public void SetBusyFor (float dur = 1f)
	{
		nextFreeTime = Time.time + dur;
	}

	public bool CanUse (Vector3 dirFrom)
	{
		return Time.time > nextFreeTime;
	}

	internal override void OnEntityEnter (BaseEntity ent)
	{
		base.OnEntityEnter (ent);
	}

	public AITraversalWaitPoint GetEntryPointNear (Vector3 pos)
	{
		Vector3 position = GetClosestEntry (pos).position;
		Vector3 position2 = GetFarthestEntry (pos).position;
		BaseEntity[] array = new BaseEntity[1];
		AITraversalWaitPoint result = null;
		float num = 0f;
		AITraversalWaitPoint[] array2 = waitPoints;
		foreach (AITraversalWaitPoint aITraversalWaitPoint in array2) {
			if (aITraversalWaitPoint.Occupied ()) {
				continue;
			}
			Vector3 position3 = aITraversalWaitPoint.transform.position;
			float num2 = Vector3.Distance (position, position3);
			float num3 = Vector3.Distance (position2, position3);
			if (!(num3 < num2)) {
				float value = Vector3.Distance (position3, pos);
				float num4 = (1f - Mathf.InverseLerp (0f, 20f, value)) * 100f;
				if (num4 > num) {
					num = num4;
					result = aITraversalWaitPoint;
				}
			}
		}
		return result;
	}

	public bool EntityFilter (BaseEntity ent)
	{
		return ent.IsNpc && ent.isServer;
	}

	internal override void OnEntityLeave (BaseEntity ent)
	{
		base.OnEntityLeave (ent);
	}

	public void OnDrawGizmos ()
	{
		Gizmos.color = Color.magenta;
		Gizmos.DrawCube (entryPoint1.position + Vector3.up * 0.125f, new Vector3 (0.5f, 0.25f, 0.5f));
		Gizmos.DrawCube (entryPoint2.position + Vector3.up * 0.125f, new Vector3 (0.5f, 0.25f, 0.5f));
		Gizmos.color = new Color (0.2f, 1f, 0.2f, 0.5f);
		Gizmos.DrawCube (movementArea.center, movementArea.size);
		Gizmos.color = Color.magenta;
		AITraversalWaitPoint[] array = waitPoints;
		foreach (AITraversalWaitPoint aITraversalWaitPoint in array) {
			GizmosUtil.DrawCircleY (aITraversalWaitPoint.transform.position, 0.5f);
		}
	}
}
