using UnityEngine;

public class ElectricalHeater : IOEntity
{
	public float fadeDuration = 1f;

	public Light sourceLight;

	public GrowableHeatSource growableHeatSource;

	public override int ConsumptionAmount ()
	{
		return 3;
	}

	public override void ResetState ()
	{
		base.ResetState ();
	}

	public override void OnFlagsChanged (Flags old, Flags next)
	{
		base.OnFlagsChanged (old, next);
		bool flag = next.HasFlag (Flags.Reserved8);
		if (old.HasFlag (Flags.Reserved8) != flag && growableHeatSource != null) {
			growableHeatSource.ForceUpdateGrowablesInRange ();
		}
	}

	public override void OnKilled (HitInfo info)
	{
		base.OnKilled (info);
		if (growableHeatSource != null) {
			growableHeatSource.ForceUpdateGrowablesInRange ();
		}
	}
}
