using UnityEngine;

public class DeployVolumeOBB : DeployVolume
{
	public Bounds bounds = new Bounds (Vector3.zero, Vector3.one);

	protected override bool Check (Vector3 position, Quaternion rotation, int mask = -1)
	{
		position += rotation * (worldRotation * bounds.center + worldPosition);
		OBB obb = new OBB (position, bounds.size, rotation * worldRotation);
		if (DeployVolume.CheckOBB (obb, (int)layers & mask, this)) {
			return true;
		}
		return false;
	}

	protected override bool Check (Vector3 position, Quaternion rotation, OBB test, int mask = -1)
	{
		position += rotation * (worldRotation * bounds.center + worldPosition);
		OBB oBB = new OBB (position, bounds.size, rotation * worldRotation);
		if (((int)layers & mask) != 0 && oBB.Intersects (test)) {
			return true;
		}
		return false;
	}
}
