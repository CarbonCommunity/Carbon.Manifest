using System;
using System.Runtime.InteropServices;
using Epic.OnlineServices;
using Epic.OnlineServices.Lobby;

[StructLayout (LayoutKind.Sequential, Pack = 8)]
internal struct LeaveLobbyCallbackInfoInternal : ICallbackInfoInternal, IGettable<LeaveLobbyCallbackInfo>, ISettable<LeaveLobbyCallbackInfo>, IDisposable
{
	private Result m_ResultCode;

	private IntPtr m_ClientData;

	private IntPtr m_LobbyId;

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

	public Utf8String LobbyId {
		get {
			Helper.Get (m_LobbyId, out Utf8String to);
			return to;
		}
		set {
			Helper.Set (value, ref m_LobbyId);
		}
	}

	public void Set (ref LeaveLobbyCallbackInfo other)
	{
		ResultCode = other.ResultCode;
		ClientData = other.ClientData;
		LobbyId = other.LobbyId;
	}

	public void Set (ref LeaveLobbyCallbackInfo? other)
	{
		if (other.HasValue) {
			ResultCode = other.Value.ResultCode;
			ClientData = other.Value.ClientData;
			LobbyId = other.Value.LobbyId;
		}
	}

	public void Dispose ()
	{
		Helper.Dispose (ref m_ClientData);
		Helper.Dispose (ref m_LobbyId);
	}

	public void Get (out LeaveLobbyCallbackInfo output)
	{
		output = default(LeaveLobbyCallbackInfo);
		output.Set (ref this);
	}
}
