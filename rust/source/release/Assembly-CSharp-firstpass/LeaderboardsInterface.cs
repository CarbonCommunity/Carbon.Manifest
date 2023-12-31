using System;
using Epic.OnlineServices;
using Epic.OnlineServices.Leaderboards;

public sealed class LeaderboardsInterface : Handle
{
	public const int CopyleaderboarddefinitionbyindexApiLatest = 1;

	public const int CopyleaderboarddefinitionbyleaderboardidApiLatest = 1;

	public const int CopyleaderboardrecordbyindexApiLatest = 2;

	public const int CopyleaderboardrecordbyuseridApiLatest = 2;

	public const int CopyleaderboarduserscorebyindexApiLatest = 1;

	public const int CopyleaderboarduserscorebyuseridApiLatest = 1;

	public const int DefinitionApiLatest = 1;

	public const int GetleaderboarddefinitioncountApiLatest = 1;

	public const int GetleaderboardrecordcountApiLatest = 1;

	public const int GetleaderboarduserscorecountApiLatest = 1;

	public const int LeaderboardrecordApiLatest = 2;

	public const int LeaderboarduserscoreApiLatest = 1;

	public const int QueryleaderboarddefinitionsApiLatest = 2;

	public const int QueryleaderboardranksApiLatest = 2;

	public const int QueryleaderboarduserscoresApiLatest = 2;

	public const int TimeUndefined = -1;

	public const int UserscoresquerystatinfoApiLatest = 1;

	public LeaderboardsInterface ()
	{
	}

	public LeaderboardsInterface (IntPtr innerHandle)
		: base (innerHandle)
	{
	}

	public Result CopyLeaderboardDefinitionByIndex (ref CopyLeaderboardDefinitionByIndexOptions options, out Definition? outLeaderboardDefinition)
	{
		CopyLeaderboardDefinitionByIndexOptionsInternal options2 = default(CopyLeaderboardDefinitionByIndexOptionsInternal);
		options2.Set (ref options);
		IntPtr outLeaderboardDefinition2 = IntPtr.Zero;
		Result result = Bindings.EOS_Leaderboards_CopyLeaderboardDefinitionByIndex (base.InnerHandle, ref options2, ref outLeaderboardDefinition2);
		Helper.Dispose (ref options2);
		Helper.Get<DefinitionInternal, Definition> (outLeaderboardDefinition2, out outLeaderboardDefinition);
		if (outLeaderboardDefinition.HasValue) {
			Bindings.EOS_Leaderboards_Definition_Release (outLeaderboardDefinition2);
		}
		return result;
	}

	public Result CopyLeaderboardDefinitionByLeaderboardId (ref CopyLeaderboardDefinitionByLeaderboardIdOptions options, out Definition? outLeaderboardDefinition)
	{
		CopyLeaderboardDefinitionByLeaderboardIdOptionsInternal options2 = default(CopyLeaderboardDefinitionByLeaderboardIdOptionsInternal);
		options2.Set (ref options);
		IntPtr outLeaderboardDefinition2 = IntPtr.Zero;
		Result result = Bindings.EOS_Leaderboards_CopyLeaderboardDefinitionByLeaderboardId (base.InnerHandle, ref options2, ref outLeaderboardDefinition2);
		Helper.Dispose (ref options2);
		Helper.Get<DefinitionInternal, Definition> (outLeaderboardDefinition2, out outLeaderboardDefinition);
		if (outLeaderboardDefinition.HasValue) {
			Bindings.EOS_Leaderboards_Definition_Release (outLeaderboardDefinition2);
		}
		return result;
	}

	public Result CopyLeaderboardRecordByIndex (ref CopyLeaderboardRecordByIndexOptions options, out LeaderboardRecord? outLeaderboardRecord)
	{
		CopyLeaderboardRecordByIndexOptionsInternal options2 = default(CopyLeaderboardRecordByIndexOptionsInternal);
		options2.Set (ref options);
		IntPtr outLeaderboardRecord2 = IntPtr.Zero;
		Result result = Bindings.EOS_Leaderboards_CopyLeaderboardRecordByIndex (base.InnerHandle, ref options2, ref outLeaderboardRecord2);
		Helper.Dispose (ref options2);
		Helper.Get<LeaderboardRecordInternal, LeaderboardRecord> (outLeaderboardRecord2, out outLeaderboardRecord);
		if (outLeaderboardRecord.HasValue) {
			Bindings.EOS_Leaderboards_LeaderboardRecord_Release (outLeaderboardRecord2);
		}
		return result;
	}

	public Result CopyLeaderboardRecordByUserId (ref CopyLeaderboardRecordByUserIdOptions options, out LeaderboardRecord? outLeaderboardRecord)
	{
		CopyLeaderboardRecordByUserIdOptionsInternal options2 = default(CopyLeaderboardRecordByUserIdOptionsInternal);
		options2.Set (ref options);
		IntPtr outLeaderboardRecord2 = IntPtr.Zero;
		Result result = Bindings.EOS_Leaderboards_CopyLeaderboardRecordByUserId (base.InnerHandle, ref options2, ref outLeaderboardRecord2);
		Helper.Dispose (ref options2);
		Helper.Get<LeaderboardRecordInternal, LeaderboardRecord> (outLeaderboardRecord2, out outLeaderboardRecord);
		if (outLeaderboardRecord.HasValue) {
			Bindings.EOS_Leaderboards_LeaderboardRecord_Release (outLeaderboardRecord2);
		}
		return result;
	}

	public Result CopyLeaderboardUserScoreByIndex (ref CopyLeaderboardUserScoreByIndexOptions options, out LeaderboardUserScore? outLeaderboardUserScore)
	{
		CopyLeaderboardUserScoreByIndexOptionsInternal options2 = default(CopyLeaderboardUserScoreByIndexOptionsInternal);
		options2.Set (ref options);
		IntPtr outLeaderboardUserScore2 = IntPtr.Zero;
		Result result = Bindings.EOS_Leaderboards_CopyLeaderboardUserScoreByIndex (base.InnerHandle, ref options2, ref outLeaderboardUserScore2);
		Helper.Dispose (ref options2);
		Helper.Get<LeaderboardUserScoreInternal, LeaderboardUserScore> (outLeaderboardUserScore2, out outLeaderboardUserScore);
		if (outLeaderboardUserScore.HasValue) {
			Bindings.EOS_Leaderboards_LeaderboardUserScore_Release (outLeaderboardUserScore2);
		}
		return result;
	}

	public Result CopyLeaderboardUserScoreByUserId (ref CopyLeaderboardUserScoreByUserIdOptions options, out LeaderboardUserScore? outLeaderboardUserScore)
	{
		CopyLeaderboardUserScoreByUserIdOptionsInternal options2 = default(CopyLeaderboardUserScoreByUserIdOptionsInternal);
		options2.Set (ref options);
		IntPtr outLeaderboardUserScore2 = IntPtr.Zero;
		Result result = Bindings.EOS_Leaderboards_CopyLeaderboardUserScoreByUserId (base.InnerHandle, ref options2, ref outLeaderboardUserScore2);
		Helper.Dispose (ref options2);
		Helper.Get<LeaderboardUserScoreInternal, LeaderboardUserScore> (outLeaderboardUserScore2, out outLeaderboardUserScore);
		if (outLeaderboardUserScore.HasValue) {
			Bindings.EOS_Leaderboards_LeaderboardUserScore_Release (outLeaderboardUserScore2);
		}
		return result;
	}

	public uint GetLeaderboardDefinitionCount (ref GetLeaderboardDefinitionCountOptions options)
	{
		GetLeaderboardDefinitionCountOptionsInternal options2 = default(GetLeaderboardDefinitionCountOptionsInternal);
		options2.Set (ref options);
		uint result = Bindings.EOS_Leaderboards_GetLeaderboardDefinitionCount (base.InnerHandle, ref options2);
		Helper.Dispose (ref options2);
		return result;
	}

	public uint GetLeaderboardRecordCount (ref GetLeaderboardRecordCountOptions options)
	{
		GetLeaderboardRecordCountOptionsInternal options2 = default(GetLeaderboardRecordCountOptionsInternal);
		options2.Set (ref options);
		uint result = Bindings.EOS_Leaderboards_GetLeaderboardRecordCount (base.InnerHandle, ref options2);
		Helper.Dispose (ref options2);
		return result;
	}

	public uint GetLeaderboardUserScoreCount (ref GetLeaderboardUserScoreCountOptions options)
	{
		GetLeaderboardUserScoreCountOptionsInternal options2 = default(GetLeaderboardUserScoreCountOptionsInternal);
		options2.Set (ref options);
		uint result = Bindings.EOS_Leaderboards_GetLeaderboardUserScoreCount (base.InnerHandle, ref options2);
		Helper.Dispose (ref options2);
		return result;
	}

	public void QueryLeaderboardDefinitions (ref QueryLeaderboardDefinitionsOptions options, object clientData, OnQueryLeaderboardDefinitionsCompleteCallback completionDelegate)
	{
		QueryLeaderboardDefinitionsOptionsInternal options2 = default(QueryLeaderboardDefinitionsOptionsInternal);
		options2.Set (ref options);
		IntPtr clientDataAddress = IntPtr.Zero;
		OnQueryLeaderboardDefinitionsCompleteCallbackInternal onQueryLeaderboardDefinitionsCompleteCallbackInternal = OnQueryLeaderboardDefinitionsCompleteCallbackInternalImplementation;
		Helper.AddCallback (out clientDataAddress, clientData, completionDelegate, onQueryLeaderboardDefinitionsCompleteCallbackInternal);
		Bindings.EOS_Leaderboards_QueryLeaderboardDefinitions (base.InnerHandle, ref options2, clientDataAddress, onQueryLeaderboardDefinitionsCompleteCallbackInternal);
		Helper.Dispose (ref options2);
	}

	public void QueryLeaderboardRanks (ref QueryLeaderboardRanksOptions options, object clientData, OnQueryLeaderboardRanksCompleteCallback completionDelegate)
	{
		QueryLeaderboardRanksOptionsInternal options2 = default(QueryLeaderboardRanksOptionsInternal);
		options2.Set (ref options);
		IntPtr clientDataAddress = IntPtr.Zero;
		OnQueryLeaderboardRanksCompleteCallbackInternal onQueryLeaderboardRanksCompleteCallbackInternal = OnQueryLeaderboardRanksCompleteCallbackInternalImplementation;
		Helper.AddCallback (out clientDataAddress, clientData, completionDelegate, onQueryLeaderboardRanksCompleteCallbackInternal);
		Bindings.EOS_Leaderboards_QueryLeaderboardRanks (base.InnerHandle, ref options2, clientDataAddress, onQueryLeaderboardRanksCompleteCallbackInternal);
		Helper.Dispose (ref options2);
	}

	public void QueryLeaderboardUserScores (ref QueryLeaderboardUserScoresOptions options, object clientData, OnQueryLeaderboardUserScoresCompleteCallback completionDelegate)
	{
		QueryLeaderboardUserScoresOptionsInternal options2 = default(QueryLeaderboardUserScoresOptionsInternal);
		options2.Set (ref options);
		IntPtr clientDataAddress = IntPtr.Zero;
		OnQueryLeaderboardUserScoresCompleteCallbackInternal onQueryLeaderboardUserScoresCompleteCallbackInternal = OnQueryLeaderboardUserScoresCompleteCallbackInternalImplementation;
		Helper.AddCallback (out clientDataAddress, clientData, completionDelegate, onQueryLeaderboardUserScoresCompleteCallbackInternal);
		Bindings.EOS_Leaderboards_QueryLeaderboardUserScores (base.InnerHandle, ref options2, clientDataAddress, onQueryLeaderboardUserScoresCompleteCallbackInternal);
		Helper.Dispose (ref options2);
	}

	[MonoPInvokeCallback (typeof(OnQueryLeaderboardDefinitionsCompleteCallbackInternal))]
	internal static void OnQueryLeaderboardDefinitionsCompleteCallbackInternalImplementation (ref OnQueryLeaderboardDefinitionsCompleteCallbackInfoInternal data)
	{
		if (Helper.TryGetAndRemoveCallback<OnQueryLeaderboardDefinitionsCompleteCallbackInfoInternal, OnQueryLeaderboardDefinitionsCompleteCallback, OnQueryLeaderboardDefinitionsCompleteCallbackInfo> (ref data, out var callback, out var callbackInfo)) {
			callback (ref callbackInfo);
		}
	}

	[MonoPInvokeCallback (typeof(OnQueryLeaderboardRanksCompleteCallbackInternal))]
	internal static void OnQueryLeaderboardRanksCompleteCallbackInternalImplementation (ref OnQueryLeaderboardRanksCompleteCallbackInfoInternal data)
	{
		if (Helper.TryGetAndRemoveCallback<OnQueryLeaderboardRanksCompleteCallbackInfoInternal, OnQueryLeaderboardRanksCompleteCallback, OnQueryLeaderboardRanksCompleteCallbackInfo> (ref data, out var callback, out var callbackInfo)) {
			callback (ref callbackInfo);
		}
	}

	[MonoPInvokeCallback (typeof(OnQueryLeaderboardUserScoresCompleteCallbackInternal))]
	internal static void OnQueryLeaderboardUserScoresCompleteCallbackInternalImplementation (ref OnQueryLeaderboardUserScoresCompleteCallbackInfoInternal data)
	{
		if (Helper.TryGetAndRemoveCallback<OnQueryLeaderboardUserScoresCompleteCallbackInfoInternal, OnQueryLeaderboardUserScoresCompleteCallback, OnQueryLeaderboardUserScoresCompleteCallbackInfo> (ref data, out var callback, out var callbackInfo)) {
			callback (ref callbackInfo);
		}
	}
}
