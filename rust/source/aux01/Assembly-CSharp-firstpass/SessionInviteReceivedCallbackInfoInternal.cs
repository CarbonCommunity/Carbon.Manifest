using System;
using System.Runtime.InteropServices;
using Epic.OnlineServices;
using Epic.OnlineServices.Sessions;

[StructLayout (LayoutKind.Sequential, Pack = 8)]
internal struct SessionInviteReceivedCallbackInfoInternal : ICallbackInfoInternal, IGettable<SessionInviteReceivedCallbackInfo>, ISettable<SessionInviteReceivedCallbackInfo>, IDisposable
{
	private IntPtr m_ClientData;

	private IntPtr m_LocalUserId;

	private IntPtr m_TargetUserId;

	private IntPtr m_InviteId;

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

	public Utf8String InviteId {
		get {
			Helper.Get (m_InviteId, out Utf8String to);
			return to;
		}
		set {
			Helper.Set (value, ref m_InviteId);
		}
	}

	public void Set (ref SessionInviteReceivedCallbackInfo other)
	{
		ClientData = other.ClientData;
		LocalUserId = other.LocalUserId;
		TargetUserId = other.TargetUserId;
		InviteId = other.InviteId;
	}

	public void Set (ref SessionInviteReceivedCallbackInfo? other)
	{
		if (other.HasValue) {
			ClientData = other.Value.ClientData;
			LocalUserId = other.Value.LocalUserId;
			TargetUserId = other.Value.TargetUserId;
			InviteId = other.Value.InviteId;
		}
	}

	public void Dispose ()
	{
		Helper.Dispose (ref m_ClientData);
		Helper.Dispose (ref m_LocalUserId);
		Helper.Dispose (ref m_TargetUserId);
		Helper.Dispose (ref m_InviteId);
	}

	public void Get (out SessionInviteReceivedCallbackInfo output)
	{
		output = default(SessionInviteReceivedCallbackInfo);
		output.Set (ref this);
	}
}
