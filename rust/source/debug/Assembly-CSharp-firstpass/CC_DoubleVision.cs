#define ENABLE_PROFILER
using UnityEngine;
using UnityEngine.Profiling;

[ExecuteInEditMode]
[AddComponentMenu ("Colorful/Double Vision")]
public class CC_DoubleVision : CC_Base, IImageEffect
{
	public Vector2 displace = new Vector2 (0.7f, 0f);

	public float amount = 1f;

	public bool IsActive ()
	{
		return base.enabled && amount != 0f;
	}

	public void OnRenderImage (RenderTexture source, RenderTexture destination)
	{
		if (amount == 0f) {
			Graphics.Blit (source, destination);
			return;
		}
		Profiler.BeginSample ("CC_DoubleVision");
		base.material.SetVector ("_displace", new Vector2 (displace.x / (float)Screen.width, displace.y / (float)Screen.height));
		base.material.SetFloat ("_amount", amount);
		Graphics.Blit (source, destination, base.material);
		Profiler.EndSample ();
	}
}
