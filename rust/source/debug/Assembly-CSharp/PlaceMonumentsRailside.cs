using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class PlaceMonumentsRailside : ProceduralComponent
{
	private struct SpawnInfo
	{
		public Prefab<MonumentInfo> prefab;

		public Vector3 position;

		public Quaternion rotation;

		public Vector3 scale;
	}

	private class SpawnInfoGroup
	{
		public bool processed;

		public Prefab<MonumentInfo> prefab;

		public List<SpawnInfo> candidates;
	}

	private struct DistanceInfo
	{
		public float minDistanceSameType;

		public float maxDistanceSameType;

		public float minDistanceDifferentType;

		public float maxDistanceDifferentType;
	}

	public enum DistanceMode
	{
		Any,
		Min,
		Max
	}

	public SpawnFilter Filter = null;

	public string ResourceFolder = string.Empty;

	public int TargetCount = 0;

	public int PositionOffset = 100;

	public int TangentInterval = 100;

	[FormerlySerializedAs ("MinDistance")]
	public int MinDistanceSameType = 500;

	public int MinDistanceDifferentType = 0;

	[FormerlySerializedAs ("MinSize")]
	public int MinWorldSize = 0;

	[Tooltip ("Distance to monuments of the same type")]
	public DistanceMode DistanceSameType = DistanceMode.Max;

	[Tooltip ("Distance to monuments of a different type")]
	public DistanceMode DistanceDifferentType = DistanceMode.Any;

	private const int GroupCandidates = 8;

	private const int IndividualCandidates = 8;

	private static Quaternion rot90 = Quaternion.Euler (0f, 90f, 0f);

	public override void Process (uint seed)
	{
		string[] array = (from folder in ResourceFolder.Split (',')
			select "assets/bundled/prefabs/autospawn/" + folder + "/").ToArray ();
		if (World.Networked) {
			World.Spawn ("Monument", array);
		} else {
			if (World.Size < MinWorldSize) {
				return;
			}
			TerrainHeightMap heightMap = TerrainMeta.HeightMap;
			List<Prefab<MonumentInfo>> list = new List<Prefab<MonumentInfo>> ();
			string[] array2 = array;
			foreach (string folder2 in array2) {
				Prefab<MonumentInfo>[] array3 = Prefab.Load<MonumentInfo> (folder2);
				array3.Shuffle (ref seed);
				list.AddRange (array3);
			}
			Prefab<MonumentInfo>[] array4 = list.ToArray ();
			if (array4 == null || array4.Length == 0) {
				return;
			}
			array4.BubbleSort ();
			SpawnInfoGroup[] array5 = new SpawnInfoGroup[array4.Length];
			for (int j = 0; j < array4.Length; j++) {
				Prefab<MonumentInfo> prefab = array4 [j];
				SpawnInfoGroup spawnInfoGroup = null;
				for (int k = 0; k < j; k++) {
					SpawnInfoGroup spawnInfoGroup2 = array5 [k];
					Prefab<MonumentInfo> prefab2 = spawnInfoGroup2.prefab;
					if (prefab == prefab2) {
						spawnInfoGroup = spawnInfoGroup2;
						break;
					}
				}
				if (spawnInfoGroup == null) {
					spawnInfoGroup = new SpawnInfoGroup ();
					spawnInfoGroup.prefab = array4 [j];
					spawnInfoGroup.candidates = new List<SpawnInfo> ();
				}
				array5 [j] = spawnInfoGroup;
			}
			SpawnInfoGroup[] array6 = array5;
			foreach (SpawnInfoGroup spawnInfoGroup3 in array6) {
				if (spawnInfoGroup3.processed) {
					continue;
				}
				Prefab<MonumentInfo> prefab3 = spawnInfoGroup3.prefab;
				MonumentInfo component = prefab3.Component;
				if (component == null || World.Size < component.MinWorldSize) {
					continue;
				}
				int num = 0;
				Vector3 zero = Vector3.zero;
				Vector3 vector = Vector3.zero;
				Vector3 vector2 = Vector3.zero;
				TerrainPathConnect[] componentsInChildren = prefab3.Object.GetComponentsInChildren<TerrainPathConnect> (includeInactive: true);
				TerrainPathConnect[] array7 = componentsInChildren;
				foreach (TerrainPathConnect terrainPathConnect in array7) {
					if (terrainPathConnect.Type == InfrastructureType.Rail) {
						switch (num) {
						case 0:
							vector = terrainPathConnect.transform.position;
							break;
						case 1:
							vector2 = terrainPathConnect.transform.position;
							break;
						}
						zero += terrainPathConnect.transform.position;
						num++;
					}
				}
				Vector3 normalized = (vector2 - vector).normalized;
				Vector3 vector3 = rot90 * normalized;
				if (num > 1) {
					zero /= (float)num;
				}
				if (PositionOffset > 0) {
					zero += vector3 * PositionOffset;
				}
				foreach (PathList rail in TerrainMeta.Path.Rails) {
					PathInterpolator path = rail.Path;
					float num2 = TangentInterval / 2;
					float num3 = 5f;
					float num4 = 5f;
					float num5 = path.StartOffset + num4;
					float num6 = path.Length - path.EndOffset - num4;
					for (float num7 = num5; num7 <= num6; num7 += num3) {
						Vector3 vector4 = (rail.Spline ? path.GetPointCubicHermite (num7) : path.GetPoint (num7));
						Vector3 normalized2 = (path.GetPoint (num7 + num2) - path.GetPoint (num7 - num2)).normalized;
						for (int n = -1; n <= 1; n += 2) {
							Quaternion quaternion = Quaternion.LookRotation (n * normalized2.XZ3D ());
							Vector3 position = vector4;
							Quaternion quaternion2 = quaternion;
							Vector3 localScale = prefab3.Object.transform.localScale;
							quaternion2 *= Quaternion.LookRotation (normalized);
							position -= quaternion2 * zero;
							SpawnInfo item = default(SpawnInfo);
							item.prefab = prefab3;
							item.position = position;
							item.rotation = quaternion2;
							item.scale = localScale;
							spawnInfoGroup3.candidates.Add (item);
						}
					}
				}
				spawnInfoGroup3.processed = true;
			}
			int num8 = 0;
			List<SpawnInfo> a = new List<SpawnInfo> ();
			int num9 = 0;
			List<SpawnInfo> b = new List<SpawnInfo> ();
			for (int num10 = 0; num10 < 8; num10++) {
				num8 = 0;
				a.Clear ();
				array5.Shuffle (ref seed);
				SpawnInfoGroup[] array8 = array5;
				foreach (SpawnInfoGroup spawnInfoGroup4 in array8) {
					Prefab<MonumentInfo> prefab4 = spawnInfoGroup4.prefab;
					MonumentInfo component2 = prefab4.Component;
					if (component2 == null || World.Size < component2.MinWorldSize) {
						continue;
					}
					DungeonGridInfo dungeonEntrance = component2.DungeonEntrance;
					int num12 = (int)((!prefab4.Parameters) ? PrefabPriority.Low : (prefab4.Parameters.Priority + 1));
					int num13 = 100000 * num12 * num12 * num12 * num12;
					int num14 = 0;
					int num15 = 0;
					SpawnInfo item2 = default(SpawnInfo);
					spawnInfoGroup4.candidates.Shuffle (ref seed);
					for (int num16 = 0; num16 < spawnInfoGroup4.candidates.Count; num16++) {
						SpawnInfo spawnInfo = spawnInfoGroup4.candidates [num16];
						DistanceInfo distanceInfo = GetDistanceInfo (a, prefab4, spawnInfo.position, spawnInfo.rotation, spawnInfo.scale);
						if (distanceInfo.minDistanceSameType < (float)MinDistanceSameType || distanceInfo.minDistanceDifferentType < (float)MinDistanceDifferentType) {
							continue;
						}
						int num17 = num13;
						if (distanceInfo.minDistanceSameType != float.MaxValue) {
							if (DistanceSameType == DistanceMode.Min) {
								num17 -= Mathf.RoundToInt (distanceInfo.minDistanceSameType * distanceInfo.minDistanceSameType * 2f);
							} else if (DistanceSameType == DistanceMode.Max) {
								num17 += Mathf.RoundToInt (distanceInfo.minDistanceSameType * distanceInfo.minDistanceSameType * 2f);
							}
						}
						if (distanceInfo.minDistanceDifferentType != float.MaxValue) {
							if (DistanceDifferentType == DistanceMode.Min) {
								num17 -= Mathf.RoundToInt (distanceInfo.minDistanceDifferentType * distanceInfo.minDistanceDifferentType);
							} else if (DistanceDifferentType == DistanceMode.Max) {
								num17 += Mathf.RoundToInt (distanceInfo.minDistanceDifferentType * distanceInfo.minDistanceDifferentType);
							}
						}
						if (num17 <= num15 || !prefab4.ApplyTerrainAnchors (ref spawnInfo.position, spawnInfo.rotation, spawnInfo.scale, Filter) || !component2.CheckPlacement (spawnInfo.position, spawnInfo.rotation, spawnInfo.scale)) {
							continue;
						}
						if ((bool)dungeonEntrance) {
							Vector3 vector5 = spawnInfo.position + spawnInfo.rotation * Vector3.Scale (spawnInfo.scale, dungeonEntrance.transform.position);
							Vector3 vector6 = dungeonEntrance.SnapPosition (vector5);
							spawnInfo.position += vector6 - vector5;
							if (!dungeonEntrance.IsValidSpawnPosition (vector6)) {
								continue;
							}
						}
						if (prefab4.ApplyTerrainChecks (spawnInfo.position, spawnInfo.rotation, spawnInfo.scale, Filter) && prefab4.ApplyTerrainFilters (spawnInfo.position, spawnInfo.rotation, spawnInfo.scale) && prefab4.ApplyWaterChecks (spawnInfo.position, spawnInfo.rotation, spawnInfo.scale) && !prefab4.CheckEnvironmentVolumes (spawnInfo.position, spawnInfo.rotation, spawnInfo.scale, EnvironmentType.Underground | EnvironmentType.TrainTunnels)) {
							num15 = num17;
							item2 = spawnInfo;
							num14++;
							if (num14 >= 8 || DistanceDifferentType == DistanceMode.Any) {
								break;
							}
						}
					}
					if (num15 > 0) {
						a.Add (item2);
						num8 += num15;
					}
					if (TargetCount > 0 && a.Count >= TargetCount) {
						break;
					}
				}
				if (num8 > num9) {
					num9 = num8;
					GenericsUtil.Swap (ref a, ref b);
				}
			}
			foreach (SpawnInfo item3 in b) {
				World.AddPrefab ("Monument", item3.prefab, item3.position, item3.rotation, item3.scale);
			}
		}
	}

	private DistanceInfo GetDistanceInfo (List<SpawnInfo> spawns, Prefab<MonumentInfo> prefab, Vector3 monumentPos, Quaternion monumentRot, Vector3 monumentScale)
	{
		DistanceInfo result = default(DistanceInfo);
		result.minDistanceDifferentType = float.MaxValue;
		result.maxDistanceDifferentType = float.MinValue;
		result.minDistanceSameType = float.MaxValue;
		result.maxDistanceSameType = float.MinValue;
		OBB oBB = new OBB (monumentPos, monumentScale, monumentRot, prefab.Component.Bounds);
		if (TerrainMeta.Path != null) {
			foreach (MonumentInfo monument in TerrainMeta.Path.Monuments) {
				if (!prefab.Component.HasDungeonLink || (!monument.HasDungeonLink && monument.WantsDungeonLink)) {
					float num = monument.SqrDistance (oBB);
					if (num < result.minDistanceDifferentType) {
						result.minDistanceDifferentType = num;
					}
					if (num > result.maxDistanceDifferentType) {
						result.maxDistanceDifferentType = num;
					}
				}
			}
			if (result.minDistanceDifferentType != float.MaxValue) {
				result.minDistanceDifferentType = Mathf.Sqrt (result.minDistanceDifferentType);
			}
			if (result.maxDistanceDifferentType != float.MinValue) {
				result.maxDistanceDifferentType = Mathf.Sqrt (result.maxDistanceDifferentType);
			}
		}
		if (spawns != null) {
			foreach (SpawnInfo spawn in spawns) {
				float num2 = new OBB (spawn.position, spawn.scale, spawn.rotation, spawn.prefab.Component.Bounds).SqrDistance (oBB);
				if (num2 < result.minDistanceSameType) {
					result.minDistanceSameType = num2;
				}
				if (num2 > result.maxDistanceSameType) {
					result.maxDistanceSameType = num2;
				}
			}
			if (prefab.Component.HasDungeonLink) {
				foreach (MonumentInfo monument2 in TerrainMeta.Path.Monuments) {
					if (monument2.HasDungeonLink || !monument2.WantsDungeonLink) {
						float num3 = monument2.SqrDistance (oBB);
						if (num3 < result.minDistanceSameType) {
							result.minDistanceSameType = num3;
						}
						if (num3 > result.maxDistanceSameType) {
							result.maxDistanceSameType = num3;
						}
					}
				}
				foreach (DungeonGridInfo dungeonGridEntrance in TerrainMeta.Path.DungeonGridEntrances) {
					float num4 = dungeonGridEntrance.SqrDistance (monumentPos);
					if (num4 < result.minDistanceSameType) {
						result.minDistanceSameType = num4;
					}
					if (num4 > result.maxDistanceSameType) {
						result.maxDistanceSameType = num4;
					}
				}
			}
			if (result.minDistanceSameType != float.MaxValue) {
				result.minDistanceSameType = Mathf.Sqrt (result.minDistanceSameType);
			}
			if (result.maxDistanceSameType != float.MinValue) {
				result.maxDistanceSameType = Mathf.Sqrt (result.maxDistanceSameType);
			}
		}
		return result;
	}
}
