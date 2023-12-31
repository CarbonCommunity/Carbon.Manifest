using System.Collections.Generic;
using Facepunch;
using UnityEngine;

public class SocketMod_PlantCheck : SocketMod
{
	public float sphereRadius = 1f;

	public LayerMask layerMask;

	public QueryTriggerInteraction queryTriggers;

	public bool wantsCollide = false;

	private void OnDrawGizmosSelected ()
	{
		Gizmos.matrix = base.transform.localToWorldMatrix;
		Gizmos.color = (wantsCollide ? new Color (0f, 1f, 0f, 0.7f) : new Color (1f, 0f, 0f, 0.7f));
		Gizmos.DrawSphere (Vector3.zero, sphereRadius);
	}

	public override bool DoCheck (Construction.Placement place)
	{
		Vector3 position = place.position + place.rotation * worldPosition;
		List<BaseEntity> obj = Pool.GetList<BaseEntity> ();
		Vis.Entities (position, sphereRadius, obj, layerMask.value, queryTriggers);
		foreach (BaseEntity item in obj) {
			GrowableEntity component = item.GetComponent<GrowableEntity> ();
			if ((bool)component && wantsCollide) {
				Pool.FreeList (ref obj);
				return true;
			}
			if ((bool)component && !wantsCollide) {
				Pool.FreeList (ref obj);
				return false;
			}
		}
		Pool.FreeList (ref obj);
		return !wantsCollide;
	}
}
