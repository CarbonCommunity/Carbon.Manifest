using UnityEngine;

public class VehicleSpawnPoint : SpaceCheckingSpawnPoint
{
	public override void ObjectSpawned (SpawnPointInstance instance)
	{
		base.ObjectSpawned (instance);
		BaseEntity baseEntity = instance.gameObject.ToBaseEntity ();
		AddStartingFuel (baseEntity as BaseVehicle);
	}

	public static void AddStartingFuel (BaseVehicle vehicle)
	{
		if (!(vehicle == null)) {
			vehicle.GetFuelSystem ()?.AddStartingFuel (vehicle.StartingFuelUnits ());
		}
	}
}
