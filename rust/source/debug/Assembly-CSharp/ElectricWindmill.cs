#define ENABLE_PROFILER
using System;
using Facepunch;
using ProtoBuf;
using UnityEngine;
using UnityEngine.Profiling;

public class ElectricWindmill : IOEntity
{
	public Animator animator;

	public int maxPowerGeneration = 100;

	public Transform vaneRot;

	public SoundDefinition wooshSound;

	public Transform wooshOrigin;

	public float targetSpeed = 0f;

	private float serverWindSpeed;

	public override int MaximalPowerOutput ()
	{
		return maxPowerGeneration;
	}

	public override bool IsRootEntity ()
	{
		return true;
	}

	public float GetWindSpeedScale ()
	{
		float num = Time.time / 600f;
		float num2 = base.transform.position.x / 512f;
		float num3 = base.transform.position.z / 512f;
		float num4 = Mathf.PerlinNoise (num2 + num, num3 + num * 0.1f);
		float height = TerrainMeta.HeightMap.GetHeight (base.transform.position);
		float num5 = base.transform.position.y - height;
		if (num5 < 0f) {
			num5 = 0f;
		}
		float num6 = Mathf.InverseLerp (0f, 50f, num5);
		return Mathf.Clamp01 (num6 * 0.5f + num4);
	}

	public override void Load (LoadInfo info)
	{
		base.Load (info);
	}

	public override void ServerInit ()
	{
		base.ServerInit ();
		InvokeRandomized (WindUpdate, 1f, 20f, 2f);
	}

	public override void Save (SaveInfo info)
	{
		base.Save (info);
		if (!info.forDisk) {
			if (info.msg.ioEntity == null) {
				info.msg.ioEntity = Pool.Get<ProtoBuf.IOEntity> ();
			}
			info.msg.ioEntity.genericFloat1 = Time.time;
			info.msg.ioEntity.genericFloat2 = serverWindSpeed;
		}
	}

	public bool AmIVisible ()
	{
		int num = 15;
		Vector3 vector = base.transform.position + Vector3.up * 6f;
		if (!IsVisible (vector + base.transform.up * num, num + 1)) {
			return false;
		}
		Vector3 windAimDir = GetWindAimDir (Time.time);
		if (!IsVisible (vector + windAimDir * num, num + 1)) {
			return false;
		}
		return true;
	}

	public void WindUpdate ()
	{
		Profiler.BeginSample ("ElectricWindmill.WindUpdate");
		serverWindSpeed = GetWindSpeedScale ();
		if (!AmIVisible ()) {
			serverWindSpeed = 0f;
		}
		int num = Mathf.FloorToInt ((float)maxPowerGeneration * serverWindSpeed);
		bool flag = currentEnergy != num;
		currentEnergy = num;
		if (flag) {
			MarkDirty ();
		}
		SendNetworkUpdate ();
		Profiler.EndSample ();
	}

	public override int GetPassthroughAmount (int outputSlot = 0)
	{
		return (outputSlot == 0) ? currentEnergy : 0;
	}

	public Vector3 GetWindAimDir (float time)
	{
		float num = time / 3600f;
		float num2 = num * 360f;
		int num3 = 10;
		return new Vector3 (Mathf.Sin (num2 * ((float)Math.PI / 180f)) * (float)num3, 0f, Mathf.Cos (num2 * ((float)Math.PI / 180f)) * (float)num3).normalized;
	}
}
