#define ENABLE_PROFILER
using UnityEngine;
using UnityEngine.Profiling;

[ExecuteInEditMode]
[AddComponentMenu ("Colorful/Frost")]
public class CC_Frost : CC_Base, IImageEffect
{
	public float scale = 1.2f;

	public float sharpness = 40f;

	public float darkness = 35f;

	public bool enableVignette = true;

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
		Profiler.BeginSample ("CC_Frost");
		base.material.SetFloat ("_scale", scale);
		base.material.SetFloat ("_sharpness", sharpness * 0.01f);
		base.material.SetFloat ("_darkness", darkness * 0.02f);
		Graphics.Blit (source, destination, base.material, enableVignette ? 1 : 0);
		Profiler.EndSample ();
	}
}
