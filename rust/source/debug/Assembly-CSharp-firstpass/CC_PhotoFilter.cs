#define ENABLE_PROFILER
using UnityEngine;
using UnityEngine.Profiling;

[ExecuteInEditMode]
[AddComponentMenu ("Colorful/Photo Filter")]
public class CC_PhotoFilter : CC_Base, IImageEffect
{
	public Color color = new Color (1f, 0.5f, 0.2f, 1f);

	public float density = 0.35f;

	public bool IsActive ()
	{
		return base.enabled && density != 0f;
	}

	public void OnRenderImage (RenderTexture source, RenderTexture destination)
	{
		if (density == 0f) {
			Graphics.Blit (source, destination);
			return;
		}
		Profiler.BeginSample ("CC_PhotoFilter");
		base.material.SetColor ("_rgb", color);
		base.material.SetFloat ("_density", density);
		Graphics.Blit (source, destination, base.material);
		Profiler.EndSample ();
	}
}
