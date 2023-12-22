using Rust.UI;
using UnityEngine;

public class ServerBrowserTag : MonoBehaviour
{
	public string serverTag;

	public RustButton button;

	public bool IsActive => button != null && button.IsPressed;
}
