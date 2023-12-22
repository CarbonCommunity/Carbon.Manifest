#define ENABLE_PROFILER
using System.Collections.Generic;
using Rust;
using UnityEngine;
using UnityEngine.Profiling;

public class WaterVisibilityTrigger : EnvironmentVolumeTrigger
{
	public bool togglePhysics = true;

	public bool toggleVisuals = true;

	private long enteredTick = 0L;

	private static long ticks = 1L;

	private static SortedList<long, WaterVisibilityTrigger> tracker = new SortedList<long, WaterVisibilityTrigger> ();

	public static void Reset ()
	{
		ticks = 1L;
		tracker.Clear ();
	}

	protected void OnDestroy ()
	{
		if (!Rust.Application.isQuitting) {
			tracker.Remove (enteredTick);
		}
	}

	private void ToggleVisibility ()
	{
	}

	private void ResetVisibility ()
	{
	}

	private void ToggleCollision (Collider other)
	{
		if (togglePhysics && WaterSystem.Collision != null) {
			WaterSystem.Collision.SetIgnore (other, base.volume.trigger);
		}
	}

	private void ResetCollision (Collider other)
	{
		if (togglePhysics && WaterSystem.Collision != null) {
			WaterSystem.Collision.SetIgnore (other, base.volume.trigger, ignore: false);
		}
	}

	protected void OnTriggerEnter (Collider other)
	{
		Profiler.BeginSample ("WaterVisibilityTrigger.OnTriggerEnter");
		bool flag = other.gameObject.GetComponent<PlayerWalkMovement> () != null;
		bool flag2 = other.gameObject.CompareTag ("MainCamera");
		if ((flag || flag2) && !tracker.ContainsValue (this)) {
			enteredTick = ticks++;
			tracker.Add (enteredTick, this);
			ToggleVisibility ();
		}
		if (!flag2 && !other.isTrigger) {
			ToggleCollision (other);
		}
		Profiler.EndSample ();
	}

	protected void OnTriggerExit (Collider other)
	{
		Profiler.BeginSample ("WaterVisibilityTrigger.OnTriggerExit");
		bool flag = other.gameObject.GetComponent<PlayerWalkMovement> () != null;
		bool flag2 = other.gameObject.CompareTag ("MainCamera");
		if ((flag || flag2) && tracker.ContainsValue (this)) {
			tracker.Remove (enteredTick);
			if (tracker.Count > 0) {
				tracker.Values [tracker.Count - 1].ToggleVisibility ();
			} else {
				ResetVisibility ();
			}
		}
		if (!flag2 && !other.isTrigger) {
			ResetCollision (other);
		}
		Profiler.EndSample ();
	}
}
