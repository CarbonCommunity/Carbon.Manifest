using UnityEngine;
using UnityEngine.UI;

[RequireComponent (typeof(Toggle))]
public class DamageToggle : MonoBehaviour
{
	public Toggle toggle = null;

	private void Reset ()
	{
		toggle = GetComponent<Toggle> ();
	}
}
