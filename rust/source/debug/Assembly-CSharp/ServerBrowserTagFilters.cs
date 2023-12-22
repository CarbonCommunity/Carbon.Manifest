using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ServerBrowserTagFilters : MonoBehaviour
{
	public UnityEvent TagFiltersChanged = new UnityEvent ();

	private ServerBrowserTagGroup[] _groups;

	private List<bool> _previousState;

	public void Start ()
	{
		_groups = base.gameObject.GetComponentsInChildren<ServerBrowserTagGroup> ();
		UnityAction call = delegate {
			TagFiltersChanged?.Invoke ();
		};
		ServerBrowserTagGroup[] groups = _groups;
		foreach (ServerBrowserTagGroup serverBrowserTagGroup in groups) {
			ServerBrowserTag[] tags = serverBrowserTagGroup.tags;
			foreach (ServerBrowserTag serverBrowserTag in tags) {
				serverBrowserTag.button.OnPressed.AddListener (call);
				serverBrowserTag.button.OnReleased.AddListener (call);
			}
		}
	}

	public void DeselectAll ()
	{
		if (_groups == null) {
			return;
		}
		ServerBrowserTagGroup[] groups = _groups;
		foreach (ServerBrowserTagGroup serverBrowserTagGroup in groups) {
			if (serverBrowserTagGroup.tags != null) {
				ServerBrowserTag[] tags = serverBrowserTagGroup.tags;
				foreach (ServerBrowserTag serverBrowserTag in tags) {
					serverBrowserTag.button.SetToggleFalse ();
				}
			}
		}
	}

	public void GetTags (out List<HashSet<string>> searchTagGroups, out HashSet<string> excludeTags)
	{
		searchTagGroups = new List<HashSet<string>> ();
		excludeTags = new HashSet<string> ();
		ServerBrowserTagGroup[] groups = _groups;
		foreach (ServerBrowserTagGroup serverBrowserTagGroup in groups) {
			if (!serverBrowserTagGroup.AnyActive ()) {
				continue;
			}
			if (serverBrowserTagGroup.isExclusive) {
				HashSet<string> hashSet = new HashSet<string> (StringComparer.OrdinalIgnoreCase);
				ServerBrowserTag[] tags = serverBrowserTagGroup.tags;
				foreach (ServerBrowserTag serverBrowserTag in tags) {
					if (serverBrowserTag.IsActive) {
						hashSet.Add (serverBrowserTag.serverTag);
					} else if (serverBrowserTagGroup.isExclusive) {
						excludeTags.Add (serverBrowserTag.serverTag);
					}
				}
				if (hashSet.Count > 0) {
					searchTagGroups.Add (hashSet);
				}
				continue;
			}
			ServerBrowserTag[] tags2 = serverBrowserTagGroup.tags;
			foreach (ServerBrowserTag serverBrowserTag2 in tags2) {
				if (serverBrowserTag2.IsActive) {
					HashSet<string> hashSet2 = new HashSet<string> (StringComparer.OrdinalIgnoreCase);
					hashSet2.Add (serverBrowserTag2.serverTag);
					searchTagGroups.Add (hashSet2);
				}
			}
		}
	}
}
