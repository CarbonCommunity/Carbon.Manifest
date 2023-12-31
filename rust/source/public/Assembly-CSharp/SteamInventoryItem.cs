using Facepunch.Extend;
using Rust.UI;
using TMPro;
using UnityEngine;

public class SteamInventoryItem : MonoBehaviour
{
	public IPlayerItem Item;

	public HttpImage Image;

	public bool Setup (IPlayerItem item)
	{
		Item = item;
		if (item.GetDefinition () == null) {
			return false;
		}
		base.transform.FindChildRecursive ("ItemName").GetComponent<TextMeshProUGUI> ().text = item.GetDefinition ().Name;
		return Image.Load (item.GetDefinition ().IconUrl);
	}
}
