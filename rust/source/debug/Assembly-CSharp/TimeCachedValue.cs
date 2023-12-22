#define ENABLE_PROFILER
using System;
using UnityEngine;
using UnityEngine.Profiling;

public class TimeCachedValue<T>
{
	public float refreshCooldown;

	public float refreshRandomRange;

	public Func<T> updateValue;

	private T cachedValue;

	private TimeSince cooldown;

	private bool hasRun;

	private bool forceNextRun;

	public T Get (bool force)
	{
		if ((float)cooldown < refreshCooldown && !force && hasRun && !forceNextRun) {
			Profiler.BeginSample ("TimeCachedValue.Cache");
			Profiler.EndSample ();
			return cachedValue;
		}
		Profiler.BeginSample ("TimeCachedValue.UpdateValue");
		hasRun = true;
		forceNextRun = false;
		cooldown = 0f - UnityEngine.Random.Range (0f, refreshRandomRange);
		if (updateValue != null) {
			cachedValue = updateValue ();
		} else {
			cachedValue = default(T);
		}
		Profiler.EndSample ();
		return cachedValue;
	}

	public void ForceNextRun ()
	{
		forceNextRun = true;
	}
}
