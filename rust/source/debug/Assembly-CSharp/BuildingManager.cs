#define ENABLE_PROFILER
using ConVar;
using UnityEngine.AI;
using UnityEngine.Profiling;

public abstract class BuildingManager
{
	public class Building
	{
		public uint ID;

		public ListHashSet<BuildingPrivlidge> buildingPrivileges = new ListHashSet<BuildingPrivlidge> ();

		public ListHashSet<BuildingBlock> buildingBlocks = new ListHashSet<BuildingBlock> ();

		public ListHashSet<DecayEntity> decayEntities = new ListHashSet<DecayEntity> ();

		public NavMeshObstacle buildingNavMeshObstacle;

		public ListHashSet<NavMeshObstacle> navmeshCarvers;

		public bool isNavMeshCarvingDirty = false;

		public bool isNavMeshCarveOptimized = false;

		public bool IsEmpty ()
		{
			if (HasBuildingPrivileges ()) {
				return false;
			}
			if (HasBuildingBlocks ()) {
				return false;
			}
			if (HasDecayEntities ()) {
				return false;
			}
			return true;
		}

		public BuildingPrivlidge GetDominatingBuildingPrivilege ()
		{
			BuildingPrivlidge buildingPrivlidge = null;
			if (HasBuildingPrivileges ()) {
				for (int i = 0; i < buildingPrivileges.Count; i++) {
					BuildingPrivlidge buildingPrivlidge2 = buildingPrivileges [i];
					if (!(buildingPrivlidge2 == null) && buildingPrivlidge2.IsOlderThan (buildingPrivlidge)) {
						buildingPrivlidge = buildingPrivlidge2;
					}
				}
			}
			return buildingPrivlidge;
		}

		public bool HasBuildingPrivileges ()
		{
			return buildingPrivileges != null && buildingPrivileges.Count > 0;
		}

		public bool HasBuildingBlocks ()
		{
			return buildingBlocks != null && buildingBlocks.Count > 0;
		}

		public bool HasDecayEntities ()
		{
			return decayEntities != null && decayEntities.Count > 0;
		}

		public void AddBuildingPrivilege (BuildingPrivlidge ent)
		{
			if (!(ent == null) && !buildingPrivileges.Contains (ent)) {
				buildingPrivileges.Add (ent);
			}
		}

		public void RemoveBuildingPrivilege (BuildingPrivlidge ent)
		{
			if (!(ent == null)) {
				buildingPrivileges.Remove (ent);
			}
		}

		public void AddBuildingBlock (BuildingBlock ent)
		{
			if (ent == null || buildingBlocks.Contains (ent)) {
				return;
			}
			buildingBlocks.Add (ent);
			if (!AI.nav_carve_use_building_optimization) {
				return;
			}
			NavMeshObstacle component = ent.GetComponent<NavMeshObstacle> ();
			if (component != null) {
				isNavMeshCarvingDirty = true;
				if (navmeshCarvers == null) {
					navmeshCarvers = new ListHashSet<NavMeshObstacle> ();
				}
				navmeshCarvers.Add (component);
			}
		}

		public void RemoveBuildingBlock (BuildingBlock ent)
		{
			if (ent == null) {
				return;
			}
			buildingBlocks.Remove (ent);
			if (!AI.nav_carve_use_building_optimization || navmeshCarvers == null) {
				return;
			}
			NavMeshObstacle component = ent.GetComponent<NavMeshObstacle> ();
			if (!(component != null)) {
				return;
			}
			navmeshCarvers.Remove (component);
			if (navmeshCarvers.Count == 0) {
				navmeshCarvers = null;
			}
			isNavMeshCarvingDirty = true;
			if (navmeshCarvers == null) {
				Building building = ent.GetBuilding ();
				if (building != null) {
					int ticks = 2;
					server.UpdateNavMeshCarver (building, ref ticks, 0);
				}
			}
		}

		public void AddDecayEntity (DecayEntity ent)
		{
			if (!(ent == null) && !decayEntities.Contains (ent)) {
				decayEntities.Add (ent);
			}
		}

		public void RemoveDecayEntity (DecayEntity ent)
		{
			if (!(ent == null)) {
				decayEntities.Remove (ent);
			}
		}

		public void Add (DecayEntity ent)
		{
			Profiler.BeginSample ("BuildingManager.Add");
			AddDecayEntity (ent);
			AddBuildingBlock (ent as BuildingBlock);
			AddBuildingPrivilege (ent as BuildingPrivlidge);
			Profiler.EndSample ();
		}

		public void Remove (DecayEntity ent)
		{
			Profiler.BeginSample ("BuildingManager.Remove");
			RemoveDecayEntity (ent);
			RemoveBuildingBlock (ent as BuildingBlock);
			RemoveBuildingPrivilege (ent as BuildingPrivlidge);
			Profiler.EndSample ();
		}

		public void Dirty ()
		{
			BuildingPrivlidge dominatingBuildingPrivilege = GetDominatingBuildingPrivilege ();
			if (dominatingBuildingPrivilege != null) {
				dominatingBuildingPrivilege.BuildingDirty ();
			}
		}
	}

	public static ServerBuildingManager server = new ServerBuildingManager ();

	protected ListHashSet<DecayEntity> decayEntities = new ListHashSet<DecayEntity> ();

	protected ListDictionary<uint, Building> buildingDictionary = new ListDictionary<uint, Building> ();

	public Building GetBuilding (uint buildingID)
	{
		Building val = null;
		buildingDictionary.TryGetValue (buildingID, out val);
		return val;
	}

	public void Add (DecayEntity ent)
	{
		if (ent.buildingID == 0) {
			if (!decayEntities.Contains (ent)) {
				decayEntities.Add (ent);
			}
			return;
		}
		Building building = GetBuilding (ent.buildingID);
		if (building == null) {
			building = CreateBuilding (ent.buildingID);
			buildingDictionary.Add (ent.buildingID, building);
		}
		building.Add (ent);
		building.Dirty ();
	}

	public void Remove (DecayEntity ent)
	{
		if (ent.buildingID == 0) {
			decayEntities.Remove (ent);
			return;
		}
		Building building = GetBuilding (ent.buildingID);
		if (building != null) {
			building.Remove (ent);
			if (building.IsEmpty ()) {
				buildingDictionary.Remove (ent.buildingID);
				DisposeBuilding (ref building);
			} else {
				building.Dirty ();
			}
		}
	}

	public void Clear ()
	{
		buildingDictionary.Clear ();
	}

	protected abstract Building CreateBuilding (uint id);

	protected abstract void DisposeBuilding (ref Building building);
}
