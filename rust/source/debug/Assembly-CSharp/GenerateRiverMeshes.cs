using System.Collections.Generic;
using UnityEngine;

public class GenerateRiverMeshes : ProceduralComponent
{
	public const float NormalSmoothing = 0.1f;

	public const bool SnapToTerrain = true;

	public Mesh RiverMesh = null;

	public Mesh[] RiverMeshes = null;

	public Material RiverMaterial = null;

	public PhysicMaterial RiverPhysicMaterial = null;

	public override bool RunOnCache => true;

	public override void Process (uint seed)
	{
		RiverMeshes = new Mesh[1] { RiverMesh };
		List<PathList> rivers = TerrainMeta.Path.Rivers;
		foreach (PathList item in rivers) {
			foreach (PathList.MeshObject item2 in item.CreateMesh (RiverMeshes, 0.1f, snapToTerrain: true, !item.Path.Circular, !item.Path.Circular)) {
				GameObject gameObject = new GameObject ("River Mesh");
				gameObject.transform.position = item2.Position;
				gameObject.tag = "River";
				gameObject.layer = 4;
				gameObject.SetHierarchyGroup (item.Name);
				gameObject.SetActive (value: false);
				MeshCollider meshCollider = gameObject.AddComponent<MeshCollider> ();
				meshCollider.sharedMaterial = RiverPhysicMaterial;
				meshCollider.sharedMesh = item2.Meshes [0];
				gameObject.AddComponent<RiverInfo> ();
				WaterBody waterBody = gameObject.AddComponent<WaterBody> ();
				waterBody.FishingType = WaterBody.FishingTag.River;
				gameObject.AddComponent<AddToWaterMap> ();
				gameObject.SetActive (value: true);
			}
		}
	}
}
