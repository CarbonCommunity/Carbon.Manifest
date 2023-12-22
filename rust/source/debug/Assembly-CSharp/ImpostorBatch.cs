#define ENABLE_PROFILER
using System.Collections.Generic;
using Facepunch;
using UnityEngine;
using UnityEngine.Profiling;

public class ImpostorBatch
{
	public FPNativeList<Vector4> Positions = null;

	private FPNativeList<uint> args = null;

	private Queue<int> recycle = new Queue<int> (32);

	public Mesh Mesh { get; private set; } = null;


	public Material Material { get; private set; } = null;


	public ComputeBuffer PositionBuffer { get; private set; } = null;


	public ComputeBuffer ArgsBuffer { get; private set; } = null;


	public bool IsDirty { get; set; }

	public int Count => Positions.Count;

	public bool Visible => Positions.Count - recycle.Count > 0;

	private ComputeBuffer SafeRelease (ComputeBuffer buffer)
	{
		buffer?.Release ();
		return null;
	}

	public void Initialize (Mesh mesh, Material material)
	{
		Mesh = mesh;
		Material = material;
		Positions = Pool.Get<FPNativeList<Vector4>> ();
		args = Pool.Get<FPNativeList<uint>> ();
		args.Resize (5);
		ArgsBuffer = SafeRelease (ArgsBuffer);
		ArgsBuffer = new ComputeBuffer (1, args.Count * 4, ComputeBufferType.DrawIndirect);
		args [0] = Mesh.GetIndexCount (0);
		args [2] = Mesh.GetIndexStart (0);
		args [3] = Mesh.GetBaseVertex (0);
	}

	public void Release ()
	{
		recycle.Clear ();
		Pool.Free (ref Positions);
		Pool.Free (ref args);
		PositionBuffer = SafeRelease (PositionBuffer);
		ArgsBuffer = SafeRelease (ArgsBuffer);
	}

	public void AddInstance (ImpostorInstanceData data)
	{
		data.Batch = this;
		if (recycle.Count > 0) {
			data.BatchIndex = recycle.Dequeue ();
			Positions [data.BatchIndex] = data.PositionAndScale ();
		} else {
			data.BatchIndex = Positions.Count;
			Positions.Add (data.PositionAndScale ());
		}
		IsDirty = true;
	}

	public void RemoveInstance (ImpostorInstanceData data)
	{
		Positions [data.BatchIndex] = new Vector4 (0f, 0f, 0f, -1f);
		recycle.Enqueue (data.BatchIndex);
		data.BatchIndex = 0;
		data.Batch = null;
		IsDirty = true;
	}

	public void UpdateBuffers ()
	{
		if (IsDirty) {
			bool flag = false;
			if (PositionBuffer == null || PositionBuffer.count != Positions.Count) {
				Profiler.BeginSample ("PositionBuffer.Resize");
				PositionBuffer = SafeRelease (PositionBuffer);
				PositionBuffer = new ComputeBuffer (Positions.Count, 16);
				flag = true;
				Profiler.EndSample ();
			}
			if (PositionBuffer != null) {
				Profiler.BeginSample ("PositionBuffer.SetData");
				PositionBuffer.SetData (Positions.Array, 0, 0, Positions.Count);
				Profiler.EndSample ();
			}
			if (ArgsBuffer != null && flag) {
				Profiler.BeginSample ("ArgsBuffer.SetData");
				args [1] = (uint)Positions.Count;
				ArgsBuffer.SetData (args.Array, 0, 0, args.Count);
				Profiler.EndSample ();
			}
			IsDirty = false;
		}
	}
}
