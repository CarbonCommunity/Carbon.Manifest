using UnityEngine;

public class ImpostorInstanceData
{
	public ImpostorBatch Batch = null;

	public int BatchIndex = 0;

	private int hash = 0;

	private Vector4 positionAndScale = Vector4.zero;

	public Renderer Renderer { get; private set; } = null;


	public Mesh Mesh { get; private set; } = null;


	public Material Material { get; private set; } = null;


	public ImpostorInstanceData (Renderer renderer, Mesh mesh, Material material)
	{
		Renderer = renderer;
		Mesh = mesh;
		Material = material;
		hash = GenerateHashCode ();
		Update ();
	}

	public ImpostorInstanceData (Vector3 position, Vector3 scale, Mesh mesh, Material material)
	{
		positionAndScale = new Vector4 (position.x, position.y, position.z, scale.x);
		Mesh = mesh;
		Material = material;
		hash = GenerateHashCode ();
		Update ();
	}

	private int GenerateHashCode ()
	{
		int num = 17;
		num = num * 31 + Material.GetHashCode ();
		return num * 31 + Mesh.GetHashCode ();
	}

	public override bool Equals (object obj)
	{
		ImpostorInstanceData impostorInstanceData = obj as ImpostorInstanceData;
		return impostorInstanceData.Material == Material && impostorInstanceData.Mesh == Mesh;
	}

	public override int GetHashCode ()
	{
		return hash;
	}

	public Vector4 PositionAndScale ()
	{
		if (Renderer != null) {
			Transform transform = Renderer.transform;
			Vector3 position = transform.position;
			Vector3 lossyScale = transform.lossyScale;
			float w = (Renderer.enabled ? lossyScale.x : (0f - lossyScale.x));
			positionAndScale = new Vector4 (position.x, position.y, position.z, w);
		}
		return positionAndScale;
	}

	public void Update ()
	{
		if (Batch != null) {
			Batch.Positions [BatchIndex] = PositionAndScale ();
			Batch.IsDirty = true;
		}
	}
}
