using System;
using Epic.OnlineServices;
using Epic.OnlineServices.UI;

public sealed class UIInterface : Handle
{
	public const int AcknowledgecorrelationidApiLatest = 1;

	public const int AcknowledgeeventidApiLatest = 1;

	public const int AddnotifydisplaysettingsupdatedApiLatest = 1;

	public const int AddnotifymemorymonitorApiLatest = 1;

	public const int AddnotifymemorymonitoroptionsApiLatest = 1;

	public const int EventidInvalid = 0;

	public const int GetfriendsexclusiveinputApiLatest = 1;

	public const int GetfriendsvisibleApiLatest = 1;

	public const int GettogglefriendsbuttonApiLatest = 1;

	public const int GettogglefriendskeyApiLatest = 1;

	public const int HidefriendsApiLatest = 1;

	public const int IssocialoverlaypausedApiLatest = 1;

	public const int MemorymonitorcallbackinfoApiLatest = 1;

	public const int PausesocialoverlayApiLatest = 1;

	public const int PrepresentApiLatest = 1;

	public const int RectApiLatest = 1;

	public const int ReportinputstateApiLatest = 2;

	public const int SetdisplaypreferenceApiLatest = 1;

	public const int SettogglefriendsbuttonApiLatest = 1;

	public const int SettogglefriendskeyApiLatest = 1;

	public const int ShowblockplayerApiLatest = 1;

	public const int ShowfriendsApiLatest = 1;

	public const int ShownativeprofileApiLatest = 1;

	public const int ShowreportplayerApiLatest = 1;

	public UIInterface ()
	{
	}

	public UIInterface (IntPtr innerHandle)
		: base (innerHandle)
	{
	}

	public Result AcknowledgeEventId (ref AcknowledgeEventIdOptions options)
	{
		AcknowledgeEventIdOptionsInternal options2 = default(AcknowledgeEventIdOptionsInternal);
		options2.Set (ref options);
		Result result = Bindings.EOS_UI_AcknowledgeEventId (base.InnerHandle, ref options2);
		Helper.Dispose (ref options2);
		return result;
	}

	public ulong AddNotifyDisplaySettingsUpdated (ref AddNotifyDisplaySettingsUpdatedOptions options, object clientData, OnDisplaySettingsUpdatedCallback notificationFn)
	{
		AddNotifyDisplaySettingsUpdatedOptionsInternal options2 = default(AddNotifyDisplaySettingsUpdatedOptionsInternal);
		options2.Set (ref options);
		IntPtr clientDataAddress = IntPtr.Zero;
		OnDisplaySettingsUpdatedCallbackInternal onDisplaySettingsUpdatedCallbackInternal = OnDisplaySettingsUpdatedCallbackInternalImplementation;
		Helper.AddCallback (out clientDataAddress, clientData, notificationFn, onDisplaySettingsUpdatedCallbackInternal);
		ulong num = Bindings.EOS_UI_AddNotifyDisplaySettingsUpdated (base.InnerHandle, ref options2, clientDataAddress, onDisplaySettingsUpdatedCallbackInternal);
		Helper.Dispose (ref options2);
		Helper.AssignNotificationIdToCallback (clientDataAddress, num);
		return num;
	}

	public ulong AddNotifyMemoryMonitor (ref AddNotifyMemoryMonitorOptions options, object clientData, OnMemoryMonitorCallback notificationFn)
	{
		AddNotifyMemoryMonitorOptionsInternal options2 = default(AddNotifyMemoryMonitorOptionsInternal);
		options2.Set (ref options);
		IntPtr clientDataAddress = IntPtr.Zero;
		OnMemoryMonitorCallbackInternal onMemoryMonitorCallbackInternal = OnMemoryMonitorCallbackInternalImplementation;
		Helper.AddCallback (out clientDataAddress, clientData, notificationFn, onMemoryMonitorCallbackInternal);
		ulong num = Bindings.EOS_UI_AddNotifyMemoryMonitor (base.InnerHandle, ref options2, clientDataAddress, onMemoryMonitorCallbackInternal);
		Helper.Dispose (ref options2);
		Helper.AssignNotificationIdToCallback (clientDataAddress, num);
		return num;
	}

	public bool GetFriendsExclusiveInput (ref GetFriendsExclusiveInputOptions options)
	{
		GetFriendsExclusiveInputOptionsInternal options2 = default(GetFriendsExclusiveInputOptionsInternal);
		options2.Set (ref options);
		int from = Bindings.EOS_UI_GetFriendsExclusiveInput (base.InnerHandle, ref options2);
		Helper.Dispose (ref options2);
		Helper.Get (from, out var to);
		return to;
	}

	public bool GetFriendsVisible (ref GetFriendsVisibleOptions options)
	{
		GetFriendsVisibleOptionsInternal options2 = default(GetFriendsVisibleOptionsInternal);
		options2.Set (ref options);
		int from = Bindings.EOS_UI_GetFriendsVisible (base.InnerHandle, ref options2);
		Helper.Dispose (ref options2);
		Helper.Get (from, out var to);
		return to;
	}

	public NotificationLocation GetNotificationLocationPreference ()
	{
		return Bindings.EOS_UI_GetNotificationLocationPreference (base.InnerHandle);
	}

	public InputStateButtonFlags GetToggleFriendsButton (ref GetToggleFriendsButtonOptions options)
	{
		GetToggleFriendsButtonOptionsInternal options2 = default(GetToggleFriendsButtonOptionsInternal);
		options2.Set (ref options);
		InputStateButtonFlags result = Bindings.EOS_UI_GetToggleFriendsButton (base.InnerHandle, ref options2);
		Helper.Dispose (ref options2);
		return result;
	}

	public KeyCombination GetToggleFriendsKey (ref GetToggleFriendsKeyOptions options)
	{
		GetToggleFriendsKeyOptionsInternal options2 = default(GetToggleFriendsKeyOptionsInternal);
		options2.Set (ref options);
		KeyCombination result = Bindings.EOS_UI_GetToggleFriendsKey (base.InnerHandle, ref options2);
		Helper.Dispose (ref options2);
		return result;
	}

	public void HideFriends (ref HideFriendsOptions options, object clientData, OnHideFriendsCallback completionDelegate)
	{
		HideFriendsOptionsInternal options2 = default(HideFriendsOptionsInternal);
		options2.Set (ref options);
		IntPtr clientDataAddress = IntPtr.Zero;
		OnHideFriendsCallbackInternal onHideFriendsCallbackInternal = OnHideFriendsCallbackInternalImplementation;
		Helper.AddCallback (out clientDataAddress, clientData, completionDelegate, onHideFriendsCallbackInternal);
		Bindings.EOS_UI_HideFriends (base.InnerHandle, ref options2, clientDataAddress, onHideFriendsCallbackInternal);
		Helper.Dispose (ref options2);
	}

	public bool IsSocialOverlayPaused (ref IsSocialOverlayPausedOptions options)
	{
		IsSocialOverlayPausedOptionsInternal options2 = default(IsSocialOverlayPausedOptionsInternal);
		options2.Set (ref options);
		int from = Bindings.EOS_UI_IsSocialOverlayPaused (base.InnerHandle, ref options2);
		Helper.Dispose (ref options2);
		Helper.Get (from, out var to);
		return to;
	}

	public bool IsValidButtonCombination (InputStateButtonFlags buttonCombination)
	{
		Helper.Get (Bindings.EOS_UI_IsValidButtonCombination (base.InnerHandle, buttonCombination), out var to);
		return to;
	}

	public bool IsValidKeyCombination (KeyCombination keyCombination)
	{
		Helper.Get (Bindings.EOS_UI_IsValidKeyCombination (base.InnerHandle, keyCombination), out var to);
		return to;
	}

	public Result PauseSocialOverlay (ref PauseSocialOverlayOptions options)
	{
		PauseSocialOverlayOptionsInternal options2 = default(PauseSocialOverlayOptionsInternal);
		options2.Set (ref options);
		Result result = Bindings.EOS_UI_PauseSocialOverlay (base.InnerHandle, ref options2);
		Helper.Dispose (ref options2);
		return result;
	}

	public Result PrePresent (ref PrePresentOptions options)
	{
		PrePresentOptionsInternal options2 = default(PrePresentOptionsInternal);
		options2.Set (ref options);
		Result result = Bindings.EOS_UI_PrePresent (base.InnerHandle, ref options2);
		Helper.Dispose (ref options2);
		return result;
	}

	public void RemoveNotifyDisplaySettingsUpdated (ulong id)
	{
		Bindings.EOS_UI_RemoveNotifyDisplaySettingsUpdated (base.InnerHandle, id);
		Helper.RemoveCallbackByNotificationId (id);
	}

	public void RemoveNotifyMemoryMonitor (ulong id)
	{
		Bindings.EOS_UI_RemoveNotifyMemoryMonitor (base.InnerHandle, id);
		Helper.RemoveCallbackByNotificationId (id);
	}

	public Result ReportInputState (ref ReportInputStateOptions options)
	{
		ReportInputStateOptionsInternal options2 = default(ReportInputStateOptionsInternal);
		options2.Set (ref options);
		Result result = Bindings.EOS_UI_ReportInputState (base.InnerHandle, ref options2);
		Helper.Dispose (ref options2);
		return result;
	}

	public Result SetDisplayPreference (ref SetDisplayPreferenceOptions options)
	{
		SetDisplayPreferenceOptionsInternal options2 = default(SetDisplayPreferenceOptionsInternal);
		options2.Set (ref options);
		Result result = Bindings.EOS_UI_SetDisplayPreference (base.InnerHandle, ref options2);
		Helper.Dispose (ref options2);
		return result;
	}

	public Result SetToggleFriendsButton (ref SetToggleFriendsButtonOptions options)
	{
		SetToggleFriendsButtonOptionsInternal options2 = default(SetToggleFriendsButtonOptionsInternal);
		options2.Set (ref options);
		Result result = Bindings.EOS_UI_SetToggleFriendsButton (base.InnerHandle, ref options2);
		Helper.Dispose (ref options2);
		return result;
	}

	public Result SetToggleFriendsKey (ref SetToggleFriendsKeyOptions options)
	{
		SetToggleFriendsKeyOptionsInternal options2 = default(SetToggleFriendsKeyOptionsInternal);
		options2.Set (ref options);
		Result result = Bindings.EOS_UI_SetToggleFriendsKey (base.InnerHandle, ref options2);
		Helper.Dispose (ref options2);
		return result;
	}

	public void ShowBlockPlayer (ref ShowBlockPlayerOptions options, object clientData, OnShowBlockPlayerCallback completionDelegate)
	{
		ShowBlockPlayerOptionsInternal options2 = default(ShowBlockPlayerOptionsInternal);
		options2.Set (ref options);
		IntPtr clientDataAddress = IntPtr.Zero;
		OnShowBlockPlayerCallbackInternal onShowBlockPlayerCallbackInternal = OnShowBlockPlayerCallbackInternalImplementation;
		Helper.AddCallback (out clientDataAddress, clientData, completionDelegate, onShowBlockPlayerCallbackInternal);
		Bindings.EOS_UI_ShowBlockPlayer (base.InnerHandle, ref options2, clientDataAddress, onShowBlockPlayerCallbackInternal);
		Helper.Dispose (ref options2);
	}

	public void ShowFriends (ref ShowFriendsOptions options, object clientData, OnShowFriendsCallback completionDelegate)
	{
		ShowFriendsOptionsInternal options2 = default(ShowFriendsOptionsInternal);
		options2.Set (ref options);
		IntPtr clientDataAddress = IntPtr.Zero;
		OnShowFriendsCallbackInternal onShowFriendsCallbackInternal = OnShowFriendsCallbackInternalImplementation;
		Helper.AddCallback (out clientDataAddress, clientData, completionDelegate, onShowFriendsCallbackInternal);
		Bindings.EOS_UI_ShowFriends (base.InnerHandle, ref options2, clientDataAddress, onShowFriendsCallbackInternal);
		Helper.Dispose (ref options2);
	}

	public void ShowNativeProfile (ref ShowNativeProfileOptions options, object clientData, OnShowNativeProfileCallback completionDelegate)
	{
		ShowNativeProfileOptionsInternal options2 = default(ShowNativeProfileOptionsInternal);
		options2.Set (ref options);
		IntPtr clientDataAddress = IntPtr.Zero;
		OnShowNativeProfileCallbackInternal onShowNativeProfileCallbackInternal = OnShowNativeProfileCallbackInternalImplementation;
		Helper.AddCallback (out clientDataAddress, clientData, completionDelegate, onShowNativeProfileCallbackInternal);
		Bindings.EOS_UI_ShowNativeProfile (base.InnerHandle, ref options2, clientDataAddress, onShowNativeProfileCallbackInternal);
		Helper.Dispose (ref options2);
	}

	public void ShowReportPlayer (ref ShowReportPlayerOptions options, object clientData, OnShowReportPlayerCallback completionDelegate)
	{
		ShowReportPlayerOptionsInternal options2 = default(ShowReportPlayerOptionsInternal);
		options2.Set (ref options);
		IntPtr clientDataAddress = IntPtr.Zero;
		OnShowReportPlayerCallbackInternal onShowReportPlayerCallbackInternal = OnShowReportPlayerCallbackInternalImplementation;
		Helper.AddCallback (out clientDataAddress, clientData, completionDelegate, onShowReportPlayerCallbackInternal);
		Bindings.EOS_UI_ShowReportPlayer (base.InnerHandle, ref options2, clientDataAddress, onShowReportPlayerCallbackInternal);
		Helper.Dispose (ref options2);
	}

	[MonoPInvokeCallback (typeof(OnDisplaySettingsUpdatedCallbackInternal))]
	internal static void OnDisplaySettingsUpdatedCallbackInternalImplementation (ref OnDisplaySettingsUpdatedCallbackInfoInternal data)
	{
		if (Helper.TryGetCallback<OnDisplaySettingsUpdatedCallbackInfoInternal, OnDisplaySettingsUpdatedCallback, OnDisplaySettingsUpdatedCallbackInfo> (ref data, out var callback, out var callbackInfo)) {
			callback (ref callbackInfo);
		}
	}

	[MonoPInvokeCallback (typeof(OnHideFriendsCallbackInternal))]
	internal static void OnHideFriendsCallbackInternalImplementation (ref HideFriendsCallbackInfoInternal data)
	{
		if (Helper.TryGetAndRemoveCallback<HideFriendsCallbackInfoInternal, OnHideFriendsCallback, HideFriendsCallbackInfo> (ref data, out var callback, out var callbackInfo)) {
			callback (ref callbackInfo);
		}
	}

	[MonoPInvokeCallback (typeof(OnMemoryMonitorCallbackInternal))]
	internal static void OnMemoryMonitorCallbackInternalImplementation (ref MemoryMonitorCallbackInfoInternal data)
	{
		if (Helper.TryGetCallback<MemoryMonitorCallbackInfoInternal, OnMemoryMonitorCallback, MemoryMonitorCallbackInfo> (ref data, out var callback, out var callbackInfo)) {
			callback (ref callbackInfo);
		}
	}

	[MonoPInvokeCallback (typeof(OnShowBlockPlayerCallbackInternal))]
	internal static void OnShowBlockPlayerCallbackInternalImplementation (ref OnShowBlockPlayerCallbackInfoInternal data)
	{
		if (Helper.TryGetAndRemoveCallback<OnShowBlockPlayerCallbackInfoInternal, OnShowBlockPlayerCallback, OnShowBlockPlayerCallbackInfo> (ref data, out var callback, out var callbackInfo)) {
			callback (ref callbackInfo);
		}
	}

	[MonoPInvokeCallback (typeof(OnShowFriendsCallbackInternal))]
	internal static void OnShowFriendsCallbackInternalImplementation (ref ShowFriendsCallbackInfoInternal data)
	{
		if (Helper.TryGetAndRemoveCallback<ShowFriendsCallbackInfoInternal, OnShowFriendsCallback, ShowFriendsCallbackInfo> (ref data, out var callback, out var callbackInfo)) {
			callback (ref callbackInfo);
		}
	}

	[MonoPInvokeCallback (typeof(OnShowNativeProfileCallbackInternal))]
	internal static void OnShowNativeProfileCallbackInternalImplementation (ref ShowNativeProfileCallbackInfoInternal data)
	{
		if (Helper.TryGetAndRemoveCallback<ShowNativeProfileCallbackInfoInternal, OnShowNativeProfileCallback, ShowNativeProfileCallbackInfo> (ref data, out var callback, out var callbackInfo)) {
			callback (ref callbackInfo);
		}
	}

	[MonoPInvokeCallback (typeof(OnShowReportPlayerCallbackInternal))]
	internal static void OnShowReportPlayerCallbackInternalImplementation (ref OnShowReportPlayerCallbackInfoInternal data)
	{
		if (Helper.TryGetAndRemoveCallback<OnShowReportPlayerCallbackInfoInternal, OnShowReportPlayerCallback, OnShowReportPlayerCallbackInfo> (ref data, out var callback, out var callbackInfo)) {
			callback (ref callbackInfo);
		}
	}
}
