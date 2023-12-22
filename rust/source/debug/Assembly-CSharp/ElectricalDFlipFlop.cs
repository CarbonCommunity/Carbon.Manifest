using System;
using UnityEngine;

public class ElectricalDFlipFlop : IOEntity
{
	[NonSerialized]
	private int setAmount = 0;

	[NonSerialized]
	private int resetAmount = 0;

	[NonSerialized]
	private int toggleAmount = 0;

	public override void UpdateHasPower (int inputAmount, int inputSlot)
	{
		if (inputSlot == 0) {
			base.UpdateHasPower (inputAmount, inputSlot);
		}
	}

	public bool GetDesiredState ()
	{
		if (setAmount > 0 && resetAmount == 0) {
			return true;
		}
		if (setAmount > 0 && resetAmount > 0) {
			return true;
		}
		if (setAmount == 0 && resetAmount > 0) {
			return false;
		}
		if (toggleAmount > 0) {
			return !IsOn ();
		}
		if (setAmount == 0 && resetAmount == 0) {
			return IsOn ();
		}
		return false;
	}

	public void UpdateState ()
	{
		if (IsPowered ()) {
			bool flag = IsOn ();
			bool desiredState = GetDesiredState ();
			SetFlag (Flags.On, desiredState);
			if (flag != IsOn ()) {
				MarkDirtyForceUpdateOutputs ();
			}
		}
	}

	public override void UpdateFromInput (int inputAmount, int inputSlot)
	{
		switch (inputSlot) {
		case 1:
			setAmount = inputAmount;
			UpdateState ();
			break;
		case 2:
			resetAmount = inputAmount;
			UpdateState ();
			break;
		case 3:
			toggleAmount = inputAmount;
			UpdateState ();
			break;
		case 0:
			base.UpdateFromInput (inputAmount, inputSlot);
			UpdateState ();
			break;
		}
	}

	public override int GetPassthroughAmount (int outputSlot = 0)
	{
		return base.GetPassthroughAmount (outputSlot);
	}

	public override void UpdateOutputs ()
	{
		if (ShouldUpdateOutputs () && ensureOutputsUpdated) {
			int num = Mathf.Max (0, currentEnergy - 1);
			if ((Object)(object)outputs [0].connectedTo.Get () != (Object)null) {
				outputs [0].connectedTo.Get ().UpdateFromInput (IsOn () ? num : 0, outputs [0].connectedToSlot);
			}
			if ((Object)(object)outputs [1].connectedTo.Get () != (Object)null) {
				outputs [1].connectedTo.Get ().UpdateFromInput ((!IsOn ()) ? num : 0, outputs [1].connectedToSlot);
			}
		}
	}
}
