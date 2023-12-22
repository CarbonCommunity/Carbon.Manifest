using System;
using System.Runtime.InteropServices;
using Epic.OnlineServices;
using Epic.OnlineServices.Lobby;

[StructLayout (LayoutKind.Sequential, Pack = 8)]
internal struct JoinLobbyByIdOptionsInternal : ISettable<JoinLobbyByIdOptions>, IDisposable
{
	private int m_ApiVersion;

	private IntPtr m_LobbyId;

	private IntPtr m_LocalUserId;

	private int m_PresenceEnabled;

	private IntPtr m_LocalRTCOptions;

	public Utf8String LobbyId {
		set {
			Helper.Set (value, ref m_LobbyId);
		}
	}

	public ProductUserId LocalUserId {
		set {
			Helper.Set (value, ref m_LocalUserId);
		}
	}

	public bool PresenceEnabled {
		set {
			Helper.Set (value, ref m_PresenceEnabled);
		}
	}

	public LocalRTCOptions? LocalRTCOptions {
		set {
			Helper.Set<LocalRTCOptions, LocalRTCOptionsInternal> (ref value, ref m_LocalRTCOptions);
		}
	}

	public void Set (ref JoinLobbyByIdOptions other)
	{
		m_ApiVersion = 1;
		LobbyId = other.LobbyId;
		LocalUserId = other.LocalUserId;
		PresenceEnabled = other.PresenceEnabled;
		LocalRTCOptions = other.LocalRTCOptions;
	}

	public void Set (ref JoinLobbyByIdOptions? other)
	{
		if (other.HasValue) {
			m_ApiVersion = 1;
			LobbyId = other.Value.LobbyId;
			LocalUserId = other.Value.LocalUserId;
			PresenceEnabled = other.Value.PresenceEnabled;
			LocalRTCOptions = other.Value.LocalRTCOptions;
		}
	}

	public void Dispose ()
	{
		Helper.Dispose (ref m_LobbyId);
		Helper.Dispose (ref m_LocalUserId);
		Helper.Dispose (ref m_LocalRTCOptions);
	}
}
