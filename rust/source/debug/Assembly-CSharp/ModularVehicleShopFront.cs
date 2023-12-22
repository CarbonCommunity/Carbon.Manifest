using UnityEngine;

public class ModularVehicleShopFront : ShopFront
{
	[SerializeField]
	private float maxUseDistance = 1.5f;

	public override bool CanBeLooted (BasePlayer player)
	{
		return WithinUseDistance (player) && base.CanBeLooted (player);
	}

	private bool WithinUseDistance (BasePlayer player)
	{
		float num = Distance (player.eyes.position);
		return num <= maxUseDistance;
	}
}
