using UnityEngine;

[ExecuteInEditMode]
[AddComponentMenu ("Colorful/Channel Mixer")]
public class CC_ChannelMixer : CC_Base
{
	public float redR = 100f;

	public float redG = 0f;

	public float redB = 0f;

	public float greenR = 0f;

	public float greenG = 100f;

	public float greenB = 0f;

	public float blueR = 0f;

	public float blueG = 0f;

	public float blueB = 100f;

	public float constantR = 0f;

	public float constantG = 0f;

	public float constantB = 0f;

	private void OnRenderImage (RenderTexture source, RenderTexture destination)
	{
		base.material.SetVector ("_red", new Vector4 (redR * 0.01f, greenR * 0.01f, blueR * 0.01f));
		base.material.SetVector ("_green", new Vector4 (redG * 0.01f, greenG * 0.01f, blueG * 0.01f));
		base.material.SetVector ("_blue", new Vector4 (redB * 0.01f, greenB * 0.01f, blueB * 0.01f));
		base.material.SetVector ("_constant", new Vector4 (constantR * 0.01f, constantG * 0.01f, constantB * 0.01f));
		Graphics.Blit (source, destination, base.material);
	}
}
