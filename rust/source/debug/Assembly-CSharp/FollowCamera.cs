#define ENABLE_PROFILER
using UnityEngine;
using UnityEngine.Profiling;

public class FollowCamera : MonoBehaviour, IClientComponent
{
	private void LateUpdate ()
	{
		if (!(MainCamera.mainCamera == null)) {
			Profiler.BeginSample ("FollowCamera");
			base.transform.position = MainCamera.position;
			Profiler.EndSample ();
		}
	}
}
