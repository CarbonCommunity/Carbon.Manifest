#define ENABLE_PROFILER
using UnityEngine;
using UnityEngine.Profiling;
using UnityStandardAssets.ImageEffects;

[ExecuteInEditMode]
[AddComponentMenu ("Image Effects/Other/Scope Overlay")]
public class ScopeEffect : PostEffectsBase, IImageEffect
{
	public Material overlayMaterial = null;

	public override bool CheckResources ()
	{
		return true;
	}

	public bool IsActive ()
	{
		return base.enabled && CheckResources ();
	}

	public void OnRenderImage (RenderTexture source, RenderTexture destination)
	{
		Profiler.BeginSample ("ScopeEffect");
		overlayMaterial.SetVector ("_Screen", new Vector2 (Screen.width, Screen.height));
		Graphics.Blit (source, destination, overlayMaterial);
		Profiler.EndSample ();
	}
}
