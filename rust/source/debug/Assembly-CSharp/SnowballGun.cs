public class SnowballGun : BaseProjectile
{
	public ItemDefinition OverrideProjectile = null;

	protected override ItemDefinition PrimaryMagazineAmmo => (OverrideProjectile != null) ? OverrideProjectile : base.PrimaryMagazineAmmo;

	protected override bool CanRefundAmmo => false;

	protected override void ReloadMagazine (int desiredAmount = -1)
	{
		BasePlayer ownerPlayer = GetOwnerPlayer ();
		if ((bool)ownerPlayer) {
			desiredAmount = 1;
			primaryMagazine.Reload (ownerPlayer, desiredAmount, CanRefundAmmo);
			primaryMagazine.contents = primaryMagazine.capacity;
			primaryMagazine.ammoType = OverrideProjectile;
			SendNetworkUpdateImmediate ();
			ItemManager.DoRemoves ();
			ownerPlayer.inventory.ServerUpdate (0f);
		}
	}
}
