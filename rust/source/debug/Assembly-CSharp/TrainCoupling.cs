using UnityEngine;

public class TrainCoupling
{
	public readonly TrainCar owner;

	public readonly bool isFrontCoupling;

	public readonly TrainCouplingController controller;

	public readonly Transform couplingPoint;

	public readonly Transform couplingPivot;

	public readonly BaseEntity.Flags flag;

	public readonly bool isValid;

	public TimeSince timeSinceCouplingBlock = default(TimeSince);

	public bool IsCoupled => owner.HasFlag (flag);

	public bool IsUncoupled => !owner.HasFlag (flag);

	public TrainCoupling CoupledTo { get; private set; }

	public TrainCoupling (TrainCar owner, bool isFrontCoupling, TrainCouplingController controller)
		: this (owner, isFrontCoupling, controller, null, null, BaseEntity.Flags.Placeholder)
	{
	}

	public TrainCoupling (TrainCar owner, bool isFrontCoupling, TrainCouplingController controller, Transform couplingPoint, Transform couplingPivot, BaseEntity.Flags flag)
	{
		this.owner = owner;
		this.isFrontCoupling = isFrontCoupling;
		this.controller = controller;
		this.couplingPoint = couplingPoint;
		this.couplingPivot = couplingPivot;
		this.flag = flag;
		isValid = couplingPoint != null;
	}

	public bool IsCoupledTo (TrainCar them)
	{
		return CoupledTo != null && CoupledTo.owner == them;
	}

	public bool IsCoupledTo (TrainCoupling them)
	{
		return CoupledTo != null && CoupledTo == them;
	}

	public bool TryCouple (TrainCoupling theirCoupling, bool reflect)
	{
		if (!isValid) {
			return false;
		}
		if (CoupledTo == theirCoupling) {
			return true;
		}
		if (IsCoupled) {
			return false;
		}
		if (reflect && !theirCoupling.TryCouple (this, reflect: false)) {
			return false;
		}
		controller.OnPreCouplingChange ();
		CoupledTo = theirCoupling;
		owner.SetFlag (flag, b: true, recursive: false, networkupdate: false);
		owner.SendNetworkUpdate ();
		return true;
	}

	public void Uncouple (bool reflect)
	{
		if (!IsUncoupled) {
			if (reflect && CoupledTo != null) {
				CoupledTo.Uncouple (reflect: false);
			}
			controller.OnPreCouplingChange ();
			CoupledTo = null;
			owner.SetFlag (flag, b: false, recursive: false, networkupdate: false);
			owner.SendNetworkUpdate ();
			timeSinceCouplingBlock = 0f;
		}
	}

	public TrainCoupling GetOppositeCoupling ()
	{
		return isFrontCoupling ? controller.rearCoupling : controller.frontCoupling;
	}

	public bool TryGetCoupledToID (out NetworkableId id)
	{
		if (CoupledTo != null && CoupledTo.owner != null && CoupledTo.owner.IsValid ()) {
			id = CoupledTo.owner.net.ID;
			return true;
		}
		id = default(NetworkableId);
		return false;
	}
}
