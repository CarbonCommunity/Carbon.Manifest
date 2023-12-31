using System;
using System.Runtime.InteropServices;
using Epic.OnlineServices;
using Epic.OnlineServices.Platform;

[StructLayout (LayoutKind.Sequential, Pack = 8)]
internal struct RTCOptionsInternal : IGettable<RTCOptions>, ISettable<RTCOptions>, IDisposable
{
	private int m_ApiVersion;

	private IntPtr m_PlatformSpecificOptions;

	public IntPtr PlatformSpecificOptions {
		get {
			return m_PlatformSpecificOptions;
		}
		set {
			m_PlatformSpecificOptions = value;
		}
	}

	public void Set (ref RTCOptions other)
	{
		m_ApiVersion = 1;
		PlatformSpecificOptions = other.PlatformSpecificOptions;
	}

	public void Set (ref RTCOptions? other)
	{
		if (other.HasValue) {
			m_ApiVersion = 1;
			PlatformSpecificOptions = other.Value.PlatformSpecificOptions;
		}
	}

	public void Dispose ()
	{
		Helper.Dispose (ref m_PlatformSpecificOptions);
	}

	public void Get (out RTCOptions output)
	{
		output = default(RTCOptions);
		output.Set (ref this);
	}
}
