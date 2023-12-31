using System;
using Epic.OnlineServices;
using Epic.OnlineServices.Sessions;

public sealed class ActiveSession : Handle
{
	public const int ActivesessionCopyinfoApiLatest = 1;

	public const int ActivesessionGetregisteredplayerbyindexApiLatest = 1;

	public const int ActivesessionGetregisteredplayercountApiLatest = 1;

	public const int ActivesessionInfoApiLatest = 1;

	public ActiveSession ()
	{
	}

	public ActiveSession (IntPtr innerHandle)
		: base (innerHandle)
	{
	}

	public Result CopyInfo (ref ActiveSessionCopyInfoOptions options, out ActiveSessionInfo? outActiveSessionInfo)
	{
		ActiveSessionCopyInfoOptionsInternal options2 = default(ActiveSessionCopyInfoOptionsInternal);
		options2.Set (ref options);
		IntPtr outActiveSessionInfo2 = IntPtr.Zero;
		Result result = Bindings.EOS_ActiveSession_CopyInfo (base.InnerHandle, ref options2, ref outActiveSessionInfo2);
		Helper.Dispose (ref options2);
		Helper.Get<ActiveSessionInfoInternal, ActiveSessionInfo> (outActiveSessionInfo2, out outActiveSessionInfo);
		if (outActiveSessionInfo.HasValue) {
			Bindings.EOS_ActiveSession_Info_Release (outActiveSessionInfo2);
		}
		return result;
	}

	public ProductUserId GetRegisteredPlayerByIndex (ref ActiveSessionGetRegisteredPlayerByIndexOptions options)
	{
		ActiveSessionGetRegisteredPlayerByIndexOptionsInternal options2 = default(ActiveSessionGetRegisteredPlayerByIndexOptionsInternal);
		options2.Set (ref options);
		IntPtr from = Bindings.EOS_ActiveSession_GetRegisteredPlayerByIndex (base.InnerHandle, ref options2);
		Helper.Dispose (ref options2);
		Helper.Get (from, out ProductUserId to);
		return to;
	}

	public uint GetRegisteredPlayerCount (ref ActiveSessionGetRegisteredPlayerCountOptions options)
	{
		ActiveSessionGetRegisteredPlayerCountOptionsInternal options2 = default(ActiveSessionGetRegisteredPlayerCountOptionsInternal);
		options2.Set (ref options);
		uint result = Bindings.EOS_ActiveSession_GetRegisteredPlayerCount (base.InnerHandle, ref options2);
		Helper.Dispose (ref options2);
		return result;
	}

	public void Release ()
	{
		Bindings.EOS_ActiveSession_Release (base.InnerHandle);
	}
}
