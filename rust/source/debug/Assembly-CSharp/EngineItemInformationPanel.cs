using UnityEngine;
using UnityEngine.UI;

public class EngineItemInformationPanel : ItemInformationPanel
{
	[SerializeField]
	private Text tier;

	[SerializeField]
	private Translate.Phrase low;

	[SerializeField]
	private Translate.Phrase medium;

	[SerializeField]
	private Translate.Phrase high;

	[SerializeField]
	private GameObject accelerationRoot = null;

	[SerializeField]
	private GameObject topSpeedRoot = null;

	[SerializeField]
	private GameObject fuelEconomyRoot = null;
}
