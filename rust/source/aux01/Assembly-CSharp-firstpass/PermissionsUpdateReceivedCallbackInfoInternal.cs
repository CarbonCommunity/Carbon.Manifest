using System;
using System.Runtime.InteropServices;
using Epic.OnlineServices;
using Epic.OnlineServices.KWS;

[StructLayout (LayoutKind.Sequential, Pack = 8)]
internal struct PermissionsUpdateReceivedCallbackInfoInternal : ICallbackInfoInternal, IGettable<PermissionsUpdateReceivedCallbackInfo>, ISettable<PermissionsUpdateReceivedCallbackInfo>, IDisposable
{
	private IntPtr m_ClientData;

	private IntPtr m_LocalUserId;

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

	public void Set (ref PermissionsUpdateReceivedCallbackInfo other)
	{
		ClientData = other.ClientData;
		LocalUserId = other.LocalUserId;
	}

	public void Set (ref PermissionsUpdateReceivedCallbackInfo? other)
	{
		if (other.HasValue) {
			ClientData = other.Value.ClientData;
			LocalUserId = other.Value.LocalUserId;
		}
	}

	public void Dispose ()
	{
		Helper.Dispose (ref m_ClientData);
		Helper.Dispose (ref m_LocalUserId);
	}

	public void Get (out PermissionsUpdateReceivedCallbackInfo output)
	{
		output = default(PermissionsUpdateReceivedCallbackInfo);
		output.Set (ref this);
	}
}
