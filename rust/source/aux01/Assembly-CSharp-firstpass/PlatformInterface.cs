using System;
using Epic.OnlineServices;
using Epic.OnlineServices.Achievements;
using Epic.OnlineServices.AntiCheatClient;
using Epic.OnlineServices.AntiCheatServer;
using Epic.OnlineServices.Auth;
using Epic.OnlineServices.Connect;
using Epic.OnlineServices.CustomInvites;
using Epic.OnlineServices.Ecom;
using Epic.OnlineServices.Friends;
using Epic.OnlineServices.KWS;
using Epic.OnlineServices.Leaderboards;
using Epic.OnlineServices.Lobby;
using Epic.OnlineServices.Metrics;
using Epic.OnlineServices.Mods;
using Epic.OnlineServices.P2P;
using Epic.OnlineServices.Platform;
using Epic.OnlineServices.PlayerDataStorage;
using Epic.OnlineServices.Presence;
using Epic.OnlineServices.ProgressionSnapshot;
using Epic.OnlineServices.Reports;
using Epic.OnlineServices.RTC;
using Epic.OnlineServices.RTCAdmin;
using Epic.OnlineServices.Sanctions;
using Epic.OnlineServices.Sessions;
using Epic.OnlineServices.Stats;
using Epic.OnlineServices.TitleStorage;
using Epic.OnlineServices.UI;
using Epic.OnlineServices.UserInfo;

public sealed class PlatformInterface : Handle
{
	public const int AndroidInitializeoptionssysteminitializeoptionsApiLatest = 2;

	public const int CountrycodeMaxBufferLen = 5;

	public const int CountrycodeMaxLength = 4;

	public const int GetdesktopcrossplaystatusApiLatest = 1;

	public const int InitializeApiLatest = 4;

	public const int InitializeThreadaffinityApiLatest = 2;

	public const int LocalecodeMaxBufferLen = 10;

	public const int LocalecodeMaxLength = 9;

	public const int OptionsApiLatest = 12;

	public const int RtcoptionsApiLatest = 1;

	public const int WindowsRtcoptionsplatformspecificoptionsApiLatest = 1;

	public static Result Initialize (ref AndroidInitializeOptions options)
	{
		AndroidInitializeOptionsInternal options2 = default(AndroidInitializeOptionsInternal);
		options2.Set (ref options);
		Result result = AndroidBindings.EOS_Initialize (ref options2);
		Helper.Dispose (ref options2);
		return result;
	}

	public PlatformInterface ()
	{
	}

	public PlatformInterface (IntPtr innerHandle)
		: base (innerHandle)
	{
	}

	public Result CheckForLauncherAndRestart ()
	{
		return Bindings.EOS_Platform_CheckForLauncherAndRestart (base.InnerHandle);
	}

	public static PlatformInterface Create (ref Options options)
	{
		OptionsInternal options2 = default(OptionsInternal);
		options2.Set (ref options);
		IntPtr from = Bindings.EOS_Platform_Create (ref options2);
		Helper.Dispose (ref options2);
		Helper.Get (from, out PlatformInterface to);
		return to;
	}

	public AchievementsInterface GetAchievementsInterface ()
	{
		Helper.Get (Bindings.EOS_Platform_GetAchievementsInterface (base.InnerHandle), out AchievementsInterface to);
		return to;
	}

	public Result GetActiveCountryCode (EpicAccountId localUserId, out Utf8String outBuffer)
	{
		IntPtr to = IntPtr.Zero;
		Helper.Set (localUserId, ref to);
		int inOutBufferLength = 5;
		IntPtr value = Helper.AddAllocation (inOutBufferLength);
		Result result = Bindings.EOS_Platform_GetActiveCountryCode (base.InnerHandle, to, value, ref inOutBufferLength);
		Helper.Get (value, out outBuffer);
		Helper.Dispose (ref value);
		return result;
	}

	public Result GetActiveLocaleCode (EpicAccountId localUserId, out Utf8String outBuffer)
	{
		IntPtr to = IntPtr.Zero;
		Helper.Set (localUserId, ref to);
		int inOutBufferLength = 10;
		IntPtr value = Helper.AddAllocation (inOutBufferLength);
		Result result = Bindings.EOS_Platform_GetActiveLocaleCode (base.InnerHandle, to, value, ref inOutBufferLength);
		Helper.Get (value, out outBuffer);
		Helper.Dispose (ref value);
		return result;
	}

	public AntiCheatClientInterface GetAntiCheatClientInterface ()
	{
		Helper.Get (Bindings.EOS_Platform_GetAntiCheatClientInterface (base.InnerHandle), out AntiCheatClientInterface to);
		return to;
	}

	public AntiCheatServerInterface GetAntiCheatServerInterface ()
	{
		Helper.Get (Bindings.EOS_Platform_GetAntiCheatServerInterface (base.InnerHandle), out AntiCheatServerInterface to);
		return to;
	}

	public ApplicationStatus GetApplicationStatus ()
	{
		return Bindings.EOS_Platform_GetApplicationStatus (base.InnerHandle);
	}

	public AuthInterface GetAuthInterface ()
	{
		Helper.Get (Bindings.EOS_Platform_GetAuthInterface (base.InnerHandle), out AuthInterface to);
		return to;
	}

	public ConnectInterface GetConnectInterface ()
	{
		Helper.Get (Bindings.EOS_Platform_GetConnectInterface (base.InnerHandle), out ConnectInterface to);
		return to;
	}

	public CustomInvitesInterface GetCustomInvitesInterface ()
	{
		Helper.Get (Bindings.EOS_Platform_GetCustomInvitesInterface (base.InnerHandle), out CustomInvitesInterface to);
		return to;
	}

	public Result GetDesktopCrossplayStatus (ref GetDesktopCrossplayStatusOptions options, out GetDesktopCrossplayStatusInfo outDesktopCrossplayStatusInfo)
	{
		GetDesktopCrossplayStatusOptionsInternal options2 = default(GetDesktopCrossplayStatusOptionsInternal);
		options2.Set (ref options);
		GetDesktopCrossplayStatusInfoInternal outDesktopCrossplayStatusInfo2 = Helper.GetDefault<GetDesktopCrossplayStatusInfoInternal> ();
		Result result = Bindings.EOS_Platform_GetDesktopCrossplayStatus (base.InnerHandle, ref options2, ref outDesktopCrossplayStatusInfo2);
		Helper.Dispose (ref options2);
		Helper.Get<GetDesktopCrossplayStatusInfoInternal, GetDesktopCrossplayStatusInfo> (ref outDesktopCrossplayStatusInfo2, out outDesktopCrossplayStatusInfo);
		return result;
	}

	public EcomInterface GetEcomInterface ()
	{
		Helper.Get (Bindings.EOS_Platform_GetEcomInterface (base.InnerHandle), out EcomInterface to);
		return to;
	}

	public FriendsInterface GetFriendsInterface ()
	{
		Helper.Get (Bindings.EOS_Platform_GetFriendsInterface (base.InnerHandle), out FriendsInterface to);
		return to;
	}

	public KWSInterface GetKWSInterface ()
	{
		Helper.Get (Bindings.EOS_Platform_GetKWSInterface (base.InnerHandle), out KWSInterface to);
		return to;
	}

	public LeaderboardsInterface GetLeaderboardsInterface ()
	{
		Helper.Get (Bindings.EOS_Platform_GetLeaderboardsInterface (base.InnerHandle), out LeaderboardsInterface to);
		return to;
	}

	public LobbyInterface GetLobbyInterface ()
	{
		Helper.Get (Bindings.EOS_Platform_GetLobbyInterface (base.InnerHandle), out LobbyInterface to);
		return to;
	}

	public MetricsInterface GetMetricsInterface ()
	{
		Helper.Get (Bindings.EOS_Platform_GetMetricsInterface (base.InnerHandle), out MetricsInterface to);
		return to;
	}

	public ModsInterface GetModsInterface ()
	{
		Helper.Get (Bindings.EOS_Platform_GetModsInterface (base.InnerHandle), out ModsInterface to);
		return to;
	}

	public NetworkStatus GetNetworkStatus ()
	{
		return Bindings.EOS_Platform_GetNetworkStatus (base.InnerHandle);
	}

	public Result GetOverrideCountryCode (out Utf8String outBuffer)
	{
		int inOutBufferLength = 5;
		IntPtr value = Helper.AddAllocation (inOutBufferLength);
		Result result = Bindings.EOS_Platform_GetOverrideCountryCode (base.InnerHandle, value, ref inOutBufferLength);
		Helper.Get (value, out outBuffer);
		Helper.Dispose (ref value);
		return result;
	}

	public Result GetOverrideLocaleCode (out Utf8String outBuffer)
	{
		int inOutBufferLength = 10;
		IntPtr value = Helper.AddAllocation (inOutBufferLength);
		Result result = Bindings.EOS_Platform_GetOverrideLocaleCode (base.InnerHandle, value, ref inOutBufferLength);
		Helper.Get (value, out outBuffer);
		Helper.Dispose (ref value);
		return result;
	}

	public P2PInterface GetP2PInterface ()
	{
		Helper.Get (Bindings.EOS_Platform_GetP2PInterface (base.InnerHandle), out P2PInterface to);
		return to;
	}

	public PlayerDataStorageInterface GetPlayerDataStorageInterface ()
	{
		Helper.Get (Bindings.EOS_Platform_GetPlayerDataStorageInterface (base.InnerHandle), out PlayerDataStorageInterface to);
		return to;
	}

	public PresenceInterface GetPresenceInterface ()
	{
		Helper.Get (Bindings.EOS_Platform_GetPresenceInterface (base.InnerHandle), out PresenceInterface to);
		return to;
	}

	public ProgressionSnapshotInterface GetProgressionSnapshotInterface ()
	{
		Helper.Get (Bindings.EOS_Platform_GetProgressionSnapshotInterface (base.InnerHandle), out ProgressionSnapshotInterface to);
		return to;
	}

	public RTCAdminInterface GetRTCAdminInterface ()
	{
		Helper.Get (Bindings.EOS_Platform_GetRTCAdminInterface (base.InnerHandle), out RTCAdminInterface to);
		return to;
	}

	public RTCInterface GetRTCInterface ()
	{
		Helper.Get (Bindings.EOS_Platform_GetRTCInterface (base.InnerHandle), out RTCInterface to);
		return to;
	}

	public ReportsInterface GetReportsInterface ()
	{
		Helper.Get (Bindings.EOS_Platform_GetReportsInterface (base.InnerHandle), out ReportsInterface to);
		return to;
	}

	public SanctionsInterface GetSanctionsInterface ()
	{
		Helper.Get (Bindings.EOS_Platform_GetSanctionsInterface (base.InnerHandle), out SanctionsInterface to);
		return to;
	}

	public SessionsInterface GetSessionsInterface ()
	{
		Helper.Get (Bindings.EOS_Platform_GetSessionsInterface (base.InnerHandle), out SessionsInterface to);
		return to;
	}

	public StatsInterface GetStatsInterface ()
	{
		Helper.Get (Bindings.EOS_Platform_GetStatsInterface (base.InnerHandle), out StatsInterface to);
		return to;
	}

	public TitleStorageInterface GetTitleStorageInterface ()
	{
		Helper.Get (Bindings.EOS_Platform_GetTitleStorageInterface (base.InnerHandle), out TitleStorageInterface to);
		return to;
	}

	public UIInterface GetUIInterface ()
	{
		Helper.Get (Bindings.EOS_Platform_GetUIInterface (base.InnerHandle), out UIInterface to);
		return to;
	}

	public UserInfoInterface GetUserInfoInterface ()
	{
		Helper.Get (Bindings.EOS_Platform_GetUserInfoInterface (base.InnerHandle), out UserInfoInterface to);
		return to;
	}

	public static Result Initialize (ref InitializeOptions options)
	{
		InitializeOptionsInternal options2 = default(InitializeOptionsInternal);
		options2.Set (ref options);
		Result result = Bindings.EOS_Initialize (ref options2);
		Helper.Dispose (ref options2);
		return result;
	}

	public void Release ()
	{
		Bindings.EOS_Platform_Release (base.InnerHandle);
	}

	public Result SetApplicationStatus (ApplicationStatus newStatus)
	{
		return Bindings.EOS_Platform_SetApplicationStatus (base.InnerHandle, newStatus);
	}

	public Result SetNetworkStatus (NetworkStatus newStatus)
	{
		return Bindings.EOS_Platform_SetNetworkStatus (base.InnerHandle, newStatus);
	}

	public Result SetOverrideCountryCode (Utf8String newCountryCode)
	{
		IntPtr to = IntPtr.Zero;
		Helper.Set (newCountryCode, ref to);
		Result result = Bindings.EOS_Platform_SetOverrideCountryCode (base.InnerHandle, to);
		Helper.Dispose (ref to);
		return result;
	}

	public Result SetOverrideLocaleCode (Utf8String newLocaleCode)
	{
		IntPtr to = IntPtr.Zero;
		Helper.Set (newLocaleCode, ref to);
		Result result = Bindings.EOS_Platform_SetOverrideLocaleCode (base.InnerHandle, to);
		Helper.Dispose (ref to);
		return result;
	}

	public static Result Shutdown ()
	{
		return Bindings.EOS_Shutdown ();
	}

	public void Tick ()
	{
		Bindings.EOS_Platform_Tick (base.InnerHandle);
	}

	public static PlatformInterface Create (ref WindowsOptions options)
	{
		WindowsOptionsInternal options2 = default(WindowsOptionsInternal);
		options2.Set (ref options);
		IntPtr from = WindowsBindings.EOS_Platform_Create (ref options2);
		Helper.Dispose (ref options2);
		Helper.Get (from, out PlatformInterface to);
		return to;
	}
}
