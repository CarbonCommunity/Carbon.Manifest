using System;
using System.Runtime.InteropServices;
using Epic.OnlineServices;
using Epic.OnlineServices.Sessions;

[StructLayout (LayoutKind.Sequential, Pack = 8)]
internal struct AddNotifySessionInviteRejectedOptionsInternal : ISettable<AddNotifySessionInviteRejectedOptions>, IDisposable
{
	private int m_ApiVersion;

	public void Set (ref AddNotifySessionInviteRejectedOptions other)
	{
		m_ApiVersion = 1;
	}

	public void Set (ref AddNotifySessionInviteRejectedOptions? other)
	{
		if (other.HasValue) {
			m_ApiVersion = 1;
		}
	}

	public void Dispose ()
	{
	}
}
