using System;
using Rust;
using UnityEngine;

public class SwapArrows : MonoBehaviour, IClientComponent
{
	public enum ArrowType
	{
		One,
		Two,
		Three,
		Four
	}

	public GameObject[] arrowModels;

	[NonSerialized]
	private string curAmmoType = "";

	private bool wasHidden = false;

	public void SelectArrowType (int iType)
	{
		HideAllArrowHeads ();
		if (iType < arrowModels.Length) {
			arrowModels [iType].SetActive (value: true);
		}
	}

	public void HideAllArrowHeads ()
	{
		GameObject[] array = arrowModels;
		foreach (GameObject gameObject in array) {
			gameObject.SetActive (value: false);
		}
	}

	public void UpdateAmmoType (ItemDefinition ammoType, bool hidden = false)
	{
		if (hidden) {
			wasHidden = hidden;
			HideAllArrowHeads ();
		} else if (!(curAmmoType == ammoType.shortname) || hidden != wasHidden) {
			curAmmoType = ammoType.shortname;
			wasHidden = hidden;
			switch (curAmmoType) {
			default:
				HideAllArrowHeads ();
				break;
			case "arrow.bone":
				SelectArrowType (0);
				break;
			case "arrow.fire":
				SelectArrowType (1);
				break;
			case "arrow.hv":
				SelectArrowType (2);
				break;
			case "ammo_arrow_poison":
				SelectArrowType (3);
				break;
			case "ammo_arrow_stone":
				SelectArrowType (4);
				break;
			}
		}
	}

	private void Cleanup ()
	{
		HideAllArrowHeads ();
		curAmmoType = "";
	}

	public void OnDisable ()
	{
		if (!Rust.Application.isQuitting) {
			Cleanup ();
		}
	}

	public void OnEnable ()
	{
		Cleanup ();
	}
}
