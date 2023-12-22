using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DevDressPlayer : MonoBehaviour
{
	public bool DressRandomly;

	public List<ItemAmount> clothesToWear;

	private void ServerInitComponent ()
	{
		BasePlayer component = GetComponent<BasePlayer> ();
		if (DressRandomly) {
			DoRandomClothes (component);
		}
		foreach (ItemAmount item2 in clothesToWear) {
			if (!(item2.itemDef == null)) {
				Item item = ItemManager.Create (item2.itemDef, 1, 0uL);
				item.MoveToContainer (component.inventory.containerWear);
			}
		}
	}

	private void DoRandomClothes (BasePlayer player)
	{
		string text = "";
		IEnumerable<ItemDefinition> enumerable = (from x in ItemManager.GetItemDefinitions ()
			where x.GetComponent<ItemModWearable> ()
			orderby Guid.NewGuid ()
			select x).Take (UnityEngine.Random.Range (0, 4));
		foreach (ItemDefinition item2 in enumerable) {
			Item item = ItemManager.Create (item2, 1, 0uL);
			item.MoveToContainer (player.inventory.containerWear);
			text = text + item2.shortname + " ";
		}
		text = text.Trim ();
		if (text == "") {
			text = "naked";
		}
		player.displayName = text;
	}
}
