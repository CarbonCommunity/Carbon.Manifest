using System;
using System.Runtime.InteropServices;
using Epic.OnlineServices;
using Epic.OnlineServices.PlayerDataStorage;

[StructLayout (LayoutKind.Sequential, Pack = 8)]
internal struct ReadFileCallbackInfoInternal : ICallbackInfoInternal, IGettable<ReadFileCallbackInfo>, ISettable<ReadFileCallbackInfo>, IDisposable
{
	private Result m_ResultCode;

	private IntPtr m_ClientData;

	private IntPtr m_LocalUserId;

	private IntPtr m_Filename;

	public Result ResultCode {
		get {
			return m_ResultCode;
		}
		set {
			m_ResultCode = value;
		}
	}

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

	public void Set (ref ReadFileCallbackInfo other)
	{
		ResultCode = other.ResultCode;
		ClientData = other.ClientData;
		LocalUserId = other.LocalUserId;
		Filename = other.Filename;
	}

	public void Set (ref ReadFileCallbackInfo? other)
	{
		if (other.HasValue) {
			ResultCode = other.Value.ResultCode;
			ClientData = other.Value.ClientData;
			LocalUserId = other.Value.LocalUserId;
			Filename = other.Value.Filename;
		}
	}

	public void Dispose ()
	{
		Helper.Dispose (ref m_ClientData);
		Helper.Dispose (ref m_LocalUserId);
		Helper.Dispose (ref m_Filename);
	}

	public void Get (out ReadFileCallbackInfo output)
	{
		output = default(ReadFileCallbackInfo);
		output.Set (ref this);
	}
}
