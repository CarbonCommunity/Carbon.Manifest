#define ENABLE_PROFILER
using System.Collections.Generic;
using Facepunch;
using UnityEngine;
using UnityEngine.Profiling;

public static class OnPostNetworkUpdateEx
{
	public static void BroadcastOnPostNetworkUpdate (this GameObject go, BaseEntity entity)
	{
		Profiler.BeginSample ("BroadcastOnPostNetworkUpdate.GetComponents");
		List<IOnPostNetworkUpdate> obj = Pool.GetList<IOnPostNetworkUpdate> ();
		Profiler.EndSample ();
		Profiler.BeginSample ("BroadcastOnPostNetworkUpdate");
		go.GetComponentsInChildren (obj);
		for (int i = 0; i < obj.Count; i++) {
			obj [i].OnPostNetworkUpdate (entity);
		}
		Pool.FreeList (ref obj);
		Profiler.EndSample ();
	}

	public static void SendOnPostNetworkUpdate (this GameObject go, BaseEntity entity)
	{
		Profiler.BeginSample ("OnPostNetworkUpdate");
		List<IOnPostNetworkUpdate> obj = Pool.GetList<IOnPostNetworkUpdate> ();
		go.GetComponents (obj);
		for (int i = 0; i < obj.Count; i++) {
			obj [i].OnPostNetworkUpdate (entity);
		}
		Pool.FreeList (ref obj);
		Profiler.EndSample ();
	}
}
