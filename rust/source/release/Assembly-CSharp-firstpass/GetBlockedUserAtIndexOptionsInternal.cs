using System;
using System.Runtime.InteropServices;
using Epic.OnlineServices;
using Epic.OnlineServices.Friends;

[StructLayout (LayoutKind.Sequential, Pack = 8)]
internal struct GetBlockedUserAtIndexOptionsInternal : ISettable<GetBlockedUserAtIndexOptions>, IDisposable
{
	private int m_ApiVersion;

	private IntPtr m_LocalUserId;

	private int m_Index;

	public EpicAccountId LocalUserId {
		set {
			Helper.Set (value, ref m_LocalUserId);
		}
	}

	public int Index {
		set {
			m_Index = value;
		}
	}

	public void Set (ref GetBlockedUserAtIndexOptions other)
	{
		m_ApiVersion = 1;
		LocalUserId = other.LocalUserId;
		Index = other.Index;
	}

	public void Set (ref GetBlockedUserAtIndexOptions? other)
	{
		if (other.HasValue) {
			m_ApiVersion = 1;
			LocalUserId = other.Value.LocalUserId;
			Index = other.Value.Index;
		}
	}

	public void Dispose ()
	{
		Helper.Dispose (ref m_LocalUserId);
	}
}
