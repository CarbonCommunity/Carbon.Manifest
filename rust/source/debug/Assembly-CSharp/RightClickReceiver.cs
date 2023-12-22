using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class RightClickReceiver : MonoBehaviour, IPointerClickHandler, IEventSystemHandler
{
	public UnityEvent ClickReceiver;

	public void OnPointerClick (PointerEventData eventData)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Invalid comparison between Unknown and I4
		if ((int)eventData.button == 1) {
			UnityEvent clickReceiver = ClickReceiver;
			if (clickReceiver != null) {
				clickReceiver.Invoke ();
			}
		}
	}
}
