using UnityEngine;

public class Sled : BaseVehicle, INotifyTrigger
{
	private const Flags BrakeOn = Flags.Reserved1;

	private const Flags OnSnow = Flags.Reserved2;

	private const Flags IsGrounded = Flags.Reserved3;

	private const Flags OnSand = Flags.Reserved4;

	public PhysicMaterial BrakeMaterial = null;

	public PhysicMaterial SnowMaterial = null;

	public PhysicMaterial NonSnowMaterial = null;

	public Transform CentreOfMassTransform;

	public Collider[] PhysicsMaterialTargets;

	public float InitialForceCutoff = 3f;

	public float InitialForceIncreaseRate = 0.05f;

	public float TurnForce = 1f;

	public float DirectionMatchForce = 1f;

	public float VerticalAdjustmentForce = 1f;

	public float VerticalAdjustmentAngleThreshold = 15f;

	public float NudgeCooldown = 3f;

	public float NudgeForce = 2f;

	public float MaxNudgeVelocity = 2f;

	public const float DecayFrequency = 60f;

	public float DecayAmount = 10f;

	public ParticleSystemContainer TrailEffects;

	public SoundDefinition enterSnowSoundDef;

	public SoundDefinition snowSlideLoopSoundDef;

	public SoundDefinition dirtSlideLoopSoundDef;

	public AnimationCurve movementLoopGainCurve;

	public AnimationCurve movementLoopPitchCurve;

	private VehicleTerrainHandler terrainHandler = null;

	private PhysicMaterial cachedMaterial = null;

	private float initialForceScale = 0f;

	private TimeSince leftIce;

	private TimeSince lastNudge;

	public override bool BlocksDoors => false;

	public override void ServerInit ()
	{
		base.ServerInit ();
		terrainHandler = new VehicleTerrainHandler (this);
		terrainHandler.RayLength = 0.6f;
		rigidBody.centerOfMass = CentreOfMassTransform.localPosition;
		InvokeRandomized (DecayOverTime, Random.Range (30f, 60f), 60f, 6f);
	}

	public override void OnDeployed (BaseEntity parent, BasePlayer deployedBy, Item fromItem)
	{
		base.OnDeployed (parent, deployedBy, fromItem);
		SetFlag (Flags.Reserved1, b: true);
		UpdateGroundedFlag ();
		UpdatePhysicsMaterial ();
	}

	public override void VehicleFixedUpdate ()
	{
		base.VehicleFixedUpdate ();
		if (!AnyMounted ()) {
			return;
		}
		terrainHandler.FixedUpdate ();
		if (!terrainHandler.IsGrounded) {
			Vector3 up = base.transform.up;
			Quaternion b = Quaternion.FromToRotation (up, Vector3.up) * rigidBody.rotation;
			float num = Quaternion.Angle (rigidBody.rotation, b);
			if (num > VerticalAdjustmentAngleThreshold) {
				rigidBody.MoveRotation (Quaternion.Slerp (rigidBody.rotation, b, Time.fixedDeltaTime * VerticalAdjustmentForce));
			}
		}
	}

	private void UpdatePhysicsMaterial ()
	{
		cachedMaterial = GetPhysicMaterial ();
		Collider[] physicsMaterialTargets = PhysicsMaterialTargets;
		foreach (Collider collider in physicsMaterialTargets) {
			collider.sharedMaterial = cachedMaterial;
		}
		if (!AnyMounted () && rigidBody.IsSleeping ()) {
			CancelInvoke (UpdatePhysicsMaterial);
		}
		SetFlag (Flags.Reserved2, terrainHandler.IsOnSnowOrIce);
		SetFlag (Flags.Reserved4, terrainHandler.OnSurface == VehicleTerrainHandler.Surface.Sand);
	}

	private void UpdateGroundedFlag ()
	{
		if (!AnyMounted () && rigidBody.IsSleeping ()) {
			CancelInvoke (UpdateGroundedFlag);
		}
		SetFlag (Flags.Reserved3, terrainHandler.IsGrounded);
	}

	private PhysicMaterial GetPhysicMaterial ()
	{
		if (HasFlag (Flags.Reserved1) || !AnyMounted ()) {
			return BrakeMaterial;
		}
		bool flag = terrainHandler.IsOnSnowOrIce || terrainHandler.OnSurface == VehicleTerrainHandler.Surface.Sand;
		if (flag) {
			leftIce = 0f;
		} else if ((float)leftIce < 2f) {
			flag = true;
		}
		return flag ? SnowMaterial : NonSnowMaterial;
	}

	public override void PlayerMounted (BasePlayer player, BaseMountable seat)
	{
		base.PlayerMounted (player, seat);
		if (HasFlag (Flags.Reserved1)) {
			initialForceScale = 0f;
			InvokeRepeating (ApplyInitialForce, 0f, 0.1f);
			SetFlag (Flags.Reserved1, b: false);
		}
		if (!IsInvoking (UpdatePhysicsMaterial)) {
			InvokeRepeating (UpdatePhysicsMaterial, 0f, 0.5f);
		}
		if (!IsInvoking (UpdateGroundedFlag)) {
			InvokeRepeating (UpdateGroundedFlag, 0f, 0.1f);
		}
		if (rigidBody.IsSleeping ()) {
			rigidBody.WakeUp ();
		}
	}

	private void ApplyInitialForce ()
	{
		Vector3 forward = base.transform.forward;
		Vector3 vector = ((Vector3.Dot (forward, -Vector3.up) > Vector3.Dot (-forward, -Vector3.up)) ? forward : (-forward));
		rigidBody.AddForce (vector * initialForceScale * (terrainHandler.IsOnSnowOrIce ? 1f : 0.25f), ForceMode.Acceleration);
		initialForceScale += InitialForceIncreaseRate;
		if (initialForceScale >= InitialForceCutoff && (rigidBody.velocity.magnitude > 1f || !terrainHandler.IsOnSnowOrIce)) {
			CancelInvoke (ApplyInitialForce);
		}
	}

	public override void PlayerServerInput (InputState inputState, BasePlayer player)
	{
		base.PlayerServerInput (inputState, player);
		float num = Vector3.Dot (base.transform.up, Vector3.up);
		if (num < 0.1f || WaterFactor () > 0.25f) {
			DismountAllPlayers ();
			return;
		}
		float num2 = (inputState.IsDown (BUTTON.LEFT) ? (-1f) : 0f);
		num2 += (inputState.IsDown (BUTTON.RIGHT) ? 1f : 0f);
		if (inputState.IsDown (BUTTON.FORWARD) && (float)lastNudge > NudgeCooldown && rigidBody.velocity.magnitude < MaxNudgeVelocity) {
			rigidBody.WakeUp ();
			rigidBody.AddForce (base.transform.forward * NudgeForce, ForceMode.Impulse);
			rigidBody.AddForce (base.transform.up * NudgeForce * 0.5f, ForceMode.Impulse);
			lastNudge = 0f;
		}
		num2 *= TurnForce;
		Vector3 velocity = rigidBody.velocity;
		if (num2 != 0f) {
			base.transform.Rotate (Vector3.up * num2 * Time.deltaTime * velocity.magnitude, Space.Self);
		}
		if (terrainHandler.IsGrounded) {
			float num3 = Vector3.Dot (rigidBody.velocity.normalized, base.transform.forward);
			if (num3 >= 0.5f) {
				rigidBody.velocity = Vector3.Lerp (rigidBody.velocity, base.transform.forward * velocity.magnitude, Time.deltaTime * DirectionMatchForce);
			}
		}
	}

	private void DecayOverTime ()
	{
		if (!AnyMounted ()) {
			Hurt (DecayAmount);
		}
	}

	public override bool CanPickup (BasePlayer player)
	{
		return base.CanPickup (player) && !player.isMounted;
	}

	public void OnObjects (TriggerNotify trigger)
	{
		foreach (BaseEntity entityContent in trigger.entityContents) {
			if (!(entityContent is Sled)) {
				if (entityContent is BaseVehicleModule baseVehicleModule && baseVehicleModule.Vehicle != null && (baseVehicleModule.Vehicle.IsOn () || !baseVehicleModule.Vehicle.IsStationary ())) {
					Kill (DestroyMode.Gib);
					break;
				}
				if (entityContent is BaseVehicle baseVehicle && baseVehicle.HasDriver () && (baseVehicle.IsMoving () || baseVehicle.HasFlag (Flags.On))) {
					Kill (DestroyMode.Gib);
					break;
				}
			}
		}
	}

	public void OnEmpty ()
	{
	}
}
