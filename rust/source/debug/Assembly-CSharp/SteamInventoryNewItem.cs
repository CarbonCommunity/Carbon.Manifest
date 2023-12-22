using System.Threading.Tasks;
using Rust.UI;
using UnityEngine;

public class SteamInventoryNewItem : MonoBehaviour
{
	public async Task Open (IPlayerItem item)
	{
		((Component)this).gameObject.SetActive (true);
		((Component)this).GetComponentInChildren<Rust.UI.SteamInventoryItem> ().Setup (item);
		while (Object.op_Implicit ((Object)(object)this) && ((Component)this).gameObject.activeSelf) {
			await Task.Delay (100);
		}
	}
}
