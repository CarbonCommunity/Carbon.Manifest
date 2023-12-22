using System;
using System.Runtime.InteropServices;
using Epic.OnlineServices;
using Epic.OnlineServices.Mods;

[StructLayout (LayoutKind.Sequential, Pack = 8)]
internal struct CopyModInfoOptionsInternal : ISettable<CopyModInfoOptions>, IDisposable
{
	private int m_ApiVersion;

	private IntPtr m_LocalUserId;

	private ModEnumerationType m_Type;

	public EpicAccountId LocalUserId {
		set {
			Helper.Set (value, ref m_LocalUserId);
		}
	}

	public ModEnumerationType Type {
		set {
			m_Type = value;
		}
	}

	public void Set (ref CopyModInfoOptions other)
	{
		m_ApiVersion = 1;
		LocalUserId = other.LocalUserId;
		Type = other.Type;
	}

	public void Set (ref CopyModInfoOptions? other)
	{
		if (other.HasValue) {
			m_ApiVersion = 1;
			LocalUserId = other.Value.LocalUserId;
			Type = other.Value.Type;
		}
	}

	public void Dispose ()
	{
		Helper.Dispose (ref m_LocalUserId);
	}
}
