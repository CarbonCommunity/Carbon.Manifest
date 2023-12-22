public enum Result
{
	Success = 0,
	NoConnection = 1,
	InvalidCredentials = 2,
	InvalidUser = 3,
	InvalidAuth = 4,
	AccessDenied = 5,
	MissingPermissions = 6,
	TokenNotAccount = 7,
	TooManyRequests = 8,
	AlreadyPending = 9,
	InvalidParameters = 10,
	InvalidRequest = 11,
	UnrecognizedResponse = 12,
	IncompatibleVersion = 13,
	NotConfigured = 14,
	AlreadyConfigured = 15,
	NotImplemented = 16,
	Canceled = 17,
	NotFound = 18,
	OperationWillRetry = 19,
	NoChange = 20,
	VersionMismatch = 21,
	LimitExceeded = 22,
	Disabled = 23,
	DuplicateNotAllowed = 24,
	MissingParametersDEPRECATED = 25,
	InvalidSandboxId = 26,
	TimedOut = 27,
	PartialResult = 28,
	MissingRole = 29,
	MissingFeature = 30,
	InvalidSandbox = 31,
	InvalidDeployment = 32,
	InvalidProduct = 33,
	InvalidProductUserID = 34,
	ServiceFailure = 35,
	CacheDirectoryMissing = 36,
	CacheDirectoryInvalid = 37,
	InvalidState = 38,
	RequestInProgress = 39,
	ApplicationSuspended = 40,
	NetworkDisconnected = 41,
	AuthAccountLocked = 1001,
	AuthAccountLockedForUpdate = 1002,
	AuthInvalidRefreshToken = 1003,
	AuthInvalidToken = 1004,
	AuthAuthenticationFailure = 1005,
	AuthInvalidPlatformToken = 1006,
	AuthWrongAccount = 1007,
	AuthWrongClient = 1008,
	AuthFullAccountRequired = 1009,
	AuthHeadlessAccountRequired = 1010,
	AuthPasswordResetRequired = 1011,
	AuthPasswordCannotBeReused = 1012,
	AuthExpired = 1013,
	AuthScopeConsentRequired = 1014,
	AuthApplicationNotFound = 1015,
	AuthScopeNotFound = 1016,
	AuthAccountFeatureRestricted = 1017,
	AuthAccountPortalLoadError = 1018,
	AuthCorrectiveActionRequired = 1019,
	AuthPinGrantCode = 1020,
	AuthPinGrantExpired = 1021,
	AuthPinGrantPending = 1022,
	AuthExternalAuthNotLinked = 1030,
	AuthExternalAuthRevoked = 1032,
	AuthExternalAuthInvalid = 1033,
	AuthExternalAuthRestricted = 1034,
	AuthExternalAuthCannotLogin = 1035,
	AuthExternalAuthExpired = 1036,
	AuthExternalAuthIsLastLoginType = 1037,
	AuthExchangeCodeNotFound = 1040,
	AuthOriginatingExchangeCodeSessionExpired = 1041,
	AuthAccountNotActive = 1050,
	AuthMFARequired = 1060,
	AuthParentalControls = 1070,
	AuthNoRealId = 1080,
	FriendsInviteAwaitingAcceptance = 2000,
	FriendsNoInvitation = 2001,
	FriendsAlreadyFriends = 2003,
	FriendsNotFriends = 2004,
	FriendsTargetUserTooManyInvites = 2005,
	FriendsLocalUserTooManyInvites = 2006,
	FriendsTargetUserFriendLimitExceeded = 2007,
	FriendsLocalUserFriendLimitExceeded = 2008,
	PresenceDataInvalid = 3000,
	PresenceDataLengthInvalid = 3001,
	PresenceDataKeyInvalid = 3002,
	PresenceDataKeyLengthInvalid = 3003,
	PresenceDataValueInvalid = 3004,
	PresenceDataValueLengthInvalid = 3005,
	PresenceRichTextInvalid = 3006,
	PresenceRichTextLengthInvalid = 3007,
	PresenceStatusInvalid = 3008,
	EcomEntitlementStale = 4000,
	EcomCatalogOfferStale = 4001,
	EcomCatalogItemStale = 4002,
	EcomCatalogOfferPriceInvalid = 4003,
	EcomCheckoutLoadError = 4004,
	SessionsSessionInProgress = 5000,
	SessionsTooManyPlayers = 5001,
	SessionsNoPermission = 5002,
	SessionsSessionAlreadyExists = 5003,
	SessionsInvalidLock = 5004,
	SessionsInvalidSession = 5005,
	SessionsSandboxNotAllowed = 5006,
	SessionsInviteFailed = 5007,
	SessionsInviteNotFound = 5008,
	SessionsUpsertNotAllowed = 5009,
	SessionsAggregationFailed = 5010,
	SessionsHostAtCapacity = 5011,
	SessionsSandboxAtCapacity = 5012,
	SessionsSessionNotAnonymous = 5013,
	SessionsOutOfSync = 5014,
	SessionsTooManyInvites = 5015,
	SessionsPresenceSessionExists = 5016,
	SessionsDeploymentAtCapacity = 5017,
	SessionsNotAllowed = 5018,
	SessionsPlayerSanctioned = 5019,
	PlayerDataStorageFilenameInvalid = 6000,
	PlayerDataStorageFilenameLengthInvalid = 6001,
	PlayerDataStorageFilenameInvalidChars = 6002,
	PlayerDataStorageFileSizeTooLarge = 6003,
	PlayerDataStorageFileSizeInvalid = 6004,
	PlayerDataStorageFileHandleInvalid = 6005,
	PlayerDataStorageDataInvalid = 6006,
	PlayerDataStorageDataLengthInvalid = 6007,
	PlayerDataStorageStartIndexInvalid = 6008,
	PlayerDataStorageRequestInProgress = 6009,
	PlayerDataStorageUserThrottled = 6010,
	PlayerDataStorageEncryptionKeyNotSet = 6011,
	PlayerDataStorageUserErrorFromDataCallback = 6012,
	PlayerDataStorageFileHeaderHasNewerVersion = 6013,
	PlayerDataStorageFileCorrupted = 6014,
	ConnectExternalTokenValidationFailed = 7000,
	ConnectUserAlreadyExists = 7001,
	ConnectAuthExpired = 7002,
	ConnectInvalidToken = 7003,
	ConnectUnsupportedTokenType = 7004,
	ConnectLinkAccountFailed = 7005,
	ConnectExternalServiceUnavailable = 7006,
	ConnectExternalServiceConfigurationFailure = 7007,
	ConnectLinkAccountFailedMissingNintendoIdAccountDEPRECATED = 7008,
	SocialOverlayLoadError = 8000,
	LobbyNotOwner = 9000,
	LobbyInvalidLock = 9001,
	LobbyLobbyAlreadyExists = 9002,
	LobbySessionInProgress = 9003,
	LobbyTooManyPlayers = 9004,
	LobbyNoPermission = 9005,
	LobbyInvalidSession = 9006,
	LobbySandboxNotAllowed = 9007,
	LobbyInviteFailed = 9008,
	LobbyInviteNotFound = 9009,
	LobbyUpsertNotAllowed = 9010,
	LobbyAggregationFailed = 9011,
	LobbyHostAtCapacity = 9012,
	LobbySandboxAtCapacity = 9013,
	LobbyTooManyInvites = 9014,
	LobbyDeploymentAtCapacity = 9015,
	LobbyNotAllowed = 9016,
	LobbyMemberUpdateOnly = 9017,
	LobbyPresenceLobbyExists = 9018,
	LobbyVoiceNotEnabled = 9019,
	TitleStorageUserErrorFromDataCallback = 10000,
	TitleStorageEncryptionKeyNotSet = 10001,
	TitleStorageFileCorrupted = 10002,
	TitleStorageFileHeaderHasNewerVersion = 10003,
	ModsModSdkProcessIsAlreadyRunning = 11000,
	ModsModSdkCommandIsEmpty = 11001,
	ModsModSdkProcessCreationFailed = 11002,
	ModsCriticalError = 11003,
	ModsToolInternalError = 11004,
	ModsIPCFailure = 11005,
	ModsInvalidIPCResponse = 11006,
	ModsURILaunchFailure = 11007,
	ModsModIsNotInstalled = 11008,
	ModsUserDoesNotOwnTheGame = 11009,
	ModsOfferRequestByIdInvalidResult = 11010,
	ModsCouldNotFindOffer = 11011,
	ModsOfferRequestByIdFailure = 11012,
	ModsPurchaseFailure = 11013,
	ModsInvalidGameInstallInfo = 11014,
	ModsCannotGetManifestLocation = 11015,
	ModsUnsupportedOS = 11016,
	AntiCheatClientProtectionNotAvailable = 12000,
	AntiCheatInvalidMode = 12001,
	AntiCheatClientProductIdMismatch = 12002,
	AntiCheatClientSandboxIdMismatch = 12003,
	AntiCheatProtectMessageSessionKeyRequired = 12004,
	AntiCheatProtectMessageValidationFailed = 12005,
	AntiCheatProtectMessageInitializationFailed = 12006,
	AntiCheatPeerAlreadyRegistered = 12007,
	AntiCheatPeerNotFound = 12008,
	AntiCheatPeerNotProtected = 12009,
	AntiCheatClientDeploymentIdMismatch = 12010,
	AntiCheatDeviceIdAuthIsNotSupported = 12011,
	TooManyParticipants = 13000,
	RoomAlreadyExists = 13001,
	UserKicked = 13002,
	UserBanned = 13003,
	RoomWasLeft = 13004,
	ReconnectionTimegateExpired = 13005,
	ShutdownInvoked = 13006,
	UserIsInBlocklist = 13007,
	ProgressionSnapshotSnapshotIdUnavailable = 14000,
	ParentEmailMissing = 15000,
	UserGraduated = 15001,
	AndroidJavaVMNotStored = 17000,
	PermissionRequiredPatchAvailable = 18000,
	PermissionRequiredSystemUpdate = 18001,
	PermissionAgeRestrictionFailure = 18002,
	PermissionAccountTypeFailure = 18003,
	PermissionChatRestriction = 18004,
	PermissionUGCRestriction = 18005,
	PermissionOnlinePlayRestricted = 18006,
	DesktopCrossplayApplicationNotBootstrapped = 19000,
	DesktopCrossplayServiceNotInstalled = 19001,
	DesktopCrossplayServiceStartFailed = 19002,
	DesktopCrossplayServiceNotRunning = 19003,
	UnexpectedError = int.MaxValue
}
