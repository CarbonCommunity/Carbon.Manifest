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

	public const int CopyinputdeviceinformationbyindexApiLatest = 1;

	public const int CopyoutputdeviceinformationbyindexApiLatest = 1;

	public const int GetaudioinputdevicebyindexApiLatest = 1;

	public const int GetaudioinputdevicescountApiLatest = 1;

	public const int GetaudiooutputdevicebyindexApiLatest = 1;

	public const int GetaudiooutputdevicescountApiLatest = 1;

	public const int GetinputdevicescountApiLatest = 1;

	public const int GetoutputdevicescountApiLatest = 1;

	public const int InputdeviceinformationApiLatest = 1;

	public const int OutputdeviceinformationApiLatest = 1;

	public const int QueryinputdevicesinformationApiLatest = 1;

	public const int QueryoutputdevicesinformationApiLatest = 1;

	public const int RegisterplatformaudiouserApiLatest = 1;

	public const int RegisterplatformuserApiLatest = 1;

	public const int SendaudioApiLatest = 1;

	public const int SetaudioinputsettingsApiLatest = 1;

	public const int SetaudiooutputsettingsApiLatest = 1;

	public const int SetinputdevicesettingsApiLatest = 1;

	public const int SetoutputdevicesettingsApiLatest = 1;

	public const int UnregisterplatformaudiouserApiLatest = 1;

	public const int UnregisterplatformuserApiLatest = 1;

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

	public Result CopyInputDeviceInformationByIndex (ref CopyInputDeviceInformationByIndexOptions options, out InputDeviceInformation? outInputDeviceInformation)
	{
		CopyInputDeviceInformationByIndexOptionsInternal options2 = default(CopyInputDeviceInformationByIndexOptionsInternal);
		options2.Set (ref options);
		IntPtr outInputDeviceInformation2 = IntPtr.Zero;
		Result result = Bindings.EOS_RTCAudio_CopyInputDeviceInformationByIndex (base.InnerHandle, ref options2, ref outInputDeviceInformation2);
		Helper.Dispose (ref options2);
		Helper.Get<InputDeviceInformationInternal, InputDeviceInformation> (outInputDeviceInformation2, out outInputDeviceInformation);
		if (outInputDeviceInformation.HasValue) {
			Bindings.EOS_RTCAudio_InputDeviceInformation_Release (outInputDeviceInformation2);
		}
		return result;
	}

	public Result CopyOutputDeviceInformationByIndex (ref CopyOutputDeviceInformationByIndexOptions options, out OutputDeviceInformation? outOutputDeviceInformation)
	{
		CopyOutputDeviceInformationByIndexOptionsInternal options2 = default(CopyOutputDeviceInformationByIndexOptionsInternal);
		options2.Set (ref options);
		IntPtr outOutputDeviceInformation2 = IntPtr.Zero;
		Result result = Bindings.EOS_RTCAudio_CopyOutputDeviceInformationByIndex (base.InnerHandle, ref options2, ref outOutputDeviceInformation2);
		Helper.Dispose (ref options2);
		Helper.Get<OutputDeviceInformationInternal, OutputDeviceInformation> (outOutputDeviceInformation2, out outOutputDeviceInformation);
		if (outOutputDeviceInformation.HasValue) {
			Bindings.EOS_RTCAudio_OutputDeviceInformation_Release (outOutputDeviceInformation2);
		}
		return result;
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

	public uint GetInputDevicesCount (ref GetInputDevicesCountOptions options)
	{
		GetInputDevicesCountOptionsInternal options2 = default(GetInputDevicesCountOptionsInternal);
		options2.Set (ref options);
		uint result = Bindings.EOS_RTCAudio_GetInputDevicesCount (base.InnerHandle, ref options2);
		Helper.Dispose (ref options2);
		return result;
	}

	public uint GetOutputDevicesCount (ref GetOutputDevicesCountOptions options)
	{
		GetOutputDevicesCountOptionsInternal options2 = default(GetOutputDevicesCountOptionsInternal);
		options2.Set (ref options);
		uint result = Bindings.EOS_RTCAudio_GetOutputDevicesCount (base.InnerHandle, ref options2);
		Helper.Dispose (ref options2);
		return result;
	}

	public void QueryInputDevicesInformation (ref QueryInputDevicesInformationOptions options, object clientData, OnQueryInputDevicesInformationCallback completionDelegate)
	{
		QueryInputDevicesInformationOptionsInternal options2 = default(QueryInputDevicesInformationOptionsInternal);
		options2.Set (ref options);
		IntPtr clientDataAddress = IntPtr.Zero;
		OnQueryInputDevicesInformationCallbackInternal onQueryInputDevicesInformationCallbackInternal = OnQueryInputDevicesInformationCallbackInternalImplementation;
		Helper.AddCallback (out clientDataAddress, clientData, completionDelegate, onQueryInputDevicesInformationCallbackInternal);
		Bindings.EOS_RTCAudio_QueryInputDevicesInformation (base.InnerHandle, ref options2, clientDataAddress, onQueryInputDevicesInformationCallbackInternal);
		Helper.Dispose (ref options2);
	}

	public void QueryOutputDevicesInformation (ref QueryOutputDevicesInformationOptions options, object clientData, OnQueryOutputDevicesInformationCallback completionDelegate)
	{
		QueryOutputDevicesInformationOptionsInternal options2 = default(QueryOutputDevicesInformationOptionsInternal);
		options2.Set (ref options);
		IntPtr clientDataAddress = IntPtr.Zero;
		OnQueryOutputDevicesInformationCallbackInternal onQueryOutputDevicesInformationCallbackInternal = OnQueryOutputDevicesInformationCallbackInternalImplementation;
		Helper.AddCallback (out clientDataAddress, clientData, completionDelegate, onQueryOutputDevicesInformationCallbackInternal);
		Bindings.EOS_RTCAudio_QueryOutputDevicesInformation (base.InnerHandle, ref options2, clientDataAddress, onQueryOutputDevicesInformationCallbackInternal);
		Helper.Dispose (ref options2);
	}

	public Result RegisterPlatformAudioUser (ref RegisterPlatformAudioUserOptions options)
	{
		RegisterPlatformAudioUserOptionsInternal options2 = default(RegisterPlatformAudioUserOptionsInternal);
		options2.Set (ref options);
		Result result = Bindings.EOS_RTCAudio_RegisterPlatformAudioUser (base.InnerHandle, ref options2);
		Helper.Dispose (ref options2);
		return result;
	}

	public void RegisterPlatformUser (ref RegisterPlatformUserOptions options, object clientData, OnRegisterPlatformUserCallback completionDelegate)
	{
		RegisterPlatformUserOptionsInternal options2 = default(RegisterPlatformUserOptionsInternal);
		options2.Set (ref options);
		IntPtr clientDataAddress = IntPtr.Zero;
		OnRegisterPlatformUserCallbackInternal onRegisterPlatformUserCallbackInternal = OnRegisterPlatformUserCallbackInternalImplementation;
		Helper.AddCallback (out clientDataAddress, clientData, completionDelegate, onRegisterPlatformUserCallbackInternal);
		Bindings.EOS_RTCAudio_RegisterPlatformUser (base.InnerHandle, ref options2, clientDataAddress, onRegisterPlatformUserCallbackInternal);
		Helper.Dispose (ref options2);
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

	public void SetInputDeviceSettings (ref SetInputDeviceSettingsOptions options, object clientData, OnSetInputDeviceSettingsCallback completionDelegate)
	{
		SetInputDeviceSettingsOptionsInternal options2 = default(SetInputDeviceSettingsOptionsInternal);
		options2.Set (ref options);
		IntPtr clientDataAddress = IntPtr.Zero;
		OnSetInputDeviceSettingsCallbackInternal onSetInputDeviceSettingsCallbackInternal = OnSetInputDeviceSettingsCallbackInternalImplementation;
		Helper.AddCallback (out clientDataAddress, clientData, completionDelegate, onSetInputDeviceSettingsCallbackInternal);
		Bindings.EOS_RTCAudio_SetInputDeviceSettings (base.InnerHandle, ref options2, clientDataAddress, onSetInputDeviceSettingsCallbackInternal);
		Helper.Dispose (ref options2);
	}

	public void SetOutputDeviceSettings (ref SetOutputDeviceSettingsOptions options, object clientData, OnSetOutputDeviceSettingsCallback completionDelegate)
	{
		SetOutputDeviceSettingsOptionsInternal options2 = default(SetOutputDeviceSettingsOptionsInternal);
		options2.Set (ref options);
		IntPtr clientDataAddress = IntPtr.Zero;
		OnSetOutputDeviceSettingsCallbackInternal onSetOutputDeviceSettingsCallbackInternal = OnSetOutputDeviceSettingsCallbackInternalImplementation;
		Helper.AddCallback (out clientDataAddress, clientData, completionDelegate, onSetOutputDeviceSettingsCallbackInternal);
		Bindings.EOS_RTCAudio_SetOutputDeviceSettings (base.InnerHandle, ref options2, clientDataAddress, onSetOutputDeviceSettingsCallbackInternal);
		Helper.Dispose (ref options2);
	}

	public Result UnregisterPlatformAudioUser (ref UnregisterPlatformAudioUserOptions options)
	{
		UnregisterPlatformAudioUserOptionsInternal options2 = default(UnregisterPlatformAudioUserOptionsInternal);
		options2.Set (ref options);
		Result result = Bindings.EOS_RTCAudio_UnregisterPlatformAudioUser (base.InnerHandle, ref options2);
		Helper.Dispose (ref options2);
		return result;
	}

	public void UnregisterPlatformUser (ref UnregisterPlatformUserOptions options, object clientData, OnUnregisterPlatformUserCallback completionDelegate)
	{
		UnregisterPlatformUserOptionsInternal options2 = default(UnregisterPlatformUserOptionsInternal);
		options2.Set (ref options);
		IntPtr clientDataAddress = IntPtr.Zero;
		OnUnregisterPlatformUserCallbackInternal onUnregisterPlatformUserCallbackInternal = OnUnregisterPlatformUserCallbackInternalImplementation;
		Helper.AddCallback (out clientDataAddress, clientData, completionDelegate, onUnregisterPlatformUserCallbackInternal);
		Bindings.EOS_RTCAudio_UnregisterPlatformUser (base.InnerHandle, ref options2, clientDataAddress, onUnregisterPlatformUserCallbackInternal);
		Helper.Dispose (ref options2);
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
		if (Helper.TryGetCallback<AudioBeforeRenderCallbackInfoInternal, OnAudioBeforeRenderCallback, AudioBeforeRenderCallbackInfo> (ref data, out var callback, out var callbackInfo)) {
			callback (ref callbackInfo);
		}
	}

	[MonoPInvokeCallback (typeof(OnAudioBeforeSendCallbackInternal))]
	internal static void OnAudioBeforeSendCallbackInternalImplementation (ref AudioBeforeSendCallbackInfoInternal data)
	{
		if (Helper.TryGetCallback<AudioBeforeSendCallbackInfoInternal, OnAudioBeforeSendCallback, AudioBeforeSendCallbackInfo> (ref data, out var callback, out var callbackInfo)) {
			callback (ref callbackInfo);
		}
	}

	[MonoPInvokeCallback (typeof(OnAudioDevicesChangedCallbackInternal))]
	internal static void OnAudioDevicesChangedCallbackInternalImplementation (ref AudioDevicesChangedCallbackInfoInternal data)
	{
		if (Helper.TryGetCallback<AudioDevicesChangedCallbackInfoInternal, OnAudioDevicesChangedCallback, AudioDevicesChangedCallbackInfo> (ref data, out var callback, out var callbackInfo)) {
			callback (ref callbackInfo);
		}
	}

	[MonoPInvokeCallback (typeof(OnAudioInputStateCallbackInternal))]
	internal static void OnAudioInputStateCallbackInternalImplementation (ref AudioInputStateCallbackInfoInternal data)
	{
		if (Helper.TryGetCallback<AudioInputStateCallbackInfoInternal, OnAudioInputStateCallback, AudioInputStateCallbackInfo> (ref data, out var callback, out var callbackInfo)) {
			callback (ref callbackInfo);
		}
	}

	[MonoPInvokeCallback (typeof(OnAudioOutputStateCallbackInternal))]
	internal static void OnAudioOutputStateCallbackInternalImplementation (ref AudioOutputStateCallbackInfoInternal data)
	{
		if (Helper.TryGetCallback<AudioOutputStateCallbackInfoInternal, OnAudioOutputStateCallback, AudioOutputStateCallbackInfo> (ref data, out var callback, out var callbackInfo)) {
			callback (ref callbackInfo);
		}
	}

	[MonoPInvokeCallback (typeof(OnParticipantUpdatedCallbackInternal))]
	internal static void OnParticipantUpdatedCallbackInternalImplementation (ref ParticipantUpdatedCallbackInfoInternal data)
	{
		if (Helper.TryGetCallback<ParticipantUpdatedCallbackInfoInternal, OnParticipantUpdatedCallback, ParticipantUpdatedCallbackInfo> (ref data, out var callback, out var callbackInfo)) {
			callback (ref callbackInfo);
		}
	}

	[MonoPInvokeCallback (typeof(OnQueryInputDevicesInformationCallbackInternal))]
	internal static void OnQueryInputDevicesInformationCallbackInternalImplementation (ref OnQueryInputDevicesInformationCallbackInfoInternal data)
	{
		if (Helper.TryGetAndRemoveCallback<OnQueryInputDevicesInformationCallbackInfoInternal, OnQueryInputDevicesInformationCallback, OnQueryInputDevicesInformationCallbackInfo> (ref data, out var callback, out var callbackInfo)) {
			callback (ref callbackInfo);
		}
	}

	[MonoPInvokeCallback (typeof(OnQueryOutputDevicesInformationCallbackInternal))]
	internal static void OnQueryOutputDevicesInformationCallbackInternalImplementation (ref OnQueryOutputDevicesInformationCallbackInfoInternal data)
	{
		if (Helper.TryGetAndRemoveCallback<OnQueryOutputDevicesInformationCallbackInfoInternal, OnQueryOutputDevicesInformationCallback, OnQueryOutputDevicesInformationCallbackInfo> (ref data, out var callback, out var callbackInfo)) {
			callback (ref callbackInfo);
		}
	}

	[MonoPInvokeCallback (typeof(OnRegisterPlatformUserCallbackInternal))]
	internal static void OnRegisterPlatformUserCallbackInternalImplementation (ref OnRegisterPlatformUserCallbackInfoInternal data)
	{
		if (Helper.TryGetAndRemoveCallback<OnRegisterPlatformUserCallbackInfoInternal, OnRegisterPlatformUserCallback, OnRegisterPlatformUserCallbackInfo> (ref data, out var callback, out var callbackInfo)) {
			callback (ref callbackInfo);
		}
	}

	[MonoPInvokeCallback (typeof(OnSetInputDeviceSettingsCallbackInternal))]
	internal static void OnSetInputDeviceSettingsCallbackInternalImplementation (ref OnSetInputDeviceSettingsCallbackInfoInternal data)
	{
		if (Helper.TryGetAndRemoveCallback<OnSetInputDeviceSettingsCallbackInfoInternal, OnSetInputDeviceSettingsCallback, OnSetInputDeviceSettingsCallbackInfo> (ref data, out var callback, out var callbackInfo)) {
			callback (ref callbackInfo);
		}
	}

	[MonoPInvokeCallback (typeof(OnSetOutputDeviceSettingsCallbackInternal))]
	internal static void OnSetOutputDeviceSettingsCallbackInternalImplementation (ref OnSetOutputDeviceSettingsCallbackInfoInternal data)
	{
		if (Helper.TryGetAndRemoveCallback<OnSetOutputDeviceSettingsCallbackInfoInternal, OnSetOutputDeviceSettingsCallback, OnSetOutputDeviceSettingsCallbackInfo> (ref data, out var callback, out var callbackInfo)) {
			callback (ref callbackInfo);
		}
	}

	[MonoPInvokeCallback (typeof(OnUnregisterPlatformUserCallbackInternal))]
	internal static void OnUnregisterPlatformUserCallbackInternalImplementation (ref OnUnregisterPlatformUserCallbackInfoInternal data)
	{
		if (Helper.TryGetAndRemoveCallback<OnUnregisterPlatformUserCallbackInfoInternal, OnUnregisterPlatformUserCallback, OnUnregisterPlatformUserCallbackInfo> (ref data, out var callback, out var callbackInfo)) {
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
