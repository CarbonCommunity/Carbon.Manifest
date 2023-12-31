using System;
using System.Runtime.InteropServices;
using Epic.OnlineServices;
using Epic.OnlineServices.Achievements;
using Epic.OnlineServices.AntiCheatClient;
using Epic.OnlineServices.AntiCheatCommon;
using Epic.OnlineServices.AntiCheatServer;
using Epic.OnlineServices.Auth;
using Epic.OnlineServices.Connect;
using Epic.OnlineServices.CustomInvites;
using Epic.OnlineServices.Ecom;
using Epic.OnlineServices.Friends;
using Epic.OnlineServices.IntegratedPlatform;
using Epic.OnlineServices.KWS;
using Epic.OnlineServices.Leaderboards;
using Epic.OnlineServices.Lobby;
using Epic.OnlineServices.Logging;
using Epic.OnlineServices.Metrics;
using Epic.OnlineServices.Mods;
using Epic.OnlineServices.P2P;
using Epic.OnlineServices.Platform;
using Epic.OnlineServices.PlayerDataStorage;
using Epic.OnlineServices.Presence;
using Epic.OnlineServices.ProgressionSnapshot;
using Epic.OnlineServices.Reports;
using Epic.OnlineServices.RTC;
using Epic.OnlineServices.RTCAdmin;
using Epic.OnlineServices.RTCAudio;
using Epic.OnlineServices.Sanctions;
using Epic.OnlineServices.Sessions;
using Epic.OnlineServices.Stats;
using Epic.OnlineServices.TitleStorage;
using Epic.OnlineServices.UI;
using Epic.OnlineServices.UserInfo;

public static class Bindings
{
	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern ulong EOS_Achievements_AddNotifyAchievementsUnlocked (IntPtr handle, ref AddNotifyAchievementsUnlockedOptionsInternal options, IntPtr clientData, OnAchievementsUnlockedCallbackInternal notificationFn);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern ulong EOS_Achievements_AddNotifyAchievementsUnlockedV2 (IntPtr handle, ref AddNotifyAchievementsUnlockedV2OptionsInternal options, IntPtr clientData, OnAchievementsUnlockedCallbackV2Internal notificationFn);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_Achievements_CopyAchievementDefinitionByAchievementId (IntPtr handle, ref CopyAchievementDefinitionByAchievementIdOptionsInternal options, ref IntPtr outDefinition);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_Achievements_CopyAchievementDefinitionByIndex (IntPtr handle, ref CopyAchievementDefinitionByIndexOptionsInternal options, ref IntPtr outDefinition);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_Achievements_CopyAchievementDefinitionV2ByAchievementId (IntPtr handle, ref CopyAchievementDefinitionV2ByAchievementIdOptionsInternal options, ref IntPtr outDefinition);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_Achievements_CopyAchievementDefinitionV2ByIndex (IntPtr handle, ref CopyAchievementDefinitionV2ByIndexOptionsInternal options, ref IntPtr outDefinition);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_Achievements_CopyPlayerAchievementByAchievementId (IntPtr handle, ref CopyPlayerAchievementByAchievementIdOptionsInternal options, ref IntPtr outAchievement);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_Achievements_CopyPlayerAchievementByIndex (IntPtr handle, ref CopyPlayerAchievementByIndexOptionsInternal options, ref IntPtr outAchievement);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_Achievements_CopyUnlockedAchievementByAchievementId (IntPtr handle, ref CopyUnlockedAchievementByAchievementIdOptionsInternal options, ref IntPtr outAchievement);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_Achievements_CopyUnlockedAchievementByIndex (IntPtr handle, ref CopyUnlockedAchievementByIndexOptionsInternal options, ref IntPtr outAchievement);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_Achievements_DefinitionV2_Release (IntPtr achievementDefinition);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_Achievements_Definition_Release (IntPtr achievementDefinition);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern uint EOS_Achievements_GetAchievementDefinitionCount (IntPtr handle, ref GetAchievementDefinitionCountOptionsInternal options);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern uint EOS_Achievements_GetPlayerAchievementCount (IntPtr handle, ref GetPlayerAchievementCountOptionsInternal options);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern uint EOS_Achievements_GetUnlockedAchievementCount (IntPtr handle, ref GetUnlockedAchievementCountOptionsInternal options);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_Achievements_PlayerAchievement_Release (IntPtr achievement);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_Achievements_QueryDefinitions (IntPtr handle, ref QueryDefinitionsOptionsInternal options, IntPtr clientData, OnQueryDefinitionsCompleteCallbackInternal completionDelegate);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_Achievements_QueryPlayerAchievements (IntPtr handle, ref QueryPlayerAchievementsOptionsInternal options, IntPtr clientData, OnQueryPlayerAchievementsCompleteCallbackInternal completionDelegate);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_Achievements_RemoveNotifyAchievementsUnlocked (IntPtr handle, ulong inId);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_Achievements_UnlockAchievements (IntPtr handle, ref UnlockAchievementsOptionsInternal options, IntPtr clientData, OnUnlockAchievementsCompleteCallbackInternal completionDelegate);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_Achievements_UnlockedAchievement_Release (IntPtr achievement);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_ActiveSession_CopyInfo (IntPtr handle, ref ActiveSessionCopyInfoOptionsInternal options, ref IntPtr outActiveSessionInfo);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern IntPtr EOS_ActiveSession_GetRegisteredPlayerByIndex (IntPtr handle, ref ActiveSessionGetRegisteredPlayerByIndexOptionsInternal options);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern uint EOS_ActiveSession_GetRegisteredPlayerCount (IntPtr handle, ref ActiveSessionGetRegisteredPlayerCountOptionsInternal options);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_ActiveSession_Info_Release (IntPtr activeSessionInfo);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_ActiveSession_Release (IntPtr activeSessionHandle);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_AntiCheatClient_AddExternalIntegrityCatalog (IntPtr handle, ref AddExternalIntegrityCatalogOptionsInternal options);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern ulong EOS_AntiCheatClient_AddNotifyClientIntegrityViolated (IntPtr handle, ref AddNotifyClientIntegrityViolatedOptionsInternal options, IntPtr clientData, OnClientIntegrityViolatedCallbackInternal notificationFn);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern ulong EOS_AntiCheatClient_AddNotifyMessageToPeer (IntPtr handle, ref AddNotifyMessageToPeerOptionsInternal options, IntPtr clientData, OnMessageToPeerCallbackInternal notificationFn);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern ulong EOS_AntiCheatClient_AddNotifyMessageToServer (IntPtr handle, ref AddNotifyMessageToServerOptionsInternal options, IntPtr clientData, OnMessageToServerCallbackInternal notificationFn);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern ulong EOS_AntiCheatClient_AddNotifyPeerActionRequired (IntPtr handle, ref AddNotifyPeerActionRequiredOptionsInternal options, IntPtr clientData, OnPeerActionRequiredCallbackInternal notificationFn);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern ulong EOS_AntiCheatClient_AddNotifyPeerAuthStatusChanged (IntPtr handle, ref AddNotifyPeerAuthStatusChangedOptionsInternal options, IntPtr clientData, OnPeerAuthStatusChangedCallbackInternal notificationFn);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_AntiCheatClient_BeginSession (IntPtr handle, ref Epic.OnlineServices.AntiCheatClient.BeginSessionOptionsInternal options);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_AntiCheatClient_EndSession (IntPtr handle, ref Epic.OnlineServices.AntiCheatClient.EndSessionOptionsInternal options);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_AntiCheatClient_GetProtectMessageOutputLength (IntPtr handle, ref Epic.OnlineServices.AntiCheatClient.GetProtectMessageOutputLengthOptionsInternal options, ref uint outBufferSizeBytes);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_AntiCheatClient_PollStatus (IntPtr handle, ref PollStatusOptionsInternal options, ref AntiCheatClientViolationType outViolationType, IntPtr outMessage);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_AntiCheatClient_ProtectMessage (IntPtr handle, ref Epic.OnlineServices.AntiCheatClient.ProtectMessageOptionsInternal options, IntPtr outBuffer, ref uint outBytesWritten);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_AntiCheatClient_ReceiveMessageFromPeer (IntPtr handle, ref ReceiveMessageFromPeerOptionsInternal options);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_AntiCheatClient_ReceiveMessageFromServer (IntPtr handle, ref ReceiveMessageFromServerOptionsInternal options);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_AntiCheatClient_RegisterPeer (IntPtr handle, ref RegisterPeerOptionsInternal options);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_AntiCheatClient_RemoveNotifyClientIntegrityViolated (IntPtr handle, ulong notificationId);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_AntiCheatClient_RemoveNotifyMessageToPeer (IntPtr handle, ulong notificationId);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_AntiCheatClient_RemoveNotifyMessageToServer (IntPtr handle, ulong notificationId);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_AntiCheatClient_RemoveNotifyPeerActionRequired (IntPtr handle, ulong notificationId);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_AntiCheatClient_RemoveNotifyPeerAuthStatusChanged (IntPtr handle, ulong notificationId);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_AntiCheatClient_UnprotectMessage (IntPtr handle, ref Epic.OnlineServices.AntiCheatClient.UnprotectMessageOptionsInternal options, IntPtr outBuffer, ref uint outBytesWritten);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_AntiCheatClient_UnregisterPeer (IntPtr handle, ref UnregisterPeerOptionsInternal options);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern ulong EOS_AntiCheatServer_AddNotifyClientActionRequired (IntPtr handle, ref AddNotifyClientActionRequiredOptionsInternal options, IntPtr clientData, OnClientActionRequiredCallbackInternal notificationFn);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern ulong EOS_AntiCheatServer_AddNotifyClientAuthStatusChanged (IntPtr handle, ref AddNotifyClientAuthStatusChangedOptionsInternal options, IntPtr clientData, OnClientAuthStatusChangedCallbackInternal notificationFn);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern ulong EOS_AntiCheatServer_AddNotifyMessageToClient (IntPtr handle, ref AddNotifyMessageToClientOptionsInternal options, IntPtr clientData, OnMessageToClientCallbackInternal notificationFn);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_AntiCheatServer_BeginSession (IntPtr handle, ref Epic.OnlineServices.AntiCheatServer.BeginSessionOptionsInternal options);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_AntiCheatServer_EndSession (IntPtr handle, ref Epic.OnlineServices.AntiCheatServer.EndSessionOptionsInternal options);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_AntiCheatServer_GetProtectMessageOutputLength (IntPtr handle, ref Epic.OnlineServices.AntiCheatServer.GetProtectMessageOutputLengthOptionsInternal options, ref uint outBufferSizeBytes);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_AntiCheatServer_LogEvent (IntPtr handle, ref LogEventOptionsInternal options);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_AntiCheatServer_LogGameRoundEnd (IntPtr handle, ref LogGameRoundEndOptionsInternal options);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_AntiCheatServer_LogGameRoundStart (IntPtr handle, ref LogGameRoundStartOptionsInternal options);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_AntiCheatServer_LogPlayerDespawn (IntPtr handle, ref LogPlayerDespawnOptionsInternal options);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_AntiCheatServer_LogPlayerRevive (IntPtr handle, ref LogPlayerReviveOptionsInternal options);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_AntiCheatServer_LogPlayerSpawn (IntPtr handle, ref LogPlayerSpawnOptionsInternal options);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_AntiCheatServer_LogPlayerTakeDamage (IntPtr handle, ref LogPlayerTakeDamageOptionsInternal options);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_AntiCheatServer_LogPlayerTick (IntPtr handle, ref LogPlayerTickOptionsInternal options);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_AntiCheatServer_LogPlayerUseAbility (IntPtr handle, ref LogPlayerUseAbilityOptionsInternal options);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_AntiCheatServer_LogPlayerUseWeapon (IntPtr handle, ref LogPlayerUseWeaponOptionsInternal options);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_AntiCheatServer_ProtectMessage (IntPtr handle, ref Epic.OnlineServices.AntiCheatServer.ProtectMessageOptionsInternal options, IntPtr outBuffer, ref uint outBytesWritten);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_AntiCheatServer_ReceiveMessageFromClient (IntPtr handle, ref ReceiveMessageFromClientOptionsInternal options);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_AntiCheatServer_RegisterClient (IntPtr handle, ref RegisterClientOptionsInternal options);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_AntiCheatServer_RegisterEvent (IntPtr handle, ref RegisterEventOptionsInternal options);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_AntiCheatServer_RemoveNotifyClientActionRequired (IntPtr handle, ulong notificationId);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_AntiCheatServer_RemoveNotifyClientAuthStatusChanged (IntPtr handle, ulong notificationId);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_AntiCheatServer_RemoveNotifyMessageToClient (IntPtr handle, ulong notificationId);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_AntiCheatServer_SetClientDetails (IntPtr handle, ref SetClientDetailsOptionsInternal options);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_AntiCheatServer_SetClientNetworkState (IntPtr handle, ref SetClientNetworkStateOptionsInternal options);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_AntiCheatServer_SetGameSessionId (IntPtr handle, ref SetGameSessionIdOptionsInternal options);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_AntiCheatServer_UnprotectMessage (IntPtr handle, ref Epic.OnlineServices.AntiCheatServer.UnprotectMessageOptionsInternal options, IntPtr outBuffer, ref uint outBytesWritten);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_AntiCheatServer_UnregisterClient (IntPtr handle, ref UnregisterClientOptionsInternal options);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern ulong EOS_Auth_AddNotifyLoginStatusChanged (IntPtr handle, ref Epic.OnlineServices.Auth.AddNotifyLoginStatusChangedOptionsInternal options, IntPtr clientData, Epic.OnlineServices.Auth.OnLoginStatusChangedCallbackInternal notification);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_Auth_CopyIdToken (IntPtr handle, ref Epic.OnlineServices.Auth.CopyIdTokenOptionsInternal options, ref IntPtr outIdToken);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_Auth_CopyUserAuthToken (IntPtr handle, ref CopyUserAuthTokenOptionsInternal options, IntPtr localUserId, ref IntPtr outUserAuthToken);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_Auth_DeletePersistentAuth (IntPtr handle, ref DeletePersistentAuthOptionsInternal options, IntPtr clientData, OnDeletePersistentAuthCallbackInternal completionDelegate);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern IntPtr EOS_Auth_GetLoggedInAccountByIndex (IntPtr handle, int index);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern int EOS_Auth_GetLoggedInAccountsCount (IntPtr handle);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern LoginStatus EOS_Auth_GetLoginStatus (IntPtr handle, IntPtr localUserId);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern IntPtr EOS_Auth_GetMergedAccountByIndex (IntPtr handle, IntPtr localUserId, uint index);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern uint EOS_Auth_GetMergedAccountsCount (IntPtr handle, IntPtr localUserId);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_Auth_GetSelectedAccountId (IntPtr handle, IntPtr localUserId, ref IntPtr outSelectedAccountId);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_Auth_IdToken_Release (IntPtr idToken);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_Auth_LinkAccount (IntPtr handle, ref Epic.OnlineServices.Auth.LinkAccountOptionsInternal options, IntPtr clientData, Epic.OnlineServices.Auth.OnLinkAccountCallbackInternal completionDelegate);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_Auth_Login (IntPtr handle, ref Epic.OnlineServices.Auth.LoginOptionsInternal options, IntPtr clientData, Epic.OnlineServices.Auth.OnLoginCallbackInternal completionDelegate);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_Auth_Logout (IntPtr handle, ref LogoutOptionsInternal options, IntPtr clientData, OnLogoutCallbackInternal completionDelegate);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_Auth_QueryIdToken (IntPtr handle, ref QueryIdTokenOptionsInternal options, IntPtr clientData, OnQueryIdTokenCallbackInternal completionDelegate);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_Auth_RemoveNotifyLoginStatusChanged (IntPtr handle, ulong inId);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_Auth_Token_Release (IntPtr authToken);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_Auth_VerifyIdToken (IntPtr handle, ref Epic.OnlineServices.Auth.VerifyIdTokenOptionsInternal options, IntPtr clientData, Epic.OnlineServices.Auth.OnVerifyIdTokenCallbackInternal completionDelegate);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_Auth_VerifyUserAuth (IntPtr handle, ref VerifyUserAuthOptionsInternal options, IntPtr clientData, OnVerifyUserAuthCallbackInternal completionDelegate);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_ByteArray_ToString (IntPtr byteArray, uint length, IntPtr outBuffer, ref uint inOutBufferLength);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern ulong EOS_Connect_AddNotifyAuthExpiration (IntPtr handle, ref AddNotifyAuthExpirationOptionsInternal options, IntPtr clientData, OnAuthExpirationCallbackInternal notification);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern ulong EOS_Connect_AddNotifyLoginStatusChanged (IntPtr handle, ref Epic.OnlineServices.Connect.AddNotifyLoginStatusChangedOptionsInternal options, IntPtr clientData, Epic.OnlineServices.Connect.OnLoginStatusChangedCallbackInternal notification);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_Connect_CopyIdToken (IntPtr handle, ref Epic.OnlineServices.Connect.CopyIdTokenOptionsInternal options, ref IntPtr outIdToken);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_Connect_CopyProductUserExternalAccountByAccountId (IntPtr handle, ref CopyProductUserExternalAccountByAccountIdOptionsInternal options, ref IntPtr outExternalAccountInfo);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_Connect_CopyProductUserExternalAccountByAccountType (IntPtr handle, ref CopyProductUserExternalAccountByAccountTypeOptionsInternal options, ref IntPtr outExternalAccountInfo);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_Connect_CopyProductUserExternalAccountByIndex (IntPtr handle, ref CopyProductUserExternalAccountByIndexOptionsInternal options, ref IntPtr outExternalAccountInfo);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_Connect_CopyProductUserInfo (IntPtr handle, ref CopyProductUserInfoOptionsInternal options, ref IntPtr outExternalAccountInfo);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_Connect_CreateDeviceId (IntPtr handle, ref CreateDeviceIdOptionsInternal options, IntPtr clientData, OnCreateDeviceIdCallbackInternal completionDelegate);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_Connect_CreateUser (IntPtr handle, ref Epic.OnlineServices.Connect.CreateUserOptionsInternal options, IntPtr clientData, Epic.OnlineServices.Connect.OnCreateUserCallbackInternal completionDelegate);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_Connect_DeleteDeviceId (IntPtr handle, ref DeleteDeviceIdOptionsInternal options, IntPtr clientData, OnDeleteDeviceIdCallbackInternal completionDelegate);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_Connect_ExternalAccountInfo_Release (IntPtr externalAccountInfo);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern IntPtr EOS_Connect_GetExternalAccountMapping (IntPtr handle, ref GetExternalAccountMappingsOptionsInternal options);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern IntPtr EOS_Connect_GetLoggedInUserByIndex (IntPtr handle, int index);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern int EOS_Connect_GetLoggedInUsersCount (IntPtr handle);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern LoginStatus EOS_Connect_GetLoginStatus (IntPtr handle, IntPtr localUserId);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern uint EOS_Connect_GetProductUserExternalAccountCount (IntPtr handle, ref GetProductUserExternalAccountCountOptionsInternal options);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_Connect_GetProductUserIdMapping (IntPtr handle, ref GetProductUserIdMappingOptionsInternal options, IntPtr outBuffer, ref int inOutBufferLength);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_Connect_IdToken_Release (IntPtr idToken);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_Connect_LinkAccount (IntPtr handle, ref Epic.OnlineServices.Connect.LinkAccountOptionsInternal options, IntPtr clientData, Epic.OnlineServices.Connect.OnLinkAccountCallbackInternal completionDelegate);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_Connect_Login (IntPtr handle, ref Epic.OnlineServices.Connect.LoginOptionsInternal options, IntPtr clientData, Epic.OnlineServices.Connect.OnLoginCallbackInternal completionDelegate);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_Connect_QueryExternalAccountMappings (IntPtr handle, ref QueryExternalAccountMappingsOptionsInternal options, IntPtr clientData, OnQueryExternalAccountMappingsCallbackInternal completionDelegate);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_Connect_QueryProductUserIdMappings (IntPtr handle, ref QueryProductUserIdMappingsOptionsInternal options, IntPtr clientData, OnQueryProductUserIdMappingsCallbackInternal completionDelegate);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_Connect_RemoveNotifyAuthExpiration (IntPtr handle, ulong inId);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_Connect_RemoveNotifyLoginStatusChanged (IntPtr handle, ulong inId);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_Connect_TransferDeviceIdAccount (IntPtr handle, ref TransferDeviceIdAccountOptionsInternal options, IntPtr clientData, OnTransferDeviceIdAccountCallbackInternal completionDelegate);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_Connect_UnlinkAccount (IntPtr handle, ref UnlinkAccountOptionsInternal options, IntPtr clientData, OnUnlinkAccountCallbackInternal completionDelegate);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_Connect_VerifyIdToken (IntPtr handle, ref Epic.OnlineServices.Connect.VerifyIdTokenOptionsInternal options, IntPtr clientData, Epic.OnlineServices.Connect.OnVerifyIdTokenCallbackInternal completionDelegate);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_ContinuanceToken_ToString (IntPtr continuanceToken, IntPtr outBuffer, ref int inOutBufferLength);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_CustomInvites_AcceptRequestToJoin (IntPtr handle, ref AcceptRequestToJoinOptionsInternal options, IntPtr clientData, OnAcceptRequestToJoinCallbackInternal completionDelegate);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern ulong EOS_CustomInvites_AddNotifyCustomInviteAccepted (IntPtr handle, ref AddNotifyCustomInviteAcceptedOptionsInternal options, IntPtr clientData, OnCustomInviteAcceptedCallbackInternal notificationFn);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern ulong EOS_CustomInvites_AddNotifyCustomInviteReceived (IntPtr handle, ref AddNotifyCustomInviteReceivedOptionsInternal options, IntPtr clientData, OnCustomInviteReceivedCallbackInternal notificationFn);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern ulong EOS_CustomInvites_AddNotifyCustomInviteRejected (IntPtr handle, ref AddNotifyCustomInviteRejectedOptionsInternal options, IntPtr clientData, OnCustomInviteRejectedCallbackInternal notificationFn);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern ulong EOS_CustomInvites_AddNotifyRequestToJoinAccepted (IntPtr handle, ref AddNotifyRequestToJoinAcceptedOptionsInternal options, IntPtr clientData, OnRequestToJoinAcceptedCallbackInternal notificationFn);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern ulong EOS_CustomInvites_AddNotifyRequestToJoinReceived (IntPtr handle, ref AddNotifyRequestToJoinReceivedOptionsInternal options, IntPtr clientData, OnRequestToJoinReceivedCallbackInternal notificationFn);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern ulong EOS_CustomInvites_AddNotifyRequestToJoinRejected (IntPtr handle, ref AddNotifyRequestToJoinRejectedOptionsInternal options, IntPtr clientData, OnRequestToJoinRejectedCallbackInternal notificationFn);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern ulong EOS_CustomInvites_AddNotifyRequestToJoinResponseReceived (IntPtr handle, ref AddNotifyRequestToJoinResponseReceivedOptionsInternal options, IntPtr clientData, OnRequestToJoinResponseReceivedCallbackInternal notificationFn);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern ulong EOS_CustomInvites_AddNotifySendCustomNativeInviteRequested (IntPtr handle, ref AddNotifySendCustomNativeInviteRequestedOptionsInternal options, IntPtr clientData, OnSendCustomNativeInviteRequestedCallbackInternal notificationFn);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_CustomInvites_FinalizeInvite (IntPtr handle, ref FinalizeInviteOptionsInternal options);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_CustomInvites_RejectRequestToJoin (IntPtr handle, ref RejectRequestToJoinOptionsInternal options, IntPtr clientData, OnRejectRequestToJoinCallbackInternal completionDelegate);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_CustomInvites_RemoveNotifyCustomInviteAccepted (IntPtr handle, ulong inId);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_CustomInvites_RemoveNotifyCustomInviteReceived (IntPtr handle, ulong inId);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_CustomInvites_RemoveNotifyCustomInviteRejected (IntPtr handle, ulong inId);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_CustomInvites_RemoveNotifyRequestToJoinAccepted (IntPtr handle, ulong inId);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_CustomInvites_RemoveNotifyRequestToJoinReceived (IntPtr handle, ulong inId);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_CustomInvites_RemoveNotifyRequestToJoinRejected (IntPtr handle, ulong inId);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_CustomInvites_RemoveNotifyRequestToJoinResponseReceived (IntPtr handle, ulong inId);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_CustomInvites_RemoveNotifySendCustomNativeInviteRequested (IntPtr handle, ulong inId);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_CustomInvites_SendCustomInvite (IntPtr handle, ref SendCustomInviteOptionsInternal options, IntPtr clientData, OnSendCustomInviteCallbackInternal completionDelegate);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_CustomInvites_SendRequestToJoin (IntPtr handle, ref SendRequestToJoinOptionsInternal options, IntPtr clientData, OnSendRequestToJoinCallbackInternal completionDelegate);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_CustomInvites_SetCustomInvite (IntPtr handle, ref SetCustomInviteOptionsInternal options);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern IntPtr EOS_EApplicationStatus_ToString (ApplicationStatus applicationStatus);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern IntPtr EOS_ENetworkStatus_ToString (NetworkStatus networkStatus);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern int EOS_EResult_IsOperationComplete (Result result);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern IntPtr EOS_EResult_ToString (Result result);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_Ecom_CatalogItem_Release (IntPtr catalogItem);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_Ecom_CatalogOffer_Release (IntPtr catalogOffer);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_Ecom_CatalogRelease_Release (IntPtr catalogRelease);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_Ecom_Checkout (IntPtr handle, ref CheckoutOptionsInternal options, IntPtr clientData, OnCheckoutCallbackInternal completionDelegate);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_Ecom_CopyEntitlementById (IntPtr handle, ref CopyEntitlementByIdOptionsInternal options, ref IntPtr outEntitlement);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_Ecom_CopyEntitlementByIndex (IntPtr handle, ref CopyEntitlementByIndexOptionsInternal options, ref IntPtr outEntitlement);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_Ecom_CopyEntitlementByNameAndIndex (IntPtr handle, ref CopyEntitlementByNameAndIndexOptionsInternal options, ref IntPtr outEntitlement);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_Ecom_CopyItemById (IntPtr handle, ref CopyItemByIdOptionsInternal options, ref IntPtr outItem);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_Ecom_CopyItemImageInfoByIndex (IntPtr handle, ref CopyItemImageInfoByIndexOptionsInternal options, ref IntPtr outImageInfo);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_Ecom_CopyItemReleaseByIndex (IntPtr handle, ref CopyItemReleaseByIndexOptionsInternal options, ref IntPtr outRelease);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_Ecom_CopyLastRedeemedEntitlementByIndex (IntPtr handle, ref CopyLastRedeemedEntitlementByIndexOptionsInternal options, IntPtr outRedeemedEntitlementId, ref int inOutRedeemedEntitlementIdLength);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_Ecom_CopyOfferById (IntPtr handle, ref CopyOfferByIdOptionsInternal options, ref IntPtr outOffer);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_Ecom_CopyOfferByIndex (IntPtr handle, ref CopyOfferByIndexOptionsInternal options, ref IntPtr outOffer);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_Ecom_CopyOfferImageInfoByIndex (IntPtr handle, ref CopyOfferImageInfoByIndexOptionsInternal options, ref IntPtr outImageInfo);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_Ecom_CopyOfferItemByIndex (IntPtr handle, ref CopyOfferItemByIndexOptionsInternal options, ref IntPtr outItem);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_Ecom_CopyTransactionById (IntPtr handle, ref CopyTransactionByIdOptionsInternal options, ref IntPtr outTransaction);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_Ecom_CopyTransactionByIndex (IntPtr handle, ref CopyTransactionByIndexOptionsInternal options, ref IntPtr outTransaction);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_Ecom_Entitlement_Release (IntPtr entitlement);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern uint EOS_Ecom_GetEntitlementsByNameCount (IntPtr handle, ref GetEntitlementsByNameCountOptionsInternal options);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern uint EOS_Ecom_GetEntitlementsCount (IntPtr handle, ref GetEntitlementsCountOptionsInternal options);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern uint EOS_Ecom_GetItemImageInfoCount (IntPtr handle, ref GetItemImageInfoCountOptionsInternal options);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern uint EOS_Ecom_GetItemReleaseCount (IntPtr handle, ref GetItemReleaseCountOptionsInternal options);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern uint EOS_Ecom_GetLastRedeemedEntitlementsCount (IntPtr handle, ref GetLastRedeemedEntitlementsCountOptionsInternal options);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern uint EOS_Ecom_GetOfferCount (IntPtr handle, ref GetOfferCountOptionsInternal options);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern uint EOS_Ecom_GetOfferImageInfoCount (IntPtr handle, ref GetOfferImageInfoCountOptionsInternal options);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern uint EOS_Ecom_GetOfferItemCount (IntPtr handle, ref GetOfferItemCountOptionsInternal options);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern uint EOS_Ecom_GetTransactionCount (IntPtr handle, ref GetTransactionCountOptionsInternal options);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_Ecom_KeyImageInfo_Release (IntPtr keyImageInfo);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_Ecom_QueryEntitlementToken (IntPtr handle, ref QueryEntitlementTokenOptionsInternal options, IntPtr clientData, OnQueryEntitlementTokenCallbackInternal completionDelegate);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_Ecom_QueryEntitlements (IntPtr handle, ref QueryEntitlementsOptionsInternal options, IntPtr clientData, OnQueryEntitlementsCallbackInternal completionDelegate);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_Ecom_QueryOffers (IntPtr handle, ref QueryOffersOptionsInternal options, IntPtr clientData, OnQueryOffersCallbackInternal completionDelegate);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_Ecom_QueryOwnership (IntPtr handle, ref QueryOwnershipOptionsInternal options, IntPtr clientData, OnQueryOwnershipCallbackInternal completionDelegate);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_Ecom_QueryOwnershipBySandboxIds (IntPtr handle, ref QueryOwnershipBySandboxIdsOptionsInternal options, IntPtr clientData, OnQueryOwnershipBySandboxIdsCallbackInternal completionDelegate);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_Ecom_QueryOwnershipToken (IntPtr handle, ref QueryOwnershipTokenOptionsInternal options, IntPtr clientData, OnQueryOwnershipTokenCallbackInternal completionDelegate);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_Ecom_RedeemEntitlements (IntPtr handle, ref RedeemEntitlementsOptionsInternal options, IntPtr clientData, OnRedeemEntitlementsCallbackInternal completionDelegate);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_Ecom_Transaction_CopyEntitlementByIndex (IntPtr handle, ref TransactionCopyEntitlementByIndexOptionsInternal options, ref IntPtr outEntitlement);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern uint EOS_Ecom_Transaction_GetEntitlementsCount (IntPtr handle, ref TransactionGetEntitlementsCountOptionsInternal options);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_Ecom_Transaction_GetTransactionId (IntPtr handle, IntPtr outBuffer, ref int inOutBufferLength);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_Ecom_Transaction_Release (IntPtr transaction);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern IntPtr EOS_EpicAccountId_FromString (IntPtr accountIdString);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern int EOS_EpicAccountId_IsValid (IntPtr accountId);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_EpicAccountId_ToString (IntPtr accountId, IntPtr outBuffer, ref int inOutBufferLength);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_Friends_AcceptInvite (IntPtr handle, ref AcceptInviteOptionsInternal options, IntPtr clientData, OnAcceptInviteCallbackInternal completionDelegate);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern ulong EOS_Friends_AddNotifyBlockedUsersUpdate (IntPtr handle, ref AddNotifyBlockedUsersUpdateOptionsInternal options, IntPtr clientData, OnBlockedUsersUpdateCallbackInternal blockedUsersUpdateHandler);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern ulong EOS_Friends_AddNotifyFriendsUpdate (IntPtr handle, ref AddNotifyFriendsUpdateOptionsInternal options, IntPtr clientData, OnFriendsUpdateCallbackInternal friendsUpdateHandler);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern IntPtr EOS_Friends_GetBlockedUserAtIndex (IntPtr handle, ref GetBlockedUserAtIndexOptionsInternal options);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern int EOS_Friends_GetBlockedUsersCount (IntPtr handle, ref GetBlockedUsersCountOptionsInternal options);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern IntPtr EOS_Friends_GetFriendAtIndex (IntPtr handle, ref GetFriendAtIndexOptionsInternal options);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern int EOS_Friends_GetFriendsCount (IntPtr handle, ref GetFriendsCountOptionsInternal options);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern FriendsStatus EOS_Friends_GetStatus (IntPtr handle, ref GetStatusOptionsInternal options);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_Friends_QueryFriends (IntPtr handle, ref QueryFriendsOptionsInternal options, IntPtr clientData, OnQueryFriendsCallbackInternal completionDelegate);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_Friends_RejectInvite (IntPtr handle, ref Epic.OnlineServices.Friends.RejectInviteOptionsInternal options, IntPtr clientData, Epic.OnlineServices.Friends.OnRejectInviteCallbackInternal completionDelegate);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_Friends_RemoveNotifyBlockedUsersUpdate (IntPtr handle, ulong notificationId);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_Friends_RemoveNotifyFriendsUpdate (IntPtr handle, ulong notificationId);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_Friends_SendInvite (IntPtr handle, ref Epic.OnlineServices.Friends.SendInviteOptionsInternal options, IntPtr clientData, Epic.OnlineServices.Friends.OnSendInviteCallbackInternal completionDelegate);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern IntPtr EOS_GetVersion ();

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_Initialize (ref InitializeOptionsInternal options);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_IntegratedPlatformOptionsContainer_Add (IntPtr handle, ref IntegratedPlatformOptionsContainerAddOptionsInternal inOptions);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_IntegratedPlatformOptionsContainer_Release (IntPtr integratedPlatformOptionsContainerHandle);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern ulong EOS_IntegratedPlatform_AddNotifyUserLoginStatusChanged (IntPtr handle, ref AddNotifyUserLoginStatusChangedOptionsInternal options, IntPtr clientData, OnUserLoginStatusChangedCallbackInternal callbackFunction);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_IntegratedPlatform_ClearUserPreLogoutCallback (IntPtr handle, ref ClearUserPreLogoutCallbackOptionsInternal options);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_IntegratedPlatform_CreateIntegratedPlatformOptionsContainer (ref CreateIntegratedPlatformOptionsContainerOptionsInternal options, ref IntPtr outIntegratedPlatformOptionsContainerHandle);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_IntegratedPlatform_FinalizeDeferredUserLogout (IntPtr handle, ref FinalizeDeferredUserLogoutOptionsInternal options);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_IntegratedPlatform_RemoveNotifyUserLoginStatusChanged (IntPtr handle, ulong notificationId);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_IntegratedPlatform_SetUserLoginStatus (IntPtr handle, ref SetUserLoginStatusOptionsInternal options);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_IntegratedPlatform_SetUserPreLogoutCallback (IntPtr handle, ref SetUserPreLogoutCallbackOptionsInternal options, IntPtr clientData, OnUserPreLogoutCallbackInternal callbackFunction);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern ulong EOS_KWS_AddNotifyPermissionsUpdateReceived (IntPtr handle, ref AddNotifyPermissionsUpdateReceivedOptionsInternal options, IntPtr clientData, OnPermissionsUpdateReceivedCallbackInternal notificationFn);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_KWS_CopyPermissionByIndex (IntPtr handle, ref CopyPermissionByIndexOptionsInternal options, ref IntPtr outPermission);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_KWS_CreateUser (IntPtr handle, ref Epic.OnlineServices.KWS.CreateUserOptionsInternal options, IntPtr clientData, Epic.OnlineServices.KWS.OnCreateUserCallbackInternal completionDelegate);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_KWS_GetPermissionByKey (IntPtr handle, ref GetPermissionByKeyOptionsInternal options, ref KWSPermissionStatus outPermission);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern int EOS_KWS_GetPermissionsCount (IntPtr handle, ref GetPermissionsCountOptionsInternal options);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_KWS_PermissionStatus_Release (IntPtr permissionStatus);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_KWS_QueryAgeGate (IntPtr handle, ref QueryAgeGateOptionsInternal options, IntPtr clientData, OnQueryAgeGateCallbackInternal completionDelegate);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_KWS_QueryPermissions (IntPtr handle, ref QueryPermissionsOptionsInternal options, IntPtr clientData, OnQueryPermissionsCallbackInternal completionDelegate);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_KWS_RemoveNotifyPermissionsUpdateReceived (IntPtr handle, ulong inId);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_KWS_RequestPermissions (IntPtr handle, ref RequestPermissionsOptionsInternal options, IntPtr clientData, OnRequestPermissionsCallbackInternal completionDelegate);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_KWS_UpdateParentEmail (IntPtr handle, ref UpdateParentEmailOptionsInternal options, IntPtr clientData, OnUpdateParentEmailCallbackInternal completionDelegate);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_Leaderboards_CopyLeaderboardDefinitionByIndex (IntPtr handle, ref CopyLeaderboardDefinitionByIndexOptionsInternal options, ref IntPtr outLeaderboardDefinition);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_Leaderboards_CopyLeaderboardDefinitionByLeaderboardId (IntPtr handle, ref CopyLeaderboardDefinitionByLeaderboardIdOptionsInternal options, ref IntPtr outLeaderboardDefinition);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_Leaderboards_CopyLeaderboardRecordByIndex (IntPtr handle, ref CopyLeaderboardRecordByIndexOptionsInternal options, ref IntPtr outLeaderboardRecord);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_Leaderboards_CopyLeaderboardRecordByUserId (IntPtr handle, ref CopyLeaderboardRecordByUserIdOptionsInternal options, ref IntPtr outLeaderboardRecord);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_Leaderboards_CopyLeaderboardUserScoreByIndex (IntPtr handle, ref CopyLeaderboardUserScoreByIndexOptionsInternal options, ref IntPtr outLeaderboardUserScore);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_Leaderboards_CopyLeaderboardUserScoreByUserId (IntPtr handle, ref CopyLeaderboardUserScoreByUserIdOptionsInternal options, ref IntPtr outLeaderboardUserScore);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_Leaderboards_Definition_Release (IntPtr leaderboardDefinition);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern uint EOS_Leaderboards_GetLeaderboardDefinitionCount (IntPtr handle, ref GetLeaderboardDefinitionCountOptionsInternal options);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern uint EOS_Leaderboards_GetLeaderboardRecordCount (IntPtr handle, ref GetLeaderboardRecordCountOptionsInternal options);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern uint EOS_Leaderboards_GetLeaderboardUserScoreCount (IntPtr handle, ref GetLeaderboardUserScoreCountOptionsInternal options);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_Leaderboards_LeaderboardDefinition_Release (IntPtr leaderboardDefinition);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_Leaderboards_LeaderboardRecord_Release (IntPtr leaderboardRecord);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_Leaderboards_LeaderboardUserScore_Release (IntPtr leaderboardUserScore);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_Leaderboards_QueryLeaderboardDefinitions (IntPtr handle, ref QueryLeaderboardDefinitionsOptionsInternal options, IntPtr clientData, OnQueryLeaderboardDefinitionsCompleteCallbackInternal completionDelegate);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_Leaderboards_QueryLeaderboardRanks (IntPtr handle, ref QueryLeaderboardRanksOptionsInternal options, IntPtr clientData, OnQueryLeaderboardRanksCompleteCallbackInternal completionDelegate);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_Leaderboards_QueryLeaderboardUserScores (IntPtr handle, ref QueryLeaderboardUserScoresOptionsInternal options, IntPtr clientData, OnQueryLeaderboardUserScoresCompleteCallbackInternal completionDelegate);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_LobbyDetails_CopyAttributeByIndex (IntPtr handle, ref LobbyDetailsCopyAttributeByIndexOptionsInternal options, ref IntPtr outAttribute);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_LobbyDetails_CopyAttributeByKey (IntPtr handle, ref LobbyDetailsCopyAttributeByKeyOptionsInternal options, ref IntPtr outAttribute);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_LobbyDetails_CopyInfo (IntPtr handle, ref LobbyDetailsCopyInfoOptionsInternal options, ref IntPtr outLobbyDetailsInfo);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_LobbyDetails_CopyMemberAttributeByIndex (IntPtr handle, ref LobbyDetailsCopyMemberAttributeByIndexOptionsInternal options, ref IntPtr outAttribute);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_LobbyDetails_CopyMemberAttributeByKey (IntPtr handle, ref LobbyDetailsCopyMemberAttributeByKeyOptionsInternal options, ref IntPtr outAttribute);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_LobbyDetails_CopyMemberInfo (IntPtr handle, ref LobbyDetailsCopyMemberInfoOptionsInternal options, ref IntPtr outLobbyDetailsMemberInfo);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern uint EOS_LobbyDetails_GetAttributeCount (IntPtr handle, ref LobbyDetailsGetAttributeCountOptionsInternal options);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern IntPtr EOS_LobbyDetails_GetLobbyOwner (IntPtr handle, ref LobbyDetailsGetLobbyOwnerOptionsInternal options);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern uint EOS_LobbyDetails_GetMemberAttributeCount (IntPtr handle, ref LobbyDetailsGetMemberAttributeCountOptionsInternal options);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern IntPtr EOS_LobbyDetails_GetMemberByIndex (IntPtr handle, ref LobbyDetailsGetMemberByIndexOptionsInternal options);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern uint EOS_LobbyDetails_GetMemberCount (IntPtr handle, ref LobbyDetailsGetMemberCountOptionsInternal options);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_LobbyDetails_Info_Release (IntPtr lobbyDetailsInfo);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_LobbyDetails_MemberInfo_Release (IntPtr lobbyDetailsMemberInfo);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_LobbyDetails_Release (IntPtr lobbyHandle);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_LobbyModification_AddAttribute (IntPtr handle, ref LobbyModificationAddAttributeOptionsInternal options);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_LobbyModification_AddMemberAttribute (IntPtr handle, ref LobbyModificationAddMemberAttributeOptionsInternal options);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_LobbyModification_Release (IntPtr lobbyModificationHandle);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_LobbyModification_RemoveAttribute (IntPtr handle, ref LobbyModificationRemoveAttributeOptionsInternal options);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_LobbyModification_RemoveMemberAttribute (IntPtr handle, ref LobbyModificationRemoveMemberAttributeOptionsInternal options);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_LobbyModification_SetAllowedPlatformIds (IntPtr handle, ref LobbyModificationSetAllowedPlatformIdsOptionsInternal options);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_LobbyModification_SetBucketId (IntPtr handle, ref LobbyModificationSetBucketIdOptionsInternal options);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_LobbyModification_SetInvitesAllowed (IntPtr handle, ref LobbyModificationSetInvitesAllowedOptionsInternal options);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_LobbyModification_SetMaxMembers (IntPtr handle, ref LobbyModificationSetMaxMembersOptionsInternal options);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_LobbyModification_SetPermissionLevel (IntPtr handle, ref LobbyModificationSetPermissionLevelOptionsInternal options);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_LobbySearch_CopySearchResultByIndex (IntPtr handle, ref LobbySearchCopySearchResultByIndexOptionsInternal options, ref IntPtr outLobbyDetailsHandle);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_LobbySearch_Find (IntPtr handle, ref LobbySearchFindOptionsInternal options, IntPtr clientData, LobbySearchOnFindCallbackInternal completionDelegate);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern uint EOS_LobbySearch_GetSearchResultCount (IntPtr handle, ref LobbySearchGetSearchResultCountOptionsInternal options);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_LobbySearch_Release (IntPtr lobbySearchHandle);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_LobbySearch_RemoveParameter (IntPtr handle, ref LobbySearchRemoveParameterOptionsInternal options);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_LobbySearch_SetLobbyId (IntPtr handle, ref LobbySearchSetLobbyIdOptionsInternal options);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_LobbySearch_SetMaxResults (IntPtr handle, ref LobbySearchSetMaxResultsOptionsInternal options);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_LobbySearch_SetParameter (IntPtr handle, ref LobbySearchSetParameterOptionsInternal options);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_LobbySearch_SetTargetUserId (IntPtr handle, ref LobbySearchSetTargetUserIdOptionsInternal options);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern ulong EOS_Lobby_AddNotifyJoinLobbyAccepted (IntPtr handle, ref AddNotifyJoinLobbyAcceptedOptionsInternal options, IntPtr clientData, OnJoinLobbyAcceptedCallbackInternal notificationFn);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern ulong EOS_Lobby_AddNotifyLeaveLobbyRequested (IntPtr handle, ref AddNotifyLeaveLobbyRequestedOptionsInternal options, IntPtr clientData, OnLeaveLobbyRequestedCallbackInternal notificationFn);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern ulong EOS_Lobby_AddNotifyLobbyInviteAccepted (IntPtr handle, ref AddNotifyLobbyInviteAcceptedOptionsInternal options, IntPtr clientData, OnLobbyInviteAcceptedCallbackInternal notificationFn);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern ulong EOS_Lobby_AddNotifyLobbyInviteReceived (IntPtr handle, ref AddNotifyLobbyInviteReceivedOptionsInternal options, IntPtr clientData, OnLobbyInviteReceivedCallbackInternal notificationFn);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern ulong EOS_Lobby_AddNotifyLobbyInviteRejected (IntPtr handle, ref AddNotifyLobbyInviteRejectedOptionsInternal options, IntPtr clientData, OnLobbyInviteRejectedCallbackInternal notificationFn);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern ulong EOS_Lobby_AddNotifyLobbyMemberStatusReceived (IntPtr handle, ref AddNotifyLobbyMemberStatusReceivedOptionsInternal options, IntPtr clientData, OnLobbyMemberStatusReceivedCallbackInternal notificationFn);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern ulong EOS_Lobby_AddNotifyLobbyMemberUpdateReceived (IntPtr handle, ref AddNotifyLobbyMemberUpdateReceivedOptionsInternal options, IntPtr clientData, OnLobbyMemberUpdateReceivedCallbackInternal notificationFn);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern ulong EOS_Lobby_AddNotifyLobbyUpdateReceived (IntPtr handle, ref AddNotifyLobbyUpdateReceivedOptionsInternal options, IntPtr clientData, OnLobbyUpdateReceivedCallbackInternal notificationFn);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern ulong EOS_Lobby_AddNotifyRTCRoomConnectionChanged (IntPtr handle, ref AddNotifyRTCRoomConnectionChangedOptionsInternal options, IntPtr clientData, OnRTCRoomConnectionChangedCallbackInternal notificationFn);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern ulong EOS_Lobby_AddNotifySendLobbyNativeInviteRequested (IntPtr handle, ref AddNotifySendLobbyNativeInviteRequestedOptionsInternal options, IntPtr clientData, OnSendLobbyNativeInviteRequestedCallbackInternal notificationFn);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_Lobby_Attribute_Release (IntPtr lobbyAttribute);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_Lobby_CopyLobbyDetailsHandle (IntPtr handle, ref CopyLobbyDetailsHandleOptionsInternal options, ref IntPtr outLobbyDetailsHandle);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_Lobby_CopyLobbyDetailsHandleByInviteId (IntPtr handle, ref CopyLobbyDetailsHandleByInviteIdOptionsInternal options, ref IntPtr outLobbyDetailsHandle);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_Lobby_CopyLobbyDetailsHandleByUiEventId (IntPtr handle, ref CopyLobbyDetailsHandleByUiEventIdOptionsInternal options, ref IntPtr outLobbyDetailsHandle);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_Lobby_CreateLobby (IntPtr handle, ref CreateLobbyOptionsInternal options, IntPtr clientData, OnCreateLobbyCallbackInternal completionDelegate);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_Lobby_CreateLobbySearch (IntPtr handle, ref CreateLobbySearchOptionsInternal options, ref IntPtr outLobbySearchHandle);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_Lobby_DestroyLobby (IntPtr handle, ref DestroyLobbyOptionsInternal options, IntPtr clientData, OnDestroyLobbyCallbackInternal completionDelegate);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_Lobby_GetConnectString (IntPtr handle, ref GetConnectStringOptionsInternal options, IntPtr outBuffer, ref uint inOutBufferLength);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern uint EOS_Lobby_GetInviteCount (IntPtr handle, ref Epic.OnlineServices.Lobby.GetInviteCountOptionsInternal options);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_Lobby_GetInviteIdByIndex (IntPtr handle, ref Epic.OnlineServices.Lobby.GetInviteIdByIndexOptionsInternal options, IntPtr outBuffer, ref int inOutBufferLength);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_Lobby_GetRTCRoomName (IntPtr handle, ref GetRTCRoomNameOptionsInternal options, IntPtr outBuffer, ref uint inOutBufferLength);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_Lobby_HardMuteMember (IntPtr handle, ref HardMuteMemberOptionsInternal options, IntPtr clientData, OnHardMuteMemberCallbackInternal completionDelegate);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_Lobby_IsRTCRoomConnected (IntPtr handle, ref IsRTCRoomConnectedOptionsInternal options, ref int bOutIsConnected);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_Lobby_JoinLobby (IntPtr handle, ref JoinLobbyOptionsInternal options, IntPtr clientData, OnJoinLobbyCallbackInternal completionDelegate);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_Lobby_JoinLobbyById (IntPtr handle, ref JoinLobbyByIdOptionsInternal options, IntPtr clientData, OnJoinLobbyByIdCallbackInternal completionDelegate);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_Lobby_KickMember (IntPtr handle, ref KickMemberOptionsInternal options, IntPtr clientData, OnKickMemberCallbackInternal completionDelegate);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_Lobby_LeaveLobby (IntPtr handle, ref LeaveLobbyOptionsInternal options, IntPtr clientData, OnLeaveLobbyCallbackInternal completionDelegate);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_Lobby_ParseConnectString (IntPtr handle, ref ParseConnectStringOptionsInternal options, IntPtr outBuffer, ref uint inOutBufferLength);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_Lobby_PromoteMember (IntPtr handle, ref PromoteMemberOptionsInternal options, IntPtr clientData, OnPromoteMemberCallbackInternal completionDelegate);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_Lobby_QueryInvites (IntPtr handle, ref Epic.OnlineServices.Lobby.QueryInvitesOptionsInternal options, IntPtr clientData, Epic.OnlineServices.Lobby.OnQueryInvitesCallbackInternal completionDelegate);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_Lobby_RejectInvite (IntPtr handle, ref Epic.OnlineServices.Lobby.RejectInviteOptionsInternal options, IntPtr clientData, Epic.OnlineServices.Lobby.OnRejectInviteCallbackInternal completionDelegate);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_Lobby_RemoveNotifyJoinLobbyAccepted (IntPtr handle, ulong inId);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_Lobby_RemoveNotifyLeaveLobbyRequested (IntPtr handle, ulong inId);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_Lobby_RemoveNotifyLobbyInviteAccepted (IntPtr handle, ulong inId);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_Lobby_RemoveNotifyLobbyInviteReceived (IntPtr handle, ulong inId);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_Lobby_RemoveNotifyLobbyInviteRejected (IntPtr handle, ulong inId);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_Lobby_RemoveNotifyLobbyMemberStatusReceived (IntPtr handle, ulong inId);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_Lobby_RemoveNotifyLobbyMemberUpdateReceived (IntPtr handle, ulong inId);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_Lobby_RemoveNotifyLobbyUpdateReceived (IntPtr handle, ulong inId);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_Lobby_RemoveNotifyRTCRoomConnectionChanged (IntPtr handle, ulong inId);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_Lobby_RemoveNotifySendLobbyNativeInviteRequested (IntPtr handle, ulong inId);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_Lobby_SendInvite (IntPtr handle, ref Epic.OnlineServices.Lobby.SendInviteOptionsInternal options, IntPtr clientData, Epic.OnlineServices.Lobby.OnSendInviteCallbackInternal completionDelegate);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_Lobby_UpdateLobby (IntPtr handle, ref UpdateLobbyOptionsInternal options, IntPtr clientData, OnUpdateLobbyCallbackInternal completionDelegate);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_Lobby_UpdateLobbyModification (IntPtr handle, ref UpdateLobbyModificationOptionsInternal options, ref IntPtr outLobbyModificationHandle);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_Logging_SetCallback (LogMessageFuncInternal callback);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_Logging_SetLogLevel (LogCategory logCategory, LogLevel logLevel);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_Metrics_BeginPlayerSession (IntPtr handle, ref BeginPlayerSessionOptionsInternal options);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_Metrics_EndPlayerSession (IntPtr handle, ref EndPlayerSessionOptionsInternal options);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_Mods_CopyModInfo (IntPtr handle, ref CopyModInfoOptionsInternal options, ref IntPtr outEnumeratedMods);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_Mods_EnumerateMods (IntPtr handle, ref EnumerateModsOptionsInternal options, IntPtr clientData, OnEnumerateModsCallbackInternal completionDelegate);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_Mods_InstallMod (IntPtr handle, ref InstallModOptionsInternal options, IntPtr clientData, OnInstallModCallbackInternal completionDelegate);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_Mods_ModInfo_Release (IntPtr modInfo);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_Mods_UninstallMod (IntPtr handle, ref UninstallModOptionsInternal options, IntPtr clientData, OnUninstallModCallbackInternal completionDelegate);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_Mods_UpdateMod (IntPtr handle, ref UpdateModOptionsInternal options, IntPtr clientData, OnUpdateModCallbackInternal completionDelegate);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_P2P_AcceptConnection (IntPtr handle, ref AcceptConnectionOptionsInternal options);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern ulong EOS_P2P_AddNotifyIncomingPacketQueueFull (IntPtr handle, ref AddNotifyIncomingPacketQueueFullOptionsInternal options, IntPtr clientData, OnIncomingPacketQueueFullCallbackInternal incomingPacketQueueFullHandler);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern ulong EOS_P2P_AddNotifyPeerConnectionClosed (IntPtr handle, ref AddNotifyPeerConnectionClosedOptionsInternal options, IntPtr clientData, OnRemoteConnectionClosedCallbackInternal connectionClosedHandler);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern ulong EOS_P2P_AddNotifyPeerConnectionEstablished (IntPtr handle, ref AddNotifyPeerConnectionEstablishedOptionsInternal options, IntPtr clientData, OnPeerConnectionEstablishedCallbackInternal connectionEstablishedHandler);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern ulong EOS_P2P_AddNotifyPeerConnectionInterrupted (IntPtr handle, ref AddNotifyPeerConnectionInterruptedOptionsInternal options, IntPtr clientData, OnPeerConnectionInterruptedCallbackInternal connectionInterruptedHandler);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern ulong EOS_P2P_AddNotifyPeerConnectionRequest (IntPtr handle, ref AddNotifyPeerConnectionRequestOptionsInternal options, IntPtr clientData, OnIncomingConnectionRequestCallbackInternal connectionRequestHandler);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_P2P_ClearPacketQueue (IntPtr handle, ref ClearPacketQueueOptionsInternal options);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_P2P_CloseConnection (IntPtr handle, ref CloseConnectionOptionsInternal options);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_P2P_CloseConnections (IntPtr handle, ref CloseConnectionsOptionsInternal options);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_P2P_GetNATType (IntPtr handle, ref GetNATTypeOptionsInternal options, ref NATType outNATType);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_P2P_GetNextReceivedPacketSize (IntPtr handle, ref GetNextReceivedPacketSizeOptionsInternal options, ref uint outPacketSizeBytes);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_P2P_GetPacketQueueInfo (IntPtr handle, ref GetPacketQueueInfoOptionsInternal options, ref PacketQueueInfoInternal outPacketQueueInfo);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_P2P_GetPortRange (IntPtr handle, ref GetPortRangeOptionsInternal options, ref ushort outPort, ref ushort outNumAdditionalPortsToTry);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_P2P_GetRelayControl (IntPtr handle, ref GetRelayControlOptionsInternal options, ref RelayControl outRelayControl);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_P2P_QueryNATType (IntPtr handle, ref QueryNATTypeOptionsInternal options, IntPtr clientData, OnQueryNATTypeCompleteCallbackInternal completionDelegate);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_P2P_ReceivePacket (IntPtr handle, ref ReceivePacketOptionsInternal options, ref IntPtr outPeerId, ref SocketIdInternal outSocketId, ref byte outChannel, IntPtr outData, ref uint outBytesWritten);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_P2P_RemoveNotifyIncomingPacketQueueFull (IntPtr handle, ulong notificationId);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_P2P_RemoveNotifyPeerConnectionClosed (IntPtr handle, ulong notificationId);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_P2P_RemoveNotifyPeerConnectionEstablished (IntPtr handle, ulong notificationId);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_P2P_RemoveNotifyPeerConnectionInterrupted (IntPtr handle, ulong notificationId);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_P2P_RemoveNotifyPeerConnectionRequest (IntPtr handle, ulong notificationId);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_P2P_SendPacket (IntPtr handle, ref SendPacketOptionsInternal options);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_P2P_SetPacketQueueSize (IntPtr handle, ref SetPacketQueueSizeOptionsInternal options);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_P2P_SetPortRange (IntPtr handle, ref SetPortRangeOptionsInternal options);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_P2P_SetRelayControl (IntPtr handle, ref SetRelayControlOptionsInternal options);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_Platform_CheckForLauncherAndRestart (IntPtr handle);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern IntPtr EOS_Platform_Create (ref Epic.OnlineServices.Platform.OptionsInternal options);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern IntPtr EOS_Platform_GetAchievementsInterface (IntPtr handle);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_Platform_GetActiveCountryCode (IntPtr handle, IntPtr localUserId, IntPtr outBuffer, ref int inOutBufferLength);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_Platform_GetActiveLocaleCode (IntPtr handle, IntPtr localUserId, IntPtr outBuffer, ref int inOutBufferLength);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern IntPtr EOS_Platform_GetAntiCheatClientInterface (IntPtr handle);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern IntPtr EOS_Platform_GetAntiCheatServerInterface (IntPtr handle);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern ApplicationStatus EOS_Platform_GetApplicationStatus (IntPtr handle);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern IntPtr EOS_Platform_GetAuthInterface (IntPtr handle);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern IntPtr EOS_Platform_GetConnectInterface (IntPtr handle);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern IntPtr EOS_Platform_GetCustomInvitesInterface (IntPtr handle);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_Platform_GetDesktopCrossplayStatus (IntPtr handle, ref GetDesktopCrossplayStatusOptionsInternal options, ref DesktopCrossplayStatusInfoInternal outDesktopCrossplayStatusInfo);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern IntPtr EOS_Platform_GetEcomInterface (IntPtr handle);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern IntPtr EOS_Platform_GetFriendsInterface (IntPtr handle);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern IntPtr EOS_Platform_GetIntegratedPlatformInterface (IntPtr handle);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern IntPtr EOS_Platform_GetKWSInterface (IntPtr handle);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern IntPtr EOS_Platform_GetLeaderboardsInterface (IntPtr handle);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern IntPtr EOS_Platform_GetLobbyInterface (IntPtr handle);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern IntPtr EOS_Platform_GetMetricsInterface (IntPtr handle);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern IntPtr EOS_Platform_GetModsInterface (IntPtr handle);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern NetworkStatus EOS_Platform_GetNetworkStatus (IntPtr handle);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_Platform_GetOverrideCountryCode (IntPtr handle, IntPtr outBuffer, ref int inOutBufferLength);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_Platform_GetOverrideLocaleCode (IntPtr handle, IntPtr outBuffer, ref int inOutBufferLength);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern IntPtr EOS_Platform_GetP2PInterface (IntPtr handle);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern IntPtr EOS_Platform_GetPlayerDataStorageInterface (IntPtr handle);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern IntPtr EOS_Platform_GetPresenceInterface (IntPtr handle);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern IntPtr EOS_Platform_GetProgressionSnapshotInterface (IntPtr handle);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern IntPtr EOS_Platform_GetRTCAdminInterface (IntPtr handle);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern IntPtr EOS_Platform_GetRTCInterface (IntPtr handle);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern IntPtr EOS_Platform_GetReportsInterface (IntPtr handle);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern IntPtr EOS_Platform_GetSanctionsInterface (IntPtr handle);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern IntPtr EOS_Platform_GetSessionsInterface (IntPtr handle);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern IntPtr EOS_Platform_GetStatsInterface (IntPtr handle);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern IntPtr EOS_Platform_GetTitleStorageInterface (IntPtr handle);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern IntPtr EOS_Platform_GetUIInterface (IntPtr handle);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern IntPtr EOS_Platform_GetUserInfoInterface (IntPtr handle);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_Platform_Release (IntPtr handle);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_Platform_SetApplicationStatus (IntPtr handle, ApplicationStatus newStatus);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_Platform_SetNetworkStatus (IntPtr handle, NetworkStatus newStatus);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_Platform_SetOverrideCountryCode (IntPtr handle, IntPtr newCountryCode);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_Platform_SetOverrideLocaleCode (IntPtr handle, IntPtr newLocaleCode);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_Platform_Tick (IntPtr handle);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_PlayerDataStorageFileTransferRequest_CancelRequest (IntPtr handle);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_PlayerDataStorageFileTransferRequest_GetFileRequestState (IntPtr handle);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_PlayerDataStorageFileTransferRequest_GetFilename (IntPtr handle, uint filenameStringBufferSizeBytes, IntPtr outStringBuffer, ref int outStringLength);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_PlayerDataStorageFileTransferRequest_Release (IntPtr playerDataStorageFileTransferHandle);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_PlayerDataStorage_CopyFileMetadataAtIndex (IntPtr handle, ref Epic.OnlineServices.PlayerDataStorage.CopyFileMetadataAtIndexOptionsInternal copyFileMetadataOptions, ref IntPtr outMetadata);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_PlayerDataStorage_CopyFileMetadataByFilename (IntPtr handle, ref Epic.OnlineServices.PlayerDataStorage.CopyFileMetadataByFilenameOptionsInternal copyFileMetadataOptions, ref IntPtr outMetadata);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_PlayerDataStorage_DeleteCache (IntPtr handle, ref Epic.OnlineServices.PlayerDataStorage.DeleteCacheOptionsInternal options, IntPtr clientData, Epic.OnlineServices.PlayerDataStorage.OnDeleteCacheCompleteCallbackInternal completionCallback);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_PlayerDataStorage_DeleteFile (IntPtr handle, ref DeleteFileOptionsInternal deleteOptions, IntPtr clientData, OnDeleteFileCompleteCallbackInternal completionCallback);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_PlayerDataStorage_DuplicateFile (IntPtr handle, ref DuplicateFileOptionsInternal duplicateOptions, IntPtr clientData, OnDuplicateFileCompleteCallbackInternal completionCallback);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_PlayerDataStorage_FileMetadata_Release (IntPtr fileMetadata);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_PlayerDataStorage_GetFileMetadataCount (IntPtr handle, ref Epic.OnlineServices.PlayerDataStorage.GetFileMetadataCountOptionsInternal getFileMetadataCountOptions, ref int outFileMetadataCount);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_PlayerDataStorage_QueryFile (IntPtr handle, ref Epic.OnlineServices.PlayerDataStorage.QueryFileOptionsInternal queryFileOptions, IntPtr clientData, Epic.OnlineServices.PlayerDataStorage.OnQueryFileCompleteCallbackInternal completionCallback);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_PlayerDataStorage_QueryFileList (IntPtr handle, ref Epic.OnlineServices.PlayerDataStorage.QueryFileListOptionsInternal queryFileListOptions, IntPtr clientData, Epic.OnlineServices.PlayerDataStorage.OnQueryFileListCompleteCallbackInternal completionCallback);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern IntPtr EOS_PlayerDataStorage_ReadFile (IntPtr handle, ref Epic.OnlineServices.PlayerDataStorage.ReadFileOptionsInternal readOptions, IntPtr clientData, Epic.OnlineServices.PlayerDataStorage.OnReadFileCompleteCallbackInternal completionCallback);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern IntPtr EOS_PlayerDataStorage_WriteFile (IntPtr handle, ref WriteFileOptionsInternal writeOptions, IntPtr clientData, OnWriteFileCompleteCallbackInternal completionCallback);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_PresenceModification_DeleteData (IntPtr handle, ref PresenceModificationDeleteDataOptionsInternal options);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_PresenceModification_Release (IntPtr presenceModificationHandle);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_PresenceModification_SetData (IntPtr handle, ref PresenceModificationSetDataOptionsInternal options);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_PresenceModification_SetJoinInfo (IntPtr handle, ref PresenceModificationSetJoinInfoOptionsInternal options);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_PresenceModification_SetRawRichText (IntPtr handle, ref PresenceModificationSetRawRichTextOptionsInternal options);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_PresenceModification_SetStatus (IntPtr handle, ref PresenceModificationSetStatusOptionsInternal options);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern ulong EOS_Presence_AddNotifyJoinGameAccepted (IntPtr handle, ref AddNotifyJoinGameAcceptedOptionsInternal options, IntPtr clientData, OnJoinGameAcceptedCallbackInternal notificationFn);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern ulong EOS_Presence_AddNotifyOnPresenceChanged (IntPtr handle, ref AddNotifyOnPresenceChangedOptionsInternal options, IntPtr clientData, OnPresenceChangedCallbackInternal notificationHandler);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_Presence_CopyPresence (IntPtr handle, ref CopyPresenceOptionsInternal options, ref IntPtr outPresence);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_Presence_CreatePresenceModification (IntPtr handle, ref CreatePresenceModificationOptionsInternal options, ref IntPtr outPresenceModificationHandle);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_Presence_GetJoinInfo (IntPtr handle, ref GetJoinInfoOptionsInternal options, IntPtr outBuffer, ref int inOutBufferLength);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern int EOS_Presence_HasPresence (IntPtr handle, ref HasPresenceOptionsInternal options);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_Presence_Info_Release (IntPtr presenceInfo);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_Presence_QueryPresence (IntPtr handle, ref QueryPresenceOptionsInternal options, IntPtr clientData, OnQueryPresenceCompleteCallbackInternal completionDelegate);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_Presence_RemoveNotifyJoinGameAccepted (IntPtr handle, ulong inId);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_Presence_RemoveNotifyOnPresenceChanged (IntPtr handle, ulong notificationId);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_Presence_SetPresence (IntPtr handle, ref SetPresenceOptionsInternal options, IntPtr clientData, SetPresenceCompleteCallbackInternal completionDelegate);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern IntPtr EOS_ProductUserId_FromString (IntPtr productUserIdString);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern int EOS_ProductUserId_IsValid (IntPtr accountId);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_ProductUserId_ToString (IntPtr accountId, IntPtr outBuffer, ref int inOutBufferLength);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_ProgressionSnapshot_AddProgression (IntPtr handle, ref AddProgressionOptionsInternal options);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_ProgressionSnapshot_BeginSnapshot (IntPtr handle, ref BeginSnapshotOptionsInternal options, ref uint outSnapshotId);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_ProgressionSnapshot_DeleteSnapshot (IntPtr handle, ref DeleteSnapshotOptionsInternal options, IntPtr clientData, OnDeleteSnapshotCallbackInternal completionDelegate);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_ProgressionSnapshot_EndSnapshot (IntPtr handle, ref EndSnapshotOptionsInternal options);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_ProgressionSnapshot_SubmitSnapshot (IntPtr handle, ref SubmitSnapshotOptionsInternal options, IntPtr clientData, OnSubmitSnapshotCallbackInternal completionDelegate);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_RTCAdmin_CopyUserTokenByIndex (IntPtr handle, ref CopyUserTokenByIndexOptionsInternal options, ref IntPtr outUserToken);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_RTCAdmin_CopyUserTokenByUserId (IntPtr handle, ref CopyUserTokenByUserIdOptionsInternal options, ref IntPtr outUserToken);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_RTCAdmin_Kick (IntPtr handle, ref KickOptionsInternal options, IntPtr clientData, OnKickCompleteCallbackInternal completionDelegate);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_RTCAdmin_QueryJoinRoomToken (IntPtr handle, ref QueryJoinRoomTokenOptionsInternal options, IntPtr clientData, OnQueryJoinRoomTokenCompleteCallbackInternal completionDelegate);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_RTCAdmin_SetParticipantHardMute (IntPtr handle, ref SetParticipantHardMuteOptionsInternal options, IntPtr clientData, OnSetParticipantHardMuteCompleteCallbackInternal completionDelegate);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_RTCAdmin_UserToken_Release (IntPtr userToken);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern ulong EOS_RTCAudio_AddNotifyAudioBeforeRender (IntPtr handle, ref AddNotifyAudioBeforeRenderOptionsInternal options, IntPtr clientData, OnAudioBeforeRenderCallbackInternal completionDelegate);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern ulong EOS_RTCAudio_AddNotifyAudioBeforeSend (IntPtr handle, ref AddNotifyAudioBeforeSendOptionsInternal options, IntPtr clientData, OnAudioBeforeSendCallbackInternal completionDelegate);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern ulong EOS_RTCAudio_AddNotifyAudioDevicesChanged (IntPtr handle, ref AddNotifyAudioDevicesChangedOptionsInternal options, IntPtr clientData, OnAudioDevicesChangedCallbackInternal completionDelegate);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern ulong EOS_RTCAudio_AddNotifyAudioInputState (IntPtr handle, ref AddNotifyAudioInputStateOptionsInternal options, IntPtr clientData, OnAudioInputStateCallbackInternal completionDelegate);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern ulong EOS_RTCAudio_AddNotifyAudioOutputState (IntPtr handle, ref AddNotifyAudioOutputStateOptionsInternal options, IntPtr clientData, OnAudioOutputStateCallbackInternal completionDelegate);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern ulong EOS_RTCAudio_AddNotifyParticipantUpdated (IntPtr handle, ref AddNotifyParticipantUpdatedOptionsInternal options, IntPtr clientData, OnParticipantUpdatedCallbackInternal completionDelegate);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_RTCAudio_CopyInputDeviceInformationByIndex (IntPtr handle, ref CopyInputDeviceInformationByIndexOptionsInternal options, ref IntPtr outInputDeviceInformation);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_RTCAudio_CopyOutputDeviceInformationByIndex (IntPtr handle, ref CopyOutputDeviceInformationByIndexOptionsInternal options, ref IntPtr outOutputDeviceInformation);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern IntPtr EOS_RTCAudio_GetAudioInputDeviceByIndex (IntPtr handle, ref GetAudioInputDeviceByIndexOptionsInternal options);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern uint EOS_RTCAudio_GetAudioInputDevicesCount (IntPtr handle, ref GetAudioInputDevicesCountOptionsInternal options);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern IntPtr EOS_RTCAudio_GetAudioOutputDeviceByIndex (IntPtr handle, ref GetAudioOutputDeviceByIndexOptionsInternal options);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern uint EOS_RTCAudio_GetAudioOutputDevicesCount (IntPtr handle, ref GetAudioOutputDevicesCountOptionsInternal options);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern uint EOS_RTCAudio_GetInputDevicesCount (IntPtr handle, ref GetInputDevicesCountOptionsInternal options);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern uint EOS_RTCAudio_GetOutputDevicesCount (IntPtr handle, ref GetOutputDevicesCountOptionsInternal options);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_RTCAudio_InputDeviceInformation_Release (IntPtr deviceInformation);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_RTCAudio_OutputDeviceInformation_Release (IntPtr deviceInformation);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_RTCAudio_QueryInputDevicesInformation (IntPtr handle, ref QueryInputDevicesInformationOptionsInternal options, IntPtr clientData, OnQueryInputDevicesInformationCallbackInternal completionDelegate);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_RTCAudio_QueryOutputDevicesInformation (IntPtr handle, ref QueryOutputDevicesInformationOptionsInternal options, IntPtr clientData, OnQueryOutputDevicesInformationCallbackInternal completionDelegate);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_RTCAudio_RegisterPlatformAudioUser (IntPtr handle, ref RegisterPlatformAudioUserOptionsInternal options);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_RTCAudio_RegisterPlatformUser (IntPtr handle, ref RegisterPlatformUserOptionsInternal options, IntPtr clientData, OnRegisterPlatformUserCallbackInternal completionDelegate);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_RTCAudio_RemoveNotifyAudioBeforeRender (IntPtr handle, ulong notificationId);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_RTCAudio_RemoveNotifyAudioBeforeSend (IntPtr handle, ulong notificationId);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_RTCAudio_RemoveNotifyAudioDevicesChanged (IntPtr handle, ulong notificationId);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_RTCAudio_RemoveNotifyAudioInputState (IntPtr handle, ulong notificationId);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_RTCAudio_RemoveNotifyAudioOutputState (IntPtr handle, ulong notificationId);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_RTCAudio_RemoveNotifyParticipantUpdated (IntPtr handle, ulong notificationId);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_RTCAudio_SendAudio (IntPtr handle, ref SendAudioOptionsInternal options);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_RTCAudio_SetAudioInputSettings (IntPtr handle, ref SetAudioInputSettingsOptionsInternal options);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_RTCAudio_SetAudioOutputSettings (IntPtr handle, ref SetAudioOutputSettingsOptionsInternal options);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_RTCAudio_SetInputDeviceSettings (IntPtr handle, ref SetInputDeviceSettingsOptionsInternal options, IntPtr clientData, OnSetInputDeviceSettingsCallbackInternal completionDelegate);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_RTCAudio_SetOutputDeviceSettings (IntPtr handle, ref SetOutputDeviceSettingsOptionsInternal options, IntPtr clientData, OnSetOutputDeviceSettingsCallbackInternal completionDelegate);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_RTCAudio_UnregisterPlatformAudioUser (IntPtr handle, ref UnregisterPlatformAudioUserOptionsInternal options);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_RTCAudio_UnregisterPlatformUser (IntPtr handle, ref UnregisterPlatformUserOptionsInternal options, IntPtr clientData, OnUnregisterPlatformUserCallbackInternal completionDelegate);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_RTCAudio_UpdateParticipantVolume (IntPtr handle, ref UpdateParticipantVolumeOptionsInternal options, IntPtr clientData, OnUpdateParticipantVolumeCallbackInternal completionDelegate);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_RTCAudio_UpdateReceiving (IntPtr handle, ref UpdateReceivingOptionsInternal options, IntPtr clientData, OnUpdateReceivingCallbackInternal completionDelegate);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_RTCAudio_UpdateReceivingVolume (IntPtr handle, ref UpdateReceivingVolumeOptionsInternal options, IntPtr clientData, OnUpdateReceivingVolumeCallbackInternal completionDelegate);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_RTCAudio_UpdateSending (IntPtr handle, ref UpdateSendingOptionsInternal options, IntPtr clientData, OnUpdateSendingCallbackInternal completionDelegate);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_RTCAudio_UpdateSendingVolume (IntPtr handle, ref UpdateSendingVolumeOptionsInternal options, IntPtr clientData, OnUpdateSendingVolumeCallbackInternal completionDelegate);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern ulong EOS_RTC_AddNotifyDisconnected (IntPtr handle, ref AddNotifyDisconnectedOptionsInternal options, IntPtr clientData, OnDisconnectedCallbackInternal completionDelegate);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern ulong EOS_RTC_AddNotifyParticipantStatusChanged (IntPtr handle, ref AddNotifyParticipantStatusChangedOptionsInternal options, IntPtr clientData, OnParticipantStatusChangedCallbackInternal completionDelegate);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern ulong EOS_RTC_AddNotifyRoomStatisticsUpdated (IntPtr handle, ref AddNotifyRoomStatisticsUpdatedOptionsInternal options, IntPtr clientData, OnRoomStatisticsUpdatedCallbackInternal statisticsUpdateHandler);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_RTC_BlockParticipant (IntPtr handle, ref BlockParticipantOptionsInternal options, IntPtr clientData, OnBlockParticipantCallbackInternal completionDelegate);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern IntPtr EOS_RTC_GetAudioInterface (IntPtr handle);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_RTC_JoinRoom (IntPtr handle, ref JoinRoomOptionsInternal options, IntPtr clientData, OnJoinRoomCallbackInternal completionDelegate);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_RTC_LeaveRoom (IntPtr handle, ref LeaveRoomOptionsInternal options, IntPtr clientData, OnLeaveRoomCallbackInternal completionDelegate);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_RTC_RemoveNotifyDisconnected (IntPtr handle, ulong notificationId);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_RTC_RemoveNotifyParticipantStatusChanged (IntPtr handle, ulong notificationId);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_RTC_RemoveNotifyRoomStatisticsUpdated (IntPtr handle, ulong notificationId);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_RTC_SetRoomSetting (IntPtr handle, ref SetRoomSettingOptionsInternal options);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_RTC_SetSetting (IntPtr handle, ref SetSettingOptionsInternal options);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_Reports_SendPlayerBehaviorReport (IntPtr handle, ref SendPlayerBehaviorReportOptionsInternal options, IntPtr clientData, OnSendPlayerBehaviorReportCompleteCallbackInternal completionDelegate);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_Sanctions_CopyPlayerSanctionByIndex (IntPtr handle, ref CopyPlayerSanctionByIndexOptionsInternal options, ref IntPtr outSanction);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern uint EOS_Sanctions_GetPlayerSanctionCount (IntPtr handle, ref GetPlayerSanctionCountOptionsInternal options);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_Sanctions_PlayerSanction_Release (IntPtr sanction);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_Sanctions_QueryActivePlayerSanctions (IntPtr handle, ref QueryActivePlayerSanctionsOptionsInternal options, IntPtr clientData, OnQueryActivePlayerSanctionsCallbackInternal completionDelegate);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_SessionDetails_Attribute_Release (IntPtr sessionAttribute);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_SessionDetails_CopyInfo (IntPtr handle, ref SessionDetailsCopyInfoOptionsInternal options, ref IntPtr outSessionInfo);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_SessionDetails_CopySessionAttributeByIndex (IntPtr handle, ref SessionDetailsCopySessionAttributeByIndexOptionsInternal options, ref IntPtr outSessionAttribute);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_SessionDetails_CopySessionAttributeByKey (IntPtr handle, ref SessionDetailsCopySessionAttributeByKeyOptionsInternal options, ref IntPtr outSessionAttribute);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern uint EOS_SessionDetails_GetSessionAttributeCount (IntPtr handle, ref SessionDetailsGetSessionAttributeCountOptionsInternal options);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_SessionDetails_Info_Release (IntPtr sessionInfo);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_SessionDetails_Release (IntPtr sessionHandle);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_SessionModification_AddAttribute (IntPtr handle, ref SessionModificationAddAttributeOptionsInternal options);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_SessionModification_Release (IntPtr sessionModificationHandle);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_SessionModification_RemoveAttribute (IntPtr handle, ref SessionModificationRemoveAttributeOptionsInternal options);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_SessionModification_SetAllowedPlatformIds (IntPtr handle, ref SessionModificationSetAllowedPlatformIdsOptionsInternal options);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_SessionModification_SetBucketId (IntPtr handle, ref SessionModificationSetBucketIdOptionsInternal options);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_SessionModification_SetHostAddress (IntPtr handle, ref SessionModificationSetHostAddressOptionsInternal options);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_SessionModification_SetInvitesAllowed (IntPtr handle, ref SessionModificationSetInvitesAllowedOptionsInternal options);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_SessionModification_SetJoinInProgressAllowed (IntPtr handle, ref SessionModificationSetJoinInProgressAllowedOptionsInternal options);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_SessionModification_SetMaxPlayers (IntPtr handle, ref SessionModificationSetMaxPlayersOptionsInternal options);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_SessionModification_SetPermissionLevel (IntPtr handle, ref SessionModificationSetPermissionLevelOptionsInternal options);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_SessionSearch_CopySearchResultByIndex (IntPtr handle, ref SessionSearchCopySearchResultByIndexOptionsInternal options, ref IntPtr outSessionHandle);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_SessionSearch_Find (IntPtr handle, ref SessionSearchFindOptionsInternal options, IntPtr clientData, SessionSearchOnFindCallbackInternal completionDelegate);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern uint EOS_SessionSearch_GetSearchResultCount (IntPtr handle, ref SessionSearchGetSearchResultCountOptionsInternal options);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_SessionSearch_Release (IntPtr sessionSearchHandle);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_SessionSearch_RemoveParameter (IntPtr handle, ref SessionSearchRemoveParameterOptionsInternal options);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_SessionSearch_SetMaxResults (IntPtr handle, ref SessionSearchSetMaxResultsOptionsInternal options);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_SessionSearch_SetParameter (IntPtr handle, ref SessionSearchSetParameterOptionsInternal options);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_SessionSearch_SetSessionId (IntPtr handle, ref SessionSearchSetSessionIdOptionsInternal options);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_SessionSearch_SetTargetUserId (IntPtr handle, ref SessionSearchSetTargetUserIdOptionsInternal options);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern ulong EOS_Sessions_AddNotifyJoinSessionAccepted (IntPtr handle, ref AddNotifyJoinSessionAcceptedOptionsInternal options, IntPtr clientData, OnJoinSessionAcceptedCallbackInternal notificationFn);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern ulong EOS_Sessions_AddNotifyLeaveSessionRequested (IntPtr handle, ref AddNotifyLeaveSessionRequestedOptionsInternal options, IntPtr clientData, OnLeaveSessionRequestedCallbackInternal notificationFn);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern ulong EOS_Sessions_AddNotifySendSessionNativeInviteRequested (IntPtr handle, ref AddNotifySendSessionNativeInviteRequestedOptionsInternal options, IntPtr clientData, OnSendSessionNativeInviteRequestedCallbackInternal notificationFn);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern ulong EOS_Sessions_AddNotifySessionInviteAccepted (IntPtr handle, ref AddNotifySessionInviteAcceptedOptionsInternal options, IntPtr clientData, OnSessionInviteAcceptedCallbackInternal notificationFn);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern ulong EOS_Sessions_AddNotifySessionInviteReceived (IntPtr handle, ref AddNotifySessionInviteReceivedOptionsInternal options, IntPtr clientData, OnSessionInviteReceivedCallbackInternal notificationFn);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern ulong EOS_Sessions_AddNotifySessionInviteRejected (IntPtr handle, ref AddNotifySessionInviteRejectedOptionsInternal options, IntPtr clientData, OnSessionInviteRejectedCallbackInternal notificationFn);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_Sessions_CopyActiveSessionHandle (IntPtr handle, ref CopyActiveSessionHandleOptionsInternal options, ref IntPtr outSessionHandle);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_Sessions_CopySessionHandleByInviteId (IntPtr handle, ref CopySessionHandleByInviteIdOptionsInternal options, ref IntPtr outSessionHandle);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_Sessions_CopySessionHandleByUiEventId (IntPtr handle, ref CopySessionHandleByUiEventIdOptionsInternal options, ref IntPtr outSessionHandle);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_Sessions_CopySessionHandleForPresence (IntPtr handle, ref CopySessionHandleForPresenceOptionsInternal options, ref IntPtr outSessionHandle);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_Sessions_CreateSessionModification (IntPtr handle, ref CreateSessionModificationOptionsInternal options, ref IntPtr outSessionModificationHandle);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_Sessions_CreateSessionSearch (IntPtr handle, ref CreateSessionSearchOptionsInternal options, ref IntPtr outSessionSearchHandle);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_Sessions_DestroySession (IntPtr handle, ref DestroySessionOptionsInternal options, IntPtr clientData, OnDestroySessionCallbackInternal completionDelegate);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_Sessions_DumpSessionState (IntPtr handle, ref DumpSessionStateOptionsInternal options);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_Sessions_EndSession (IntPtr handle, ref Epic.OnlineServices.Sessions.EndSessionOptionsInternal options, IntPtr clientData, OnEndSessionCallbackInternal completionDelegate);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern uint EOS_Sessions_GetInviteCount (IntPtr handle, ref Epic.OnlineServices.Sessions.GetInviteCountOptionsInternal options);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_Sessions_GetInviteIdByIndex (IntPtr handle, ref Epic.OnlineServices.Sessions.GetInviteIdByIndexOptionsInternal options, IntPtr outBuffer, ref int inOutBufferLength);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_Sessions_IsUserInSession (IntPtr handle, ref IsUserInSessionOptionsInternal options);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_Sessions_JoinSession (IntPtr handle, ref JoinSessionOptionsInternal options, IntPtr clientData, OnJoinSessionCallbackInternal completionDelegate);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_Sessions_QueryInvites (IntPtr handle, ref Epic.OnlineServices.Sessions.QueryInvitesOptionsInternal options, IntPtr clientData, Epic.OnlineServices.Sessions.OnQueryInvitesCallbackInternal completionDelegate);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_Sessions_RegisterPlayers (IntPtr handle, ref RegisterPlayersOptionsInternal options, IntPtr clientData, OnRegisterPlayersCallbackInternal completionDelegate);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_Sessions_RejectInvite (IntPtr handle, ref Epic.OnlineServices.Sessions.RejectInviteOptionsInternal options, IntPtr clientData, Epic.OnlineServices.Sessions.OnRejectInviteCallbackInternal completionDelegate);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_Sessions_RemoveNotifyJoinSessionAccepted (IntPtr handle, ulong inId);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_Sessions_RemoveNotifyLeaveSessionRequested (IntPtr handle, ulong inId);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_Sessions_RemoveNotifySendSessionNativeInviteRequested (IntPtr handle, ulong inId);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_Sessions_RemoveNotifySessionInviteAccepted (IntPtr handle, ulong inId);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_Sessions_RemoveNotifySessionInviteReceived (IntPtr handle, ulong inId);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_Sessions_RemoveNotifySessionInviteRejected (IntPtr handle, ulong inId);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_Sessions_SendInvite (IntPtr handle, ref Epic.OnlineServices.Sessions.SendInviteOptionsInternal options, IntPtr clientData, Epic.OnlineServices.Sessions.OnSendInviteCallbackInternal completionDelegate);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_Sessions_StartSession (IntPtr handle, ref StartSessionOptionsInternal options, IntPtr clientData, OnStartSessionCallbackInternal completionDelegate);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_Sessions_UnregisterPlayers (IntPtr handle, ref UnregisterPlayersOptionsInternal options, IntPtr clientData, OnUnregisterPlayersCallbackInternal completionDelegate);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_Sessions_UpdateSession (IntPtr handle, ref UpdateSessionOptionsInternal options, IntPtr clientData, OnUpdateSessionCallbackInternal completionDelegate);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_Sessions_UpdateSessionModification (IntPtr handle, ref UpdateSessionModificationOptionsInternal options, ref IntPtr outSessionModificationHandle);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_Shutdown ();

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_Stats_CopyStatByIndex (IntPtr handle, ref CopyStatByIndexOptionsInternal options, ref IntPtr outStat);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_Stats_CopyStatByName (IntPtr handle, ref CopyStatByNameOptionsInternal options, ref IntPtr outStat);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern uint EOS_Stats_GetStatsCount (IntPtr handle, ref GetStatCountOptionsInternal options);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_Stats_IngestStat (IntPtr handle, ref IngestStatOptionsInternal options, IntPtr clientData, OnIngestStatCompleteCallbackInternal completionDelegate);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_Stats_QueryStats (IntPtr handle, ref QueryStatsOptionsInternal options, IntPtr clientData, OnQueryStatsCompleteCallbackInternal completionDelegate);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_Stats_Stat_Release (IntPtr stat);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_TitleStorageFileTransferRequest_CancelRequest (IntPtr handle);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_TitleStorageFileTransferRequest_GetFileRequestState (IntPtr handle);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_TitleStorageFileTransferRequest_GetFilename (IntPtr handle, uint filenameStringBufferSizeBytes, IntPtr outStringBuffer, ref int outStringLength);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_TitleStorageFileTransferRequest_Release (IntPtr titleStorageFileTransferHandle);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_TitleStorage_CopyFileMetadataAtIndex (IntPtr handle, ref Epic.OnlineServices.TitleStorage.CopyFileMetadataAtIndexOptionsInternal options, ref IntPtr outMetadata);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_TitleStorage_CopyFileMetadataByFilename (IntPtr handle, ref Epic.OnlineServices.TitleStorage.CopyFileMetadataByFilenameOptionsInternal options, ref IntPtr outMetadata);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_TitleStorage_DeleteCache (IntPtr handle, ref Epic.OnlineServices.TitleStorage.DeleteCacheOptionsInternal options, IntPtr clientData, Epic.OnlineServices.TitleStorage.OnDeleteCacheCompleteCallbackInternal completionCallback);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_TitleStorage_FileMetadata_Release (IntPtr fileMetadata);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern uint EOS_TitleStorage_GetFileMetadataCount (IntPtr handle, ref Epic.OnlineServices.TitleStorage.GetFileMetadataCountOptionsInternal options);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_TitleStorage_QueryFile (IntPtr handle, ref Epic.OnlineServices.TitleStorage.QueryFileOptionsInternal options, IntPtr clientData, Epic.OnlineServices.TitleStorage.OnQueryFileCompleteCallbackInternal completionCallback);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_TitleStorage_QueryFileList (IntPtr handle, ref Epic.OnlineServices.TitleStorage.QueryFileListOptionsInternal options, IntPtr clientData, Epic.OnlineServices.TitleStorage.OnQueryFileListCompleteCallbackInternal completionCallback);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern IntPtr EOS_TitleStorage_ReadFile (IntPtr handle, ref Epic.OnlineServices.TitleStorage.ReadFileOptionsInternal options, IntPtr clientData, Epic.OnlineServices.TitleStorage.OnReadFileCompleteCallbackInternal completionCallback);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_UI_AcknowledgeEventId (IntPtr handle, ref AcknowledgeEventIdOptionsInternal options);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern ulong EOS_UI_AddNotifyDisplaySettingsUpdated (IntPtr handle, ref AddNotifyDisplaySettingsUpdatedOptionsInternal options, IntPtr clientData, OnDisplaySettingsUpdatedCallbackInternal notificationFn);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern ulong EOS_UI_AddNotifyMemoryMonitor (IntPtr handle, ref AddNotifyMemoryMonitorOptionsInternal options, IntPtr clientData, OnMemoryMonitorCallbackInternal notificationFn);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern int EOS_UI_GetFriendsExclusiveInput (IntPtr handle, ref GetFriendsExclusiveInputOptionsInternal options);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern int EOS_UI_GetFriendsVisible (IntPtr handle, ref GetFriendsVisibleOptionsInternal options);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern NotificationLocation EOS_UI_GetNotificationLocationPreference (IntPtr handle);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern InputStateButtonFlags EOS_UI_GetToggleFriendsButton (IntPtr handle, ref GetToggleFriendsButtonOptionsInternal options);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern KeyCombination EOS_UI_GetToggleFriendsKey (IntPtr handle, ref GetToggleFriendsKeyOptionsInternal options);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_UI_HideFriends (IntPtr handle, ref HideFriendsOptionsInternal options, IntPtr clientData, OnHideFriendsCallbackInternal completionDelegate);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern int EOS_UI_IsSocialOverlayPaused (IntPtr handle, ref IsSocialOverlayPausedOptionsInternal options);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern int EOS_UI_IsValidButtonCombination (IntPtr handle, InputStateButtonFlags buttonCombination);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern int EOS_UI_IsValidKeyCombination (IntPtr handle, KeyCombination keyCombination);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_UI_PauseSocialOverlay (IntPtr handle, ref PauseSocialOverlayOptionsInternal options);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_UI_PrePresent (IntPtr handle, ref PrePresentOptionsInternal options);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_UI_RemoveNotifyDisplaySettingsUpdated (IntPtr handle, ulong id);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_UI_RemoveNotifyMemoryMonitor (IntPtr handle, ulong id);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_UI_ReportInputState (IntPtr handle, ref ReportInputStateOptionsInternal options);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_UI_SetDisplayPreference (IntPtr handle, ref SetDisplayPreferenceOptionsInternal options);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_UI_SetToggleFriendsButton (IntPtr handle, ref SetToggleFriendsButtonOptionsInternal options);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_UI_SetToggleFriendsKey (IntPtr handle, ref SetToggleFriendsKeyOptionsInternal options);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_UI_ShowBlockPlayer (IntPtr handle, ref ShowBlockPlayerOptionsInternal options, IntPtr clientData, OnShowBlockPlayerCallbackInternal completionDelegate);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_UI_ShowFriends (IntPtr handle, ref ShowFriendsOptionsInternal options, IntPtr clientData, OnShowFriendsCallbackInternal completionDelegate);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_UI_ShowNativeProfile (IntPtr handle, ref ShowNativeProfileOptionsInternal options, IntPtr clientData, OnShowNativeProfileCallbackInternal completionDelegate);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_UI_ShowReportPlayer (IntPtr handle, ref ShowReportPlayerOptionsInternal options, IntPtr clientData, OnShowReportPlayerCallbackInternal completionDelegate);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_UserInfo_BestDisplayName_Release (IntPtr bestDisplayName);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_UserInfo_CopyBestDisplayName (IntPtr handle, ref CopyBestDisplayNameOptionsInternal options, ref IntPtr outBestDisplayName);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_UserInfo_CopyBestDisplayNameWithPlatform (IntPtr handle, ref CopyBestDisplayNameWithPlatformOptionsInternal options, ref IntPtr outBestDisplayName);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_UserInfo_CopyExternalUserInfoByAccountId (IntPtr handle, ref CopyExternalUserInfoByAccountIdOptionsInternal options, ref IntPtr outExternalUserInfo);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_UserInfo_CopyExternalUserInfoByAccountType (IntPtr handle, ref CopyExternalUserInfoByAccountTypeOptionsInternal options, ref IntPtr outExternalUserInfo);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_UserInfo_CopyExternalUserInfoByIndex (IntPtr handle, ref CopyExternalUserInfoByIndexOptionsInternal options, ref IntPtr outExternalUserInfo);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_UserInfo_CopyUserInfo (IntPtr handle, ref CopyUserInfoOptionsInternal options, ref IntPtr outUserInfo);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_UserInfo_ExternalUserInfo_Release (IntPtr externalUserInfo);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern uint EOS_UserInfo_GetExternalUserInfoCount (IntPtr handle, ref GetExternalUserInfoCountOptionsInternal options);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern uint EOS_UserInfo_GetLocalPlatformType (IntPtr handle, ref GetLocalPlatformTypeOptionsInternal options);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_UserInfo_QueryUserInfo (IntPtr handle, ref QueryUserInfoOptionsInternal options, IntPtr clientData, OnQueryUserInfoCallbackInternal completionDelegate);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_UserInfo_QueryUserInfoByDisplayName (IntPtr handle, ref QueryUserInfoByDisplayNameOptionsInternal options, IntPtr clientData, OnQueryUserInfoByDisplayNameCallbackInternal completionDelegate);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_UserInfo_QueryUserInfoByExternalAccount (IntPtr handle, ref QueryUserInfoByExternalAccountOptionsInternal options, IntPtr clientData, OnQueryUserInfoByExternalAccountCallbackInternal completionDelegate);

	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_UserInfo_Release (IntPtr userInfo);
}
