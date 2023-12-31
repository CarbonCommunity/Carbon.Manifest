using UnityEngine;
using UnityEngine.Events;

public class EnableDisableEvent : MonoBehaviour
{
	[SerializeField]
	private UnityEvent enableEvent;

	[SerializeField]
	private UnityEvent disableEvent;

	protected void OnEnable ()
	{
		if (enableEvent != null) {
			enableEvent.Invoke ();
		}
	}

	protected void OnDisable ()
	{
		if (disableEvent != null) {
			disableEvent.Invoke ();
		}
	}
}
