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

	public static float GetMaximumPointTutorial ()
	{
		return Mathf.Min (TerrainMeta.Position.x + TerrainMeta.Size.x * 2f - TutorialIsland.TutorialBoundsSize, SingletonComponent<ValidBounds>.Instance.worldBounds.size.x * 0.5f);
	}

	public static float GetMaximumPoint ()
	{
		if (SingletonComponent<ValidBounds>.Instance == null) {
			return 0f;
		}
		float num = SingletonComponent<ValidBounds>.Instance.worldBounds.max.x;
		if (TerrainMeta.Terrain != null) {
			num = Mathf.Min (TerrainMeta.Position.x + TerrainMeta.Size.x, num);
		}
		return num;
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
