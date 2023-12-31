using UnityEngine;

public class VehicleEngineController<TOwner> where TOwner : BaseVehicle, IEngineControllerUser
{
	public enum EngineState
	{
		Off,
		Starting,
		On
	}

	private readonly TOwner owner;

	private readonly bool isServer;

	private readonly float engineStartupTime;

	private readonly Transform waterloggedPoint;

	private readonly BaseEntity.Flags engineStartingFlag;

	public EngineState CurEngineState {
		get {
			if (owner.HasFlag (engineStartingFlag)) {
				return EngineState.Starting;
			}
			if (owner.HasFlag (BaseEntity.Flags.On)) {
				return EngineState.On;
			}
			return EngineState.Off;
		}
	}

	public bool IsOn => CurEngineState == EngineState.On;

	public bool IsOff => CurEngineState == EngineState.Off;

	public bool IsStarting => CurEngineState == EngineState.Starting;

	public bool IsStartingOrOn => CurEngineState != EngineState.Off;

	public EntityFuelSystem FuelSystem { get; private set; }

	public VehicleEngineController (TOwner owner, bool isServer, float engineStartupTime, GameObjectRef fuelStoragePrefab, Transform waterloggedPoint = null, BaseEntity.Flags engineStartingFlag = BaseEntity.Flags.Reserved1)
	{
		FuelSystem = new EntityFuelSystem (isServer, fuelStoragePrefab, owner.children);
		this.owner = owner;
		this.isServer = isServer;
		this.engineStartupTime = engineStartupTime;
		this.waterloggedPoint = waterloggedPoint;
		this.engineStartingFlag = engineStartingFlag;
	}

	public EngineState EngineStateFrom (BaseEntity.Flags flags)
	{
		if (flags.HasFlag (engineStartingFlag)) {
			return EngineState.Starting;
		}
		if (flags.HasFlag (BaseEntity.Flags.On)) {
			return EngineState.On;
		}
		return EngineState.Off;
	}

	public void TryStartEngine (BasePlayer player)
	{
		if (isServer && !owner.IsDead () && !IsStartingOrOn && player.net != null) {
			if (!CanRunEngine ()) {
				owner.OnEngineStartFailed ();
				return;
			}
			owner.SetFlag (engineStartingFlag, b: true);
			owner.SetFlag (BaseEntity.Flags.On, b: false);
			owner.Invoke (FinishStartingEngine, engineStartupTime);
		}
	}

	public void FinishStartingEngine ()
	{
		if (isServer && !owner.IsDead () && !IsOn) {
			owner.SetFlag (BaseEntity.Flags.On, b: true);
			owner.SetFlag (engineStartingFlag, b: false);
		}
	}

	public void StopEngine ()
	{
		if (isServer && !IsOff) {
			CancelEngineStart ();
			owner.SetFlag (BaseEntity.Flags.On, b: false);
			owner.SetFlag (engineStartingFlag, b: false);
		}
	}

	public void CheckEngineState ()
	{
		if (IsStartingOrOn && !CanRunEngine ()) {
			StopEngine ();
		}
	}

	public bool CanRunEngine ()
	{
		return owner.MeetsEngineRequirements () && FuelSystem.HasFuel () && !IsWaterlogged () && !owner.IsDead ();
	}

	public bool IsWaterlogged ()
	{
		return waterloggedPoint != null && WaterLevel.Test (waterloggedPoint.position, waves: true, volumes: true, owner);
	}

	public int TickFuel (float fuelPerSecond)
	{
		if (IsOn) {
			return FuelSystem.TryUseFuel (Time.fixedDeltaTime, fuelPerSecond);
		}
		return 0;
	}

	private void CancelEngineStart ()
	{
		if (CurEngineState == EngineState.Starting) {
			owner.CancelInvoke (FinishStartingEngine);
		}
	}
}
