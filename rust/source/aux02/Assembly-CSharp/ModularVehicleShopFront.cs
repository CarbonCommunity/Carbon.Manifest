using UnityEngine;

public class ModularVehicleShopFront : ShopFront
{
	[SerializeField]
	private float maxUseDistance = 1.5f;

	public override bool CanBeLooted (BasePlayer player)
	{
		if (WithinUseDistance (player)) {
			return base.CanBeLooted (player);
		}
		return false;
	}

	private bool WithinUseDistance (BasePlayer player)
	{
		return Distance (player.eyes.position) <= maxUseDistance;
	}
}
