using UnityEngine;
using UnityEngine.UI;

public class ToolsHUDUI : MonoBehaviour
{
	[SerializeField]
	private GameObject prefab;

	[SerializeField]
	private Transform parent;

	private bool initialised;

	protected void OnEnable ()
	{
		Init ();
	}

	private void Init ()
	{
		if (initialised) {
			return;
		}
		UIHUD instance = SingletonComponent<UIHUD>.Instance;
		if (instance == null) {
			return;
		}
		initialised = true;
		Transform[] componentsInChildren = instance.GetComponentsInChildren<Transform> ();
		Transform[] array = componentsInChildren;
		foreach (Transform transform in array) {
			string text = transform.name;
			if (!text.ToLower ().StartsWith ("gameui.hud.")) {
				continue;
			}
			if (text.ToLower () == "gameui.hud.crosshair") {
				foreach (Transform item in transform) {
					AddToggleObj (item.name, "<color=yellow>Crosshair sub:</color> " + item.name);
				}
			}
			AddToggleObj (text, text.Substring (11));
		}
	}

	private void AddToggleObj (string trName, string labelText)
	{
		GameObject gameObject = Object.Instantiate (prefab, Vector3.zero, Quaternion.identity, parent);
		gameObject.name = trName;
		ToggleHUDLayer component = gameObject.GetComponent<ToggleHUDLayer> ();
		component.hudComponentName = trName;
		component.textControl.text = labelText;
	}

	public void SelectAll ()
	{
		Toggle[] componentsInChildren = parent.GetComponentsInChildren<Toggle> ();
		Toggle[] array = componentsInChildren;
		foreach (Toggle toggle in array) {
			toggle.isOn = true;
		}
	}

	public void SelectNone ()
	{
		Toggle[] componentsInChildren = parent.GetComponentsInChildren<Toggle> ();
		Toggle[] array = componentsInChildren;
		foreach (Toggle toggle in array) {
			toggle.isOn = false;
		}
	}
}
