using System;
using Epic.OnlineServices;
using Epic.OnlineServices.Achievements;

public sealed class AchievementsInterface : Handle
{
	public const int AchievementUnlocktimeUndefined = -1;

	public const int AddnotifyachievementsunlockedApiLatest = 1;

	public const int Addnotifyachievementsunlockedv2ApiLatest = 2;

	public const int Copyachievementdefinitionv2ByachievementidApiLatest = 2;

	public const int Copyachievementdefinitionv2ByindexApiLatest = 2;

	public const int CopydefinitionbyachievementidApiLatest = 1;

	public const int CopydefinitionbyindexApiLatest = 1;

	public const int Copydefinitionv2ByachievementidApiLatest = 2;

	public const int Copydefinitionv2ByindexApiLatest = 2;

	public const int CopyplayerachievementbyachievementidApiLatest = 2;

	public const int CopyplayerachievementbyindexApiLatest = 2;

	public const int CopyunlockedachievementbyachievementidApiLatest = 1;

	public const int CopyunlockedachievementbyindexApiLatest = 1;

	public const int DefinitionApiLatest = 1;

	public const int Definitionv2ApiLatest = 2;

	public const int GetachievementdefinitioncountApiLatest = 1;

	public const int GetplayerachievementcountApiLatest = 1;

	public const int GetunlockedachievementcountApiLatest = 1;

	public const int PlayerachievementApiLatest = 2;

	public const int PlayerstatinfoApiLatest = 1;

	public const int QuerydefinitionsApiLatest = 3;

	public const int QueryplayerachievementsApiLatest = 2;

	public const int StatthresholdApiLatest = 1;

	public const int StatthresholdsApiLatest = 1;

	public const int UnlockachievementsApiLatest = 1;

	public const int UnlockedachievementApiLatest = 1;

	public AchievementsInterface ()
	{
	}

	public AchievementsInterface (IntPtr innerHandle)
		: base (innerHandle)
	{
	}

	public ulong AddNotifyAchievementsUnlocked (ref AddNotifyAchievementsUnlockedOptions options, object clientData, OnAchievementsUnlockedCallback notificationFn)
	{
		AddNotifyAchievementsUnlockedOptionsInternal options2 = default(AddNotifyAchievementsUnlockedOptionsInternal);
		options2.Set (ref options);
		IntPtr clientDataAddress = IntPtr.Zero;
		OnAchievementsUnlockedCallbackInternal onAchievementsUnlockedCallbackInternal = OnAchievementsUnlockedCallbackInternalImplementation;
		Helper.AddCallback (out clientDataAddress, clientData, notificationFn, onAchievementsUnlockedCallbackInternal);
		ulong num = Bindings.EOS_Achievements_AddNotifyAchievementsUnlocked (base.InnerHandle, ref options2, clientDataAddress, onAchievementsUnlockedCallbackInternal);
		Helper.Dispose (ref options2);
		Helper.AssignNotificationIdToCallback (clientDataAddress, num);
		return num;
	}

	public ulong AddNotifyAchievementsUnlockedV2 (ref AddNotifyAchievementsUnlockedV2Options options, object clientData, OnAchievementsUnlockedCallbackV2 notificationFn)
	{
		AddNotifyAchievementsUnlockedV2OptionsInternal options2 = default(AddNotifyAchievementsUnlockedV2OptionsInternal);
		options2.Set (ref options);
		IntPtr clientDataAddress = IntPtr.Zero;
		OnAchievementsUnlockedCallbackV2Internal onAchievementsUnlockedCallbackV2Internal = OnAchievementsUnlockedCallbackV2InternalImplementation;
		Helper.AddCallback (out clientDataAddress, clientData, notificationFn, onAchievementsUnlockedCallbackV2Internal);
		ulong num = Bindings.EOS_Achievements_AddNotifyAchievementsUnlockedV2 (base.InnerHandle, ref options2, clientDataAddress, onAchievementsUnlockedCallbackV2Internal);
		Helper.Dispose (ref options2);
		Helper.AssignNotificationIdToCallback (clientDataAddress, num);
		return num;
	}

	public Result CopyAchievementDefinitionByAchievementId (ref CopyAchievementDefinitionByAchievementIdOptions options, out Definition? outDefinition)
	{
		CopyAchievementDefinitionByAchievementIdOptionsInternal options2 = default(CopyAchievementDefinitionByAchievementIdOptionsInternal);
		options2.Set (ref options);
		IntPtr outDefinition2 = IntPtr.Zero;
		Result result = Bindings.EOS_Achievements_CopyAchievementDefinitionByAchievementId (base.InnerHandle, ref options2, ref outDefinition2);
		Helper.Dispose (ref options2);
		Helper.Get<DefinitionInternal, Definition> (outDefinition2, out outDefinition);
		if (outDefinition.HasValue) {
			Bindings.EOS_Achievements_Definition_Release (outDefinition2);
		}
		return result;
	}

	public Result CopyAchievementDefinitionByIndex (ref CopyAchievementDefinitionByIndexOptions options, out Definition? outDefinition)
	{
		CopyAchievementDefinitionByIndexOptionsInternal options2 = default(CopyAchievementDefinitionByIndexOptionsInternal);
		options2.Set (ref options);
		IntPtr outDefinition2 = IntPtr.Zero;
		Result result = Bindings.EOS_Achievements_CopyAchievementDefinitionByIndex (base.InnerHandle, ref options2, ref outDefinition2);
		Helper.Dispose (ref options2);
		Helper.Get<DefinitionInternal, Definition> (outDefinition2, out outDefinition);
		if (outDefinition.HasValue) {
			Bindings.EOS_Achievements_Definition_Release (outDefinition2);
		}
		return result;
	}

	public Result CopyAchievementDefinitionV2ByAchievementId (ref CopyAchievementDefinitionV2ByAchievementIdOptions options, out DefinitionV2? outDefinition)
	{
		CopyAchievementDefinitionV2ByAchievementIdOptionsInternal options2 = default(CopyAchievementDefinitionV2ByAchievementIdOptionsInternal);
		options2.Set (ref options);
		IntPtr outDefinition2 = IntPtr.Zero;
		Result result = Bindings.EOS_Achievements_CopyAchievementDefinitionV2ByAchievementId (base.InnerHandle, ref options2, ref outDefinition2);
		Helper.Dispose (ref options2);
		Helper.Get<DefinitionV2Internal, DefinitionV2> (outDefinition2, out outDefinition);
		if (outDefinition.HasValue) {
			Bindings.EOS_Achievements_DefinitionV2_Release (outDefinition2);
		}
		return result;
	}

	public Result CopyAchievementDefinitionV2ByIndex (ref CopyAchievementDefinitionV2ByIndexOptions options, out DefinitionV2? outDefinition)
	{
		CopyAchievementDefinitionV2ByIndexOptionsInternal options2 = default(CopyAchievementDefinitionV2ByIndexOptionsInternal);
		options2.Set (ref options);
		IntPtr outDefinition2 = IntPtr.Zero;
		Result result = Bindings.EOS_Achievements_CopyAchievementDefinitionV2ByIndex (base.InnerHandle, ref options2, ref outDefinition2);
		Helper.Dispose (ref options2);
		Helper.Get<DefinitionV2Internal, DefinitionV2> (outDefinition2, out outDefinition);
		if (outDefinition.HasValue) {
			Bindings.EOS_Achievements_DefinitionV2_Release (outDefinition2);
		}
		return result;
	}

	public Result CopyPlayerAchievementByAchievementId (ref CopyPlayerAchievementByAchievementIdOptions options, out PlayerAchievement? outAchievement)
	{
		CopyPlayerAchievementByAchievementIdOptionsInternal options2 = default(CopyPlayerAchievementByAchievementIdOptionsInternal);
		options2.Set (ref options);
		IntPtr outAchievement2 = IntPtr.Zero;
		Result result = Bindings.EOS_Achievements_CopyPlayerAchievementByAchievementId (base.InnerHandle, ref options2, ref outAchievement2);
		Helper.Dispose (ref options2);
		Helper.Get<PlayerAchievementInternal, PlayerAchievement> (outAchievement2, out outAchievement);
		if (outAchievement.HasValue) {
			Bindings.EOS_Achievements_PlayerAchievement_Release (outAchievement2);
		}
		return result;
	}

	public Result CopyPlayerAchievementByIndex (ref CopyPlayerAchievementByIndexOptions options, out PlayerAchievement? outAchievement)
	{
		CopyPlayerAchievementByIndexOptionsInternal options2 = default(CopyPlayerAchievementByIndexOptionsInternal);
		options2.Set (ref options);
		IntPtr outAchievement2 = IntPtr.Zero;
		Result result = Bindings.EOS_Achievements_CopyPlayerAchievementByIndex (base.InnerHandle, ref options2, ref outAchievement2);
		Helper.Dispose (ref options2);
		Helper.Get<PlayerAchievementInternal, PlayerAchievement> (outAchievement2, out outAchievement);
		if (outAchievement.HasValue) {
			Bindings.EOS_Achievements_PlayerAchievement_Release (outAchievement2);
		}
		return result;
	}

	public Result CopyUnlockedAchievementByAchievementId (ref CopyUnlockedAchievementByAchievementIdOptions options, out UnlockedAchievement? outAchievement)
	{
		CopyUnlockedAchievementByAchievementIdOptionsInternal options2 = default(CopyUnlockedAchievementByAchievementIdOptionsInternal);
		options2.Set (ref options);
		IntPtr outAchievement2 = IntPtr.Zero;
		Result result = Bindings.EOS_Achievements_CopyUnlockedAchievementByAchievementId (base.InnerHandle, ref options2, ref outAchievement2);
		Helper.Dispose (ref options2);
		Helper.Get<UnlockedAchievementInternal, UnlockedAchievement> (outAchievement2, out outAchievement);
		if (outAchievement.HasValue) {
			Bindings.EOS_Achievements_UnlockedAchievement_Release (outAchievement2);
		}
		return result;
	}

	public Result CopyUnlockedAchievementByIndex (ref CopyUnlockedAchievementByIndexOptions options, out UnlockedAchievement? outAchievement)
	{
		CopyUnlockedAchievementByIndexOptionsInternal options2 = default(CopyUnlockedAchievementByIndexOptionsInternal);
		options2.Set (ref options);
		IntPtr outAchievement2 = IntPtr.Zero;
		Result result = Bindings.EOS_Achievements_CopyUnlockedAchievementByIndex (base.InnerHandle, ref options2, ref outAchievement2);
		Helper.Dispose (ref options2);
		Helper.Get<UnlockedAchievementInternal, UnlockedAchievement> (outAchievement2, out outAchievement);
		if (outAchievement.HasValue) {
			Bindings.EOS_Achievements_UnlockedAchievement_Release (outAchievement2);
		}
		return result;
	}

	public uint GetAchievementDefinitionCount (ref GetAchievementDefinitionCountOptions options)
	{
		GetAchievementDefinitionCountOptionsInternal options2 = default(GetAchievementDefinitionCountOptionsInternal);
		options2.Set (ref options);
		uint result = Bindings.EOS_Achievements_GetAchievementDefinitionCount (base.InnerHandle, ref options2);
		Helper.Dispose (ref options2);
		return result;
	}

	public uint GetPlayerAchievementCount (ref GetPlayerAchievementCountOptions options)
	{
		GetPlayerAchievementCountOptionsInternal options2 = default(GetPlayerAchievementCountOptionsInternal);
		options2.Set (ref options);
		uint result = Bindings.EOS_Achievements_GetPlayerAchievementCount (base.InnerHandle, ref options2);
		Helper.Dispose (ref options2);
		return result;
	}

	public uint GetUnlockedAchievementCount (ref GetUnlockedAchievementCountOptions options)
	{
		GetUnlockedAchievementCountOptionsInternal options2 = default(GetUnlockedAchievementCountOptionsInternal);
		options2.Set (ref options);
		uint result = Bindings.EOS_Achievements_GetUnlockedAchievementCount (base.InnerHandle, ref options2);
		Helper.Dispose (ref options2);
		return result;
	}

	public void QueryDefinitions (ref QueryDefinitionsOptions options, object clientData, OnQueryDefinitionsCompleteCallback completionDelegate)
	{
		QueryDefinitionsOptionsInternal options2 = default(QueryDefinitionsOptionsInternal);
		options2.Set (ref options);
		IntPtr clientDataAddress = IntPtr.Zero;
		OnQueryDefinitionsCompleteCallbackInternal onQueryDefinitionsCompleteCallbackInternal = OnQueryDefinitionsCompleteCallbackInternalImplementation;
		Helper.AddCallback (out clientDataAddress, clientData, completionDelegate, onQueryDefinitionsCompleteCallbackInternal);
		Bindings.EOS_Achievements_QueryDefinitions (base.InnerHandle, ref options2, clientDataAddress, onQueryDefinitionsCompleteCallbackInternal);
		Helper.Dispose (ref options2);
	}

	public void QueryPlayerAchievements (ref QueryPlayerAchievementsOptions options, object clientData, OnQueryPlayerAchievementsCompleteCallback completionDelegate)
	{
		QueryPlayerAchievementsOptionsInternal options2 = default(QueryPlayerAchievementsOptionsInternal);
		options2.Set (ref options);
		IntPtr clientDataAddress = IntPtr.Zero;
		OnQueryPlayerAchievementsCompleteCallbackInternal onQueryPlayerAchievementsCompleteCallbackInternal = OnQueryPlayerAchievementsCompleteCallbackInternalImplementation;
		Helper.AddCallback (out clientDataAddress, clientData, completionDelegate, onQueryPlayerAchievementsCompleteCallbackInternal);
		Bindings.EOS_Achievements_QueryPlayerAchievements (base.InnerHandle, ref options2, clientDataAddress, onQueryPlayerAchievementsCompleteCallbackInternal);
		Helper.Dispose (ref options2);
	}

	public void RemoveNotifyAchievementsUnlocked (ulong inId)
	{
		Bindings.EOS_Achievements_RemoveNotifyAchievementsUnlocked (base.InnerHandle, inId);
		Helper.RemoveCallbackByNotificationId (inId);
	}

	public void UnlockAchievements (ref UnlockAchievementsOptions options, object clientData, OnUnlockAchievementsCompleteCallback completionDelegate)
	{
		UnlockAchievementsOptionsInternal options2 = default(UnlockAchievementsOptionsInternal);
		options2.Set (ref options);
		IntPtr clientDataAddress = IntPtr.Zero;
		OnUnlockAchievementsCompleteCallbackInternal onUnlockAchievementsCompleteCallbackInternal = OnUnlockAchievementsCompleteCallbackInternalImplementation;
		Helper.AddCallback (out clientDataAddress, clientData, completionDelegate, onUnlockAchievementsCompleteCallbackInternal);
		Bindings.EOS_Achievements_UnlockAchievements (base.InnerHandle, ref options2, clientDataAddress, onUnlockAchievementsCompleteCallbackInternal);
		Helper.Dispose (ref options2);
	}

	[MonoPInvokeCallback (typeof(OnAchievementsUnlockedCallbackInternal))]
	internal static void OnAchievementsUnlockedCallbackInternalImplementation (ref OnAchievementsUnlockedCallbackInfoInternal data)
	{
		if (Helper.TryGetCallback<OnAchievementsUnlockedCallbackInfoInternal, OnAchievementsUnlockedCallback, OnAchievementsUnlockedCallbackInfo> (ref data, out var callback, out var callbackInfo)) {
			callback (ref callbackInfo);
		}
	}

	[MonoPInvokeCallback (typeof(OnAchievementsUnlockedCallbackV2Internal))]
	internal static void OnAchievementsUnlockedCallbackV2InternalImplementation (ref OnAchievementsUnlockedCallbackV2InfoInternal data)
	{
		if (Helper.TryGetCallback<OnAchievementsUnlockedCallbackV2InfoInternal, OnAchievementsUnlockedCallbackV2, OnAchievementsUnlockedCallbackV2Info> (ref data, out var callback, out var callbackInfo)) {
			callback (ref callbackInfo);
		}
	}

	[MonoPInvokeCallback (typeof(OnQueryDefinitionsCompleteCallbackInternal))]
	internal static void OnQueryDefinitionsCompleteCallbackInternalImplementation (ref OnQueryDefinitionsCompleteCallbackInfoInternal data)
	{
		if (Helper.TryGetAndRemoveCallback<OnQueryDefinitionsCompleteCallbackInfoInternal, OnQueryDefinitionsCompleteCallback, OnQueryDefinitionsCompleteCallbackInfo> (ref data, out var callback, out var callbackInfo)) {
			callback (ref callbackInfo);
		}
	}

	[MonoPInvokeCallback (typeof(OnQueryPlayerAchievementsCompleteCallbackInternal))]
	internal static void OnQueryPlayerAchievementsCompleteCallbackInternalImplementation (ref OnQueryPlayerAchievementsCompleteCallbackInfoInternal data)
	{
		if (Helper.TryGetAndRemoveCallback<OnQueryPlayerAchievementsCompleteCallbackInfoInternal, OnQueryPlayerAchievementsCompleteCallback, OnQueryPlayerAchievementsCompleteCallbackInfo> (ref data, out var callback, out var callbackInfo)) {
			callback (ref callbackInfo);
		}
	}

	[MonoPInvokeCallback (typeof(OnUnlockAchievementsCompleteCallbackInternal))]
	internal static void OnUnlockAchievementsCompleteCallbackInternalImplementation (ref OnUnlockAchievementsCompleteCallbackInfoInternal data)
	{
		if (Helper.TryGetAndRemoveCallback<OnUnlockAchievementsCompleteCallbackInfoInternal, OnUnlockAchievementsCompleteCallback, OnUnlockAchievementsCompleteCallbackInfo> (ref data, out var callback, out var callbackInfo)) {
			callback (ref callbackInfo);
		}
	}
}
