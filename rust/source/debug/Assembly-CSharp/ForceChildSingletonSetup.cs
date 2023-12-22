using UnityEngine;

public class ForceChildSingletonSetup : MonoBehaviour
{
	[ComponentHelp ("Any child objects of this object that contain SingletonComponents will be registered - even if they're not enabled")]
	private void Awake ()
	{
		SingletonComponent[] componentsInChildren = GetComponentsInChildren<SingletonComponent> (includeInactive: true);
		foreach (SingletonComponent singletonComponent in componentsInChildren) {
			singletonComponent.SingletonSetup ();
		}
	}
}
