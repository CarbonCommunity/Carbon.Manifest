using System;
using System.Runtime.InteropServices;
using Epic.OnlineServices;
using Epic.OnlineServices.UI;

[StructLayout (LayoutKind.Sequential, Pack = 8)]
internal struct ReportKeyEventOptionsInternal : IGettable<ReportKeyEventOptions>, ISettable<ReportKeyEventOptions>, IDisposable
{
	private int m_ApiVersion;

	private IntPtr m_PlatformSpecificInputData;

	public IntPtr PlatformSpecificInputData {
		get {
			return m_PlatformSpecificInputData;
		}
		set {
			m_PlatformSpecificInputData = value;
		}
	}

	public void Set (ref ReportKeyEventOptions other)
	{
		m_ApiVersion = 1;
		PlatformSpecificInputData = other.PlatformSpecificInputData;
	}

	public void Set (ref ReportKeyEventOptions? other)
	{
		if (other.HasValue) {
			m_ApiVersion = 1;
			PlatformSpecificInputData = other.Value.PlatformSpecificInputData;
		}
	}

	public void Dispose ()
	{
		Helper.Dispose (ref m_PlatformSpecificInputData);
	}

	public void Get (out ReportKeyEventOptions output)
	{
		output = default(ReportKeyEventOptions);
		output.Set (ref this);
	}
}
