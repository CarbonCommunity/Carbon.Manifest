using UnityEngine;
using UnityStandardAssets.ImageEffects;

[ExecuteInEditMode]
[AddComponentMenu ("Image Effects/Blur/Blur (Optimized)")]
public class BlurOptimized : PostEffectsBase, IImageEffect
{
	public enum BlurType
	{
		StandardGauss,
		SgxGauss
	}

	[Range (0f, 2f)]
	public int downsample = 1;

	[Range (0f, 10f)]
	public float blurSize = 3f;

	[Range (1f, 4f)]
	public int blurIterations = 2;

	public float fadeToBlurDistance;

	public BlurType blurType;

	public Shader blurShader;

	private Material blurMaterial;

	public override bool CheckResources ()
	{
		CheckSupport (needDepth: false);
		blurMaterial = CheckShaderAndCreateMaterial (blurShader, blurMaterial);
		if (!isSupported) {
			ReportAutoDisable ();
		}
		return isSupported;
	}

	public void OnDisable ()
	{
		if ((bool)blurMaterial) {
			Object.DestroyImmediate (blurMaterial);
		}
	}

	public bool IsActive ()
	{
		if (base.enabled) {
			return CheckResources ();
		}
		return false;
	}

	public void OnRenderImage (RenderTexture source, RenderTexture destination)
	{
		if (!CheckResources ()) {
			Graphics.Blit (source, destination);
			return;
		}
		float num = 1f / (1f * (float)(1 << downsample));
		float z = 1f / Mathf.Clamp (fadeToBlurDistance, 0.001f, 10000f);
		blurMaterial.SetVector ("_Parameter", new Vector4 (blurSize * num, (0f - blurSize) * num, z, 0f));
		source.filterMode = FilterMode.Bilinear;
		int width = source.width >> downsample;
		int height = source.height >> downsample;
		RenderTexture renderTexture = RenderTexture.GetTemporary (width, height, 0, source.format);
		renderTexture.filterMode = FilterMode.Bilinear;
		Graphics.Blit (source, renderTexture, blurMaterial, 0);
		int num2 = ((blurType != 0) ? 2 : 0);
		for (int i = 0; i < blurIterations; i++) {
			float num3 = (float)i * 1f;
			blurMaterial.SetVector ("_Parameter", new Vector4 (blurSize * num + num3, (0f - blurSize) * num - num3, z, 0f));
			RenderTexture temporary = RenderTexture.GetTemporary (width, height, 0, source.format);
			temporary.filterMode = FilterMode.Bilinear;
			Graphics.Blit (renderTexture, temporary, blurMaterial, 1 + num2);
			RenderTexture.ReleaseTemporary (renderTexture);
			renderTexture = temporary;
			temporary = RenderTexture.GetTemporary (width, height, 0, source.format);
			temporary.filterMode = FilterMode.Bilinear;
			Graphics.Blit (renderTexture, temporary, blurMaterial, 2 + num2);
			RenderTexture.ReleaseTemporary (renderTexture);
			renderTexture = temporary;
		}
		if (fadeToBlurDistance <= 0f) {
			Graphics.Blit (renderTexture, destination);
		} else {
			blurMaterial.SetTexture ("_Source", source);
			Graphics.Blit (renderTexture, destination, blurMaterial, 5);
		}
		RenderTexture.ReleaseTemporary (renderTexture);
	}
}
