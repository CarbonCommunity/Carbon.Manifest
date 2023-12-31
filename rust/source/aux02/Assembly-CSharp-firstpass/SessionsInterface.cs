using System;
using Epic.OnlineServices;
using Epic.OnlineServices.Sessions;

public sealed class SessionsInterface : Handle
{
	public const int AddnotifyjoinsessionacceptedApiLatest = 1;

	public const int AddnotifyleavesessionrequestedApiLatest = 1;

	public const int AddnotifysendsessionnativeinviterequestedApiLatest = 1;

	public const int AddnotifysessioninviteacceptedApiLatest = 1;

	public const int AddnotifysessioninvitereceivedApiLatest = 1;

	public const int AddnotifysessioninviterejectedApiLatest = 1;

	public const int AttributedataApiLatest = 1;

	public const int CopyactivesessionhandleApiLatest = 1;

	public const int CopysessionhandlebyinviteidApiLatest = 1;

	public const int CopysessionhandlebyuieventidApiLatest = 1;

	public const int CopysessionhandleforpresenceApiLatest = 1;

	public const int CreatesessionmodificationApiLatest = 5;

	public const int CreatesessionsearchApiLatest = 1;

	public const int DestroysessionApiLatest = 1;

	public const int DumpsessionstateApiLatest = 1;

	public const int EndsessionApiLatest = 1;

	public const int GetinvitecountApiLatest = 1;

	public const int GetinviteidbyindexApiLatest = 1;

	public const int InviteidMaxLength = 64;

	public const int IsuserinsessionApiLatest = 1;

	public const int JoinsessionApiLatest = 2;

	public const int MaxSearchResults = 200;

	public const int Maxregisteredplayers = 1000;

	public const int QueryinvitesApiLatest = 1;

	public const int RegisterplayersApiLatest = 3;

	public const int RejectinviteApiLatest = 1;

	public static readonly Utf8String SearchBucketId = "bucket";

	public static readonly Utf8String SearchEmptyServersOnly = "emptyonly";

	public static readonly Utf8String SearchMinslotsavailable = "minslotsavailable";

	public static readonly Utf8String SearchNonemptyServersOnly = "nonemptyonly";

	public const int SendinviteApiLatest = 1;

	public const int SessionattributeApiLatest = 1;

	public const int SessionattributedataApiLatest = 1;

	public const int StartsessionApiLatest = 1;

	public const int UnregisterplayersApiLatest = 2;

	public const int UpdatesessionApiLatest = 1;

	public const int UpdatesessionmodificationApiLatest = 1;

	public SessionsInterface ()
	{
	}

	public SessionsInterface (IntPtr innerHandle)
		: base (innerHandle)
	{
	}

	public ulong AddNotifyJoinSessionAccepted (ref AddNotifyJoinSessionAcceptedOptions options, object clientData, OnJoinSessionAcceptedCallback notificationFn)
	{
		AddNotifyJoinSessionAcceptedOptionsInternal options2 = default(AddNotifyJoinSessionAcceptedOptionsInternal);
		options2.Set (ref options);
		IntPtr clientDataAddress = IntPtr.Zero;
		OnJoinSessionAcceptedCallbackInternal onJoinSessionAcceptedCallbackInternal = OnJoinSessionAcceptedCallbackInternalImplementation;
		Helper.AddCallback (out clientDataAddress, clientData, notificationFn, onJoinSessionAcceptedCallbackInternal);
		ulong num = Bindings.EOS_Sessions_AddNotifyJoinSessionAccepted (base.InnerHandle, ref options2, clientDataAddress, onJoinSessionAcceptedCallbackInternal);
		Helper.Dispose (ref options2);
		Helper.AssignNotificationIdToCallback (clientDataAddress, num);
		return num;
	}

	public ulong AddNotifyLeaveSessionRequested (ref AddNotifyLeaveSessionRequestedOptions options, object clientData, OnLeaveSessionRequestedCallback notificationFn)
	{
		AddNotifyLeaveSessionRequestedOptionsInternal options2 = default(AddNotifyLeaveSessionRequestedOptionsInternal);
		options2.Set (ref options);
		IntPtr clientDataAddress = IntPtr.Zero;
		OnLeaveSessionRequestedCallbackInternal onLeaveSessionRequestedCallbackInternal = OnLeaveSessionRequestedCallbackInternalImplementation;
		Helper.AddCallback (out clientDataAddress, clientData, notificationFn, onLeaveSessionRequestedCallbackInternal);
		ulong num = Bindings.EOS_Sessions_AddNotifyLeaveSessionRequested (base.InnerHandle, ref options2, clientDataAddress, onLeaveSessionRequestedCallbackInternal);
		Helper.Dispose (ref options2);
		Helper.AssignNotificationIdToCallback (clientDataAddress, num);
		return num;
	}

	public ulong AddNotifySendSessionNativeInviteRequested (ref AddNotifySendSessionNativeInviteRequestedOptions options, object clientData, OnSendSessionNativeInviteRequestedCallback notificationFn)
	{
		AddNotifySendSessionNativeInviteRequestedOptionsInternal options2 = default(AddNotifySendSessionNativeInviteRequestedOptionsInternal);
		options2.Set (ref options);
		IntPtr clientDataAddress = IntPtr.Zero;
		OnSendSessionNativeInviteRequestedCallbackInternal onSendSessionNativeInviteRequestedCallbackInternal = OnSendSessionNativeInviteRequestedCallbackInternalImplementation;
		Helper.AddCallback (out clientDataAddress, clientData, notificationFn, onSendSessionNativeInviteRequestedCallbackInternal);
		ulong num = Bindings.EOS_Sessions_AddNotifySendSessionNativeInviteRequested (base.InnerHandle, ref options2, clientDataAddress, onSendSessionNativeInviteRequestedCallbackInternal);
		Helper.Dispose (ref options2);
		Helper.AssignNotificationIdToCallback (clientDataAddress, num);
		return num;
	}

	public ulong AddNotifySessionInviteAccepted (ref AddNotifySessionInviteAcceptedOptions options, object clientData, OnSessionInviteAcceptedCallback notificationFn)
	{
		AddNotifySessionInviteAcceptedOptionsInternal options2 = default(AddNotifySessionInviteAcceptedOptionsInternal);
		options2.Set (ref options);
		IntPtr clientDataAddress = IntPtr.Zero;
		OnSessionInviteAcceptedCallbackInternal onSessionInviteAcceptedCallbackInternal = OnSessionInviteAcceptedCallbackInternalImplementation;
		Helper.AddCallback (out clientDataAddress, clientData, notificationFn, onSessionInviteAcceptedCallbackInternal);
		ulong num = Bindings.EOS_Sessions_AddNotifySessionInviteAccepted (base.InnerHandle, ref options2, clientDataAddress, onSessionInviteAcceptedCallbackInternal);
		Helper.Dispose (ref options2);
		Helper.AssignNotificationIdToCallback (clientDataAddress, num);
		return num;
	}

	public ulong AddNotifySessionInviteReceived (ref AddNotifySessionInviteReceivedOptions options, object clientData, OnSessionInviteReceivedCallback notificationFn)
	{
		AddNotifySessionInviteReceivedOptionsInternal options2 = default(AddNotifySessionInviteReceivedOptionsInternal);
		options2.Set (ref options);
		IntPtr clientDataAddress = IntPtr.Zero;
		OnSessionInviteReceivedCallbackInternal onSessionInviteReceivedCallbackInternal = OnSessionInviteReceivedCallbackInternalImplementation;
		Helper.AddCallback (out clientDataAddress, clientData, notificationFn, onSessionInviteReceivedCallbackInternal);
		ulong num = Bindings.EOS_Sessions_AddNotifySessionInviteReceived (base.InnerHandle, ref options2, clientDataAddress, onSessionInviteReceivedCallbackInternal);
		Helper.Dispose (ref options2);
		Helper.AssignNotificationIdToCallback (clientDataAddress, num);
		return num;
	}

	public ulong AddNotifySessionInviteRejected (ref AddNotifySessionInviteRejectedOptions options, object clientData, OnSessionInviteRejectedCallback notificationFn)
	{
		AddNotifySessionInviteRejectedOptionsInternal options2 = default(AddNotifySessionInviteRejectedOptionsInternal);
		options2.Set (ref options);
		IntPtr clientDataAddress = IntPtr.Zero;
		OnSessionInviteRejectedCallbackInternal onSessionInviteRejectedCallbackInternal = OnSessionInviteRejectedCallbackInternalImplementation;
		Helper.AddCallback (out clientDataAddress, clientData, notificationFn, onSessionInviteRejectedCallbackInternal);
		ulong num = Bindings.EOS_Sessions_AddNotifySessionInviteRejected (base.InnerHandle, ref options2, clientDataAddress, onSessionInviteRejectedCallbackInternal);
		Helper.Dispose (ref options2);
		Helper.AssignNotificationIdToCallback (clientDataAddress, num);
		return num;
	}

	public Result CopyActiveSessionHandle (ref CopyActiveSessionHandleOptions options, out ActiveSession outSessionHandle)
	{
		CopyActiveSessionHandleOptionsInternal options2 = default(CopyActiveSessionHandleOptionsInternal);
		options2.Set (ref options);
		IntPtr outSessionHandle2 = IntPtr.Zero;
		Result result = Bindings.EOS_Sessions_CopyActiveSessionHandle (base.InnerHandle, ref options2, ref outSessionHandle2);
		Helper.Dispose (ref options2);
		Helper.Get (outSessionHandle2, out outSessionHandle);
		return result;
	}

	public Result CopySessionHandleByInviteId (ref CopySessionHandleByInviteIdOptions options, out SessionDetails outSessionHandle)
	{
		CopySessionHandleByInviteIdOptionsInternal options2 = default(CopySessionHandleByInviteIdOptionsInternal);
		options2.Set (ref options);
		IntPtr outSessionHandle2 = IntPtr.Zero;
		Result result = Bindings.EOS_Sessions_CopySessionHandleByInviteId (base.InnerHandle, ref options2, ref outSessionHandle2);
		Helper.Dispose (ref options2);
		Helper.Get (outSessionHandle2, out outSessionHandle);
		return result;
	}

	public Result CopySessionHandleByUiEventId (ref CopySessionHandleByUiEventIdOptions options, out SessionDetails outSessionHandle)
	{
		CopySessionHandleByUiEventIdOptionsInternal options2 = default(CopySessionHandleByUiEventIdOptionsInternal);
		options2.Set (ref options);
		IntPtr outSessionHandle2 = IntPtr.Zero;
		Result result = Bindings.EOS_Sessions_CopySessionHandleByUiEventId (base.InnerHandle, ref options2, ref outSessionHandle2);
		Helper.Dispose (ref options2);
		Helper.Get (outSessionHandle2, out outSessionHandle);
		return result;
	}

	public Result CopySessionHandleForPresence (ref CopySessionHandleForPresenceOptions options, out SessionDetails outSessionHandle)
	{
		CopySessionHandleForPresenceOptionsInternal options2 = default(CopySessionHandleForPresenceOptionsInternal);
		options2.Set (ref options);
		IntPtr outSessionHandle2 = IntPtr.Zero;
		Result result = Bindings.EOS_Sessions_CopySessionHandleForPresence (base.InnerHandle, ref options2, ref outSessionHandle2);
		Helper.Dispose (ref options2);
		Helper.Get (outSessionHandle2, out outSessionHandle);
		return result;
	}

	public Result CreateSessionModification (ref CreateSessionModificationOptions options, out SessionModification outSessionModificationHandle)
	{
		CreateSessionModificationOptionsInternal options2 = default(CreateSessionModificationOptionsInternal);
		options2.Set (ref options);
		IntPtr outSessionModificationHandle2 = IntPtr.Zero;
		Result result = Bindings.EOS_Sessions_CreateSessionModification (base.InnerHandle, ref options2, ref outSessionModificationHandle2);
		Helper.Dispose (ref options2);
		Helper.Get (outSessionModificationHandle2, out outSessionModificationHandle);
		return result;
	}

	public Result CreateSessionSearch (ref CreateSessionSearchOptions options, out SessionSearch outSessionSearchHandle)
	{
		CreateSessionSearchOptionsInternal options2 = default(CreateSessionSearchOptionsInternal);
		options2.Set (ref options);
		IntPtr outSessionSearchHandle2 = IntPtr.Zero;
		Result result = Bindings.EOS_Sessions_CreateSessionSearch (base.InnerHandle, ref options2, ref outSessionSearchHandle2);
		Helper.Dispose (ref options2);
		Helper.Get (outSessionSearchHandle2, out outSessionSearchHandle);
		return result;
	}

	public void DestroySession (ref DestroySessionOptions options, object clientData, OnDestroySessionCallback completionDelegate)
	{
		DestroySessionOptionsInternal options2 = default(DestroySessionOptionsInternal);
		options2.Set (ref options);
		IntPtr clientDataAddress = IntPtr.Zero;
		OnDestroySessionCallbackInternal onDestroySessionCallbackInternal = OnDestroySessionCallbackInternalImplementation;
		Helper.AddCallback (out clientDataAddress, clientData, completionDelegate, onDestroySessionCallbackInternal);
		Bindings.EOS_Sessions_DestroySession (base.InnerHandle, ref options2, clientDataAddress, onDestroySessionCallbackInternal);
		Helper.Dispose (ref options2);
	}

	public Result DumpSessionState (ref DumpSessionStateOptions options)
	{
		DumpSessionStateOptionsInternal options2 = default(DumpSessionStateOptionsInternal);
		options2.Set (ref options);
		Result result = Bindings.EOS_Sessions_DumpSessionState (base.InnerHandle, ref options2);
		Helper.Dispose (ref options2);
		return result;
	}

	public void EndSession (ref EndSessionOptions options, object clientData, OnEndSessionCallback completionDelegate)
	{
		EndSessionOptionsInternal options2 = default(EndSessionOptionsInternal);
		options2.Set (ref options);
		IntPtr clientDataAddress = IntPtr.Zero;
		OnEndSessionCallbackInternal onEndSessionCallbackInternal = OnEndSessionCallbackInternalImplementation;
		Helper.AddCallback (out clientDataAddress, clientData, completionDelegate, onEndSessionCallbackInternal);
		Bindings.EOS_Sessions_EndSession (base.InnerHandle, ref options2, clientDataAddress, onEndSessionCallbackInternal);
		Helper.Dispose (ref options2);
	}

	public uint GetInviteCount (ref GetInviteCountOptions options)
	{
		GetInviteCountOptionsInternal options2 = default(GetInviteCountOptionsInternal);
		options2.Set (ref options);
		uint result = Bindings.EOS_Sessions_GetInviteCount (base.InnerHandle, ref options2);
		Helper.Dispose (ref options2);
		return result;
	}

	public Result GetInviteIdByIndex (ref GetInviteIdByIndexOptions options, out Utf8String outBuffer)
	{
		GetInviteIdByIndexOptionsInternal options2 = default(GetInviteIdByIndexOptionsInternal);
		options2.Set (ref options);
		int inOutBufferLength = 65;
		IntPtr value = Helper.AddAllocation (inOutBufferLength);
		Result result = Bindings.EOS_Sessions_GetInviteIdByIndex (base.InnerHandle, ref options2, value, ref inOutBufferLength);
		Helper.Dispose (ref options2);
		Helper.Get (value, out outBuffer);
		Helper.Dispose (ref value);
		return result;
	}

	public Result IsUserInSession (ref IsUserInSessionOptions options)
	{
		IsUserInSessionOptionsInternal options2 = default(IsUserInSessionOptionsInternal);
		options2.Set (ref options);
		Result result = Bindings.EOS_Sessions_IsUserInSession (base.InnerHandle, ref options2);
		Helper.Dispose (ref options2);
		return result;
	}

	public void JoinSession (ref JoinSessionOptions options, object clientData, OnJoinSessionCallback completionDelegate)
	{
		JoinSessionOptionsInternal options2 = default(JoinSessionOptionsInternal);
		options2.Set (ref options);
		IntPtr clientDataAddress = IntPtr.Zero;
		OnJoinSessionCallbackInternal onJoinSessionCallbackInternal = OnJoinSessionCallbackInternalImplementation;
		Helper.AddCallback (out clientDataAddress, clientData, completionDelegate, onJoinSessionCallbackInternal);
		Bindings.EOS_Sessions_JoinSession (base.InnerHandle, ref options2, clientDataAddress, onJoinSessionCallbackInternal);
		Helper.Dispose (ref options2);
	}

	public void QueryInvites (ref QueryInvitesOptions options, object clientData, OnQueryInvitesCallback completionDelegate)
	{
		QueryInvitesOptionsInternal options2 = default(QueryInvitesOptionsInternal);
		options2.Set (ref options);
		IntPtr clientDataAddress = IntPtr.Zero;
		OnQueryInvitesCallbackInternal onQueryInvitesCallbackInternal = OnQueryInvitesCallbackInternalImplementation;
		Helper.AddCallback (out clientDataAddress, clientData, completionDelegate, onQueryInvitesCallbackInternal);
		Bindings.EOS_Sessions_QueryInvites (base.InnerHandle, ref options2, clientDataAddress, onQueryInvitesCallbackInternal);
		Helper.Dispose (ref options2);
	}

	public void RegisterPlayers (ref RegisterPlayersOptions options, object clientData, OnRegisterPlayersCallback completionDelegate)
	{
		RegisterPlayersOptionsInternal options2 = default(RegisterPlayersOptionsInternal);
		options2.Set (ref options);
		IntPtr clientDataAddress = IntPtr.Zero;
		OnRegisterPlayersCallbackInternal onRegisterPlayersCallbackInternal = OnRegisterPlayersCallbackInternalImplementation;
		Helper.AddCallback (out clientDataAddress, clientData, completionDelegate, onRegisterPlayersCallbackInternal);
		Bindings.EOS_Sessions_RegisterPlayers (base.InnerHandle, ref options2, clientDataAddress, onRegisterPlayersCallbackInternal);
		Helper.Dispose (ref options2);
	}

	public void RejectInvite (ref RejectInviteOptions options, object clientData, OnRejectInviteCallback completionDelegate)
	{
		RejectInviteOptionsInternal options2 = default(RejectInviteOptionsInternal);
		options2.Set (ref options);
		IntPtr clientDataAddress = IntPtr.Zero;
		OnRejectInviteCallbackInternal onRejectInviteCallbackInternal = OnRejectInviteCallbackInternalImplementation;
		Helper.AddCallback (out clientDataAddress, clientData, completionDelegate, onRejectInviteCallbackInternal);
		Bindings.EOS_Sessions_RejectInvite (base.InnerHandle, ref options2, clientDataAddress, onRejectInviteCallbackInternal);
		Helper.Dispose (ref options2);
	}

	public void RemoveNotifyJoinSessionAccepted (ulong inId)
	{
		Bindings.EOS_Sessions_RemoveNotifyJoinSessionAccepted (base.InnerHandle, inId);
		Helper.RemoveCallbackByNotificationId (inId);
	}

	public void RemoveNotifyLeaveSessionRequested (ulong inId)
	{
		Bindings.EOS_Sessions_RemoveNotifyLeaveSessionRequested (base.InnerHandle, inId);
		Helper.RemoveCallbackByNotificationId (inId);
	}

	public void RemoveNotifySendSessionNativeInviteRequested (ulong inId)
	{
		Bindings.EOS_Sessions_RemoveNotifySendSessionNativeInviteRequested (base.InnerHandle, inId);
		Helper.RemoveCallbackByNotificationId (inId);
	}

	public void RemoveNotifySessionInviteAccepted (ulong inId)
	{
		Bindings.EOS_Sessions_RemoveNotifySessionInviteAccepted (base.InnerHandle, inId);
		Helper.RemoveCallbackByNotificationId (inId);
	}

	public void RemoveNotifySessionInviteReceived (ulong inId)
	{
		Bindings.EOS_Sessions_RemoveNotifySessionInviteReceived (base.InnerHandle, inId);
		Helper.RemoveCallbackByNotificationId (inId);
	}

	public void RemoveNotifySessionInviteRejected (ulong inId)
	{
		Bindings.EOS_Sessions_RemoveNotifySessionInviteRejected (base.InnerHandle, inId);
		Helper.RemoveCallbackByNotificationId (inId);
	}

	public void SendInvite (ref SendInviteOptions options, object clientData, OnSendInviteCallback completionDelegate)
	{
		SendInviteOptionsInternal options2 = default(SendInviteOptionsInternal);
		options2.Set (ref options);
		IntPtr clientDataAddress = IntPtr.Zero;
		OnSendInviteCallbackInternal onSendInviteCallbackInternal = OnSendInviteCallbackInternalImplementation;
		Helper.AddCallback (out clientDataAddress, clientData, completionDelegate, onSendInviteCallbackInternal);
		Bindings.EOS_Sessions_SendInvite (base.InnerHandle, ref options2, clientDataAddress, onSendInviteCallbackInternal);
		Helper.Dispose (ref options2);
	}

	public void StartSession (ref StartSessionOptions options, object clientData, OnStartSessionCallback completionDelegate)
	{
		StartSessionOptionsInternal options2 = default(StartSessionOptionsInternal);
		options2.Set (ref options);
		IntPtr clientDataAddress = IntPtr.Zero;
		OnStartSessionCallbackInternal onStartSessionCallbackInternal = OnStartSessionCallbackInternalImplementation;
		Helper.AddCallback (out clientDataAddress, clientData, completionDelegate, onStartSessionCallbackInternal);
		Bindings.EOS_Sessions_StartSession (base.InnerHandle, ref options2, clientDataAddress, onStartSessionCallbackInternal);
		Helper.Dispose (ref options2);
	}

	public void UnregisterPlayers (ref UnregisterPlayersOptions options, object clientData, OnUnregisterPlayersCallback completionDelegate)
	{
		UnregisterPlayersOptionsInternal options2 = default(UnregisterPlayersOptionsInternal);
		options2.Set (ref options);
		IntPtr clientDataAddress = IntPtr.Zero;
		OnUnregisterPlayersCallbackInternal onUnregisterPlayersCallbackInternal = OnUnregisterPlayersCallbackInternalImplementation;
		Helper.AddCallback (out clientDataAddress, clientData, completionDelegate, onUnregisterPlayersCallbackInternal);
		Bindings.EOS_Sessions_UnregisterPlayers (base.InnerHandle, ref options2, clientDataAddress, onUnregisterPlayersCallbackInternal);
		Helper.Dispose (ref options2);
	}

	public void UpdateSession (ref UpdateSessionOptions options, object clientData, OnUpdateSessionCallback completionDelegate)
	{
		UpdateSessionOptionsInternal options2 = default(UpdateSessionOptionsInternal);
		options2.Set (ref options);
		IntPtr clientDataAddress = IntPtr.Zero;
		OnUpdateSessionCallbackInternal onUpdateSessionCallbackInternal = OnUpdateSessionCallbackInternalImplementation;
		Helper.AddCallback (out clientDataAddress, clientData, completionDelegate, onUpdateSessionCallbackInternal);
		Bindings.EOS_Sessions_UpdateSession (base.InnerHandle, ref options2, clientDataAddress, onUpdateSessionCallbackInternal);
		Helper.Dispose (ref options2);
	}

	public Result UpdateSessionModification (ref UpdateSessionModificationOptions options, out SessionModification outSessionModificationHandle)
	{
		UpdateSessionModificationOptionsInternal options2 = default(UpdateSessionModificationOptionsInternal);
		options2.Set (ref options);
		IntPtr outSessionModificationHandle2 = IntPtr.Zero;
		Result result = Bindings.EOS_Sessions_UpdateSessionModification (base.InnerHandle, ref options2, ref outSessionModificationHandle2);
		Helper.Dispose (ref options2);
		Helper.Get (outSessionModificationHandle2, out outSessionModificationHandle);
		return result;
	}

	[MonoPInvokeCallback (typeof(OnDestroySessionCallbackInternal))]
	internal static void OnDestroySessionCallbackInternalImplementation (ref DestroySessionCallbackInfoInternal data)
	{
		if (Helper.TryGetAndRemoveCallback<DestroySessionCallbackInfoInternal, OnDestroySessionCallback, DestroySessionCallbackInfo> (ref data, out var callback, out var callbackInfo)) {
			callback (ref callbackInfo);
		}
	}

	[MonoPInvokeCallback (typeof(OnEndSessionCallbackInternal))]
	internal static void OnEndSessionCallbackInternalImplementation (ref EndSessionCallbackInfoInternal data)
	{
		if (Helper.TryGetAndRemoveCallback<EndSessionCallbackInfoInternal, OnEndSessionCallback, EndSessionCallbackInfo> (ref data, out var callback, out var callbackInfo)) {
			callback (ref callbackInfo);
		}
	}

	[MonoPInvokeCallback (typeof(OnJoinSessionAcceptedCallbackInternal))]
	internal static void OnJoinSessionAcceptedCallbackInternalImplementation (ref JoinSessionAcceptedCallbackInfoInternal data)
	{
		if (Helper.TryGetCallback<JoinSessionAcceptedCallbackInfoInternal, OnJoinSessionAcceptedCallback, JoinSessionAcceptedCallbackInfo> (ref data, out var callback, out var callbackInfo)) {
			callback (ref callbackInfo);
		}
	}

	[MonoPInvokeCallback (typeof(OnJoinSessionCallbackInternal))]
	internal static void OnJoinSessionCallbackInternalImplementation (ref JoinSessionCallbackInfoInternal data)
	{
		if (Helper.TryGetAndRemoveCallback<JoinSessionCallbackInfoInternal, OnJoinSessionCallback, JoinSessionCallbackInfo> (ref data, out var callback, out var callbackInfo)) {
			callback (ref callbackInfo);
		}
	}

	[MonoPInvokeCallback (typeof(OnLeaveSessionRequestedCallbackInternal))]
	internal static void OnLeaveSessionRequestedCallbackInternalImplementation (ref LeaveSessionRequestedCallbackInfoInternal data)
	{
		if (Helper.TryGetCallback<LeaveSessionRequestedCallbackInfoInternal, OnLeaveSessionRequestedCallback, LeaveSessionRequestedCallbackInfo> (ref data, out var callback, out var callbackInfo)) {
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

	[MonoPInvokeCallback (typeof(OnRegisterPlayersCallbackInternal))]
	internal static void OnRegisterPlayersCallbackInternalImplementation (ref RegisterPlayersCallbackInfoInternal data)
	{
		if (Helper.TryGetAndRemoveCallback<RegisterPlayersCallbackInfoInternal, OnRegisterPlayersCallback, RegisterPlayersCallbackInfo> (ref data, out var callback, out var callbackInfo)) {
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

	[MonoPInvokeCallback (typeof(OnSendSessionNativeInviteRequestedCallbackInternal))]
	internal static void OnSendSessionNativeInviteRequestedCallbackInternalImplementation (ref SendSessionNativeInviteRequestedCallbackInfoInternal data)
	{
		if (Helper.TryGetCallback<SendSessionNativeInviteRequestedCallbackInfoInternal, OnSendSessionNativeInviteRequestedCallback, SendSessionNativeInviteRequestedCallbackInfo> (ref data, out var callback, out var callbackInfo)) {
			callback (ref callbackInfo);
		}
	}

	[MonoPInvokeCallback (typeof(OnSessionInviteAcceptedCallbackInternal))]
	internal static void OnSessionInviteAcceptedCallbackInternalImplementation (ref SessionInviteAcceptedCallbackInfoInternal data)
	{
		if (Helper.TryGetCallback<SessionInviteAcceptedCallbackInfoInternal, OnSessionInviteAcceptedCallback, SessionInviteAcceptedCallbackInfo> (ref data, out var callback, out var callbackInfo)) {
			callback (ref callbackInfo);
		}
	}

	[MonoPInvokeCallback (typeof(OnSessionInviteReceivedCallbackInternal))]
	internal static void OnSessionInviteReceivedCallbackInternalImplementation (ref SessionInviteReceivedCallbackInfoInternal data)
	{
		if (Helper.TryGetCallback<SessionInviteReceivedCallbackInfoInternal, OnSessionInviteReceivedCallback, SessionInviteReceivedCallbackInfo> (ref data, out var callback, out var callbackInfo)) {
			callback (ref callbackInfo);
		}
	}

	[MonoPInvokeCallback (typeof(OnSessionInviteRejectedCallbackInternal))]
	internal static void OnSessionInviteRejectedCallbackInternalImplementation (ref SessionInviteRejectedCallbackInfoInternal data)
	{
		if (Helper.TryGetCallback<SessionInviteRejectedCallbackInfoInternal, OnSessionInviteRejectedCallback, SessionInviteRejectedCallbackInfo> (ref data, out var callback, out var callbackInfo)) {
			callback (ref callbackInfo);
		}
	}

	[MonoPInvokeCallback (typeof(OnStartSessionCallbackInternal))]
	internal static void OnStartSessionCallbackInternalImplementation (ref StartSessionCallbackInfoInternal data)
	{
		if (Helper.TryGetAndRemoveCallback<StartSessionCallbackInfoInternal, OnStartSessionCallback, StartSessionCallbackInfo> (ref data, out var callback, out var callbackInfo)) {
			callback (ref callbackInfo);
		}
	}

	[MonoPInvokeCallback (typeof(OnUnregisterPlayersCallbackInternal))]
	internal static void OnUnregisterPlayersCallbackInternalImplementation (ref UnregisterPlayersCallbackInfoInternal data)
	{
		if (Helper.TryGetAndRemoveCallback<UnregisterPlayersCallbackInfoInternal, OnUnregisterPlayersCallback, UnregisterPlayersCallbackInfo> (ref data, out var callback, out var callbackInfo)) {
			callback (ref callbackInfo);
		}
	}

	[MonoPInvokeCallback (typeof(OnUpdateSessionCallbackInternal))]
	internal static void OnUpdateSessionCallbackInternalImplementation (ref UpdateSessionCallbackInfoInternal data)
	{
		if (Helper.TryGetAndRemoveCallback<UpdateSessionCallbackInfoInternal, OnUpdateSessionCallback, UpdateSessionCallbackInfo> (ref data, out var callback, out var callbackInfo)) {
			callback (ref callbackInfo);
		}
	}
}
