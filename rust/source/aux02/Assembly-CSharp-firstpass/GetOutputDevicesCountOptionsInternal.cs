using System;
using System.Runtime.InteropServices;
using Epic.OnlineServices;
using Epic.OnlineServices.RTCAudio;

[StructLayout (LayoutKind.Sequential, Pack = 8)]
internal struct GetOutputDevicesCountOptionsInternal : ISettable<GetOutputDevicesCountOptions>, IDisposable
{
	private int m_ApiVersion;

	public void Set (ref GetOutputDevicesCountOptions other)
	{
		m_ApiVersion = 1;
	}

	public void Set (ref GetOutputDevicesCountOptions? other)
	{
		if (other.HasValue) {
			m_ApiVersion = 1;
		}
	}

	public void Dispose ()
	{
	}
}
