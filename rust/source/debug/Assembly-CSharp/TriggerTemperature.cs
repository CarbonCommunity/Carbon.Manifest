using ConVar;
using UnityEngine;

public class TriggerTemperature : TriggerBase
{
	public float Temperature = 50f;

	public float triggerSize;

	public float minSize = 0f;

	public bool sunlightBlocker = false;

	public float sunlightBlockAmount;

	[Range (0f, 24f)]
	public float blockMinHour = 8.5f;

	[Range (0f, 24f)]
	public float blockMaxHour = 18.5f;

	private void OnValidate ()
	{
		SphereCollider component = GetComponent<SphereCollider> ();
		if (component != null) {
			triggerSize = GetComponent<SphereCollider> ().radius * base.transform.localScale.y;
			return;
		}
		BoxCollider component2 = GetComponent<BoxCollider> ();
		Vector3 v = Vector3.Scale (component2.size, base.transform.localScale);
		triggerSize = v.Max () * 0.5f;
	}

	public float WorkoutTemperature (Vector3 position, float oldTemperature)
	{
		if (sunlightBlocker) {
			float time = Env.time;
			if (time >= blockMinHour && time <= blockMaxHour) {
				Vector3 position2 = TOD_Sky.Instance.Components.SunTransform.position;
				if (!GamePhysics.LineOfSight (position, position2, 256)) {
					return oldTemperature - sunlightBlockAmount;
				}
			}
			return oldTemperature;
		}
		float value = Vector3.Distance (base.gameObject.transform.position, position);
		float t = Mathf.InverseLerp (triggerSize, minSize, value);
		return Mathf.Lerp (oldTemperature, Temperature, t);
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
		return baseEntity.gameObject;
	}
}
