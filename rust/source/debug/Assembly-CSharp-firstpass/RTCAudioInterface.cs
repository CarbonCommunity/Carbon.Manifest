using System;
using Epic.OnlineServices;
using Epic.OnlineServices.RTCAudio;

public sealed class RTCAudioInterface : Handle
{
	public const int AddnotifyaudiobeforerenderApiLatest = 1;

	public const int AddnotifyaudiobeforesendApiLatest = 1;

	public const int AddnotifyaudiodeviceschangedApiLatest = 1;

	public const int AddnotifyaudioinputstateApiLatest = 1;

	public const int AddnotifyaudiooutputstateApiLatest = 1;

	public const int AddnotifyparticipantupdatedApiLatest = 1;

	public const int AudiobufferApiLatest = 1;

	public const int AudioinputdeviceinfoApiLatest = 1;

	public const int AudiooutputdeviceinfoApiLatest = 1;

	public const int GetaudioinputdevicebyindexApiLatest = 1;

	public const int GetaudioinputdevicescountApiLatest = 1;

	public const int GetaudiooutputdevicebyindexApiLatest = 1;

	public const int GetaudiooutputdevicescountApiLatest = 1;

	public const int RegisterplatformaudiouserApiLatest = 1;

	public const int SendaudioApiLatest = 1;

	public const int SetaudioinputsettingsApiLatest = 1;

	public const int SetaudiooutputsettingsApiLatest = 1;

	public const int UnregisterplatformaudiouserApiLatest = 1;

	public const int UpdateparticipantvolumeApiLatest = 1;

	public const int UpdatereceivingApiLatest = 1;

	public const int UpdatereceivingvolumeApiLatest = 1;

	public const int UpdatesendingApiLatest = 1;

	public const int UpdatesendingvolumeApiLatest = 1;

	public RTCAudioInterface ()
	{
	}

	public RTCAudioInterface (IntPtr innerHandle)
		: base (innerHandle)
	{
	}

	public ulong AddNotifyAudioBeforeRender (ref AddNotifyAudioBeforeRenderOptions options, object clientData, OnAudioBeforeRenderCallback completionDelegate)
	{
		AddNotifyAudioBeforeRenderOptionsInternal options2 = default(AddNotifyAudioBeforeRenderOptionsInternal);
		options2.Set (ref options);
		IntPtr clientDataAddress = IntPtr.Zero;
		OnAudioBeforeRenderCallbackInternal onAudioBeforeRenderCallbackInternal = OnAudioBeforeRenderCallbackInternalImplementation;
		Helper.AddCallback (out clientDataAddress, clientData, completionDelegate, onAudioBeforeRenderCallbackInternal);
		ulong num = Bindings.EOS_RTCAudio_AddNotifyAudioBeforeRender (base.InnerHandle, ref options2, clientDataAddress, onAudioBeforeRenderCallbackInternal);
		Helper.Dispose (ref options2);
		Helper.AssignNotificationIdToCallback (clientDataAddress, num);
		return num;
	}

	public ulong AddNotifyAudioBeforeSend (ref AddNotifyAudioBeforeSendOptions options, object clientData, OnAudioBeforeSendCallback completionDelegate)
	{
		AddNotifyAudioBeforeSendOptionsInternal options2 = default(AddNotifyAudioBeforeSendOptionsInternal);
		options2.Set (ref options);
		IntPtr clientDataAddress = IntPtr.Zero;
		OnAudioBeforeSendCallbackInternal onAudioBeforeSendCallbackInternal = OnAudioBeforeSendCallbackInternalImplementation;
		Helper.AddCallback (out clientDataAddress, clientData, completionDelegate, onAudioBeforeSendCallbackInternal);
		ulong num = Bindings.EOS_RTCAudio_AddNotifyAudioBeforeSend (base.InnerHandle, ref options2, clientDataAddress, onAudioBeforeSendCallbackInternal);
		Helper.Dispose (ref options2);
		Helper.AssignNotificationIdToCallback (clientDataAddress, num);
		return num;
	}

	public ulong AddNotifyAudioDevicesChanged (ref AddNotifyAudioDevicesChangedOptions options, object clientData, OnAudioDevicesChangedCallback completionDelegate)
	{
		AddNotifyAudioDevicesChangedOptionsInternal options2 = default(AddNotifyAudioDevicesChangedOptionsInternal);
		options2.Set (ref options);
		IntPtr clientDataAddress = IntPtr.Zero;
		OnAudioDevicesChangedCallbackInternal onAudioDevicesChangedCallbackInternal = OnAudioDevicesChangedCallbackInternalImplementation;
		Helper.AddCallback (out clientDataAddress, clientData, completionDelegate, onAudioDevicesChangedCallbackInternal);
		ulong num = Bindings.EOS_RTCAudio_AddNotifyAudioDevicesChanged (base.InnerHandle, ref options2, clientDataAddress, onAudioDevicesChangedCallbackInternal);
		Helper.Dispose (ref options2);
		Helper.AssignNotificationIdToCallback (clientDataAddress, num);
		return num;
	}

	public ulong AddNotifyAudioInputState (ref AddNotifyAudioInputStateOptions options, object clientData, OnAudioInputStateCallback completionDelegate)
	{
		AddNotifyAudioInputStateOptionsInternal options2 = default(AddNotifyAudioInputStateOptionsInternal);
		options2.Set (ref options);
		IntPtr clientDataAddress = IntPtr.Zero;
		OnAudioInputStateCallbackInternal onAudioInputStateCallbackInternal = OnAudioInputStateCallbackInternalImplementation;
		Helper.AddCallback (out clientDataAddress, clientData, completionDelegate, onAudioInputStateCallbackInternal);
		ulong num = Bindings.EOS_RTCAudio_AddNotifyAudioInputState (base.InnerHandle, ref options2, clientDataAddress, onAudioInputStateCallbackInternal);
		Helper.Dispose (ref options2);
		Helper.AssignNotificationIdToCallback (clientDataAddress, num);
		return num;
	}

	public ulong AddNotifyAudioOutputState (ref AddNotifyAudioOutputStateOptions options, object clientData, OnAudioOutputStateCallback completionDelegate)
	{
		AddNotifyAudioOutputStateOptionsInternal options2 = default(AddNotifyAudioOutputStateOptionsInternal);
		options2.Set (ref options);
		IntPtr clientDataAddress = IntPtr.Zero;
		OnAudioOutputStateCallbackInternal onAudioOutputStateCallbackInternal = OnAudioOutputStateCallbackInternalImplementation;
		Helper.AddCallback (out clientDataAddress, clientData, completionDelegate, onAudioOutputStateCallbackInternal);
		ulong num = Bindings.EOS_RTCAudio_AddNotifyAudioOutputState (base.InnerHandle, ref options2, clientDataAddress, onAudioOutputStateCallbackInternal);
		Helper.Dispose (ref options2);
		Helper.AssignNotificationIdToCallback (clientDataAddress, num);
		return num;
	}

	public ulong AddNotifyParticipantUpdated (ref AddNotifyParticipantUpdatedOptions options, object clientData, OnParticipantUpdatedCallback completionDelegate)
	{
		AddNotifyParticipantUpdatedOptionsInternal options2 = default(AddNotifyParticipantUpdatedOptionsInternal);
		options2.Set (ref options);
		IntPtr clientDataAddress = IntPtr.Zero;
		OnParticipantUpdatedCallbackInternal onParticipantUpdatedCallbackInternal = OnParticipantUpdatedCallbackInternalImplementation;
		Helper.AddCallback (out clientDataAddress, clientData, completionDelegate, onParticipantUpdatedCallbackInternal);
		ulong num = Bindings.EOS_RTCAudio_AddNotifyParticipantUpdated (base.InnerHandle, ref options2, clientDataAddress, onParticipantUpdatedCallbackInternal);
		Helper.Dispose (ref options2);
		Helper.AssignNotificationIdToCallback (clientDataAddress, num);
		return num;
	}

	public AudioInputDeviceInfo? GetAudioInputDeviceByIndex (ref GetAudioInputDeviceByIndexOptions options)
	{
		GetAudioInputDeviceByIndexOptionsInternal options2 = default(GetAudioInputDeviceByIndexOptionsInternal);
		options2.Set (ref options);
		IntPtr from = Bindings.EOS_RTCAudio_GetAudioInputDeviceByIndex (base.InnerHandle, ref options2);
		Helper.Dispose (ref options2);
		Helper.Get<AudioInputDeviceInfoInternal, AudioInputDeviceInfo> (from, out AudioInputDeviceInfo? to);
		return to;
	}

	public uint GetAudioInputDevicesCount (ref GetAudioInputDevicesCountOptions options)
	{
		GetAudioInputDevicesCountOptionsInternal options2 = default(GetAudioInputDevicesCountOptionsInternal);
		options2.Set (ref options);
		uint result = Bindings.EOS_RTCAudio_GetAudioInputDevicesCount (base.InnerHandle, ref options2);
		Helper.Dispose (ref options2);
		return result;
	}

	public AudioOutputDeviceInfo? GetAudioOutputDeviceByIndex (ref GetAudioOutputDeviceByIndexOptions options)
	{
		GetAudioOutputDeviceByIndexOptionsInternal options2 = default(GetAudioOutputDeviceByIndexOptionsInternal);
		options2.Set (ref options);
		IntPtr from = Bindings.EOS_RTCAudio_GetAudioOutputDeviceByIndex (base.InnerHandle, ref options2);
		Helper.Dispose (ref options2);
		Helper.Get<AudioOutputDeviceInfoInternal, AudioOutputDeviceInfo> (from, out AudioOutputDeviceInfo? to);
		return to;
	}

	public uint GetAudioOutputDevicesCount (ref GetAudioOutputDevicesCountOptions options)
	{
		GetAudioOutputDevicesCountOptionsInternal options2 = default(GetAudioOutputDevicesCountOptionsInternal);
		options2.Set (ref options);
		uint result = Bindings.EOS_RTCAudio_GetAudioOutputDevicesCount (base.InnerHandle, ref options2);
		Helper.Dispose (ref options2);
		return result;
	}

	public Result RegisterPlatformAudioUser (ref RegisterPlatformAudioUserOptions options)
	{
		RegisterPlatformAudioUserOptionsInternal options2 = default(RegisterPlatformAudioUserOptionsInternal);
		options2.Set (ref options);
		Result result = Bindings.EOS_RTCAudio_RegisterPlatformAudioUser (base.InnerHandle, ref options2);
		Helper.Dispose (ref options2);
		return result;
	}

	public void RemoveNotifyAudioBeforeRender (ulong notificationId)
	{
		Bindings.EOS_RTCAudio_RemoveNotifyAudioBeforeRender (base.InnerHandle, notificationId);
		Helper.RemoveCallbackByNotificationId (notificationId);
	}

	public void RemoveNotifyAudioBeforeSend (ulong notificationId)
	{
		Bindings.EOS_RTCAudio_RemoveNotifyAudioBeforeSend (base.InnerHandle, notificationId);
		Helper.RemoveCallbackByNotificationId (notificationId);
	}

	public void RemoveNotifyAudioDevicesChanged (ulong notificationId)
	{
		Bindings.EOS_RTCAudio_RemoveNotifyAudioDevicesChanged (base.InnerHandle, notificationId);
		Helper.RemoveCallbackByNotificationId (notificationId);
	}

	public void RemoveNotifyAudioInputState (ulong notificationId)
	{
		Bindings.EOS_RTCAudio_RemoveNotifyAudioInputState (base.InnerHandle, notificationId);
		Helper.RemoveCallbackByNotificationId (notificationId);
	}

	public void RemoveNotifyAudioOutputState (ulong notificationId)
	{
		Bindings.EOS_RTCAudio_RemoveNotifyAudioOutputState (base.InnerHandle, notificationId);
		Helper.RemoveCallbackByNotificationId (notificationId);
	}

	public void RemoveNotifyParticipantUpdated (ulong notificationId)
	{
		Bindings.EOS_RTCAudio_RemoveNotifyParticipantUpdated (base.InnerHandle, notificationId);
		Helper.RemoveCallbackByNotificationId (notificationId);
	}

	public Result SendAudio (ref SendAudioOptions options)
	{
		SendAudioOptionsInternal options2 = default(SendAudioOptionsInternal);
		options2.Set (ref options);
		Result result = Bindings.EOS_RTCAudio_SendAudio (base.InnerHandle, ref options2);
		Helper.Dispose (ref options2);
		return result;
	}

	public Result SetAudioInputSettings (ref SetAudioInputSettingsOptions options)
	{
		SetAudioInputSettingsOptionsInternal options2 = default(SetAudioInputSettingsOptionsInternal);
		options2.Set (ref options);
		Result result = Bindings.EOS_RTCAudio_SetAudioInputSettings (base.InnerHandle, ref options2);
		Helper.Dispose (ref options2);
		return result;
	}

	public Result SetAudioOutputSettings (ref SetAudioOutputSettingsOptions options)
	{
		SetAudioOutputSettingsOptionsInternal options2 = default(SetAudioOutputSettingsOptionsInternal);
		options2.Set (ref options);
		Result result = Bindings.EOS_RTCAudio_SetAudioOutputSettings (base.InnerHandle, ref options2);
		Helper.Dispose (ref options2);
		return result;
	}

	public Result UnregisterPlatformAudioUser (ref UnregisterPlatformAudioUserOptions options)
	{
		UnregisterPlatformAudioUserOptionsInternal options2 = default(UnregisterPlatformAudioUserOptionsInternal);
		options2.Set (ref options);
		Result result = Bindings.EOS_RTCAudio_UnregisterPlatformAudioUser (base.InnerHandle, ref options2);
		Helper.Dispose (ref options2);
		return result;
	}

	public void UpdateParticipantVolume (ref UpdateParticipantVolumeOptions options, object clientData, OnUpdateParticipantVolumeCallback completionDelegate)
	{
		UpdateParticipantVolumeOptionsInternal options2 = default(UpdateParticipantVolumeOptionsInternal);
		options2.Set (ref options);
		IntPtr clientDataAddress = IntPtr.Zero;
		OnUpdateParticipantVolumeCallbackInternal onUpdateParticipantVolumeCallbackInternal = OnUpdateParticipantVolumeCallbackInternalImplementation;
		Helper.AddCallback (out clientDataAddress, clientData, completionDelegate, onUpdateParticipantVolumeCallbackInternal);
		Bindings.EOS_RTCAudio_UpdateParticipantVolume (base.InnerHandle, ref options2, clientDataAddress, onUpdateParticipantVolumeCallbackInternal);
		Helper.Dispose (ref options2);
	}

	public void UpdateReceiving (ref UpdateReceivingOptions options, object clientData, OnUpdateReceivingCallback completionDelegate)
	{
		UpdateReceivingOptionsInternal options2 = default(UpdateReceivingOptionsInternal);
		options2.Set (ref options);
		IntPtr clientDataAddress = IntPtr.Zero;
		OnUpdateReceivingCallbackInternal onUpdateReceivingCallbackInternal = OnUpdateReceivingCallbackInternalImplementation;
		Helper.AddCallback (out clientDataAddress, clientData, completionDelegate, onUpdateReceivingCallbackInternal);
		Bindings.EOS_RTCAudio_UpdateReceiving (base.InnerHandle, ref options2, clientDataAddress, onUpdateReceivingCallbackInternal);
		Helper.Dispose (ref options2);
	}

	public void UpdateReceivingVolume (ref UpdateReceivingVolumeOptions options, object clientData, OnUpdateReceivingVolumeCallback completionDelegate)
	{
		UpdateReceivingVolumeOptionsInternal options2 = default(UpdateReceivingVolumeOptionsInternal);
		options2.Set (ref options);
		IntPtr clientDataAddress = IntPtr.Zero;
		OnUpdateReceivingVolumeCallbackInternal onUpdateReceivingVolumeCallbackInternal = OnUpdateReceivingVolumeCallbackInternalImplementation;
		Helper.AddCallback (out clientDataAddress, clientData, completionDelegate, onUpdateReceivingVolumeCallbackInternal);
		Bindings.EOS_RTCAudio_UpdateReceivingVolume (base.InnerHandle, ref options2, clientDataAddress, onUpdateReceivingVolumeCallbackInternal);
		Helper.Dispose (ref options2);
	}

	public void UpdateSending (ref UpdateSendingOptions options, object clientData, OnUpdateSendingCallback completionDelegate)
	{
		UpdateSendingOptionsInternal options2 = default(UpdateSendingOptionsInternal);
		options2.Set (ref options);
		IntPtr clientDataAddress = IntPtr.Zero;
		OnUpdateSendingCallbackInternal onUpdateSendingCallbackInternal = OnUpdateSendingCallbackInternalImplementation;
		Helper.AddCallback (out clientDataAddress, clientData, completionDelegate, onUpdateSendingCallbackInternal);
		Bindings.EOS_RTCAudio_UpdateSending (base.InnerHandle, ref options2, clientDataAddress, onUpdateSendingCallbackInternal);
		Helper.Dispose (ref options2);
	}

	public void UpdateSendingVolume (ref UpdateSendingVolumeOptions options, object clientData, OnUpdateSendingVolumeCallback completionDelegate)
	{
		UpdateSendingVolumeOptionsInternal options2 = default(UpdateSendingVolumeOptionsInternal);
		options2.Set (ref options);
		IntPtr clientDataAddress = IntPtr.Zero;
		OnUpdateSendingVolumeCallbackInternal onUpdateSendingVolumeCallbackInternal = OnUpdateSendingVolumeCallbackInternalImplementation;
		Helper.AddCallback (out clientDataAddress, clientData, completionDelegate, onUpdateSendingVolumeCallbackInternal);
		Bindings.EOS_RTCAudio_UpdateSendingVolume (base.InnerHandle, ref options2, clientDataAddress, onUpdateSendingVolumeCallbackInternal);
		Helper.Dispose (ref options2);
	}

	[MonoPInvokeCallback (typeof(OnAudioBeforeRenderCallbackInternal))]
	internal static void OnAudioBeforeRenderCallbackInternalImplementation (ref AudioBeforeRenderCallbackInfoInternal data)
	{
		if (Helper.TryGetAndRemoveCallback<AudioBeforeRenderCallbackInfoInternal, OnAudioBeforeRenderCallback, AudioBeforeRenderCallbackInfo> (ref data, out var callback, out var callbackInfo)) {
			callback (ref callbackInfo);
		}
	}

	[MonoPInvokeCallback (typeof(OnAudioBeforeSendCallbackInternal))]
	internal static void OnAudioBeforeSendCallbackInternalImplementation (ref AudioBeforeSendCallbackInfoInternal data)
	{
		if (Helper.TryGetAndRemoveCallback<AudioBeforeSendCallbackInfoInternal, OnAudioBeforeSendCallback, AudioBeforeSendCallbackInfo> (ref data, out var callback, out var callbackInfo)) {
			callback (ref callbackInfo);
		}
	}

	[MonoPInvokeCallback (typeof(OnAudioDevicesChangedCallbackInternal))]
	internal static void OnAudioDevicesChangedCallbackInternalImplementation (ref AudioDevicesChangedCallbackInfoInternal data)
	{
		if (Helper.TryGetAndRemoveCallback<AudioDevicesChangedCallbackInfoInternal, OnAudioDevicesChangedCallback, AudioDevicesChangedCallbackInfo> (ref data, out var callback, out var callbackInfo)) {
			callback (ref callbackInfo);
		}
	}

	[MonoPInvokeCallback (typeof(OnAudioInputStateCallbackInternal))]
	internal static void OnAudioInputStateCallbackInternalImplementation (ref AudioInputStateCallbackInfoInternal data)
	{
		if (Helper.TryGetAndRemoveCallback<AudioInputStateCallbackInfoInternal, OnAudioInputStateCallback, AudioInputStateCallbackInfo> (ref data, out var callback, out var callbackInfo)) {
			callback (ref callbackInfo);
		}
	}

	[MonoPInvokeCallback (typeof(OnAudioOutputStateCallbackInternal))]
	internal static void OnAudioOutputStateCallbackInternalImplementation (ref AudioOutputStateCallbackInfoInternal data)
	{
		if (Helper.TryGetAndRemoveCallback<AudioOutputStateCallbackInfoInternal, OnAudioOutputStateCallback, AudioOutputStateCallbackInfo> (ref data, out var callback, out var callbackInfo)) {
			callback (ref callbackInfo);
		}
	}

	[MonoPInvokeCallback (typeof(OnParticipantUpdatedCallbackInternal))]
	internal static void OnParticipantUpdatedCallbackInternalImplementation (ref ParticipantUpdatedCallbackInfoInternal data)
	{
		if (Helper.TryGetAndRemoveCallback<ParticipantUpdatedCallbackInfoInternal, OnParticipantUpdatedCallback, ParticipantUpdatedCallbackInfo> (ref data, out var callback, out var callbackInfo)) {
			callback (ref callbackInfo);
		}
	}

	[MonoPInvokeCallback (typeof(OnUpdateParticipantVolumeCallbackInternal))]
	internal static void OnUpdateParticipantVolumeCallbackInternalImplementation (ref UpdateParticipantVolumeCallbackInfoInternal data)
	{
		if (Helper.TryGetAndRemoveCallback<UpdateParticipantVolumeCallbackInfoInternal, OnUpdateParticipantVolumeCallback, UpdateParticipantVolumeCallbackInfo> (ref data, out var callback, out var callbackInfo)) {
			callback (ref callbackInfo);
		}
	}

	[MonoPInvokeCallback (typeof(OnUpdateReceivingCallbackInternal))]
	internal static void OnUpdateReceivingCallbackInternalImplementation (ref UpdateReceivingCallbackInfoInternal data)
	{
		if (Helper.TryGetAndRemoveCallback<UpdateReceivingCallbackInfoInternal, OnUpdateReceivingCallback, UpdateReceivingCallbackInfo> (ref data, out var callback, out var callbackInfo)) {
			callback (ref callbackInfo);
		}
	}

	[MonoPInvokeCallback (typeof(OnUpdateReceivingVolumeCallbackInternal))]
	internal static void OnUpdateReceivingVolumeCallbackInternalImplementation (ref UpdateReceivingVolumeCallbackInfoInternal data)
	{
		if (Helper.TryGetAndRemoveCallback<UpdateReceivingVolumeCallbackInfoInternal, OnUpdateReceivingVolumeCallback, UpdateReceivingVolumeCallbackInfo> (ref data, out var callback, out var callbackInfo)) {
			callback (ref callbackInfo);
		}
	}

	[MonoPInvokeCallback (typeof(OnUpdateSendingCallbackInternal))]
	internal static void OnUpdateSendingCallbackInternalImplementation (ref UpdateSendingCallbackInfoInternal data)
	{
		if (Helper.TryGetAndRemoveCallback<UpdateSendingCallbackInfoInternal, OnUpdateSendingCallback, UpdateSendingCallbackInfo> (ref data, out var callback, out var callbackInfo)) {
			callback (ref callbackInfo);
		}
	}

	[MonoPInvokeCallback (typeof(OnUpdateSendingVolumeCallbackInternal))]
	internal static void OnUpdateSendingVolumeCallbackInternalImplementation (ref UpdateSendingVolumeCallbackInfoInternal data)
	{
		if (Helper.TryGetAndRemoveCallback<UpdateSendingVolumeCallbackInfoInternal, OnUpdateSendingVolumeCallback, UpdateSendingVolumeCallbackInfo> (ref data, out var callback, out var callbackInfo)) {
			callback (ref callbackInfo);
		}
	}
}
