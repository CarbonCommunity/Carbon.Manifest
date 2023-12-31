using System;
using Epic.OnlineServices;
using Epic.OnlineServices.Connect;

public sealed class ConnectInterface : Handle
{
	public const int AddnotifyauthexpirationApiLatest = 1;

	public const int AddnotifyloginstatuschangedApiLatest = 1;

	public const int CopyidtokenApiLatest = 1;

	public const int CopyproductuserexternalaccountbyaccountidApiLatest = 1;

	public const int CopyproductuserexternalaccountbyaccounttypeApiLatest = 1;

	public const int CopyproductuserexternalaccountbyindexApiLatest = 1;

	public const int CopyproductuserinfoApiLatest = 1;

	public const int CreatedeviceidApiLatest = 1;

	public const int CreatedeviceidDevicemodelMaxLength = 64;

	public const int CreateuserApiLatest = 1;

	public const int CredentialsApiLatest = 1;

	public const int DeletedeviceidApiLatest = 1;

	public const int ExternalAccountIdMaxLength = 256;

	public const int ExternalaccountinfoApiLatest = 1;

	public const int GetexternalaccountmappingApiLatest = 1;

	public const int GetexternalaccountmappingsApiLatest = 1;

	public const int GetproductuserexternalaccountcountApiLatest = 1;

	public const int GetproductuseridmappingApiLatest = 1;

	public const int IdtokenApiLatest = 1;

	public const int LinkaccountApiLatest = 1;

	public const int LoginApiLatest = 2;

	public const int OnauthexpirationcallbackApiLatest = 1;

	public const int QueryexternalaccountmappingsApiLatest = 1;

	public const int QueryexternalaccountmappingsMaxAccountIds = 128;

	public const int QueryproductuseridmappingsApiLatest = 2;

	public const int TimeUndefined = -1;

	public const int TransferdeviceidaccountApiLatest = 1;

	public const int UnlinkaccountApiLatest = 1;

	public const int UserlogininfoApiLatest = 1;

	public const int UserlogininfoDisplaynameMaxLength = 32;

	public const int VerifyidtokenApiLatest = 1;

	public ConnectInterface ()
	{
	}

	public ConnectInterface (IntPtr innerHandle)
		: base (innerHandle)
	{
	}

	public ulong AddNotifyAuthExpiration (ref AddNotifyAuthExpirationOptions options, object clientData, OnAuthExpirationCallback notification)
	{
		AddNotifyAuthExpirationOptionsInternal options2 = default(AddNotifyAuthExpirationOptionsInternal);
		options2.Set (ref options);
		IntPtr clientDataAddress = IntPtr.Zero;
		OnAuthExpirationCallbackInternal onAuthExpirationCallbackInternal = OnAuthExpirationCallbackInternalImplementation;
		Helper.AddCallback (out clientDataAddress, clientData, notification, onAuthExpirationCallbackInternal);
		ulong num = Bindings.EOS_Connect_AddNotifyAuthExpiration (base.InnerHandle, ref options2, clientDataAddress, onAuthExpirationCallbackInternal);
		Helper.Dispose (ref options2);
		Helper.AssignNotificationIdToCallback (clientDataAddress, num);
		return num;
	}

	public ulong AddNotifyLoginStatusChanged (ref AddNotifyLoginStatusChangedOptions options, object clientData, OnLoginStatusChangedCallback notification)
	{
		AddNotifyLoginStatusChangedOptionsInternal options2 = default(AddNotifyLoginStatusChangedOptionsInternal);
		options2.Set (ref options);
		IntPtr clientDataAddress = IntPtr.Zero;
		OnLoginStatusChangedCallbackInternal onLoginStatusChangedCallbackInternal = OnLoginStatusChangedCallbackInternalImplementation;
		Helper.AddCallback (out clientDataAddress, clientData, notification, onLoginStatusChangedCallbackInternal);
		ulong num = Bindings.EOS_Connect_AddNotifyLoginStatusChanged (base.InnerHandle, ref options2, clientDataAddress, onLoginStatusChangedCallbackInternal);
		Helper.Dispose (ref options2);
		Helper.AssignNotificationIdToCallback (clientDataAddress, num);
		return num;
	}

	public Result CopyIdToken (ref CopyIdTokenOptions options, out IdToken? outIdToken)
	{
		CopyIdTokenOptionsInternal options2 = default(CopyIdTokenOptionsInternal);
		options2.Set (ref options);
		IntPtr outIdToken2 = IntPtr.Zero;
		Result result = Bindings.EOS_Connect_CopyIdToken (base.InnerHandle, ref options2, ref outIdToken2);
		Helper.Dispose (ref options2);
		Helper.Get<IdTokenInternal, IdToken> (outIdToken2, out outIdToken);
		if (outIdToken.HasValue) {
			Bindings.EOS_Connect_IdToken_Release (outIdToken2);
		}
		return result;
	}

	public Result CopyProductUserExternalAccountByAccountId (ref CopyProductUserExternalAccountByAccountIdOptions options, out ExternalAccountInfo? outExternalAccountInfo)
	{
		CopyProductUserExternalAccountByAccountIdOptionsInternal options2 = default(CopyProductUserExternalAccountByAccountIdOptionsInternal);
		options2.Set (ref options);
		IntPtr outExternalAccountInfo2 = IntPtr.Zero;
		Result result = Bindings.EOS_Connect_CopyProductUserExternalAccountByAccountId (base.InnerHandle, ref options2, ref outExternalAccountInfo2);
		Helper.Dispose (ref options2);
		Helper.Get<ExternalAccountInfoInternal, ExternalAccountInfo> (outExternalAccountInfo2, out outExternalAccountInfo);
		if (outExternalAccountInfo.HasValue) {
			Bindings.EOS_Connect_ExternalAccountInfo_Release (outExternalAccountInfo2);
		}
		return result;
	}

	public Result CopyProductUserExternalAccountByAccountType (ref CopyProductUserExternalAccountByAccountTypeOptions options, out ExternalAccountInfo? outExternalAccountInfo)
	{
		CopyProductUserExternalAccountByAccountTypeOptionsInternal options2 = default(CopyProductUserExternalAccountByAccountTypeOptionsInternal);
		options2.Set (ref options);
		IntPtr outExternalAccountInfo2 = IntPtr.Zero;
		Result result = Bindings.EOS_Connect_CopyProductUserExternalAccountByAccountType (base.InnerHandle, ref options2, ref outExternalAccountInfo2);
		Helper.Dispose (ref options2);
		Helper.Get<ExternalAccountInfoInternal, ExternalAccountInfo> (outExternalAccountInfo2, out outExternalAccountInfo);
		if (outExternalAccountInfo.HasValue) {
			Bindings.EOS_Connect_ExternalAccountInfo_Release (outExternalAccountInfo2);
		}
		return result;
	}

	public Result CopyProductUserExternalAccountByIndex (ref CopyProductUserExternalAccountByIndexOptions options, out ExternalAccountInfo? outExternalAccountInfo)
	{
		CopyProductUserExternalAccountByIndexOptionsInternal options2 = default(CopyProductUserExternalAccountByIndexOptionsInternal);
		options2.Set (ref options);
		IntPtr outExternalAccountInfo2 = IntPtr.Zero;
		Result result = Bindings.EOS_Connect_CopyProductUserExternalAccountByIndex (base.InnerHandle, ref options2, ref outExternalAccountInfo2);
		Helper.Dispose (ref options2);
		Helper.Get<ExternalAccountInfoInternal, ExternalAccountInfo> (outExternalAccountInfo2, out outExternalAccountInfo);
		if (outExternalAccountInfo.HasValue) {
			Bindings.EOS_Connect_ExternalAccountInfo_Release (outExternalAccountInfo2);
		}
		return result;
	}

	public Result CopyProductUserInfo (ref CopyProductUserInfoOptions options, out ExternalAccountInfo? outExternalAccountInfo)
	{
		CopyProductUserInfoOptionsInternal options2 = default(CopyProductUserInfoOptionsInternal);
		options2.Set (ref options);
		IntPtr outExternalAccountInfo2 = IntPtr.Zero;
		Result result = Bindings.EOS_Connect_CopyProductUserInfo (base.InnerHandle, ref options2, ref outExternalAccountInfo2);
		Helper.Dispose (ref options2);
		Helper.Get<ExternalAccountInfoInternal, ExternalAccountInfo> (outExternalAccountInfo2, out outExternalAccountInfo);
		if (outExternalAccountInfo.HasValue) {
			Bindings.EOS_Connect_ExternalAccountInfo_Release (outExternalAccountInfo2);
		}
		return result;
	}

	public void CreateDeviceId (ref CreateDeviceIdOptions options, object clientData, OnCreateDeviceIdCallback completionDelegate)
	{
		CreateDeviceIdOptionsInternal options2 = default(CreateDeviceIdOptionsInternal);
		options2.Set (ref options);
		IntPtr clientDataAddress = IntPtr.Zero;
		OnCreateDeviceIdCallbackInternal onCreateDeviceIdCallbackInternal = OnCreateDeviceIdCallbackInternalImplementation;
		Helper.AddCallback (out clientDataAddress, clientData, completionDelegate, onCreateDeviceIdCallbackInternal);
		Bindings.EOS_Connect_CreateDeviceId (base.InnerHandle, ref options2, clientDataAddress, onCreateDeviceIdCallbackInternal);
		Helper.Dispose (ref options2);
	}

	public void CreateUser (ref CreateUserOptions options, object clientData, OnCreateUserCallback completionDelegate)
	{
		CreateUserOptionsInternal options2 = default(CreateUserOptionsInternal);
		options2.Set (ref options);
		IntPtr clientDataAddress = IntPtr.Zero;
		OnCreateUserCallbackInternal onCreateUserCallbackInternal = OnCreateUserCallbackInternalImplementation;
		Helper.AddCallback (out clientDataAddress, clientData, completionDelegate, onCreateUserCallbackInternal);
		Bindings.EOS_Connect_CreateUser (base.InnerHandle, ref options2, clientDataAddress, onCreateUserCallbackInternal);
		Helper.Dispose (ref options2);
	}

	public void DeleteDeviceId (ref DeleteDeviceIdOptions options, object clientData, OnDeleteDeviceIdCallback completionDelegate)
	{
		DeleteDeviceIdOptionsInternal options2 = default(DeleteDeviceIdOptionsInternal);
		options2.Set (ref options);
		IntPtr clientDataAddress = IntPtr.Zero;
		OnDeleteDeviceIdCallbackInternal onDeleteDeviceIdCallbackInternal = OnDeleteDeviceIdCallbackInternalImplementation;
		Helper.AddCallback (out clientDataAddress, clientData, completionDelegate, onDeleteDeviceIdCallbackInternal);
		Bindings.EOS_Connect_DeleteDeviceId (base.InnerHandle, ref options2, clientDataAddress, onDeleteDeviceIdCallbackInternal);
		Helper.Dispose (ref options2);
	}

	public ProductUserId GetExternalAccountMapping (ref GetExternalAccountMappingsOptions options)
	{
		GetExternalAccountMappingsOptionsInternal options2 = default(GetExternalAccountMappingsOptionsInternal);
		options2.Set (ref options);
		IntPtr from = Bindings.EOS_Connect_GetExternalAccountMapping (base.InnerHandle, ref options2);
		Helper.Dispose (ref options2);
		Helper.Get (from, out ProductUserId to);
		return to;
	}

	public ProductUserId GetLoggedInUserByIndex (int index)
	{
		Helper.Get (Bindings.EOS_Connect_GetLoggedInUserByIndex (base.InnerHandle, index), out ProductUserId to);
		return to;
	}

	public int GetLoggedInUsersCount ()
	{
		return Bindings.EOS_Connect_GetLoggedInUsersCount (base.InnerHandle);
	}

	public LoginStatus GetLoginStatus (ProductUserId localUserId)
	{
		IntPtr to = IntPtr.Zero;
		Helper.Set (localUserId, ref to);
		return Bindings.EOS_Connect_GetLoginStatus (base.InnerHandle, to);
	}

	public uint GetProductUserExternalAccountCount (ref GetProductUserExternalAccountCountOptions options)
	{
		GetProductUserExternalAccountCountOptionsInternal options2 = default(GetProductUserExternalAccountCountOptionsInternal);
		options2.Set (ref options);
		uint result = Bindings.EOS_Connect_GetProductUserExternalAccountCount (base.InnerHandle, ref options2);
		Helper.Dispose (ref options2);
		return result;
	}

	public Result GetProductUserIdMapping (ref GetProductUserIdMappingOptions options, out Utf8String outBuffer)
	{
		GetProductUserIdMappingOptionsInternal options2 = default(GetProductUserIdMappingOptionsInternal);
		options2.Set (ref options);
		int inOutBufferLength = 257;
		IntPtr value = Helper.AddAllocation (inOutBufferLength);
		Result result = Bindings.EOS_Connect_GetProductUserIdMapping (base.InnerHandle, ref options2, value, ref inOutBufferLength);
		Helper.Dispose (ref options2);
		Helper.Get (value, out outBuffer);
		Helper.Dispose (ref value);
		return result;
	}

	public void LinkAccount (ref LinkAccountOptions options, object clientData, OnLinkAccountCallback completionDelegate)
	{
		LinkAccountOptionsInternal options2 = default(LinkAccountOptionsInternal);
		options2.Set (ref options);
		IntPtr clientDataAddress = IntPtr.Zero;
		OnLinkAccountCallbackInternal onLinkAccountCallbackInternal = OnLinkAccountCallbackInternalImplementation;
		Helper.AddCallback (out clientDataAddress, clientData, completionDelegate, onLinkAccountCallbackInternal);
		Bindings.EOS_Connect_LinkAccount (base.InnerHandle, ref options2, clientDataAddress, onLinkAccountCallbackInternal);
		Helper.Dispose (ref options2);
	}

	public void Login (ref LoginOptions options, object clientData, OnLoginCallback completionDelegate)
	{
		LoginOptionsInternal options2 = default(LoginOptionsInternal);
		options2.Set (ref options);
		IntPtr clientDataAddress = IntPtr.Zero;
		OnLoginCallbackInternal onLoginCallbackInternal = OnLoginCallbackInternalImplementation;
		Helper.AddCallback (out clientDataAddress, clientData, completionDelegate, onLoginCallbackInternal);
		Bindings.EOS_Connect_Login (base.InnerHandle, ref options2, clientDataAddress, onLoginCallbackInternal);
		Helper.Dispose (ref options2);
	}

	public void QueryExternalAccountMappings (ref QueryExternalAccountMappingsOptions options, object clientData, OnQueryExternalAccountMappingsCallback completionDelegate)
	{
		QueryExternalAccountMappingsOptionsInternal options2 = default(QueryExternalAccountMappingsOptionsInternal);
		options2.Set (ref options);
		IntPtr clientDataAddress = IntPtr.Zero;
		OnQueryExternalAccountMappingsCallbackInternal onQueryExternalAccountMappingsCallbackInternal = OnQueryExternalAccountMappingsCallbackInternalImplementation;
		Helper.AddCallback (out clientDataAddress, clientData, completionDelegate, onQueryExternalAccountMappingsCallbackInternal);
		Bindings.EOS_Connect_QueryExternalAccountMappings (base.InnerHandle, ref options2, clientDataAddress, onQueryExternalAccountMappingsCallbackInternal);
		Helper.Dispose (ref options2);
	}

	public void QueryProductUserIdMappings (ref QueryProductUserIdMappingsOptions options, object clientData, OnQueryProductUserIdMappingsCallback completionDelegate)
	{
		QueryProductUserIdMappingsOptionsInternal options2 = default(QueryProductUserIdMappingsOptionsInternal);
		options2.Set (ref options);
		IntPtr clientDataAddress = IntPtr.Zero;
		OnQueryProductUserIdMappingsCallbackInternal onQueryProductUserIdMappingsCallbackInternal = OnQueryProductUserIdMappingsCallbackInternalImplementation;
		Helper.AddCallback (out clientDataAddress, clientData, completionDelegate, onQueryProductUserIdMappingsCallbackInternal);
		Bindings.EOS_Connect_QueryProductUserIdMappings (base.InnerHandle, ref options2, clientDataAddress, onQueryProductUserIdMappingsCallbackInternal);
		Helper.Dispose (ref options2);
	}

	public void RemoveNotifyAuthExpiration (ulong inId)
	{
		Bindings.EOS_Connect_RemoveNotifyAuthExpiration (base.InnerHandle, inId);
		Helper.RemoveCallbackByNotificationId (inId);
	}

	public void RemoveNotifyLoginStatusChanged (ulong inId)
	{
		Bindings.EOS_Connect_RemoveNotifyLoginStatusChanged (base.InnerHandle, inId);
		Helper.RemoveCallbackByNotificationId (inId);
	}

	public void TransferDeviceIdAccount (ref TransferDeviceIdAccountOptions options, object clientData, OnTransferDeviceIdAccountCallback completionDelegate)
	{
		TransferDeviceIdAccountOptionsInternal options2 = default(TransferDeviceIdAccountOptionsInternal);
		options2.Set (ref options);
		IntPtr clientDataAddress = IntPtr.Zero;
		OnTransferDeviceIdAccountCallbackInternal onTransferDeviceIdAccountCallbackInternal = OnTransferDeviceIdAccountCallbackInternalImplementation;
		Helper.AddCallback (out clientDataAddress, clientData, completionDelegate, onTransferDeviceIdAccountCallbackInternal);
		Bindings.EOS_Connect_TransferDeviceIdAccount (base.InnerHandle, ref options2, clientDataAddress, onTransferDeviceIdAccountCallbackInternal);
		Helper.Dispose (ref options2);
	}

	public void UnlinkAccount (ref UnlinkAccountOptions options, object clientData, OnUnlinkAccountCallback completionDelegate)
	{
		UnlinkAccountOptionsInternal options2 = default(UnlinkAccountOptionsInternal);
		options2.Set (ref options);
		IntPtr clientDataAddress = IntPtr.Zero;
		OnUnlinkAccountCallbackInternal onUnlinkAccountCallbackInternal = OnUnlinkAccountCallbackInternalImplementation;
		Helper.AddCallback (out clientDataAddress, clientData, completionDelegate, onUnlinkAccountCallbackInternal);
		Bindings.EOS_Connect_UnlinkAccount (base.InnerHandle, ref options2, clientDataAddress, onUnlinkAccountCallbackInternal);
		Helper.Dispose (ref options2);
	}

	public void VerifyIdToken (ref VerifyIdTokenOptions options, object clientData, OnVerifyIdTokenCallback completionDelegate)
	{
		VerifyIdTokenOptionsInternal options2 = default(VerifyIdTokenOptionsInternal);
		options2.Set (ref options);
		IntPtr clientDataAddress = IntPtr.Zero;
		OnVerifyIdTokenCallbackInternal onVerifyIdTokenCallbackInternal = OnVerifyIdTokenCallbackInternalImplementation;
		Helper.AddCallback (out clientDataAddress, clientData, completionDelegate, onVerifyIdTokenCallbackInternal);
		Bindings.EOS_Connect_VerifyIdToken (base.InnerHandle, ref options2, clientDataAddress, onVerifyIdTokenCallbackInternal);
		Helper.Dispose (ref options2);
	}

	[MonoPInvokeCallback (typeof(OnAuthExpirationCallbackInternal))]
	internal static void OnAuthExpirationCallbackInternalImplementation (ref AuthExpirationCallbackInfoInternal data)
	{
		if (Helper.TryGetAndRemoveCallback<AuthExpirationCallbackInfoInternal, OnAuthExpirationCallback, AuthExpirationCallbackInfo> (ref data, out var callback, out var callbackInfo)) {
			callback (ref callbackInfo);
		}
	}

	[MonoPInvokeCallback (typeof(OnCreateDeviceIdCallbackInternal))]
	internal static void OnCreateDeviceIdCallbackInternalImplementation (ref CreateDeviceIdCallbackInfoInternal data)
	{
		if (Helper.TryGetAndRemoveCallback<CreateDeviceIdCallbackInfoInternal, OnCreateDeviceIdCallback, CreateDeviceIdCallbackInfo> (ref data, out var callback, out var callbackInfo)) {
			callback (ref callbackInfo);
		}
	}

	[MonoPInvokeCallback (typeof(OnCreateUserCallbackInternal))]
	internal static void OnCreateUserCallbackInternalImplementation (ref CreateUserCallbackInfoInternal data)
	{
		if (Helper.TryGetAndRemoveCallback<CreateUserCallbackInfoInternal, OnCreateUserCallback, CreateUserCallbackInfo> (ref data, out var callback, out var callbackInfo)) {
			callback (ref callbackInfo);
		}
	}

	[MonoPInvokeCallback (typeof(OnDeleteDeviceIdCallbackInternal))]
	internal static void OnDeleteDeviceIdCallbackInternalImplementation (ref DeleteDeviceIdCallbackInfoInternal data)
	{
		if (Helper.TryGetAndRemoveCallback<DeleteDeviceIdCallbackInfoInternal, OnDeleteDeviceIdCallback, DeleteDeviceIdCallbackInfo> (ref data, out var callback, out var callbackInfo)) {
			callback (ref callbackInfo);
		}
	}

	[MonoPInvokeCallback (typeof(OnLinkAccountCallbackInternal))]
	internal static void OnLinkAccountCallbackInternalImplementation (ref LinkAccountCallbackInfoInternal data)
	{
		if (Helper.TryGetAndRemoveCallback<LinkAccountCallbackInfoInternal, OnLinkAccountCallback, LinkAccountCallbackInfo> (ref data, out var callback, out var callbackInfo)) {
			callback (ref callbackInfo);
		}
	}

	[MonoPInvokeCallback (typeof(OnLoginCallbackInternal))]
	internal static void OnLoginCallbackInternalImplementation (ref LoginCallbackInfoInternal data)
	{
		if (Helper.TryGetAndRemoveCallback<LoginCallbackInfoInternal, OnLoginCallback, LoginCallbackInfo> (ref data, out var callback, out var callbackInfo)) {
			callback (ref callbackInfo);
		}
	}

	[MonoPInvokeCallback (typeof(OnLoginStatusChangedCallbackInternal))]
	internal static void OnLoginStatusChangedCallbackInternalImplementation (ref LoginStatusChangedCallbackInfoInternal data)
	{
		if (Helper.TryGetAndRemoveCallback<LoginStatusChangedCallbackInfoInternal, OnLoginStatusChangedCallback, LoginStatusChangedCallbackInfo> (ref data, out var callback, out var callbackInfo)) {
			callback (ref callbackInfo);
		}
	}

	[MonoPInvokeCallback (typeof(OnQueryExternalAccountMappingsCallbackInternal))]
	internal static void OnQueryExternalAccountMappingsCallbackInternalImplementation (ref QueryExternalAccountMappingsCallbackInfoInternal data)
	{
		if (Helper.TryGetAndRemoveCallback<QueryExternalAccountMappingsCallbackInfoInternal, OnQueryExternalAccountMappingsCallback, QueryExternalAccountMappingsCallbackInfo> (ref data, out var callback, out var callbackInfo)) {
			callback (ref callbackInfo);
		}
	}

	[MonoPInvokeCallback (typeof(OnQueryProductUserIdMappingsCallbackInternal))]
	internal static void OnQueryProductUserIdMappingsCallbackInternalImplementation (ref QueryProductUserIdMappingsCallbackInfoInternal data)
	{
		if (Helper.TryGetAndRemoveCallback<QueryProductUserIdMappingsCallbackInfoInternal, OnQueryProductUserIdMappingsCallback, QueryProductUserIdMappingsCallbackInfo> (ref data, out var callback, out var callbackInfo)) {
			callback (ref callbackInfo);
		}
	}

	[MonoPInvokeCallback (typeof(OnTransferDeviceIdAccountCallbackInternal))]
	internal static void OnTransferDeviceIdAccountCallbackInternalImplementation (ref TransferDeviceIdAccountCallbackInfoInternal data)
	{
		if (Helper.TryGetAndRemoveCallback<TransferDeviceIdAccountCallbackInfoInternal, OnTransferDeviceIdAccountCallback, TransferDeviceIdAccountCallbackInfo> (ref data, out var callback, out var callbackInfo)) {
			callback (ref callbackInfo);
		}
	}

	[MonoPInvokeCallback (typeof(OnUnlinkAccountCallbackInternal))]
	internal static void OnUnlinkAccountCallbackInternalImplementation (ref UnlinkAccountCallbackInfoInternal data)
	{
		if (Helper.TryGetAndRemoveCallback<UnlinkAccountCallbackInfoInternal, OnUnlinkAccountCallback, UnlinkAccountCallbackInfo> (ref data, out var callback, out var callbackInfo)) {
			callback (ref callbackInfo);
		}
	}

	[MonoPInvokeCallback (typeof(OnVerifyIdTokenCallbackInternal))]
	internal static void OnVerifyIdTokenCallbackInternalImplementation (ref VerifyIdTokenCallbackInfoInternal data)
	{
		if (Helper.TryGetAndRemoveCallback<VerifyIdTokenCallbackInfoInternal, OnVerifyIdTokenCallback, VerifyIdTokenCallbackInfo> (ref data, out var callback, out var callbackInfo)) {
			callback (ref callbackInfo);
		}
	}
}
