#define ENABLE_PROFILER
using System.Collections.Generic;
using Facepunch;
using UnityEngine;
using UnityEngine.Profiling;

public static class OnParentSpawningEx
{
	public static void BroadcastOnParentSpawning (this GameObject go)
	{
		Profiler.BeginSample ("OnParentSpawning");
		List<IOnParentSpawning> obj = Pool.GetList<IOnParentSpawning> ();
		go.GetComponentsInChildren (obj);
		for (int i = 0; i < obj.Count; i++) {
			obj [i].OnParentSpawning ();
		}
		Pool.FreeList (ref obj);
		Profiler.EndSample ();
	}

	public static void SendOnParentSpawning (this GameObject go)
	{
		Profiler.BeginSample ("OnParentSpawning");
		List<IOnParentSpawning> obj = Pool.GetList<IOnParentSpawning> ();
		go.GetComponents (obj);
		for (int i = 0; i < obj.Count; i++) {
			obj [i].OnParentSpawning ();
		}
		Pool.FreeList (ref obj);
		Profiler.EndSample ();
	}
}
