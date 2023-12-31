using System;
using Epic.OnlineServices;
using Epic.OnlineServices.Stats;

public sealed class StatsInterface : Handle
{
	public const int CopystatbyindexApiLatest = 1;

	public const int CopystatbynameApiLatest = 1;

	public const int GetstatcountApiLatest = 1;

	public const int GetstatscountApiLatest = 1;

	public const int IngestdataApiLatest = 1;

	public const int IngeststatApiLatest = 3;

	public const int MaxIngestStats = 3000;

	public const int MaxQueryStats = 1000;

	public const int QuerystatsApiLatest = 3;

	public const int StatApiLatest = 1;

	public const int TimeUndefined = -1;

	public StatsInterface ()
	{
	}

	public StatsInterface (IntPtr innerHandle)
		: base (innerHandle)
	{
	}

	public Result CopyStatByIndex (ref CopyStatByIndexOptions options, out Stat? outStat)
	{
		CopyStatByIndexOptionsInternal options2 = default(CopyStatByIndexOptionsInternal);
		options2.Set (ref options);
		IntPtr outStat2 = IntPtr.Zero;
		Result result = Bindings.EOS_Stats_CopyStatByIndex (base.InnerHandle, ref options2, ref outStat2);
		Helper.Dispose (ref options2);
		Helper.Get<StatInternal, Stat> (outStat2, out outStat);
		if (outStat.HasValue) {
			Bindings.EOS_Stats_Stat_Release (outStat2);
		}
		return result;
	}

	public Result CopyStatByName (ref CopyStatByNameOptions options, out Stat? outStat)
	{
		CopyStatByNameOptionsInternal options2 = default(CopyStatByNameOptionsInternal);
		options2.Set (ref options);
		IntPtr outStat2 = IntPtr.Zero;
		Result result = Bindings.EOS_Stats_CopyStatByName (base.InnerHandle, ref options2, ref outStat2);
		Helper.Dispose (ref options2);
		Helper.Get<StatInternal, Stat> (outStat2, out outStat);
		if (outStat.HasValue) {
			Bindings.EOS_Stats_Stat_Release (outStat2);
		}
		return result;
	}

	public uint GetStatsCount (ref GetStatCountOptions options)
	{
		GetStatCountOptionsInternal options2 = default(GetStatCountOptionsInternal);
		options2.Set (ref options);
		uint result = Bindings.EOS_Stats_GetStatsCount (base.InnerHandle, ref options2);
		Helper.Dispose (ref options2);
		return result;
	}

	public void IngestStat (ref IngestStatOptions options, object clientData, OnIngestStatCompleteCallback completionDelegate)
	{
		IngestStatOptionsInternal options2 = default(IngestStatOptionsInternal);
		options2.Set (ref options);
		IntPtr clientDataAddress = IntPtr.Zero;
		OnIngestStatCompleteCallbackInternal onIngestStatCompleteCallbackInternal = OnIngestStatCompleteCallbackInternalImplementation;
		Helper.AddCallback (out clientDataAddress, clientData, completionDelegate, onIngestStatCompleteCallbackInternal);
		Bindings.EOS_Stats_IngestStat (base.InnerHandle, ref options2, clientDataAddress, onIngestStatCompleteCallbackInternal);
		Helper.Dispose (ref options2);
	}

	public void QueryStats (ref QueryStatsOptions options, object clientData, OnQueryStatsCompleteCallback completionDelegate)
	{
		QueryStatsOptionsInternal options2 = default(QueryStatsOptionsInternal);
		options2.Set (ref options);
		IntPtr clientDataAddress = IntPtr.Zero;
		OnQueryStatsCompleteCallbackInternal onQueryStatsCompleteCallbackInternal = OnQueryStatsCompleteCallbackInternalImplementation;
		Helper.AddCallback (out clientDataAddress, clientData, completionDelegate, onQueryStatsCompleteCallbackInternal);
		Bindings.EOS_Stats_QueryStats (base.InnerHandle, ref options2, clientDataAddress, onQueryStatsCompleteCallbackInternal);
		Helper.Dispose (ref options2);
	}

	[MonoPInvokeCallback (typeof(OnIngestStatCompleteCallbackInternal))]
	internal static void OnIngestStatCompleteCallbackInternalImplementation (ref IngestStatCompleteCallbackInfoInternal data)
	{
		if (Helper.TryGetAndRemoveCallback<IngestStatCompleteCallbackInfoInternal, OnIngestStatCompleteCallback, IngestStatCompleteCallbackInfo> (ref data, out var callback, out var callbackInfo)) {
			callback (ref callbackInfo);
		}
	}

	[MonoPInvokeCallback (typeof(OnQueryStatsCompleteCallbackInternal))]
	internal static void OnQueryStatsCompleteCallbackInternalImplementation (ref OnQueryStatsCompleteCallbackInfoInternal data)
	{
		if (Helper.TryGetAndRemoveCallback<OnQueryStatsCompleteCallbackInfoInternal, OnQueryStatsCompleteCallback, OnQueryStatsCompleteCallbackInfo> (ref data, out var callback, out var callbackInfo)) {
			callback (ref callbackInfo);
		}
	}
}
