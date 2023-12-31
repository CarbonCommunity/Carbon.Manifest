using UnityEngine;

public class TriggerNotify : TriggerBase, IPrefabPreProcess
{
	public GameObject notifyTarget;

	private INotifyTrigger toNotify = null;

	public bool runClientside = true;

	public bool runServerside = true;

	public bool HasContents => contents != null && contents.Count > 0;

	internal override void OnObjects ()
	{
		base.OnObjects ();
		if (toNotify != null || (notifyTarget != null && notifyTarget.TryGetComponent<INotifyTrigger> (out toNotify))) {
			toNotify.OnObjects (this);
		}
	}

	internal override void OnEmpty ()
	{
		base.OnEmpty ();
		if (toNotify != null || (notifyTarget != null && notifyTarget.TryGetComponent<INotifyTrigger> (out toNotify))) {
			toNotify.OnEmpty ();
		}
	}

	public void PreProcess (IPrefabProcessor preProcess, GameObject rootObj, string name, bool serverside, bool clientside, bool bundling)
	{
		if ((!clientside || !runClientside) && (!serverside || !runServerside)) {
			preProcess.RemoveComponent (this);
		}
	}
}
