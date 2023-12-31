using System;
using Epic.OnlineServices;
using Epic.OnlineServices.Lobby;

public sealed class LobbyInterface : Handle
{
	public const int AddnotifyjoinlobbyacceptedApiLatest = 1;

	public const int AddnotifyleavelobbyrequestedApiLatest = 1;

	public const int AddnotifylobbyinviteacceptedApiLatest = 1;

	public const int AddnotifylobbyinvitereceivedApiLatest = 1;

	public const int AddnotifylobbyinviterejectedApiLatest = 1;

	public const int AddnotifylobbymemberstatusreceivedApiLatest = 1;

	public const int AddnotifylobbymemberupdatereceivedApiLatest = 1;

	public const int AddnotifylobbyupdatereceivedApiLatest = 1;

	public const int AddnotifyrtcroomconnectionchangedApiLatest = 2;

	public const int AddnotifysendlobbynativeinviterequestedApiLatest = 1;

	public const int AttributeApiLatest = 1;

	public const int AttributedataApiLatest = 1;

	public const int CopylobbydetailshandleApiLatest = 1;

	public const int CopylobbydetailshandlebyinviteidApiLatest = 1;

	public const int CopylobbydetailshandlebyuieventidApiLatest = 1;

	public const int CreatelobbyApiLatest = 9;

	public const int CreatelobbysearchApiLatest = 1;

	public const int DestroylobbyApiLatest = 1;

	public const int GetconnectstringApiLatest = 1;

	public const int GetconnectstringBufferSize = 256;

	public const int GetinvitecountApiLatest = 1;

	public const int GetinviteidbyindexApiLatest = 1;

	public const int GetrtcroomnameApiLatest = 1;

	public const int HardmutememberApiLatest = 1;

	public const int InviteidMaxLength = 64;

	public const int IsrtcroomconnectedApiLatest = 1;

	public const int JoinlobbyApiLatest = 4;

	public const int JoinlobbybyidApiLatest = 2;

	public const int KickmemberApiLatest = 1;

	public const int LeavelobbyApiLatest = 1;

	public const int LocalrtcoptionsApiLatest = 1;

	public const int MaxLobbies = 16;

	public const int MaxLobbyMembers = 64;

	public const int MaxLobbyidoverrideLength = 60;

	public const int MaxSearchResults = 200;

	public const int MinLobbyidoverrideLength = 4;

	public const int ParseconnectstringApiLatest = 1;

	public const int ParseconnectstringBufferSize = 256;

	public const int PromotememberApiLatest = 1;

	public const int QueryinvitesApiLatest = 1;

	public const int RejectinviteApiLatest = 1;

	public static readonly Utf8String SearchBucketId = "bucket";

	public static readonly Utf8String SearchMincurrentmembers = "mincurrentmembers";

	public static readonly Utf8String SearchMinslotsavailable = "minslotsavailable";

	public const int SendinviteApiLatest = 1;

	public const int UpdatelobbyApiLatest = 1;

	public const int UpdatelobbymodificationApiLatest = 1;

	public LobbyInterface ()
	{
	}

	public LobbyInterface (IntPtr innerHandle)
		: base (innerHandle)
	{
	}

	public ulong AddNotifyJoinLobbyAccepted (ref AddNotifyJoinLobbyAcceptedOptions options, object clientData, OnJoinLobbyAcceptedCallback notificationFn)
	{
		AddNotifyJoinLobbyAcceptedOptionsInternal options2 = default(AddNotifyJoinLobbyAcceptedOptionsInternal);
		options2.Set (ref options);
		IntPtr clientDataAddress = IntPtr.Zero;
		OnJoinLobbyAcceptedCallbackInternal onJoinLobbyAcceptedCallbackInternal = OnJoinLobbyAcceptedCallbackInternalImplementation;
		Helper.AddCallback (out clientDataAddress, clientData, notificationFn, onJoinLobbyAcceptedCallbackInternal);
		ulong num = Bindings.EOS_Lobby_AddNotifyJoinLobbyAccepted (base.InnerHandle, ref options2, clientDataAddress, onJoinLobbyAcceptedCallbackInternal);
		Helper.Dispose (ref options2);
		Helper.AssignNotificationIdToCallback (clientDataAddress, num);
		return num;
	}

	public ulong AddNotifyLeaveLobbyRequested (ref AddNotifyLeaveLobbyRequestedOptions options, object clientData, OnLeaveLobbyRequestedCallback notificationFn)
	{
		AddNotifyLeaveLobbyRequestedOptionsInternal options2 = default(AddNotifyLeaveLobbyRequestedOptionsInternal);
		options2.Set (ref options);
		IntPtr clientDataAddress = IntPtr.Zero;
		OnLeaveLobbyRequestedCallbackInternal onLeaveLobbyRequestedCallbackInternal = OnLeaveLobbyRequestedCallbackInternalImplementation;
		Helper.AddCallback (out clientDataAddress, clientData, notificationFn, onLeaveLobbyRequestedCallbackInternal);
		ulong num = Bindings.EOS_Lobby_AddNotifyLeaveLobbyRequested (base.InnerHandle, ref options2, clientDataAddress, onLeaveLobbyRequestedCallbackInternal);
		Helper.Dispose (ref options2);
		Helper.AssignNotificationIdToCallback (clientDataAddress, num);
		return num;
	}

	public ulong AddNotifyLobbyInviteAccepted (ref AddNotifyLobbyInviteAcceptedOptions options, object clientData, OnLobbyInviteAcceptedCallback notificationFn)
	{
		AddNotifyLobbyInviteAcceptedOptionsInternal options2 = default(AddNotifyLobbyInviteAcceptedOptionsInternal);
		options2.Set (ref options);
		IntPtr clientDataAddress = IntPtr.Zero;
		OnLobbyInviteAcceptedCallbackInternal onLobbyInviteAcceptedCallbackInternal = OnLobbyInviteAcceptedCallbackInternalImplementation;
		Helper.AddCallback (out clientDataAddress, clientData, notificationFn, onLobbyInviteAcceptedCallbackInternal);
		ulong num = Bindings.EOS_Lobby_AddNotifyLobbyInviteAccepted (base.InnerHandle, ref options2, clientDataAddress, onLobbyInviteAcceptedCallbackInternal);
		Helper.Dispose (ref options2);
		Helper.AssignNotificationIdToCallback (clientDataAddress, num);
		return num;
	}

	public ulong AddNotifyLobbyInviteReceived (ref AddNotifyLobbyInviteReceivedOptions options, object clientData, OnLobbyInviteReceivedCallback notificationFn)
	{
		AddNotifyLobbyInviteReceivedOptionsInternal options2 = default(AddNotifyLobbyInviteReceivedOptionsInternal);
		options2.Set (ref options);
		IntPtr clientDataAddress = IntPtr.Zero;
		OnLobbyInviteReceivedCallbackInternal onLobbyInviteReceivedCallbackInternal = OnLobbyInviteReceivedCallbackInternalImplementation;
		Helper.AddCallback (out clientDataAddress, clientData, notificationFn, onLobbyInviteReceivedCallbackInternal);
		ulong num = Bindings.EOS_Lobby_AddNotifyLobbyInviteReceived (base.InnerHandle, ref options2, clientDataAddress, onLobbyInviteReceivedCallbackInternal);
		Helper.Dispose (ref options2);
		Helper.AssignNotificationIdToCallback (clientDataAddress, num);
		return num;
	}

	public ulong AddNotifyLobbyInviteRejected (ref AddNotifyLobbyInviteRejectedOptions options, object clientData, OnLobbyInviteRejectedCallback notificationFn)
	{
		AddNotifyLobbyInviteRejectedOptionsInternal options2 = default(AddNotifyLobbyInviteRejectedOptionsInternal);
		options2.Set (ref options);
		IntPtr clientDataAddress = IntPtr.Zero;
		OnLobbyInviteRejectedCallbackInternal onLobbyInviteRejectedCallbackInternal = OnLobbyInviteRejectedCallbackInternalImplementation;
		Helper.AddCallback (out clientDataAddress, clientData, notificationFn, onLobbyInviteRejectedCallbackInternal);
		ulong num = Bindings.EOS_Lobby_AddNotifyLobbyInviteRejected (base.InnerHandle, ref options2, clientDataAddress, onLobbyInviteRejectedCallbackInternal);
		Helper.Dispose (ref options2);
		Helper.AssignNotificationIdToCallback (clientDataAddress, num);
		return num;
	}

	public ulong AddNotifyLobbyMemberStatusReceived (ref AddNotifyLobbyMemberStatusReceivedOptions options, object clientData, OnLobbyMemberStatusReceivedCallback notificationFn)
	{
		AddNotifyLobbyMemberStatusReceivedOptionsInternal options2 = default(AddNotifyLobbyMemberStatusReceivedOptionsInternal);
		options2.Set (ref options);
		IntPtr clientDataAddress = IntPtr.Zero;
		OnLobbyMemberStatusReceivedCallbackInternal onLobbyMemberStatusReceivedCallbackInternal = OnLobbyMemberStatusReceivedCallbackInternalImplementation;
		Helper.AddCallback (out clientDataAddress, clientData, notificationFn, onLobbyMemberStatusReceivedCallbackInternal);
		ulong num = Bindings.EOS_Lobby_AddNotifyLobbyMemberStatusReceived (base.InnerHandle, ref options2, clientDataAddress, onLobbyMemberStatusReceivedCallbackInternal);
		Helper.Dispose (ref options2);
		Helper.AssignNotificationIdToCallback (clientDataAddress, num);
		return num;
	}

	public ulong AddNotifyLobbyMemberUpdateReceived (ref AddNotifyLobbyMemberUpdateReceivedOptions options, object clientData, OnLobbyMemberUpdateReceivedCallback notificationFn)
	{
		AddNotifyLobbyMemberUpdateReceivedOptionsInternal options2 = default(AddNotifyLobbyMemberUpdateReceivedOptionsInternal);
		options2.Set (ref options);
		IntPtr clientDataAddress = IntPtr.Zero;
		OnLobbyMemberUpdateReceivedCallbackInternal onLobbyMemberUpdateReceivedCallbackInternal = OnLobbyMemberUpdateReceivedCallbackInternalImplementation;
		Helper.AddCallback (out clientDataAddress, clientData, notificationFn, onLobbyMemberUpdateReceivedCallbackInternal);
		ulong num = Bindings.EOS_Lobby_AddNotifyLobbyMemberUpdateReceived (base.InnerHandle, ref options2, clientDataAddress, onLobbyMemberUpdateReceivedCallbackInternal);
		Helper.Dispose (ref options2);
		Helper.AssignNotificationIdToCallback (clientDataAddress, num);
		return num;
	}

	public ulong AddNotifyLobbyUpdateReceived (ref AddNotifyLobbyUpdateReceivedOptions options, object clientData, OnLobbyUpdateReceivedCallback notificationFn)
	{
		AddNotifyLobbyUpdateReceivedOptionsInternal options2 = default(AddNotifyLobbyUpdateReceivedOptionsInternal);
		options2.Set (ref options);
		IntPtr clientDataAddress = IntPtr.Zero;
		OnLobbyUpdateReceivedCallbackInternal onLobbyUpdateReceivedCallbackInternal = OnLobbyUpdateReceivedCallbackInternalImplementation;
		Helper.AddCallback (out clientDataAddress, clientData, notificationFn, onLobbyUpdateReceivedCallbackInternal);
		ulong num = Bindings.EOS_Lobby_AddNotifyLobbyUpdateReceived (base.InnerHandle, ref options2, clientDataAddress, onLobbyUpdateReceivedCallbackInternal);
		Helper.Dispose (ref options2);
		Helper.AssignNotificationIdToCallback (clientDataAddress, num);
		return num;
	}

	public ulong AddNotifyRTCRoomConnectionChanged (ref AddNotifyRTCRoomConnectionChangedOptions options, object clientData, OnRTCRoomConnectionChangedCallback notificationFn)
	{
		AddNotifyRTCRoomConnectionChangedOptionsInternal options2 = default(AddNotifyRTCRoomConnectionChangedOptionsInternal);
		options2.Set (ref options);
		IntPtr clientDataAddress = IntPtr.Zero;
		OnRTCRoomConnectionChangedCallbackInternal onRTCRoomConnectionChangedCallbackInternal = OnRTCRoomConnectionChangedCallbackInternalImplementation;
		Helper.AddCallback (out clientDataAddress, clientData, notificationFn, onRTCRoomConnectionChangedCallbackInternal);
		ulong num = Bindings.EOS_Lobby_AddNotifyRTCRoomConnectionChanged (base.InnerHandle, ref options2, clientDataAddress, onRTCRoomConnectionChangedCallbackInternal);
		Helper.Dispose (ref options2);
		Helper.AssignNotificationIdToCallback (clientDataAddress, num);
		return num;
	}

	public ulong AddNotifySendLobbyNativeInviteRequested (ref AddNotifySendLobbyNativeInviteRequestedOptions options, object clientData, OnSendLobbyNativeInviteRequestedCallback notificationFn)
	{
		AddNotifySendLobbyNativeInviteRequestedOptionsInternal options2 = default(AddNotifySendLobbyNativeInviteRequestedOptionsInternal);
		options2.Set (ref options);
		IntPtr clientDataAddress = IntPtr.Zero;
		OnSendLobbyNativeInviteRequestedCallbackInternal onSendLobbyNativeInviteRequestedCallbackInternal = OnSendLobbyNativeInviteRequestedCallbackInternalImplementation;
		Helper.AddCallback (out clientDataAddress, clientData, notificationFn, onSendLobbyNativeInviteRequestedCallbackInternal);
		ulong num = Bindings.EOS_Lobby_AddNotifySendLobbyNativeInviteRequested (base.InnerHandle, ref options2, clientDataAddress, onSendLobbyNativeInviteRequestedCallbackInternal);
		Helper.Dispose (ref options2);
		Helper.AssignNotificationIdToCallback (clientDataAddress, num);
		return num;
	}

	public Result CopyLobbyDetailsHandle (ref CopyLobbyDetailsHandleOptions options, out LobbyDetails outLobbyDetailsHandle)
	{
		CopyLobbyDetailsHandleOptionsInternal options2 = default(CopyLobbyDetailsHandleOptionsInternal);
		options2.Set (ref options);
		IntPtr outLobbyDetailsHandle2 = IntPtr.Zero;
		Result result = Bindings.EOS_Lobby_CopyLobbyDetailsHandle (base.InnerHandle, ref options2, ref outLobbyDetailsHandle2);
		Helper.Dispose (ref options2);
		Helper.Get (outLobbyDetailsHandle2, out outLobbyDetailsHandle);
		return result;
	}

	public Result CopyLobbyDetailsHandleByInviteId (ref CopyLobbyDetailsHandleByInviteIdOptions options, out LobbyDetails outLobbyDetailsHandle)
	{
		CopyLobbyDetailsHandleByInviteIdOptionsInternal options2 = default(CopyLobbyDetailsHandleByInviteIdOptionsInternal);
		options2.Set (ref options);
		IntPtr outLobbyDetailsHandle2 = IntPtr.Zero;
		Result result = Bindings.EOS_Lobby_CopyLobbyDetailsHandleByInviteId (base.InnerHandle, ref options2, ref outLobbyDetailsHandle2);
		Helper.Dispose (ref options2);
		Helper.Get (outLobbyDetailsHandle2, out outLobbyDetailsHandle);
		return result;
	}

	public Result CopyLobbyDetailsHandleByUiEventId (ref CopyLobbyDetailsHandleByUiEventIdOptions options, out LobbyDetails outLobbyDetailsHandle)
	{
		CopyLobbyDetailsHandleByUiEventIdOptionsInternal options2 = default(CopyLobbyDetailsHandleByUiEventIdOptionsInternal);
		options2.Set (ref options);
		IntPtr outLobbyDetailsHandle2 = IntPtr.Zero;
		Result result = Bindings.EOS_Lobby_CopyLobbyDetailsHandleByUiEventId (base.InnerHandle, ref options2, ref outLobbyDetailsHandle2);
		Helper.Dispose (ref options2);
		Helper.Get (outLobbyDetailsHandle2, out outLobbyDetailsHandle);
		return result;
	}

	public void CreateLobby (ref CreateLobbyOptions options, object clientData, OnCreateLobbyCallback completionDelegate)
	{
		CreateLobbyOptionsInternal options2 = default(CreateLobbyOptionsInternal);
		options2.Set (ref options);
		IntPtr clientDataAddress = IntPtr.Zero;
		OnCreateLobbyCallbackInternal onCreateLobbyCallbackInternal = OnCreateLobbyCallbackInternalImplementation;
		Helper.AddCallback (out clientDataAddress, clientData, completionDelegate, onCreateLobbyCallbackInternal);
		Bindings.EOS_Lobby_CreateLobby (base.InnerHandle, ref options2, clientDataAddress, onCreateLobbyCallbackInternal);
		Helper.Dispose (ref options2);
	}

	public Result CreateLobbySearch (ref CreateLobbySearchOptions options, out LobbySearch outLobbySearchHandle)
	{
		CreateLobbySearchOptionsInternal options2 = default(CreateLobbySearchOptionsInternal);
		options2.Set (ref options);
		IntPtr outLobbySearchHandle2 = IntPtr.Zero;
		Result result = Bindings.EOS_Lobby_CreateLobbySearch (base.InnerHandle, ref options2, ref outLobbySearchHandle2);
		Helper.Dispose (ref options2);
		Helper.Get (outLobbySearchHandle2, out outLobbySearchHandle);
		return result;
	}

	public void DestroyLobby (ref DestroyLobbyOptions options, object clientData, OnDestroyLobbyCallback completionDelegate)
	{
		DestroyLobbyOptionsInternal options2 = default(DestroyLobbyOptionsInternal);
		options2.Set (ref options);
		IntPtr clientDataAddress = IntPtr.Zero;
		OnDestroyLobbyCallbackInternal onDestroyLobbyCallbackInternal = OnDestroyLobbyCallbackInternalImplementation;
		Helper.AddCallback (out clientDataAddress, clientData, completionDelegate, onDestroyLobbyCallbackInternal);
		Bindings.EOS_Lobby_DestroyLobby (base.InnerHandle, ref options2, clientDataAddress, onDestroyLobbyCallbackInternal);
		Helper.Dispose (ref options2);
	}

	public Result GetConnectString (ref GetConnectStringOptions options, out Utf8String outBuffer)
	{
		GetConnectStringOptionsInternal options2 = default(GetConnectStringOptionsInternal);
		options2.Set (ref options);
		uint inOutBufferLength = 256u;
		IntPtr value = Helper.AddAllocation (inOutBufferLength);
		Result result = Bindings.EOS_Lobby_GetConnectString (base.InnerHandle, ref options2, value, ref inOutBufferLength);
		Helper.Dispose (ref options2);
		Helper.Get (value, out outBuffer);
		Helper.Dispose (ref value);
		return result;
	}

	public uint GetInviteCount (ref GetInviteCountOptions options)
	{
		GetInviteCountOptionsInternal options2 = default(GetInviteCountOptionsInternal);
		options2.Set (ref options);
		uint result = Bindings.EOS_Lobby_GetInviteCount (base.InnerHandle, ref options2);
		Helper.Dispose (ref options2);
		return result;
	}

	public Result GetInviteIdByIndex (ref GetInviteIdByIndexOptions options, out Utf8String outBuffer)
	{
		GetInviteIdByIndexOptionsInternal options2 = default(GetInviteIdByIndexOptionsInternal);
		options2.Set (ref options);
		int inOutBufferLength = 65;
		IntPtr value = Helper.AddAllocation (inOutBufferLength);
		Result result = Bindings.EOS_Lobby_GetInviteIdByIndex (base.InnerHandle, ref options2, value, ref inOutBufferLength);
		Helper.Dispose (ref options2);
		Helper.Get (value, out outBuffer);
		Helper.Dispose (ref value);
		return result;
	}

	public Result GetRTCRoomName (ref GetRTCRoomNameOptions options, out Utf8String outBuffer)
	{
		GetRTCRoomNameOptionsInternal options2 = default(GetRTCRoomNameOptionsInternal);
		options2.Set (ref options);
		uint inOutBufferLength = 256u;
		IntPtr value = Helper.AddAllocation (inOutBufferLength);
		Result result = Bindings.EOS_Lobby_GetRTCRoomName (base.InnerHandle, ref options2, value, ref inOutBufferLength);
		Helper.Dispose (ref options2);
		Helper.Get (value, out outBuffer);
		Helper.Dispose (ref value);
		return result;
	}

	public void HardMuteMember (ref HardMuteMemberOptions options, object clientData, OnHardMuteMemberCallback completionDelegate)
	{
		HardMuteMemberOptionsInternal options2 = default(HardMuteMemberOptionsInternal);
		options2.Set (ref options);
		IntPtr clientDataAddress = IntPtr.Zero;
		OnHardMuteMemberCallbackInternal onHardMuteMemberCallbackInternal = OnHardMuteMemberCallbackInternalImplementation;
		Helper.AddCallback (out clientDataAddress, clientData, completionDelegate, onHardMuteMemberCallbackInternal);
		Bindings.EOS_Lobby_HardMuteMember (base.InnerHandle, ref options2, clientDataAddress, onHardMuteMemberCallbackInternal);
		Helper.Dispose (ref options2);
	}

	public Result IsRTCRoomConnected (ref IsRTCRoomConnectedOptions options, out bool bOutIsConnected)
	{
		IsRTCRoomConnectedOptionsInternal options2 = default(IsRTCRoomConnectedOptionsInternal);
		options2.Set (ref options);
		int bOutIsConnected2 = 0;
		Result result = Bindings.EOS_Lobby_IsRTCRoomConnected (base.InnerHandle, ref options2, ref bOutIsConnected2);
		Helper.Dispose (ref options2);
		Helper.Get (bOutIsConnected2, out bOutIsConnected);
		return result;
	}

	public void JoinLobby (ref JoinLobbyOptions options, object clientData, OnJoinLobbyCallback completionDelegate)
	{
		JoinLobbyOptionsInternal options2 = default(JoinLobbyOptionsInternal);
		options2.Set (ref options);
		IntPtr clientDataAddress = IntPtr.Zero;
		OnJoinLobbyCallbackInternal onJoinLobbyCallbackInternal = OnJoinLobbyCallbackInternalImplementation;
		Helper.AddCallback (out clientDataAddress, clientData, completionDelegate, onJoinLobbyCallbackInternal);
		Bindings.EOS_Lobby_JoinLobby (base.InnerHandle, ref options2, clientDataAddress, onJoinLobbyCallbackInternal);
		Helper.Dispose (ref options2);
	}

	public void JoinLobbyById (ref JoinLobbyByIdOptions options, object clientData, OnJoinLobbyByIdCallback completionDelegate)
	{
		JoinLobbyByIdOptionsInternal options2 = default(JoinLobbyByIdOptionsInternal);
		options2.Set (ref options);
		IntPtr clientDataAddress = IntPtr.Zero;
		OnJoinLobbyByIdCallbackInternal onJoinLobbyByIdCallbackInternal = OnJoinLobbyByIdCallbackInternalImplementation;
		Helper.AddCallback (out clientDataAddress, clientData, completionDelegate, onJoinLobbyByIdCallbackInternal);
		Bindings.EOS_Lobby_JoinLobbyById (base.InnerHandle, ref options2, clientDataAddress, onJoinLobbyByIdCallbackInternal);
		Helper.Dispose (ref options2);
	}

	public void KickMember (ref KickMemberOptions options, object clientData, OnKickMemberCallback completionDelegate)
	{
		KickMemberOptionsInternal options2 = default(KickMemberOptionsInternal);
		options2.Set (ref options);
		IntPtr clientDataAddress = IntPtr.Zero;
		OnKickMemberCallbackInternal onKickMemberCallbackInternal = OnKickMemberCallbackInternalImplementation;
		Helper.AddCallback (out clientDataAddress, clientData, completionDelegate, onKickMemberCallbackInternal);
		Bindings.EOS_Lobby_KickMember (base.InnerHandle, ref options2, clientDataAddress, onKickMemberCallbackInternal);
		Helper.Dispose (ref options2);
	}

	public void LeaveLobby (ref LeaveLobbyOptions options, object clientData, OnLeaveLobbyCallback completionDelegate)
	{
		LeaveLobbyOptionsInternal options2 = default(LeaveLobbyOptionsInternal);
		options2.Set (ref options);
		IntPtr clientDataAddress = IntPtr.Zero;
		OnLeaveLobbyCallbackInternal onLeaveLobbyCallbackInternal = OnLeaveLobbyCallbackInternalImplementation;
		Helper.AddCallback (out clientDataAddress, clientData, completionDelegate, onLeaveLobbyCallbackInternal);
		Bindings.EOS_Lobby_LeaveLobby (base.InnerHandle, ref options2, clientDataAddress, onLeaveLobbyCallbackInternal);
		Helper.Dispose (ref options2);
	}

	public Result ParseConnectString (ref ParseConnectStringOptions options, out Utf8String outBuffer)
	{
		ParseConnectStringOptionsInternal options2 = default(ParseConnectStringOptionsInternal);
		options2.Set (ref options);
		uint inOutBufferLength = 256u;
		IntPtr value = Helper.AddAllocation (inOutBufferLength);
		Result result = Bindings.EOS_Lobby_ParseConnectString (base.InnerHandle, ref options2, value, ref inOutBufferLength);
		Helper.Dispose (ref options2);
		Helper.Get (value, out outBuffer);
		Helper.Dispose (ref value);
		return result;
	}

	public void PromoteMember (ref PromoteMemberOptions options, object clientData, OnPromoteMemberCallback completionDelegate)
	{
		PromoteMemberOptionsInternal options2 = default(PromoteMemberOptionsInternal);
		options2.Set (ref options);
		IntPtr clientDataAddress = IntPtr.Zero;
		OnPromoteMemberCallbackInternal onPromoteMemberCallbackInternal = OnPromoteMemberCallbackInternalImplementation;
		Helper.AddCallback (out clientDataAddress, clientData, completionDelegate, onPromoteMemberCallbackInternal);
		Bindings.EOS_Lobby_PromoteMember (base.InnerHandle, ref options2, clientDataAddress, onPromoteMemberCallbackInternal);
		Helper.Dispose (ref options2);
	}

	public void QueryInvites (ref QueryInvitesOptions options, object clientData, OnQueryInvitesCallback completionDelegate)
	{
		QueryInvitesOptionsInternal options2 = default(QueryInvitesOptionsInternal);
		options2.Set (ref options);
		IntPtr clientDataAddress = IntPtr.Zero;
		OnQueryInvitesCallbackInternal onQueryInvitesCallbackInternal = OnQueryInvitesCallbackInternalImplementation;
		Helper.AddCallback (out clientDataAddress, clientData, completionDelegate, onQueryInvitesCallbackInternal);
		Bindings.EOS_Lobby_QueryInvites (base.InnerHandle, ref options2, clientDataAddress, onQueryInvitesCallbackInternal);
		Helper.Dispose (ref options2);
	}

	public void RejectInvite (ref RejectInviteOptions options, object clientData, OnRejectInviteCallback completionDelegate)
	{
		RejectInviteOptionsInternal options2 = default(RejectInviteOptionsInternal);
		options2.Set (ref options);
		IntPtr clientDataAddress = IntPtr.Zero;
		OnRejectInviteCallbackInternal onRejectInviteCallbackInternal = OnRejectInviteCallbackInternalImplementation;
		Helper.AddCallback (out clientDataAddress, clientData, completionDelegate, onRejectInviteCallbackInternal);
		Bindings.EOS_Lobby_RejectInvite (base.InnerHandle, ref options2, clientDataAddress, onRejectInviteCallbackInternal);
		Helper.Dispose (ref options2);
	}

	public void RemoveNotifyJoinLobbyAccepted (ulong inId)
	{
		Bindings.EOS_Lobby_RemoveNotifyJoinLobbyAccepted (base.InnerHandle, inId);
		Helper.RemoveCallbackByNotificationId (inId);
	}

	public void RemoveNotifyLeaveLobbyRequested (ulong inId)
	{
		Bindings.EOS_Lobby_RemoveNotifyLeaveLobbyRequested (base.InnerHandle, inId);
		Helper.RemoveCallbackByNotificationId (inId);
	}

	public void RemoveNotifyLobbyInviteAccepted (ulong inId)
	{
		Bindings.EOS_Lobby_RemoveNotifyLobbyInviteAccepted (base.InnerHandle, inId);
		Helper.RemoveCallbackByNotificationId (inId);
	}

	public void RemoveNotifyLobbyInviteReceived (ulong inId)
	{
		Bindings.EOS_Lobby_RemoveNotifyLobbyInviteReceived (base.InnerHandle, inId);
		Helper.RemoveCallbackByNotificationId (inId);
	}

	public void RemoveNotifyLobbyInviteRejected (ulong inId)
	{
		Bindings.EOS_Lobby_RemoveNotifyLobbyInviteRejected (base.InnerHandle, inId);
		Helper.RemoveCallbackByNotificationId (inId);
	}

	public void RemoveNotifyLobbyMemberStatusReceived (ulong inId)
	{
		Bindings.EOS_Lobby_RemoveNotifyLobbyMemberStatusReceived (base.InnerHandle, inId);
		Helper.RemoveCallbackByNotificationId (inId);
	}

	public void RemoveNotifyLobbyMemberUpdateReceived (ulong inId)
	{
		Bindings.EOS_Lobby_RemoveNotifyLobbyMemberUpdateReceived (base.InnerHandle, inId);
		Helper.RemoveCallbackByNotificationId (inId);
	}

	public void RemoveNotifyLobbyUpdateReceived (ulong inId)
	{
		Bindings.EOS_Lobby_RemoveNotifyLobbyUpdateReceived (base.InnerHandle, inId);
		Helper.RemoveCallbackByNotificationId (inId);
	}

	public void RemoveNotifyRTCRoomConnectionChanged (ulong inId)
	{
		Bindings.EOS_Lobby_RemoveNotifyRTCRoomConnectionChanged (base.InnerHandle, inId);
		Helper.RemoveCallbackByNotificationId (inId);
	}

	public void RemoveNotifySendLobbyNativeInviteRequested (ulong inId)
	{
		Bindings.EOS_Lobby_RemoveNotifySendLobbyNativeInviteRequested (base.InnerHandle, inId);
		Helper.RemoveCallbackByNotificationId (inId);
	}

	public void SendInvite (ref SendInviteOptions options, object clientData, OnSendInviteCallback completionDelegate)
	{
		SendInviteOptionsInternal options2 = default(SendInviteOptionsInternal);
		options2.Set (ref options);
		IntPtr clientDataAddress = IntPtr.Zero;
		OnSendInviteCallbackInternal onSendInviteCallbackInternal = OnSendInviteCallbackInternalImplementation;
		Helper.AddCallback (out clientDataAddress, clientData, completionDelegate, onSendInviteCallbackInternal);
		Bindings.EOS_Lobby_SendInvite (base.InnerHandle, ref options2, clientDataAddress, onSendInviteCallbackInternal);
		Helper.Dispose (ref options2);
	}

	public void UpdateLobby (ref UpdateLobbyOptions options, object clientData, OnUpdateLobbyCallback completionDelegate)
	{
		UpdateLobbyOptionsInternal options2 = default(UpdateLobbyOptionsInternal);
		options2.Set (ref options);
		IntPtr clientDataAddress = IntPtr.Zero;
		OnUpdateLobbyCallbackInternal onUpdateLobbyCallbackInternal = OnUpdateLobbyCallbackInternalImplementation;
		Helper.AddCallback (out clientDataAddress, clientData, completionDelegate, onUpdateLobbyCallbackInternal);
		Bindings.EOS_Lobby_UpdateLobby (base.InnerHandle, ref options2, clientDataAddress, onUpdateLobbyCallbackInternal);
		Helper.Dispose (ref options2);
	}

	public Result UpdateLobbyModification (ref UpdateLobbyModificationOptions options, out LobbyModification outLobbyModificationHandle)
	{
		UpdateLobbyModificationOptionsInternal options2 = default(UpdateLobbyModificationOptionsInternal);
		options2.Set (ref options);
		IntPtr outLobbyModificationHandle2 = IntPtr.Zero;
		Result result = Bindings.EOS_Lobby_UpdateLobbyModification (base.InnerHandle, ref options2, ref outLobbyModificationHandle2);
		Helper.Dispose (ref options2);
		Helper.Get (outLobbyModificationHandle2, out outLobbyModificationHandle);
		return result;
	}

	[MonoPInvokeCallback (typeof(OnCreateLobbyCallbackInternal))]
	internal static void OnCreateLobbyCallbackInternalImplementation (ref CreateLobbyCallbackInfoInternal data)
	{
		if (Helper.TryGetAndRemoveCallback<CreateLobbyCallbackInfoInternal, OnCreateLobbyCallback, CreateLobbyCallbackInfo> (ref data, out var callback, out var callbackInfo)) {
			callback (ref callbackInfo);
		}
	}

	[MonoPInvokeCallback (typeof(OnDestroyLobbyCallbackInternal))]
	internal static void OnDestroyLobbyCallbackInternalImplementation (ref DestroyLobbyCallbackInfoInternal data)
	{
		if (Helper.TryGetAndRemoveCallback<DestroyLobbyCallbackInfoInternal, OnDestroyLobbyCallback, DestroyLobbyCallbackInfo> (ref data, out var callback, out var callbackInfo)) {
			callback (ref callbackInfo);
		}
	}

	[MonoPInvokeCallback (typeof(OnHardMuteMemberCallbackInternal))]
	internal static void OnHardMuteMemberCallbackInternalImplementation (ref HardMuteMemberCallbackInfoInternal data)
	{
		if (Helper.TryGetAndRemoveCallback<HardMuteMemberCallbackInfoInternal, OnHardMuteMemberCallback, HardMuteMemberCallbackInfo> (ref data, out var callback, out var callbackInfo)) {
			callback (ref callbackInfo);
		}
	}

	[MonoPInvokeCallback (typeof(OnJoinLobbyAcceptedCallbackInternal))]
	internal static void OnJoinLobbyAcceptedCallbackInternalImplementation (ref JoinLobbyAcceptedCallbackInfoInternal data)
	{
		if (Helper.TryGetCallback<JoinLobbyAcceptedCallbackInfoInternal, OnJoinLobbyAcceptedCallback, JoinLobbyAcceptedCallbackInfo> (ref data, out var callback, out var callbackInfo)) {
			callback (ref callbackInfo);
		}
	}

	[MonoPInvokeCallback (typeof(OnJoinLobbyByIdCallbackInternal))]
	internal static void OnJoinLobbyByIdCallbackInternalImplementation (ref JoinLobbyByIdCallbackInfoInternal data)
	{
		if (Helper.TryGetAndRemoveCallback<JoinLobbyByIdCallbackInfoInternal, OnJoinLobbyByIdCallback, JoinLobbyByIdCallbackInfo> (ref data, out var callback, out var callbackInfo)) {
			callback (ref callbackInfo);
		}
	}

	[MonoPInvokeCallback (typeof(OnJoinLobbyCallbackInternal))]
	internal static void OnJoinLobbyCallbackInternalImplementation (ref JoinLobbyCallbackInfoInternal data)
	{
		if (Helper.TryGetAndRemoveCallback<JoinLobbyCallbackInfoInternal, OnJoinLobbyCallback, JoinLobbyCallbackInfo> (ref data, out var callback, out var callbackInfo)) {
			callback (ref callbackInfo);
		}
	}

	[MonoPInvokeCallback (typeof(OnKickMemberCallbackInternal))]
	internal static void OnKickMemberCallbackInternalImplementation (ref KickMemberCallbackInfoInternal data)
	{
		if (Helper.TryGetAndRemoveCallback<KickMemberCallbackInfoInternal, OnKickMemberCallback, KickMemberCallbackInfo> (ref data, out var callback, out var callbackInfo)) {
			callback (ref callbackInfo);
		}
	}

	[MonoPInvokeCallback (typeof(OnLeaveLobbyCallbackInternal))]
	internal static void OnLeaveLobbyCallbackInternalImplementation (ref LeaveLobbyCallbackInfoInternal data)
	{
		if (Helper.TryGetAndRemoveCallback<LeaveLobbyCallbackInfoInternal, OnLeaveLobbyCallback, LeaveLobbyCallbackInfo> (ref data, out var callback, out var callbackInfo)) {
			callback (ref callbackInfo);
		}
	}

	[MonoPInvokeCallback (typeof(OnLeaveLobbyRequestedCallbackInternal))]
	internal static void OnLeaveLobbyRequestedCallbackInternalImplementation (ref LeaveLobbyRequestedCallbackInfoInternal data)
	{
		if (Helper.TryGetCallback<LeaveLobbyRequestedCallbackInfoInternal, OnLeaveLobbyRequestedCallback, LeaveLobbyRequestedCallbackInfo> (ref data, out var callback, out var callbackInfo)) {
			callback (ref callbackInfo);
		}
	}

	[MonoPInvokeCallback (typeof(OnLobbyInviteAcceptedCallbackInternal))]
	internal static void OnLobbyInviteAcceptedCallbackInternalImplementation (ref LobbyInviteAcceptedCallbackInfoInternal data)
	{
		if (Helper.TryGetCallback<LobbyInviteAcceptedCallbackInfoInternal, OnLobbyInviteAcceptedCallback, LobbyInviteAcceptedCallbackInfo> (ref data, out var callback, out var callbackInfo)) {
			callback (ref callbackInfo);
		}
	}

	[MonoPInvokeCallback (typeof(OnLobbyInviteReceivedCallbackInternal))]
	internal static void OnLobbyInviteReceivedCallbackInternalImplementation (ref LobbyInviteReceivedCallbackInfoInternal data)
	{
		if (Helper.TryGetCallback<LobbyInviteReceivedCallbackInfoInternal, OnLobbyInviteReceivedCallback, LobbyInviteReceivedCallbackInfo> (ref data, out var callback, out var callbackInfo)) {
			callback (ref callbackInfo);
		}
	}

	[MonoPInvokeCallback (typeof(OnLobbyInviteRejectedCallbackInternal))]
	internal static void OnLobbyInviteRejectedCallbackInternalImplementation (ref LobbyInviteRejectedCallbackInfoInternal data)
	{
		if (Helper.TryGetCallback<LobbyInviteRejectedCallbackInfoInternal, OnLobbyInviteRejectedCallback, LobbyInviteRejectedCallbackInfo> (ref data, out var callback, out var callbackInfo)) {
			callback (ref callbackInfo);
		}
	}

	[MonoPInvokeCallback (typeof(OnLobbyMemberStatusReceivedCallbackInternal))]
	internal static void OnLobbyMemberStatusReceivedCallbackInternalImplementation (ref LobbyMemberStatusReceivedCallbackInfoInternal data)
	{
		if (Helper.TryGetCallback<LobbyMemberStatusReceivedCallbackInfoInternal, OnLobbyMemberStatusReceivedCallback, LobbyMemberStatusReceivedCallbackInfo> (ref data, out var callback, out var callbackInfo)) {
			callback (ref callbackInfo);
		}
	}

	[MonoPInvokeCallback (typeof(OnLobbyMemberUpdateReceivedCallbackInternal))]
	internal static void OnLobbyMemberUpdateReceivedCallbackInternalImplementation (ref LobbyMemberUpdateReceivedCallbackInfoInternal data)
	{
		if (Helper.TryGetCallback<LobbyMemberUpdateReceivedCallbackInfoInternal, OnLobbyMemberUpdateReceivedCallback, LobbyMemberUpdateReceivedCallbackInfo> (ref data, out var callback, out var callbackInfo)) {
			callback (ref callbackInfo);
		}
	}

	[MonoPInvokeCallback (typeof(OnLobbyUpdateReceivedCallbackInternal))]
	internal static void OnLobbyUpdateReceivedCallbackInternalImplementation (ref LobbyUpdateReceivedCallbackInfoInternal data)
	{
		if (Helper.TryGetCallback<LobbyUpdateReceivedCallbackInfoInternal, OnLobbyUpdateReceivedCallback, LobbyUpdateReceivedCallbackInfo> (ref data, out var callback, out var callbackInfo)) {
			callback (ref callbackInfo);
		}
	}

	[MonoPInvokeCallback (typeof(OnPromoteMemberCallbackInternal))]
	internal static void OnPromoteMemberCallbackInternalImplementation (ref PromoteMemberCallbackInfoInternal data)
	{
		if (Helper.TryGetAndRemoveCallback<PromoteMemberCallbackInfoInternal, OnPromoteMemberCallback, PromoteMemberCallbackInfo> (ref data, out var callback, out var callbackInfo)) {
			callback (ref callbackInfo);
		}
	}

	[MonoPInvokeCallback (typeof(OnQueryInvitesCallbackInternal))]
	internal static void OnQueryInvitesCallbackInternalImplementation (ref QueryInvitesCallbackInfoInternal data)
	{
		if (Helper.TryGetAndRemoveCallback<QueryInvitesCallbackInfoInternal, OnQueryInvitesCallback, QueryInvitesCallbackInfo> (ref data, out var callback, out var callbackInfo)) {
			callback (ref callbackInfo);
		}
	}

	[MonoPInvokeCallback (typeof(OnRTCRoomConnectionChangedCallbackInternal))]
	internal static void OnRTCRoomConnectionChangedCallbackInternalImplementation (ref RTCRoomConnectionChangedCallbackInfoInternal data)
	{
		if (Helper.TryGetCallback<RTCRoomConnectionChangedCallbackInfoInternal, OnRTCRoomConnectionChangedCallback, RTCRoomConnectionChangedCallbackInfo> (ref data, out var callback, out var callbackInfo)) {
			callback (ref callbackInfo);
		}
	}

	[MonoPInvokeCallback (typeof(OnRejectInviteCallbackInternal))]
	internal static void OnRejectInviteCallbackInternalImplementation (ref RejectInviteCallbackInfoInternal data)
	{
		if (Helper.TryGetAndRemoveCallback<RejectInviteCallbackInfoInternal, OnRejectInviteCallback, RejectInviteCallbackInfo> (ref data, out var callback, out var callbackInfo)) {
			callback (ref callbackInfo);
		}
	}

	[MonoPInvokeCallback (typeof(OnSendInviteCallbackInternal))]
	internal static void OnSendInviteCallbackInternalImplementation (ref SendInviteCallbackInfoInternal data)
	{
		if (Helper.TryGetAndRemoveCallback<SendInviteCallbackInfoInternal, OnSendInviteCallback, SendInviteCallbackInfo> (ref data, out var callback, out var callbackInfo)) {
			callback (ref callbackInfo);
		}
	}

	[MonoPInvokeCallback (typeof(OnSendLobbyNativeInviteRequestedCallbackInternal))]
	internal static void OnSendLobbyNativeInviteRequestedCallbackInternalImplementation (ref SendLobbyNativeInviteRequestedCallbackInfoInternal data)
	{
		if (Helper.TryGetCallback<SendLobbyNativeInviteRequestedCallbackInfoInternal, OnSendLobbyNativeInviteRequestedCallback, SendLobbyNativeInviteRequestedCallbackInfo> (ref data, out var callback, out var callbackInfo)) {
			callback (ref callbackInfo);
		}
	}

	[MonoPInvokeCallback (typeof(OnUpdateLobbyCallbackInternal))]
	internal static void OnUpdateLobbyCallbackInternalImplementation (ref UpdateLobbyCallbackInfoInternal data)
	{
		if (Helper.TryGetAndRemoveCallback<UpdateLobbyCallbackInfoInternal, OnUpdateLobbyCallback, UpdateLobbyCallbackInfo> (ref data, out var callback, out var callbackInfo)) {
			callback (ref callbackInfo);
		}
	}
}
