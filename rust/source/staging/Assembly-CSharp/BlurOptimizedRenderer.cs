using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;

public class BlurOptimizedRenderer : PostProcessEffectRenderer<BlurOptimized>
{
	private int dataProperty = Shader.PropertyToID ("_data");

	private Shader blurShader;

	public override void Init ()
	{
		base.Init ();
		blurShader = Shader.Find ("Hidden/PostProcessing/BlurOptimized");
	}

	public override void Render (PostProcessRenderContext context)
	{
		CommandBuffer command = context.command;
		command.BeginSample ("BlurOptimized");
		int value = base.settings.downsample.value;
		float value2 = base.settings.fadeToBlurDistance.value;
		float value3 = base.settings.blurSize.value;
		int value4 = base.settings.blurIterations.value;
		BlurType value5 = base.settings.blurType.value;
		float num = 1f / (1f * (float)(1 << value));
		float z = 1f / Mathf.Clamp (value2, 0.001f, 10000f);
		PropertySheet propertySheet = context.propertySheets.Get (blurShader);
		propertySheet.properties.SetVector ("_Parameter", new Vector4 (value3 * num, (0f - value3) * num, z, 0f));
		int width = context.width >> value;
		int height = context.height >> value;
		int num2 = Shader.PropertyToID ("_BlurRT1");
		int num3 = Shader.PropertyToID ("_BlurRT2");
		command.GetTemporaryRT (num2, width, height, 0, FilterMode.Bilinear, context.sourceFormat, RenderTextureReadWrite.Default);
		command.BlitFullscreenTriangle (context.source, num2, propertySheet, 0);
		int num4 = ((value5 != 0) ? 2 : 0);
		for (int i = 0; i < value4; i++) {
			float num5 = (float)i * 1f;
			propertySheet.properties.SetVector ("_Parameter", new Vector4 (value3 * num + num5, (0f - value3) * num - num5, z, 0f));
			command.GetTemporaryRT (num3, width, height, 0, FilterMode.Bilinear, context.sourceFormat);
			command.BlitFullscreenTriangle (num2, num3, propertySheet, 1 + num4);
			command.ReleaseTemporaryRT (num2);
			command.GetTemporaryRT (num2, width, height, 0, FilterMode.Bilinear, context.sourceFormat);
			command.BlitFullscreenTriangle (num3, num2, propertySheet, 2 + num4);
			command.ReleaseTemporaryRT (num3);
		}
		if (value2 <= 0f) {
			command.BlitFullscreenTriangle (num2, context.destination);
		} else {
			command.SetGlobalTexture ("_Source", context.source);
			command.BlitFullscreenTriangle (num2, context.destination, propertySheet, 5);
		}
		command.ReleaseTemporaryRT (num2);
		command.EndSample ("BlurOptimized");
	}
}
