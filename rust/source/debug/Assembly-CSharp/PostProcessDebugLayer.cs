using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;

[Serializable]
public sealed class PostProcessDebugLayer
{
	[Serializable]
	public class OverlaySettings
	{
		public bool linearDepth = false;

		[Range (0f, 16f)]
		public float motionColorIntensity = 4f;

		[Range (4f, 128f)]
		public int motionGridSize = 64;

		public ColorBlindnessType colorBlindnessType = ColorBlindnessType.Deuteranopia;

		[Range (0f, 1f)]
		public float colorBlindnessStrength = 1f;
	}

	public LightMeterMonitor lightMeter;

	public HistogramMonitor histogram;

	public WaveformMonitor waveform;

	public VectorscopeMonitor vectorscope;

	private Dictionary<MonitorType, Monitor> m_Monitors;

	private int frameWidth;

	private int frameHeight;

	public OverlaySettings overlaySettings;

	public RenderTexture debugOverlayTarget { get; private set; }

	public bool debugOverlayActive { get; private set; }

	public DebugOverlay debugOverlay { get; private set; }

	internal void OnEnable ()
	{
		RuntimeUtilities.CreateIfNull (ref lightMeter);
		RuntimeUtilities.CreateIfNull (ref histogram);
		RuntimeUtilities.CreateIfNull (ref waveform);
		RuntimeUtilities.CreateIfNull (ref vectorscope);
		RuntimeUtilities.CreateIfNull (ref overlaySettings);
		m_Monitors = new Dictionary<MonitorType, Monitor> {
			{
				MonitorType.LightMeter,
				lightMeter
			},
			{
				MonitorType.Histogram,
				histogram
			},
			{
				MonitorType.Waveform,
				waveform
			},
			{
				MonitorType.Vectorscope,
				vectorscope
			}
		};
		foreach (KeyValuePair<MonitorType, Monitor> monitor in m_Monitors) {
			monitor.Value.OnEnable ();
		}
	}

	internal void OnDisable ()
	{
		foreach (KeyValuePair<MonitorType, Monitor> monitor in m_Monitors) {
			monitor.Value.OnDisable ();
		}
		DestroyDebugOverlayTarget ();
	}

	private void DestroyDebugOverlayTarget ()
	{
		RuntimeUtilities.Destroy ((Object)(object)debugOverlayTarget);
		debugOverlayTarget = null;
	}

	public void RequestMonitorPass (MonitorType monitor)
	{
		m_Monitors [monitor].requested = true;
	}

	public void RequestDebugOverlay (DebugOverlay mode)
	{
		debugOverlay = mode;
	}

	internal void SetFrameSize (int width, int height)
	{
		frameWidth = width;
		frameHeight = height;
		debugOverlayActive = false;
	}

	public void PushDebugOverlay (CommandBuffer cmd, RenderTargetIdentifier source, PropertySheet sheet, int pass)
	{
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Expected O, but got Unknown
		if ((Object)(object)debugOverlayTarget == (Object)null || !debugOverlayTarget.IsCreated () || ((Texture)debugOverlayTarget).width != frameWidth || ((Texture)debugOverlayTarget).height != frameHeight) {
			RuntimeUtilities.Destroy ((Object)(object)debugOverlayTarget);
			debugOverlayTarget = new RenderTexture (frameWidth, frameHeight, 0, (RenderTextureFormat)0) {
				name = "Debug Overlay Target",
				anisoLevel = 1,
				filterMode = (FilterMode)1,
				wrapMode = (TextureWrapMode)1,
				hideFlags = (HideFlags)61
			};
			debugOverlayTarget.Create ();
		}
		cmd.BlitFullscreenTriangle (source, RenderTargetIdentifier.op_Implicit ((Texture)(object)debugOverlayTarget), sheet, pass);
		debugOverlayActive = true;
	}

	internal DepthTextureMode GetCameraFlags ()
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		if (debugOverlay == DebugOverlay.Depth) {
			return (DepthTextureMode)1;
		}
		if (debugOverlay == DebugOverlay.Normals) {
			return (DepthTextureMode)2;
		}
		if (debugOverlay == DebugOverlay.MotionVectors) {
			return (DepthTextureMode)5;
		}
		return (DepthTextureMode)0;
	}

	internal void RenderMonitors (PostProcessRenderContext context)
	{
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		bool flag = false;
		bool flag2 = false;
		foreach (KeyValuePair<MonitorType, Monitor> monitor in m_Monitors) {
			bool flag3 = monitor.Value.IsRequestedAndSupported (context);
			flag = flag || flag3;
			flag2 |= flag3 && monitor.Value.NeedsHalfRes ();
		}
		if (!flag) {
			return;
		}
		CommandBuffer command = context.command;
		command.BeginSample ("Monitors");
		if (flag2) {
			command.GetTemporaryRT (ShaderIDs.HalfResFinalCopy, context.width / 2, context.height / 2, 0, (FilterMode)1, context.sourceFormat);
			command.Blit (context.destination, RenderTargetIdentifier.op_Implicit (ShaderIDs.HalfResFinalCopy));
		}
		foreach (KeyValuePair<MonitorType, Monitor> monitor2 in m_Monitors) {
			Monitor value = monitor2.Value;
			if (value.requested) {
				value.Render (context);
			}
		}
		if (flag2) {
			command.ReleaseTemporaryRT (ShaderIDs.HalfResFinalCopy);
		}
		command.EndSample ("Monitors");
	}

	internal void RenderSpecialOverlays (PostProcessRenderContext context)
	{
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Invalid comparison between Unknown and I4
		//IL_014a: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_020d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0220: Unknown result type (might be due to invalid IL or missing references)
		if (debugOverlay == DebugOverlay.Depth) {
			PropertySheet propertySheet = context.propertySheets.Get (context.resources.shaders.debugOverlays);
			propertySheet.properties.SetVector (ShaderIDs.Params, new Vector4 (overlaySettings.linearDepth ? 1f : 0f, 0f, 0f, 0f));
			PushDebugOverlay (context.command, RenderTargetIdentifier.op_Implicit ((BuiltinRenderTextureType)0), propertySheet, 0);
		} else if (debugOverlay == DebugOverlay.Normals) {
			PropertySheet propertySheet2 = context.propertySheets.Get (context.resources.shaders.debugOverlays);
			propertySheet2.ClearKeywords ();
			if ((int)context.camera.actualRenderingPath == 2) {
				propertySheet2.EnableKeyword ("SOURCE_GBUFFER");
			}
			PushDebugOverlay (context.command, RenderTargetIdentifier.op_Implicit ((BuiltinRenderTextureType)0), propertySheet2, 1);
		} else if (debugOverlay == DebugOverlay.MotionVectors) {
			PropertySheet propertySheet3 = context.propertySheets.Get (context.resources.shaders.debugOverlays);
			propertySheet3.properties.SetVector (ShaderIDs.Params, new Vector4 (overlaySettings.motionColorIntensity, (float)overlaySettings.motionGridSize, 0f, 0f));
			PushDebugOverlay (context.command, context.source, propertySheet3, 2);
		} else if (debugOverlay == DebugOverlay.NANTracker) {
			PropertySheet sheet = context.propertySheets.Get (context.resources.shaders.debugOverlays);
			PushDebugOverlay (context.command, context.source, sheet, 3);
		} else if (debugOverlay == DebugOverlay.ColorBlindnessSimulation) {
			PropertySheet propertySheet4 = context.propertySheets.Get (context.resources.shaders.debugOverlays);
			propertySheet4.properties.SetVector (ShaderIDs.Params, new Vector4 (overlaySettings.colorBlindnessStrength, 0f, 0f, 0f));
			PushDebugOverlay (context.command, context.source, propertySheet4, (int)(4 + overlaySettings.colorBlindnessType));
		}
	}

	internal void EndFrame ()
	{
		foreach (KeyValuePair<MonitorType, Monitor> monitor in m_Monitors) {
			monitor.Value.requested = false;
		}
		if (!debugOverlayActive) {
			DestroyDebugOverlayTarget ();
		}
		debugOverlay = DebugOverlay.None;
	}
}
