using System.Collections.Generic;
using System.Linq;
using Rust.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemListTools : MonoBehaviour
{
	public GameObject categoryButton;

	public GameObject itemButton;

	public RustInput searchInputText;

	internal Button lastCategory;

	private IOrderedEnumerable<ItemDefinition> currentItems;

	private IOrderedEnumerable<ItemDefinition> allItems;

	public void OnPanelOpened ()
	{
		CacheAllItems ();
		Refresh ();
		searchInputText.InputField.ActivateInputField ();
	}

	private void OnOpenDevTools ()
	{
		searchInputText.InputField.ActivateInputField ();
	}

	private void CacheAllItems ()
	{
		if (allItems == null) {
			allItems = from x in ItemManager.GetItemDefinitions ()
				orderby x.displayName.translated
				select x;
		}
	}

	public void Refresh ()
	{
		RebuildCategories ();
	}

	private void RebuildCategories ()
	{
		for (int i = 0; i < categoryButton.transform.parent.childCount; i++) {
			Transform child = categoryButton.transform.parent.GetChild (i);
			if (!(child == categoryButton.transform)) {
				GameManager.Destroy (child.gameObject);
			}
		}
		categoryButton.SetActive (value: true);
		IEnumerable<IGrouping<ItemCategory, ItemDefinition>> source = from x in ItemManager.GetItemDefinitions ()
			group x by x.category;
		foreach (IGrouping<ItemCategory, ItemDefinition> item in source.OrderBy ((IGrouping<ItemCategory, ItemDefinition> x) => x.First ().category)) {
			GameObject gameObject = Object.Instantiate (categoryButton);
			gameObject.transform.SetParent (categoryButton.transform.parent, worldPositionStays: false);
			TextMeshProUGUI componentInChildren = gameObject.GetComponentInChildren<TextMeshProUGUI> ();
			componentInChildren.text = item.First ().category.ToString ();
			Button btn = gameObject.GetComponentInChildren<Button> ();
			ItemDefinition[] itemArray = item.ToArray ();
			btn.onClick.AddListener (delegate {
				if ((bool)lastCategory) {
					lastCategory.interactable = true;
				}
				lastCategory = btn;
				lastCategory.interactable = false;
				SwitchItemCategory (itemArray);
			});
			if (lastCategory == null) {
				lastCategory = btn;
				lastCategory.interactable = false;
				SwitchItemCategory (itemArray);
			}
		}
		categoryButton.SetActive (value: false);
	}

	private void SwitchItemCategory (ItemDefinition[] defs)
	{
		currentItems = defs.OrderBy ((ItemDefinition x) => x.displayName.translated);
		searchInputText.Text = "";
		FilterItems (null);
	}

	public void FilterItems (string searchText)
	{
		if (itemButton == null) {
			return;
		}
		for (int i = 0; i < itemButton.transform.parent.childCount; i++) {
			Transform child = itemButton.transform.parent.GetChild (i);
			if (!(child == itemButton.transform)) {
				GameManager.Destroy (child.gameObject);
			}
		}
		itemButton.SetActive (value: true);
		bool flag = !string.IsNullOrEmpty (searchText);
		string value = (flag ? searchText.ToLower () : null);
		IOrderedEnumerable<ItemDefinition> orderedEnumerable = (flag ? allItems : currentItems);
		int num = 0;
		foreach (ItemDefinition item in orderedEnumerable) {
			if (!item.hidden && (!flag || item.displayName.translated.ToLower ().Contains (value))) {
				GameObject gameObject = Object.Instantiate (itemButton);
				gameObject.transform.SetParent (itemButton.transform.parent, worldPositionStays: false);
				TextMeshProUGUI componentInChildren = gameObject.GetComponentInChildren<TextMeshProUGUI> ();
				componentInChildren.text = item.displayName.translated;
				gameObject.GetComponentInChildren<ItemButtonTools> ().itemDef = item;
				gameObject.GetComponentInChildren<ItemButtonTools> ().image.sprite = item.iconSprite;
				num++;
				if (num >= 160) {
					break;
				}
			}
		}
		itemButton.SetActive (value: false);
	}
}
