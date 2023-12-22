using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class PrefabPoolCollection
{
	public Dictionary<uint, PrefabPool> storage = new Dictionary<uint, PrefabPool> ();

	public void Push (GameObject instance)
	{
		Poolable component = instance.GetComponent<Poolable> ();
		if (!storage.TryGetValue (component.prefabID, out var value)) {
			value = new PrefabPool ();
			storage.Add (component.prefabID, value);
		}
		value.Push (component);
	}

	public GameObject Pop (uint id, Vector3 pos = default(Vector3), Quaternion rot = default(Quaternion))
	{
		if (storage.TryGetValue (id, out var value)) {
			return value.Pop (pos, rot);
		}
		return null;
	}

	public void Clear (string filter = null)
	{
		if (string.IsNullOrEmpty (filter)) {
			foreach (KeyValuePair<uint, PrefabPool> item in storage) {
				item.Value.Clear ();
			}
			return;
		}
		foreach (KeyValuePair<uint, PrefabPool> item2 in storage) {
			string haystack = StringPool.Get (item2.Key);
			if (haystack.Contains (filter, CompareOptions.IgnoreCase)) {
				item2.Value.Clear ();
			}
		}
	}
}
