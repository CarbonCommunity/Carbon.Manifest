using UnityEngine;

[ExecuteInEditMode]
[AddComponentMenu ("Colorful/Levels")]
public class CC_Levels : CC_Base
{
	public int mode = 0;

	public float inputMinL = 0f;

	public float inputMaxL = 255f;

	public float inputGammaL = 1f;

	public float inputMinR = 0f;

	public float inputMaxR = 255f;

	public float inputGammaR = 1f;

	public float inputMinG = 0f;

	public float inputMaxG = 255f;

	public float inputGammaG = 1f;

	public float inputMinB = 0f;

	public float inputMaxB = 255f;

	public float inputGammaB = 1f;

	public float outputMinL = 0f;

	public float outputMaxL = 255f;

	public float outputMinR = 0f;

	public float outputMaxR = 255f;

	public float outputMinG = 0f;

	public float outputMaxG = 255f;

	public float outputMinB = 0f;

	public float outputMaxB = 255f;

	private void OnRenderImage (RenderTexture source, RenderTexture destination)
	{
		if (mode == 0) {
			base.material.SetVector ("_inputMin", new Vector4 (inputMinL / 255f, inputMinL / 255f, inputMinL / 255f, 1f));
			base.material.SetVector ("_inputMax", new Vector4 (inputMaxL / 255f, inputMaxL / 255f, inputMaxL / 255f, 1f));
			base.material.SetVector ("_inputGamma", new Vector4 (inputGammaL, inputGammaL, inputGammaL, 1f));
			base.material.SetVector ("_outputMin", new Vector4 (outputMinL / 255f, outputMinL / 255f, outputMinL / 255f, 1f));
			base.material.SetVector ("_outputMax", new Vector4 (outputMaxL / 255f, outputMaxL / 255f, outputMaxL / 255f, 1f));
		} else {
			base.material.SetVector ("_inputMin", new Vector4 (inputMinR / 255f, inputMinG / 255f, inputMinB / 255f, 1f));
			base.material.SetVector ("_inputMax", new Vector4 (inputMaxR / 255f, inputMaxG / 255f, inputMaxB / 255f, 1f));
			base.material.SetVector ("_inputGamma", new Vector4 (inputGammaR, inputGammaG, inputGammaB, 1f));
			base.material.SetVector ("_outputMin", new Vector4 (outputMinR / 255f, outputMinG / 255f, outputMinB / 255f, 1f));
			base.material.SetVector ("_outputMax", new Vector4 (outputMaxR / 255f, outputMaxG / 255f, outputMaxB / 255f, 1f));
		}
		Graphics.Blit (source, destination, base.material);
	}
}
