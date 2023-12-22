using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using ConVar;
using Rust;
using Rust.Ai;
using UnityEngine;
using UnityEngine.AI;

public class MonumentNavMesh : FacepunchBehaviour, IServerComponent
{
	public int NavMeshAgentTypeIndex = 0;

	[Tooltip ("The default area associated with the NavMeshAgent index.")]
	public string DefaultAreaName = "HumanNPC";

	[Tooltip ("How many cells to use squared")]
	public int CellCount = 1;

	[Tooltip ("The size of each cell for async object gathering")]
	public int CellSize = 80;

	public int Height = 100;

	public float NavmeshResolutionModifier = 0.5f;

	[Tooltip ("Use the bounds specified in editor instead of generating it from cellsize * cellcount")]
	public bool overrideAutoBounds = false;

	[Tooltip ("Bounds which are auto calculated from CellSize * CellCount")]
	public Bounds Bounds;

	public NavMeshData NavMeshData;

	public NavMeshDataInstance NavMeshDataInstance;

	public LayerMask LayerMask;

	public NavMeshCollectGeometry NavMeshCollectGeometry;

	public bool forceCollectTerrain = false;

	public bool shouldNotifyAIZones = true;

	public Transform CustomNavMeshRoot;

	[ServerVar]
	public static bool use_baked_terrain_mesh = true;

	private List<NavMeshBuildSource> sources;

	private AsyncOperation BuildingOperation;

	private bool HasBuildOperationStarted = false;

	private Stopwatch BuildTimer = new Stopwatch ();

	private int defaultArea;

	private int agentTypeId;

	public bool IsBuilding {
		get {
			if (!HasBuildOperationStarted || BuildingOperation != null) {
				return true;
			}
			return false;
		}
	}

	private void OnEnable ()
	{
		agentTypeId = NavMesh.GetSettingsByIndex (NavMeshAgentTypeIndex).agentTypeID;
		NavMeshData = new NavMeshData (agentTypeId);
		sources = new List<NavMeshBuildSource> ();
		defaultArea = NavMesh.GetAreaFromName (DefaultAreaName);
		InvokeRepeating (FinishBuildingNavmesh, 0f, 1f);
	}

	private void OnDisable ()
	{
		if (!Rust.Application.isQuitting) {
			CancelInvoke (FinishBuildingNavmesh);
			NavMeshDataInstance.Remove ();
		}
	}

	[ContextMenu ("Update Monument Nav Mesh")]
	public void UpdateNavMeshAsync ()
	{
		if (!HasBuildOperationStarted && !AiManager.nav_disable && AI.npc_enable) {
			float realtimeSinceStartup = UnityEngine.Time.realtimeSinceStartup;
			UnityEngine.Debug.Log ("Starting Monument Navmesh Build with " + sources.Count + " sources");
			NavMeshBuildSettings settingsByIndex = NavMesh.GetSettingsByIndex (NavMeshAgentTypeIndex);
			settingsByIndex.overrideVoxelSize = true;
			settingsByIndex.voxelSize *= NavmeshResolutionModifier;
			BuildingOperation = NavMeshBuilder.UpdateNavMeshDataAsync (NavMeshData, settingsByIndex, sources, Bounds);
			BuildTimer.Reset ();
			BuildTimer.Start ();
			HasBuildOperationStarted = true;
			float num = UnityEngine.Time.realtimeSinceStartup - realtimeSinceStartup;
			if (num > 0.1f) {
				UnityEngine.Debug.LogWarning ("Calling UpdateNavMesh took " + num);
			}
			if (shouldNotifyAIZones) {
				NotifyInformationZonesOfCompletion ();
			}
		}
	}

	public IEnumerator UpdateNavMeshAndWait ()
	{
		if (HasBuildOperationStarted || AiManager.nav_disable || !AI.npc_enable) {
			yield break;
		}
		HasBuildOperationStarted = false;
		Bounds.center = base.transform.position;
		if (!overrideAutoBounds) {
			Bounds.size = new Vector3 (CellSize * CellCount, Height, CellSize * CellCount);
		}
		IEnumerator collectSourcesAsync = NavMeshTools.CollectSourcesAsync (Bounds, LayerMask, NavMeshCollectGeometry, defaultArea, use_baked_terrain_mesh && !forceCollectTerrain, CellSize, sources, AppendModifierVolumes, UpdateNavMeshAsync, CustomNavMeshRoot);
		if (AiManager.nav_wait) {
			yield return collectSourcesAsync;
		} else {
			StartCoroutine (collectSourcesAsync);
		}
		if (!AiManager.nav_wait) {
			UnityEngine.Debug.Log ("nav_wait is false, so we're not waiting for the navmesh to finish generating. This might cause your server to sputter while it's generating.");
			yield break;
		}
		int lastPct = 0;
		while (!HasBuildOperationStarted) {
			yield return CoroutineEx.waitForSecondsRealtime (0.25f);
		}
		while (BuildingOperation != null) {
			int pctDone = (int)(BuildingOperation.progress * 100f);
			if (lastPct != pctDone) {
				UnityEngine.Debug.LogFormat ("{0}%", pctDone);
				lastPct = pctDone;
			}
			yield return CoroutineEx.waitForSecondsRealtime (0.25f);
			FinishBuildingNavmesh ();
		}
	}

	public void NotifyInformationZonesOfCompletion ()
	{
		foreach (AIInformationZone zone in AIInformationZone.zones) {
			zone.NavmeshBuildingComplete ();
		}
	}

	private void AppendModifierVolumes (List<NavMeshBuildSource> sources)
	{
		List<NavMeshModifierVolume> activeModifiers = NavMeshModifierVolume.activeModifiers;
		foreach (NavMeshModifierVolume item2 in activeModifiers) {
			if (((int)LayerMask & (1 << item2.gameObject.layer)) != 0 && item2.AffectsAgentType (agentTypeId)) {
				Vector3 vector = item2.transform.TransformPoint (item2.center);
				if (Bounds.Contains (vector)) {
					Vector3 lossyScale = item2.transform.lossyScale;
					Vector3 size = new Vector3 (item2.size.x * Mathf.Abs (lossyScale.x), item2.size.y * Mathf.Abs (lossyScale.y), item2.size.z * Mathf.Abs (lossyScale.z));
					NavMeshBuildSource item = default(NavMeshBuildSource);
					item.shape = NavMeshBuildSourceShape.ModifierBox;
					item.transform = Matrix4x4.TRS (vector, item2.transform.rotation, Vector3.one);
					item.size = size;
					item.area = item2.area;
					sources.Add (item);
				}
			}
		}
	}

	public void FinishBuildingNavmesh ()
	{
		if (BuildingOperation != null && BuildingOperation.isDone) {
			if (!NavMeshDataInstance.valid) {
				NavMeshDataInstance = NavMesh.AddNavMeshData (NavMeshData);
			}
			UnityEngine.Debug.Log ($"Monument Navmesh Build took {BuildTimer.Elapsed.TotalSeconds:0.00} seconds");
			BuildingOperation = null;
		}
	}

	public void OnDrawGizmosSelected ()
	{
		Gizmos.color = Color.magenta * new Color (1f, 1f, 1f, 0.5f);
		Gizmos.DrawCube (base.transform.position + Bounds.center, Bounds.size);
	}
}
