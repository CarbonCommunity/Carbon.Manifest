using UnityEngine;

public class EggSwap : MonoBehaviour
{
	public Renderer[] eggRenderers;

	public void Show (int index)
	{
		HideAll ();
		eggRenderers [index].enabled = true;
	}

	public void HideAll ()
	{
		Renderer[] array = eggRenderers;
		foreach (Renderer renderer in array) {
			renderer.enabled = false;
		}
	}
}
