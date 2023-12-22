using System;
using System.Runtime.InteropServices;
using Epic.OnlineServices;
using Epic.OnlineServices.Auth;

[StructLayout (LayoutKind.Sequential, Pack = 8)]
internal struct CopyUserAuthTokenOptionsInternal : ISettable<CopyUserAuthTokenOptions>, IDisposable
{
	private int m_ApiVersion;

	public void Set (ref CopyUserAuthTokenOptions other)
	{
		m_ApiVersion = 1;
	}

	public void Set (ref CopyUserAuthTokenOptions? other)
	{
		if (other.HasValue) {
			m_ApiVersion = 1;
		}
	}

	public void Dispose ()
	{
	}
}