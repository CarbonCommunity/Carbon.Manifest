using Rust.UI;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class TutorialHelpModalStageWidget : MonoBehaviour
{
	public RustText HelpText;

	public Image HelpImage;

	public VideoPlayer HelpVideo;

	public RawImage HelpVideoTexture;

	public RustText StageCountText;

	public GameObject PreviousButton;

	public GameObject NextButton;

	public Image[] StageIndicators;

	public Color SelectedColour = Color.white;

	public Color DeselectedColour = Color.white.WithAlpha (0.5f);
}
