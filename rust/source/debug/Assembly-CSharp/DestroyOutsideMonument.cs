using System.Collections.Generic;
using UnityEngine;

public class DestroyOutsideMonument : FacepunchBehaviour
{
	[SerializeField]
	private BaseCombatEntity baseCombatEntity;

	[SerializeField]
	private float checkEvery = 10f;

	private MonumentInfo ourMonument;

	private Vector3 OurPos => baseCombatEntity.transform.position;

	protected void OnEnable ()
	{
		if (ourMonument == null) {
			ourMonument = GetOurMonument ();
		}
		if (ourMonument == null) {
			DoOutsideMonument ();
		} else {
			InvokeRandomized (CheckPosition, checkEvery, checkEvery, checkEvery * 0.1f);
		}
	}

	protected void OnDisable ()
	{
		CancelInvoke (CheckPosition);
	}

	private MonumentInfo GetOurMonument ()
	{
		List<MonumentInfo> monuments = TerrainMeta.Path.Monuments;
		foreach (MonumentInfo item in monuments) {
			if (item.IsInBounds (OurPos)) {
				return item;
			}
		}
		return null;
	}

	private void CheckPosition ()
	{
		if (ourMonument == null) {
			DoOutsideMonument ();
		}
		if (!ourMonument.IsInBounds (OurPos)) {
			DoOutsideMonument ();
		}
	}

	private void DoOutsideMonument ()
	{
		baseCombatEntity.Kill (BaseNetworkable.DestroyMode.Gib);
	}
}
