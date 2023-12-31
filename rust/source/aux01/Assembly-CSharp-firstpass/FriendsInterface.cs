using System;
using Epic.OnlineServices;
using Epic.OnlineServices.Friends;

public sealed class FriendsInterface : Handle
{
	public const int AcceptinviteApiLatest = 1;

	public const int AddnotifyfriendsupdateApiLatest = 1;

	public const int GetfriendatindexApiLatest = 1;

	public const int GetfriendscountApiLatest = 1;

	public const int GetstatusApiLatest = 1;

	public const int QueryfriendsApiLatest = 1;

	public const int RejectinviteApiLatest = 1;

	public const int SendinviteApiLatest = 1;

	public FriendsInterface ()
	{
	}

	public FriendsInterface (IntPtr innerHandle)
		: base (innerHandle)
	{
	}

	public void AcceptInvite (ref AcceptInviteOptions options, object clientData, OnAcceptInviteCallback completionDelegate)
	{
		AcceptInviteOptionsInternal options2 = default(AcceptInviteOptionsInternal);
		options2.Set (ref options);
		IntPtr clientDataAddress = IntPtr.Zero;
		OnAcceptInviteCallbackInternal onAcceptInviteCallbackInternal = OnAcceptInviteCallbackInternalImplementation;
		Helper.AddCallback (out clientDataAddress, clientData, completionDelegate, onAcceptInviteCallbackInternal);
		Bindings.EOS_Friends_AcceptInvite (base.InnerHandle, ref options2, clientDataAddress, onAcceptInviteCallbackInternal);
		Helper.Dispose (ref options2);
	}

	public ulong AddNotifyFriendsUpdate (ref AddNotifyFriendsUpdateOptions options, object clientData, OnFriendsUpdateCallback friendsUpdateHandler)
	{
		AddNotifyFriendsUpdateOptionsInternal options2 = default(AddNotifyFriendsUpdateOptionsInternal);
		options2.Set (ref options);
		IntPtr clientDataAddress = IntPtr.Zero;
		OnFriendsUpdateCallbackInternal onFriendsUpdateCallbackInternal = OnFriendsUpdateCallbackInternalImplementation;
		Helper.AddCallback (out clientDataAddress, clientData, friendsUpdateHandler, onFriendsUpdateCallbackInternal);
		ulong num = Bindings.EOS_Friends_AddNotifyFriendsUpdate (base.InnerHandle, ref options2, clientDataAddress, onFriendsUpdateCallbackInternal);
		Helper.Dispose (ref options2);
		Helper.AssignNotificationIdToCallback (clientDataAddress, num);
		return num;
	}

	public EpicAccountId GetFriendAtIndex (ref GetFriendAtIndexOptions options)
	{
		GetFriendAtIndexOptionsInternal options2 = default(GetFriendAtIndexOptionsInternal);
		options2.Set (ref options);
		IntPtr from = Bindings.EOS_Friends_GetFriendAtIndex (base.InnerHandle, ref options2);
		Helper.Dispose (ref options2);
		Helper.Get (from, out EpicAccountId to);
		return to;
	}

	public int GetFriendsCount (ref GetFriendsCountOptions options)
	{
		GetFriendsCountOptionsInternal options2 = default(GetFriendsCountOptionsInternal);
		options2.Set (ref options);
		int result = Bindings.EOS_Friends_GetFriendsCount (base.InnerHandle, ref options2);
		Helper.Dispose (ref options2);
		return result;
	}

	public FriendsStatus GetStatus (ref GetStatusOptions options)
	{
		GetStatusOptionsInternal options2 = default(GetStatusOptionsInternal);
		options2.Set (ref options);
		FriendsStatus result = Bindings.EOS_Friends_GetStatus (base.InnerHandle, ref options2);
		Helper.Dispose (ref options2);
		return result;
	}

	public void QueryFriends (ref QueryFriendsOptions options, object clientData, OnQueryFriendsCallback completionDelegate)
	{
		QueryFriendsOptionsInternal options2 = default(QueryFriendsOptionsInternal);
		options2.Set (ref options);
		IntPtr clientDataAddress = IntPtr.Zero;
		OnQueryFriendsCallbackInternal onQueryFriendsCallbackInternal = OnQueryFriendsCallbackInternalImplementation;
		Helper.AddCallback (out clientDataAddress, clientData, completionDelegate, onQueryFriendsCallbackInternal);
		Bindings.EOS_Friends_QueryFriends (base.InnerHandle, ref options2, clientDataAddress, onQueryFriendsCallbackInternal);
		Helper.Dispose (ref options2);
	}

	public void RejectInvite (ref RejectInviteOptions options, object clientData, OnRejectInviteCallback completionDelegate)
	{
		RejectInviteOptionsInternal options2 = default(RejectInviteOptionsInternal);
		options2.Set (ref options);
		IntPtr clientDataAddress = IntPtr.Zero;
		OnRejectInviteCallbackInternal onRejectInviteCallbackInternal = OnRejectInviteCallbackInternalImplementation;
		Helper.AddCallback (out clientDataAddress, clientData, completionDelegate, onRejectInviteCallbackInternal);
		Bindings.EOS_Friends_RejectInvite (base.InnerHandle, ref options2, clientDataAddress, onRejectInviteCallbackInternal);
		Helper.Dispose (ref options2);
	}

	public void RemoveNotifyFriendsUpdate (ulong notificationId)
	{
		Bindings.EOS_Friends_RemoveNotifyFriendsUpdate (base.InnerHandle, notificationId);
		Helper.RemoveCallbackByNotificationId (notificationId);
	}

	public void SendInvite (ref SendInviteOptions options, object clientData, OnSendInviteCallback completionDelegate)
	{
		SendInviteOptionsInternal options2 = default(SendInviteOptionsInternal);
		options2.Set (ref options);
		IntPtr clientDataAddress = IntPtr.Zero;
		OnSendInviteCallbackInternal onSendInviteCallbackInternal = OnSendInviteCallbackInternalImplementation;
		Helper.AddCallback (out clientDataAddress, clientData, completionDelegate, onSendInviteCallbackInternal);
		Bindings.EOS_Friends_SendInvite (base.InnerHandle, ref options2, clientDataAddress, onSendInviteCallbackInternal);
		Helper.Dispose (ref options2);
	}

	[MonoPInvokeCallback (typeof(OnAcceptInviteCallbackInternal))]
	internal static void OnAcceptInviteCallbackInternalImplementation (ref AcceptInviteCallbackInfoInternal data)
	{
		if (Helper.TryGetAndRemoveCallback<AcceptInviteCallbackInfoInternal, OnAcceptInviteCallback, AcceptInviteCallbackInfo> (ref data, out var callback, out var callbackInfo)) {
			callback (ref callbackInfo);
		}
	}

	[MonoPInvokeCallback (typeof(OnFriendsUpdateCallbackInternal))]
	internal static void OnFriendsUpdateCallbackInternalImplementation (ref OnFriendsUpdateInfoInternal data)
	{
		if (Helper.TryGetAndRemoveCallback<OnFriendsUpdateInfoInternal, OnFriendsUpdateCallback, OnFriendsUpdateInfo> (ref data, out var callback, out var callbackInfo)) {
			callback (ref callbackInfo);
		}
	}

	[MonoPInvokeCallback (typeof(OnQueryFriendsCallbackInternal))]
	internal static void OnQueryFriendsCallbackInternalImplementation (ref QueryFriendsCallbackInfoInternal data)
	{
		if (Helper.TryGetAndRemoveCallback<QueryFriendsCallbackInfoInternal, OnQueryFriendsCallback, QueryFriendsCallbackInfo> (ref data, out var callback, out var callbackInfo)) {
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
}
