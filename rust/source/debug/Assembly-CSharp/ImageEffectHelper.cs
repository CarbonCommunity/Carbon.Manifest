using UnityEngine;

public static class ImageEffectHelper
{
	public static bool supportsDX11 => SystemInfo.graphicsShaderLevel >= 50 && SystemInfo.supportsComputeShaders;

	public static bool IsSupported (Shader s, bool needDepth, bool needHdr, MonoBehaviour effect)
	{
		if ((Object)(object)s == (Object)null || !s.isSupported) {
			Debug.LogWarningFormat ("Missing shader for image effect {0}", new object[1] { effect });
			return false;
		}
		if (!SystemInfo.supportsImageEffects || !SystemInfo.supportsRenderTextures) {
			Debug.LogWarningFormat ("Image effects aren't supported on this device ({0})", new object[1] { effect });
			return false;
		}
		if (needDepth && !SystemInfo.SupportsRenderTextureFormat ((RenderTextureFormat)1)) {
			Debug.LogWarningFormat ("Depth textures aren't supported on this device ({0})", new object[1] { effect });
			return false;
		}
		if (needHdr && !SystemInfo.SupportsRenderTextureFormat ((RenderTextureFormat)2)) {
			Debug.LogWarningFormat ("Floating point textures aren't supported on this device ({0})", new object[1] { effect });
			return false;
		}
		return true;
	}

	public static Material CheckShaderAndCreateMaterial (Shader s)
	{
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Expected O, but got Unknown
		if ((Object)(object)s == (Object)null || !s.isSupported) {
			return null;
		}
		Material val = new Material (s);
		((Object)val).hideFlags = (HideFlags)52;
		return val;
	}
}
