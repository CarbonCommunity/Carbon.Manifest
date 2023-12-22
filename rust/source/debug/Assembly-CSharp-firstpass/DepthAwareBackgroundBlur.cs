using UnityEngine;
using UnityStandardAssets.ImageEffects;

[ExecuteInEditMode]
[RequireComponent (typeof(Camera))]
[AddComponentMenu ("Image Effects/Camera/Depth-aware Background Blur")]
public class DepthAwareBackgroundBlur : DepthOfField
{
	public float StartDistance = 20f;

	public float BlurSize = 5f;

	public BlurSampleCount BlurQuality = BlurSampleCount.Low;

	private void Awake ()
	{
		base.ForceOnlyFarBlur = true;
		focalLength = StartDistance;
		maxBlurSize = BlurSize;
		blurSampleCount = BlurQuality;
	}
}
