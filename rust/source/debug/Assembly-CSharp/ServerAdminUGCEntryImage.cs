using Rust.UI;
using Rust.UI.ServerAdmin;
using UnityEngine;
using UnityEngine.UI;

public class ServerAdminUGCEntryImage : ServerAdminUGCEntry
{
	public RawImage Image;

	public RectTransform Backing;

	public GameObject MultiImageRoot = null;

	public RustText ImageIndex = null;

	public Vector2 OriginalImageSize;
}
