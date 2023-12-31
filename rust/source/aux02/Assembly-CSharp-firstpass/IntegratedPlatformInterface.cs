using System;
using Epic.OnlineServices;
using Epic.OnlineServices.IntegratedPlatform;

public sealed class IntegratedPlatformInterface : Handle
{
	public const int AddnotifyuserloginstatuschangedApiLatest = 1;

	public const int ClearuserprelogoutcallbackApiLatest = 1;

	public const int CreateintegratedplatformoptionscontainerApiLatest = 1;

	public const int FinalizedeferreduserlogoutApiLatest = 1;

	public static readonly Utf8String IptSteam = "STEAM";

	public const int OptionsApiLatest = 1;

	public const int SetuserloginstatusApiLatest = 1;

	public const int SetuserprelogoutcallbackApiLatest = 1;

	public const int SteamOptionsApiLatest = 2;

	public IntegratedPlatformInterface ()
	{
	}

	public IntegratedPlatformInterface (IntPtr innerHandle)
		: base (innerHandle)
	{
	}

	public ulong AddNotifyUserLoginStatusChanged (ref AddNotifyUserLoginStatusChangedOptions options, object clientData, OnUserLoginStatusChangedCallback callbackFunction)
	{
		AddNotifyUserLoginStatusChangedOptionsInternal options2 = default(AddNotifyUserLoginStatusChangedOptionsInternal);
		options2.Set (ref options);
		IntPtr clientDataAddress = IntPtr.Zero;
		OnUserLoginStatusChangedCallbackInternal onUserLoginStatusChangedCallbackInternal = OnUserLoginStatusChangedCallbackInternalImplementation;
		Helper.AddCallback (out clientDataAddress, clientData, callbackFunction, onUserLoginStatusChangedCallbackInternal);
		ulong num = Bindings.EOS_IntegratedPlatform_AddNotifyUserLoginStatusChanged (base.InnerHandle, ref options2, clientDataAddress, onUserLoginStatusChangedCallbackInternal);
		Helper.Dispose (ref options2);
		Helper.AssignNotificationIdToCallback (clientDataAddress, num);
		return num;
	}

	public void ClearUserPreLogoutCallback (ref ClearUserPreLogoutCallbackOptions options)
	{
		ClearUserPreLogoutCallbackOptionsInternal options2 = default(ClearUserPreLogoutCallbackOptionsInternal);
		options2.Set (ref options);
		Bindings.EOS_IntegratedPlatform_ClearUserPreLogoutCallback (base.InnerHandle, ref options2);
		Helper.Dispose (ref options2);
	}

	public static Result CreateIntegratedPlatformOptionsContainer (ref CreateIntegratedPlatformOptionsContainerOptions options, out IntegratedPlatformOptionsContainer outIntegratedPlatformOptionsContainerHandle)
	{
		CreateIntegratedPlatformOptionsContainerOptionsInternal options2 = default(CreateIntegratedPlatformOptionsContainerOptionsInternal);
		options2.Set (ref options);
		IntPtr outIntegratedPlatformOptionsContainerHandle2 = IntPtr.Zero;
		Result result = Bindings.EOS_IntegratedPlatform_CreateIntegratedPlatformOptionsContainer (ref options2, ref outIntegratedPlatformOptionsContainerHandle2);
		Helper.Dispose (ref options2);
		Helper.Get (outIntegratedPlatformOptionsContainerHandle2, out outIntegratedPlatformOptionsContainerHandle);
		return result;
	}

	public Result FinalizeDeferredUserLogout (ref FinalizeDeferredUserLogoutOptions options)
	{
		FinalizeDeferredUserLogoutOptionsInternal options2 = default(FinalizeDeferredUserLogoutOptionsInternal);
		options2.Set (ref options);
		Result result = Bindings.EOS_IntegratedPlatform_FinalizeDeferredUserLogout (base.InnerHandle, ref options2);
		Helper.Dispose (ref options2);
		return result;
	}

	public void RemoveNotifyUserLoginStatusChanged (ulong notificationId)
	{
		Bindings.EOS_IntegratedPlatform_RemoveNotifyUserLoginStatusChanged (base.InnerHandle, notificationId);
		Helper.RemoveCallbackByNotificationId (notificationId);
	}

	public Result SetUserLoginStatus (ref SetUserLoginStatusOptions options)
	{
		SetUserLoginStatusOptionsInternal options2 = default(SetUserLoginStatusOptionsInternal);
		options2.Set (ref options);
		Result result = Bindings.EOS_IntegratedPlatform_SetUserLoginStatus (base.InnerHandle, ref options2);
		Helper.Dispose (ref options2);
		return result;
	}

	public Result SetUserPreLogoutCallback (ref SetUserPreLogoutCallbackOptions options, object clientData, OnUserPreLogoutCallback callbackFunction)
	{
		SetUserPreLogoutCallbackOptionsInternal options2 = default(SetUserPreLogoutCallbackOptionsInternal);
		options2.Set (ref options);
		IntPtr clientDataAddress = IntPtr.Zero;
		OnUserPreLogoutCallbackInternal onUserPreLogoutCallbackInternal = OnUserPreLogoutCallbackInternalImplementation;
		Helper.AddCallback (out clientDataAddress, clientData, callbackFunction, onUserPreLogoutCallbackInternal);
		Result result = Bindings.EOS_IntegratedPlatform_SetUserPreLogoutCallback (base.InnerHandle, ref options2, clientDataAddress, onUserPreLogoutCallbackInternal);
		Helper.Dispose (ref options2);
		return result;
	}

	[MonoPInvokeCallback (typeof(OnUserLoginStatusChangedCallbackInternal))]
	internal static void OnUserLoginStatusChangedCallbackInternalImplementation (ref UserLoginStatusChangedCallbackInfoInternal data)
	{
		if (Helper.TryGetCallback<UserLoginStatusChangedCallbackInfoInternal, OnUserLoginStatusChangedCallback, UserLoginStatusChangedCallbackInfo> (ref data, out var callback, out var callbackInfo)) {
			callback (ref callbackInfo);
		}
	}

	[MonoPInvokeCallback (typeof(OnUserPreLogoutCallbackInternal))]
	internal static IntegratedPlatformPreLogoutAction OnUserPreLogoutCallbackInternalImplementation (ref UserPreLogoutCallbackInfoInternal data)
	{
		if (Helper.TryGetCallback<UserPreLogoutCallbackInfoInternal, OnUserPreLogoutCallback, UserPreLogoutCallbackInfo> (ref data, out var callback, out var callbackInfo)) {
			return callback (ref callbackInfo);
		}
		return Helper.GetDefault<IntegratedPlatformPreLogoutAction> ();
	}
}
