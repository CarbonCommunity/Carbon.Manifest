using System;
using System.Runtime.InteropServices;
using Epic.OnlineServices;
using Epic.OnlineServices.CustomInvites;

[StructLayout (LayoutKind.Sequential, Pack = 8)]
internal struct RejectRequestToJoinCallbackInfoInternal : ICallbackInfoInternal, IGettable<RejectRequestToJoinCallbackInfo>, ISettable<RejectRequestToJoinCallbackInfo>, IDisposable
{
	private Result m_ResultCode;

	private IntPtr m_ClientData;

	private IntPtr m_LocalUserId;

	private IntPtr m_TargetUserId;

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

	public ProductUserId TargetUserId {
		get {
			Helper.Get (m_TargetUserId, out ProductUserId to);
			return to;
		}
		set {
			Helper.Set (value, ref m_TargetUserId);
		}
	}

	public void Set (ref RejectRequestToJoinCallbackInfo other)
	{
		ResultCode = other.ResultCode;
		ClientData = other.ClientData;
		LocalUserId = other.LocalUserId;
		TargetUserId = other.TargetUserId;
	}

	public void Set (ref RejectRequestToJoinCallbackInfo? other)
	{
		if (other.HasValue) {
			ResultCode = other.Value.ResultCode;
			ClientData = other.Value.ClientData;
			LocalUserId = other.Value.LocalUserId;
			TargetUserId = other.Value.TargetUserId;
		}
	}

	public void Dispose ()
	{
		Helper.Dispose (ref m_ClientData);
		Helper.Dispose (ref m_LocalUserId);
		Helper.Dispose (ref m_TargetUserId);
	}

	public void Get (out RejectRequestToJoinCallbackInfo output)
	{
		output = default(RejectRequestToJoinCallbackInfo);
		output.Set (ref this);
	}
}
