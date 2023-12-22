using System;
using System.Runtime.InteropServices;
using Epic.OnlineServices;
using Epic.OnlineServices.Sessions;

[StructLayout (LayoutKind.Sequential, Pack = 8)]
internal struct CreateSessionModificationOptionsInternal : ISettable<CreateSessionModificationOptions>, IDisposable
{
	private int m_ApiVersion;

	private IntPtr m_SessionName;

	private IntPtr m_BucketId;

	private uint m_MaxPlayers;

	private IntPtr m_LocalUserId;

	private int m_PresenceEnabled;

	private IntPtr m_SessionId;

	private int m_SanctionsEnabled;

	public Utf8String SessionName {
		set {
			Helper.Set (value, ref m_SessionName);
		}
	}

	public Utf8String BucketId {
		set {
			Helper.Set (value, ref m_BucketId);
		}
	}

	public uint MaxPlayers {
		set {
			m_MaxPlayers = value;
		}
	}

	public ProductUserId LocalUserId {
		set {
			Helper.Set (value, ref m_LocalUserId);
		}
	}

	public bool PresenceEnabled {
		set {
			Helper.Set (value, ref m_PresenceEnabled);
		}
	}

	public Utf8String SessionId {
		set {
			Helper.Set (value, ref m_SessionId);
		}
	}

	public bool SanctionsEnabled {
		set {
			Helper.Set (value, ref m_SanctionsEnabled);
		}
	}

	public void Set (ref CreateSessionModificationOptions other)
	{
		m_ApiVersion = 4;
		SessionName = other.SessionName;
		BucketId = other.BucketId;
		MaxPlayers = other.MaxPlayers;
		LocalUserId = other.LocalUserId;
		PresenceEnabled = other.PresenceEnabled;
		SessionId = other.SessionId;
		SanctionsEnabled = other.SanctionsEnabled;
	}

	public void Set (ref CreateSessionModificationOptions? other)
	{
		if (other.HasValue) {
			m_ApiVersion = 4;
			SessionName = other.Value.SessionName;
			BucketId = other.Value.BucketId;
			MaxPlayers = other.Value.MaxPlayers;
			LocalUserId = other.Value.LocalUserId;
			PresenceEnabled = other.Value.PresenceEnabled;
			SessionId = other.Value.SessionId;
			SanctionsEnabled = other.Value.SanctionsEnabled;
		}
	}

	public void Dispose ()
	{
		Helper.Dispose (ref m_SessionName);
		Helper.Dispose (ref m_BucketId);
		Helper.Dispose (ref m_LocalUserId);
		Helper.Dispose (ref m_SessionId);
	}
}
