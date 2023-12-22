using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.Scripting;

[Preserve]
internal sealed class GrainRenderer : PostProcessEffectRenderer<Grain>
{
	private RenderTexture m_GrainLookupRT;

	private const int k_SampleCount = 1024;

	private int m_SampleIndex;

	public override void Render (PostProcessRenderContext context)
	{
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		//IL_0164: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Expected O, but got Unknown
		//IL_0201: Unknown result type (might be due to invalid IL or missing references)
		//IL_0206: Unknown result type (might be due to invalid IL or missing references)
		//IL_0269: Unknown result type (might be due to invalid IL or missing references)
		float realtimeSinceStartup = Time.realtimeSinceStartup;
		float num = HaltonSeq.Get (m_SampleIndex & 0x3FF, 2);
		float num2 = HaltonSeq.Get (m_SampleIndex & 0x3FF, 3);
		if (++m_SampleIndex >= 1024) {
			m_SampleIndex = 0;
		}
		if ((Object)(object)m_GrainLookupRT == (Object)null || !m_GrainLookupRT.IsCreated ()) {
			RuntimeUtilities.Destroy ((Object)(object)m_GrainLookupRT);
			m_GrainLookupRT = new RenderTexture (128, 128, 0, GetLookupFormat ()) {
				filterMode = (FilterMode)1,
				wrapMode = (TextureWrapMode)0,
				anisoLevel = 0,
				name = "Grain Lookup Texture"
			};
			m_GrainLookupRT.Create ();
		}
		PropertySheet propertySheet = context.propertySheets.Get (context.resources.shaders.grainBaker);
		propertySheet.properties.Clear ();
		propertySheet.properties.SetFloat (ShaderIDs.Phase, realtimeSinceStartup % 10f);
		propertySheet.properties.SetVector (ShaderIDs.GrainNoiseParameters, Vector4.op_Implicit (new Vector3 (12.9898f, 78.233f, 43758.5469f)));
		context.command.BeginSample ("GrainLookup");
		context.command.BlitFullscreenTriangle (RenderTargetIdentifier.op_Implicit ((BuiltinRenderTextureType)0), RenderTargetIdentifier.op_Implicit ((Texture)(object)m_GrainLookupRT), propertySheet, base.settings.colored.value ? 1 : 0);
		context.command.EndSample ("GrainLookup");
		PropertySheet uberSheet = context.uberSheet;
		uberSheet.EnableKeyword ("GRAIN");
		uberSheet.properties.SetTexture (ShaderIDs.GrainTex, (Texture)(object)m_GrainLookupRT);
		uberSheet.properties.SetVector (ShaderIDs.Grain_Params1, Vector4.op_Implicit (new Vector2 (base.settings.lumContrib.value, base.settings.intensity.value * 20f)));
		uberSheet.properties.SetVector (ShaderIDs.Grain_Params2, new Vector4 ((float)context.width / (float)((Texture)m_GrainLookupRT).width / base.settings.size.value, (float)context.height / (float)((Texture)m_GrainLookupRT).height / base.settings.size.value, num, num2));
	}

	private RenderTextureFormat GetLookupFormat ()
	{
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		if (((RenderTextureFormat)2).IsSupported ()) {
			return (RenderTextureFormat)2;
		}
		return (RenderTextureFormat)0;
	}

	public override void Release ()
	{
		RuntimeUtilities.Destroy ((Object)(object)m_GrainLookupRT);
		m_GrainLookupRT = null;
		m_SampleIndex = 0;
	}
}
