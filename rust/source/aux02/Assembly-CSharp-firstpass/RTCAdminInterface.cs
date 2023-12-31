using System;
using Epic.OnlineServices;
using Epic.OnlineServices.RTCAdmin;

public sealed class RTCAdminInterface : Handle
{
	public const int CopyusertokenbyindexApiLatest = 2;

	public const int CopyusertokenbyuseridApiLatest = 2;

	public const int KickApiLatest = 1;

	public const int QueryjoinroomtokenApiLatest = 2;

	public const int SetparticipanthardmuteApiLatest = 1;

	public const int UsertokenApiLatest = 1;

	public RTCAdminInterface ()
	{
	}

	public RTCAdminInterface (IntPtr innerHandle)
		: base (innerHandle)
	{
	}

	public Result CopyUserTokenByIndex (ref CopyUserTokenByIndexOptions options, out UserToken? outUserToken)
	{
		CopyUserTokenByIndexOptionsInternal options2 = default(CopyUserTokenByIndexOptionsInternal);
		options2.Set (ref options);
		IntPtr outUserToken2 = IntPtr.Zero;
		Result result = Bindings.EOS_RTCAdmin_CopyUserTokenByIndex (base.InnerHandle, ref options2, ref outUserToken2);
		Helper.Dispose (ref options2);
		Helper.Get<UserTokenInternal, UserToken> (outUserToken2, out outUserToken);
		if (outUserToken.HasValue) {
			Bindings.EOS_RTCAdmin_UserToken_Release (outUserToken2);
		}
		return result;
	}

	public Result CopyUserTokenByUserId (ref CopyUserTokenByUserIdOptions options, out UserToken? outUserToken)
	{
		CopyUserTokenByUserIdOptionsInternal options2 = default(CopyUserTokenByUserIdOptionsInternal);
		options2.Set (ref options);
		IntPtr outUserToken2 = IntPtr.Zero;
		Result result = Bindings.EOS_RTCAdmin_CopyUserTokenByUserId (base.InnerHandle, ref options2, ref outUserToken2);
		Helper.Dispose (ref options2);
		Helper.Get<UserTokenInternal, UserToken> (outUserToken2, out outUserToken);
		if (outUserToken.HasValue) {
			Bindings.EOS_RTCAdmin_UserToken_Release (outUserToken2);
		}
		return result;
	}

	public void Kick (ref KickOptions options, object clientData, OnKickCompleteCallback completionDelegate)
	{
		KickOptionsInternal options2 = default(KickOptionsInternal);
		options2.Set (ref options);
		IntPtr clientDataAddress = IntPtr.Zero;
		OnKickCompleteCallbackInternal onKickCompleteCallbackInternal = OnKickCompleteCallbackInternalImplementation;
		Helper.AddCallback (out clientDataAddress, clientData, completionDelegate, onKickCompleteCallbackInternal);
		Bindings.EOS_RTCAdmin_Kick (base.InnerHandle, ref options2, clientDataAddress, onKickCompleteCallbackInternal);
		Helper.Dispose (ref options2);
	}

	public void QueryJoinRoomToken (ref QueryJoinRoomTokenOptions options, object clientData, OnQueryJoinRoomTokenCompleteCallback completionDelegate)
	{
		QueryJoinRoomTokenOptionsInternal options2 = default(QueryJoinRoomTokenOptionsInternal);
		options2.Set (ref options);
		IntPtr clientDataAddress = IntPtr.Zero;
		OnQueryJoinRoomTokenCompleteCallbackInternal onQueryJoinRoomTokenCompleteCallbackInternal = OnQueryJoinRoomTokenCompleteCallbackInternalImplementation;
		Helper.AddCallback (out clientDataAddress, clientData, completionDelegate, onQueryJoinRoomTokenCompleteCallbackInternal);
		Bindings.EOS_RTCAdmin_QueryJoinRoomToken (base.InnerHandle, ref options2, clientDataAddress, onQueryJoinRoomTokenCompleteCallbackInternal);
		Helper.Dispose (ref options2);
	}

	public void SetParticipantHardMute (ref SetParticipantHardMuteOptions options, object clientData, OnSetParticipantHardMuteCompleteCallback completionDelegate)
	{
		SetParticipantHardMuteOptionsInternal options2 = default(SetParticipantHardMuteOptionsInternal);
		options2.Set (ref options);
		IntPtr clientDataAddress = IntPtr.Zero;
		OnSetParticipantHardMuteCompleteCallbackInternal onSetParticipantHardMuteCompleteCallbackInternal = OnSetParticipantHardMuteCompleteCallbackInternalImplementation;
		Helper.AddCallback (out clientDataAddress, clientData, completionDelegate, onSetParticipantHardMuteCompleteCallbackInternal);
		Bindings.EOS_RTCAdmin_SetParticipantHardMute (base.InnerHandle, ref options2, clientDataAddress, onSetParticipantHardMuteCompleteCallbackInternal);
		Helper.Dispose (ref options2);
	}

	[MonoPInvokeCallback (typeof(OnKickCompleteCallbackInternal))]
	internal static void OnKickCompleteCallbackInternalImplementation (ref KickCompleteCallbackInfoInternal data)
	{
		if (Helper.TryGetAndRemoveCallback<KickCompleteCallbackInfoInternal, OnKickCompleteCallback, KickCompleteCallbackInfo> (ref data, out var callback, out var callbackInfo)) {
			callback (ref callbackInfo);
		}
	}

	[MonoPInvokeCallback (typeof(OnQueryJoinRoomTokenCompleteCallbackInternal))]
	internal static void OnQueryJoinRoomTokenCompleteCallbackInternalImplementation (ref QueryJoinRoomTokenCompleteCallbackInfoInternal data)
	{
		if (Helper.TryGetAndRemoveCallback<QueryJoinRoomTokenCompleteCallbackInfoInternal, OnQueryJoinRoomTokenCompleteCallback, QueryJoinRoomTokenCompleteCallbackInfo> (ref data, out var callback, out var callbackInfo)) {
			callback (ref callbackInfo);
		}
	}

	[MonoPInvokeCallback (typeof(OnSetParticipantHardMuteCompleteCallbackInternal))]
	internal static void OnSetParticipantHardMuteCompleteCallbackInternalImplementation (ref SetParticipantHardMuteCompleteCallbackInfoInternal data)
	{
		if (Helper.TryGetAndRemoveCallback<SetParticipantHardMuteCompleteCallbackInfoInternal, OnSetParticipantHardMuteCompleteCallback, SetParticipantHardMuteCompleteCallbackInfo> (ref data, out var callback, out var callbackInfo)) {
			callback (ref callbackInfo);
		}
	}
}
