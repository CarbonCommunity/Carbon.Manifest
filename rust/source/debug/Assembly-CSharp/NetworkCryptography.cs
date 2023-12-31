#define ENABLE_PROFILER
using System;
using Network;
using UnityEngine.Profiling;

public abstract class NetworkCryptography : INetworkCryptography
{
	private byte[] buffer = new byte[8388608];

	public unsafe ArraySegment<byte> EncryptCopy (Connection connection, ArraySegment<byte> data)
	{
		Profiler.BeginSample ("NetworkCryptography.EncryptCopy");
		ArraySegment<byte> src = new ArraySegment<byte> (data.Array, data.Offset, data.Count);
		ArraySegment<byte> dst = new ArraySegment<byte> (buffer, data.Offset, buffer.Length - data.Offset);
		if (data.Offset > 0) {
			fixed (byte* destination = dst.Array) {
				fixed (byte* source = data.Array) {
					Buffer.MemoryCopy (source, destination, dst.Array.Length, data.Offset);
				}
			}
		}
		EncryptionHandler (connection, src, ref dst);
		Profiler.EndSample ();
		return dst;
	}

	public unsafe ArraySegment<byte> DecryptCopy (Connection connection, ArraySegment<byte> data)
	{
		Profiler.BeginSample ("NetworkCryptography.DecryptCopy");
		ArraySegment<byte> src = new ArraySegment<byte> (data.Array, data.Offset, data.Count);
		ArraySegment<byte> dst = new ArraySegment<byte> (buffer, data.Offset, buffer.Length - data.Offset);
		if (data.Offset > 0) {
			fixed (byte* destination = dst.Array) {
				fixed (byte* source = data.Array) {
					Buffer.MemoryCopy (source, destination, dst.Array.Length, data.Offset);
				}
			}
		}
		DecryptionHandler (connection, src, ref dst);
		Profiler.EndSample ();
		return dst;
	}

	public void Encrypt (Connection connection, ref ArraySegment<byte> data)
	{
		Profiler.BeginSample ("NetworkCryptography.Encrypt");
		ArraySegment<byte> src = new ArraySegment<byte> (data.Array, data.Offset, data.Count);
		ArraySegment<byte> dst = new ArraySegment<byte> (data.Array, data.Offset, data.Array.Length - data.Offset);
		EncryptionHandler (connection, src, ref dst);
		Profiler.EndSample ();
		data = dst;
	}

	public void Decrypt (Connection connection, ref ArraySegment<byte> data)
	{
		Profiler.BeginSample ("NetworkCryptography.Decrypt");
		ArraySegment<byte> src = new ArraySegment<byte> (data.Array, data.Offset, data.Count);
		ArraySegment<byte> dst = new ArraySegment<byte> (data.Array, data.Offset, data.Array.Length - data.Offset);
		DecryptionHandler (connection, src, ref dst);
		Profiler.EndSample ();
		data = dst;
	}

	protected abstract void EncryptionHandler (Connection connection, ArraySegment<byte> src, ref ArraySegment<byte> dst);

	protected abstract void DecryptionHandler (Connection connection, ArraySegment<byte> src, ref ArraySegment<byte> dst);
}
