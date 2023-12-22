#define ENABLE_PROFILER
using System.Collections.Generic;
using System.Diagnostics;
using Rust;
using UnityEngine;
using UnityEngine.Profiling;

public class LoadBalancer : SingletonComponent<LoadBalancer>
{
	public static bool Paused = false;

	private const float MinMilliseconds = 1f;

	private const float MaxMilliseconds = 100f;

	private const int MinBacklog = 1000;

	private const int MaxBacklog = 100000;

	private Queue<DeferredAction>[] queues = new Queue<DeferredAction>[5] {
		new Queue<DeferredAction> (),
		new Queue<DeferredAction> (),
		new Queue<DeferredAction> (),
		new Queue<DeferredAction> (),
		new Queue<DeferredAction> ()
	};

	private Stopwatch watch = Stopwatch.StartNew ();

	protected void LateUpdate ()
	{
		if (Rust.Application.isReceiving || Rust.Application.isLoading || Paused) {
			return;
		}
		int num = Count ();
		float t = Mathf.InverseLerp (1000f, 100000f, num);
		float num2 = Mathf.SmoothStep (1f, 100f, t);
		watch.Reset ();
		watch.Start ();
		for (int i = 0; i < queues.Length; i++) {
			Queue<DeferredAction> queue = queues [i];
			while (queue.Count > 0) {
				DeferredAction deferredAction = queue.Dequeue ();
				Profiler.BeginSample ("LoadBalancer.Tick");
				deferredAction.Action ();
				Profiler.EndSample ();
				if (watch.Elapsed.TotalMilliseconds > (double)num2) {
					return;
				}
			}
		}
	}

	public static int Count ()
	{
		if (!SingletonComponent<LoadBalancer>.Instance) {
			return 0;
		}
		Queue<DeferredAction>[] array = SingletonComponent<LoadBalancer>.Instance.queues;
		int num = 0;
		for (int i = 0; i < array.Length; i++) {
			num += array [i].Count;
		}
		return num;
	}

	public static void ProcessAll ()
	{
		if (!SingletonComponent<LoadBalancer>.Instance) {
			CreateInstance ();
		}
		Queue<DeferredAction>[] array = SingletonComponent<LoadBalancer>.Instance.queues;
		foreach (Queue<DeferredAction> queue in array) {
			while (queue.Count > 0) {
				DeferredAction deferredAction = queue.Dequeue ();
				deferredAction.Action ();
			}
		}
	}

	public static void Enqueue (DeferredAction action)
	{
		Profiler.BeginSample ("LoadBalancer.Enqueue");
		if (!SingletonComponent<LoadBalancer>.Instance) {
			CreateInstance ();
		}
		Queue<DeferredAction>[] array = SingletonComponent<LoadBalancer>.Instance.queues;
		Queue<DeferredAction> queue = array [action.Index];
		queue.Enqueue (action);
		Profiler.EndSample ();
	}

	private static void CreateInstance ()
	{
		Profiler.BeginSample ("LoadBalancer.CreateInstance");
		GameObject gameObject = new GameObject ();
		gameObject.name = "LoadBalancer";
		gameObject.AddComponent<LoadBalancer> ();
		Object.DontDestroyOnLoad (gameObject);
		Profiler.EndSample ();
	}
}
