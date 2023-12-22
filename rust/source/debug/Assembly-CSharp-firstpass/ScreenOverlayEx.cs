#define ENABLE_PROFILER
using UnityEngine;
using UnityEngine.Profiling;
using UnityStandardAssets.ImageEffects;

[ExecuteInEditMode]
public class ScreenOverlayEx : PostEffectsBase, IImageEffect
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

	public Texture normals = null;

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
		Profiler.BeginSample ("ScreenOverlayEx");
		Vector4 value = new Vector4 (1f, 0f, 0f, 1f);
		overlayMaterial.SetVector ("_UV_Transform", value);
		overlayMaterial.SetFloat ("_Intensity", intensity);
		if ((bool)TOD_Sky.Instance) {
			overlayMaterial.SetVector ("_LightDir", base.transform.InverseTransformDirection (TOD_Sky.Instance.LightDirection));
			overlayMaterial.SetColor ("_LightCol", TOD_Sky.Instance.LightColor * TOD_Sky.Instance.LightIntensity);
		}
		if ((bool)texture) {
			overlayMaterial.SetTexture ("_Overlay", texture);
		}
		if ((bool)normals) {
			overlayMaterial.SetTexture ("_Normals", normals);
		}
		Graphics.Blit (source, destination, overlayMaterial, (int)blendMode);
		Profiler.EndSample ();
	}
}
