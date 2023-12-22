using System;
using System.Runtime.InteropServices;
using Epic.OnlineServices;
using Epic.OnlineServices.Lobby;

[StructLayout (LayoutKind.Sequential, Pack = 8)]
internal struct GetInviteIdByIndexOptionsInternal : ISettable<GetInviteIdByIndexOptions>, IDisposable
{
	private int m_ApiVersion;

	private IntPtr m_LocalUserId;

	private uint m_Index;

	public ProductUserId LocalUserId {
		set {
			Helper.Set (value, ref m_LocalUserId);
		}
	}

	public uint Index {
		set {
			m_Index = value;
		}
	}

	public void Set (ref GetInviteIdByIndexOptions other)
	{
		m_ApiVersion = 1;
		LocalUserId = other.LocalUserId;
		Index = other.Index;
	}

	public void Set (ref GetInviteIdByIndexOptions? other)
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
