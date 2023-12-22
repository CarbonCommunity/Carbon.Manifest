using System;
using System.Runtime.InteropServices;
using Epic.OnlineServices;
using Epic.OnlineServices.Platform;

[StructLayout (LayoutKind.Sequential, Pack = 8)]
internal struct WindowsRTCOptionsInternal : IGettable<WindowsRTCOptions>, ISettable<WindowsRTCOptions>, IDisposable
{
	private int m_ApiVersion;

	private IntPtr m_PlatformSpecificOptions;

	public WindowsRTCOptionsPlatformSpecificOptions? PlatformSpecificOptions {
		get {
			Helper.Get<WindowsRTCOptionsPlatformSpecificOptionsInternal, WindowsRTCOptionsPlatformSpecificOptions> (m_PlatformSpecificOptions, out WindowsRTCOptionsPlatformSpecificOptions? to);
			return to;
		}
		set {
			Helper.Set<WindowsRTCOptionsPlatformSpecificOptions, WindowsRTCOptionsPlatformSpecificOptionsInternal> (ref value, ref m_PlatformSpecificOptions);
		}
	}

	public void Set (ref WindowsRTCOptions other)
	{
		m_ApiVersion = 1;
		PlatformSpecificOptions = other.PlatformSpecificOptions;
	}

	public void Set (ref WindowsRTCOptions? other)
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

	public void Get (out WindowsRTCOptions output)
	{
		output = default(WindowsRTCOptions);
		output.Set (ref this);
	}
}
