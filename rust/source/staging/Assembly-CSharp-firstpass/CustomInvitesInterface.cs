using System;
using Epic.OnlineServices;
using Epic.OnlineServices.CustomInvites;

public sealed class CustomInvitesInterface : Handle
{
	public const int AcceptrequesttojoinApiLatest = 1;

	public const int AddnotifycustominviteacceptedApiLatest = 1;

	public const int AddnotifycustominvitereceivedApiLatest = 1;

	public const int AddnotifycustominviterejectedApiLatest = 1;

	public const int AddnotifyrequesttojoinacceptedApiLatest = 1;

	public const int AddnotifyrequesttojoinreceivedApiLatest = 1;

	public const int AddnotifyrequesttojoinrejectedApiLatest = 1;

	public const int AddnotifyrequesttojoinresponsereceivedApiLatest = 1;

	public const int AddnotifysendcustomnativeinviterequestedApiLatest = 1;

	public const int FinalizeinviteApiLatest = 1;

	public const int MaxPayloadLength = 500;

	public const int RejectrequesttojoinApiLatest = 1;

	public const int SendcustominviteApiLatest = 1;

	public const int SendrequesttojoinApiLatest = 1;

	public const int SetcustominviteApiLatest = 1;

	public CustomInvitesInterface ()
	{
	}

	public CustomInvitesInterface (IntPtr innerHandle)
		: base (innerHandle)
	{
	}

	public void AcceptRequestToJoin (ref AcceptRequestToJoinOptions options, object clientData, OnAcceptRequestToJoinCallback completionDelegate)
	{
		AcceptRequestToJoinOptionsInternal options2 = default(AcceptRequestToJoinOptionsInternal);
		options2.Set (ref options);
		IntPtr clientDataAddress = IntPtr.Zero;
		OnAcceptRequestToJoinCallbackInternal onAcceptRequestToJoinCallbackInternal = OnAcceptRequestToJoinCallbackInternalImplementation;
		Helper.AddCallback (out clientDataAddress, clientData, completionDelegate, onAcceptRequestToJoinCallbackInternal);
		Bindings.EOS_CustomInvites_AcceptRequestToJoin (base.InnerHandle, ref options2, clientDataAddress, onAcceptRequestToJoinCallbackInternal);
		Helper.Dispose (ref options2);
	}

	public ulong AddNotifyCustomInviteAccepted (ref AddNotifyCustomInviteAcceptedOptions options, object clientData, OnCustomInviteAcceptedCallback notificationFn)
	{
		AddNotifyCustomInviteAcceptedOptionsInternal options2 = default(AddNotifyCustomInviteAcceptedOptionsInternal);
		options2.Set (ref options);
		IntPtr clientDataAddress = IntPtr.Zero;
		OnCustomInviteAcceptedCallbackInternal onCustomInviteAcceptedCallbackInternal = OnCustomInviteAcceptedCallbackInternalImplementation;
		Helper.AddCallback (out clientDataAddress, clientData, notificationFn, onCustomInviteAcceptedCallbackInternal);
		ulong num = Bindings.EOS_CustomInvites_AddNotifyCustomInviteAccepted (base.InnerHandle, ref options2, clientDataAddress, onCustomInviteAcceptedCallbackInternal);
		Helper.Dispose (ref options2);
		Helper.AssignNotificationIdToCallback (clientDataAddress, num);
		return num;
	}

	public ulong AddNotifyCustomInviteReceived (ref AddNotifyCustomInviteReceivedOptions options, object clientData, OnCustomInviteReceivedCallback notificationFn)
	{
		AddNotifyCustomInviteReceivedOptionsInternal options2 = default(AddNotifyCustomInviteReceivedOptionsInternal);
		options2.Set (ref options);
		IntPtr clientDataAddress = IntPtr.Zero;
		OnCustomInviteReceivedCallbackInternal onCustomInviteReceivedCallbackInternal = OnCustomInviteReceivedCallbackInternalImplementation;
		Helper.AddCallback (out clientDataAddress, clientData, notificationFn, onCustomInviteReceivedCallbackInternal);
		ulong num = Bindings.EOS_CustomInvites_AddNotifyCustomInviteReceived (base.InnerHandle, ref options2, clientDataAddress, onCustomInviteReceivedCallbackInternal);
		Helper.Dispose (ref options2);
		Helper.AssignNotificationIdToCallback (clientDataAddress, num);
		return num;
	}

	public ulong AddNotifyCustomInviteRejected (ref AddNotifyCustomInviteRejectedOptions options, object clientData, OnCustomInviteRejectedCallback notificationFn)
	{
		AddNotifyCustomInviteRejectedOptionsInternal options2 = default(AddNotifyCustomInviteRejectedOptionsInternal);
		options2.Set (ref options);
		IntPtr clientDataAddress = IntPtr.Zero;
		OnCustomInviteRejectedCallbackInternal onCustomInviteRejectedCallbackInternal = OnCustomInviteRejectedCallbackInternalImplementation;
		Helper.AddCallback (out clientDataAddress, clientData, notificationFn, onCustomInviteRejectedCallbackInternal);
		ulong num = Bindings.EOS_CustomInvites_AddNotifyCustomInviteRejected (base.InnerHandle, ref options2, clientDataAddress, onCustomInviteRejectedCallbackInternal);
		Helper.Dispose (ref options2);
		Helper.AssignNotificationIdToCallback (clientDataAddress, num);
		return num;
	}

	public ulong AddNotifyRequestToJoinAccepted (ref AddNotifyRequestToJoinAcceptedOptions options, object clientData, OnRequestToJoinAcceptedCallback notificationFn)
	{
		AddNotifyRequestToJoinAcceptedOptionsInternal options2 = default(AddNotifyRequestToJoinAcceptedOptionsInternal);
		options2.Set (ref options);
		IntPtr clientDataAddress = IntPtr.Zero;
		OnRequestToJoinAcceptedCallbackInternal onRequestToJoinAcceptedCallbackInternal = OnRequestToJoinAcceptedCallbackInternalImplementation;
		Helper.AddCallback (out clientDataAddress, clientData, notificationFn, onRequestToJoinAcceptedCallbackInternal);
		ulong num = Bindings.EOS_CustomInvites_AddNotifyRequestToJoinAccepted (base.InnerHandle, ref options2, clientDataAddress, onRequestToJoinAcceptedCallbackInternal);
		Helper.Dispose (ref options2);
		Helper.AssignNotificationIdToCallback (clientDataAddress, num);
		return num;
	}

	public ulong AddNotifyRequestToJoinReceived (ref AddNotifyRequestToJoinReceivedOptions options, object clientData, OnRequestToJoinReceivedCallback notificationFn)
	{
		AddNotifyRequestToJoinReceivedOptionsInternal options2 = default(AddNotifyRequestToJoinReceivedOptionsInternal);
		options2.Set (ref options);
		IntPtr clientDataAddress = IntPtr.Zero;
		OnRequestToJoinReceivedCallbackInternal onRequestToJoinReceivedCallbackInternal = OnRequestToJoinReceivedCallbackInternalImplementation;
		Helper.AddCallback (out clientDataAddress, clientData, notificationFn, onRequestToJoinReceivedCallbackInternal);
		ulong num = Bindings.EOS_CustomInvites_AddNotifyRequestToJoinReceived (base.InnerHandle, ref options2, clientDataAddress, onRequestToJoinReceivedCallbackInternal);
		Helper.Dispose (ref options2);
		Helper.AssignNotificationIdToCallback (clientDataAddress, num);
		return num;
	}

	public ulong AddNotifyRequestToJoinRejected (ref AddNotifyRequestToJoinRejectedOptions options, object clientData, OnRequestToJoinRejectedCallback notificationFn)
	{
		AddNotifyRequestToJoinRejectedOptionsInternal options2 = default(AddNotifyRequestToJoinRejectedOptionsInternal);
		options2.Set (ref options);
		IntPtr clientDataAddress = IntPtr.Zero;
		OnRequestToJoinRejectedCallbackInternal onRequestToJoinRejectedCallbackInternal = OnRequestToJoinRejectedCallbackInternalImplementation;
		Helper.AddCallback (out clientDataAddress, clientData, notificationFn, onRequestToJoinRejectedCallbackInternal);
		ulong num = Bindings.EOS_CustomInvites_AddNotifyRequestToJoinRejected (base.InnerHandle, ref options2, clientDataAddress, onRequestToJoinRejectedCallbackInternal);
		Helper.Dispose (ref options2);
		Helper.AssignNotificationIdToCallback (clientDataAddress, num);
		return num;
	}

	public ulong AddNotifyRequestToJoinResponseReceived (ref AddNotifyRequestToJoinResponseReceivedOptions options, object clientData, OnRequestToJoinResponseReceivedCallback notificationFn)
	{
		AddNotifyRequestToJoinResponseReceivedOptionsInternal options2 = default(AddNotifyRequestToJoinResponseReceivedOptionsInternal);
		options2.Set (ref options);
		IntPtr clientDataAddress = IntPtr.Zero;
		OnRequestToJoinResponseReceivedCallbackInternal onRequestToJoinResponseReceivedCallbackInternal = OnRequestToJoinResponseReceivedCallbackInternalImplementation;
		Helper.AddCallback (out clientDataAddress, clientData, notificationFn, onRequestToJoinResponseReceivedCallbackInternal);
		ulong num = Bindings.EOS_CustomInvites_AddNotifyRequestToJoinResponseReceived (base.InnerHandle, ref options2, clientDataAddress, onRequestToJoinResponseReceivedCallbackInternal);
		Helper.Dispose (ref options2);
		Helper.AssignNotificationIdToCallback (clientDataAddress, num);
		return num;
	}

	public ulong AddNotifySendCustomNativeInviteRequested (ref AddNotifySendCustomNativeInviteRequestedOptions options, object clientData, OnSendCustomNativeInviteRequestedCallback notificationFn)
	{
		AddNotifySendCustomNativeInviteRequestedOptionsInternal options2 = default(AddNotifySendCustomNativeInviteRequestedOptionsInternal);
		options2.Set (ref options);
		IntPtr clientDataAddress = IntPtr.Zero;
		OnSendCustomNativeInviteRequestedCallbackInternal onSendCustomNativeInviteRequestedCallbackInternal = OnSendCustomNativeInviteRequestedCallbackInternalImplementation;
		Helper.AddCallback (out clientDataAddress, clientData, notificationFn, onSendCustomNativeInviteRequestedCallbackInternal);
		ulong num = Bindings.EOS_CustomInvites_AddNotifySendCustomNativeInviteRequested (base.InnerHandle, ref options2, clientDataAddress, onSendCustomNativeInviteRequestedCallbackInternal);
		Helper.Dispose (ref options2);
		Helper.AssignNotificationIdToCallback (clientDataAddress, num);
		return num;
	}

	public Result FinalizeInvite (ref FinalizeInviteOptions options)
	{
		FinalizeInviteOptionsInternal options2 = default(FinalizeInviteOptionsInternal);
		options2.Set (ref options);
		Result result = Bindings.EOS_CustomInvites_FinalizeInvite (base.InnerHandle, ref options2);
		Helper.Dispose (ref options2);
		return result;
	}

	public void RejectRequestToJoin (ref RejectRequestToJoinOptions options, object clientData, OnRejectRequestToJoinCallback completionDelegate)
	{
		RejectRequestToJoinOptionsInternal options2 = default(RejectRequestToJoinOptionsInternal);
		options2.Set (ref options);
		IntPtr clientDataAddress = IntPtr.Zero;
		OnRejectRequestToJoinCallbackInternal onRejectRequestToJoinCallbackInternal = OnRejectRequestToJoinCallbackInternalImplementation;
		Helper.AddCallback (out clientDataAddress, clientData, completionDelegate, onRejectRequestToJoinCallbackInternal);
		Bindings.EOS_CustomInvites_RejectRequestToJoin (base.InnerHandle, ref options2, clientDataAddress, onRejectRequestToJoinCallbackInternal);
		Helper.Dispose (ref options2);
	}

	public void RemoveNotifyCustomInviteAccepted (ulong inId)
	{
		Bindings.EOS_CustomInvites_RemoveNotifyCustomInviteAccepted (base.InnerHandle, inId);
		Helper.RemoveCallbackByNotificationId (inId);
	}

	public void RemoveNotifyCustomInviteReceived (ulong inId)
	{
		Bindings.EOS_CustomInvites_RemoveNotifyCustomInviteReceived (base.InnerHandle, inId);
		Helper.RemoveCallbackByNotificationId (inId);
	}

	public void RemoveNotifyCustomInviteRejected (ulong inId)
	{
		Bindings.EOS_CustomInvites_RemoveNotifyCustomInviteRejected (base.InnerHandle, inId);
		Helper.RemoveCallbackByNotificationId (inId);
	}

	public void RemoveNotifyRequestToJoinAccepted (ulong inId)
	{
		Bindings.EOS_CustomInvites_RemoveNotifyRequestToJoinAccepted (base.InnerHandle, inId);
		Helper.RemoveCallbackByNotificationId (inId);
	}

	public void RemoveNotifyRequestToJoinReceived (ulong inId)
	{
		Bindings.EOS_CustomInvites_RemoveNotifyRequestToJoinReceived (base.InnerHandle, inId);
		Helper.RemoveCallbackByNotificationId (inId);
	}

	public void RemoveNotifyRequestToJoinRejected (ulong inId)
	{
		Bindings.EOS_CustomInvites_RemoveNotifyRequestToJoinRejected (base.InnerHandle, inId);
		Helper.RemoveCallbackByNotificationId (inId);
	}

	public void RemoveNotifyRequestToJoinResponseReceived (ulong inId)
	{
		Bindings.EOS_CustomInvites_RemoveNotifyRequestToJoinResponseReceived (base.InnerHandle, inId);
		Helper.RemoveCallbackByNotificationId (inId);
	}

	public void RemoveNotifySendCustomNativeInviteRequested (ulong inId)
	{
		Bindings.EOS_CustomInvites_RemoveNotifySendCustomNativeInviteRequested (base.InnerHandle, inId);
		Helper.RemoveCallbackByNotificationId (inId);
	}

	public void SendCustomInvite (ref SendCustomInviteOptions options, object clientData, OnSendCustomInviteCallback completionDelegate)
	{
		SendCustomInviteOptionsInternal options2 = default(SendCustomInviteOptionsInternal);
		options2.Set (ref options);
		IntPtr clientDataAddress = IntPtr.Zero;
		OnSendCustomInviteCallbackInternal onSendCustomInviteCallbackInternal = OnSendCustomInviteCallbackInternalImplementation;
		Helper.AddCallback (out clientDataAddress, clientData, completionDelegate, onSendCustomInviteCallbackInternal);
		Bindings.EOS_CustomInvites_SendCustomInvite (base.InnerHandle, ref options2, clientDataAddress, onSendCustomInviteCallbackInternal);
		Helper.Dispose (ref options2);
	}

	public void SendRequestToJoin (ref SendRequestToJoinOptions options, object clientData, OnSendRequestToJoinCallback completionDelegate)
	{
		SendRequestToJoinOptionsInternal options2 = default(SendRequestToJoinOptionsInternal);
		options2.Set (ref options);
		IntPtr clientDataAddress = IntPtr.Zero;
		OnSendRequestToJoinCallbackInternal onSendRequestToJoinCallbackInternal = OnSendRequestToJoinCallbackInternalImplementation;
		Helper.AddCallback (out clientDataAddress, clientData, completionDelegate, onSendRequestToJoinCallbackInternal);
		Bindings.EOS_CustomInvites_SendRequestToJoin (base.InnerHandle, ref options2, clientDataAddress, onSendRequestToJoinCallbackInternal);
		Helper.Dispose (ref options2);
	}

	public Result SetCustomInvite (ref SetCustomInviteOptions options)
	{
		SetCustomInviteOptionsInternal options2 = default(SetCustomInviteOptionsInternal);
		options2.Set (ref options);
		Result result = Bindings.EOS_CustomInvites_SetCustomInvite (base.InnerHandle, ref options2);
		Helper.Dispose (ref options2);
		return result;
	}

	[MonoPInvokeCallback (typeof(OnAcceptRequestToJoinCallbackInternal))]
	internal static void OnAcceptRequestToJoinCallbackInternalImplementation (ref AcceptRequestToJoinCallbackInfoInternal data)
	{
		if (Helper.TryGetAndRemoveCallback<AcceptRequestToJoinCallbackInfoInternal, OnAcceptRequestToJoinCallback, AcceptRequestToJoinCallbackInfo> (ref data, out var callback, out var callbackInfo)) {
			callback (ref callbackInfo);
		}
	}

	[MonoPInvokeCallback (typeof(OnCustomInviteAcceptedCallbackInternal))]
	internal static void OnCustomInviteAcceptedCallbackInternalImplementation (ref OnCustomInviteAcceptedCallbackInfoInternal data)
	{
		if (Helper.TryGetCallback<OnCustomInviteAcceptedCallbackInfoInternal, OnCustomInviteAcceptedCallback, OnCustomInviteAcceptedCallbackInfo> (ref data, out var callback, out var callbackInfo)) {
			callback (ref callbackInfo);
		}
	}

	[MonoPInvokeCallback (typeof(OnCustomInviteReceivedCallbackInternal))]
	internal static void OnCustomInviteReceivedCallbackInternalImplementation (ref OnCustomInviteReceivedCallbackInfoInternal data)
	{
		if (Helper.TryGetCallback<OnCustomInviteReceivedCallbackInfoInternal, OnCustomInviteReceivedCallback, OnCustomInviteReceivedCallbackInfo> (ref data, out var callback, out var callbackInfo)) {
			callback (ref callbackInfo);
		}
	}

	[MonoPInvokeCallback (typeof(OnCustomInviteRejectedCallbackInternal))]
	internal static void OnCustomInviteRejectedCallbackInternalImplementation (ref CustomInviteRejectedCallbackInfoInternal data)
	{
		if (Helper.TryGetCallback<CustomInviteRejectedCallbackInfoInternal, OnCustomInviteRejectedCallback, CustomInviteRejectedCallbackInfo> (ref data, out var callback, out var callbackInfo)) {
			callback (ref callbackInfo);
		}
	}

	[MonoPInvokeCallback (typeof(OnRejectRequestToJoinCallbackInternal))]
	internal static void OnRejectRequestToJoinCallbackInternalImplementation (ref RejectRequestToJoinCallbackInfoInternal data)
	{
		if (Helper.TryGetAndRemoveCallback<RejectRequestToJoinCallbackInfoInternal, OnRejectRequestToJoinCallback, RejectRequestToJoinCallbackInfo> (ref data, out var callback, out var callbackInfo)) {
			callback (ref callbackInfo);
		}
	}

	[MonoPInvokeCallback (typeof(OnRequestToJoinAcceptedCallbackInternal))]
	internal static void OnRequestToJoinAcceptedCallbackInternalImplementation (ref OnRequestToJoinAcceptedCallbackInfoInternal data)
	{
		if (Helper.TryGetCallback<OnRequestToJoinAcceptedCallbackInfoInternal, OnRequestToJoinAcceptedCallback, OnRequestToJoinAcceptedCallbackInfo> (ref data, out var callback, out var callbackInfo)) {
			callback (ref callbackInfo);
		}
	}

	[MonoPInvokeCallback (typeof(OnRequestToJoinReceivedCallbackInternal))]
	internal static void OnRequestToJoinReceivedCallbackInternalImplementation (ref RequestToJoinReceivedCallbackInfoInternal data)
	{
		if (Helper.TryGetCallback<RequestToJoinReceivedCallbackInfoInternal, OnRequestToJoinReceivedCallback, RequestToJoinReceivedCallbackInfo> (ref data, out var callback, out var callbackInfo)) {
			callback (ref callbackInfo);
		}
	}

	[MonoPInvokeCallback (typeof(OnRequestToJoinRejectedCallbackInternal))]
	internal static void OnRequestToJoinRejectedCallbackInternalImplementation (ref OnRequestToJoinRejectedCallbackInfoInternal data)
	{
		if (Helper.TryGetCallback<OnRequestToJoinRejectedCallbackInfoInternal, OnRequestToJoinRejectedCallback, OnRequestToJoinRejectedCallbackInfo> (ref data, out var callback, out var callbackInfo)) {
			callback (ref callbackInfo);
		}
	}

	[MonoPInvokeCallback (typeof(OnRequestToJoinResponseReceivedCallbackInternal))]
	internal static void OnRequestToJoinResponseReceivedCallbackInternalImplementation (ref RequestToJoinResponseReceivedCallbackInfoInternal data)
	{
		if (Helper.TryGetCallback<RequestToJoinResponseReceivedCallbackInfoInternal, OnRequestToJoinResponseReceivedCallback, RequestToJoinResponseReceivedCallbackInfo> (ref data, out var callback, out var callbackInfo)) {
			callback (ref callbackInfo);
		}
	}

	[MonoPInvokeCallback (typeof(OnSendCustomInviteCallbackInternal))]
	internal static void OnSendCustomInviteCallbackInternalImplementation (ref SendCustomInviteCallbackInfoInternal data)
	{
		if (Helper.TryGetAndRemoveCallback<SendCustomInviteCallbackInfoInternal, OnSendCustomInviteCallback, SendCustomInviteCallbackInfo> (ref data, out var callback, out var callbackInfo)) {
			callback (ref callbackInfo);
		}
	}

	[MonoPInvokeCallback (typeof(OnSendCustomNativeInviteRequestedCallbackInternal))]
	internal static void OnSendCustomNativeInviteRequestedCallbackInternalImplementation (ref SendCustomNativeInviteRequestedCallbackInfoInternal data)
	{
		if (Helper.TryGetCallback<SendCustomNativeInviteRequestedCallbackInfoInternal, OnSendCustomNativeInviteRequestedCallback, SendCustomNativeInviteRequestedCallbackInfo> (ref data, out var callback, out var callbackInfo)) {
			callback (ref callbackInfo);
		}
	}

	[MonoPInvokeCallback (typeof(OnSendRequestToJoinCallbackInternal))]
	internal static void OnSendRequestToJoinCallbackInternalImplementation (ref SendRequestToJoinCallbackInfoInternal data)
	{
		if (Helper.TryGetAndRemoveCallback<SendRequestToJoinCallbackInfoInternal, OnSendRequestToJoinCallback, SendRequestToJoinCallbackInfo> (ref data, out var callback, out var callbackInfo)) {
			callback (ref callbackInfo);
		}
	}
}
