using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.Scripting;

[Serializable]
[Preserve]
public sealed class TemporalAntialiasing
{
	private enum Pass
	{
		SolverDilate,
		SolverNoDilate
	}

	[Tooltip ("The diameter (in texels) inside which jitter samples are spread. Smaller values result in crisper but more aliased output, while larger values result in more stable, but blurrier, output.")]
	[Range (0.1f, 1f)]
	public float jitterSpread = 0.75f;

	[Tooltip ("Controls the amount of sharpening applied to the color buffer. High values may introduce dark-border artifacts.")]
	[Range (0f, 3f)]
	public float sharpness = 0.25f;

	[Tooltip ("The blend coefficient for a stationary fragment. Controls the percentage of history sample blended into the final color.")]
	[Range (0f, 0.99f)]
	public float stationaryBlending = 0.95f;

	[Tooltip ("The blend coefficient for a fragment with significant motion. Controls the percentage of history sample blended into the final color.")]
	[Range (0f, 0.99f)]
	public float motionBlending = 0.85f;

	public Func<Camera, Vector2, Matrix4x4> jitteredMatrixFunc;

	private readonly RenderTargetIdentifier[] m_Mrt = (RenderTargetIdentifier[])(object)new RenderTargetIdentifier[2];

	private bool m_ResetHistory = true;

	private const int k_NumEyes = 2;

	private const int k_NumHistoryTextures = 2;

	private readonly RenderTexture[][] m_HistoryTextures = new RenderTexture[2][];

	private readonly int[] m_HistoryPingPong = new int[2];

	public Vector2 jitter { get; private set; }

	public Vector2 jitterRaw { get; private set; }

	public int sampleIndex { get; private set; }

	public int sampleCount { get; set; }

	public bool IsSupported ()
	{
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Invalid comparison between Unknown and I4
		return SystemInfo.supportedRenderTargetCount >= 2 && SystemInfo.supportsMotionVectors && (int)SystemInfo.graphicsDeviceType != 8;
	}

	internal DepthTextureMode GetCameraFlags ()
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		return (DepthTextureMode)5;
	}

	internal void ResetHistory ()
	{
		m_ResetHistory = true;
	}

	private Vector2 GenerateRandomOffset ()
	{
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		Vector2 result = default(Vector2);
		((Vector2)(ref result))..ctor (HaltonSeq.Get ((sampleIndex & 0x3FF) + 1, 2) - 0.5f, HaltonSeq.Get ((sampleIndex & 0x3FF) + 1, 3) - 0.5f);
		if (++sampleIndex >= sampleCount) {
			sampleIndex = 0;
		}
		return result;
	}

	public Matrix4x4 GetJitteredProjectionMatrix (Camera camera)
	{
		//IL_0003: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		jitter = GenerateRandomOffset ();
		jitter *= jitterSpread;
		Matrix4x4 result = ((jitteredMatrixFunc == null) ? (camera.orthographic ? RuntimeUtilities.GetJitteredOrthographicProjectionMatrix (camera, jitter) : RuntimeUtilities.GetJitteredPerspectiveProjectionMatrix (camera, jitter)) : jitteredMatrixFunc (camera, jitter));
		jitterRaw = jitter;
		jitter = new Vector2 (jitter.x / (float)camera.pixelWidth, jitter.y / (float)camera.pixelHeight);
		return result;
	}

	public void ConfigureJitteredProjectionMatrix (PostProcessRenderContext context)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		Camera camera = context.camera;
		camera.nonJitteredProjectionMatrix = camera.projectionMatrix;
		camera.projectionMatrix = GetJitteredProjectionMatrix (camera);
		camera.useJitteredProjectionMatrixForTransparentRendering = true;
	}

	public void ConfigureStereoJitteredProjectionMatrices (PostProcessRenderContext context)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Invalid comparison between Unknown and I4
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		Camera camera = context.camera;
		jitter = GenerateRandomOffset ();
		jitter *= jitterSpread;
		for (StereoscopicEye val = (StereoscopicEye)0; (int)val <= 1; val = (StereoscopicEye)(val + 1)) {
			context.camera.CopyStereoDeviceProjectionMatrixToNonJittered (val);
			Matrix4x4 stereoNonJitteredProjectionMatrix = context.camera.GetStereoNonJitteredProjectionMatrix (val);
			Matrix4x4 val2 = RuntimeUtilities.GenerateJitteredProjectionMatrixFromOriginal (context, stereoNonJitteredProjectionMatrix, jitter);
			context.camera.SetStereoProjectionMatrix (val, val2);
		}
		jitter = new Vector2 (jitter.x / (float)context.screenWidth, jitter.y / (float)context.screenHeight);
		camera.useJitteredProjectionMatrixForTransparentRendering = true;
	}

	private void GenerateHistoryName (RenderTexture rt, int id, PostProcessRenderContext context)
	{
		((Object)rt).name = "Temporal Anti-aliasing History id #" + id;
		if (context.stereoActive) {
			((Object)rt).name = ((Object)rt).name + " for eye " + context.xrActiveEye;
		}
	}

	private RenderTexture CheckHistory (int id, PostProcessRenderContext context)
	{
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		int xrActiveEye = context.xrActiveEye;
		if (m_HistoryTextures [xrActiveEye] == null) {
			m_HistoryTextures [xrActiveEye] = (RenderTexture[])(object)new RenderTexture[2];
		}
		RenderTexture val = m_HistoryTextures [xrActiveEye] [id];
		if (m_ResetHistory || (Object)(object)val == (Object)null || !val.IsCreated ()) {
			RenderTexture.ReleaseTemporary (val);
			val = context.GetScreenSpaceTemporaryRT (0, context.sourceFormat, (RenderTextureReadWrite)0);
			GenerateHistoryName (val, id, context);
			((Texture)val).filterMode = (FilterMode)1;
			m_HistoryTextures [xrActiveEye] [id] = val;
			context.command.BlitFullscreenTriangle (context.source, RenderTargetIdentifier.op_Implicit ((Texture)(object)val));
		} else if (((Texture)val).width != context.width || ((Texture)val).height != context.height) {
			RenderTexture screenSpaceTemporaryRT = context.GetScreenSpaceTemporaryRT (0, context.sourceFormat, (RenderTextureReadWrite)0);
			GenerateHistoryName (screenSpaceTemporaryRT, id, context);
			((Texture)screenSpaceTemporaryRT).filterMode = (FilterMode)1;
			m_HistoryTextures [xrActiveEye] [id] = screenSpaceTemporaryRT;
			context.command.BlitFullscreenTriangle (RenderTargetIdentifier.op_Implicit ((Texture)(object)val), RenderTargetIdentifier.op_Implicit ((Texture)(object)screenSpaceTemporaryRT));
			RenderTexture.ReleaseTemporary (val);
		}
		return m_HistoryTextures [xrActiveEye] [id];
	}

	internal void Render (PostProcessRenderContext context)
	{
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		PropertySheet propertySheet = context.propertySheets.Get (context.resources.shaders.temporalAntialiasing);
		CommandBuffer command = context.command;
		command.BeginSample ("TemporalAntialiasing");
		int num = m_HistoryPingPong [context.xrActiveEye];
		RenderTexture val = CheckHistory (++num % 2, context);
		RenderTexture val2 = CheckHistory (++num % 2, context);
		m_HistoryPingPong [context.xrActiveEye] = ++num % 2;
		propertySheet.properties.SetVector (ShaderIDs.Jitter, Vector4.op_Implicit (jitter));
		propertySheet.properties.SetFloat (ShaderIDs.Sharpness, sharpness);
		propertySheet.properties.SetVector (ShaderIDs.FinalBlendParameters, new Vector4 (stationaryBlending, motionBlending, 6000f, 0f));
		propertySheet.properties.SetTexture (ShaderIDs.HistoryTex, (Texture)(object)val);
		int pass = (context.camera.orthographic ? 1 : 0);
		m_Mrt [0] = context.destination;
		m_Mrt [1] = RenderTargetIdentifier.op_Implicit ((Texture)(object)val2);
		command.BlitFullscreenTriangle (context.source, m_Mrt, context.source, propertySheet, pass);
		command.EndSample ("TemporalAntialiasing");
		m_ResetHistory = false;
	}

	internal void Release ()
	{
		if (m_HistoryTextures != null) {
			for (int i = 0; i < m_HistoryTextures.Length; i++) {
				if (m_HistoryTextures [i] != null) {
					for (int j = 0; j < m_HistoryTextures [i].Length; j++) {
						RenderTexture.ReleaseTemporary (m_HistoryTextures [i] [j]);
						m_HistoryTextures [i] [j] = null;
					}
					m_HistoryTextures [i] = null;
				}
			}
		}
		sampleIndex = 0;
		m_HistoryPingPong [0] = 0;
		m_HistoryPingPong [1] = 0;
		ResetHistory ();
	}
}
