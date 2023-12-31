using System;
using Epic.OnlineServices;
using Epic.OnlineServices.KWS;

public sealed class KWSInterface : Handle
{
	public const int AddnotifypermissionsupdatereceivedApiLatest = 1;

	public const int CopypermissionbyindexApiLatest = 1;

	public const int CreateuserApiLatest = 1;

	public const int GetpermissionbykeyApiLatest = 1;

	public const int GetpermissionscountApiLatest = 1;

	public const int MaxPermissionLength = 32;

	public const int MaxPermissions = 16;

	public const int PermissionstatusApiLatest = 1;

	public const int QueryagegateApiLatest = 1;

	public const int QuerypermissionsApiLatest = 1;

	public const int RequestpermissionsApiLatest = 1;

	public const int UpdateparentemailApiLatest = 1;

	public KWSInterface ()
	{
	}

	public KWSInterface (IntPtr innerHandle)
		: base (innerHandle)
	{
	}

	public ulong AddNotifyPermissionsUpdateReceived (ref AddNotifyPermissionsUpdateReceivedOptions options, object clientData, OnPermissionsUpdateReceivedCallback notificationFn)
	{
		AddNotifyPermissionsUpdateReceivedOptionsInternal options2 = default(AddNotifyPermissionsUpdateReceivedOptionsInternal);
		options2.Set (ref options);
		IntPtr clientDataAddress = IntPtr.Zero;
		OnPermissionsUpdateReceivedCallbackInternal onPermissionsUpdateReceivedCallbackInternal = OnPermissionsUpdateReceivedCallbackInternalImplementation;
		Helper.AddCallback (out clientDataAddress, clientData, notificationFn, onPermissionsUpdateReceivedCallbackInternal);
		ulong num = Bindings.EOS_KWS_AddNotifyPermissionsUpdateReceived (base.InnerHandle, ref options2, clientDataAddress, onPermissionsUpdateReceivedCallbackInternal);
		Helper.Dispose (ref options2);
		Helper.AssignNotificationIdToCallback (clientDataAddress, num);
		return num;
	}

	public Result CopyPermissionByIndex (ref CopyPermissionByIndexOptions options, out PermissionStatus? outPermission)
	{
		CopyPermissionByIndexOptionsInternal options2 = default(CopyPermissionByIndexOptionsInternal);
		options2.Set (ref options);
		IntPtr outPermission2 = IntPtr.Zero;
		Result result = Bindings.EOS_KWS_CopyPermissionByIndex (base.InnerHandle, ref options2, ref outPermission2);
		Helper.Dispose (ref options2);
		Helper.Get<PermissionStatusInternal, PermissionStatus> (outPermission2, out outPermission);
		if (outPermission.HasValue) {
			Bindings.EOS_KWS_PermissionStatus_Release (outPermission2);
		}
		return result;
	}

	public void CreateUser (ref CreateUserOptions options, object clientData, OnCreateUserCallback completionDelegate)
	{
		CreateUserOptionsInternal options2 = default(CreateUserOptionsInternal);
		options2.Set (ref options);
		IntPtr clientDataAddress = IntPtr.Zero;
		OnCreateUserCallbackInternal onCreateUserCallbackInternal = OnCreateUserCallbackInternalImplementation;
		Helper.AddCallback (out clientDataAddress, clientData, completionDelegate, onCreateUserCallbackInternal);
		Bindings.EOS_KWS_CreateUser (base.InnerHandle, ref options2, clientDataAddress, onCreateUserCallbackInternal);
		Helper.Dispose (ref options2);
	}

	public Result GetPermissionByKey (ref GetPermissionByKeyOptions options, out KWSPermissionStatus outPermission)
	{
		GetPermissionByKeyOptionsInternal options2 = default(GetPermissionByKeyOptionsInternal);
		options2.Set (ref options);
		outPermission = Helper.GetDefault<KWSPermissionStatus> ();
		Result result = Bindings.EOS_KWS_GetPermissionByKey (base.InnerHandle, ref options2, ref outPermission);
		Helper.Dispose (ref options2);
		return result;
	}

	public int GetPermissionsCount (ref GetPermissionsCountOptions options)
	{
		GetPermissionsCountOptionsInternal options2 = default(GetPermissionsCountOptionsInternal);
		options2.Set (ref options);
		int result = Bindings.EOS_KWS_GetPermissionsCount (base.InnerHandle, ref options2);
		Helper.Dispose (ref options2);
		return result;
	}

	public void QueryAgeGate (ref QueryAgeGateOptions options, object clientData, OnQueryAgeGateCallback completionDelegate)
	{
		QueryAgeGateOptionsInternal options2 = default(QueryAgeGateOptionsInternal);
		options2.Set (ref options);
		IntPtr clientDataAddress = IntPtr.Zero;
		OnQueryAgeGateCallbackInternal onQueryAgeGateCallbackInternal = OnQueryAgeGateCallbackInternalImplementation;
		Helper.AddCallback (out clientDataAddress, clientData, completionDelegate, onQueryAgeGateCallbackInternal);
		Bindings.EOS_KWS_QueryAgeGate (base.InnerHandle, ref options2, clientDataAddress, onQueryAgeGateCallbackInternal);
		Helper.Dispose (ref options2);
	}

	public void QueryPermissions (ref QueryPermissionsOptions options, object clientData, OnQueryPermissionsCallback completionDelegate)
	{
		QueryPermissionsOptionsInternal options2 = default(QueryPermissionsOptionsInternal);
		options2.Set (ref options);
		IntPtr clientDataAddress = IntPtr.Zero;
		OnQueryPermissionsCallbackInternal onQueryPermissionsCallbackInternal = OnQueryPermissionsCallbackInternalImplementation;
		Helper.AddCallback (out clientDataAddress, clientData, completionDelegate, onQueryPermissionsCallbackInternal);
		Bindings.EOS_KWS_QueryPermissions (base.InnerHandle, ref options2, clientDataAddress, onQueryPermissionsCallbackInternal);
		Helper.Dispose (ref options2);
	}

	public void RemoveNotifyPermissionsUpdateReceived (ulong inId)
	{
		Bindings.EOS_KWS_RemoveNotifyPermissionsUpdateReceived (base.InnerHandle, inId);
		Helper.RemoveCallbackByNotificationId (inId);
	}

	public void RequestPermissions (ref RequestPermissionsOptions options, object clientData, OnRequestPermissionsCallback completionDelegate)
	{
		RequestPermissionsOptionsInternal options2 = default(RequestPermissionsOptionsInternal);
		options2.Set (ref options);
		IntPtr clientDataAddress = IntPtr.Zero;
		OnRequestPermissionsCallbackInternal onRequestPermissionsCallbackInternal = OnRequestPermissionsCallbackInternalImplementation;
		Helper.AddCallback (out clientDataAddress, clientData, completionDelegate, onRequestPermissionsCallbackInternal);
		Bindings.EOS_KWS_RequestPermissions (base.InnerHandle, ref options2, clientDataAddress, onRequestPermissionsCallbackInternal);
		Helper.Dispose (ref options2);
	}

	public void UpdateParentEmail (ref UpdateParentEmailOptions options, object clientData, OnUpdateParentEmailCallback completionDelegate)
	{
		UpdateParentEmailOptionsInternal options2 = default(UpdateParentEmailOptionsInternal);
		options2.Set (ref options);
		IntPtr clientDataAddress = IntPtr.Zero;
		OnUpdateParentEmailCallbackInternal onUpdateParentEmailCallbackInternal = OnUpdateParentEmailCallbackInternalImplementation;
		Helper.AddCallback (out clientDataAddress, clientData, completionDelegate, onUpdateParentEmailCallbackInternal);
		Bindings.EOS_KWS_UpdateParentEmail (base.InnerHandle, ref options2, clientDataAddress, onUpdateParentEmailCallbackInternal);
		Helper.Dispose (ref options2);
	}

	[MonoPInvokeCallback (typeof(OnCreateUserCallbackInternal))]
	internal static void OnCreateUserCallbackInternalImplementation (ref CreateUserCallbackInfoInternal data)
	{
		if (Helper.TryGetAndRemoveCallback<CreateUserCallbackInfoInternal, OnCreateUserCallback, CreateUserCallbackInfo> (ref data, out var callback, out var callbackInfo)) {
			callback (ref callbackInfo);
		}
	}

	[MonoPInvokeCallback (typeof(OnPermissionsUpdateReceivedCallbackInternal))]
	internal static void OnPermissionsUpdateReceivedCallbackInternalImplementation (ref PermissionsUpdateReceivedCallbackInfoInternal data)
	{
		if (Helper.TryGetAndRemoveCallback<PermissionsUpdateReceivedCallbackInfoInternal, OnPermissionsUpdateReceivedCallback, PermissionsUpdateReceivedCallbackInfo> (ref data, out var callback, out var callbackInfo)) {
			callback (ref callbackInfo);
		}
	}

	[MonoPInvokeCallback (typeof(OnQueryAgeGateCallbackInternal))]
	internal static void OnQueryAgeGateCallbackInternalImplementation (ref QueryAgeGateCallbackInfoInternal data)
	{
		if (Helper.TryGetAndRemoveCallback<QueryAgeGateCallbackInfoInternal, OnQueryAgeGateCallback, QueryAgeGateCallbackInfo> (ref data, out var callback, out var callbackInfo)) {
			callback (ref callbackInfo);
		}
	}

	[MonoPInvokeCallback (typeof(OnQueryPermissionsCallbackInternal))]
	internal static void OnQueryPermissionsCallbackInternalImplementation (ref QueryPermissionsCallbackInfoInternal data)
	{
		if (Helper.TryGetAndRemoveCallback<QueryPermissionsCallbackInfoInternal, OnQueryPermissionsCallback, QueryPermissionsCallbackInfo> (ref data, out var callback, out var callbackInfo)) {
			callback (ref callbackInfo);
		}
	}

	[MonoPInvokeCallback (typeof(OnRequestPermissionsCallbackInternal))]
	internal static void OnRequestPermissionsCallbackInternalImplementation (ref RequestPermissionsCallbackInfoInternal data)
	{
		if (Helper.TryGetAndRemoveCallback<RequestPermissionsCallbackInfoInternal, OnRequestPermissionsCallback, RequestPermissionsCallbackInfo> (ref data, out var callback, out var callbackInfo)) {
			callback (ref callbackInfo);
		}
	}

	[MonoPInvokeCallback (typeof(OnUpdateParentEmailCallbackInternal))]
	internal static void OnUpdateParentEmailCallbackInternalImplementation (ref UpdateParentEmailCallbackInfoInternal data)
	{
		if (Helper.TryGetAndRemoveCallback<UpdateParentEmailCallbackInfoInternal, OnUpdateParentEmailCallback, UpdateParentEmailCallbackInfo> (ref data, out var callback, out var callbackInfo)) {
			callback (ref callbackInfo);
		}
	}
}
