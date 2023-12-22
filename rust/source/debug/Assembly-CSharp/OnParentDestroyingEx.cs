#define ENABLE_PROFILER
using System.Collections.Generic;
using Facepunch;
using UnityEngine;
using UnityEngine.Profiling;

public static class OnParentDestroyingEx
{
	public static void BroadcastOnParentDestroying (this GameObject go)
	{
		Profiler.BeginSample ("OnParentDestroying");
		List<IOnParentDestroying> obj = Pool.GetList<IOnParentDestroying> ();
		go.GetComponentsInChildren (obj);
		for (int i = 0; i < obj.Count; i++) {
			obj [i].OnParentDestroying ();
		}
		Pool.FreeList (ref obj);
		Profiler.EndSample ();
	}

	public static void SendOnParentDestroying (this GameObject go)
	{
		Profiler.BeginSample ("OnParentDestroying");
		List<IOnParentDestroying> obj = Pool.GetList<IOnParentDestroying> ();
		go.GetComponents (obj);
		for (int i = 0; i < obj.Count; i++) {
			obj [i].OnParentDestroying ();
		}
		Pool.FreeList (ref obj);
		Profiler.EndSample ();
	}
}
