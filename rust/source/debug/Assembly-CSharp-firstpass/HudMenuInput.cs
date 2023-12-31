using Rust.UI;
using UnityEngine.UI;

public class HudMenuInput : ListComponent<HudMenuInput>
{
	private InputField inputField;

	private RustInput rustInput;

	public static bool AnyActive ()
	{
		for (int i = 0; i < ListComponent<HudMenuInput>.InstanceList.Count; i++) {
			HudMenuInput hudMenuInput = ListComponent<HudMenuInput>.InstanceList [i];
			if (hudMenuInput.IsCurrentlyActive ()) {
				return true;
			}
		}
		return false;
	}

	private void Start ()
	{
		inputField = GetComponent<InputField> ();
		rustInput = GetComponent<RustInput> ();
	}

	private bool IsCurrentlyActive ()
	{
		if (!base.enabled) {
			return false;
		}
		if (rustInput != null) {
			return rustInput.IsFocused;
		}
		if (inputField == null) {
			return false;
		}
		return inputField.isFocused;
	}
}
