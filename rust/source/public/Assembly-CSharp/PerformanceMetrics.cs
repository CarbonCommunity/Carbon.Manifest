using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;

public static class PerformanceMetrics
{
	[Serializable]
	[CompilerGenerated]
	private sealed class <>c
	{
		public static readonly <>c <>9 = new <>c ();

		public static UnityAction <>9__3_0;

		internal void <Setup>b__3_0 ()
		{
			OnBeforeRender?.Invoke ();
		}
	}

	private static PerformanceSamplePoint current;

	private static Action OnBeforeRender;

	private static int _mainThreadId;

	public static PerformanceSamplePoint GetCurrent (bool reset = false)
	{
		PerformanceSamplePoint result = current;
		if (reset) {
			current = default(PerformanceSamplePoint);
		}
		return result;
	}

	public static void Setup ()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Expected O, but got Unknown
		object obj = <>c.<>9__3_0;
		if (obj == null) {
			UnityAction val = delegate {
				OnBeforeRender?.Invoke ();
			};
			<>c.<>9__3_0 = val;
			obj = (object)val;
		}
		Application.onBeforeRender += (UnityAction)obj;
		_mainThreadId = Thread.CurrentThread.ManagedThreadId;
		AddStopwatch (PerformanceSample.PreCull, ref OnBeforeRender, ref CameraUpdateHook.RustCamera_PreRender);
		AddStopwatch (PerformanceSample.Update, ref PreUpdateHook.OnUpdate, ref PostUpdateHook.OnUpdate);
		AddStopwatch (PerformanceSample.LateUpdate, ref PreUpdateHook.OnLateUpdate, ref PostUpdateHook.OnLateUpdate);
		AddStopwatch (PerformanceSample.Render, ref CameraUpdateHook.PreRender, ref CameraUpdateHook.PostRender);
		AddStopwatch (PerformanceSample.FixedUpdate, ref PreUpdateHook.OnFixedUpdate, ref PostUpdateHook.OnFixedUpdate);
		AddCPUTimeStopwatch ();
	}

	private static void AddCPUTimeStopwatch ()
	{
		Stopwatch watch = new Stopwatch ();
		TimeSpan lastTime = default(TimeSpan);
		int lastFrame = 0;
		StartOfFrameHook.OnStartOfFrame = (Action)Delegate.Combine (StartOfFrameHook.OnStartOfFrame, (Action)delegate {
			current.TotalCPU += lastTime;
			current.CpuUpdateCount++;
			lastTime = default(TimeSpan);
			if (Time.frameCount != lastFrame) {
				lastFrame = Time.frameCount;
				watch.Restart ();
			}
		});
		CameraUpdateHook.PostRender = (Action)Delegate.Combine (CameraUpdateHook.PostRender, (Action)delegate {
			lastTime = watch.Elapsed;
		});
	}

	private static void AddStopwatch (PerformanceSample sample, ref Action pre, ref Action post)
	{
		Stopwatch watch = new Stopwatch ();
		bool active = false;
		pre = (Action)Delegate.Combine (pre, (Action)delegate {
			if (!active) {
				active = true;
				watch.Restart ();
			}
		});
		post = (Action)Delegate.Combine (post, (Action)delegate {
			if (active) {
				active = false;
				watch.Stop ();
				switch (sample) {
				case PerformanceSample.Update:
					current.UpdateCount++;
					current.Update += watch.Elapsed;
					break;
				case PerformanceSample.LateUpdate:
					current.LateUpdate += watch.Elapsed;
					break;
				case PerformanceSample.FixedUpdate:
					current.FixedUpdate += watch.Elapsed;
					current.FixedUpdateCount++;
					break;
				case PerformanceSample.PreCull:
					current.PreCull += watch.Elapsed;
					break;
				case PerformanceSample.Render:
					current.Render += watch.Elapsed;
					current.RenderCount++;
					break;
				case PerformanceSample.TotalCPU:
					current.TotalCPU += watch.Elapsed;
					break;
				case PerformanceSample.NetworkMessage:
					break;
				}
			}
		});
	}
}
