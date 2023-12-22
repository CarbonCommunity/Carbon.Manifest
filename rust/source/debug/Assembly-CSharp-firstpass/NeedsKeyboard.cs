using System;
using System.Collections.Generic;
using Facepunch;
using UnityEngine;
using UnityEngine.Events;

public class NeedsKeyboard : ListComponent<NeedsKeyboard>
{
	[Flags]
	public enum BypassOption
	{
		Voice = 1,
		Chat = 4,
		Gesture = 8,
		CardGames = 0x10,
		Movement = 0x20,
		Ping = 0x40
	}

	public UnityEvent onNoKeysDown;

	public bool ShowEscapeUI;

	public bool blockUnspecifiedInput = true;

	public BypassOption AllowedBinds = (BypassOption)0;

	private List<string> binds = new List<string> ();

	private bool watchForNoKeys = false;

	public static bool ShouldShowUI => ListComponent<NeedsKeyboard>.InstanceList.Count > 0 && ListComponent<NeedsKeyboard>.InstanceList [0].ShowEscapeUI;

	private static void GetBindString (BypassOption bypassOption, List<string> resultBinds)
	{
		if ((bypassOption & BypassOption.Voice) == BypassOption.Voice) {
			resultBinds.Add ("+voice");
		}
		if ((bypassOption & BypassOption.Chat) == BypassOption.Chat) {
			resultBinds.Add ("chat.open");
		}
		if ((bypassOption & BypassOption.Gesture) == BypassOption.Gesture) {
			resultBinds.Add ("+gestures");
		}
		if ((bypassOption & BypassOption.Movement) == BypassOption.Movement) {
			resultBinds.Add ("+left");
			resultBinds.Add ("+right");
			resultBinds.Add ("+backward");
			resultBinds.Add ("+forward");
			resultBinds.Add ("+sprint");
			resultBinds.Add ("+duck");
			resultBinds.Add ("+jump");
		}
		if ((bypassOption & BypassOption.Ping) == BypassOption.Ping) {
			resultBinds.Add ("+ping");
		}
	}

	public static bool AnyActive (KeyCode key = KeyCode.None, BypassOption forBypass = (BypassOption)0)
	{
		if (key != 0 || forBypass != 0) {
			foreach (NeedsKeyboard instance in ListComponent<NeedsKeyboard>.InstanceList) {
				if (!instance.ShouldBlockInput () || instance.AllowKeyInput (key, forBypass)) {
					continue;
				}
				return true;
			}
			return false;
		}
		foreach (NeedsKeyboard instance2 in ListComponent<NeedsKeyboard>.InstanceList) {
			if (instance2.blockUnspecifiedInput && instance2.ShouldBlockInput ()) {
				return true;
			}
		}
		return false;
	}

	protected override void OnEnable ()
	{
		base.OnEnable ();
		watchForNoKeys = true;
		if (AllowedBinds == (BypassOption)0) {
			return;
		}
		binds.Clear ();
		List<string> obj = Pool.GetList<string> ();
		GetBindString (AllowedBinds, obj);
		foreach (string item in obj) {
			binds.AddRange (Facepunch.Input.GetButtonsWithBind (item));
		}
		Pool.FreeList (ref obj);
	}

	public void Update ()
	{
		if (watchForNoKeys && !UnityEngine.Input.anyKey) {
			watchForNoKeys = false;
			onNoKeysDown?.Invoke ();
		}
	}

	private bool AllowKeyInput (KeyCode k, BypassOption forBypass)
	{
		if (AllowedBinds == (BypassOption)0) {
			return false;
		}
		if (forBypass != 0 && (forBypass & AllowedBinds) == forBypass) {
			return true;
		}
		if (forBypass == (BypassOption)0 && k == KeyCode.None) {
			return false;
		}
		string b = k.ToString ();
		foreach (string bind in binds) {
			if (string.Equals (bind, b, StringComparison.OrdinalIgnoreCase)) {
				return true;
			}
		}
		return false;
	}

	protected virtual bool ShouldBlockInput ()
	{
		return true;
	}
}
