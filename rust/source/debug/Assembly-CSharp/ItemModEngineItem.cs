using Rust.Modular;
using UnityEngine;

public class ItemModEngineItem : ItemMod
{
	public EngineStorage.EngineItemTypes engineItemType;

	[Range (1f, 3f)]
	public int tier = 1;
}
