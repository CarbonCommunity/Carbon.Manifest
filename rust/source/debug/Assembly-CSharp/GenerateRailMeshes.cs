using System.Collections.Generic;
using UnityEngine;

public class GenerateRailMeshes : ProceduralComponent
{
	public const float NormalSmoothing = 0f;

	public const bool SnapToTerrain = false;

	public Mesh RailMesh = null;

	public Mesh[] RailMeshes = null;

	public Material RailMaterial = null;

	public PhysicMaterial RailPhysicMaterial = null;

	public override bool RunOnCache => true;

	public override void Process (uint seed)
	{
		if (RailMeshes == null || RailMeshes.Length == 0) {
			RailMeshes = new Mesh[1] { RailMesh };
		}
		List<PathList> rails = TerrainMeta.Path.Rails;
		foreach (PathList item in rails) {
			foreach (PathList.MeshObject item2 in item.CreateMesh (RailMeshes, 0f, snapToTerrain: false, !item.Path.Circular && !item.Start, !item.Path.Circular && !item.End)) {
				GameObject gameObject = new GameObject ("Rail Mesh");
				gameObject.transform.position = item2.Position;
				gameObject.tag = "Railway";
				gameObject.layer = 16;
				gameObject.SetHierarchyGroup (item.Name);
				gameObject.SetActive (value: false);
				MeshCollider meshCollider = gameObject.AddComponent<MeshCollider> ();
				meshCollider.sharedMaterial = RailPhysicMaterial;
				meshCollider.sharedMesh = item2.Meshes [0];
				gameObject.AddComponent<AddToHeightMap> ();
				gameObject.SetActive (value: true);
			}
			AddTrackSpline (item);
		}
	}

	private void AddTrackSpline (PathList rail)
	{
		GameObject root = HierarchyUtil.GetRoot (rail.Name);
		TrainTrackSpline trainTrackSpline = root.AddComponent<TrainTrackSpline> ();
		trainTrackSpline.aboveGroundSpawn = rail.Hierarchy == 2;
		trainTrackSpline.hierarchy = rail.Hierarchy;
		if (trainTrackSpline.aboveGroundSpawn) {
			TrainTrackSpline.SidingSplines.Add (trainTrackSpline);
		}
		Vector3[] array = new Vector3[rail.Path.Points.Length];
		for (int i = 0; i < array.Length; i++) {
			array [i] = rail.Path.Points [i];
			array [i].y += 0.41f;
		}
		Vector3[] array2 = new Vector3[rail.Path.Tangents.Length];
		for (int j = 0; j < array.Length; j++) {
			array2 [j] = rail.Path.Tangents [j];
		}
		trainTrackSpline.SetAll (array, array2, 0.25f);
	}
}
