using System;
using System.Runtime.InteropServices;
using Epic.OnlineServices;
using Epic.OnlineServices.P2P;

[StructLayout (LayoutKind.Sequential, Pack = 8)]
internal struct GetNATTypeOptionsInternal : ISettable<GetNATTypeOptions>, IDisposable
{
	private int m_ApiVersion;

	public void Set (ref GetNATTypeOptions other)
	{
		m_ApiVersion = 1;
	}

	public void Set (ref GetNATTypeOptions? other)
	{
		if (other.HasValue) {
			m_ApiVersion = 1;
		}
	}

	public void Dispose ()
	{
	}
}
