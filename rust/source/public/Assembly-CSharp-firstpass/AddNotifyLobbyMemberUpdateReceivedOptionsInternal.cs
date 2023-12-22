using System;
using System.Runtime.InteropServices;
using Epic.OnlineServices;
using Epic.OnlineServices.Lobby;

[StructLayout (LayoutKind.Sequential, Pack = 8)]
internal struct AddNotifyLobbyMemberUpdateReceivedOptionsInternal : ISettable<AddNotifyLobbyMemberUpdateReceivedOptions>, IDisposable
{
	private int m_ApiVersion;

	public void Set (ref AddNotifyLobbyMemberUpdateReceivedOptions other)
	{
		m_ApiVersion = 1;
	}

	public void Set (ref AddNotifyLobbyMemberUpdateReceivedOptions? other)
	{
		if (other.HasValue) {
			m_ApiVersion = 1;
		}
	}

	public void Dispose ()
	{
	}
}
