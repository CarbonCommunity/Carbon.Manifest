using System;
using System.Runtime.InteropServices;
using Epic.OnlineServices;
using Epic.OnlineServices.AntiCheatClient;

[StructLayout (LayoutKind.Sequential, Pack = 8)]
internal struct EndSessionOptionsInternal : ISettable<EndSessionOptions>, IDisposable
{
	private int m_ApiVersion;

	public void Set (ref EndSessionOptions other)
	{
		m_ApiVersion = 1;
	}

	public void Set (ref EndSessionOptions? other)
	{
		if (other.HasValue) {
			m_ApiVersion = 1;
		}
	}

	public void Dispose ()
	{
	}
}
