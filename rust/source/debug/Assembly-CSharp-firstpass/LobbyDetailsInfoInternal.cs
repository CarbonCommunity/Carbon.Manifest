using System;
using System.Runtime.InteropServices;
using Epic.OnlineServices;
using Epic.OnlineServices.Lobby;

[StructLayout (LayoutKind.Sequential, Pack = 8)]
internal struct LobbyDetailsInfoInternal : IGettable<LobbyDetailsInfo>, ISettable<LobbyDetailsInfo>, IDisposable
{
	private int m_ApiVersion;

	private IntPtr m_LobbyId;

	private IntPtr m_LobbyOwnerUserId;

	private LobbyPermissionLevel m_PermissionLevel;

	private uint m_AvailableSlots;

	private uint m_MaxMembers;

	private int m_AllowInvites;

	private IntPtr m_BucketId;

	private int m_AllowHostMigration;

	private int m_RTCRoomEnabled;

	private int m_AllowJoinById;

	private int m_RejoinAfterKickRequiresInvite;

	public Utf8String LobbyId {
		get {
			Helper.Get (m_LobbyId, out Utf8String to);
			return to;
		}
		set {
			Helper.Set (value, ref m_LobbyId);
		}
	}

	public ProductUserId LobbyOwnerUserId {
		get {
			Helper.Get (m_LobbyOwnerUserId, out ProductUserId to);
			return to;
		}
		set {
			Helper.Set (value, ref m_LobbyOwnerUserId);
		}
	}

	public LobbyPermissionLevel PermissionLevel {
		get {
			return m_PermissionLevel;
		}
		set {
			m_PermissionLevel = value;
		}
	}

	public uint AvailableSlots {
		get {
			return m_AvailableSlots;
		}
		set {
			m_AvailableSlots = value;
		}
	}

	public uint MaxMembers {
		get {
			return m_MaxMembers;
		}
		set {
			m_MaxMembers = value;
		}
	}

	public bool AllowInvites {
		get {
			Helper.Get (m_AllowInvites, out var to);
			return to;
		}
		set {
			Helper.Set (value, ref m_AllowInvites);
		}
	}

	public Utf8String BucketId {
		get {
			Helper.Get (m_BucketId, out Utf8String to);
			return to;
		}
		set {
			Helper.Set (value, ref m_BucketId);
		}
	}

	public bool AllowHostMigration {
		get {
			Helper.Get (m_AllowHostMigration, out var to);
			return to;
		}
		set {
			Helper.Set (value, ref m_AllowHostMigration);
		}
	}

	public bool RTCRoomEnabled {
		get {
			Helper.Get (m_RTCRoomEnabled, out var to);
			return to;
		}
		set {
			Helper.Set (value, ref m_RTCRoomEnabled);
		}
	}

	public bool AllowJoinById {
		get {
			Helper.Get (m_AllowJoinById, out var to);
			return to;
		}
		set {
			Helper.Set (value, ref m_AllowJoinById);
		}
	}

	public bool RejoinAfterKickRequiresInvite {
		get {
			Helper.Get (m_RejoinAfterKickRequiresInvite, out var to);
			return to;
		}
		set {
			Helper.Set (value, ref m_RejoinAfterKickRequiresInvite);
		}
	}

	public void Set (ref LobbyDetailsInfo other)
	{
		m_ApiVersion = 2;
		LobbyId = other.LobbyId;
		LobbyOwnerUserId = other.LobbyOwnerUserId;
		PermissionLevel = other.PermissionLevel;
		AvailableSlots = other.AvailableSlots;
		MaxMembers = other.MaxMembers;
		AllowInvites = other.AllowInvites;
		BucketId = other.BucketId;
		AllowHostMigration = other.AllowHostMigration;
		RTCRoomEnabled = other.RTCRoomEnabled;
		AllowJoinById = other.AllowJoinById;
		RejoinAfterKickRequiresInvite = other.RejoinAfterKickRequiresInvite;
	}

	public void Set (ref LobbyDetailsInfo? other)
	{
		if (other.HasValue) {
			m_ApiVersion = 2;
			LobbyId = other.Value.LobbyId;
			LobbyOwnerUserId = other.Value.LobbyOwnerUserId;
			PermissionLevel = other.Value.PermissionLevel;
			AvailableSlots = other.Value.AvailableSlots;
			MaxMembers = other.Value.MaxMembers;
			AllowInvites = other.Value.AllowInvites;
			BucketId = other.Value.BucketId;
			AllowHostMigration = other.Value.AllowHostMigration;
			RTCRoomEnabled = other.Value.RTCRoomEnabled;
			AllowJoinById = other.Value.AllowJoinById;
			RejoinAfterKickRequiresInvite = other.Value.RejoinAfterKickRequiresInvite;
		}
	}

	public void Dispose ()
	{
		Helper.Dispose (ref m_LobbyId);
		Helper.Dispose (ref m_LobbyOwnerUserId);
		Helper.Dispose (ref m_BucketId);
	}

	public void Get (out LobbyDetailsInfo output)
	{
		output = default(LobbyDetailsInfo);
		output.Set (ref this);
	}
}
