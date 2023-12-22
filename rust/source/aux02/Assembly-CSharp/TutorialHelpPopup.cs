using Rust.UI;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class TutorialHelpPopup : SingletonComponent<TutorialHelpPopup>
{
	public RustText HelpText;

	public Image HelpImage;

	public VideoPlayer HelpVideo;

	public RawImage HelpVideoTexture;

	public CanvasGroup Group;
}
