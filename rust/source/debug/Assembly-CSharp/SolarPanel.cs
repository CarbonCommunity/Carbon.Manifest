#define ENABLE_PROFILER
using UnityEngine;
using UnityEngine.Profiling;

public class SolarPanel : IOEntity
{
	public Transform sunSampler;

	private const int tickrateSeconds = 60;

	public int maximalPowerOutput = 10;

	public float dot_minimum = 0.1f;

	public float dot_maximum = 0.6f;

	public override bool IsRootEntity ()
	{
		return true;
	}

	public override int MaximalPowerOutput ()
	{
		return maximalPowerOutput;
	}

	public override int ConsumptionAmount ()
	{
		return 0;
	}

	public override void ServerInit ()
	{
		base.ServerInit ();
		InvokeRandomized (SunUpdate, 1f, 5f, 2f);
	}

	public void SunUpdate ()
	{
		int num = currentEnergy;
		Profiler.BeginSample ("SolarPanel.SunUpdate");
		if (TOD_Sky.Instance.IsNight) {
			num = 0;
		} else {
			Vector3 sunDirection = TOD_Sky.Instance.SunDirection;
			float value = Vector3.Dot (sunSampler.transform.forward, sunDirection);
			float num2 = Mathf.InverseLerp (dot_minimum, dot_maximum, value);
			if (num2 > 0f && !IsVisible (sunSampler.transform.position + sunDirection * 100f, 101f)) {
				num2 = 0f;
			}
			num = Mathf.FloorToInt ((float)maximalPowerOutput * num2 * base.healthFraction);
		}
		bool flag = currentEnergy != num;
		currentEnergy = num;
		if (flag) {
			MarkDirty ();
		}
		Profiler.EndSample ();
	}

	public override int GetPassthroughAmount (int outputSlot = 0)
	{
		return (outputSlot == 0) ? currentEnergy : 0;
	}
}
