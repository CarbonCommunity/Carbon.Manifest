using System;
using Epic.OnlineServices;
using Epic.OnlineServices.CustomInvites;

public sealed class CustomInvitesInterface : Handle
{
	public const int AddnotifycustominviteacceptedApiLatest = 1;

	public const int AddnotifycustominvitereceivedApiLatest = 1;

	public const int AddnotifycustominviterejectedApiLatest = 1;

	public const int FinalizeinviteApiLatest = 1;

	public const int MaxPayloadLength = 500;

	public const int SendcustominviteApiLatest = 1;

	public const int SetcustominviteApiLatest = 1;

	public CustomInvitesInterface ()
	{
	}

	public CustomInvitesInterface (IntPtr innerHandle)
		: base (innerHandle)
	{
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

	public Result FinalizeInvite (ref FinalizeInviteOptions options)
	{
		FinalizeInviteOptionsInternal options2 = default(FinalizeInviteOptionsInternal);
		options2.Set (ref options);
		Result result = Bindings.EOS_CustomInvites_FinalizeInvite (base.InnerHandle, ref options2);
		Helper.Dispose (ref options2);
		return result;
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

	public Result SetCustomInvite (ref SetCustomInviteOptions options)
	{
		SetCustomInviteOptionsInternal options2 = default(SetCustomInviteOptionsInternal);
		options2.Set (ref options);
		Result result = Bindings.EOS_CustomInvites_SetCustomInvite (base.InnerHandle, ref options2);
		Helper.Dispose (ref options2);
		return result;
	}

	[MonoPInvokeCallback (typeof(OnCustomInviteAcceptedCallbackInternal))]
	internal static void OnCustomInviteAcceptedCallbackInternalImplementation (ref OnCustomInviteAcceptedCallbackInfoInternal data)
	{
		if (Helper.TryGetAndRemoveCallback<OnCustomInviteAcceptedCallbackInfoInternal, OnCustomInviteAcceptedCallback, OnCustomInviteAcceptedCallbackInfo> (ref data, out var callback, out var callbackInfo)) {
			callback (ref callbackInfo);
		}
	}

	[MonoPInvokeCallback (typeof(OnCustomInviteReceivedCallbackInternal))]
	internal static void OnCustomInviteReceivedCallbackInternalImplementation (ref OnCustomInviteReceivedCallbackInfoInternal data)
	{
		if (Helper.TryGetAndRemoveCallback<OnCustomInviteReceivedCallbackInfoInternal, OnCustomInviteReceivedCallback, OnCustomInviteReceivedCallbackInfo> (ref data, out var callback, out var callbackInfo)) {
			callback (ref callbackInfo);
		}
	}

	[MonoPInvokeCallback (typeof(OnCustomInviteRejectedCallbackInternal))]
	internal static void OnCustomInviteRejectedCallbackInternalImplementation (ref CustomInviteRejectedCallbackInfoInternal data)
	{
		if (Helper.TryGetAndRemoveCallback<CustomInviteRejectedCallbackInfoInternal, OnCustomInviteRejectedCallback, CustomInviteRejectedCallbackInfo> (ref data, out var callback, out var callbackInfo)) {
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
}
