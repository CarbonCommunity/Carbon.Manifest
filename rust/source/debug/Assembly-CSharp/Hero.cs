using Rust.UI;
using Rust.UI.MainMenu;
using UnityEngine;

public class Hero : SingletonComponent<Hero>
{
	public CanvasGroup CanvasGroup;

	public Video VideoPlayer;

	public RustText TitleText;

	public RustText ButtonText;

	public HttpImage TitleImage;

	[Header ("Item Store Links")]
	public RustButton ItemStoreButton;

	public RustButton LimitedTabButton;

	public RustButton GeneralTabButton;
}
