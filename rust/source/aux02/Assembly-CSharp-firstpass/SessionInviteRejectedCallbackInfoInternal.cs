using System;
using System.Runtime.InteropServices;
using Epic.OnlineServices;
using Epic.OnlineServices.Sessions;

[StructLayout (LayoutKind.Sequential, Pack = 8)]
internal struct SessionInviteRejectedCallbackInfoInternal : ICallbackInfoInternal, IGettable<SessionInviteRejectedCallbackInfo>, ISettable<SessionInviteRejectedCallbackInfo>, IDisposable
{
	private IntPtr m_ClientData;

	private IntPtr m_InviteId;

	private IntPtr m_LocalUserId;

	private IntPtr m_TargetUserId;

	private IntPtr m_SessionId;

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

	public Utf8String InviteId {
		get {
			Helper.Get (m_InviteId, out Utf8String to);
			return to;
		}
		set {
			Helper.Set (value, ref m_InviteId);
		}
	}

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

	public Utf8String SessionId {
		get {
			Helper.Get (m_SessionId, out Utf8String to);
			return to;
		}
		set {
			Helper.Set (value, ref m_SessionId);
		}
	}

	public void Set (ref SessionInviteRejectedCallbackInfo other)
	{
		ClientData = other.ClientData;
		InviteId = other.InviteId;
		LocalUserId = other.LocalUserId;
		TargetUserId = other.TargetUserId;
		SessionId = other.SessionId;
	}

	public void Set (ref SessionInviteRejectedCallbackInfo? other)
	{
		if (other.HasValue) {
			ClientData = other.Value.ClientData;
			InviteId = other.Value.InviteId;
			LocalUserId = other.Value.LocalUserId;
			TargetUserId = other.Value.TargetUserId;
			SessionId = other.Value.SessionId;
		}
	}

	public void Dispose ()
	{
		Helper.Dispose (ref m_ClientData);
		Helper.Dispose (ref m_InviteId);
		Helper.Dispose (ref m_LocalUserId);
		Helper.Dispose (ref m_TargetUserId);
		Helper.Dispose (ref m_SessionId);
	}

	public void Get (out SessionInviteRejectedCallbackInfo output)
	{
		output = default(SessionInviteRejectedCallbackInfo);
		output.Set (ref this);
	}
}
