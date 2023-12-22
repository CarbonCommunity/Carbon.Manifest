using Rust;
using UnityEngine;

public static class ColliderEx
{
	public static PhysicMaterial GetMaterialAt (this Collider obj, Vector3 pos)
	{
		if (obj == null) {
			return TerrainMeta.Config.WaterMaterial;
		}
		if (obj is TerrainCollider) {
			return TerrainMeta.Physics.GetMaterial (pos);
		}
		return obj.sharedMaterial;
	}

	public static bool IsOnLayer (this Collider col, Layer rustLayer)
	{
		return col != null && col.gameObject.IsOnLayer (rustLayer);
	}

	public static bool IsOnLayer (this Collider col, int layer)
	{
		return col != null && col.gameObject.IsOnLayer (layer);
	}

	public static float GetRadius (this Collider col, Vector3 transformScale)
	{
		float result = 1f;
		if (col is SphereCollider sphereCollider) {
			result = sphereCollider.radius * transformScale.Max ();
		} else if (col is BoxCollider boxCollider) {
			Vector3 v = Vector3.Scale (boxCollider.size, transformScale);
			result = v.Max () * 0.5f;
		} else if (col is CapsuleCollider { direction: var direction } capsuleCollider) {
			float num = direction switch {
				0 => transformScale.y, 
				1 => transformScale.x, 
				_ => transformScale.x, 
			};
			result = capsuleCollider.radius * num;
		} else if (col is MeshCollider { bounds: var bounds }) {
			Vector3 v2 = Vector3.Scale (bounds.size, transformScale);
			result = v2.Max () * 0.5f;
		}
		return result;
	}
}
