using System.Linq;
using UnityEngine.UI;

public class TweakUIMultiSelect : TweakUIBase
{
	public ToggleGroup toggleGroup;

	protected override void Init ()
	{
		base.Init ();
		UpdateToggleGroup ();
	}

	protected void OnEnable ()
	{
		UpdateToggleGroup ();
	}

	public void OnChanged ()
	{
		UpdateConVar ();
	}

	private void UpdateToggleGroup ()
	{
		if (conVar != null) {
			string @string = conVar.String;
			Toggle[] componentsInChildren = toggleGroup.GetComponentsInChildren<Toggle> ();
			foreach (Toggle obj in componentsInChildren) {
				obj.isOn = obj.name == @string;
			}
		}
	}

	private void UpdateConVar ()
	{
		if (conVar != null) {
			Toggle toggle = (from x in toggleGroup.GetComponentsInChildren<Toggle> ()
				where x.isOn
				select x).FirstOrDefault ();
			if (!(toggle == null) && !(conVar.String == toggle.name)) {
				conVar.Set (toggle.name);
			}
		}
	}
}
