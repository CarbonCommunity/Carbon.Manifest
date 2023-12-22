#define ENABLE_PROFILER
using System.Collections.Generic;
using Facepunch;
using Rust;
using Rust.Registry;
using UnityEngine;
using UnityEngine.Profiling;

public static class GameObjectEx
{
	public static BaseEntity ToBaseEntity (this GameObject go)
	{
		return go.transform.ToBaseEntity ();
	}

	public static BaseEntity ToBaseEntity (this Collider collider)
	{
		return collider.transform.ToBaseEntity ();
	}

	public static BaseEntity ToBaseEntity (this Transform transform)
	{
		Profiler.BeginSample ("GetEntityFromRegistry");
		IEntity entity = GetEntityFromRegistry (transform);
		Profiler.EndSample ();
		if (entity == null && !transform.gameObject.activeInHierarchy) {
			Profiler.BeginSample ("GetEntityFromComponent");
			entity = GetEntityFromComponent (transform);
			Profiler.EndSample ();
		}
		return entity as BaseEntity;
	}

	public static bool IsOnLayer (this GameObject go, Layer rustLayer)
	{
		return go.IsOnLayer ((int)rustLayer);
	}

	public static bool IsOnLayer (this GameObject go, int layer)
	{
		return go != null && go.layer == layer;
	}

	private static IEntity GetEntityFromRegistry (Transform transform)
	{
		Transform transform2 = transform;
		IEntity entity = Entity.Get (transform2);
		while (entity == null && transform2.parent != null) {
			transform2 = transform2.parent;
			entity = Entity.Get (transform2);
		}
		return (entity == null || entity.IsDestroyed) ? null : entity;
	}

	private static IEntity GetEntityFromComponent (Transform transform)
	{
		Transform transform2 = transform;
		IEntity component = transform2.GetComponent<IEntity> ();
		while (component == null && transform2.parent != null) {
			transform2 = transform2.parent;
			component = transform2.GetComponent<IEntity> ();
		}
		return (component == null || component.IsDestroyed) ? null : component;
	}

	public static void SetHierarchyGroup (this GameObject obj, string strRoot, bool groupActive = true, bool persistant = false)
	{
		obj.transform.SetParent (HierarchyUtil.GetRoot (strRoot, groupActive, persistant).transform, worldPositionStays: true);
	}

	public static bool HasComponent<T> (this GameObject obj) where T : Component
	{
		return obj.GetComponent<T> () != null;
	}

	public static void SetChildComponentsEnabled<T> (this GameObject gameObject, bool enabled) where T : MonoBehaviour
	{
		List<T> obj = Pool.GetList<T> ();
		gameObject.GetComponentsInChildren (includeInactive: true, obj);
		foreach (T item in obj) {
			item.enabled = enabled;
		}
		Pool.FreeList (ref obj);
	}
}
