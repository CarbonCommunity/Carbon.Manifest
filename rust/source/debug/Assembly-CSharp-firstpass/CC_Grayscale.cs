#define ENABLE_PROFILER
using UnityEngine;
using UnityEngine.Profiling;

[ExecuteInEditMode]
[AddComponentMenu ("Colorful/Grayscale")]
public class CC_Grayscale : CC_Base, IImageEffect
{
	public float redLuminance = 0.3f;

	public float greenLuminance = 0.59f;

	public float blueLuminance = 0.11f;

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
		Profiler.BeginSample ("CC_Grayscale");
		base.material.SetVector ("_data", new Vector4 (redLuminance, greenLuminance, blueLuminance, amount));
		Graphics.Blit (source, destination, base.material);
		Profiler.EndSample ();
	}
}
