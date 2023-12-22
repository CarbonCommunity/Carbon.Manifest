#define ENABLE_PROFILER
using UnityEngine;
using UnityEngine.Profiling;
using UnityStandardAssets.ImageEffects;

[ExecuteInEditMode]
[AddComponentMenu ("Image Effects/Other/Screen Overlay")]
public class ScreenOverlay : PostEffectsBase, IImageEffect
{
	public enum OverlayBlendMode
	{
		Additive,
		ScreenBlend,
		Multiply,
		Overlay,
		AlphaBlend
	}

	public OverlayBlendMode blendMode = OverlayBlendMode.Overlay;

	public float intensity = 1f;

	public Texture texture = null;

	public Shader overlayShader = null;

	private Material overlayMaterial = null;

	public override bool CheckResources ()
	{
		CheckSupport (needDepth: false);
		overlayMaterial = CheckShaderAndCreateMaterial (overlayShader, overlayMaterial);
		if (!isSupported) {
			ReportAutoDisable ();
		}
		return isSupported;
	}

	public bool IsActive ()
	{
		return base.enabled && CheckResources ();
	}

	public void OnRenderImage (RenderTexture source, RenderTexture destination)
	{
		if (!CheckResources ()) {
			Graphics.Blit (source, destination);
			return;
		}
		Profiler.BeginSample ("ScreenOverlay");
		Vector4 value = new Vector4 (1f, 0f, 0f, 1f);
		overlayMaterial.SetVector ("_UV_Transform", value);
		overlayMaterial.SetFloat ("_Intensity", intensity);
		overlayMaterial.SetTexture ("_Overlay", texture);
		Graphics.Blit (source, destination, overlayMaterial, (int)blendMode);
		Profiler.EndSample ();
	}
}
