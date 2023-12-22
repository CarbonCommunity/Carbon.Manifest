using System;
using System.Collections.Generic;
using Facepunch;
using UnityEngine;
using UnityEngine.AI;

public class AsyncTerrainNavMeshBake : CustomYieldInstruction
{
	private List<int> indices;

	private List<Vector3> vertices;

	private List<Vector3> normals;

	private List<int> triangles;

	private Vector3 pivot;

	private int width;

	private int height;

	private bool normal;

	private bool alpha;

	private Action worker;

	public override bool keepWaiting => worker != null;

	public bool isDone => worker == null;

	public Mesh mesh {
		get {
			Mesh mesh = new Mesh ();
			if (vertices != null) {
				mesh.SetVertices (vertices);
				Pool.FreeList (ref vertices);
			}
			if (normals != null) {
				mesh.SetNormals (normals);
				Pool.FreeList (ref normals);
			}
			if (triangles != null) {
				mesh.SetTriangles (triangles, 0);
				Pool.FreeList (ref triangles);
			}
			if (indices != null) {
				Pool.FreeList (ref indices);
			}
			return mesh;
		}
	}

	public NavMeshBuildSource CreateNavMeshBuildSource ()
	{
		NavMeshBuildSource result = default(NavMeshBuildSource);
		result.transform = Matrix4x4.TRS (pivot, Quaternion.identity, Vector3.one);
		result.shape = NavMeshBuildSourceShape.Mesh;
		result.sourceObject = mesh;
		return result;
	}

	public NavMeshBuildSource CreateNavMeshBuildSource (int area)
	{
		NavMeshBuildSource result = CreateNavMeshBuildSource ();
		result.area = area;
		return result;
	}

	public AsyncTerrainNavMeshBake (Vector3 pivot, int width, int height, bool normal, bool alpha)
	{
		this.pivot = pivot;
		this.width = width;
		this.height = height;
		this.normal = normal;
		this.alpha = alpha;
		indices = Pool.GetList<int> ();
		vertices = Pool.GetList<Vector3> ();
		normals = (normal ? Pool.GetList<Vector3> () : null);
		triangles = Pool.GetList<int> ();
		Invoke ();
	}

	private void DoWork ()
	{
		Vector3 vector = new Vector3 (width / 2, 0f, height / 2);
		Vector3 vector2 = new Vector3 (pivot.x - vector.x, 0f, pivot.z - vector.z);
		TerrainHeightMap heightMap = TerrainMeta.HeightMap;
		TerrainAlphaMap alphaMap = TerrainMeta.AlphaMap;
		int i = 0;
		for (int j = 0; j <= height; j++) {
			for (int k = 0; k <= width; k++, i++) {
				Vector3 worldPos = new Vector3 (k, 0f, j) + vector2;
				Vector3 item = new Vector3 (k, 0f, j) - vector;
				float num = heightMap.GetHeight (worldPos);
				if (num < -1f) {
					indices.Add (-1);
					continue;
				}
				if (alpha) {
					float num2 = alphaMap.GetAlpha (worldPos);
					if (num2 < 0.1f) {
						indices.Add (-1);
						continue;
					}
				}
				if (normal) {
					Vector3 item2 = heightMap.GetNormal (worldPos);
					normals.Add (item2);
				}
				worldPos.y = (item.y = num - pivot.y);
				indices.Add (vertices.Count);
				vertices.Add (item);
			}
		}
		int num3 = 0;
		int num4 = 0;
		while (num4 < height) {
			int num5 = 0;
			while (num5 < width) {
				int num6 = indices [num3];
				int num7 = indices [num3 + width + 1];
				int num8 = indices [num3 + 1];
				int num9 = indices [num3 + 1];
				int num10 = indices [num3 + width + 1];
				int num11 = indices [num3 + width + 2];
				if (num6 != -1 && num7 != -1 && num8 != -1) {
					triangles.Add (num6);
					triangles.Add (num7);
					triangles.Add (num8);
				}
				if (num9 != -1 && num10 != -1 && num11 != -1) {
					triangles.Add (num9);
					triangles.Add (num10);
					triangles.Add (num11);
				}
				num5++;
				num3++;
			}
			num4++;
			num3++;
		}
	}

	private void Invoke ()
	{
		worker = DoWork;
		worker.BeginInvoke (Callback, null);
	}

	private void Callback (IAsyncResult result)
	{
		worker.EndInvoke (result);
		worker = null;
	}
}
