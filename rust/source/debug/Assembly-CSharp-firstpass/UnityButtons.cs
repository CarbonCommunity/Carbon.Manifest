using System;
using Facepunch;
using UnityEngine;

public static class UnityButtons
{
	private static bool isRegistered = false;

	public static void Register ()
	{
		if (isRegistered) {
			Debug.LogError ("UnityButtons.Register called twice!");
			return;
		}
		isRegistered = true;
		foreach (KeyCode value in Enum.GetValues (typeof(KeyCode))) {
			if (value == KeyCode.None || value >= KeyCode.JoystickButton0) {
				continue;
			}
			string text = value.ToString ();
			KeyCode localKey = value;
			bool isFKey = text.Length == 2 && text.StartsWith ("F");
			bool isMouseButton = text.StartsWith ("mouse", StringComparison.CurrentCultureIgnoreCase);
			if (text.StartsWith ("Alpha")) {
				text = text.Replace ("Alpha", "");
			}
			Facepunch.Input.AddButton (text, value, delegate {
				if (!UnityEngine.Input.GetKey (localKey)) {
					return false;
				}
				if (!isMouseButton && !isFKey && !KeyBinding.IsOpen && (NeedsKeyboard.AnyActive (localKey) || HudMenuInput.AnyActive ())) {
					return false;
				}
				return (!isMouseButton || !NeedsMouseButtons.AnyActive ()) ? true : false;
			});
		}
		float wheelValue2 = 0f;
		float lastWheelValue2 = 0f;
		Facepunch.Input.AddButton ("MouseWheelUp", KeyCode.None, delegate {
			if (lastWheelValue2 > 0f) {
				wheelValue2 = 0f;
			}
			lastWheelValue2 = wheelValue2;
			wheelValue2 = 0f;
			return lastWheelValue2 > 0f;
		}, delegate {
			if (Cursor.visible) {
				wheelValue2 = 0f;
			} else {
				wheelValue2 = Mathf.Max (wheelValue2, UnityEngine.Input.GetAxis ("Mouse ScrollWheel"));
			}
		});
		float wheelValue = 0f;
		float lastWheelValue = 0f;
		Facepunch.Input.AddButton ("MouseWheelDown", KeyCode.None, delegate {
			if (lastWheelValue > 0f) {
				wheelValue = 0f;
			}
			lastWheelValue = wheelValue;
			wheelValue = 0f;
			return lastWheelValue > 0f;
		}, delegate {
			if (Cursor.visible) {
				wheelValue = 0f;
			} else {
				wheelValue = Mathf.Max (wheelValue, UnityEngine.Input.GetAxis ("Mouse ScrollWheel") * -1f);
			}
		});
	}
}
