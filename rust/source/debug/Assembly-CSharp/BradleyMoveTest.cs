using UnityEngine;

public class BradleyMoveTest : MonoBehaviour
{
	public WheelCollider[] leftWheels;

	public WheelCollider[] rightWheels;

	public float moveForceMax = 2000f;

	public float brakeForce = 100f;

	public float throttle = 1f;

	public float turnForce = 2000f;

	public float sideStiffnessMax = 1f;

	public float sideStiffnessMin = 0.5f;

	public Transform centerOfMass;

	public float turning = 0f;

	public bool brake = false;

	public Rigidbody myRigidBody;

	public Vector3 destination;

	public float stoppingDist = 5f;

	public GameObject followTest;

	public void Awake ()
	{
		Initialize ();
	}

	public void Initialize ()
	{
		myRigidBody.centerOfMass = centerOfMass.localPosition;
		destination = base.transform.position;
	}

	public void SetDestination (Vector3 dest)
	{
		destination = dest;
	}

	public void FixedUpdate ()
	{
		Vector3 velocity = myRigidBody.velocity;
		SetDestination (followTest.transform.position);
		float num = Vector3.Distance (base.transform.position, destination);
		if (num > stoppingDist) {
			Vector3 zero = Vector3.zero;
			float num2 = Vector3.Dot (zero, base.transform.right);
			float num3 = Vector3.Dot (zero, -base.transform.right);
			float num4 = Vector3.Dot (zero, base.transform.right);
			float num5 = Vector3.Dot (zero, -base.transform.forward);
			if (num5 > num4) {
				if (num2 >= num3) {
					turning = 1f;
				} else {
					turning = -1f;
				}
			} else {
				turning = num4;
			}
			throttle = Mathf.InverseLerp (stoppingDist, 30f, num);
		}
		throttle = Mathf.Clamp (throttle, -1f, 1f);
		float num6 = throttle;
		float num7 = throttle;
		if (turning > 0f) {
			num7 = 0f - turning;
			num6 = turning;
		} else if (turning < 0f) {
			num6 = turning;
			num7 = turning * -1f;
		}
		ApplyBrakes (brake ? 1f : 0f);
		float num8 = throttle;
		num6 = Mathf.Clamp (num6 + num8, -1f, 1f);
		num7 = Mathf.Clamp (num7 + num8, -1f, 1f);
		AdjustFriction ();
		float t = Mathf.InverseLerp (3f, 1f, velocity.magnitude * Mathf.Abs (Vector3.Dot (velocity.normalized, base.transform.forward)));
		float torqueAmount = Mathf.Lerp (moveForceMax, turnForce, t);
		SetMotorTorque (num6, rightSide: false, torqueAmount);
		SetMotorTorque (num7, rightSide: true, torqueAmount);
	}

	public void ApplyBrakes (float amount)
	{
		ApplyBrakeTorque (amount, rightSide: true);
		ApplyBrakeTorque (amount, rightSide: false);
	}

	public float GetMotorTorque (bool rightSide)
	{
		float num = 0f;
		WheelCollider[] array = (rightSide ? rightWheels : leftWheels);
		foreach (WheelCollider wheelCollider in array) {
			num += wheelCollider.motorTorque;
		}
		return num / (float)rightWheels.Length;
	}

	public void SetMotorTorque (float newThrottle, bool rightSide, float torqueAmount)
	{
		newThrottle = Mathf.Clamp (newThrottle, -1f, 1f);
		float motorTorque = torqueAmount * newThrottle;
		WheelCollider[] array = (rightSide ? rightWheels : leftWheels);
		foreach (WheelCollider wheelCollider in array) {
			wheelCollider.motorTorque = motorTorque;
		}
	}

	public void ApplyBrakeTorque (float amount, bool rightSide)
	{
		WheelCollider[] array = (rightSide ? rightWheels : leftWheels);
		foreach (WheelCollider wheelCollider in array) {
			wheelCollider.brakeTorque = brakeForce * amount;
		}
	}

	public void AdjustFriction ()
	{
	}
}
