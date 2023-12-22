using System;
using System.Runtime.InteropServices;
using Epic.OnlineServices;
using Epic.OnlineServices.Platform;

[StructLayout (LayoutKind.Sequential, Pack = 8)]
internal struct GetDesktopCrossplayStatusInfoInternal : IGettable<GetDesktopCrossplayStatusInfo>, ISettable<GetDesktopCrossplayStatusInfo>, IDisposable
{
	private DesktopCrossplayStatus m_Status;

	private int m_ServiceInitResult;

	public DesktopCrossplayStatus Status {
		get {
			return m_Status;
		}
		set {
			m_Status = value;
		}
	}

	public int ServiceInitResult {
		get {
			return m_ServiceInitResult;
		}
		set {
			m_ServiceInitResult = value;
		}
	}

	public void Set (ref GetDesktopCrossplayStatusInfo other)
	{
		Status = other.Status;
		ServiceInitResult = other.ServiceInitResult;
	}

	public void Set (ref GetDesktopCrossplayStatusInfo? other)
	{
		if (other.HasValue) {
			Status = other.Value.Status;
			ServiceInitResult = other.Value.ServiceInitResult;
		}
	}

	public void Dispose ()
	{
	}

	public void Get (out GetDesktopCrossplayStatusInfo output)
	{
		output = default(GetDesktopCrossplayStatusInfo);
		output.Set (ref this);
	}
}
