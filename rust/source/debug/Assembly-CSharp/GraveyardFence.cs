using System.Collections.Generic;
using Facepunch;
using UnityEngine;

public class GraveyardFence : SimpleBuildingBlock
{
	public BoxCollider[] pillars;

	public override void ServerInit ()
	{
		base.ServerInit ();
		UpdatePillars ();
	}

	public override void DestroyShared ()
	{
		base.DestroyShared ();
		List<GraveyardFence> obj = Pool.GetList<GraveyardFence> ();
		Vis.Entities (base.transform.position, 5f, obj, 2097152);
		foreach (GraveyardFence item in obj) {
			item.UpdatePillars ();
		}
		Pool.FreeList (ref obj);
	}

	public virtual void UpdatePillars ()
	{
		BoxCollider[] array = pillars;
		foreach (BoxCollider boxCollider in array) {
			boxCollider.gameObject.SetActive (value: true);
			Vector3 center = boxCollider.transform.TransformPoint (boxCollider.center);
			Collider[] array2 = Physics.OverlapBox (center, boxCollider.size * 0.5f, boxCollider.transform.rotation, 2097152);
			Collider[] array3 = array2;
			foreach (Collider collider in array3) {
				if (collider.CompareTag ("Usable Auxiliary")) {
					BaseEntity baseEntity = collider.gameObject.ToBaseEntity ();
					if (!(baseEntity == null) && !EqualNetID (baseEntity) && collider != boxCollider) {
						boxCollider.gameObject.SetActive (value: false);
					}
				}
			}
		}
	}
}
