#define ENABLE_PROFILER
using System;
using System.Collections.Generic;
using Facepunch;
using UnityEngine;
using UnityEngine.Profiling;

[CreateAssetMenu (menuName = "Rust/Hair Set")]
public class HairSet : ScriptableObject
{
	[Serializable]
	public class MeshReplace
	{
		[HideInInspector]
		public string FindName;

		public Mesh Find;

		public Mesh[] ReplaceShapes;

		public bool Test (string materialName)
		{
			return FindName == materialName;
		}
	}

	public MeshReplace[] MeshReplacements;

	public void Process (PlayerModelHair playerModelHair, HairDyeCollection dyeCollection, HairDye dye, MaterialPropertyBlock block)
	{
		List<SkinnedMeshRenderer> obj = Pool.GetList<SkinnedMeshRenderer> ();
		playerModelHair.gameObject.GetComponentsInChildren (includeInactive: true, obj);
		foreach (SkinnedMeshRenderer item in obj) {
			if (item.sharedMesh == null || item.sharedMaterial == null) {
				continue;
			}
			string materialName = item.sharedMesh.name;
			string text = item.sharedMaterial.name;
			if (!item.gameObject.activeSelf) {
				item.gameObject.SetActive (value: true);
			}
			for (int i = 0; i < MeshReplacements.Length; i++) {
				Profiler.BeginSample ("MeshReplace");
				if (MeshReplacements [i].Test (materialName)) {
				}
				Profiler.EndSample ();
			}
			Profiler.BeginSample ("ApplyHairDye");
			if (dye != null && item.gameObject.activeSelf) {
				dye.Apply (dyeCollection, block);
			}
			Profiler.EndSample ();
		}
		Pool.FreeList (ref obj);
	}

	public void ProcessMorphs (GameObject obj, int blendShapeIndex = -1)
	{
	}
}
