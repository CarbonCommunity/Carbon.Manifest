using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class PlaceMonuments : ProceduralComponent
{
	private struct SpawnInfo
	{
		public Prefab<MonumentInfo> prefab;

		public Vector3 position;

		public Quaternion rotation;

		public Vector3 scale;

		public bool dungeonEntrance;

		public Vector3 dungeonEntrancePos;
	}

	private struct DistanceInfo
	{
		public float minDistanceSameType;

		public float maxDistanceSameType;

		public float minDistanceDifferentType;

		public float maxDistanceDifferentType;

		public float minDistanceDungeonEntrance;

		public float maxDistanceDungeonEntrance;
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

	private const int Attempts = 10000;

	private const int MaxDepth = 100000;

	public override void Process (uint seed)
	{
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_07df: Unknown result type (might be due to invalid IL or missing references)
		//IL_07e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_07ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_027c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0281: Unknown result type (might be due to invalid IL or missing references)
		//IL_028f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0294: Unknown result type (might be due to invalid IL or missing references)
		//IL_0296: Unknown result type (might be due to invalid IL or missing references)
		//IL_0298: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_02af: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_041f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0421: Unknown result type (might be due to invalid IL or missing references)
		//IL_043e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0440: Unknown result type (might be due to invalid IL or missing references)
		//IL_0442: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0465: Unknown result type (might be due to invalid IL or missing references)
		//IL_0467: Unknown result type (might be due to invalid IL or missing references)
		//IL_0469: Unknown result type (might be due to invalid IL or missing references)
		//IL_0472: Unknown result type (might be due to invalid IL or missing references)
		//IL_0477: Unknown result type (might be due to invalid IL or missing references)
		//IL_047c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0481: Unknown result type (might be due to invalid IL or missing references)
		//IL_0486: Unknown result type (might be due to invalid IL or missing references)
		//IL_048a: Unknown result type (might be due to invalid IL or missing references)
		//IL_048c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0491: Unknown result type (might be due to invalid IL or missing references)
		//IL_0493: Unknown result type (might be due to invalid IL or missing references)
		//IL_0495: Unknown result type (might be due to invalid IL or missing references)
		//IL_0497: Unknown result type (might be due to invalid IL or missing references)
		//IL_0499: Unknown result type (might be due to invalid IL or missing references)
		//IL_049e: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_04be: Unknown result type (might be due to invalid IL or missing references)
		//IL_0500: Unknown result type (might be due to invalid IL or missing references)
		//IL_0502: Unknown result type (might be due to invalid IL or missing references)
		//IL_0504: Unknown result type (might be due to invalid IL or missing references)
		//IL_051b: Unknown result type (might be due to invalid IL or missing references)
		//IL_051d: Unknown result type (might be due to invalid IL or missing references)
		//IL_051f: Unknown result type (might be due to invalid IL or missing references)
		//IL_06bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_06bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_06cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_06cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0636: Unknown result type (might be due to invalid IL or missing references)
		//IL_0638: Unknown result type (might be due to invalid IL or missing references)
		//IL_063a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0643: Unknown result type (might be due to invalid IL or missing references)
		//IL_0648: Unknown result type (might be due to invalid IL or missing references)
		//IL_064d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0652: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ee: Unknown result type (might be due to invalid IL or missing references)
		string[] array = (from folder in ResourceFolder.Split (',')
			select "assets/bundled/prefabs/autospawn/" + folder + "/").ToArray ();
		if (World.Networked) {
			World.Spawn ("Monument", array);
		} else {
			if (World.Size < MinWorldSize) {
				return;
			}
			TerrainHeightMap heightMap = TerrainMeta.HeightMap;
			PathFinder pathFinder = null;
			List<PathFinder.Point> endList = null;
			List<Prefab<MonumentInfo>> list = new List<Prefab<MonumentInfo>> ();
			string[] array2 = array;
			foreach (string folder2 in array2) {
				Prefab<MonumentInfo>[] array3 = Prefab.Load<MonumentInfo> (folder2, (GameManager)null, (PrefabAttribute.Library)null, useProbabilities: true);
				array3.Shuffle (ref seed);
				list.AddRange (array3);
			}
			Prefab<MonumentInfo>[] array4 = list.ToArray ();
			if (array4 == null || array4.Length == 0) {
				return;
			}
			array4.BubbleSort ();
			Vector3 position = TerrainMeta.Position;
			Vector3 size = TerrainMeta.Size;
			float x = position.x;
			float z = position.z;
			float num = position.x + size.x;
			float num2 = position.z + size.z;
			int num3 = 0;
			List<SpawnInfo> list2 = new List<SpawnInfo> ();
			int num4 = 0;
			List<SpawnInfo> list3 = new List<SpawnInfo> ();
			Vector3 pos = default(Vector3);
			for (int j = 0; j < 8; j++) {
				num3 = 0;
				list2.Clear ();
				Prefab<MonumentInfo>[] array5 = array4;
				foreach (Prefab<MonumentInfo> prefab in array5) {
					MonumentInfo component = prefab.Component;
					if ((Object)(object)component == (Object)null || World.Size < component.MinWorldSize) {
						continue;
					}
					DungeonGridInfo dungeonEntrance = component.DungeonEntrance;
					int num5 = (int)((!Object.op_Implicit ((Object)(object)prefab.Parameters)) ? PrefabPriority.Low : (prefab.Parameters.Priority + 1));
					int num6 = 100000 * num5 * num5 * num5 * num5;
					int num7 = 0;
					int num8 = 0;
					SpawnInfo item = default(SpawnInfo);
					for (int l = 0; l < 10000; l++) {
						float num9 = SeedRandom.Range (ref seed, x, num);
						float num10 = SeedRandom.Range (ref seed, z, num2);
						float normX = TerrainMeta.NormalizeX (num9);
						float normZ = TerrainMeta.NormalizeZ (num10);
						float num11 = SeedRandom.Value (ref seed);
						float factor = Filter.GetFactor (normX, normZ);
						if (factor * factor < num11) {
							continue;
						}
						float height = heightMap.GetHeight (normX, normZ);
						((Vector3)(ref pos))..ctor (num9, height, num10);
						Quaternion rot = prefab.Object.transform.localRotation;
						Vector3 scale = prefab.Object.transform.localScale;
						Vector3 val = pos;
						prefab.ApplyDecorComponents (ref pos, ref rot, ref scale);
						DistanceInfo distanceInfo = GetDistanceInfo (list2, prefab, pos, rot, scale, val);
						if (distanceInfo.minDistanceSameType < (float)MinDistanceSameType || distanceInfo.minDistanceDifferentType < (float)MinDistanceDifferentType || (Object.op_Implicit ((Object)(object)dungeonEntrance) && distanceInfo.minDistanceDungeonEntrance < dungeonEntrance.MinDistance)) {
							continue;
						}
						int num12 = num6;
						if (distanceInfo.minDistanceSameType != float.MaxValue) {
							if (DistanceSameType == DistanceMode.Min) {
								num12 -= Mathf.RoundToInt (distanceInfo.minDistanceSameType * distanceInfo.minDistanceSameType * 2f);
							} else if (DistanceSameType == DistanceMode.Max) {
								num12 += Mathf.RoundToInt (distanceInfo.minDistanceSameType * distanceInfo.minDistanceSameType * 2f);
							}
						}
						if (distanceInfo.minDistanceDifferentType != float.MaxValue) {
							if (DistanceDifferentType == DistanceMode.Min) {
								num12 -= Mathf.RoundToInt (distanceInfo.minDistanceDifferentType * distanceInfo.minDistanceDifferentType);
							} else if (DistanceDifferentType == DistanceMode.Max) {
								num12 += Mathf.RoundToInt (distanceInfo.minDistanceDifferentType * distanceInfo.minDistanceDifferentType);
							}
						}
						if (num12 <= num8 || !prefab.ApplyTerrainAnchors (ref pos, rot, scale, Filter) || !component.CheckPlacement (pos, rot, scale)) {
							continue;
						}
						if (Object.op_Implicit ((Object)(object)dungeonEntrance)) {
							Vector3 val2 = pos + rot * Vector3.Scale (scale, ((Component)dungeonEntrance).transform.position);
							Vector3 val3 = dungeonEntrance.SnapPosition (val2);
							pos += val3 - val2;
							if (!dungeonEntrance.IsValidSpawnPosition (val3)) {
								continue;
							}
							val = val3;
						}
						if (!prefab.ApplyTerrainChecks (pos, rot, scale, Filter) || !prefab.ApplyTerrainFilters (pos, rot, scale) || !prefab.ApplyWaterChecks (pos, rot, scale) || prefab.CheckEnvironmentVolumes (pos, rot, scale, EnvironmentType.Underground | EnvironmentType.TrainTunnels)) {
							continue;
						}
						bool flag = false;
						TerrainPathConnect[] componentsInChildren = prefab.Object.GetComponentsInChildren<TerrainPathConnect> (true);
						foreach (TerrainPathConnect terrainPathConnect in componentsInChildren) {
							if (terrainPathConnect.Type == InfrastructureType.Boat) {
								if (pathFinder == null) {
									int[,] array6 = TerrainPath.CreateBoatCostmap (2f);
									int length = array6.GetLength (0);
									pathFinder = new PathFinder (array6);
									endList = new List<PathFinder.Point> {
										new PathFinder.Point (0, 0),
										new PathFinder.Point (0, length / 2),
										new PathFinder.Point (0, length - 1),
										new PathFinder.Point (length / 2, 0),
										new PathFinder.Point (length / 2, length - 1),
										new PathFinder.Point (length - 1, 0),
										new PathFinder.Point (length - 1, length / 2),
										new PathFinder.Point (length - 1, length - 1)
									};
								}
								PathFinder.Point pathFinderPoint = terrainPathConnect.GetPathFinderPoint (pathFinder.GetResolution (0), pos + rot * Vector3.Scale (scale, ((Component)terrainPathConnect).transform.localPosition));
								PathFinder.Node node = pathFinder.FindPathUndirected (new List<PathFinder.Point> { pathFinderPoint }, endList, 100000);
								if (node == null) {
									flag = true;
									break;
								}
							}
						}
						if (!flag) {
							SpawnInfo spawnInfo = default(SpawnInfo);
							spawnInfo.prefab = prefab;
							spawnInfo.position = pos;
							spawnInfo.rotation = rot;
							spawnInfo.scale = scale;
							if (Object.op_Implicit ((Object)(object)dungeonEntrance)) {
								spawnInfo.dungeonEntrance = true;
								spawnInfo.dungeonEntrancePos = val;
							}
							num8 = num12;
							item = spawnInfo;
							num7++;
							if (num7 >= 8 || DistanceDifferentType == DistanceMode.Any) {
								break;
							}
						}
					}
					if (num8 > 0) {
						list2.Add (item);
						num3 += num8;
					}
					if (TargetCount > 0 && list2.Count >= TargetCount) {
						break;
					}
				}
				if (num3 > num4) {
					num4 = num3;
					GenericsUtil.Swap<List<SpawnInfo>> (ref list2, ref list3);
				}
			}
			foreach (SpawnInfo item2 in list3) {
				World.AddPrefab ("Monument", item2.prefab, item2.position, item2.rotation, item2.scale);
			}
		}
	}

	private DistanceInfo GetDistanceInfo (List<SpawnInfo> spawns, Prefab<MonumentInfo> prefab, Vector3 monumentPos, Quaternion monumentRot, Vector3 monumentScale, Vector3 dungeonPos)
	{
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_022a: Unknown result type (might be due to invalid IL or missing references)
		//IL_029f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		//IL_0195: Unknown result type (might be due to invalid IL or missing references)
		//IL_0197: Unknown result type (might be due to invalid IL or missing references)
		//IL_019c: Unknown result type (might be due to invalid IL or missing references)
		DistanceInfo result = default(DistanceInfo);
		result.minDistanceSameType = float.MaxValue;
		result.maxDistanceSameType = float.MinValue;
		result.minDistanceDifferentType = float.MaxValue;
		result.maxDistanceDifferentType = float.MinValue;
		result.minDistanceDungeonEntrance = float.MaxValue;
		result.maxDistanceDungeonEntrance = float.MinValue;
		OBB val = default(OBB);
		((OBB)(ref val))..ctor (monumentPos, monumentScale, monumentRot, prefab.Component.Bounds);
		if (spawns != null) {
			foreach (SpawnInfo spawn in spawns) {
				OBB val2 = new OBB (spawn.position, spawn.scale, spawn.rotation, spawn.prefab.Component.Bounds);
				float num = ((OBB)(ref val2)).SqrDistance (val);
				if (spawn.prefab.Folder == prefab.Folder) {
					if (num < result.minDistanceSameType) {
						result.minDistanceSameType = num;
					}
					if (num > result.maxDistanceSameType) {
						result.maxDistanceSameType = num;
					}
				} else {
					if (num < result.minDistanceDifferentType) {
						result.minDistanceDifferentType = num;
					}
					if (num > result.maxDistanceDifferentType) {
						result.maxDistanceDifferentType = num;
					}
				}
			}
			foreach (SpawnInfo spawn2 in spawns) {
				if (spawn2.dungeonEntrance) {
					Vector3 val3 = spawn2.dungeonEntrancePos - dungeonPos;
					float sqrMagnitude = ((Vector3)(ref val3)).sqrMagnitude;
					if (sqrMagnitude < result.minDistanceDungeonEntrance) {
						result.minDistanceDungeonEntrance = sqrMagnitude;
					}
					if (sqrMagnitude > result.maxDistanceDungeonEntrance) {
						result.maxDistanceDungeonEntrance = sqrMagnitude;
					}
				}
			}
		}
		if ((Object)(object)TerrainMeta.Path != (Object)null) {
			foreach (MonumentInfo monument in TerrainMeta.Path.Monuments) {
				float num2 = monument.SqrDistance (val);
				if (num2 < result.minDistanceDifferentType) {
					result.minDistanceDifferentType = num2;
				}
				if (num2 > result.maxDistanceDifferentType) {
					result.maxDistanceDifferentType = num2;
				}
			}
			foreach (DungeonGridInfo dungeonGridEntrance in TerrainMeta.Path.DungeonGridEntrances) {
				float num3 = dungeonGridEntrance.SqrDistance (dungeonPos);
				if (num3 < result.minDistanceDungeonEntrance) {
					result.minDistanceDungeonEntrance = num3;
				}
				if (num3 > result.maxDistanceDungeonEntrance) {
					result.maxDistanceDungeonEntrance = num3;
				}
			}
		}
		if (result.minDistanceSameType != float.MaxValue) {
			result.minDistanceSameType = Mathf.Sqrt (result.minDistanceSameType);
		}
		if (result.maxDistanceSameType != float.MinValue) {
			result.maxDistanceSameType = Mathf.Sqrt (result.maxDistanceSameType);
		}
		if (result.minDistanceDifferentType != float.MaxValue) {
			result.minDistanceDifferentType = Mathf.Sqrt (result.minDistanceDifferentType);
		}
		if (result.maxDistanceDifferentType != float.MinValue) {
			result.maxDistanceDifferentType = Mathf.Sqrt (result.maxDistanceDifferentType);
		}
		if (result.minDistanceDungeonEntrance != float.MaxValue) {
			result.minDistanceDungeonEntrance = Mathf.Sqrt (result.minDistanceDungeonEntrance);
		}
		if (result.maxDistanceDungeonEntrance != float.MinValue) {
			result.maxDistanceDungeonEntrance = Mathf.Sqrt (result.maxDistanceDungeonEntrance);
		}
		return result;
	}
}
