using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChatEntry : MonoBehaviour
{
	public TextMeshProUGUI text;

	public RawImage avatar;

	public CanvasGroup canvasGroup;

	public float lifeStarted = 0f;

	public ulong steamid;

	public Phrase LocalPhrase = new Phrase ("local", "local");

	public Phrase CardsPhrase = new Phrase ("cards", "cards");

	public Phrase TeamPhrase = new Phrase ("team", "team");

	public TmProEmojiRedirector EmojiRedirector = null;
}
