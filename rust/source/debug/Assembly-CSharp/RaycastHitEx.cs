using Rust;
using UnityEngine;

public static class RaycastHitEx
{
	public static Transform GetTransform (this RaycastHit hit)
	{
		return hit.transform;
	}

	public static Rigidbody GetRigidbody (this RaycastHit hit)
	{
		return hit.rigidbody;
	}

	public static Collider GetCollider (this RaycastHit hit)
	{
		return hit.collider;
	}

	public static BaseEntity GetEntity (this RaycastHit hit)
	{
		return (hit.collider != null) ? hit.collider.ToBaseEntity () : null;
	}

	public static bool IsOnLayer (this RaycastHit hit, Layer rustLayer)
	{
		return hit.collider != null && hit.collider.gameObject.IsOnLayer (rustLayer);
	}

	public static bool IsOnLayer (this RaycastHit hit, int layer)
	{
		return hit.collider != null && hit.collider.gameObject.IsOnLayer (layer);
	}

	public static bool IsWaterHit (this RaycastHit hit)
	{
		return hit.collider == null || hit.collider.gameObject.IsOnLayer (Layer.Water);
	}

	public static WaterBody GetWaterBody (this RaycastHit hit)
	{
		if (hit.collider == null) {
			return WaterSystem.Ocean;
		}
		Transform transform = hit.collider.transform;
		if (transform.TryGetComponent<WaterBody> (out var component)) {
			return component;
		}
		return transform.parent.GetComponentInChildren<WaterBody> ();
	}
}
