using Kino;
using UnityEngine;

[RequireComponent (typeof(Camera))]
[AddComponentMenu ("Kino Image Effects/Motion")]
public class Motion : MonoBehaviour
{
	public enum ExposureMode
	{
		Constant,
		DeltaTime
	}

	public enum SampleCount
	{
		Low,
		Medium,
		High,
		Variable
	}

	private enum DebugMode
	{
		Off,
		Velocity,
		NeighborMax,
		Depth
	}

	[SerializeField]
	[Tooltip ("How the exposure time (shutter speed) is determined.")]
	private ExposureMode _exposureMode = ExposureMode.DeltaTime;

	[SerializeField]
	[Tooltip ("The denominator of the shutter speed.")]
	private int _shutterSpeed = 30;

	[SerializeField]
	[Tooltip ("The scale factor to the exposure time.")]
	private float _exposureTimeScale = 1f;

	[SerializeField]
	[Tooltip ("The amount of sample points, which affects quality and performance.")]
	private SampleCount _sampleCount = SampleCount.Medium;

	[SerializeField]
	private int _sampleCountValue = 12;

	[SerializeField]
	[Range (0.5f, 10f)]
	[Tooltip ("The maximum length of blur trails, specified as a percentage to the screen height. Large values may introduce artifacts.")]
	private float _maxBlurRadius = 3.5f;

	[SerializeField]
	[Tooltip ("The debug visualization mode.")]
	private DebugMode _debugMode = DebugMode.Off;

	private Material _prefilterMaterial;

	private Material _reconstructionMaterial;

	public ExposureMode exposureMode {
		get {
			return _exposureMode;
		}
		set {
			_exposureMode = value;
		}
	}

	public int shutterSpeed {
		get {
			return _shutterSpeed;
		}
		set {
			_shutterSpeed = value;
		}
	}

	public float exposureTimeScale {
		get {
			return _exposureTimeScale;
		}
		set {
			_exposureTimeScale = value;
		}
	}

	public SampleCount sampleCount {
		get {
			return _sampleCount;
		}
		set {
			_sampleCount = value;
		}
	}

	public int sampleCountValue {
		get {
			return _sampleCount switch {
				SampleCount.Low => 8, 
				SampleCount.Medium => 16, 
				SampleCount.High => 32, 
				_ => Mathf.Clamp (_sampleCountValue, 2, 128), 
			};
		}
		set {
			_sampleCountValue = value;
		}
	}

	public float maxBlurRadius {
		get {
			return Mathf.Clamp (_maxBlurRadius, 0.5f, 10f);
		}
		set {
			_maxBlurRadius = value;
		}
	}

	private float VelocityScale {
		get {
			if (exposureMode == ExposureMode.Constant) {
				return 1f / ((float)shutterSpeed * Time.smoothDeltaTime);
			}
			return exposureTimeScale;
		}
	}

	private RenderTexture GetTemporaryRT (Texture source, int divider, RenderTextureFormat format, RenderTextureReadWrite rw)
	{
		int width = Mathf.Clamp (source.width / divider, 1, 65536);
		int height = Mathf.Clamp (source.height / divider, 1, 65536);
		RenderTexture temporary = RenderTexture.GetTemporary (width, height, 0, format, rw);
		temporary.filterMode = FilterMode.Point;
		return temporary;
	}

	private void ReleaseTemporaryRT (RenderTexture rt)
	{
		RenderTexture.ReleaseTemporary (rt);
	}

	private void OnEnable ()
	{
		Shader shader = Shader.Find ("Hidden/Kino/Motion/Prefilter");
		_prefilterMaterial = new Material (shader);
		_prefilterMaterial.hideFlags = HideFlags.DontSave;
		Shader shader2 = Shader.Find ("Hidden/Kino/Motion/Reconstruction");
		_reconstructionMaterial = new Material (shader2);
		_reconstructionMaterial.hideFlags = HideFlags.DontSave;
		GetComponent<Camera> ().depthTextureMode |= DepthTextureMode.Depth | DepthTextureMode.MotionVectors;
	}

	private void OnDisable ()
	{
		Object.DestroyImmediate (_prefilterMaterial);
		_prefilterMaterial = null;
		Object.DestroyImmediate (_reconstructionMaterial);
		_reconstructionMaterial = null;
	}

	private void OnRenderImage (RenderTexture source, RenderTexture destination)
	{
		int num = (int)(maxBlurRadius * (float)source.height / 100f);
		int num2 = ((num - 1) / 8 + 1) * 8;
		_prefilterMaterial.SetFloat ("_VelocityScale", VelocityScale);
		_prefilterMaterial.SetFloat ("_MaxBlurRadius", num);
		RenderTexture temporaryRT = GetTemporaryRT (source, 1, RenderTextureFormat.ARGB2101010, RenderTextureReadWrite.Linear);
		Graphics.Blit (null, temporaryRT, _prefilterMaterial, 0);
		RenderTexture temporaryRT2 = GetTemporaryRT (source, 4, RenderTextureFormat.RGHalf, RenderTextureReadWrite.Linear);
		Graphics.Blit (temporaryRT, temporaryRT2, _prefilterMaterial, 1);
		RenderTexture temporaryRT3 = GetTemporaryRT (source, 8, RenderTextureFormat.RGHalf, RenderTextureReadWrite.Linear);
		Graphics.Blit (temporaryRT2, temporaryRT3, _prefilterMaterial, 2);
		ReleaseTemporaryRT (temporaryRT2);
		Vector2 vector = Vector2.one * ((float)num2 / 8f - 1f) * -0.5f;
		_prefilterMaterial.SetVector ("_TileMaxOffs", vector);
		_prefilterMaterial.SetInt ("_TileMaxLoop", num2 / 8);
		RenderTexture temporaryRT4 = GetTemporaryRT (source, num2, RenderTextureFormat.RGHalf, RenderTextureReadWrite.Linear);
		Graphics.Blit (temporaryRT3, temporaryRT4, _prefilterMaterial, 3);
		ReleaseTemporaryRT (temporaryRT3);
		RenderTexture temporaryRT5 = GetTemporaryRT (source, num2, RenderTextureFormat.RGHalf, RenderTextureReadWrite.Linear);
		Graphics.Blit (temporaryRT4, temporaryRT5, _prefilterMaterial, 4);
		ReleaseTemporaryRT (temporaryRT4);
		int value = Mathf.Max (sampleCountValue / 2, 1);
		_reconstructionMaterial.SetInt ("_LoopCount", value);
		_reconstructionMaterial.SetFloat ("_MaxBlurRadius", num);
		_reconstructionMaterial.SetTexture ("_NeighborMaxTex", temporaryRT5);
		_reconstructionMaterial.SetTexture ("_VelocityTex", temporaryRT);
		Graphics.Blit (source, destination, _reconstructionMaterial, (int)_debugMode);
		ReleaseTemporaryRT (temporaryRT);
		ReleaseTemporaryRT (temporaryRT5);
	}
}
