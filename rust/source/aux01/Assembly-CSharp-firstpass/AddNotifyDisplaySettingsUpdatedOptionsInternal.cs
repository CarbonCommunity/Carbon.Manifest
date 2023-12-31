using System;
using System.Runtime.InteropServices;
using Epic.OnlineServices;
using Epic.OnlineServices.UI;

[StructLayout (LayoutKind.Sequential, Pack = 8)]
internal struct AddNotifyDisplaySettingsUpdatedOptionsInternal : ISettable<AddNotifyDisplaySettingsUpdatedOptions>, IDisposable
{
	private int m_ApiVersion;

	public void Set (ref AddNotifyDisplaySettingsUpdatedOptions other)
	{
		m_ApiVersion = 1;
	}

	public void Set (ref AddNotifyDisplaySettingsUpdatedOptions? other)
	{
		if (other.HasValue) {
			m_ApiVersion = 1;
		}
	}

	public void Dispose ()
	{
	}
}
