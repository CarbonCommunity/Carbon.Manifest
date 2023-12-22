using System;
using System.Runtime.InteropServices;
using Epic.OnlineServices;
using Epic.OnlineServices.Presence;

[StructLayout (LayoutKind.Sequential, Pack = 8)]
internal struct HasPresenceOptionsInternal : ISettable<HasPresenceOptions>, IDisposable
{
	private int m_ApiVersion;

	private IntPtr m_LocalUserId;

	private IntPtr m_TargetUserId;

	public EpicAccountId LocalUserId {
		set {
			Helper.Set (value, ref m_LocalUserId);
		}
	}

	public EpicAccountId TargetUserId {
		set {
			Helper.Set (value, ref m_TargetUserId);
		}
	}

	public void Set (ref HasPresenceOptions other)
	{
		m_ApiVersion = 1;
		LocalUserId = other.LocalUserId;
		TargetUserId = other.TargetUserId;
	}

	public void Set (ref HasPresenceOptions? other)
	{
		if (other.HasValue) {
			m_ApiVersion = 1;
			LocalUserId = other.Value.LocalUserId;
			TargetUserId = other.Value.TargetUserId;
		}
	}

	public void Dispose ()
	{
		Helper.Dispose (ref m_LocalUserId);
		Helper.Dispose (ref m_TargetUserId);
	}
}
