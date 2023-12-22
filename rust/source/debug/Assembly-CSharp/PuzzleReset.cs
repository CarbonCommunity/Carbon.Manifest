#define ENABLE_PROFILER
using System.Collections.Generic;
using ConVar;
using Facepunch;
using Rust;
using UnityEngine;
using UnityEngine.Profiling;

public class PuzzleReset : FacepunchBehaviour
{
	public SpawnGroup[] respawnGroups;

	public IOEntity[] resetEnts;

	public GameObject[] resetObjects;

	public bool playersBlockReset = false;

	public bool CheckSleepingAIZForPlayers = false;

	public float playerDetectionRadius;

	public float playerHeightDetectionMinMax = -1f;

	public Transform playerDetectionOrigin;

	public float timeBetweenResets = 30f;

	public bool scaleWithServerPopulation = false;

	[HideInInspector]
	public Vector3[] resetPositions;

	public bool broadcastResetMessage = false;

	public Translate.Phrase resetPhrase;

	private AIInformationZone zone = null;

	private float resetTimeElapsed = 0f;

	private float resetTickTime = 10f;

	public float GetResetSpacing ()
	{
		return timeBetweenResets * (scaleWithServerPopulation ? (1f - SpawnHandler.PlayerLerp (Spawn.min_rate, Spawn.max_rate)) : 1f);
	}

	public void Start ()
	{
		if (timeBetweenResets != float.PositiveInfinity) {
			ResetTimer ();
		}
	}

	public void ResetTimer ()
	{
		resetTimeElapsed = 0f;
		CancelInvoke (ResetTick);
		InvokeRandomized (ResetTick, Random.Range (0f, 1f), resetTickTime, 0.5f);
	}

	public bool PassesResetCheck ()
	{
		if (playersBlockReset) {
			if (CheckSleepingAIZForPlayers) {
				return AIZSleeping ();
			}
			return !PlayersWithinDistance ();
		}
		return true;
	}

	private bool AIZSleeping ()
	{
		Profiler.BeginSample ("PuzzleReset.AIZSleeping");
		if (zone != null) {
			if (!zone.PointInside (base.transform.position)) {
				zone = AIInformationZone.GetForPoint (base.transform.position);
			}
		} else {
			zone = AIInformationZone.GetForPoint (base.transform.position);
		}
		Profiler.EndSample ();
		if (zone == null) {
			return false;
		}
		return zone.Sleeping;
	}

	private bool PlayersWithinDistance ()
	{
		return AnyPlayersWithinDistance (playerDetectionOrigin, playerDetectionRadius);
	}

	public static bool AnyPlayersWithinDistance (Transform origin, float radius)
	{
		Profiler.BeginSample ("PuzzleReset.PlayersWithinDistance");
		foreach (BasePlayer activePlayer in BasePlayer.activePlayerList) {
			if (!activePlayer.IsSleeping () && activePlayer.IsAlive ()) {
				float num = Vector3.Distance (activePlayer.transform.position, origin.position);
				if (num < radius) {
					Profiler.EndSample ();
					return true;
				}
			}
		}
		Profiler.EndSample ();
		return false;
	}

	public void ResetTick ()
	{
		Profiler.BeginSample ("PuzzleReset.ResetTick");
		if (PassesResetCheck ()) {
			resetTimeElapsed += resetTickTime;
		}
		if (resetTimeElapsed > GetResetSpacing ()) {
			resetTimeElapsed = 0f;
			DoReset ();
		}
		Profiler.EndSample ();
	}

	public void CleanupSleepers ()
	{
		if (playerDetectionOrigin == null || BasePlayer.sleepingPlayerList == null) {
			return;
		}
		for (int num = BasePlayer.sleepingPlayerList.Count - 1; num >= 0; num--) {
			BasePlayer basePlayer = BasePlayer.sleepingPlayerList [num];
			if (!(basePlayer == null) && basePlayer.IsSleeping ()) {
				float num2 = Vector3.Distance (basePlayer.transform.position, playerDetectionOrigin.position);
				if (num2 <= playerDetectionRadius) {
					basePlayer.Hurt (1000f, DamageType.Suicide, basePlayer, useProtection: false);
				}
			}
		}
	}

	public void DoReset ()
	{
		Profiler.BeginSample ("PuzzleReset.DoReset");
		CleanupSleepers ();
		IOEntity component = GetComponent<IOEntity> ();
		if (component != null) {
			ResetIOEntRecursive (component, UnityEngine.Time.frameCount);
			component.MarkDirty ();
		} else if (resetPositions != null) {
			Vector3[] array = resetPositions;
			foreach (Vector3 position in array) {
				Vector3 position2 = base.transform.TransformPoint (position);
				List<IOEntity> obj = Facepunch.Pool.GetList<IOEntity> ();
				Vis.Entities (position2, 0.5f, obj, 1235288065, QueryTriggerInteraction.Ignore);
				foreach (IOEntity item in obj) {
					if (item.IsRootEntity () && item.isServer) {
						ResetIOEntRecursive (item, UnityEngine.Time.frameCount);
						item.MarkDirty ();
					}
				}
				Facepunch.Pool.FreeList (ref obj);
			}
		}
		List<SpawnGroup> obj2 = Facepunch.Pool.GetList<SpawnGroup> ();
		Vis.Components (base.transform.position, 1f, obj2, 262144);
		foreach (SpawnGroup item2 in obj2) {
			if (!(item2 == null)) {
				item2.Clear ();
				item2.DelayedSpawn ();
			}
		}
		Facepunch.Pool.FreeList (ref obj2);
		GameObject[] array2 = resetObjects;
		foreach (GameObject gameObject in array2) {
			if (gameObject != null) {
				gameObject.SendMessage ("OnPuzzleReset", SendMessageOptions.DontRequireReceiver);
			}
		}
		if (broadcastResetMessage) {
			foreach (BasePlayer activePlayer in BasePlayer.activePlayerList) {
				if (!activePlayer.IsNpc && activePlayer.IsConnected) {
					activePlayer.ShowToast (GameTip.Styles.Server_Event, resetPhrase);
				}
			}
		}
		Profiler.EndSample ();
	}

	public static void ResetIOEntRecursive (IOEntity target, int resetIndex)
	{
		if (target.lastResetIndex == resetIndex) {
			return;
		}
		target.lastResetIndex = resetIndex;
		target.ResetIOState ();
		IOEntity.IOSlot[] outputs = target.outputs;
		foreach (IOEntity.IOSlot iOSlot in outputs) {
			if (iOSlot.connectedTo.Get () != null && iOSlot.connectedTo.Get () != target) {
				ResetIOEntRecursive (iOSlot.connectedTo.Get (), resetIndex);
			}
		}
	}
}
