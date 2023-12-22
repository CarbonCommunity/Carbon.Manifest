using System;
using UnityEngine;

public class WaterCheck : PrefabAttribute
{
	public bool Rotate = true;

	protected void OnDrawGizmosSelected ()
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		Gizmos.color = new Color (0f, 0f, 0.5f, 1f);
		Gizmos.DrawSphere (((Component)this).transform.position, 1f);
	}

	public bool Check (Vector3 pos)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		return pos.y <= TerrainMeta.WaterMap.GetHeight (pos);
	}

	protected override Type GetIndexedType ()
	{
		return typeof(WaterCheck);
	}
}
