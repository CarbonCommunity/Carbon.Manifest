#define ENABLE_PROFILER
using System.Collections.Generic;
using System.Linq;
using Facepunch;
using UnityEngine;
using UnityEngine.Profiling;

public static class TransformUtil
{
	public static bool GetGroundInfo (Vector3 startPos, out RaycastHit hit, Transform ignoreTransform = null)
	{
		return GetGroundInfo (startPos, out hit, 100f, -1, ignoreTransform);
	}

	public static bool GetGroundInfo (Vector3 startPos, out RaycastHit hit, float range, Transform ignoreTransform = null)
	{
		return GetGroundInfo (startPos, out hit, range, -1, ignoreTransform);
	}

	public static bool GetGroundInfo (Vector3 startPos, out RaycastHit hitOut, float range, LayerMask mask, Transform ignoreTransform = null)
	{
		Profiler.BeginSample ("TransformUtil.GetGroundInfo");
		startPos.y += 0.25f;
		range += 0.25f;
		hitOut = default(RaycastHit);
		Ray ray = new Ray (startPos, Vector3.down);
		if (GamePhysics.Trace (ray, 0f, out var hitInfo, range, mask)) {
			if (ignoreTransform != null && hitInfo.collider != null && (hitInfo.collider.transform == ignoreTransform || hitInfo.collider.transform.IsChildOf (ignoreTransform))) {
				return GetGroundInfo (startPos - new Vector3 (0f, 0.01f, 0f), out hitOut, range, mask, ignoreTransform);
			}
			hitOut = hitInfo;
			Profiler.EndSample ();
			return true;
		}
		Profiler.EndSample ();
		return false;
	}

	public static bool GetGroundInfo (Vector3 startPos, out Vector3 pos, out Vector3 normal, Transform ignoreTransform = null)
	{
		return GetGroundInfo (startPos, out pos, out normal, 100f, -1, ignoreTransform);
	}

	public static bool GetGroundInfo (Vector3 startPos, out Vector3 pos, out Vector3 normal, float range, Transform ignoreTransform = null)
	{
		return GetGroundInfo (startPos, out pos, out normal, range, -1, ignoreTransform);
	}

	public static bool GetGroundInfo (Vector3 startPos, out Vector3 pos, out Vector3 normal, float range, LayerMask mask, Transform ignoreTransform = null)
	{
		Profiler.BeginSample ("TransformUtil.GetGroundInfo (All)");
		startPos.y += 0.25f;
		range += 0.25f;
		List<RaycastHit> obj = Pool.GetList<RaycastHit> ();
		Ray ray = new Ray (startPos, Vector3.down);
		GamePhysics.TraceAll (ray, 0f, obj, range, mask, QueryTriggerInteraction.Ignore);
		foreach (RaycastHit item in obj) {
			if (ignoreTransform != null && item.collider != null && (item.collider.transform == ignoreTransform || item.collider.transform.IsChildOf (ignoreTransform))) {
				continue;
			}
			pos = item.point;
			normal = item.normal;
			Profiler.EndSample ();
			Pool.FreeList (ref obj);
			return true;
		}
		pos = startPos;
		normal = Vector3.up;
		Pool.FreeList (ref obj);
		Profiler.EndSample ();
		return false;
	}

	public static bool GetGroundInfoTerrainOnly (Vector3 startPos, out Vector3 pos, out Vector3 normal)
	{
		return GetGroundInfoTerrainOnly (startPos, out pos, out normal, 100f, -1);
	}

	public static bool GetGroundInfoTerrainOnly (Vector3 startPos, out Vector3 pos, out Vector3 normal, float range)
	{
		return GetGroundInfoTerrainOnly (startPos, out pos, out normal, range, -1);
	}

	public static bool GetGroundInfoTerrainOnly (Vector3 startPos, out Vector3 pos, out Vector3 normal, float range, LayerMask mask)
	{
		Profiler.BeginSample ("TransformUtil.GetGroundInfoTerrainOnly");
		startPos.y += 0.25f;
		range += 0.25f;
		Ray ray = new Ray (startPos, Vector3.down);
		if (Physics.Raycast (ray, out var hitInfo, range, mask) && hitInfo.collider is TerrainCollider) {
			pos = hitInfo.point;
			normal = hitInfo.normal;
			Profiler.EndSample ();
			return true;
		}
		pos = startPos;
		normal = Vector3.up;
		Profiler.EndSample ();
		return false;
	}

	public static Transform[] GetRootObjects ()
	{
		return (from x in Object.FindObjectsOfType<Transform> ()
			where x.transform == x.transform.root
			select x).ToArray ();
	}
}
