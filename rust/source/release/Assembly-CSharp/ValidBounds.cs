using UnityEngine;

public class ValidBounds : SingletonComponent<ValidBounds>
{
	public Bounds worldBounds;

	public static bool Test (Vector3 vPos)
	{
		if (!SingletonComponent<ValidBounds>.Instance) {
			return true;
		}
		return SingletonComponent<ValidBounds>.Instance.IsInside (vPos);
	}

	public static float TestDist (Vector3 vPos)
	{
		if (!SingletonComponent<ValidBounds>.Instance) {
			return float.MaxValue;
		}
		return SingletonComponent<ValidBounds>.Instance.DistToWorldEdge2D (vPos);
	}

	internal bool IsInside (Vector3 vPos)
	{
		if (vPos.IsNaNOrInfinity ()) {
			return false;
		}
		if (!worldBounds.Contains (vPos)) {
			return false;
		}
		if (TerrainMeta.Terrain != null) {
			if (World.Procedural && vPos.y < TerrainMeta.Position.y) {
				return false;
			}
			if (TerrainMeta.OutOfMargin (vPos)) {
				return false;
			}
		}
		return true;
	}

	internal float DistToWorldEdge2D (Vector3 vPos)
	{
		if (!IsInside (vPos)) {
			return -1f;
		}
		float num = worldBounds.InnerDistToEdge2D (vPos);
		if (TerrainMeta.Terrain != null) {
			float b = TerrainMeta.InnerDistToEdge2D (vPos);
			return Mathf.Min (num, b);
		}
		return num;
	}
}
