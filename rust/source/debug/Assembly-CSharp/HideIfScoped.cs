using UnityEngine;

public class HideIfScoped : MonoBehaviour
{
	public Renderer[] renderers;

	public void SetVisible (bool vis)
	{
		Renderer[] array = renderers;
		foreach (Renderer renderer in array) {
			renderer.enabled = vis;
		}
	}
}
