using System;

public class BoundsCheck : PrefabAttribute
{
	public enum BlockType
	{
		Tree
	}

	public BlockType IsType = BlockType.Tree;

	protected override Type GetIndexedType ()
	{
		return typeof(BoundsCheck);
	}
}
