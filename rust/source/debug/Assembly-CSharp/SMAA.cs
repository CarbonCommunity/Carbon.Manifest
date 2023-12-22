using Smaa;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent (typeof(Camera))]
[AddComponentMenu ("Image Effects/Subpixel Morphological Antialiasing")]
public class SMAA : MonoBehaviour
{
	public DebugPass DebugPass = DebugPass.Off;

	public QualityPreset Quality = QualityPreset.High;

	public EdgeDetectionMethod DetectionMethod = EdgeDetectionMethod.Luma;

	public bool UsePredication = false;

	public Preset CustomPreset;

	public PredicationPreset CustomPredicationPreset;

	public Shader Shader;

	public Texture2D AreaTex;

	public Texture2D SearchTex;

	protected Camera m_Camera;

	protected Preset m_LowPreset;

	protected Preset m_MediumPreset;

	protected Preset m_HighPreset;

	protected Preset m_UltraPreset;

	protected Material m_Material;

	public Material Material {
		get {
			if (m_Material == null) {
				m_Material = new Material (Shader);
				m_Material.hideFlags = HideFlags.HideAndDontSave;
			}
			return m_Material;
		}
	}
}
