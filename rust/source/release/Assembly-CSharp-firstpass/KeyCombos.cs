using System;
using System.Collections.Generic;
using System.Linq;
using Facepunch;
using UnityEngine;

public static class KeyCombos
{
	public static bool TryParse (ref string name, out List<KeyCode> keys)
	{
		if (string.IsNullOrWhiteSpace (name) || name.Length < 5 || !name.StartsWith ("[") || !name.EndsWith ("]") || !name.Contains ("+")) {
			keys = null;
			return false;
		}
		string[] array = name.Substring (1, name.Length - 2).ToLowerInvariant ().Split ('+');
		List<KeyCode> list = new List<KeyCode> (array.Length);
		string[] array2 = array;
		foreach (string text in array2) {
			string value = text;
			if (text.Length == 1 && char.IsDigit (text [0])) {
				value = "alpha" + text;
			}
			if (!Enum.TryParse<KeyCode> (value, ignoreCase: true, out var result)) {
				keys = null;
				return false;
			}
			list.Add (result);
		}
		IEnumerable<string> values = from k in list
			select k.ToString ().ToLowerInvariant () into s
			select (!s.StartsWith ("alpha")) ? s : s.Replace ("alpha", "");
		name = "[" + string.Join ("+", values) + "]";
		keys = list;
		return true;
	}

	public static void RegisterButton (string name, List<KeyCode> keys)
	{
		if (string.IsNullOrWhiteSpace (name) || keys == null || keys.Count <= 1 || Facepunch.Input.HasButton (name)) {
			return;
		}
		Facepunch.Input.AddButton (name, KeyCode.None, delegate {
			foreach (KeyCode key in keys) {
				if (!UnityEngine.Input.GetKey (key)) {
					return false;
				}
				if (!IsFunctionKey (key) && !KeyBinding.IsOpen && (NeedsKeyboard.AnyActive () || HudMenuInput.AnyActive ())) {
					return false;
				}
				if (IsMouseButton (key) && NeedsMouseButtons.AnyActive ()) {
					return false;
				}
			}
			return true;
		});
	}

	private static bool IsFunctionKey (KeyCode keyCode)
	{
		if (keyCode >= KeyCode.F1) {
			return keyCode <= KeyCode.F15;
		}
		return false;
	}

	private static bool IsMouseButton (KeyCode keyCode)
	{
		if (keyCode >= KeyCode.Mouse0) {
			return keyCode <= KeyCode.Mouse6;
		}
		return false;
	}
}
