using UnityEngine;

[Factory ("bradley")]
public class Bradley : ConsoleSystem
{
	[ServerVar]
	public static float respawnDelayMinutes = 60f;

	[ServerVar]
	public static float respawnDelayVariance = 1f;

	[ServerVar]
	public static bool enabled = true;

	[ServerVar]
	public static void quickrespawn (Arg arg)
	{
		BasePlayer basePlayer = arg.Player ();
		if (!basePlayer) {
			return;
		}
		BradleySpawner singleton = BradleySpawner.singleton;
		if (singleton == null) {
			Debug.LogWarning ("No Spawner");
			return;
		}
		if ((bool)singleton.spawned) {
			singleton.spawned.Kill ();
		}
		singleton.spawned = null;
		singleton.DoRespawn ();
	}
}
