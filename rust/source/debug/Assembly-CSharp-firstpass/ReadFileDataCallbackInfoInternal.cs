using System;
using System.Runtime.InteropServices;
using Epic.OnlineServices;
using Epic.OnlineServices.PlayerDataStorage;

[StructLayout (LayoutKind.Sequential, Pack = 8)]
internal struct ReadFileDataCallbackInfoInternal : ICallbackInfoInternal, IGettable<ReadFileDataCallbackInfo>, ISettable<ReadFileDataCallbackInfo>, IDisposable
{
	private IntPtr m_ClientData;

	private IntPtr m_LocalUserId;

	private IntPtr m_Filename;

	private uint m_TotalFileSizeBytes;

	private int m_IsLastChunk;

	private uint m_DataChunkLengthBytes;

	private IntPtr m_DataChunk;

	public object ClientData {
		get {
			Helper.Get (m_ClientData, out object to);
			return to;
		}
		set {
			Helper.Set (value, ref m_ClientData);
		}
	}

	public IntPtr ClientDataAddress => m_ClientData;

	public ProductUserId LocalUserId {
		get {
			Helper.Get (m_LocalUserId, out ProductUserId to);
			return to;
		}
		set {
			Helper.Set (value, ref m_LocalUserId);
		}
	}

	public Utf8String Filename {
		get {
			Helper.Get (m_Filename, out Utf8String to);
			return to;
		}
		set {
			Helper.Set (value, ref m_Filename);
		}
	}

	public uint TotalFileSizeBytes {
		get {
			return m_TotalFileSizeBytes;
		}
		set {
			m_TotalFileSizeBytes = value;
		}
	}

	public bool IsLastChunk {
		get {
			Helper.Get (m_IsLastChunk, out var to);
			return to;
		}
		set {
			Helper.Set (value, ref m_IsLastChunk);
		}
	}

	public ArraySegment<byte> DataChunk {
		get {
			Helper.Get (m_DataChunk, out var to, m_DataChunkLengthBytes);
			return to;
		}
		set {
			Helper.Set (value, ref m_DataChunk, out m_DataChunkLengthBytes);
		}
	}

	public void Set (ref ReadFileDataCallbackInfo other)
	{
		ClientData = other.ClientData;
		LocalUserId = other.LocalUserId;
		Filename = other.Filename;
		TotalFileSizeBytes = other.TotalFileSizeBytes;
		IsLastChunk = other.IsLastChunk;
		DataChunk = other.DataChunk;
	}

	public void Set (ref ReadFileDataCallbackInfo? other)
	{
		if (other.HasValue) {
			ClientData = other.Value.ClientData;
			LocalUserId = other.Value.LocalUserId;
			Filename = other.Value.Filename;
			TotalFileSizeBytes = other.Value.TotalFileSizeBytes;
			IsLastChunk = other.Value.IsLastChunk;
			DataChunk = other.Value.DataChunk;
		}
	}

	public void Dispose ()
	{
		Helper.Dispose (ref m_ClientData);
		Helper.Dispose (ref m_LocalUserId);
		Helper.Dispose (ref m_Filename);
		Helper.Dispose (ref m_DataChunk);
	}

	public void Get (out ReadFileDataCallbackInfo output)
	{
		output = default(ReadFileDataCallbackInfo);
		output.Set (ref this);
	}
}
