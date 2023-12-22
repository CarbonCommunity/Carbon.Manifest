using UnityEngine;

public class CargoMoveTest : FacepunchBehaviour
{
	public int targetNodeIndex = -1;

	private float currentThrottle = 0f;

	private float turnScale = 0f;

	private void Awake ()
	{
		Invoke (FindInitialNode, 2f);
	}

	public void FindInitialNode ()
	{
		targetNodeIndex = GetClosestNodeToUs ();
	}

	private void Update ()
	{
		UpdateMovement ();
	}

	public void UpdateMovement ()
	{
		if (TerrainMeta.Path.OceanPatrolFar == null || TerrainMeta.Path.OceanPatrolFar.Count == 0 || targetNodeIndex == -1) {
			return;
		}
		Vector3 vector = TerrainMeta.Path.OceanPatrolFar [targetNodeIndex];
		Vector3 vector2 = vector;
		float num = 0f;
		Vector3 normalized = (vector2 - base.transform.position).normalized;
		float value = Vector3.Dot (base.transform.forward, normalized);
		float num2 = Mathf.InverseLerp (0.5f, 1f, value);
		num = num2;
		float num3 = Vector3.Dot (base.transform.right, normalized);
		float num4 = 5f;
		float b = Mathf.InverseLerp (0.05f, 0.5f, Mathf.Abs (num3));
		turnScale = Mathf.Lerp (turnScale, b, Time.deltaTime * 0.2f);
		float num5 = ((!(num3 < 0f)) ? 1 : (-1));
		base.transform.Rotate (Vector3.up, num4 * Time.deltaTime * turnScale * num5, Space.World);
		currentThrottle = Mathf.Lerp (currentThrottle, num, Time.deltaTime * 0.2f);
		base.transform.position += base.transform.forward * 5f * Time.deltaTime * currentThrottle;
		if (Vector3.Distance (base.transform.position, vector2) < 60f) {
			targetNodeIndex++;
			if (targetNodeIndex >= TerrainMeta.Path.OceanPatrolFar.Count) {
				targetNodeIndex = 0;
			}
		}
	}

	public int GetClosestNodeToUs ()
	{
		int result = 0;
		float num = float.PositiveInfinity;
		for (int i = 0; i < TerrainMeta.Path.OceanPatrolFar.Count; i++) {
			Vector3 b = TerrainMeta.Path.OceanPatrolFar [i];
			float num2 = Vector3.Distance (base.transform.position, b);
			if (num2 < num) {
				result = i;
				num = num2;
			}
		}
		return result;
	}

	public void OnDrawGizmosSelected ()
	{
		if (TerrainMeta.Path.OceanPatrolFar != null) {
			Gizmos.color = Color.red;
			Gizmos.DrawSphere (TerrainMeta.Path.OceanPatrolFar [targetNodeIndex], 10f);
			for (int i = 0; i < TerrainMeta.Path.OceanPatrolFar.Count; i++) {
				Vector3 vector = TerrainMeta.Path.OceanPatrolFar [i];
				Gizmos.color = Color.green;
				Gizmos.DrawSphere (vector, 3f);
				Vector3 to = ((i + 1 == TerrainMeta.Path.OceanPatrolFar.Count) ? TerrainMeta.Path.OceanPatrolFar [0] : TerrainMeta.Path.OceanPatrolFar [i + 1]);
				Gizmos.DrawLine (vector, to);
			}
		}
	}
}
