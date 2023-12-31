using System;
using System.Runtime.InteropServices;

[StructLayout (LayoutKind.Sequential, Pack = 8)]
internal struct ProtectMessageOptionsInternal
{
	public int m_ApiVersion;

	public uint m_DataLengthBytes;

	public IntPtr m_Data;

	public uint m_OutBufferSizeBytes;
}
