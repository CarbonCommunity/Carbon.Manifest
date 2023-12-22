using UnityEngine;

public class TriggerSubmarineMoonpool : TriggerBase, IServerComponent
{
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
		if (!baseEntity.isServer || !(baseEntity is BaseSubmarine { gameObject: var result })) {
			return null;
		}
		return result;
	}

	internal override void OnEntityEnter (BaseEntity ent)
	{
		base.OnEntityEnter (ent);
		if (ent is BaseSubmarine baseSubmarine) {
			baseSubmarine.OnSurfacedInMoonpool ();
		}
	}
}
