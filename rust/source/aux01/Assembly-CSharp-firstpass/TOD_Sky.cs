using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.Rendering;

[ExecuteInEditMode]
[RequireComponent (typeof(TOD_Resources))]
[RequireComponent (typeof(TOD_Components))]
public class TOD_Sky : MonoBehaviour
{
	public class ReflectionProbeState
	{
		public ReflectionProbe Probe;

		public GameObject ProbeInstance;

		public int ProbeRenderID = -1;

		public void InitializePrimary (Vector3 position, GameObject prefab)
		{
			if (!ProbeInstance) {
				if (!prefab) {
					ProbeInstance = new GameObject ();
					ProbeInstance.name = "Primary Reflection Probe";
					ProbeInstance.transform.position = position;
					Probe = ProbeInstance.AddComponent<ReflectionProbe> ();
					Probe.size = new Vector3 (float.MaxValue, float.MaxValue, float.MaxValue);
				} else {
					ProbeInstance = UnityEngine.Object.Instantiate (prefab);
					ProbeInstance.name = "Primary Reflection Probe";
					ProbeInstance.transform.position = position;
					Probe = ProbeInstance.GetComponent<ReflectionProbe> ();
					Probe.size = new Vector3 (float.MaxValue, float.MaxValue, float.MaxValue);
				}
			}
		}

		public void InitializeSecondary (Vector3 position)
		{
			if (!ProbeInstance) {
				ProbeInstance = new GameObject ();
				ProbeInstance.name = "Secondary Reflection Probe";
				ProbeInstance.transform.position = position;
				Probe = ProbeInstance.AddComponent<ReflectionProbe> ();
				Probe.size = new Vector3 (0f, 0f, 0f);
				Probe.importance = 0;
			}
		}
	}

	private static List<TOD_Sky> instances = new List<TOD_Sky> ();

	[Tooltip ("Auto: Use the player settings.\nLinear: Force linear color space.\nGamma: Force gamma color space.")]
	public TOD_ColorSpaceType ColorSpace;

	[Tooltip ("Auto: Use the camera settings.\nHDR: Force high dynamic range.\nLDR: Force low dynamic range.")]
	public TOD_ColorRangeType ColorRange;

	[Tooltip ("Raw: Write color without modifications.\nDithered: Add dithering to reduce banding.")]
	public TOD_ColorOutputType ColorOutput = TOD_ColorOutputType.Dithered;

	[Tooltip ("Per Vertex: Calculate sky color per vertex.\nPer Pixel: Calculate sky color per pixel.")]
	public TOD_SkyQualityType SkyQuality;

	[Tooltip ("Low: Only recommended for very old mobile devices.\nMedium: Simplified cloud shading.\nHigh: Physically based cloud shading.")]
	public TOD_CloudQualityType CloudQuality = TOD_CloudQualityType.High;

	[Tooltip ("Low: Only recommended for very old mobile devices.\nMedium: Simplified mesh geometry.\nHigh: Detailed mesh geometry.")]
	public TOD_MeshQualityType MeshQuality = TOD_MeshQualityType.High;

	[Tooltip ("Low: Recommended for most mobile devices.\nMedium: Includes most visible stars.\nHigh: Includes all visible stars.")]
	public TOD_StarQualityType StarQuality = TOD_StarQualityType.High;

	public TOD_CycleParameters Cycle;

	public TOD_WorldParameters World;

	public TOD_AtmosphereParameters Atmosphere;

	public TOD_DayParameters Day;

	public TOD_NightParameters Night;

	public TOD_SunParameters Sun;

	public TOD_MoonParameters Moon;

	public TOD_StarParameters Stars;

	public TOD_CloudParameters Clouds;

	public TOD_LightParameters Light;

	public TOD_FogParameters Fog;

	public TOD_AmbientParameters Ambient;

	public TOD_ReflectionParameters Reflection;

	private ReflectionProbeState ReflectionCur = new ReflectionProbeState ();

	private ReflectionProbeState ReflectionSrc = new ReflectionProbeState ();

	private ReflectionProbeState ReflectionDst = new ReflectionProbeState ();

	public static float ReflectionUpdateSpeed = 1f;

	public static int ReflectionResolution = 64;

	public static float ReflectionUpdateInterval = 1f;

	public static ReflectionProbeTimeSlicingMode ReflectionTimeSlicing = ReflectionProbeTimeSlicingMode.IndividualFaces;

	private float timeSinceLightUpdate = float.MaxValue;

	private float timeSinceAmbientUpdate = float.MaxValue;

	private float timeSinceReflectionUpdate = float.MaxValue;

	private const int TOD_SAMPLES = 2;

	private Vector3 kBetaMie;

	private Vector4 kSun;

	private Vector4 k4PI;

	private Vector4 kRadius;

	private Vector4 kScale;

	private const float pi = (float)Math.PI;

	private const float tau = (float)Math.PI * 2f;

	public static List<TOD_Sky> Instances => instances;

	public static TOD_Sky Instance {
		get {
			if (instances.Count != 0) {
				return instances [instances.Count - 1];
			}
			return null;
		}
	}

	public bool Initialized { get; private set; }

	public bool Headless => Camera.allCamerasCount == 0;

	public TOD_Components Components { get; private set; }

	public TOD_Resources Resources { get; private set; }

	public bool IsDay { get; private set; }

	public bool IsNight { get; private set; }

	public float Radius => Components.DomeTransform.lossyScale.y;

	public float Diameter => Components.DomeTransform.lossyScale.y * 2f;

	public float LerpValue { get; private set; }

	public float SunZenith { get; private set; }

	public float SunAltitude { get; private set; }

	public float SunAzimuth { get; private set; }

	public float MoonZenith { get; private set; }

	public float MoonAltitude { get; private set; }

	public float MoonAzimuth { get; private set; }

	public float SunsetTime { get; private set; }

	public float SunriseTime { get; private set; }

	public float LocalSiderealTime { get; private set; }

	public float LightZenith => Mathf.Min (SunZenith, MoonZenith);

	public float LightIntensity {
		get {
			return Components.LightSource.intensity;
		}
		set {
			Components.LightSource.intensity = value;
		}
	}

	public float SunVisibility { get; private set; }

	public float MoonVisibility { get; private set; }

	public Vector3 SunDirection { get; private set; }

	public Vector3 MoonDirection { get; private set; }

	public Vector3 LightDirection { get; private set; }

	public Vector3 LocalSunDirection { get; private set; }

	public Vector3 LocalMoonDirection { get; private set; }

	public Vector3 LocalLightDirection { get; private set; }

	public Color SunLightColor { get; private set; }

	public Color MoonLightColor { get; private set; }

	public Color LightColor {
		get {
			return Components.LightSource.color;
		}
		set {
			Components.LightSource.color = value;
		}
	}

	public Color SunRayColor { get; set; }

	public Color MoonRayColor { get; set; }

	public Color SunSkyColor { get; private set; }

	public Color MoonSkyColor { get; private set; }

	public Color SunMeshColor { get; private set; }

	public Color MoonMeshColor { get; private set; }

	public Color SunCloudColor { get; private set; }

	public Color MoonCloudColor { get; private set; }

	public Color FogColor { get; private set; }

	public Color GroundColor { get; private set; }

	public Color AmbientColor { get; private set; }

	public Color MoonHaloColor { get; private set; }

	public ReflectionProbe Probe => ReflectionCur.Probe;

	public GameObject ProbeInstance => ReflectionCur.ProbeInstance;

	public Vector3 OrbitalToUnity (float radius, float theta, float phi)
	{
		float num = Mathf.Sin (theta);
		float num2 = Mathf.Cos (theta);
		float num3 = Mathf.Sin (phi);
		float num4 = Mathf.Cos (phi);
		Vector3 result = default(Vector3);
		result.z = radius * num * num4;
		result.y = radius * num2;
		result.x = radius * num * num3;
		return result;
	}

	public Vector3 OrbitalToLocal (float theta, float phi)
	{
		float num = Mathf.Sin (theta);
		float y = Mathf.Cos (theta);
		float num2 = Mathf.Sin (phi);
		float num3 = Mathf.Cos (phi);
		Vector3 result = default(Vector3);
		result.z = num * num3;
		result.y = y;
		result.x = num * num2;
		return result;
	}

	public Color SampleAtmosphere (Vector3 direction, bool directLight = true)
	{
		Vector3 dir = Components.DomeTransform.InverseTransformDirection (direction);
		Color color = ShaderScatteringColor (dir, directLight);
		color = TOD_HDR2LDR (color);
		return TOD_LINEAR2GAMMA (color);
	}

	public SphericalHarmonicsL2 RenderToSphericalHarmonics ()
	{
		float saturation = Ambient.Saturation;
		float intensity = Mathf.Lerp (Night.AmbientMultiplier, Day.AmbientMultiplier, LerpValue);
		return RenderToSphericalHarmonics (intensity, saturation);
	}

	public SphericalHarmonicsL2 RenderToSphericalHarmonics (float intensity, float saturation)
	{
		SphericalHarmonicsL2 result = default(SphericalHarmonicsL2);
		bool directLight = false;
		Color color = TOD_Util.AdjustRGB (AmbientColor.linear, intensity, saturation);
		Vector3 vector = new Vector3 (0.612372458f, 0.5f, 0.612372458f);
		Vector3 up = Vector3.up;
		Color color2 = TOD_Util.AdjustRGB (SampleAtmosphere (up, directLight).linear, intensity, saturation);
		result.AddDirectionalLight (up, color2, 0.428571433f);
		Vector3 direction = new Vector3 (0f - vector.x, vector.y, 0f - vector.z);
		Color color3 = TOD_Util.AdjustRGB (SampleAtmosphere (direction, directLight).linear, intensity, saturation);
		result.AddDirectionalLight (direction, color3, 0.2857143f);
		Vector3 direction2 = new Vector3 (vector.x, vector.y, 0f - vector.z);
		Color color4 = TOD_Util.AdjustRGB (SampleAtmosphere (direction2, directLight).linear, intensity, saturation);
		result.AddDirectionalLight (direction2, color4, 0.2857143f);
		Vector3 direction3 = new Vector3 (0f - vector.x, vector.y, vector.z);
		Color color5 = TOD_Util.AdjustRGB (SampleAtmosphere (direction3, directLight).linear, intensity, saturation);
		result.AddDirectionalLight (direction3, color5, 0.2857143f);
		Vector3 direction4 = new Vector3 (vector.x, vector.y, vector.z);
		Color color6 = TOD_Util.AdjustRGB (SampleAtmosphere (direction4, directLight).linear, intensity, saturation);
		result.AddDirectionalLight (direction4, color6, 0.2857143f);
		Vector3 left = Vector3.left;
		Color color7 = TOD_Util.AdjustRGB (SampleAtmosphere (left, directLight).linear, intensity, saturation);
		result.AddDirectionalLight (left, color7, 1f / 7f);
		Vector3 right = Vector3.right;
		Color color8 = TOD_Util.AdjustRGB (SampleAtmosphere (right, directLight).linear, intensity, saturation);
		result.AddDirectionalLight (right, color8, 1f / 7f);
		Vector3 back = Vector3.back;
		Color color9 = TOD_Util.AdjustRGB (SampleAtmosphere (back, directLight).linear, intensity, saturation);
		result.AddDirectionalLight (back, color9, 1f / 7f);
		Vector3 forward = Vector3.forward;
		Color color10 = TOD_Util.AdjustRGB (SampleAtmosphere (forward, directLight).linear, intensity, saturation);
		result.AddDirectionalLight (forward, color10, 1f / 7f);
		Vector3 direction5 = new Vector3 (0f - vector.x, 0f - vector.y, 0f - vector.z);
		result.AddDirectionalLight (direction5, color, 0.2857143f);
		Vector3 direction6 = new Vector3 (vector.x, 0f - vector.y, 0f - vector.z);
		result.AddDirectionalLight (direction6, color, 0.2857143f);
		Vector3 direction7 = new Vector3 (0f - vector.x, 0f - vector.y, vector.z);
		result.AddDirectionalLight (direction7, color, 0.2857143f);
		Vector3 direction8 = new Vector3 (vector.x, 0f - vector.y, vector.z);
		result.AddDirectionalLight (direction8, color, 0.2857143f);
		Vector3 down = Vector3.down;
		result.AddDirectionalLight (down, color, 0.428571433f);
		return result;
	}

	public void RenderToCubemap (RenderTexture targetTexture = null)
	{
		ReflectionCur.InitializePrimary (Components.DomeTransform.position, Reflection.ProbePrefab);
		if (ReflectionCur.ProbeRenderID < 0 || ReflectionCur.Probe.IsFinishedRendering (ReflectionCur.ProbeRenderID)) {
			UpdateProbeProperties (ReflectionCur.Probe);
			ReflectionCur.ProbeRenderID = ReflectionCur.Probe.RenderProbe (targetTexture);
		} else {
			UpdateProbeProperties (ReflectionCur.Probe);
		}
	}

	public void RenderToProbe ()
	{
		ReflectionCur.InitializePrimary (Components.DomeTransform.position, Reflection.ProbePrefab);
		if (ReflectionCur.ProbeRenderID < 0 || ReflectionUpdateInterval == 0f) {
			UpdateProbeProperties (ReflectionCur.Probe);
			ReflectionCur.ProbeRenderID = ReflectionCur.Probe.RenderProbe ();
		} else if (ReflectionDst.ProbeRenderID < 0 || ReflectionDst.Probe.IsFinishedRendering (ReflectionDst.ProbeRenderID)) {
			ReflectionSrc.InitializeSecondary (Components.DomeTransform.position);
			ReflectionDst.InitializeSecondary (Components.DomeTransform.position);
			UpdateProbeProperties (ReflectionCur.Probe);
			UpdateProbeProperties (ReflectionSrc.Probe);
			UpdateProbeProperties (ReflectionDst.Probe);
			TOD_Util.Swap (ref ReflectionSrc, ref ReflectionDst);
			ReflectionDst.ProbeRenderID = ReflectionDst.Probe.RenderProbe ();
		}
	}

	public bool ReflectionIsUpdating ()
	{
		if (ReflectionDst.ProbeRenderID > 0) {
			return !ReflectionDst.Probe.IsFinishedRendering (ReflectionDst.ProbeRenderID);
		}
		return false;
	}

	private void UpdateProbeBlending (float lerp)
	{
		if (ReflectionCur.ProbeRenderID >= 0 && ReflectionSrc.ProbeRenderID >= 0 && ReflectionDst.ProbeRenderID >= 0) {
			ReflectionProbe.BlendCubemap (ReflectionSrc.Probe.realtimeTexture, ReflectionDst.Probe.realtimeTexture, lerp, ReflectionCur.Probe.realtimeTexture);
		}
	}

	private void UpdateProbeProperties (ReflectionProbe probe)
	{
		probe.mode = ReflectionProbeMode.Realtime;
		probe.transform.position = Components.DomeTransform.position;
		probe.intensity = RenderSettings.reflectionIntensity;
		probe.clearFlags = Reflection.ClearFlags;
		probe.cullingMask = Reflection.CullingMask;
		probe.refreshMode = ReflectionProbeRefreshMode.ViaScripting;
		probe.timeSlicingMode = ReflectionTimeSlicing;
		probe.resolution = Mathf.ClosestPowerOfTwo (ReflectionResolution);
		if (Components.Camera != null) {
			probe.backgroundColor = Components.Camera.BackgroundColor;
			if (!Reflection.ProbePrefab) {
				probe.nearClipPlane = Components.Camera.NearClipPlane;
				probe.farClipPlane = Components.Camera.FarClipPlane;
			}
		}
	}

	public Color SampleFogColor (bool directLight = true)
	{
		Vector3 vector = Vector3.forward;
		if (Components.Camera != null) {
			vector = Quaternion.Euler (0f, Components.Camera.transform.rotation.eulerAngles.y, 0f) * vector;
		}
		Color color = SampleAtmosphere (Vector3.Lerp (vector, Vector3.up, Fog.HeightBias).normalized, directLight);
		return new Color (color.r, color.g, color.b, 1f);
	}

	public Color SampleSkyColor ()
	{
		Vector3 sunDirection = SunDirection;
		sunDirection.y = Mathf.Abs (sunDirection.y);
		Color color = SampleAtmosphere (sunDirection.normalized, directLight: false);
		return new Color (color.r, color.g, color.b, 1f);
	}

	public Color SampleEquatorColor ()
	{
		Vector3 sunDirection = SunDirection;
		sunDirection.y = 0f;
		Color color = SampleAtmosphere (sunDirection.normalized, directLight: false);
		return new Color (color.r, color.g, color.b, 1f);
	}

	public void UpdateFog ()
	{
		switch (Fog.Mode) {
		case TOD_FogType.Atmosphere:
			RenderSettings.fogColor = SampleFogColor (directLight: false);
			break;
		case TOD_FogType.Directional:
			RenderSettings.fogColor = SampleFogColor ();
			break;
		case TOD_FogType.Gradient:
			RenderSettings.fogColor = FogColor;
			break;
		case TOD_FogType.None:
			break;
		}
	}

	public void UpdateAmbient ()
	{
		float saturation = Ambient.Saturation;
		float num = Mathf.Lerp (Night.AmbientMultiplier, Day.AmbientMultiplier, LerpValue);
		switch (Ambient.Mode) {
		case TOD_AmbientType.Color: {
			Color ambientLight2 = TOD_Util.AdjustRGB (AmbientColor, num, saturation);
			RenderSettings.ambientMode = AmbientMode.Flat;
			RenderSettings.ambientLight = ambientLight2;
			RenderSettings.ambientIntensity = num;
			break;
		}
		case TOD_AmbientType.Gradient: {
			Color ambientGroundColor = TOD_Util.AdjustRGB (AmbientColor, num, saturation);
			Color ambientEquatorColor = TOD_Util.AdjustRGB (SampleEquatorColor (), num, saturation);
			Color ambientSkyColor = TOD_Util.AdjustRGB (SampleSkyColor (), num, saturation);
			RenderSettings.ambientMode = AmbientMode.Trilight;
			RenderSettings.ambientSkyColor = ambientSkyColor;
			RenderSettings.ambientEquatorColor = ambientEquatorColor;
			RenderSettings.ambientGroundColor = ambientGroundColor;
			RenderSettings.ambientIntensity = num;
			break;
		}
		case TOD_AmbientType.Spherical: {
			Color ambientLight = TOD_Util.AdjustRGB (AmbientColor, num, saturation);
			RenderSettings.ambientMode = AmbientMode.Skybox;
			RenderSettings.ambientLight = ambientLight;
			RenderSettings.ambientIntensity = num;
			RenderSettings.ambientProbe = RenderToSphericalHarmonics (num, saturation);
			break;
		}
		}
	}

	public void UpdateReflection ()
	{
		TOD_ReflectionType mode = Reflection.Mode;
		if (mode == TOD_ReflectionType.Cubemap) {
			float reflectionIntensity = Mathf.Lerp (Night.ReflectionMultiplier, Day.ReflectionMultiplier, LerpValue);
			RenderSettings.defaultReflectionMode = DefaultReflectionMode.Skybox;
			RenderSettings.reflectionIntensity = reflectionIntensity;
			if (Application.isPlaying) {
				RenderToProbe ();
			}
		}
	}

	public void DelayReflectionUpdate ()
	{
		timeSinceReflectionUpdate = 0f;
	}

	public void LoadParameters (string xml)
	{
		using StringReader input = new StringReader (xml);
		using XmlTextReader xmlReader = new XmlTextReader (input);
		(new XmlSerializer (typeof(TOD_Parameters)).Deserialize (xmlReader) as TOD_Parameters).ToSky (this);
	}

	public string SaveParameters ()
	{
		StringBuilder stringBuilder = new StringBuilder ();
		using (StringWriter w = new StringWriter (stringBuilder)) {
			using XmlTextWriter xmlTextWriter = new XmlTextWriter (w);
			xmlTextWriter.Formatting = Formatting.Indented;
			XmlSerializer xmlSerializer = new XmlSerializer (typeof(TOD_Parameters));
			TOD_Parameters o = new TOD_Parameters (this);
			xmlSerializer.Serialize (xmlTextWriter, o);
		}
		return stringBuilder.ToString ();
	}

	private void UpdateQualitySettings ()
	{
		if (!Headless) {
			Mesh mesh = null;
			Mesh mesh2 = null;
			Mesh mesh3 = null;
			Mesh mesh4 = null;
			Mesh mesh5 = null;
			Mesh mesh6 = null;
			switch (MeshQuality) {
			case TOD_MeshQualityType.Low:
				mesh = Resources.SkyLOD2;
				mesh2 = Resources.SkyLOD2;
				mesh3 = Resources.SkyLOD2;
				mesh4 = Resources.CloudsLOD2;
				mesh5 = Resources.MoonLOD2;
				break;
			case TOD_MeshQualityType.Medium:
				mesh = Resources.SkyLOD1;
				mesh2 = Resources.SkyLOD1;
				mesh3 = Resources.SkyLOD2;
				mesh4 = Resources.CloudsLOD1;
				mesh5 = Resources.MoonLOD1;
				break;
			case TOD_MeshQualityType.High:
				mesh = Resources.SkyLOD0;
				mesh2 = Resources.SkyLOD0;
				mesh3 = Resources.SkyLOD2;
				mesh4 = Resources.CloudsLOD0;
				mesh5 = Resources.MoonLOD0;
				break;
			}
			switch (StarQuality) {
			case TOD_StarQualityType.Low:
				mesh6 = Resources.StarsLOD2;
				break;
			case TOD_StarQualityType.Medium:
				mesh6 = Resources.StarsLOD1;
				break;
			case TOD_StarQualityType.High:
				mesh6 = Resources.StarsLOD0;
				break;
			}
			if ((bool)Components.SpaceMeshFilter && Components.SpaceMeshFilter.sharedMesh != mesh) {
				Components.SpaceMeshFilter.mesh = mesh;
			}
			if ((bool)Components.MoonMeshFilter && Components.MoonMeshFilter.sharedMesh != mesh5) {
				Components.MoonMeshFilter.mesh = mesh5;
			}
			if ((bool)Components.AtmosphereMeshFilter && Components.AtmosphereMeshFilter.sharedMesh != mesh2) {
				Components.AtmosphereMeshFilter.mesh = mesh2;
			}
			if ((bool)Components.ClearMeshFilter && Components.ClearMeshFilter.sharedMesh != mesh3) {
				Components.ClearMeshFilter.mesh = mesh3;
			}
			if ((bool)Components.CloudMeshFilter && Components.CloudMeshFilter.sharedMesh != mesh4) {
				Components.CloudMeshFilter.mesh = mesh4;
			}
			if ((bool)Components.StarMeshFilter && Components.StarMeshFilter.sharedMesh != mesh6) {
				Components.StarMeshFilter.mesh = mesh6;
			}
		}
	}

	private void UpdateRenderSettings ()
	{
		if (Headless) {
			return;
		}
		UpdateFog ();
		if (!Application.isPlaying || timeSinceAmbientUpdate >= Ambient.UpdateInterval) {
			timeSinceAmbientUpdate = 0f;
			UpdateAmbient ();
		} else {
			timeSinceAmbientUpdate += Time.deltaTime;
		}
		if (!Application.isPlaying || timeSinceReflectionUpdate >= ReflectionUpdateInterval) {
			timeSinceReflectionUpdate = 0f;
			UpdateReflection ();
		} else if (!ReflectionIsUpdating ()) {
			timeSinceReflectionUpdate += Time.deltaTime * ReflectionUpdateSpeed;
			if (Application.isPlaying) {
				UpdateProbeBlending ((ReflectionUpdateInterval > 0f) ? (timeSinceReflectionUpdate / ReflectionUpdateInterval) : 1f);
			}
		}
	}

	private void UpdateShaderKeywords ()
	{
		if (Headless) {
			return;
		}
		switch (ColorSpace) {
		case TOD_ColorSpaceType.Auto:
			if (QualitySettings.activeColorSpace == UnityEngine.ColorSpace.Linear) {
				Shader.EnableKeyword ("TOD_OUTPUT_LINEAR");
			} else {
				Shader.DisableKeyword ("TOD_OUTPUT_LINEAR");
			}
			break;
		case TOD_ColorSpaceType.Linear:
			Shader.EnableKeyword ("TOD_OUTPUT_LINEAR");
			break;
		case TOD_ColorSpaceType.Gamma:
			Shader.DisableKeyword ("TOD_OUTPUT_LINEAR");
			break;
		}
		switch (ColorRange) {
		case TOD_ColorRangeType.Auto:
			if ((bool)Components.Camera && Components.Camera.HDR) {
				Shader.EnableKeyword ("TOD_OUTPUT_HDR");
			} else {
				Shader.DisableKeyword ("TOD_OUTPUT_HDR");
			}
			break;
		case TOD_ColorRangeType.HDR:
			Shader.EnableKeyword ("TOD_OUTPUT_HDR");
			break;
		case TOD_ColorRangeType.LDR:
			Shader.DisableKeyword ("TOD_OUTPUT_HDR");
			break;
		}
		switch (ColorOutput) {
		case TOD_ColorOutputType.Raw:
			Shader.DisableKeyword ("TOD_OUTPUT_DITHERING");
			break;
		case TOD_ColorOutputType.Dithered:
			Shader.EnableKeyword ("TOD_OUTPUT_DITHERING");
			break;
		}
		switch (SkyQuality) {
		case TOD_SkyQualityType.PerVertex:
			Shader.DisableKeyword ("TOD_SCATTERING_PER_PIXEL");
			break;
		case TOD_SkyQualityType.PerPixel:
			Shader.EnableKeyword ("TOD_SCATTERING_PER_PIXEL");
			break;
		}
		switch (CloudQuality) {
		case TOD_CloudQualityType.Low:
			Shader.DisableKeyword ("TOD_CLOUDS_DENSITY");
			Shader.DisableKeyword ("TOD_CLOUDS_BUMPED");
			break;
		case TOD_CloudQualityType.Medium:
			Shader.EnableKeyword ("TOD_CLOUDS_DENSITY");
			Shader.DisableKeyword ("TOD_CLOUDS_BUMPED");
			break;
		case TOD_CloudQualityType.High:
			Shader.EnableKeyword ("TOD_CLOUDS_DENSITY");
			Shader.EnableKeyword ("TOD_CLOUDS_BUMPED");
			break;
		}
	}

	private void UpdateShaderProperties ()
	{
		if (!Headless) {
			Shader.SetGlobalColor (Resources.ID_SunLightColor, SunLightColor);
			Shader.SetGlobalColor (Resources.ID_MoonLightColor, MoonLightColor);
			Shader.SetGlobalColor (Resources.ID_SunSkyColor, SunSkyColor);
			Shader.SetGlobalColor (Resources.ID_MoonSkyColor, MoonSkyColor);
			Shader.SetGlobalColor (Resources.ID_SunMeshColor, SunMeshColor);
			Shader.SetGlobalColor (Resources.ID_MoonMeshColor, MoonMeshColor);
			Shader.SetGlobalColor (Resources.ID_SunCloudColor, SunCloudColor);
			Shader.SetGlobalColor (Resources.ID_MoonCloudColor, MoonCloudColor);
			Shader.SetGlobalColor (Resources.ID_FogColor, FogColor);
			Shader.SetGlobalColor (Resources.ID_GroundColor, GroundColor);
			Shader.SetGlobalColor (Resources.ID_AmbientColor, AmbientColor);
			Shader.SetGlobalVector (Resources.ID_SunDirection, SunDirection);
			Shader.SetGlobalVector (Resources.ID_MoonDirection, MoonDirection);
			Shader.SetGlobalVector (Resources.ID_LightDirection, LightDirection);
			Shader.SetGlobalVector (Resources.ID_LocalSunDirection, LocalSunDirection);
			Shader.SetGlobalVector (Resources.ID_LocalMoonDirection, LocalMoonDirection);
			Shader.SetGlobalVector (Resources.ID_LocalLightDirection, LocalLightDirection);
			Shader.SetGlobalFloat (Resources.ID_Contrast, Atmosphere.Contrast);
			Shader.SetGlobalFloat (Resources.ID_Brightness, Atmosphere.Brightness);
			Shader.SetGlobalFloat (Resources.ID_Fogginess, Atmosphere.Fogginess);
			Shader.SetGlobalFloat (Resources.ID_Directionality, Atmosphere.Directionality);
			Shader.SetGlobalFloat (Resources.ID_MoonHaloPower, 1f / Moon.HaloSize);
			Shader.SetGlobalColor (Resources.ID_MoonHaloColor, MoonHaloColor);
			float value = Mathf.Lerp (0.8f, 0f, Clouds.Coverage);
			float num = Mathf.Lerp (3f, 9f, Clouds.Sharpness);
			float value2 = Mathf.Lerp (0f, 1f, Clouds.Attenuation);
			float value3 = Mathf.Lerp (0f, 2f, Clouds.Saturation);
			Shader.SetGlobalFloat (Resources.ID_CloudOpacity, Clouds.Opacity);
			Shader.SetGlobalFloat (Resources.ID_CloudCoverage, value);
			Shader.SetGlobalFloat (Resources.ID_CloudSharpness, 1f / num);
			Shader.SetGlobalFloat (Resources.ID_CloudDensity, num);
			Shader.SetGlobalFloat (Resources.ID_CloudColoring, Clouds.Coloring);
			Shader.SetGlobalFloat (Resources.ID_CloudAttenuation, value2);
			Shader.SetGlobalFloat (Resources.ID_CloudSaturation, value3);
			Shader.SetGlobalFloat (Resources.ID_CloudScattering, Clouds.Scattering);
			Shader.SetGlobalFloat (Resources.ID_CloudBrightness, Clouds.Brightness);
			Shader.SetGlobalVector (Resources.ID_CloudOffset, Components.Animation.OffsetUV);
			Shader.SetGlobalVector (Resources.ID_CloudWind, Components.Animation.CloudUV);
			Shader.SetGlobalVector (Resources.ID_CloudSize, new Vector3 (Clouds.Size * 4f, Clouds.Size, Clouds.Size * 4f));
			Shader.SetGlobalFloat (Resources.ID_StarSize, Stars.Size);
			Shader.SetGlobalFloat (Resources.ID_StarBrightness, Stars.Brightness);
			Shader.SetGlobalFloat (Resources.ID_StarVisibility, (1f - Atmosphere.Fogginess) * (1f - LerpValue));
			Shader.SetGlobalFloat (Resources.ID_SunMeshContrast, 1f / Mathf.Max (0.001f, Sun.MeshContrast));
			Shader.SetGlobalFloat (Resources.ID_SunMeshBrightness, Sun.MeshBrightness * (1f - Atmosphere.Fogginess));
			Shader.SetGlobalFloat (Resources.ID_MoonMeshContrast, 1f / Mathf.Max (0.001f, Moon.MeshContrast));
			Shader.SetGlobalFloat (Resources.ID_MoonMeshBrightness, Moon.MeshBrightness * (1f - Atmosphere.Fogginess));
			Shader.SetGlobalVector (Resources.ID_kBetaMie, kBetaMie);
			Shader.SetGlobalVector (Resources.ID_kSun, kSun);
			Shader.SetGlobalVector (Resources.ID_k4PI, k4PI);
			Shader.SetGlobalVector (Resources.ID_kRadius, kRadius);
			Shader.SetGlobalVector (Resources.ID_kScale, kScale);
			Shader.SetGlobalMatrix (Resources.ID_World2Sky, Components.DomeTransform.worldToLocalMatrix);
			Shader.SetGlobalMatrix (Resources.ID_Sky2World, Components.DomeTransform.localToWorldMatrix);
		}
	}

	private float ShaderScale (float inCos)
	{
		float num = 1f - inCos;
		return 0.25f * Mathf.Exp (-0.00287f + num * (0.459f + num * (3.83f + num * (-6.8f + num * 5.25f))));
	}

	private float ShaderMiePhase (float eyeCos, float eyeCos2)
	{
		return kBetaMie.x * (1f + eyeCos2) / Mathf.Pow (kBetaMie.y + kBetaMie.z * eyeCos, 1.5f);
	}

	private float ShaderRayleighPhase (float eyeCos2)
	{
		return 0.75f + 0.75f * eyeCos2;
	}

	private Color ShaderNightSkyColor (Vector3 dir)
	{
		dir.y = Mathf.Max (0f, dir.y);
		return MoonSkyColor * (1f - 0.75f * dir.y);
	}

	private Color ShaderMoonHaloColor (Vector3 dir)
	{
		return MoonHaloColor * Mathf.Pow (Mathf.Max (0f, Vector3.Dot (dir, LocalMoonDirection)), 1f / Moon.MeshSize);
	}

	private Color TOD_HDR2LDR (Color color)
	{
		return new Color (1f - Mathf.Pow (2f, (0f - Atmosphere.Brightness) * color.r), 1f - Mathf.Pow (2f, (0f - Atmosphere.Brightness) * color.g), 1f - Mathf.Pow (2f, (0f - Atmosphere.Brightness) * color.b), color.a);
	}

	private Color TOD_GAMMA2LINEAR (Color color)
	{
		return new Color (color.r * color.r, color.g * color.g, color.b * color.b, color.a);
	}

	private Color TOD_LINEAR2GAMMA (Color color)
	{
		return new Color (Mathf.Sqrt (color.r), Mathf.Sqrt (color.g), Mathf.Sqrt (color.b), color.a);
	}

	private Color ShaderScatteringColor (Vector3 dir, bool directLight = true)
	{
		dir.y = Mathf.Max (0f, dir.y);
		float x = kRadius.x;
		float y = kRadius.y;
		float w = kRadius.w;
		float x2 = kScale.x;
		float z = kScale.z;
		float w2 = kScale.w;
		float x3 = k4PI.x;
		float y2 = k4PI.y;
		float z2 = k4PI.z;
		float w3 = k4PI.w;
		float x4 = kSun.x;
		float y3 = kSun.y;
		float z3 = kSun.z;
		float w4 = kSun.w;
		Vector3 vector = new Vector3 (0f, x + w2, 0f);
		float num = Mathf.Sqrt (w + y * dir.y * dir.y - y) - x * dir.y;
		float num2 = Mathf.Exp (z * (0f - w2));
		float inCos = Vector3.Dot (dir, vector) / (x + w2);
		float num3 = num2 * ShaderScale (inCos);
		float num4 = num / 2f;
		float num5 = num4 * x2;
		Vector3 vector2 = dir * num4;
		Vector3 rhs = vector + vector2 * 0.5f;
		float num6 = 0f;
		float num7 = 0f;
		float num8 = 0f;
		for (int i = 0; i < 2; i++) {
			float magnitude = rhs.magnitude;
			float num9 = 1f / magnitude;
			float num10 = Mathf.Exp (z * (x - magnitude));
			float num11 = num10 * num5;
			float inCos2 = Vector3.Dot (dir, rhs) * num9;
			float inCos3 = Vector3.Dot (LocalSunDirection, rhs) * num9;
			float num12 = num3 + num10 * (ShaderScale (inCos3) - ShaderScale (inCos2));
			float num13 = Mathf.Exp ((0f - num12) * (x3 + w3));
			float num14 = Mathf.Exp ((0f - num12) * (y2 + w3));
			float num15 = Mathf.Exp ((0f - num12) * (z2 + w3));
			num6 += num13 * num11;
			num7 += num14 * num11;
			num8 += num15 * num11;
			rhs += vector2;
		}
		float num16 = SunSkyColor.r * num6 * x4;
		float num17 = SunSkyColor.g * num7 * y3;
		float num18 = SunSkyColor.b * num8 * z3;
		float num19 = SunSkyColor.r * num6 * w4;
		float num20 = SunSkyColor.g * num7 * w4;
		float num21 = SunSkyColor.b * num8 * w4;
		float num22 = 0f;
		float num23 = 0f;
		float num24 = 0f;
		float num25 = Vector3.Dot (LocalSunDirection, dir);
		float eyeCos = num25 * num25;
		float num26 = ShaderRayleighPhase (eyeCos);
		num22 += num26 * num16;
		num23 += num26 * num17;
		num24 += num26 * num18;
		if (directLight) {
			float num27 = ShaderMiePhase (num25, eyeCos);
			num22 += num27 * num19;
			num23 += num27 * num20;
			num24 += num27 * num21;
		}
		Color color = ShaderNightSkyColor (dir);
		num22 += color.r;
		num23 += color.g;
		num24 += color.b;
		if (directLight) {
			Color color2 = ShaderMoonHaloColor (dir);
			num22 += color2.r;
			num23 += color2.g;
			num24 += color2.b;
		}
		num22 = Mathf.Lerp (num22, FogColor.r, Atmosphere.Fogginess);
		num23 = Mathf.Lerp (num23, FogColor.g, Atmosphere.Fogginess);
		num24 = Mathf.Lerp (num24, FogColor.b, Atmosphere.Fogginess);
		num22 = Mathf.Pow (num22 * Atmosphere.Brightness, Atmosphere.Contrast);
		num23 = Mathf.Pow (num23 * Atmosphere.Brightness, Atmosphere.Contrast);
		num24 = Mathf.Pow (num24 * Atmosphere.Brightness, Atmosphere.Contrast);
		return new Color (num22, num23, num24, 1f);
	}

	private void Initialize ()
	{
		Components = GetComponent<TOD_Components> ();
		Components.Initialize ();
		Resources = GetComponent<TOD_Resources> ();
		Resources.Initialize ();
		instances.Add (this);
		Initialized = true;
	}

	private void Cleanup ()
	{
		if ((bool)Probe) {
			UnityEngine.Object.Destroy (Probe.gameObject);
		}
		instances.Remove (this);
		Initialized = false;
	}

	protected void OnEnable ()
	{
		LateUpdate ();
	}

	protected void OnDisable ()
	{
		Cleanup ();
	}

	protected void LateUpdate ()
	{
		if (!Initialized) {
			Initialize ();
		}
		UpdateScattering ();
		UpdateCelestials ();
		UpdateQualitySettings ();
		UpdateRenderSettings ();
		UpdateShaderKeywords ();
		UpdateShaderProperties ();
	}

	protected void OnValidate ()
	{
		Cycle.DateTime = Cycle.DateTime;
	}

	private void UpdateScattering ()
	{
		float num = 0f - Atmosphere.Directionality;
		float num2 = num * num;
		kBetaMie.x = 1.5f * ((1f - num2) / (2f + num2));
		kBetaMie.y = 1f + num2;
		kBetaMie.z = 2f * num;
		float num3 = 0.002f * Atmosphere.MieMultiplier;
		float num4 = 0.002f * Atmosphere.RayleighMultiplier;
		float x = num4 * 40f * 5.27016449f;
		float y = num4 * 40f * 9.473284f;
		float z = num4 * 40f * 19.6438026f;
		float w = num3 * 40f;
		kSun.x = x;
		kSun.y = y;
		kSun.z = z;
		kSun.w = w;
		float x2 = num4 * 4f * (float)Math.PI * 5.27016449f;
		float y2 = num4 * 4f * (float)Math.PI * 9.473284f;
		float z2 = num4 * 4f * (float)Math.PI * 19.6438026f;
		float w2 = num3 * 4f * (float)Math.PI;
		k4PI.x = x2;
		k4PI.y = y2;
		k4PI.z = z2;
		k4PI.w = w2;
		kRadius.x = 1f;
		kRadius.y = 1f;
		kRadius.z = 1.025f;
		kRadius.w = 1.050625f;
		kScale.x = 40.00004f;
		kScale.y = 0.25f;
		kScale.z = 160.000153f;
		kScale.w = 0.0001f;
	}

	private void UpdateCelestials ()
	{
		float f = (float)Math.PI / 180f * World.Latitude;
		float num = Mathf.Sin (f);
		float num2 = Mathf.Cos (f);
		float longitude = World.Longitude;
		float num3 = (float)Math.PI / 2f;
		int year = Cycle.Year;
		int month = Cycle.Month;
		int day = Cycle.Day;
		float num4 = Cycle.Hour - World.UTC;
		float num5 = (float)(367 * year - 7 * (year + (month + 9) / 12) / 4 + 275 * month / 9 + day - 730530) + num4 / 24f;
		float num6 = (float)(367 * year - 7 * (year + (month + 9) / 12) / 4 + 275 * month / 9 + day - 730530) + 0.5f;
		float num7 = 23.4393f - 3.563E-07f * num5;
		float f2 = (float)Math.PI / 180f * num7;
		float num8 = Mathf.Sin (f2);
		float num9 = Mathf.Cos (f2);
		float num10 = 282.9404f + 4.70935E-05f * num6;
		float num11 = 0.016709f - 1.151E-09f * num6;
		float num12 = 356.047f + 0.985600233f * num6;
		float num13 = (float)Math.PI / 180f * num12;
		float num14 = Mathf.Sin (num13);
		float num15 = Mathf.Cos (num13);
		float f3 = num13 + num11 * num14 * (1f + num11 * num15);
		float num16 = Mathf.Sin (f3);
		float num17 = Mathf.Cos (f3) - num11;
		float num18 = Mathf.Sqrt (1f - num11 * num11) * num16;
		float num19 = 57.29578f * Mathf.Atan2 (num18, num17);
		float num20 = Mathf.Sqrt (num17 * num17 + num18 * num18);
		float num21 = num19 + num10;
		float f4 = (float)Math.PI / 180f * num21;
		float num22 = Mathf.Sin (f4);
		float num23 = Mathf.Cos (f4);
		float num24 = num20 * num23;
		float num25 = num20 * num22;
		float num26 = num24;
		float num27 = num25 * num9;
		float y = num25 * num8;
		float num28 = Mathf.Atan2 (num27, num26);
		float num29 = 57.29578f * num28;
		float f5 = Mathf.Atan2 (y, Mathf.Sqrt (num26 * num26 + num27 * num27));
		float num30 = Mathf.Sin (f5);
		float num31 = Mathf.Cos (f5);
		float num32 = num19 + num10 + 180f;
		float num33 = num29 - num32 - longitude;
		float num34 = -6f;
		float num35 = Mathf.Acos ((Mathf.Sin ((float)Math.PI / 180f * num34) - num * num30) / (num2 * num31));
		float num36 = 57.29578f * num35;
		SunsetTime = (24f + (num33 + num36) / 15f % 24f) % 24f;
		SunriseTime = (24f + (num33 - num36) / 15f % 24f) % 24f;
		float num37 = 282.9404f + 4.70935E-05f * num5;
		float num38 = 0.016709f - 1.151E-09f * num5;
		float num39 = 356.047f + 0.985600233f * num5;
		float num40 = (float)Math.PI / 180f * num39;
		float num41 = Mathf.Sin (num40);
		float num42 = Mathf.Cos (num40);
		float f6 = num40 + num38 * num41 * (1f + num38 * num42);
		float num43 = Mathf.Sin (f6);
		float num44 = Mathf.Cos (f6) - num38;
		float num45 = Mathf.Sqrt (1f - num38 * num38) * num43;
		float num46 = 57.29578f * Mathf.Atan2 (num45, num44);
		float num47 = Mathf.Sqrt (num44 * num44 + num45 * num45);
		float num48 = num46 + num37;
		float f7 = (float)Math.PI / 180f * num48;
		float num49 = Mathf.Sin (f7);
		float num50 = Mathf.Cos (f7);
		float num51 = num47 * num50;
		float num52 = num47 * num49;
		float num53 = num51;
		float num54 = num52 * num9;
		float y2 = num52 * num8;
		float num55 = Mathf.Atan2 (num54, num53);
		float f8 = Mathf.Atan2 (y2, Mathf.Sqrt (num53 * num53 + num54 * num54));
		float num56 = Mathf.Sin (f8);
		float num57 = Mathf.Cos (f8);
		float num58 = num46 + num37 + 180f + 15f * num4;
		float num59 = (float)Math.PI / 180f * (num58 + longitude);
		LocalSiderealTime = (num58 + longitude) / 15f;
		float f9 = num59 - num55;
		float num60 = Mathf.Sin (f9);
		float num61 = Mathf.Cos (f9) * num57;
		float num62 = num60 * num57;
		float num63 = num56;
		float num64 = num61 * num - num63 * num2;
		float num65 = num62;
		float y3 = num61 * num2 + num63 * num;
		float num66 = Mathf.Atan2 (num65, num64) + (float)Math.PI;
		float num67 = Mathf.Atan2 (y3, Mathf.Sqrt (num64 * num64 + num65 * num65));
		float num68 = num3 - num67;
		float num69 = num67;
		float num70 = num66;
		SunZenith = 57.29578f * num68;
		SunAltitude = 57.29578f * num69;
		SunAzimuth = 57.29578f * num70;
		float num111;
		float num112;
		float num113;
		if (Moon.Position == TOD_MoonPositionType.Realistic) {
			float num71 = 125.1228f - 0.05295381f * num5;
			float num72 = 5.1454f;
			float num73 = 318.0634f + 0.164357319f * num5;
			float num74 = 0.0549f;
			float num75 = 115.3654f + 13.0649929f * num5;
			float f10 = (float)Math.PI / 180f * num71;
			float num76 = Mathf.Sin (f10);
			float num77 = Mathf.Cos (f10);
			float f11 = (float)Math.PI / 180f * num72;
			float num78 = Mathf.Sin (f11);
			float num79 = Mathf.Cos (f11);
			float num80 = (float)Math.PI / 180f * num75;
			float num81 = Mathf.Sin (num80);
			float num82 = Mathf.Cos (num80);
			float f12 = num80 + num74 * num81 * (1f + num74 * num82);
			float num83 = Mathf.Sin (f12);
			float num84 = Mathf.Cos (f12);
			float num85 = 60.2666f * (num84 - num74);
			float num86 = 60.2666f * (Mathf.Sqrt (1f - num74 * num74) * num83);
			float num87 = 57.29578f * Mathf.Atan2 (num86, num85);
			float num88 = Mathf.Sqrt (num85 * num85 + num86 * num86);
			float num89 = num87 + num73;
			float f13 = (float)Math.PI / 180f * num89;
			float num90 = Mathf.Sin (f13);
			float num91 = Mathf.Cos (f13);
			float num92 = num88 * (num77 * num91 - num76 * num90 * num79);
			float num93 = num88 * (num76 * num91 + num77 * num90 * num79);
			float num94 = num88 * (num90 * num78);
			float num95 = num92;
			float num96 = num93;
			float num97 = num94;
			float num98 = num95;
			float num99 = num96 * num9 - num97 * num8;
			float y4 = num96 * num8 + num97 * num9;
			float num100 = Mathf.Atan2 (num99, num98);
			float f14 = Mathf.Atan2 (y4, Mathf.Sqrt (num98 * num98 + num99 * num99));
			float num101 = Mathf.Sin (f14);
			float num102 = Mathf.Cos (f14);
			float f15 = num59 - num100;
			float num103 = Mathf.Sin (f15);
			float num104 = Mathf.Cos (f15) * num102;
			float num105 = num103 * num102;
			float num106 = num101;
			float num107 = num104 * num - num106 * num2;
			float num108 = num105;
			float y5 = num104 * num2 + num106 * num;
			float num109 = Mathf.Atan2 (num108, num107) + (float)Math.PI;
			float num110 = Mathf.Atan2 (y5, Mathf.Sqrt (num107 * num107 + num108 * num108));
			num111 = num3 - num110;
			num112 = num110;
			num113 = num109;
		} else {
			num111 = num68 - (float)Math.PI;
			num112 = num69 - (float)Math.PI;
			num113 = num70;
		}
		MoonZenith = 57.29578f * num111;
		MoonAltitude = 57.29578f * num112;
		MoonAzimuth = 57.29578f * num113;
		Quaternion quaternion = Quaternion.Euler (90f - World.Latitude, 0f, 0f) * Quaternion.Euler (0f, 180f + num59 * 57.29578f, 0f);
		if (Stars.Position == TOD_StarsPositionType.Rotating) {
			Components.SpaceTransform.localRotation = quaternion;
			Components.StarTransform.localRotation = quaternion;
		} else {
			Components.SpaceTransform.localRotation = Quaternion.identity;
			Components.StarTransform.localRotation = Quaternion.identity;
		}
		Vector3 localPosition = OrbitalToLocal (num68, num70);
		Components.SunTransform.localPosition = localPosition;
		Components.SunTransform.LookAt (Components.DomeTransform.position, Components.SunTransform.up);
		Vector3 localPosition2 = OrbitalToLocal (num111, num113);
		Vector3 worldUp = quaternion * -Vector3.right;
		Components.MoonTransform.localPosition = localPosition2;
		Components.MoonTransform.LookAt (Components.DomeTransform.position, worldUp);
		float num114 = 8f * Mathf.Tan ((float)Math.PI / 360f * Sun.MeshSize);
		Vector3 localScale = new Vector3 (num114, num114, num114);
		Components.SunTransform.localScale = localScale;
		float num115 = 4f * Mathf.Tan ((float)Math.PI / 360f * Moon.MeshSize);
		Vector3 localScale2 = new Vector3 (num115, num115, num115);
		Components.MoonTransform.localScale = localScale2;
		bool flag = (1f - Atmosphere.Fogginess) * (1f - LerpValue) > 0f;
		Components.SpaceRenderer.enabled = flag;
		Components.StarRenderer.enabled = flag;
		bool flag2 = Components.SunTransform.localPosition.y > 0f - num114;
		Components.SunRenderer.enabled = flag2;
		bool flag3 = Components.MoonTransform.localPosition.y > 0f - num115;
		Components.MoonRenderer.enabled = flag3;
		bool flag4 = true;
		Components.AtmosphereRenderer.enabled = flag4;
		bool flag5 = false;
		Components.ClearRenderer.enabled = flag5;
		bool flag6 = Clouds.Coverage > 0f && Clouds.Opacity > 0f;
		Components.CloudRenderer.enabled = flag6;
		LerpValue = Mathf.InverseLerp (105f, 90f, SunZenith);
		float time = Mathf.Clamp01 (SunZenith / 90f);
		float time2 = Mathf.Clamp01 ((SunZenith - 90f) / 90f);
		float num116 = Mathf.Clamp01 ((LerpValue - 0.1f) / 0.9f);
		float num117 = Mathf.Clamp01 ((0.1f - LerpValue) / 0.1f);
		float num118 = Mathf.Clamp01 ((90f - num111 * 57.29578f) / 5f);
		SunVisibility = (1f - Atmosphere.Fogginess) * num116;
		MoonVisibility = (1f - Atmosphere.Fogginess) * num117 * num118;
		SunLightColor = TOD_Util.ApplyAlpha (Day.LightColor.Evaluate (time));
		MoonLightColor = TOD_Util.ApplyAlpha (Night.LightColor.Evaluate (time2));
		SunRayColor = TOD_Util.ApplyAlpha (Day.RayColor.Evaluate (time));
		MoonRayColor = TOD_Util.ApplyAlpha (Night.RayColor.Evaluate (time2));
		SunSkyColor = TOD_Util.ApplyAlpha (Day.SkyColor.Evaluate (time));
		MoonSkyColor = TOD_Util.ApplyAlpha (Night.SkyColor.Evaluate (time2));
		SunMeshColor = TOD_Util.ApplyAlpha (Day.SunColor.Evaluate (time));
		MoonMeshColor = TOD_Util.ApplyAlpha (Night.MoonColor.Evaluate (time2));
		SunCloudColor = TOD_Util.ApplyAlpha (Day.CloudColor.Evaluate (time));
		MoonCloudColor = TOD_Util.ApplyAlpha (Night.CloudColor.Evaluate (time2));
		Color b = TOD_Util.ApplyAlpha (Day.FogColor.Evaluate (time));
		Color a = TOD_Util.ApplyAlpha (Night.FogColor.Evaluate (time2));
		FogColor = Color.Lerp (a, b, LerpValue);
		Color color = TOD_Util.ApplyAlpha (Day.AmbientColor.Evaluate (time));
		Color color2 = TOD_Util.ApplyAlpha (Night.AmbientColor.Evaluate (time2));
		AmbientColor = Color.Lerp (color2, color, LerpValue);
		Color b2 = color;
		Color a2 = color2;
		GroundColor = Color.Lerp (a2, b2, LerpValue);
		MoonHaloColor = TOD_Util.MulRGB (MoonSkyColor, Moon.HaloBrightness * num118);
		float shadowStrength;
		float intensity;
		Color color3;
		if (LerpValue > 0.1f) {
			IsDay = true;
			IsNight = false;
			shadowStrength = Day.ShadowStrength;
			intensity = Mathf.Lerp (0f, Day.LightIntensity, SunVisibility);
			color3 = SunLightColor;
		} else {
			IsDay = false;
			IsNight = true;
			shadowStrength = Night.ShadowStrength;
			intensity = Mathf.Lerp (0f, Night.LightIntensity, MoonVisibility);
			color3 = MoonLightColor;
		}
		Components.LightSource.color = color3;
		Components.LightSource.intensity = intensity;
		Components.LightSource.shadowStrength = shadowStrength;
		if (!Application.isPlaying || timeSinceLightUpdate >= Light.UpdateInterval) {
			timeSinceLightUpdate = 0f;
			Vector3 localPosition3 = (IsNight ? OrbitalToLocal (Mathf.Min (num111, (1f - Light.MinimumHeight) * (float)Math.PI / 2f), num113) : OrbitalToLocal (Mathf.Min (num68, (1f - Light.MinimumHeight) * (float)Math.PI / 2f), num70));
			Components.LightTransform.localPosition = localPosition3;
			Components.LightTransform.LookAt (Components.DomeTransform.position);
		} else {
			timeSinceLightUpdate += Time.deltaTime;
		}
		SunDirection = -Components.SunTransform.forward;
		LocalSunDirection = Components.DomeTransform.InverseTransformDirection (SunDirection);
		MoonDirection = -Components.MoonTransform.forward;
		LocalMoonDirection = Components.DomeTransform.InverseTransformDirection (MoonDirection);
		LightDirection = -Components.LightTransform.forward;
		LocalLightDirection = Components.DomeTransform.InverseTransformDirection (LightDirection);
	}
}
