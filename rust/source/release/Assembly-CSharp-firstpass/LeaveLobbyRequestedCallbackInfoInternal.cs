using System;
using System.Runtime.InteropServices;
using Epic.OnlineServices;
using Epic.OnlineServices.Lobby;

[StructLayout (LayoutKind.Sequential, Pack = 8)]
internal struct LeaveLobbyRequestedCallbackInfoInternal : ICallbackInfoInternal, IGettable<LeaveLobbyRequestedCallbackInfo>, ISettable<LeaveLobbyRequestedCallbackInfo>, IDisposable
{
	private IntPtr m_ClientData;

	private IntPtr m_LocalUserId;

	private IntPtr m_LobbyId;

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

	public Utf8String LobbyId {
		get {
			Helper.Get (m_LobbyId, out Utf8String to);
			return to;
		}
		set {
			Helper.Set (value, ref m_LobbyId);
		}
	}

	public void Set (ref LeaveLobbyRequestedCallbackInfo other)
	{
		ClientData = other.ClientData;
		LocalUserId = other.LocalUserId;
		LobbyId = other.LobbyId;
	}

	public void Set (ref LeaveLobbyRequestedCallbackInfo? other)
	{
		if (other.HasValue) {
			ClientData = other.Value.ClientData;
			LocalUserId = other.Value.LocalUserId;
			LobbyId = other.Value.LobbyId;
		}
	}

	public void Dispose ()
	{
		Helper.Dispose (ref m_ClientData);
		Helper.Dispose (ref m_LocalUserId);
		Helper.Dispose (ref m_LobbyId);
	}

	public void Get (out LeaveLobbyRequestedCallbackInfo output)
	{
		output = default(LeaveLobbyRequestedCallbackInfo);
		output.Set (ref this);
	}
}
