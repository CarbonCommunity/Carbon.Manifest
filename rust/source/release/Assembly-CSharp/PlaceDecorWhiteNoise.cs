using UnityEngine;

public class PlaceDecorWhiteNoise : ProceduralComponent
{
	public SpawnFilter Filter;

	public string ResourceFolder = string.Empty;

	public float ObjectDensity = 100f;

	public override void Process (uint seed)
	{
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		//IL_016c: Unknown result type (might be due to invalid IL or missing references)
		//IL_016e: Unknown result type (might be due to invalid IL or missing references)
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0181: Unknown result type (might be due to invalid IL or missing references)
		//IL_0183: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0191: Unknown result type (might be due to invalid IL or missing references)
		//IL_0193: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a7: Unknown result type (might be due to invalid IL or missing references)
		if (World.Networked) {
			World.Spawn ("Decor", "assets/bundled/prefabs/autospawn/" + ResourceFolder + "/");
			return;
		}
		TerrainHeightMap heightMap = TerrainMeta.HeightMap;
		Prefab[] array = Prefab.Load ("assets/bundled/prefabs/autospawn/" + ResourceFolder);
		if (array == null || array.Length == 0) {
			return;
		}
		Vector3 position = TerrainMeta.Position;
		Vector3 size = TerrainMeta.Size;
		int num = Mathf.RoundToInt (ObjectDensity * size.x * size.z * 1E-06f);
		float x = position.x;
		float z = position.z;
		float num2 = position.x + size.x;
		float num3 = position.z + size.z;
		Vector3 pos = default(Vector3);
		for (int i = 0; i < num; i++) {
			float num4 = SeedRandom.Range (ref seed, x, num2);
			float num5 = SeedRandom.Range (ref seed, z, num3);
			float normX = TerrainMeta.NormalizeX (num4);
			float normZ = TerrainMeta.NormalizeZ (num5);
			float num6 = SeedRandom.Value (ref seed);
			float factor = Filter.GetFactor (normX, normZ);
			Prefab random = array.GetRandom (ref seed);
			if (!(factor * factor < num6)) {
				float height = heightMap.GetHeight (normX, normZ);
				((Vector3)(ref pos))..ctor (num4, height, num5);
				Quaternion rot = random.Object.transform.localRotation;
				Vector3 scale = random.Object.transform.localScale;
				random.ApplyDecorComponents (ref pos, ref rot, ref scale);
				if (random.ApplyTerrainAnchors (ref pos, rot, scale, Filter) && random.ApplyTerrainChecks (pos, rot, scale, Filter) && random.ApplyTerrainFilters (pos, rot, scale) && random.ApplyWaterChecks (pos, rot, scale)) {
					World.AddPrefab ("Decor", random, pos, rot, scale);
				}
			}
		}
	}
}
