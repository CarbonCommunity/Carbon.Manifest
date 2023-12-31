using UnityEngine;

[ExecuteInEditMode]
[AddComponentMenu ("Colorful/Bleach Bypass")]
public class CC_BleachBypass : CC_Base
{
	public float amount = 1f;

	private void OnRenderImage (RenderTexture source, RenderTexture destination)
	{
		if (amount == 0f) {
			Graphics.Blit (source, destination);
			return;
		}
		base.material.SetFloat ("_amount", amount);
		Graphics.Blit (source, destination, base.material);
	}
}
