#define ENABLE_PROFILER
using UnityEngine;
using UnityEngine.Profiling;

public static class PoolableEx
{
	public static bool SupportsPoolingInParent (this GameObject gameObject)
	{
		Profiler.BeginSample ("GameObject.SupportsPooling", gameObject);
		Profiler.BeginSample ("GetComponent", gameObject);
		Poolable componentInParent = gameObject.GetComponentInParent<Poolable> ();
		Profiler.EndSample ();
		bool result = componentInParent != null && componentInParent.prefabID != 0;
		Profiler.EndSample ();
		return result;
	}

	public static bool SupportsPooling (this GameObject gameObject)
	{
		Profiler.BeginSample ("GameObject.SupportsPooling", gameObject);
		Profiler.BeginSample ("GetComponent", gameObject);
		Poolable component = gameObject.GetComponent<Poolable> ();
		Profiler.EndSample ();
		bool result = component != null && component.prefabID != 0;
		Profiler.EndSample ();
		return result;
	}

	public static void AwakeFromInstantiate (this GameObject gameObject)
	{
		Profiler.BeginSample ("GameObject.AwakeFromInstantiate", gameObject);
		if (gameObject.activeSelf) {
			Profiler.BeginSample ("GetComponent", gameObject);
			Poolable component = gameObject.GetComponent<Poolable> ();
			Profiler.EndSample ();
			component.SetBehaviourEnabled (state: true);
		} else {
			gameObject.SetActive (value: true);
		}
		Profiler.EndSample ();
	}
}
