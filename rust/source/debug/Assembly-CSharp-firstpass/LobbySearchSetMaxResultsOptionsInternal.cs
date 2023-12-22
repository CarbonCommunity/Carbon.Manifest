using System;
using System.Runtime.InteropServices;
using Epic.OnlineServices;
using Epic.OnlineServices.Lobby;

[StructLayout (LayoutKind.Sequential, Pack = 8)]
internal struct LobbySearchSetMaxResultsOptionsInternal : ISettable<LobbySearchSetMaxResultsOptions>, IDisposable
{
	private int m_ApiVersion;

	private uint m_MaxResults;

	public uint MaxResults {
		set {
			m_MaxResults = value;
		}
	}

	public void Set (ref LobbySearchSetMaxResultsOptions other)
	{
		m_ApiVersion = 1;
		MaxResults = other.MaxResults;
	}

	public void Set (ref LobbySearchSetMaxResultsOptions? other)
	{
		if (other.HasValue) {
			m_ApiVersion = 1;
			MaxResults = other.Value.MaxResults;
		}
	}

	public void Dispose ()
	{
	}
}
