using System;
using System.Collections.Generic;
using System.IO;
using ConVar;
using UnityEngine;

public class RustEmojiLibrary : BaseScriptableObject
{
	public struct ServerEmojiConfig
	{
		public uint CRC;

		public FileStorage.Type FileType;
	}

	public enum EmojiType
	{
		Core,
		Item,
		Server
	}

	[Serializable]
	public struct EmojiSource
	{
		public string Name;

		public EmojiType Type;

		public EmojiResult[] Emoji;

		public SteamDLCItem RequiredDLC;

		public SteamInventoryItem RequiredItem;

		public uint ServerCrc;

		public FileStorage.Type ServerFileType;

		public bool HasSkinTone => Emoji.Length > 1;

		public EmojiResult GetEmojiIndex (int index)
		{
			return Emoji [Mathf.Clamp (index, 0, Emoji.Length - 1)];
		}

		public bool CanBeUsedBy (BasePlayer p)
		{
			if (RequiredDLC != null && !RequiredDLC.CanUse (p)) {
				return false;
			}
			if (RequiredItem != null && !RequiredItem.HasUnlocked (p.userID)) {
				return false;
			}
			return true;
		}

		public bool StringMatch (string input, out int index)
		{
			index = 0;
			if (Name.Equals (input, StringComparison.CurrentCultureIgnoreCase)) {
				return true;
			}
			for (int i = 0; i < Emoji.Length; i++) {
				if ($"{Name}+{i}".Equals (input, StringComparison.CurrentCultureIgnoreCase)) {
					index = i;
					return true;
				}
			}
			return false;
		}
	}

	private const long MAX_FILE_SIZE_BYTES = 250000L;

	private const int MAX_TEX_SIZE_PIXELS = 256;

	public static Dictionary<string, ServerEmojiConfig> allServerEmoji = new Dictionary<string, ServerEmojiConfig> ();

	private static bool hasLoaded = false;

	public static NetworkableId EmojiStorageNetworkId = new NetworkableId (0uL);

	[HideInInspector]
	public RustEmojiConfig[] Configs;

	public RenderTexture DefaultRenderTexture;

	public int InitialPoolSize = 10;

	private List<EmojiSource> all = new List<EmojiSource> ();

	private List<EmojiSource> conditionalAccessOnly = new List<EmojiSource> ();

	public GameObjectRef VideoPlayerRef;

	private static RustEmojiLibrary _instance = null;

	private static bool hasPrewarmed = false;

	public static RustEmojiLibrary Instance {
		get {
			if (_instance == null) {
				_instance = FileSystem.Load<RustEmojiLibrary> ("assets/content/ui/gameui/emoji/rustemojilibrary.asset");
				_instance.Prewarm ();
			}
			return _instance;
		}
	}

	public static void FindAllServerEmoji ()
	{
		if (hasLoaded) {
			return;
		}
		hasLoaded = true;
		string serverFolder = Server.GetServerFolder ("serveremoji");
		if (!Directory.Exists (serverFolder)) {
			return;
		}
		foreach (string item in Directory.EnumerateFiles (serverFolder)) {
			try {
				if (!CheckByteArray (new FileInfo (item).Length)) {
					Debug.Log ($"{serverFolder} file size is too big for emoji, max file size is {250000L} bytes");
					continue;
				}
				byte[] array = File.ReadAllBytes (item);
				if (!CheckTextureSize (array, out var texWidth, out var texHeight)) {
					Debug.Log ($"{item} is too large, it's size is {texWidth}x{texHeight} and the maximum is {256}x{256}");
				} else if (item.EndsWith (".png") || item.EndsWith (".jpg")) {
					FileStorage.Type type = FileStorage.Type.jpg;
					if (item.EndsWith (".png")) {
						type = FileStorage.Type.png;
					}
					uint cRC = FileStorage.server.Store (array, type, EmojiStorageNetworkId);
					string[] array2 = item.Split ('/', '\\');
					string text = array2 [array2.Length - 1];
					text = text.Replace (".png", string.Empty);
					text = text.Replace (".jpg", string.Empty);
					if (!allServerEmoji.ContainsKey (text)) {
						allServerEmoji.Add (text, new ServerEmojiConfig {
							CRC = cRC,
							FileType = type
						});
					}
				}
			} catch (Exception arg) {
				Debug.Log ($"Exception loading {item} - {arg}");
			}
		}
	}

	public static void ResetServerEmoji ()
	{
		hasLoaded = false;
		allServerEmoji.Clear ();
		FindAllServerEmoji ();
	}

	public static bool CheckByteArray (long arrayLength)
	{
		return arrayLength <= 250000;
	}

	public static bool CheckByteArray (int arrayLength)
	{
		return (long)arrayLength <= 250000L;
	}

	public static bool CheckTextureSize (byte[] bytes, out int texWidth, out int texHeight)
	{
		Texture2D texture2D = new Texture2D (1, 1);
		texture2D.LoadImage (bytes);
		texWidth = texture2D.width;
		texHeight = texture2D.height;
		UnityEngine.Object.Destroy (texture2D);
		if (texWidth <= 256) {
			return texHeight <= 256;
		}
		return false;
	}

	private void Prewarm ()
	{
		if (hasPrewarmed) {
			return;
		}
		hasPrewarmed = true;
		all.Clear ();
		conditionalAccessOnly.Clear ();
		RustEmojiConfig[] configs = Configs;
		foreach (RustEmojiConfig rustEmojiConfig in configs) {
			if (!rustEmojiConfig.Hide) {
				all.Add (rustEmojiConfig.Source);
				if (rustEmojiConfig.Source.RequiredItem != null || rustEmojiConfig.Source.RequiredDLC != null) {
					conditionalAccessOnly.Add (rustEmojiConfig.Source);
				}
			}
		}
		foreach (ItemDefinition item in ItemManager.itemList) {
			if (!item.hidden && !(item.iconSprite == null)) {
				all.Add (new EmojiSource {
					Name = item.shortname,
					Type = EmojiType.Item,
					Emoji = new EmojiResult[1] {
						new EmojiResult {
							Sprite = item.iconSprite
						}
					}
				});
			}
		}
	}

	public bool TryGetEmoji (string key, out EmojiSource er, out int skinVariantIndex, out int allIndex, bool serverSide = false)
	{
		er = default(EmojiSource);
		skinVariantIndex = 0;
		allIndex = 0;
		Prewarm ();
		foreach (EmojiSource item in serverSide ? conditionalAccessOnly : all) {
			if (item.Type != EmojiType.Server || !Global.blockServerEmoji) {
				if (item.StringMatch (key, out skinVariantIndex)) {
					er = item;
					return true;
				}
				allIndex++;
			}
		}
		return false;
	}
}
