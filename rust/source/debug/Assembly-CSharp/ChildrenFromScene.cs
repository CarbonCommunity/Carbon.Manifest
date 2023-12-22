using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChildrenFromScene : MonoBehaviour
{
	public string SceneName;

	public bool StartChildrenDisabled;

	private IEnumerator Start ()
	{
		Debug.LogWarning ("WARNING: CHILDRENFROMSCENE(" + SceneName + ") - WE SHOULDN'T BE USING THIS SHITTY COMPONENT NOW WE HAVE AWESOME PREFABS", base.gameObject);
		if (!SceneManager.GetSceneByName (SceneName).isLoaded) {
			yield return SceneManager.LoadSceneAsync (SceneName, LoadSceneMode.Additive);
		}
		Scene scene = SceneManager.GetSceneByName (SceneName);
		GameObject[] objects = scene.GetRootGameObjects ();
		GameObject[] array = objects;
		foreach (GameObject ob in array) {
			ob.transform.SetParent (base.transform, worldPositionStays: false);
			ob.Identity ();
			RectTransform rt = ob.transform as RectTransform;
			if ((bool)rt) {
				rt.pivot = Vector2.zero;
				rt.anchoredPosition = Vector2.zero;
				rt.anchorMin = Vector2.zero;
				rt.anchorMax = Vector2.one;
				rt.sizeDelta = Vector2.one;
			}
			SingletonComponent[] componentsInChildren = ob.GetComponentsInChildren<SingletonComponent> (includeInactive: true);
			foreach (SingletonComponent s in componentsInChildren) {
				s.SingletonSetup ();
			}
			if (StartChildrenDisabled) {
				ob.SetActive (value: false);
			}
		}
		SceneManager.UnloadSceneAsync (scene);
	}
}
