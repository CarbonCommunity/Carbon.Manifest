using System;
using System.Runtime.InteropServices;
using Epic.OnlineServices;
using Epic.OnlineServices.P2P;

[StructLayout (LayoutKind.Sequential, Pack = 8)]
internal struct AcceptConnectionOptionsInternal : ISettable<AcceptConnectionOptions>, IDisposable
{
	private int m_ApiVersion;

	private IntPtr m_LocalUserId;

	private IntPtr m_RemoteUserId;

	private IntPtr m_SocketId;

	public ProductUserId LocalUserId {
		set {
			Helper.Set (value, ref m_LocalUserId);
		}
	}

	public ProductUserId RemoteUserId {
		set {
			Helper.Set (value, ref m_RemoteUserId);
		}
	}

	public SocketId? SocketId {
		set {
			Helper.Set<SocketId, SocketIdInternal> (ref value, ref m_SocketId);
		}
	}

	public void Set (ref AcceptConnectionOptions other)
	{
		m_ApiVersion = 1;
		LocalUserId = other.LocalUserId;
		RemoteUserId = other.RemoteUserId;
		SocketId = other.SocketId;
	}

	public void Set (ref AcceptConnectionOptions? other)
	{
		if (other.HasValue) {
			m_ApiVersion = 1;
			LocalUserId = other.Value.LocalUserId;
			RemoteUserId = other.Value.RemoteUserId;
			SocketId = other.Value.SocketId;
		}
	}

	public void Dispose ()
	{
		Helper.Dispose (ref m_LocalUserId);
		Helper.Dispose (ref m_RemoteUserId);
		Helper.Dispose (ref m_SocketId);
	}
}
