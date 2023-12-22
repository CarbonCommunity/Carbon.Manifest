using System.Collections.Generic;
using ConVar;
using UnityEngine;
using UnityEngine.Rendering;

public class MeshRendererBatch : MeshBatch
{
	private Vector3 position;

	private Mesh meshBatch;

	private MeshFilter meshFilter;

	private MeshRenderer meshRenderer;

	private MeshRendererData meshData;

	private MeshRendererGroup meshGroup;

	private MeshRendererLookup meshLookup;

	public override int VertexCapacity => Batching.renderer_capacity;

	public override int VertexCutoff => Batching.renderer_vertices;

	protected void Awake ()
	{
		meshFilter = ((Component)this).GetComponent<MeshFilter> ();
		meshRenderer = ((Component)this).GetComponent<MeshRenderer> ();
		meshData = new MeshRendererData ();
		meshGroup = new MeshRendererGroup ();
		meshLookup = new MeshRendererLookup ();
	}

	public void Setup (Vector3 position, Material material, ShadowCastingMode shadows, int layer)
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Invalid comparison between Unknown and I4
		Vector3 val2 = (((Component)this).transform.position = position);
		this.position = val2;
		((Component)this).gameObject.layer = layer;
		((Renderer)meshRenderer).sharedMaterial = material;
		((Renderer)meshRenderer).shadowCastingMode = shadows;
		if ((int)shadows == 3) {
			((Renderer)meshRenderer).receiveShadows = false;
			((Renderer)meshRenderer).motionVectors = false;
			((Renderer)meshRenderer).lightProbeUsage = (LightProbeUsage)0;
			((Renderer)meshRenderer).reflectionProbeUsage = (ReflectionProbeUsage)0;
		} else {
			((Renderer)meshRenderer).receiveShadows = true;
			((Renderer)meshRenderer).motionVectors = true;
			((Renderer)meshRenderer).lightProbeUsage = (LightProbeUsage)1;
			((Renderer)meshRenderer).reflectionProbeUsage = (ReflectionProbeUsage)1;
		}
	}

	public void Add (MeshRendererInstance instance)
	{
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		ref Vector3 reference = ref instance.position;
		reference -= position;
		meshGroup.data.Add (instance);
		AddVertices (instance.mesh.vertexCount);
	}

	protected override void AllocMemory ()
	{
		meshGroup.Alloc ();
		meshData.Alloc ();
	}

	protected override void FreeMemory ()
	{
		meshGroup.Free ();
		meshData.Free ();
	}

	protected override void RefreshMesh ()
	{
		meshLookup.dst.Clear ();
		meshData.Clear ();
		meshData.Combine (meshGroup, meshLookup);
	}

	protected override void ApplyMesh ()
	{
		if (!Object.op_Implicit ((Object)(object)meshBatch)) {
			meshBatch = AssetPool.Get<Mesh> ();
		}
		meshLookup.Apply ();
		meshData.Apply (meshBatch);
		meshBatch.UploadMeshData (false);
	}

	protected override void ToggleMesh (bool state)
	{
		List<MeshRendererLookup.LookupEntry> data = meshLookup.src.data;
		for (int i = 0; i < data.Count; i++) {
			Renderer renderer = data [i].renderer;
			if (Object.op_Implicit ((Object)(object)renderer)) {
				renderer.enabled = !state;
			}
		}
		if (state) {
			if (Object.op_Implicit ((Object)(object)meshFilter)) {
				meshFilter.sharedMesh = meshBatch;
			}
			if (Object.op_Implicit ((Object)(object)meshRenderer)) {
				((Renderer)meshRenderer).enabled = true;
			}
		} else {
			if (Object.op_Implicit ((Object)(object)meshFilter)) {
				meshFilter.sharedMesh = null;
			}
			if (Object.op_Implicit ((Object)(object)meshRenderer)) {
				((Renderer)meshRenderer).enabled = false;
			}
		}
	}

	protected override void OnPooled ()
	{
		if (Object.op_Implicit ((Object)(object)meshFilter)) {
			meshFilter.sharedMesh = null;
		}
		if (Object.op_Implicit ((Object)(object)meshBatch)) {
			AssetPool.Free (ref meshBatch);
		}
		meshData.Free ();
		meshGroup.Free ();
		meshLookup.src.Clear ();
		meshLookup.dst.Clear ();
	}
}
