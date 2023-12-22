using System;
using System.Collections.Generic;
using ConVar;
using Rust;
using UnityEngine;
using VacuumBreather;

public class BaseBoat : BaseVehicle
{
	[Header ("Boat")]
	public float engineThrust = 10f;

	public float steeringScale = 0.1f;

	public Transform thrustPoint;

	public Transform centerOfMass;

	public Buoyancy buoyancy;

	public bool preventDecayIndoors = true;

	[Header ("Correction Forces")]
	public bool applyCorrectionForces = true;

	public Transform[] planeFitPoints;

	public Vector3 inAirPID;

	public float inAirDesiredPitch = -15f;

	public Vector3 wavePID;

	public MinMax correctionRange;

	public float correctionSpringForce;

	public float correctionSpringDamping;

	private Vector3[] worldAnchors;

	private PidQuaternionController pidController;

	[ServerVar]
	public static bool generate_paths = true;

	[NonSerialized]
	public float gasPedal;

	[NonSerialized]
	public float steering;

	public bool InDryDock ()
	{
		return GetParentEntity () != null;
	}

	public override float MaxVelocity ()
	{
		return 25f;
	}

	public override void ServerInit ()
	{
		base.ServerInit ();
		rigidBody.isKinematic = false;
		if (rigidBody == null) {
			Debug.LogWarning ("Boat rigidbody null");
			return;
		}
		if (centerOfMass == null) {
			Debug.LogWarning ("boat COM null");
			return;
		}
		rigidBody.centerOfMass = centerOfMass.localPosition;
		if (planeFitPoints == null || planeFitPoints.Length != 3) {
			Debug.LogWarning ("Boats require 3 plane fit points");
			return;
		}
		worldAnchors = new Vector3[3];
		pidController = new PidQuaternionController (wavePID.x, wavePID.y, wavePID.z);
	}

	public override void PlayerServerInput (InputState inputState, BasePlayer player)
	{
		if (IsDriver (player)) {
			DriverInput (inputState, player);
		}
	}

	public virtual void DriverInput (InputState inputState, BasePlayer player)
	{
		if (inputState.IsDown (BUTTON.FORWARD)) {
			gasPedal = 1f;
		} else if (inputState.IsDown (BUTTON.BACKWARD)) {
			gasPedal = -0.5f;
		} else {
			gasPedal = 0f;
		}
		if (inputState.IsDown (BUTTON.LEFT)) {
			steering = 1f;
		} else if (inputState.IsDown (BUTTON.RIGHT)) {
			steering = -1f;
		} else {
			steering = 0f;
		}
	}

	public void OnPoolDestroyed ()
	{
		Kill (DestroyMode.Gib);
	}

	public void WakeUp ()
	{
		if (rigidBody != null) {
			rigidBody.WakeUp ();
			rigidBody.AddForce (Vector3.up * 0.1f, ForceMode.Impulse);
		}
	}

	protected override void OnServerWake ()
	{
		if (buoyancy != null) {
			buoyancy.Wake ();
		}
	}

	public virtual bool EngineOn ()
	{
		if (HasDriver ()) {
			return !IsFlipped ();
		}
		return false;
	}

	public override void VehicleFixedUpdate ()
	{
		base.VehicleFixedUpdate ();
		if (!EngineOn ()) {
			gasPedal = 0f;
			steering = 0f;
		}
		base.VehicleFixedUpdate ();
		ApplyCorrectionForces ();
		bool flag = WaterLevel.Test (thrustPoint.position, waves: true, volumes: true, this);
		if (gasPedal != 0f && flag && buoyancy.submergedFraction > 0.3f) {
			Vector3 force = (base.transform.forward + base.transform.right * steering * steeringScale).normalized * gasPedal * engineThrust;
			rigidBody.AddForceAtPosition (force, thrustPoint.position, ForceMode.Force);
		}
		if (AnyMounted () && IsFlipped ()) {
			DismountAllPlayers ();
		}
	}

	protected void ApplyCorrectionForces ()
	{
		if (applyCorrectionForces && planeFitPoints != null && planeFitPoints.Length == 3 && HasDriver () && !(buoyancy.submergedFraction < 0.5f)) {
			Matrix4x4 matrix4x = Matrix4x4.TRS (base.transform.position, Quaternion.Euler (0f, base.transform.eulerAngles.y, 0f), Vector3.one);
			for (int i = 0; i < planeFitPoints.Length; i++) {
				Vector3 vector = matrix4x.MultiplyPoint (planeFitPoints [i].localPosition);
				vector.y = WaterSystem.GetHeight (vector);
				worldAnchors [i] = vector;
			}
			Vector3 normal = new Plane (worldAnchors [0], worldAnchors [1], worldAnchors [2]).normal;
			Vector3 rhs = Vector3.Normalize (worldAnchors [2] - worldAnchors [1]);
			Quaternion desiredOrientation = Quaternion.LookRotation (Vector3.Cross (normal, rhs), normal);
			float y = planeFitPoints [0].localPosition.y;
			float num = (worldAnchors [0].y + worldAnchors [1].y + worldAnchors [2].y) / 3f - y;
			float y2 = base.transform.position.y;
			float num2 = num - y2;
			Vector3 velocity = rigidBody.velocity;
			if (y2 > num + correctionRange.x && y2 < num + correctionRange.y) {
				float num3 = num2 * correctionSpringForce;
				float num4 = (0f - velocity.y) * correctionSpringDamping;
				rigidBody.AddForce (0f, num3 + num4, 0f, ForceMode.Force);
			}
			if (y2 > num + correctionRange.y) {
				desiredOrientation = Quaternion.Euler (inAirDesiredPitch, base.transform.eulerAngles.y, 0f);
				pidController.Kp = inAirPID.x;
				pidController.Ki = inAirPID.y;
				pidController.Kd = inAirPID.z;
				Vector3 torque = pidController.ComputeRequiredAngularAcceleration (base.transform.rotation, desiredOrientation, rigidBody.angularVelocity, UnityEngine.Time.fixedDeltaTime);
				rigidBody.AddTorque (torque, ForceMode.Acceleration);
			} else if (y2 > num + correctionRange.x) {
				pidController.Kp = wavePID.x;
				pidController.Ki = wavePID.y;
				pidController.Kd = wavePID.z;
				Vector3 torque2 = pidController.ComputeRequiredAngularAcceleration (base.transform.rotation, desiredOrientation, rigidBody.angularVelocity, UnityEngine.Time.fixedDeltaTime);
				torque2.y = 0f;
				rigidBody.AddTorque (torque2, ForceMode.Acceleration);
			}
		}
	}

	public static void WaterVehicleDecay (BaseCombatEntity entity, float decayTickRate, float timeSinceLastUsed, float outsideDecayMinutes, float deepWaterDecayMinutes, float decayStartDelayMinutes, bool preventDecayIndoors)
	{
		if (entity.healthFraction != 0f && !(timeSinceLastUsed < 60f * decayStartDelayMinutes)) {
			float overallWaterDepth = WaterLevel.GetOverallWaterDepth (entity.transform.position, waves: true, volumes: false);
			float num = outsideDecayMinutes;
			if (preventDecayIndoors && !entity.IsOutside ()) {
				num = float.PositiveInfinity;
			}
			if (overallWaterDepth > 12f) {
				float t = Mathf.InverseLerp (12f, 16f, overallWaterDepth);
				float num2 = Mathf.Lerp (0.1f, 1f, t);
				num = Mathf.Min (num, deepWaterDecayMinutes / num2);
			}
			if (!float.IsPositiveInfinity (num)) {
				float num3 = decayTickRate / 60f / num;
				entity.Hurt (entity.MaxHealth () * num3, DamageType.Decay, entity, useProtection: false);
			}
		}
	}

	public virtual bool EngineInWater ()
	{
		return TerrainMeta.WaterMap.GetHeight (thrustPoint.position) > thrustPoint.position.y;
	}

	public override float WaterFactorForPlayer (BasePlayer player)
	{
		if (TerrainMeta.WaterMap.GetHeight (player.eyes.position) >= player.eyes.position.y) {
			return 1f;
		}
		return 0f;
	}

	public static float GetWaterDepth (Vector3 pos)
	{
		if (!UnityEngine.Application.isPlaying || TerrainMeta.WaterMap == null) {
			if (!UnityEngine.Physics.Raycast (pos, Vector3.down, out var hitInfo, 100f, 8388608)) {
				return 100f;
			}
			return hitInfo.distance;
		}
		return TerrainMeta.WaterMap.GetDepth (pos);
	}

	public static List<Vector3> GenerateOceanPatrolPath (float minDistanceFromShore = 50f, float minWaterDepth = 8f)
	{
		float x = TerrainMeta.Size.x;
		float num = x * 2f * (float)Math.PI;
		float num2 = 30f;
		int num3 = Mathf.CeilToInt (num / num2);
		List<Vector3> list = new List<Vector3> ();
		float num4 = x;
		float y = 0f;
		for (int i = 0; i < num3; i++) {
			float num5 = (float)i / (float)num3 * 360f;
			list.Add (new Vector3 (Mathf.Sin (num5 * ((float)Math.PI / 180f)) * num4, y, Mathf.Cos (num5 * ((float)Math.PI / 180f)) * num4));
		}
		float num6 = 4f;
		float num7 = 200f;
		bool flag = true;
		for (int j = 0; j < AI.ocean_patrol_path_iterations && flag; j++) {
			flag = false;
			for (int k = 0; k < num3; k++) {
				Vector3 vector = list [k];
				int index = ((k == 0) ? (num3 - 1) : (k - 1));
				int index2 = ((k != num3 - 1) ? (k + 1) : 0);
				Vector3 b = list [index2];
				Vector3 b2 = list [index];
				Vector3 origin = vector;
				Vector3 normalized = (Vector3.zero - vector).normalized;
				Vector3 vector2 = vector + normalized * num6;
				if (Vector3.Distance (vector2, b) > num7 || Vector3.Distance (vector2, b2) > num7) {
					continue;
				}
				bool flag2 = true;
				int num8 = 16;
				for (int l = 0; l < num8; l++) {
					float num9 = (float)l / (float)num8 * 360f;
					Vector3 normalized2 = new Vector3 (Mathf.Sin (num9 * ((float)Math.PI / 180f)), y, Mathf.Cos (num9 * ((float)Math.PI / 180f))).normalized;
					Vector3 vector3 = vector2 + normalized2 * 1f;
					GetWaterDepth (vector3);
					Vector3 direction = normalized;
					if (vector3 != Vector3.zero) {
						direction = (vector3 - vector2).normalized;
					}
					if (UnityEngine.Physics.SphereCast (origin, 3f, direction, out var _, minDistanceFromShore, 1218511105)) {
						flag2 = false;
						break;
					}
				}
				if (flag2) {
					flag = true;
					list [k] = vector2;
				}
			}
		}
		if (flag) {
			Debug.LogWarning ("Failed to generate ocean patrol path");
			return null;
		}
		List<int> list2 = new List<int> ();
		LineUtility.Simplify (list, 5f, list2);
		List<Vector3> list3 = list;
		list = new List<Vector3> ();
		foreach (int item in list2) {
			list.Add (list3 [item]);
		}
		Debug.Log ("Generated ocean patrol path with node count: " + list.Count);
		return list;
	}
}
