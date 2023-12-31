using System.Threading.Tasks;
using Rust.UI;
using UnityEngine;

public class SteamInventoryNewItem : MonoBehaviour
{
	public async Task Open (IPlayerItem item)
	{
		base.gameObject.SetActive (value: true);
		GetComponentInChildren<Rust.UI.SteamInventoryItem> ().Setup (item);
		while ((bool)this && base.gameObject.activeSelf) {
			await Task.Delay (100);
		}
	}
}
