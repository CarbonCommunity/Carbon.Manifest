#define ENABLE_PROFILER
using UnityEngine;
using UnityEngine.Profiling;

[ExecuteInEditMode]
[AddComponentMenu ("Colorful/Wiggle")]
public class CC_Wiggle : CC_Base, IImageEffect
{
	public float timer = 0f;

	public float speed = 1f;

	public float scale = 12f;

	private void Update ()
	{
		timer += speed * Time.deltaTime;
	}

	public bool IsActive ()
	{
		return base.enabled && scale != 0f;
	}

	public void OnRenderImage (RenderTexture source, RenderTexture destination)
	{
		if (scale == 0f) {
			Graphics.Blit (source, destination);
			return;
		}
		Profiler.BeginSample ("CC_Wiggle");
		base.material.SetFloat ("_timer", timer);
		base.material.SetFloat ("_scale", scale);
		Graphics.Blit (source, destination, base.material);
		Profiler.EndSample ();
	}
}
