using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

public abstract class TerrainMap<T> : TerrainMap where T : struct
{
	internal T[] src;

	internal T[] dst;

	public void Push ()
	{
		if (src == dst) {
			dst = (T[])src.Clone ();
		}
	}

	public void Pop ()
	{
		if (src != dst) {
			Array.Copy (dst, src, src.Length);
			dst = src;
		}
	}

	public IEnumerable<T> ToEnumerable ()
	{
		return src.Cast<T> ();
	}

	public int BytesPerElement ()
	{
		return Marshal.SizeOf (typeof(T));
	}

	public long GetMemoryUsage ()
	{
		return (long)BytesPerElement () * (long)src.Length;
	}

	public byte[] ToByteArray ()
	{
		byte[] array = new byte[BytesPerElement () * src.Length];
		Buffer.BlockCopy (src, 0, array, 0, array.Length);
		return array;
	}

	public void FromByteArray (byte[] dat)
	{
		Buffer.BlockCopy (dat, 0, dst, 0, dat.Length);
	}
}
