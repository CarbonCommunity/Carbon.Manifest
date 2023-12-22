using System;
using System.Runtime.InteropServices;
using Epic.OnlineServices;
using Epic.OnlineServices.Lobby;

[StructLayout (LayoutKind.Sequential, Pack = 8)]
internal struct GetConnectStringOptionsInternal : ISettable<GetConnectStringOptions>, IDisposable
{
	private int m_ApiVersion;

	private IntPtr m_LocalUserId;

	private IntPtr m_LobbyId;

	public ProductUserId LocalUserId {
		set {
			Helper.Set (value, ref m_LocalUserId);
		}
	}

	public Utf8String LobbyId {
		set {
			Helper.Set (value, ref m_LobbyId);
		}
	}

	public void Set (ref GetConnectStringOptions other)
	{
		m_ApiVersion = 1;
		LocalUserId = other.LocalUserId;
		LobbyId = other.LobbyId;
	}

	public void Set (ref GetConnectStringOptions? other)
	{
		if (other.HasValue) {
			m_ApiVersion = 1;
			LocalUserId = other.Value.LocalUserId;
			LobbyId = other.Value.LobbyId;
		}
	}

	public void Dispose ()
	{
		Helper.Dispose (ref m_LocalUserId);
		Helper.Dispose (ref m_LobbyId);
	}
}
