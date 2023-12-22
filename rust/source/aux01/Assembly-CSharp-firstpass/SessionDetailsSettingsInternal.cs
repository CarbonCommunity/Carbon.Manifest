using System;
using System.Runtime.InteropServices;
using Epic.OnlineServices;
using Epic.OnlineServices.Sessions;

[StructLayout (LayoutKind.Sequential, Pack = 8)]
internal struct SessionDetailsSettingsInternal : IGettable<SessionDetailsSettings>, ISettable<SessionDetailsSettings>, IDisposable
{
	private int m_ApiVersion;

	private IntPtr m_BucketId;

	private uint m_NumPublicConnections;

	private int m_AllowJoinInProgress;

	private OnlineSessionPermissionLevel m_PermissionLevel;

	private int m_InvitesAllowed;

	private int m_SanctionsEnabled;

	public Utf8String BucketId {
		get {
			Helper.Get (m_BucketId, out Utf8String to);
			return to;
		}
		set {
			Helper.Set (value, ref m_BucketId);
		}
	}

	public uint NumPublicConnections {
		get {
			return m_NumPublicConnections;
		}
		set {
			m_NumPublicConnections = value;
		}
	}

	public bool AllowJoinInProgress {
		get {
			Helper.Get (m_AllowJoinInProgress, out var to);
			return to;
		}
		set {
			Helper.Set (value, ref m_AllowJoinInProgress);
		}
	}

	public OnlineSessionPermissionLevel PermissionLevel {
		get {
			return m_PermissionLevel;
		}
		set {
			m_PermissionLevel = value;
		}
	}

	public bool InvitesAllowed {
		get {
			Helper.Get (m_InvitesAllowed, out var to);
			return to;
		}
		set {
			Helper.Set (value, ref m_InvitesAllowed);
		}
	}

	public bool SanctionsEnabled {
		get {
			Helper.Get (m_SanctionsEnabled, out var to);
			return to;
		}
		set {
			Helper.Set (value, ref m_SanctionsEnabled);
		}
	}

	public void Set (ref SessionDetailsSettings other)
	{
		m_ApiVersion = 3;
		BucketId = other.BucketId;
		NumPublicConnections = other.NumPublicConnections;
		AllowJoinInProgress = other.AllowJoinInProgress;
		PermissionLevel = other.PermissionLevel;
		InvitesAllowed = other.InvitesAllowed;
		SanctionsEnabled = other.SanctionsEnabled;
	}

	public void Set (ref SessionDetailsSettings? other)
	{
		if (other.HasValue) {
			m_ApiVersion = 3;
			BucketId = other.Value.BucketId;
			NumPublicConnections = other.Value.NumPublicConnections;
			AllowJoinInProgress = other.Value.AllowJoinInProgress;
			PermissionLevel = other.Value.PermissionLevel;
			InvitesAllowed = other.Value.InvitesAllowed;
			SanctionsEnabled = other.Value.SanctionsEnabled;
		}
	}

	public void Dispose ()
	{
		Helper.Dispose (ref m_BucketId);
	}

	public void Get (out SessionDetailsSettings output)
	{
		output = default(SessionDetailsSettings);
		output.Set (ref this);
	}
}
