using System;
using System.Runtime.InteropServices;
using Epic.OnlineServices;
using Epic.OnlineServices.Sessions;

[StructLayout (LayoutKind.Sequential, Pack = 8)]
internal struct SessionSearchGetSearchResultCountOptionsInternal : ISettable<SessionSearchGetSearchResultCountOptions>, IDisposable
{
	private int m_ApiVersion;

	public void Set (ref SessionSearchGetSearchResultCountOptions other)
	{
		m_ApiVersion = 1;
	}

	public void Set (ref SessionSearchGetSearchResultCountOptions? other)
	{
		if (other.HasValue) {
			m_ApiVersion = 1;
		}
	}

	public void Dispose ()
	{
	}
}
