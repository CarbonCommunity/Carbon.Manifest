using System;
using Epic.OnlineServices;
using Epic.OnlineServices.Presence;

public sealed class PresenceInterface : Handle
{
	public const int AddnotifyjoingameacceptedApiLatest = 2;

	public const int AddnotifyonpresencechangedApiLatest = 1;

	public const int CopypresenceApiLatest = 3;

	public const int CreatepresencemodificationApiLatest = 1;

	public const int DataMaxKeyLength = 64;

	public const int DataMaxKeys = 32;

	public const int DataMaxValueLength = 255;

	public const int DatarecordApiLatest = 1;

	public const int DeletedataApiLatest = 1;

	public const int GetjoininfoApiLatest = 1;

	public const int HaspresenceApiLatest = 1;

	public const int InfoApiLatest = 3;

	public static readonly Utf8String KeyPlatformPresence = "EOS_PlatformPresence";

	public const int QuerypresenceApiLatest = 1;

	public const int RichTextMaxValueLength = 255;

	public const int SetdataApiLatest = 1;

	public const int SetpresenceApiLatest = 1;

	public const int SetrawrichtextApiLatest = 1;

	public const int SetstatusApiLatest = 1;

	public PresenceInterface ()
	{
	}

	public PresenceInterface (IntPtr innerHandle)
		: base (innerHandle)
	{
	}

	public ulong AddNotifyJoinGameAccepted (ref AddNotifyJoinGameAcceptedOptions options, object clientData, OnJoinGameAcceptedCallback notificationFn)
	{
		AddNotifyJoinGameAcceptedOptionsInternal options2 = default(AddNotifyJoinGameAcceptedOptionsInternal);
		options2.Set (ref options);
		IntPtr clientDataAddress = IntPtr.Zero;
		OnJoinGameAcceptedCallbackInternal onJoinGameAcceptedCallbackInternal = OnJoinGameAcceptedCallbackInternalImplementation;
		Helper.AddCallback (out clientDataAddress, clientData, notificationFn, onJoinGameAcceptedCallbackInternal);
		ulong num = Bindings.EOS_Presence_AddNotifyJoinGameAccepted (base.InnerHandle, ref options2, clientDataAddress, onJoinGameAcceptedCallbackInternal);
		Helper.Dispose (ref options2);
		Helper.AssignNotificationIdToCallback (clientDataAddress, num);
		return num;
	}

	public ulong AddNotifyOnPresenceChanged (ref AddNotifyOnPresenceChangedOptions options, object clientData, OnPresenceChangedCallback notificationHandler)
	{
		AddNotifyOnPresenceChangedOptionsInternal options2 = default(AddNotifyOnPresenceChangedOptionsInternal);
		options2.Set (ref options);
		IntPtr clientDataAddress = IntPtr.Zero;
		OnPresenceChangedCallbackInternal onPresenceChangedCallbackInternal = OnPresenceChangedCallbackInternalImplementation;
		Helper.AddCallback (out clientDataAddress, clientData, notificationHandler, onPresenceChangedCallbackInternal);
		ulong num = Bindings.EOS_Presence_AddNotifyOnPresenceChanged (base.InnerHandle, ref options2, clientDataAddress, onPresenceChangedCallbackInternal);
		Helper.Dispose (ref options2);
		Helper.AssignNotificationIdToCallback (clientDataAddress, num);
		return num;
	}

	public Result CopyPresence (ref CopyPresenceOptions options, out Info? outPresence)
	{
		CopyPresenceOptionsInternal options2 = default(CopyPresenceOptionsInternal);
		options2.Set (ref options);
		IntPtr outPresence2 = IntPtr.Zero;
		Result result = Bindings.EOS_Presence_CopyPresence (base.InnerHandle, ref options2, ref outPresence2);
		Helper.Dispose (ref options2);
		Helper.Get<InfoInternal, Info> (outPresence2, out outPresence);
		if (outPresence.HasValue) {
			Bindings.EOS_Presence_Info_Release (outPresence2);
		}
		return result;
	}

	public Result CreatePresenceModification (ref CreatePresenceModificationOptions options, out PresenceModification outPresenceModificationHandle)
	{
		CreatePresenceModificationOptionsInternal options2 = default(CreatePresenceModificationOptionsInternal);
		options2.Set (ref options);
		IntPtr outPresenceModificationHandle2 = IntPtr.Zero;
		Result result = Bindings.EOS_Presence_CreatePresenceModification (base.InnerHandle, ref options2, ref outPresenceModificationHandle2);
		Helper.Dispose (ref options2);
		Helper.Get (outPresenceModificationHandle2, out outPresenceModificationHandle);
		return result;
	}

	public Result GetJoinInfo (ref GetJoinInfoOptions options, out Utf8String outBuffer)
	{
		GetJoinInfoOptionsInternal options2 = default(GetJoinInfoOptionsInternal);
		options2.Set (ref options);
		int inOutBufferLength = 256;
		IntPtr value = Helper.AddAllocation (inOutBufferLength);
		Result result = Bindings.EOS_Presence_GetJoinInfo (base.InnerHandle, ref options2, value, ref inOutBufferLength);
		Helper.Dispose (ref options2);
		Helper.Get (value, out outBuffer);
		Helper.Dispose (ref value);
		return result;
	}

	public bool HasPresence (ref HasPresenceOptions options)
	{
		HasPresenceOptionsInternal options2 = default(HasPresenceOptionsInternal);
		options2.Set (ref options);
		int from = Bindings.EOS_Presence_HasPresence (base.InnerHandle, ref options2);
		Helper.Dispose (ref options2);
		Helper.Get (from, out var to);
		return to;
	}

	public void QueryPresence (ref QueryPresenceOptions options, object clientData, OnQueryPresenceCompleteCallback completionDelegate)
	{
		QueryPresenceOptionsInternal options2 = default(QueryPresenceOptionsInternal);
		options2.Set (ref options);
		IntPtr clientDataAddress = IntPtr.Zero;
		OnQueryPresenceCompleteCallbackInternal onQueryPresenceCompleteCallbackInternal = OnQueryPresenceCompleteCallbackInternalImplementation;
		Helper.AddCallback (out clientDataAddress, clientData, completionDelegate, onQueryPresenceCompleteCallbackInternal);
		Bindings.EOS_Presence_QueryPresence (base.InnerHandle, ref options2, clientDataAddress, onQueryPresenceCompleteCallbackInternal);
		Helper.Dispose (ref options2);
	}

	public void RemoveNotifyJoinGameAccepted (ulong inId)
	{
		Bindings.EOS_Presence_RemoveNotifyJoinGameAccepted (base.InnerHandle, inId);
		Helper.RemoveCallbackByNotificationId (inId);
	}

	public void RemoveNotifyOnPresenceChanged (ulong notificationId)
	{
		Bindings.EOS_Presence_RemoveNotifyOnPresenceChanged (base.InnerHandle, notificationId);
		Helper.RemoveCallbackByNotificationId (notificationId);
	}

	public void SetPresence (ref SetPresenceOptions options, object clientData, SetPresenceCompleteCallback completionDelegate)
	{
		SetPresenceOptionsInternal options2 = default(SetPresenceOptionsInternal);
		options2.Set (ref options);
		IntPtr clientDataAddress = IntPtr.Zero;
		SetPresenceCompleteCallbackInternal setPresenceCompleteCallbackInternal = SetPresenceCompleteCallbackInternalImplementation;
		Helper.AddCallback (out clientDataAddress, clientData, completionDelegate, setPresenceCompleteCallbackInternal);
		Bindings.EOS_Presence_SetPresence (base.InnerHandle, ref options2, clientDataAddress, setPresenceCompleteCallbackInternal);
		Helper.Dispose (ref options2);
	}

	[MonoPInvokeCallback (typeof(OnJoinGameAcceptedCallbackInternal))]
	internal static void OnJoinGameAcceptedCallbackInternalImplementation (ref JoinGameAcceptedCallbackInfoInternal data)
	{
		if (Helper.TryGetAndRemoveCallback<JoinGameAcceptedCallbackInfoInternal, OnJoinGameAcceptedCallback, JoinGameAcceptedCallbackInfo> (ref data, out var callback, out var callbackInfo)) {
			callback (ref callbackInfo);
		}
	}

	[MonoPInvokeCallback (typeof(OnPresenceChangedCallbackInternal))]
	internal static void OnPresenceChangedCallbackInternalImplementation (ref PresenceChangedCallbackInfoInternal data)
	{
		if (Helper.TryGetAndRemoveCallback<PresenceChangedCallbackInfoInternal, OnPresenceChangedCallback, PresenceChangedCallbackInfo> (ref data, out var callback, out var callbackInfo)) {
			callback (ref callbackInfo);
		}
	}

	[MonoPInvokeCallback (typeof(OnQueryPresenceCompleteCallbackInternal))]
	internal static void OnQueryPresenceCompleteCallbackInternalImplementation (ref QueryPresenceCallbackInfoInternal data)
	{
		if (Helper.TryGetAndRemoveCallback<QueryPresenceCallbackInfoInternal, OnQueryPresenceCompleteCallback, QueryPresenceCallbackInfo> (ref data, out var callback, out var callbackInfo)) {
			callback (ref callbackInfo);
		}
	}

	[MonoPInvokeCallback (typeof(SetPresenceCompleteCallbackInternal))]
	internal static void SetPresenceCompleteCallbackInternalImplementation (ref SetPresenceCallbackInfoInternal data)
	{
		if (Helper.TryGetAndRemoveCallback<SetPresenceCallbackInfoInternal, SetPresenceCompleteCallback, SetPresenceCallbackInfo> (ref data, out var callback, out var callbackInfo)) {
			callback (ref callbackInfo);
		}
	}
}
