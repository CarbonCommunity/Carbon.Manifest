#define UNITY_ASSERTIONS
using System;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.Scripting;

[Serializable]
[Preserve]
internal sealed class Dithering
{
	private int m_NoiseTextureIndex = 0;

	internal void Render (PostProcessRenderContext context)
	{
		Texture2D[] blueNoise = context.resources.blueNoise64;
		Assert.IsTrue (blueNoise != null && blueNoise.Length != 0);
		if (++m_NoiseTextureIndex >= blueNoise.Length) {
			m_NoiseTextureIndex = 0;
		}
		float value = UnityEngine.Random.value;
		float value2 = UnityEngine.Random.value;
		Texture2D texture2D = blueNoise [m_NoiseTextureIndex];
		PropertySheet uberSheet = context.uberSheet;
		uberSheet.properties.SetTexture (ShaderIDs.DitheringTex, texture2D);
		uberSheet.properties.SetVector (ShaderIDs.Dithering_Coords, new Vector4 ((float)context.screenWidth / (float)texture2D.width, (float)context.screenHeight / (float)texture2D.height, value, value2));
	}
}
