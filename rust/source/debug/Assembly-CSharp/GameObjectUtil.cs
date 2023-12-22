using UnityEngine;

public static class GameObjectUtil
{
	public static void GlobalBroadcast (string messageName, object param = null)
	{
		Transform[] rootObjects = TransformUtil.GetRootObjects ();
		Transform[] array = rootObjects;
		foreach (Transform transform in array) {
			transform.BroadcastMessage (messageName, param, SendMessageOptions.DontRequireReceiver);
		}
	}
}
