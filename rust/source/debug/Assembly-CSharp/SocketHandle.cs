using System;
using UnityEngine;

public class SocketHandle : PrefabAttribute
{
	protected override Type GetIndexedType ()
	{
		return typeof(SocketHandle);
	}

	internal void AdjustTarget (ref Construction.Target target, float maxplaceDistance)
	{
		Vector3 vector = worldPosition;
		Vector3 vector2 = target.ray.origin + target.ray.direction * maxplaceDistance;
		Vector3 vector3 = vector2 - vector;
		target.ray.direction = (vector3 - target.ray.origin).normalized;
	}
}
