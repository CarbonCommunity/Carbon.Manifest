using System;
using System.Runtime.InteropServices;
using Epic.OnlineServices;
using Epic.OnlineServices.Sessions;

[StructLayout (LayoutKind.Sequential, Pack = 8)]
internal struct IsUserInSessionOptionsInternal : ISettable<IsUserInSessionOptions>, IDisposable
{
	private int m_ApiVersion;

	private IntPtr m_SessionName;

	private IntPtr m_TargetUserId;

	public Utf8String SessionName {
		set {
			Helper.Set (value, ref m_SessionName);
		}
	}

	public ProductUserId TargetUserId {
		set {
			Helper.Set (value, ref m_TargetUserId);
		}
	}

	public void Set (ref IsUserInSessionOptions other)
	{
		m_ApiVersion = 1;
		SessionName = other.SessionName;
		TargetUserId = other.TargetUserId;
	}

	public void Set (ref IsUserInSessionOptions? other)
	{
		if (other.HasValue) {
			m_ApiVersion = 1;
			SessionName = other.Value.SessionName;
			TargetUserId = other.Value.TargetUserId;
		}
	}

	public void Dispose ()
	{
		Helper.Dispose (ref m_SessionName);
		Helper.Dispose (ref m_TargetUserId);
	}
}
