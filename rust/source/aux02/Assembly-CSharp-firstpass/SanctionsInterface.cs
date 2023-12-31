using System;
using Epic.OnlineServices;
using Epic.OnlineServices.Sanctions;

public sealed class SanctionsInterface : Handle
{
	public const int CopyplayersanctionbyindexApiLatest = 1;

	public const int GetplayersanctioncountApiLatest = 1;

	public const int PlayersanctionApiLatest = 2;

	public const int QueryactiveplayersanctionsApiLatest = 2;

	public SanctionsInterface ()
	{
	}

	public SanctionsInterface (IntPtr innerHandle)
		: base (innerHandle)
	{
	}

	public Result CopyPlayerSanctionByIndex (ref CopyPlayerSanctionByIndexOptions options, out PlayerSanction? outSanction)
	{
		CopyPlayerSanctionByIndexOptionsInternal options2 = default(CopyPlayerSanctionByIndexOptionsInternal);
		options2.Set (ref options);
		IntPtr outSanction2 = IntPtr.Zero;
		Result result = Bindings.EOS_Sanctions_CopyPlayerSanctionByIndex (base.InnerHandle, ref options2, ref outSanction2);
		Helper.Dispose (ref options2);
		Helper.Get<PlayerSanctionInternal, PlayerSanction> (outSanction2, out outSanction);
		if (outSanction.HasValue) {
			Bindings.EOS_Sanctions_PlayerSanction_Release (outSanction2);
		}
		return result;
	}

	public uint GetPlayerSanctionCount (ref GetPlayerSanctionCountOptions options)
	{
		GetPlayerSanctionCountOptionsInternal options2 = default(GetPlayerSanctionCountOptionsInternal);
		options2.Set (ref options);
		uint result = Bindings.EOS_Sanctions_GetPlayerSanctionCount (base.InnerHandle, ref options2);
		Helper.Dispose (ref options2);
		return result;
	}

	public void QueryActivePlayerSanctions (ref QueryActivePlayerSanctionsOptions options, object clientData, OnQueryActivePlayerSanctionsCallback completionDelegate)
	{
		QueryActivePlayerSanctionsOptionsInternal options2 = default(QueryActivePlayerSanctionsOptionsInternal);
		options2.Set (ref options);
		IntPtr clientDataAddress = IntPtr.Zero;
		OnQueryActivePlayerSanctionsCallbackInternal onQueryActivePlayerSanctionsCallbackInternal = OnQueryActivePlayerSanctionsCallbackInternalImplementation;
		Helper.AddCallback (out clientDataAddress, clientData, completionDelegate, onQueryActivePlayerSanctionsCallbackInternal);
		Bindings.EOS_Sanctions_QueryActivePlayerSanctions (base.InnerHandle, ref options2, clientDataAddress, onQueryActivePlayerSanctionsCallbackInternal);
		Helper.Dispose (ref options2);
	}

	[MonoPInvokeCallback (typeof(OnQueryActivePlayerSanctionsCallbackInternal))]
	internal static void OnQueryActivePlayerSanctionsCallbackInternalImplementation (ref QueryActivePlayerSanctionsCallbackInfoInternal data)
	{
		if (Helper.TryGetAndRemoveCallback<QueryActivePlayerSanctionsCallbackInfoInternal, OnQueryActivePlayerSanctionsCallback, QueryActivePlayerSanctionsCallbackInfo> (ref data, out var callback, out var callbackInfo)) {
			callback (ref callbackInfo);
		}
	}
}
