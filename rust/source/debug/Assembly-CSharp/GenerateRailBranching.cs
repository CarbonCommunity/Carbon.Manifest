using System;
using System.Collections.Generic;
using UnityEngine;

public class GenerateRailBranching : ProceduralComponent
{
	public const float Width = 4f;

	public const float InnerPadding = 1f;

	public const float OuterPadding = 1f;

	public const float InnerFade = 1f;

	public const float OuterFade = 32f;

	public const float RandomScale = 1f;

	public const float MeshOffset = 0f;

	public const float TerrainOffset = -0.125f;

	private static Quaternion rot90 = Quaternion.Euler (0f, 90f, 0f);

	private const int MaxDepth = 250000;

	private PathList CreateSegment (int number, Vector3[] points)
	{
		PathList pathList = new PathList ("Rail " + number, points);
		pathList.Spline = true;
		pathList.Width = 4f;
		pathList.InnerPadding = 1f;
		pathList.OuterPadding = 1f;
		pathList.InnerFade = 1f;
		pathList.OuterFade = 32f;
		pathList.RandomScale = 1f;
		pathList.MeshOffset = 0f;
		pathList.TerrainOffset = -0.125f;
		pathList.Topology = 524288;
		pathList.Splat = 128;
		pathList.Hierarchy = 1;
		return pathList;
	}

	public override void Process (uint seed)
	{
		if (World.Networked) {
			TerrainMeta.Path.Rails.Clear ();
			TerrainMeta.Path.Rails.AddRange (World.GetPaths ("Rail"));
			return;
		}
		int min = Mathf.RoundToInt (40f);
		int max = Mathf.RoundToInt (53.3333321f);
		int min2 = Mathf.RoundToInt (40f);
		int max2 = Mathf.RoundToInt (120f);
		float num = 120f;
		float num2 = num * num;
		List<PathList> list = new List<PathList> ();
		int[,] array = TerrainPath.CreateRailCostmap (ref seed);
		PathFinder pathFinder = new PathFinder (array);
		int length = array.GetLength (0);
		List<Vector3> list2 = new List<Vector3> ();
		List<Vector3> list3 = new List<Vector3> ();
		List<Vector3> list4 = new List<Vector3> ();
		HashSet<Vector3> hashSet = new HashSet<Vector3> ();
		foreach (PathList rail2 in TerrainMeta.Path.Rails) {
			foreach (PathList rail3 in TerrainMeta.Path.Rails) {
				if (rail2 == rail3) {
					continue;
				}
				Vector3[] points = rail2.Path.Points;
				foreach (Vector3 vector in points) {
					Vector3[] points2 = rail3.Path.Points;
					foreach (Vector3 vector2 in points2) {
						if ((vector - vector2).sqrMagnitude < num2) {
							hashSet.Add (vector);
							break;
						}
					}
				}
			}
		}
		foreach (PathList rail4 in TerrainMeta.Path.Rails) {
			PathInterpolator path = rail4.Path;
			Vector3[] points3 = path.Points;
			Vector3[] tangents = path.Tangents;
			int num3 = path.MinIndex + 1 + 8;
			int num4 = path.MaxIndex - 1 - 8;
			for (int l = num3; l <= num4; l++) {
				list2.Clear ();
				list3.Clear ();
				list4.Clear ();
				int num5 = SeedRandom.Range (ref seed, min2, max2);
				int num6 = SeedRandom.Range (ref seed, min, max);
				int num7 = l;
				int num8 = l + num5;
				if (num8 >= num4) {
					continue;
				}
				Vector3 from = tangents [num7];
				Vector3 to = tangents [num8];
				if (Vector3.Angle (from, to) > 30f) {
					continue;
				}
				Vector3 vector3 = points3 [num7];
				Vector3 vector4 = points3 [num8];
				if (hashSet.Contains (vector3) || hashSet.Contains (vector4)) {
					continue;
				}
				PathFinder.Point pathFinderPoint = GetPathFinderPoint (vector3, length);
				PathFinder.Point pathFinderPoint2 = GetPathFinderPoint (vector4, length);
				l += num6;
				PathFinder.Node node = pathFinder.FindPath (pathFinderPoint, pathFinderPoint2, 250000);
				if (node == null) {
					continue;
				}
				PathFinder.Node node2 = null;
				PathFinder.Node node3 = null;
				PathFinder.Node node4 = node;
				while (node4 != null && node4.next != null) {
					if (node4 == node.next) {
						node2 = node4;
					}
					if (node4.next.next == null) {
						node3 = node4;
					}
					node4 = node4.next;
				}
				if (node2 == null || node3 == null) {
					continue;
				}
				node = node2;
				node3.next = null;
				for (int m = 0; m < 8; m++) {
					int num9 = num7 + (m + 1 - 8);
					int num10 = num8 + m;
					list2.Add (points3 [num9]);
					list3.Add (points3 [num10]);
				}
				list4.AddRange (list2);
				for (PathFinder.Node node5 = node2; node5 != null; node5 = node5.next) {
					float normX = ((float)node5.point.x + 0.5f) / (float)length;
					float normZ = ((float)node5.point.y + 0.5f) / (float)length;
					float x = TerrainMeta.DenormalizeX (normX);
					float z = TerrainMeta.DenormalizeZ (normZ);
					float y = Mathf.Max (TerrainMeta.HeightMap.GetHeight (normX, normZ), 1f);
					list4.Add (new Vector3 (x, y, z));
				}
				list4.AddRange (list3);
				int num11 = 8;
				int num12 = list4.Count - 1 - 8;
				Vector3 to2 = Vector3.Normalize (list4 [num11 + 16] - list4 [num11]);
				Vector3 to3 = Vector3.Normalize (list4 [num12] - list4 [num12 - 16]);
				Vector3 vector5 = Vector3.Normalize (points3 [num7 + 16] - points3 [num7]);
				Vector3 vector6 = Vector3.Normalize (points3 [num8] - points3 [(num8 - 16 + points3.Length) % points3.Length]);
				float num13 = Vector3.SignedAngle (vector5, to2, Vector3.up);
				float num14 = 0f - Vector3.SignedAngle (vector6, to3, Vector3.up);
				if (Mathf.Sign (num13) != Mathf.Sign (num14) || Mathf.Abs (num13) > 60f || Mathf.Abs (num14) > 60f) {
					continue;
				}
				Vector3 vector7 = rot90 * vector5;
				Vector3 vector8 = rot90 * vector6;
				if (num13 < 0f) {
					vector7 = -vector7;
				}
				if (num14 < 0f) {
					vector8 = -vector8;
				}
				for (int n = 0; n < 16; n++) {
					int index = n;
					int index2 = list4.Count - n - 1;
					float t = Mathf.InverseLerp (0f, 8f, n);
					float num15 = Mathf.SmoothStep (0f, 2f, t) * 4f;
					list4 [index] += vector7 * num15;
					list4 [index2] += vector8 * num15;
				}
				bool flag = false;
				bool flag2 = false;
				foreach (Vector3 item in list4) {
					bool blocked = TerrainMeta.PlacementMap.GetBlocked (item);
					if (!flag2 && !flag && !blocked) {
						flag = true;
					}
					if (flag && !flag2 && blocked) {
						flag2 = true;
					}
					if (flag && flag2 && !blocked) {
						list4.Clear ();
						break;
					}
				}
				if (list4.Count != 0) {
					if (list4.Count >= 2) {
						int number = TerrainMeta.Path.Rails.Count + list.Count;
						PathList pathList = CreateSegment (number, list4.ToArray ());
						pathList.Start = false;
						pathList.End = false;
						pathList.ProcgenStartNode = node2;
						pathList.ProcgenEndNode = node3;
						list.Add (pathList);
					}
					l += num5;
				}
			}
		}
		foreach (PathList rail in list) {
			Func<int, float> filter = delegate(int i) {
				float a = Mathf.InverseLerp (0f, 8f, i);
				float b = Mathf.InverseLerp (rail.Path.DefaultMaxIndex, rail.Path.DefaultMaxIndex - 8, i);
				return Mathf.SmoothStep (0f, 1f, Mathf.Min (a, b));
			};
			rail.Path.Smoothen (32, new Vector3 (1f, 0f, 1f), filter);
			rail.Path.Smoothen (64, new Vector3 (0f, 1f, 0f), filter);
			rail.Path.Resample (7.5f);
			rail.Path.RecalculateTangents ();
			rail.AdjustPlacementMap (20f);
		}
		TerrainMeta.Path.Rails.AddRange (list);
	}

	public PathFinder.Point GetPathFinderPoint (Vector3 worldPos, int res)
	{
		float num = TerrainMeta.NormalizeX (worldPos.x);
		float num2 = TerrainMeta.NormalizeZ (worldPos.z);
		PathFinder.Point result = default(PathFinder.Point);
		result.x = Mathf.Clamp ((int)(num * (float)res), 0, res - 1);
		result.y = Mathf.Clamp ((int)(num2 * (float)res), 0, res - 1);
		return result;
	}
}
