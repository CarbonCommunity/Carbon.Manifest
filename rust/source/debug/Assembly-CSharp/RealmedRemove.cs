using System.Linq;
using UnityEngine;

public class RealmedRemove : MonoBehaviour, IPrefabPreProcess
{
	public GameObject[] removedFromClient;

	public Component[] removedComponentFromClient;

	public GameObject[] removedFromServer;

	public Component[] removedComponentFromServer;

	public Component[] doNotRemoveFromServer;

	public Component[] doNotRemoveFromClient;

	public void PreProcess (IPrefabProcessor process, GameObject rootObj, string name, bool serverside, bool clientside, bool bundling)
	{
		if (clientside) {
			GameObject[] array = removedFromClient;
			foreach (GameObject obj in array) {
				Object.DestroyImmediate (obj, allowDestroyingAssets: true);
			}
			Component[] array2 = removedComponentFromClient;
			foreach (Component obj2 in array2) {
				Object.DestroyImmediate (obj2, allowDestroyingAssets: true);
			}
		}
		if (serverside) {
			GameObject[] array3 = removedFromServer;
			foreach (GameObject obj3 in array3) {
				Object.DestroyImmediate (obj3, allowDestroyingAssets: true);
			}
			Component[] array4 = removedComponentFromServer;
			foreach (Component obj4 in array4) {
				Object.DestroyImmediate (obj4, allowDestroyingAssets: true);
			}
		}
		if (!bundling) {
			process.RemoveComponent (this);
		}
	}

	public bool ShouldDelete (Component comp, bool client, bool server)
	{
		if (client && doNotRemoveFromClient != null && doNotRemoveFromClient.Contains (comp)) {
			return false;
		}
		if (server && doNotRemoveFromServer != null && doNotRemoveFromServer.Contains (comp)) {
			return false;
		}
		return true;
	}
}
