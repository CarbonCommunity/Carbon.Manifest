#define ENABLE_PROFILER
using System.Collections.Generic;
using Facepunch;
using UnityEngine;
using UnityEngine.Profiling;

public static class OnSendNetworkUpdateEx
{
	public static void BroadcastOnSendNetworkUpdate (this GameObject go, BaseEntity entity)
	{
		Profiler.BeginSample ("OnSendNetworkUpdate");
		List<IOnSendNetworkUpdate> obj = Pool.GetList<IOnSendNetworkUpdate> ();
		go.GetComponentsInChildren (obj);
		for (int i = 0; i < obj.Count; i++) {
			obj [i].OnSendNetworkUpdate (entity);
		}
		Pool.FreeList (ref obj);
		Profiler.EndSample ();
	}

	public static void SendOnSendNetworkUpdate (this GameObject go, BaseEntity entity)
	{
		Profiler.BeginSample ("OnSendNetworkUpdate");
		List<IOnSendNetworkUpdate> obj = Pool.GetList<IOnSendNetworkUpdate> ();
		go.GetComponents (obj);
		for (int i = 0; i < obj.Count; i++) {
			obj [i].OnSendNetworkUpdate (entity);
		}
		Pool.FreeList (ref obj);
		Profiler.EndSample ();
	}
}
