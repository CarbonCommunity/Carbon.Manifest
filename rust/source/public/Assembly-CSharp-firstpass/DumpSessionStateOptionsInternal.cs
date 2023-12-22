using System;
using System.Runtime.InteropServices;
using Epic.OnlineServices;
using Epic.OnlineServices.Sessions;

[StructLayout (LayoutKind.Sequential, Pack = 8)]
internal struct DumpSessionStateOptionsInternal : ISettable<DumpSessionStateOptions>, IDisposable
{
	private int m_ApiVersion;

	private IntPtr m_SessionName;

	public Utf8String SessionName {
		set {
			Helper.Set (value, ref m_SessionName);
		}
	}

	public void Set (ref DumpSessionStateOptions other)
	{
		m_ApiVersion = 1;
		SessionName = other.SessionName;
	}

	public void Set (ref DumpSessionStateOptions? other)
	{
		if (other.HasValue) {
			m_ApiVersion = 1;
			SessionName = other.Value.SessionName;
		}
	}

	public void Dispose ()
	{
		Helper.Dispose (ref m_SessionName);
	}
}
