using UnityEngine;

public static class TerrainFilterEx
{
	public static bool ApplyTerrainFilters (this Transform transform, TerrainFilter[] filters, Vector3 pos, Quaternion rot, Vector3 scale, SpawnFilter globalFilter = null)
	{
		if (filters.Length == 0) {
			return true;
		}
		foreach (TerrainFilter terrainFilter in filters) {
			Vector3 worldPosition = terrainFilter.worldPosition;
			Vector3 vector = Vector3.Scale (worldPosition, scale);
			vector = rot * vector;
			Vector3 vector2 = pos + vector;
			if (TerrainMeta.OutOfBounds (vector2)) {
				return false;
			}
			if (globalFilter != null && globalFilter.GetFactor (vector2) == 0f) {
				return false;
			}
			if (!terrainFilter.Check (vector2)) {
				return false;
			}
		}
		return true;
	}
}
