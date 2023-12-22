using System.Collections.Generic;
using UnityEngine;

public class AIMovePoint : AIPoint
{
	public class DistTo
	{
		public float distance;

		public AIMovePoint target;
	}

	public ListDictionary<AIMovePoint, float> distances = new ListDictionary<AIMovePoint, float> ();

	public ListDictionary<AICoverPoint, float> distancesToCover = new ListDictionary<AICoverPoint, float> ();

	public float radius = 1f;

	public float WaitTime = 0f;

	public List<Transform> LookAtPoints = null;

	public void OnDrawGizmos ()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		Color color = Gizmos.color;
		Gizmos.color = Color.green;
		GizmosUtil.DrawWireCircleY (((Component)this).transform.position, radius);
		Gizmos.color = color;
	}

	public void DrawLookAtPoints ()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		Color color = Gizmos.color;
		Gizmos.color = Color.gray;
		if (LookAtPoints != null) {
			foreach (Transform lookAtPoint in LookAtPoints) {
				if (!((Object)(object)lookAtPoint == (Object)null)) {
					Gizmos.DrawSphere (lookAtPoint.position, 0.2f);
					Gizmos.DrawLine (((Component)this).transform.position, lookAtPoint.position);
				}
			}
		}
		Gizmos.color = color;
	}

	public void Clear ()
	{
		LookAtPoints = null;
	}

	public void AddLookAtPoint (Transform transform)
	{
		if (LookAtPoints == null) {
			LookAtPoints = new List<Transform> ();
		}
		LookAtPoints.Add (transform);
	}

	public bool HasLookAtPoints ()
	{
		return LookAtPoints != null && LookAtPoints.Count > 0;
	}

	public Transform GetRandomLookAtPoint ()
	{
		if (LookAtPoints == null || LookAtPoints.Count == 0) {
			return null;
		}
		return LookAtPoints [Random.Range (0, LookAtPoints.Count)];
	}
}
