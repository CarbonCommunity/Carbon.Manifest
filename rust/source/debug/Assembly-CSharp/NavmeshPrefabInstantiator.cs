using UnityEngine;

public class NavmeshPrefabInstantiator : MonoBehaviour
{
	public GameObjectRef NavmeshPrefab;

	private void Start ()
	{
		if (NavmeshPrefab != null) {
			GameObject gameObject = NavmeshPrefab.Instantiate (base.transform);
			gameObject.SetActive (value: true);
			Object.Destroy (this);
		}
	}
}
