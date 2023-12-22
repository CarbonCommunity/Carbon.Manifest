using UnityEngine;

public class BoxStorage : StorageContainer
{
	public override Vector3 GetDropPosition ()
	{
		return ClosestPoint (base.GetDropPosition () + base.LastAttackedDir * 10f);
	}

	public override bool SupportsChildDeployables ()
	{
		return true;
	}

	public override bool CanPickup (BasePlayer player)
	{
		return children.Count == 0 && base.CanPickup (player);
	}
}
