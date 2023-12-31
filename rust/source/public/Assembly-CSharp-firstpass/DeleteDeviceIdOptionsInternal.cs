using System;
using System.Runtime.InteropServices;
using Epic.OnlineServices;
using Epic.OnlineServices.Connect;

[StructLayout (LayoutKind.Sequential, Pack = 8)]
internal struct DeleteDeviceIdOptionsInternal : ISettable<DeleteDeviceIdOptions>, IDisposable
{
	private int m_ApiVersion;

	public void Set (ref DeleteDeviceIdOptions other)
	{
		m_ApiVersion = 1;
	}

	public void Set (ref DeleteDeviceIdOptions? other)
	{
		if (other.HasValue) {
			m_ApiVersion = 1;
		}
	}

	public void Dispose ()
	{
	}
}
