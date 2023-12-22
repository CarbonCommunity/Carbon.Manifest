using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIMapVendingMachineMarkerCluster : MonoBehaviour
{
	public List<UIMapVendingMachineMarker> markers = new List<UIMapVendingMachineMarker> ();

	public GameObject OpenRoot = null;

	public TextMeshProUGUI CountText = null;
}
