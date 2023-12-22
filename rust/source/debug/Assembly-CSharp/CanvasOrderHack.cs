using UnityEngine;

public class CanvasOrderHack : MonoBehaviour
{
	private void OnEnable ()
	{
		Canvas[] componentsInChildren = GetComponentsInChildren<Canvas> (includeInactive: true);
		foreach (Canvas canvas in componentsInChildren) {
			if (canvas.overrideSorting) {
				canvas.sortingOrder++;
			}
		}
		Canvas[] componentsInChildren2 = GetComponentsInChildren<Canvas> (includeInactive: true);
		foreach (Canvas canvas2 in componentsInChildren2) {
			if (canvas2.overrideSorting) {
				canvas2.sortingOrder--;
			}
		}
	}
}
