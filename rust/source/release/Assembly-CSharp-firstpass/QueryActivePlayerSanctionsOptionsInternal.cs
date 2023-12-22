using System;
using System.Runtime.InteropServices;
using Epic.OnlineServices;
using Epic.OnlineServices.Sanctions;

[StructLayout (LayoutKind.Sequential, Pack = 8)]
internal struct QueryActivePlayerSanctionsOptionsInternal : ISettable<QueryActivePlayerSanctionsOptions>, IDisposable
{
	private int m_ApiVersion;

	private IntPtr m_TargetUserId;

	private IntPtr m_LocalUserId;

	public ProductUserId TargetUserId {
		set {
			Helper.Set (value, ref m_TargetUserId);
		}
	}

	public ProductUserId LocalUserId {
		set {
			Helper.Set (value, ref m_LocalUserId);
		}
	}

	public void Set (ref QueryActivePlayerSanctionsOptions other)
	{
		m_ApiVersion = 2;
		TargetUserId = other.TargetUserId;
		LocalUserId = other.LocalUserId;
	}

	public void Set (ref QueryActivePlayerSanctionsOptions? other)
	{
		if (other.HasValue) {
			m_ApiVersion = 2;
			TargetUserId = other.Value.TargetUserId;
			LocalUserId = other.Value.LocalUserId;
		}
	}

	public void Dispose ()
	{
		Helper.Dispose (ref m_TargetUserId);
		Helper.Dispose (ref m_LocalUserId);
	}
}
