public class ItemModConditionInWater : ItemMod
{
	public bool requiredState = false;

	public override bool Passes (Item item)
	{
		BasePlayer ownerPlayer = item.GetOwnerPlayer ();
		if (ownerPlayer == null) {
			return false;
		}
		bool flag = ownerPlayer.IsHeadUnderwater ();
		return flag == requiredState;
	}
}
