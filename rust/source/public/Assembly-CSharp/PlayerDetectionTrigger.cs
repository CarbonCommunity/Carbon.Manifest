using UnityEngine;

public class PlayerDetectionTrigger : TriggerBase
{
	public BaseDetector myDetector;

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

	internal override void OnObjects ()
	{
		base.OnObjects ();
		myDetector.OnObjects ();
	}

	internal override void OnEmpty ()
	{
		base.OnEmpty ();
		myDetector.OnEmpty ();
	}
}
