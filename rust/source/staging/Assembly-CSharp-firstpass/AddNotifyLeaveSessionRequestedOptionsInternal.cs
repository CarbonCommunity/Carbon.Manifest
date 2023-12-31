using System;
using System.Runtime.InteropServices;
using Epic.OnlineServices;
using Epic.OnlineServices.Sessions;

[StructLayout (LayoutKind.Sequential, Pack = 8)]
internal struct AddNotifyLeaveSessionRequestedOptionsInternal : ISettable<AddNotifyLeaveSessionRequestedOptions>, IDisposable
{
	private int m_ApiVersion;

	public void Set (ref AddNotifyLeaveSessionRequestedOptions other)
	{
		m_ApiVersion = 1;
	}

	public void Set (ref AddNotifyLeaveSessionRequestedOptions? other)
	{
		if (other.HasValue) {
			m_ApiVersion = 1;
		}
	}

	public void Dispose ()
	{
	}
}
