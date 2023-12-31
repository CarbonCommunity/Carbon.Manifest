using System;
using Epic.OnlineServices;
using Epic.OnlineServices.UserInfo;

public sealed class UserInfoInterface : Handle
{
	public const int BestdisplaynameApiLatest = 1;

	public const int CopybestdisplaynameApiLatest = 1;

	public const int CopybestdisplaynamewithplatformApiLatest = 1;

	public const int CopyexternaluserinfobyaccountidApiLatest = 1;

	public const int CopyexternaluserinfobyaccounttypeApiLatest = 1;

	public const int CopyexternaluserinfobyindexApiLatest = 1;

	public const int CopyuserinfoApiLatest = 3;

	public const int ExternaluserinfoApiLatest = 2;

	public const int GetexternaluserinfocountApiLatest = 1;

	public const int GetlocalplatformtypeApiLatest = 1;

	public const int MaxDisplaynameCharacters = 16;

	public const int MaxDisplaynameUtf8Length = 64;

	public const int QueryuserinfoApiLatest = 1;

	public const int QueryuserinfobydisplaynameApiLatest = 1;

	public const int QueryuserinfobyexternalaccountApiLatest = 1;

	public UserInfoInterface ()
	{
	}

	public UserInfoInterface (IntPtr innerHandle)
		: base (innerHandle)
	{
	}

	public Result CopyBestDisplayName (ref CopyBestDisplayNameOptions options, out BestDisplayName? outBestDisplayName)
	{
		CopyBestDisplayNameOptionsInternal options2 = default(CopyBestDisplayNameOptionsInternal);
		options2.Set (ref options);
		IntPtr outBestDisplayName2 = IntPtr.Zero;
		Result result = Bindings.EOS_UserInfo_CopyBestDisplayName (base.InnerHandle, ref options2, ref outBestDisplayName2);
		Helper.Dispose (ref options2);
		Helper.Get<BestDisplayNameInternal, BestDisplayName> (outBestDisplayName2, out outBestDisplayName);
		if (outBestDisplayName.HasValue) {
			Bindings.EOS_UserInfo_BestDisplayName_Release (outBestDisplayName2);
		}
		return result;
	}

	public Result CopyBestDisplayNameWithPlatform (ref CopyBestDisplayNameWithPlatformOptions options, out BestDisplayName? outBestDisplayName)
	{
		CopyBestDisplayNameWithPlatformOptionsInternal options2 = default(CopyBestDisplayNameWithPlatformOptionsInternal);
		options2.Set (ref options);
		IntPtr outBestDisplayName2 = IntPtr.Zero;
		Result result = Bindings.EOS_UserInfo_CopyBestDisplayNameWithPlatform (base.InnerHandle, ref options2, ref outBestDisplayName2);
		Helper.Dispose (ref options2);
		Helper.Get<BestDisplayNameInternal, BestDisplayName> (outBestDisplayName2, out outBestDisplayName);
		if (outBestDisplayName.HasValue) {
			Bindings.EOS_UserInfo_BestDisplayName_Release (outBestDisplayName2);
		}
		return result;
	}

	public Result CopyExternalUserInfoByAccountId (ref CopyExternalUserInfoByAccountIdOptions options, out ExternalUserInfo? outExternalUserInfo)
	{
		CopyExternalUserInfoByAccountIdOptionsInternal options2 = default(CopyExternalUserInfoByAccountIdOptionsInternal);
		options2.Set (ref options);
		IntPtr outExternalUserInfo2 = IntPtr.Zero;
		Result result = Bindings.EOS_UserInfo_CopyExternalUserInfoByAccountId (base.InnerHandle, ref options2, ref outExternalUserInfo2);
		Helper.Dispose (ref options2);
		Helper.Get<ExternalUserInfoInternal, ExternalUserInfo> (outExternalUserInfo2, out outExternalUserInfo);
		if (outExternalUserInfo.HasValue) {
			Bindings.EOS_UserInfo_ExternalUserInfo_Release (outExternalUserInfo2);
		}
		return result;
	}

	public Result CopyExternalUserInfoByAccountType (ref CopyExternalUserInfoByAccountTypeOptions options, out ExternalUserInfo? outExternalUserInfo)
	{
		CopyExternalUserInfoByAccountTypeOptionsInternal options2 = default(CopyExternalUserInfoByAccountTypeOptionsInternal);
		options2.Set (ref options);
		IntPtr outExternalUserInfo2 = IntPtr.Zero;
		Result result = Bindings.EOS_UserInfo_CopyExternalUserInfoByAccountType (base.InnerHandle, ref options2, ref outExternalUserInfo2);
		Helper.Dispose (ref options2);
		Helper.Get<ExternalUserInfoInternal, ExternalUserInfo> (outExternalUserInfo2, out outExternalUserInfo);
		if (outExternalUserInfo.HasValue) {
			Bindings.EOS_UserInfo_ExternalUserInfo_Release (outExternalUserInfo2);
		}
		return result;
	}

	public Result CopyExternalUserInfoByIndex (ref CopyExternalUserInfoByIndexOptions options, out ExternalUserInfo? outExternalUserInfo)
	{
		CopyExternalUserInfoByIndexOptionsInternal options2 = default(CopyExternalUserInfoByIndexOptionsInternal);
		options2.Set (ref options);
		IntPtr outExternalUserInfo2 = IntPtr.Zero;
		Result result = Bindings.EOS_UserInfo_CopyExternalUserInfoByIndex (base.InnerHandle, ref options2, ref outExternalUserInfo2);
		Helper.Dispose (ref options2);
		Helper.Get<ExternalUserInfoInternal, ExternalUserInfo> (outExternalUserInfo2, out outExternalUserInfo);
		if (outExternalUserInfo.HasValue) {
			Bindings.EOS_UserInfo_ExternalUserInfo_Release (outExternalUserInfo2);
		}
		return result;
	}

	public Result CopyUserInfo (ref CopyUserInfoOptions options, out UserInfoData? outUserInfo)
	{
		CopyUserInfoOptionsInternal options2 = default(CopyUserInfoOptionsInternal);
		options2.Set (ref options);
		IntPtr outUserInfo2 = IntPtr.Zero;
		Result result = Bindings.EOS_UserInfo_CopyUserInfo (base.InnerHandle, ref options2, ref outUserInfo2);
		Helper.Dispose (ref options2);
		Helper.Get<UserInfoDataInternal, UserInfoData> (outUserInfo2, out outUserInfo);
		if (outUserInfo.HasValue) {
			Bindings.EOS_UserInfo_Release (outUserInfo2);
		}
		return result;
	}

	public uint GetExternalUserInfoCount (ref GetExternalUserInfoCountOptions options)
	{
		GetExternalUserInfoCountOptionsInternal options2 = default(GetExternalUserInfoCountOptionsInternal);
		options2.Set (ref options);
		uint result = Bindings.EOS_UserInfo_GetExternalUserInfoCount (base.InnerHandle, ref options2);
		Helper.Dispose (ref options2);
		return result;
	}

	public uint GetLocalPlatformType (ref GetLocalPlatformTypeOptions options)
	{
		GetLocalPlatformTypeOptionsInternal options2 = default(GetLocalPlatformTypeOptionsInternal);
		options2.Set (ref options);
		uint result = Bindings.EOS_UserInfo_GetLocalPlatformType (base.InnerHandle, ref options2);
		Helper.Dispose (ref options2);
		return result;
	}

	public void QueryUserInfo (ref QueryUserInfoOptions options, object clientData, OnQueryUserInfoCallback completionDelegate)
	{
		QueryUserInfoOptionsInternal options2 = default(QueryUserInfoOptionsInternal);
		options2.Set (ref options);
		IntPtr clientDataAddress = IntPtr.Zero;
		OnQueryUserInfoCallbackInternal onQueryUserInfoCallbackInternal = OnQueryUserInfoCallbackInternalImplementation;
		Helper.AddCallback (out clientDataAddress, clientData, completionDelegate, onQueryUserInfoCallbackInternal);
		Bindings.EOS_UserInfo_QueryUserInfo (base.InnerHandle, ref options2, clientDataAddress, onQueryUserInfoCallbackInternal);
		Helper.Dispose (ref options2);
	}

	public void QueryUserInfoByDisplayName (ref QueryUserInfoByDisplayNameOptions options, object clientData, OnQueryUserInfoByDisplayNameCallback completionDelegate)
	{
		QueryUserInfoByDisplayNameOptionsInternal options2 = default(QueryUserInfoByDisplayNameOptionsInternal);
		options2.Set (ref options);
		IntPtr clientDataAddress = IntPtr.Zero;
		OnQueryUserInfoByDisplayNameCallbackInternal onQueryUserInfoByDisplayNameCallbackInternal = OnQueryUserInfoByDisplayNameCallbackInternalImplementation;
		Helper.AddCallback (out clientDataAddress, clientData, completionDelegate, onQueryUserInfoByDisplayNameCallbackInternal);
		Bindings.EOS_UserInfo_QueryUserInfoByDisplayName (base.InnerHandle, ref options2, clientDataAddress, onQueryUserInfoByDisplayNameCallbackInternal);
		Helper.Dispose (ref options2);
	}

	public void QueryUserInfoByExternalAccount (ref QueryUserInfoByExternalAccountOptions options, object clientData, OnQueryUserInfoByExternalAccountCallback completionDelegate)
	{
		QueryUserInfoByExternalAccountOptionsInternal options2 = default(QueryUserInfoByExternalAccountOptionsInternal);
		options2.Set (ref options);
		IntPtr clientDataAddress = IntPtr.Zero;
		OnQueryUserInfoByExternalAccountCallbackInternal onQueryUserInfoByExternalAccountCallbackInternal = OnQueryUserInfoByExternalAccountCallbackInternalImplementation;
		Helper.AddCallback (out clientDataAddress, clientData, completionDelegate, onQueryUserInfoByExternalAccountCallbackInternal);
		Bindings.EOS_UserInfo_QueryUserInfoByExternalAccount (base.InnerHandle, ref options2, clientDataAddress, onQueryUserInfoByExternalAccountCallbackInternal);
		Helper.Dispose (ref options2);
	}

	[MonoPInvokeCallback (typeof(OnQueryUserInfoByDisplayNameCallbackInternal))]
	internal static void OnQueryUserInfoByDisplayNameCallbackInternalImplementation (ref QueryUserInfoByDisplayNameCallbackInfoInternal data)
	{
		if (Helper.TryGetAndRemoveCallback<QueryUserInfoByDisplayNameCallbackInfoInternal, OnQueryUserInfoByDisplayNameCallback, QueryUserInfoByDisplayNameCallbackInfo> (ref data, out var callback, out var callbackInfo)) {
			callback (ref callbackInfo);
		}
	}

	[MonoPInvokeCallback (typeof(OnQueryUserInfoByExternalAccountCallbackInternal))]
	internal static void OnQueryUserInfoByExternalAccountCallbackInternalImplementation (ref QueryUserInfoByExternalAccountCallbackInfoInternal data)
	{
		if (Helper.TryGetAndRemoveCallback<QueryUserInfoByExternalAccountCallbackInfoInternal, OnQueryUserInfoByExternalAccountCallback, QueryUserInfoByExternalAccountCallbackInfo> (ref data, out var callback, out var callbackInfo)) {
			callback (ref callbackInfo);
		}
	}

	[MonoPInvokeCallback (typeof(OnQueryUserInfoCallbackInternal))]
	internal static void OnQueryUserInfoCallbackInternalImplementation (ref QueryUserInfoCallbackInfoInternal data)
	{
		if (Helper.TryGetAndRemoveCallback<QueryUserInfoCallbackInfoInternal, OnQueryUserInfoCallback, QueryUserInfoCallbackInfo> (ref data, out var callback, out var callbackInfo)) {
			callback (ref callbackInfo);
		}
	}
}
