using System;
using Epic.OnlineServices;
using Epic.OnlineServices.Auth;

public sealed class AuthInterface : Handle
{
	public const int AccountfeaturerestrictedinfoApiLatest = 1;

	public const int AddnotifyloginstatuschangedApiLatest = 1;

	public const int CopyidtokenApiLatest = 1;

	public const int CopyuserauthtokenApiLatest = 1;

	public const int CredentialsApiLatest = 4;

	public const int DeletepersistentauthApiLatest = 2;

	public const int IdtokenApiLatest = 1;

	public const int LinkaccountApiLatest = 1;

	public const int LoginApiLatest = 3;

	public const int LogoutApiLatest = 1;

	public const int PingrantinfoApiLatest = 2;

	public const int QueryidtokenApiLatest = 1;

	public const int TokenApiLatest = 2;

	public const int VerifyidtokenApiLatest = 1;

	public const int VerifyuserauthApiLatest = 1;

	public const int IosCredentialssystemauthcredentialsoptionsApiLatest = 1;

	public AuthInterface ()
	{
	}

	public AuthInterface (IntPtr innerHandle)
		: base (innerHandle)
	{
	}

	public ulong AddNotifyLoginStatusChanged (ref AddNotifyLoginStatusChangedOptions options, object clientData, OnLoginStatusChangedCallback notification)
	{
		AddNotifyLoginStatusChangedOptionsInternal options2 = default(AddNotifyLoginStatusChangedOptionsInternal);
		options2.Set (ref options);
		IntPtr clientDataAddress = IntPtr.Zero;
		OnLoginStatusChangedCallbackInternal onLoginStatusChangedCallbackInternal = OnLoginStatusChangedCallbackInternalImplementation;
		Helper.AddCallback (out clientDataAddress, clientData, notification, onLoginStatusChangedCallbackInternal);
		ulong num = Bindings.EOS_Auth_AddNotifyLoginStatusChanged (base.InnerHandle, ref options2, clientDataAddress, onLoginStatusChangedCallbackInternal);
		Helper.Dispose (ref options2);
		Helper.AssignNotificationIdToCallback (clientDataAddress, num);
		return num;
	}

	public Result CopyIdToken (ref CopyIdTokenOptions options, out IdToken? outIdToken)
	{
		CopyIdTokenOptionsInternal options2 = default(CopyIdTokenOptionsInternal);
		options2.Set (ref options);
		IntPtr outIdToken2 = IntPtr.Zero;
		Result result = Bindings.EOS_Auth_CopyIdToken (base.InnerHandle, ref options2, ref outIdToken2);
		Helper.Dispose (ref options2);
		Helper.Get<IdTokenInternal, IdToken> (outIdToken2, out outIdToken);
		if (outIdToken.HasValue) {
			Bindings.EOS_Auth_IdToken_Release (outIdToken2);
		}
		return result;
	}

	public Result CopyUserAuthToken (ref CopyUserAuthTokenOptions options, EpicAccountId localUserId, out Token? outUserAuthToken)
	{
		CopyUserAuthTokenOptionsInternal options2 = default(CopyUserAuthTokenOptionsInternal);
		options2.Set (ref options);
		IntPtr to = IntPtr.Zero;
		Helper.Set (localUserId, ref to);
		IntPtr outUserAuthToken2 = IntPtr.Zero;
		Result result = Bindings.EOS_Auth_CopyUserAuthToken (base.InnerHandle, ref options2, to, ref outUserAuthToken2);
		Helper.Dispose (ref options2);
		Helper.Get<TokenInternal, Token> (outUserAuthToken2, out outUserAuthToken);
		if (outUserAuthToken.HasValue) {
			Bindings.EOS_Auth_Token_Release (outUserAuthToken2);
		}
		return result;
	}

	public void DeletePersistentAuth (ref DeletePersistentAuthOptions options, object clientData, OnDeletePersistentAuthCallback completionDelegate)
	{
		DeletePersistentAuthOptionsInternal options2 = default(DeletePersistentAuthOptionsInternal);
		options2.Set (ref options);
		IntPtr clientDataAddress = IntPtr.Zero;
		OnDeletePersistentAuthCallbackInternal onDeletePersistentAuthCallbackInternal = OnDeletePersistentAuthCallbackInternalImplementation;
		Helper.AddCallback (out clientDataAddress, clientData, completionDelegate, onDeletePersistentAuthCallbackInternal);
		Bindings.EOS_Auth_DeletePersistentAuth (base.InnerHandle, ref options2, clientDataAddress, onDeletePersistentAuthCallbackInternal);
		Helper.Dispose (ref options2);
	}

	public EpicAccountId GetLoggedInAccountByIndex (int index)
	{
		Helper.Get (Bindings.EOS_Auth_GetLoggedInAccountByIndex (base.InnerHandle, index), out EpicAccountId to);
		return to;
	}

	public int GetLoggedInAccountsCount ()
	{
		return Bindings.EOS_Auth_GetLoggedInAccountsCount (base.InnerHandle);
	}

	public LoginStatus GetLoginStatus (EpicAccountId localUserId)
	{
		IntPtr to = IntPtr.Zero;
		Helper.Set (localUserId, ref to);
		return Bindings.EOS_Auth_GetLoginStatus (base.InnerHandle, to);
	}

	public EpicAccountId GetMergedAccountByIndex (EpicAccountId localUserId, uint index)
	{
		IntPtr to = IntPtr.Zero;
		Helper.Set (localUserId, ref to);
		Helper.Get (Bindings.EOS_Auth_GetMergedAccountByIndex (base.InnerHandle, to, index), out EpicAccountId to2);
		return to2;
	}

	public uint GetMergedAccountsCount (EpicAccountId localUserId)
	{
		IntPtr to = IntPtr.Zero;
		Helper.Set (localUserId, ref to);
		return Bindings.EOS_Auth_GetMergedAccountsCount (base.InnerHandle, to);
	}

	public Result GetSelectedAccountId (EpicAccountId localUserId, out EpicAccountId outSelectedAccountId)
	{
		IntPtr to = IntPtr.Zero;
		Helper.Set (localUserId, ref to);
		IntPtr outSelectedAccountId2 = IntPtr.Zero;
		Result result = Bindings.EOS_Auth_GetSelectedAccountId (base.InnerHandle, to, ref outSelectedAccountId2);
		Helper.Get (outSelectedAccountId2, out outSelectedAccountId);
		return result;
	}

	public void LinkAccount (ref LinkAccountOptions options, object clientData, OnLinkAccountCallback completionDelegate)
	{
		LinkAccountOptionsInternal options2 = default(LinkAccountOptionsInternal);
		options2.Set (ref options);
		IntPtr clientDataAddress = IntPtr.Zero;
		OnLinkAccountCallbackInternal onLinkAccountCallbackInternal = OnLinkAccountCallbackInternalImplementation;
		Helper.AddCallback (out clientDataAddress, clientData, completionDelegate, onLinkAccountCallbackInternal);
		Bindings.EOS_Auth_LinkAccount (base.InnerHandle, ref options2, clientDataAddress, onLinkAccountCallbackInternal);
		Helper.Dispose (ref options2);
	}

	public void Login (ref LoginOptions options, object clientData, OnLoginCallback completionDelegate)
	{
		LoginOptionsInternal options2 = default(LoginOptionsInternal);
		options2.Set (ref options);
		IntPtr clientDataAddress = IntPtr.Zero;
		OnLoginCallbackInternal onLoginCallbackInternal = OnLoginCallbackInternalImplementation;
		Helper.AddCallback (out clientDataAddress, clientData, completionDelegate, onLoginCallbackInternal);
		Bindings.EOS_Auth_Login (base.InnerHandle, ref options2, clientDataAddress, onLoginCallbackInternal);
		Helper.Dispose (ref options2);
	}

	public void Logout (ref LogoutOptions options, object clientData, OnLogoutCallback completionDelegate)
	{
		LogoutOptionsInternal options2 = default(LogoutOptionsInternal);
		options2.Set (ref options);
		IntPtr clientDataAddress = IntPtr.Zero;
		OnLogoutCallbackInternal onLogoutCallbackInternal = OnLogoutCallbackInternalImplementation;
		Helper.AddCallback (out clientDataAddress, clientData, completionDelegate, onLogoutCallbackInternal);
		Bindings.EOS_Auth_Logout (base.InnerHandle, ref options2, clientDataAddress, onLogoutCallbackInternal);
		Helper.Dispose (ref options2);
	}

	public void QueryIdToken (ref QueryIdTokenOptions options, object clientData, OnQueryIdTokenCallback completionDelegate)
	{
		QueryIdTokenOptionsInternal options2 = default(QueryIdTokenOptionsInternal);
		options2.Set (ref options);
		IntPtr clientDataAddress = IntPtr.Zero;
		OnQueryIdTokenCallbackInternal onQueryIdTokenCallbackInternal = OnQueryIdTokenCallbackInternalImplementation;
		Helper.AddCallback (out clientDataAddress, clientData, completionDelegate, onQueryIdTokenCallbackInternal);
		Bindings.EOS_Auth_QueryIdToken (base.InnerHandle, ref options2, clientDataAddress, onQueryIdTokenCallbackInternal);
		Helper.Dispose (ref options2);
	}

	public void RemoveNotifyLoginStatusChanged (ulong inId)
	{
		Bindings.EOS_Auth_RemoveNotifyLoginStatusChanged (base.InnerHandle, inId);
		Helper.RemoveCallbackByNotificationId (inId);
	}

	public void VerifyIdToken (ref VerifyIdTokenOptions options, object clientData, OnVerifyIdTokenCallback completionDelegate)
	{
		VerifyIdTokenOptionsInternal options2 = default(VerifyIdTokenOptionsInternal);
		options2.Set (ref options);
		IntPtr clientDataAddress = IntPtr.Zero;
		OnVerifyIdTokenCallbackInternal onVerifyIdTokenCallbackInternal = OnVerifyIdTokenCallbackInternalImplementation;
		Helper.AddCallback (out clientDataAddress, clientData, completionDelegate, onVerifyIdTokenCallbackInternal);
		Bindings.EOS_Auth_VerifyIdToken (base.InnerHandle, ref options2, clientDataAddress, onVerifyIdTokenCallbackInternal);
		Helper.Dispose (ref options2);
	}

	public void VerifyUserAuth (ref VerifyUserAuthOptions options, object clientData, OnVerifyUserAuthCallback completionDelegate)
	{
		VerifyUserAuthOptionsInternal options2 = default(VerifyUserAuthOptionsInternal);
		options2.Set (ref options);
		IntPtr clientDataAddress = IntPtr.Zero;
		OnVerifyUserAuthCallbackInternal onVerifyUserAuthCallbackInternal = OnVerifyUserAuthCallbackInternalImplementation;
		Helper.AddCallback (out clientDataAddress, clientData, completionDelegate, onVerifyUserAuthCallbackInternal);
		Bindings.EOS_Auth_VerifyUserAuth (base.InnerHandle, ref options2, clientDataAddress, onVerifyUserAuthCallbackInternal);
		Helper.Dispose (ref options2);
	}

	[MonoPInvokeCallback (typeof(OnDeletePersistentAuthCallbackInternal))]
	internal static void OnDeletePersistentAuthCallbackInternalImplementation (ref DeletePersistentAuthCallbackInfoInternal data)
	{
		if (Helper.TryGetAndRemoveCallback<DeletePersistentAuthCallbackInfoInternal, OnDeletePersistentAuthCallback, DeletePersistentAuthCallbackInfo> (ref data, out var callback, out var callbackInfo)) {
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
		if (Helper.TryGetCallback<LoginStatusChangedCallbackInfoInternal, OnLoginStatusChangedCallback, LoginStatusChangedCallbackInfo> (ref data, out var callback, out var callbackInfo)) {
			callback (ref callbackInfo);
		}
	}

	[MonoPInvokeCallback (typeof(OnLogoutCallbackInternal))]
	internal static void OnLogoutCallbackInternalImplementation (ref LogoutCallbackInfoInternal data)
	{
		if (Helper.TryGetAndRemoveCallback<LogoutCallbackInfoInternal, OnLogoutCallback, LogoutCallbackInfo> (ref data, out var callback, out var callbackInfo)) {
			callback (ref callbackInfo);
		}
	}

	[MonoPInvokeCallback (typeof(OnQueryIdTokenCallbackInternal))]
	internal static void OnQueryIdTokenCallbackInternalImplementation (ref QueryIdTokenCallbackInfoInternal data)
	{
		if (Helper.TryGetAndRemoveCallback<QueryIdTokenCallbackInfoInternal, OnQueryIdTokenCallback, QueryIdTokenCallbackInfo> (ref data, out var callback, out var callbackInfo)) {
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

	[MonoPInvokeCallback (typeof(OnVerifyUserAuthCallbackInternal))]
	internal static void OnVerifyUserAuthCallbackInternalImplementation (ref VerifyUserAuthCallbackInfoInternal data)
	{
		if (Helper.TryGetAndRemoveCallback<VerifyUserAuthCallbackInfoInternal, OnVerifyUserAuthCallback, VerifyUserAuthCallbackInfo> (ref data, out var callback, out var callbackInfo)) {
			callback (ref callbackInfo);
		}
	}

	public void Login (ref IOSLoginOptions options, object clientData, OnLoginCallback completionDelegate)
	{
		IOSLoginOptionsInternal options2 = default(IOSLoginOptionsInternal);
		options2.Set (ref options);
		IntPtr clientDataAddress = IntPtr.Zero;
		OnLoginCallbackInternal onLoginCallbackInternal = OnLoginCallbackInternalImplementation;
		Helper.AddCallback (out clientDataAddress, clientData, completionDelegate, onLoginCallbackInternal);
		IOSBindings.EOS_Auth_Login (base.InnerHandle, ref options2, clientDataAddress, onLoginCallbackInternal);
		Helper.Dispose (ref options2);
	}
}
