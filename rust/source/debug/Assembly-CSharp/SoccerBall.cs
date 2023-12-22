using UnityEngine;

public class SoccerBall : BaseCombatEntity
{
	[Header ("Soccer Ball")]
	[SerializeField]
	private Rigidbody rigidBody;

	[SerializeField]
	private float additionalForceMultiplier = 0.2f;

	[SerializeField]
	private float upForceMultiplier = 0.15f;

	[SerializeField]
	private DamageRenderer damageRenderer = null;

	[SerializeField]
	private float explosionForceMultiplier = 40f;

	[SerializeField]
	private float otherForceMultiplier = 10f;

	protected void OnCollisionEnter (Collision collision)
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		if (!base.isClient) {
			Vector3 impulse = collision.impulse;
			if (((Vector3)(ref impulse)).magnitude > 0f && (Object)(object)collision.collider.attachedRigidbody != (Object)null && !((Component)(object)collision.collider.attachedRigidbody).HasComponent<SoccerBall> ()) {
				Vector3 val = rigidBody.position - collision.collider.attachedRigidbody.position;
				impulse = collision.impulse;
				float magnitude = ((Vector3)(ref impulse)).magnitude;
				rigidBody.AddForce (val * magnitude * additionalForceMultiplier + Vector3.up * magnitude * upForceMultiplier, (ForceMode)1);
			}
		}
	}

	public override void Hurt (HitInfo info)
	{
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		if (!base.isClient) {
			float num = 0f;
			float[] types = info.damageTypes.types;
			foreach (float num2 in types) {
				num = (((int)num2 != 16 && (int)num2 != 22) ? (num + num2 * otherForceMultiplier) : (num + num2 * explosionForceMultiplier));
			}
			if (num > 3f) {
				rigidBody.AddExplosionForce (num, info.HitPositionWorld, 0.25f, 0.5f);
			}
			base.Hurt (info);
		}
	}
}
