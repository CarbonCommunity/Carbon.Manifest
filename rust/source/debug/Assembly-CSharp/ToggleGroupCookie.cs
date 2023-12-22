using System.Linq;
using Rust;
using UnityEngine;
using UnityEngine.UI;

public class ToggleGroupCookie : MonoBehaviour
{
	public ToggleGroup group => GetComponent<ToggleGroup> ();

	private void OnEnable ()
	{
		string @string = PlayerPrefs.GetString ("ToggleGroupCookie_" + base.name);
		if (!string.IsNullOrEmpty (@string)) {
			Transform transform = base.transform.Find (@string);
			if ((bool)transform) {
				Toggle component = transform.GetComponent<Toggle> ();
				if ((bool)component) {
					Toggle[] componentsInChildren = GetComponentsInChildren<Toggle> (includeInactive: true);
					foreach (Toggle toggle in componentsInChildren) {
						toggle.isOn = false;
					}
					component.isOn = false;
					component.isOn = true;
					SetupListeners ();
					return;
				}
			}
		}
		Toggle toggle2 = group.ActiveToggles ().FirstOrDefault ((Toggle x) => x.isOn);
		if ((bool)toggle2) {
			toggle2.isOn = false;
			toggle2.isOn = true;
		}
		SetupListeners ();
	}

	private void OnDisable ()
	{
		if (!Rust.Application.isQuitting) {
			Toggle[] componentsInChildren = GetComponentsInChildren<Toggle> (includeInactive: true);
			foreach (Toggle toggle in componentsInChildren) {
				toggle.onValueChanged.RemoveListener (OnToggleChanged);
			}
		}
	}

	private void SetupListeners ()
	{
		Toggle[] componentsInChildren = GetComponentsInChildren<Toggle> (includeInactive: true);
		foreach (Toggle toggle in componentsInChildren) {
			toggle.onValueChanged.AddListener (OnToggleChanged);
		}
	}

	private void OnToggleChanged (bool b)
	{
		Toggle toggle = GetComponentsInChildren<Toggle> ().FirstOrDefault ((Toggle x) => x.isOn);
		if ((bool)toggle) {
			PlayerPrefs.SetString ("ToggleGroupCookie_" + base.name, toggle.gameObject.name);
		}
	}
}
