using System.Collections.Generic;
using UnityEngine;

public class GenerateRoadMeshes : ProceduralComponent
{
	public const float NormalSmoothing = 0f;

	public const bool SnapToTerrain = true;

	public Mesh RoadMesh = null;

	public Mesh[] RoadMeshes = null;

	public Material RoadMaterial = null;

	public Material RoadRingMaterial = null;

	public PhysicMaterial RoadPhysicMaterial = null;

	public override bool RunOnCache => true;

	public override void Process (uint seed)
	{
		if (RoadMeshes == null || RoadMeshes.Length == 0) {
			RoadMeshes = new Mesh[1] { RoadMesh };
		}
		List<PathList> roads = TerrainMeta.Path.Roads;
		foreach (PathList item in roads) {
			if (item.Hierarchy >= 2) {
				continue;
			}
			foreach (PathList.MeshObject item2 in item.CreateMesh (RoadMeshes, 0f, snapToTerrain: true, !item.Path.Circular, !item.Path.Circular)) {
				GameObject gameObject = new GameObject ("Road Mesh");
				gameObject.transform.position = item2.Position;
				gameObject.layer = 16;
				gameObject.SetHierarchyGroup (item.Name);
				gameObject.SetActive (value: false);
				MeshCollider meshCollider = gameObject.AddComponent<MeshCollider> ();
				meshCollider.sharedMaterial = RoadPhysicMaterial;
				meshCollider.sharedMesh = item2.Meshes [0];
				gameObject.AddComponent<AddToHeightMap> ();
				gameObject.SetActive (value: true);
			}
		}
	}
}
