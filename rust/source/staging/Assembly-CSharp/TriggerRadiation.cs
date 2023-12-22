using UnityEngine;

public class TriggerRadiation : TriggerBase
{
	public enum RadiationTier
	{
		MINIMAL,
		LOW,
		MEDIUM,
		HIGH,
		NONE
	}

	public RadiationTier radiationTier = RadiationTier.LOW;

	public bool BypassArmor;

	public float RadiationAmountOverride;

	public float falloff = 0.1f;

	private SphereCollider sphereCollider;

	private float GetRadiationSize ()
	{
		if (!sphereCollider) {
			sphereCollider = GetComponent<SphereCollider> ();
		}
		return sphereCollider.radius * base.transform.localScale.Max ();
	}

	private float GetRadiationAmount ()
	{
		if (RadiationAmountOverride > 0f) {
			return RadiationAmountOverride;
		}
		if (radiationTier == RadiationTier.NONE) {
			return 0f;
		}
		if (radiationTier == RadiationTier.MINIMAL) {
			return 2f;
		}
		if (radiationTier == RadiationTier.LOW) {
			return 10f;
		}
		if (radiationTier == RadiationTier.MEDIUM) {
			return 25f;
		}
		if (radiationTier == RadiationTier.HIGH) {
			return 51f;
		}
		return 1f;
	}

	public float GetRadiation (Vector3 position, float radProtection)
	{
		float radiationSize = GetRadiationSize ();
		float radiationAmount = GetRadiationAmount ();
		float num = Mathf.InverseLerp (value: Vector3.Distance (base.gameObject.transform.position, position), a: radiationSize, b: radiationSize * (1f - falloff));
		float num2 = radiationAmount;
		if (!BypassArmor) {
			num2 = Mathf.Clamp (radiationAmount - radProtection, 0f, radiationAmount);
		}
		return num2 * num;
	}

	internal override GameObject InterestedInObject (GameObject obj)
	{
		obj = base.InterestedInObject (obj);
		if (obj == null) {
			return null;
		}
		BaseEntity baseEntity = obj.ToBaseEntity ();
		if (baseEntity == null) {
			return null;
		}
		if (baseEntity.isClient) {
			return null;
		}
		if (!(baseEntity is BaseCombatEntity)) {
			return null;
		}
		return baseEntity.gameObject;
	}

	public void OnDrawGizmosSelected ()
	{
		float radiationSize = GetRadiationSize ();
		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere (base.transform.position, radiationSize);
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere (base.transform.position, radiationSize * (1f - falloff));
	}
}
