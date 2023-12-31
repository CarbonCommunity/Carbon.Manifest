using UnityEngine;

public static class TerrainModifierEx
{
	public static void ApplyTerrainModifiers (this Transform transform, TerrainModifier[] modifiers, Vector3 pos, Quaternion rot, Vector3 scale)
	{
		foreach (TerrainModifier terrainModifier in modifiers) {
			Vector3 vector = Vector3.Scale (terrainModifier.worldPosition, scale);
			Vector3 pos2 = pos + rot * vector;
			float y = scale.y;
			terrainModifier.Apply (pos2, y);
		}
	}

	public static void ApplyTerrainModifiers (this Transform transform, TerrainModifier[] modifiers)
	{
		transform.ApplyTerrainModifiers (modifiers, transform.position, transform.rotation, transform.lossyScale);
	}
}
