using System;
using Epic.OnlineServices;
using Epic.OnlineServices.RTC;
using Epic.OnlineServices.RTCAudio;

public sealed class RTCInterface : Handle
{
	public const int AddnotifydisconnectedApiLatest = 1;

	public const int AddnotifyparticipantstatuschangedApiLatest = 1;

	public const int BlockparticipantApiLatest = 1;

	public const int JoinroomApiLatest = 1;

	public const int LeaveroomApiLatest = 1;

	public const int ParticipantmetadataApiLatest = 1;

	public const int ParticipantmetadataKeyMaxcharcount = 256;

	public const int ParticipantmetadataValueMaxcharcount = 256;

	public const int SetroomsettingApiLatest = 1;

	public const int SetsettingApiLatest = 1;

	public RTCInterface ()
	{
	}

	public RTCInterface (IntPtr innerHandle)
		: base (innerHandle)
	{
	}

	public ulong AddNotifyDisconnected (ref AddNotifyDisconnectedOptions options, object clientData, OnDisconnectedCallback completionDelegate)
	{
		AddNotifyDisconnectedOptionsInternal options2 = default(AddNotifyDisconnectedOptionsInternal);
		options2.Set (ref options);
		IntPtr clientDataAddress = IntPtr.Zero;
		OnDisconnectedCallbackInternal onDisconnectedCallbackInternal = OnDisconnectedCallbackInternalImplementation;
		Helper.AddCallback (out clientDataAddress, clientData, completionDelegate, onDisconnectedCallbackInternal);
		ulong num = Bindings.EOS_RTC_AddNotifyDisconnected (base.InnerHandle, ref options2, clientDataAddress, onDisconnectedCallbackInternal);
		Helper.Dispose (ref options2);
		Helper.AssignNotificationIdToCallback (clientDataAddress, num);
		return num;
	}

	public ulong AddNotifyParticipantStatusChanged (ref AddNotifyParticipantStatusChangedOptions options, object clientData, OnParticipantStatusChangedCallback completionDelegate)
	{
		AddNotifyParticipantStatusChangedOptionsInternal options2 = default(AddNotifyParticipantStatusChangedOptionsInternal);
		options2.Set (ref options);
		IntPtr clientDataAddress = IntPtr.Zero;
		OnParticipantStatusChangedCallbackInternal onParticipantStatusChangedCallbackInternal = OnParticipantStatusChangedCallbackInternalImplementation;
		Helper.AddCallback (out clientDataAddress, clientData, completionDelegate, onParticipantStatusChangedCallbackInternal);
		ulong num = Bindings.EOS_RTC_AddNotifyParticipantStatusChanged (base.InnerHandle, ref options2, clientDataAddress, onParticipantStatusChangedCallbackInternal);
		Helper.Dispose (ref options2);
		Helper.AssignNotificationIdToCallback (clientDataAddress, num);
		return num;
	}

	public void BlockParticipant (ref BlockParticipantOptions options, object clientData, OnBlockParticipantCallback completionDelegate)
	{
		BlockParticipantOptionsInternal options2 = default(BlockParticipantOptionsInternal);
		options2.Set (ref options);
		IntPtr clientDataAddress = IntPtr.Zero;
		OnBlockParticipantCallbackInternal onBlockParticipantCallbackInternal = OnBlockParticipantCallbackInternalImplementation;
		Helper.AddCallback (out clientDataAddress, clientData, completionDelegate, onBlockParticipantCallbackInternal);
		Bindings.EOS_RTC_BlockParticipant (base.InnerHandle, ref options2, clientDataAddress, onBlockParticipantCallbackInternal);
		Helper.Dispose (ref options2);
	}

	public RTCAudioInterface GetAudioInterface ()
	{
		Helper.Get (Bindings.EOS_RTC_GetAudioInterface (base.InnerHandle), out RTCAudioInterface to);
		return to;
	}

	public void JoinRoom (ref JoinRoomOptions options, object clientData, OnJoinRoomCallback completionDelegate)
	{
		JoinRoomOptionsInternal options2 = default(JoinRoomOptionsInternal);
		options2.Set (ref options);
		IntPtr clientDataAddress = IntPtr.Zero;
		OnJoinRoomCallbackInternal onJoinRoomCallbackInternal = OnJoinRoomCallbackInternalImplementation;
		Helper.AddCallback (out clientDataAddress, clientData, completionDelegate, onJoinRoomCallbackInternal);
		Bindings.EOS_RTC_JoinRoom (base.InnerHandle, ref options2, clientDataAddress, onJoinRoomCallbackInternal);
		Helper.Dispose (ref options2);
	}

	public void LeaveRoom (ref LeaveRoomOptions options, object clientData, OnLeaveRoomCallback completionDelegate)
	{
		LeaveRoomOptionsInternal options2 = default(LeaveRoomOptionsInternal);
		options2.Set (ref options);
		IntPtr clientDataAddress = IntPtr.Zero;
		OnLeaveRoomCallbackInternal onLeaveRoomCallbackInternal = OnLeaveRoomCallbackInternalImplementation;
		Helper.AddCallback (out clientDataAddress, clientData, completionDelegate, onLeaveRoomCallbackInternal);
		Bindings.EOS_RTC_LeaveRoom (base.InnerHandle, ref options2, clientDataAddress, onLeaveRoomCallbackInternal);
		Helper.Dispose (ref options2);
	}

	public void RemoveNotifyDisconnected (ulong notificationId)
	{
		Bindings.EOS_RTC_RemoveNotifyDisconnected (base.InnerHandle, notificationId);
		Helper.RemoveCallbackByNotificationId (notificationId);
	}

	public void RemoveNotifyParticipantStatusChanged (ulong notificationId)
	{
		Bindings.EOS_RTC_RemoveNotifyParticipantStatusChanged (base.InnerHandle, notificationId);
		Helper.RemoveCallbackByNotificationId (notificationId);
	}

	public Result SetRoomSetting (ref SetRoomSettingOptions options)
	{
		SetRoomSettingOptionsInternal options2 = default(SetRoomSettingOptionsInternal);
		options2.Set (ref options);
		Result result = Bindings.EOS_RTC_SetRoomSetting (base.InnerHandle, ref options2);
		Helper.Dispose (ref options2);
		return result;
	}

	public Result SetSetting (ref SetSettingOptions options)
	{
		SetSettingOptionsInternal options2 = default(SetSettingOptionsInternal);
		options2.Set (ref options);
		Result result = Bindings.EOS_RTC_SetSetting (base.InnerHandle, ref options2);
		Helper.Dispose (ref options2);
		return result;
	}

	[MonoPInvokeCallback (typeof(OnBlockParticipantCallbackInternal))]
	internal static void OnBlockParticipantCallbackInternalImplementation (ref BlockParticipantCallbackInfoInternal data)
	{
		if (Helper.TryGetAndRemoveCallback<BlockParticipantCallbackInfoInternal, OnBlockParticipantCallback, BlockParticipantCallbackInfo> (ref data, out var callback, out var callbackInfo)) {
			callback (ref callbackInfo);
		}
	}

	[MonoPInvokeCallback (typeof(OnDisconnectedCallbackInternal))]
	internal static void OnDisconnectedCallbackInternalImplementation (ref DisconnectedCallbackInfoInternal data)
	{
		if (Helper.TryGetAndRemoveCallback<DisconnectedCallbackInfoInternal, OnDisconnectedCallback, DisconnectedCallbackInfo> (ref data, out var callback, out var callbackInfo)) {
			callback (ref callbackInfo);
		}
	}

	[MonoPInvokeCallback (typeof(OnJoinRoomCallbackInternal))]
	internal static void OnJoinRoomCallbackInternalImplementation (ref JoinRoomCallbackInfoInternal data)
	{
		if (Helper.TryGetAndRemoveCallback<JoinRoomCallbackInfoInternal, OnJoinRoomCallback, JoinRoomCallbackInfo> (ref data, out var callback, out var callbackInfo)) {
			callback (ref callbackInfo);
		}
	}

	[MonoPInvokeCallback (typeof(OnLeaveRoomCallbackInternal))]
	internal static void OnLeaveRoomCallbackInternalImplementation (ref LeaveRoomCallbackInfoInternal data)
	{
		if (Helper.TryGetAndRemoveCallback<LeaveRoomCallbackInfoInternal, OnLeaveRoomCallback, LeaveRoomCallbackInfo> (ref data, out var callback, out var callbackInfo)) {
			callback (ref callbackInfo);
		}
	}

	[MonoPInvokeCallback (typeof(OnParticipantStatusChangedCallbackInternal))]
	internal static void OnParticipantStatusChangedCallbackInternalImplementation (ref ParticipantStatusChangedCallbackInfoInternal data)
	{
		if (Helper.TryGetAndRemoveCallback<ParticipantStatusChangedCallbackInfoInternal, OnParticipantStatusChangedCallback, ParticipantStatusChangedCallbackInfo> (ref data, out var callback, out var callbackInfo)) {
			callback (ref callbackInfo);
		}
	}
}
