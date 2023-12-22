using System.Runtime.InteropServices;

[StructLayout (LayoutKind.Explicit)]
public struct RenderSlice
{
	[FieldOffset (0)]
	public uint StartIndex;

	[FieldOffset (4)]
	public uint Length;
}
