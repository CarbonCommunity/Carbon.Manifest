using UnityEngine;

public class DeployVolumeEntityBounds : DeployVolume
{
	private Bounds bounds = new Bounds (Vector3.zero, Vector3.one);

	protected override bool Check (Vector3 position, Quaternion rotation, int mask = -1)
	{
		position += rotation * bounds.center;
		OBB obb = new OBB (position, bounds.size, rotation);
		if (DeployVolume.CheckOBB (obb, (int)layers & mask, this)) {
			return true;
		}
		return false;
	}

	protected override bool Check (Vector3 position, Quaternion rotation, OBB obb, int mask = -1)
	{
		return false;
	}

	protected override void AttributeSetup (GameObject rootObj, string name, bool serverside, bool clientside, bool bundling)
	{
		bounds = rootObj.GetComponent<BaseEntity> ().bounds;
	}
}
