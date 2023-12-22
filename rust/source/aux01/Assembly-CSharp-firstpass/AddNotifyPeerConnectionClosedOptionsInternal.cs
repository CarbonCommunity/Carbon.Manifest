using System;
using System.Runtime.InteropServices;
using Epic.OnlineServices;
using Epic.OnlineServices.P2P;

[StructLayout (LayoutKind.Sequential, Pack = 8)]
internal struct AddNotifyPeerConnectionClosedOptionsInternal : ISettable<AddNotifyPeerConnectionClosedOptions>, IDisposable
{
	private int m_ApiVersion;

	private IntPtr m_LocalUserId;

	private IntPtr m_SocketId;

	public ProductUserId LocalUserId {
		set {
			Helper.Set (value, ref m_LocalUserId);
		}
	}

	public SocketId? SocketId {
		set {
			Helper.Set<SocketId, SocketIdInternal> (ref value, ref m_SocketId);
		}
	}

	public void Set (ref AddNotifyPeerConnectionClosedOptions other)
	{
		m_ApiVersion = 1;
		LocalUserId = other.LocalUserId;
		SocketId = other.SocketId;
	}

	public void Set (ref AddNotifyPeerConnectionClosedOptions? other)
	{
		if (other.HasValue) {
			m_ApiVersion = 1;
			LocalUserId = other.Value.LocalUserId;
			SocketId = other.Value.SocketId;
		}
	}

	public void Dispose ()
	{
		Helper.Dispose (ref m_LocalUserId);
		Helper.Dispose (ref m_SocketId);
	}
}
