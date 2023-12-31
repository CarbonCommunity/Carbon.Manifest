using System;
using Epic.OnlineServices;
using Epic.OnlineServices.AntiCheatCommon;
using Epic.OnlineServices.AntiCheatServer;

public sealed class AntiCheatServerInterface : Handle
{
	public const int AddnotifyclientactionrequiredApiLatest = 1;

	public const int AddnotifyclientauthstatuschangedApiLatest = 1;

	public const int AddnotifymessagetoclientApiLatest = 1;

	public const int BeginsessionApiLatest = 3;

	public const int BeginsessionMaxRegistertimeout = 120;

	public const int BeginsessionMinRegistertimeout = 10;

	public const int EndsessionApiLatest = 1;

	public const int GetprotectmessageoutputlengthApiLatest = 1;

	public const int ProtectmessageApiLatest = 1;

	public const int ReceivemessagefromclientApiLatest = 1;

	public const int RegisterclientApiLatest = 2;

	public const int SetclientnetworkstateApiLatest = 1;

	public const int UnprotectmessageApiLatest = 1;

	public const int UnregisterclientApiLatest = 1;

	public AntiCheatServerInterface ()
	{
	}

	public AntiCheatServerInterface (IntPtr innerHandle)
		: base (innerHandle)
	{
	}

	public ulong AddNotifyClientActionRequired (ref AddNotifyClientActionRequiredOptions options, object clientData, OnClientActionRequiredCallback notificationFn)
	{
		AddNotifyClientActionRequiredOptionsInternal options2 = default(AddNotifyClientActionRequiredOptionsInternal);
		options2.Set (ref options);
		IntPtr clientDataAddress = IntPtr.Zero;
		OnClientActionRequiredCallbackInternal onClientActionRequiredCallbackInternal = OnClientActionRequiredCallbackInternalImplementation;
		Helper.AddCallback (out clientDataAddress, clientData, notificationFn, onClientActionRequiredCallbackInternal);
		ulong num = Bindings.EOS_AntiCheatServer_AddNotifyClientActionRequired (base.InnerHandle, ref options2, clientDataAddress, onClientActionRequiredCallbackInternal);
		Helper.Dispose (ref options2);
		Helper.AssignNotificationIdToCallback (clientDataAddress, num);
		return num;
	}

	public ulong AddNotifyClientAuthStatusChanged (ref AddNotifyClientAuthStatusChangedOptions options, object clientData, OnClientAuthStatusChangedCallback notificationFn)
	{
		AddNotifyClientAuthStatusChangedOptionsInternal options2 = default(AddNotifyClientAuthStatusChangedOptionsInternal);
		options2.Set (ref options);
		IntPtr clientDataAddress = IntPtr.Zero;
		OnClientAuthStatusChangedCallbackInternal onClientAuthStatusChangedCallbackInternal = OnClientAuthStatusChangedCallbackInternalImplementation;
		Helper.AddCallback (out clientDataAddress, clientData, notificationFn, onClientAuthStatusChangedCallbackInternal);
		ulong num = Bindings.EOS_AntiCheatServer_AddNotifyClientAuthStatusChanged (base.InnerHandle, ref options2, clientDataAddress, onClientAuthStatusChangedCallbackInternal);
		Helper.Dispose (ref options2);
		Helper.AssignNotificationIdToCallback (clientDataAddress, num);
		return num;
	}

	public ulong AddNotifyMessageToClient (ref AddNotifyMessageToClientOptions options, object clientData, OnMessageToClientCallback notificationFn)
	{
		AddNotifyMessageToClientOptionsInternal options2 = default(AddNotifyMessageToClientOptionsInternal);
		options2.Set (ref options);
		IntPtr clientDataAddress = IntPtr.Zero;
		OnMessageToClientCallbackInternal onMessageToClientCallbackInternal = OnMessageToClientCallbackInternalImplementation;
		Helper.AddCallback (out clientDataAddress, clientData, notificationFn, onMessageToClientCallbackInternal);
		ulong num = Bindings.EOS_AntiCheatServer_AddNotifyMessageToClient (base.InnerHandle, ref options2, clientDataAddress, onMessageToClientCallbackInternal);
		Helper.Dispose (ref options2);
		Helper.AssignNotificationIdToCallback (clientDataAddress, num);
		return num;
	}

	public Result BeginSession (ref BeginSessionOptions options)
	{
		BeginSessionOptionsInternal options2 = default(BeginSessionOptionsInternal);
		options2.Set (ref options);
		Result result = Bindings.EOS_AntiCheatServer_BeginSession (base.InnerHandle, ref options2);
		Helper.Dispose (ref options2);
		return result;
	}

	public Result EndSession (ref EndSessionOptions options)
	{
		EndSessionOptionsInternal options2 = default(EndSessionOptionsInternal);
		options2.Set (ref options);
		Result result = Bindings.EOS_AntiCheatServer_EndSession (base.InnerHandle, ref options2);
		Helper.Dispose (ref options2);
		return result;
	}

	public Result GetProtectMessageOutputLength (ref GetProtectMessageOutputLengthOptions options, out uint outBufferSizeBytes)
	{
		GetProtectMessageOutputLengthOptionsInternal options2 = default(GetProtectMessageOutputLengthOptionsInternal);
		options2.Set (ref options);
		outBufferSizeBytes = Helper.GetDefault<uint> ();
		Result result = Bindings.EOS_AntiCheatServer_GetProtectMessageOutputLength (base.InnerHandle, ref options2, ref outBufferSizeBytes);
		Helper.Dispose (ref options2);
		return result;
	}

	public Result LogEvent (ref LogEventOptions options)
	{
		LogEventOptionsInternal options2 = default(LogEventOptionsInternal);
		options2.Set (ref options);
		Result result = Bindings.EOS_AntiCheatServer_LogEvent (base.InnerHandle, ref options2);
		Helper.Dispose (ref options2);
		return result;
	}

	public Result LogGameRoundEnd (ref LogGameRoundEndOptions options)
	{
		LogGameRoundEndOptionsInternal options2 = default(LogGameRoundEndOptionsInternal);
		options2.Set (ref options);
		Result result = Bindings.EOS_AntiCheatServer_LogGameRoundEnd (base.InnerHandle, ref options2);
		Helper.Dispose (ref options2);
		return result;
	}

	public Result LogGameRoundStart (ref LogGameRoundStartOptions options)
	{
		LogGameRoundStartOptionsInternal options2 = default(LogGameRoundStartOptionsInternal);
		options2.Set (ref options);
		Result result = Bindings.EOS_AntiCheatServer_LogGameRoundStart (base.InnerHandle, ref options2);
		Helper.Dispose (ref options2);
		return result;
	}

	public Result LogPlayerDespawn (ref LogPlayerDespawnOptions options)
	{
		LogPlayerDespawnOptionsInternal options2 = default(LogPlayerDespawnOptionsInternal);
		options2.Set (ref options);
		Result result = Bindings.EOS_AntiCheatServer_LogPlayerDespawn (base.InnerHandle, ref options2);
		Helper.Dispose (ref options2);
		return result;
	}

	public Result LogPlayerRevive (ref LogPlayerReviveOptions options)
	{
		LogPlayerReviveOptionsInternal options2 = default(LogPlayerReviveOptionsInternal);
		options2.Set (ref options);
		Result result = Bindings.EOS_AntiCheatServer_LogPlayerRevive (base.InnerHandle, ref options2);
		Helper.Dispose (ref options2);
		return result;
	}

	public Result LogPlayerSpawn (ref LogPlayerSpawnOptions options)
	{
		LogPlayerSpawnOptionsInternal options2 = default(LogPlayerSpawnOptionsInternal);
		options2.Set (ref options);
		Result result = Bindings.EOS_AntiCheatServer_LogPlayerSpawn (base.InnerHandle, ref options2);
		Helper.Dispose (ref options2);
		return result;
	}

	public Result LogPlayerTakeDamage (ref LogPlayerTakeDamageOptions options)
	{
		LogPlayerTakeDamageOptionsInternal options2 = default(LogPlayerTakeDamageOptionsInternal);
		options2.Set (ref options);
		Result result = Bindings.EOS_AntiCheatServer_LogPlayerTakeDamage (base.InnerHandle, ref options2);
		Helper.Dispose (ref options2);
		return result;
	}

	public unsafe Result LogPlayerTick (ref LogPlayerTickOptions options)
	{
		LogPlayerTickOptionsInternal options2 = default(LogPlayerTickOptionsInternal);
		options2.m_ApiVersion = 2;
		options2.m_PlayerHandle = options.PlayerHandle;
		Helper.Set (options.IsPlayerViewZoomed, ref options2.m_IsPlayerViewZoomed);
		options2.m_PlayerHealth = options.PlayerHealth;
		options2.m_PlayerMovementState = options.PlayerMovementState;
		if (options.PlayerPosition.HasValue) {
			Vec3f value = options.PlayerPosition.Value;
			options2.m_PlayerPosition = new IntPtr (&value);
		}
		if (options.PlayerViewRotation.HasValue) {
			Quat value2 = options.PlayerViewRotation.Value;
			options2.m_PlayerViewRotation = new IntPtr (&value2);
		}
		return Bindings.EOS_AntiCheatServer_LogPlayerTick (base.InnerHandle, ref options2);
	}

	public Result LogPlayerUseAbility (ref LogPlayerUseAbilityOptions options)
	{
		LogPlayerUseAbilityOptionsInternal options2 = default(LogPlayerUseAbilityOptionsInternal);
		options2.Set (ref options);
		Result result = Bindings.EOS_AntiCheatServer_LogPlayerUseAbility (base.InnerHandle, ref options2);
		Helper.Dispose (ref options2);
		return result;
	}

	public Result LogPlayerUseWeapon (ref LogPlayerUseWeaponOptions options)
	{
		LogPlayerUseWeaponOptionsInternal options2 = default(LogPlayerUseWeaponOptionsInternal);
		options2.Set (ref options);
		Result result = Bindings.EOS_AntiCheatServer_LogPlayerUseWeapon (base.InnerHandle, ref options2);
		Helper.Dispose (ref options2);
		return result;
	}

	public unsafe Result ProtectMessage (ref ProtectMessageOptions options, ArraySegment<byte> outBuffer, out uint outBytesWritten)
	{
		Result result;
		fixed (byte* ptr = options.Data.Array) {
			IntPtr data = IntPtr.Add ((IntPtr)ptr, options.Data.Offset);
			ProtectMessageOptionsInternal options2 = default(ProtectMessageOptionsInternal);
			options2.m_ApiVersion = 1;
			options2.m_ClientHandle = options.ClientHandle;
			options2.m_Data = data;
			options2.m_DataLengthBytes = (uint)options.Data.Count;
			options2.m_OutBufferSizeBytes = options.OutBufferSizeBytes;
			outBytesWritten = 0u;
			fixed (byte* ptr2 = outBuffer.Array) {
				IntPtr outBuffer2 = IntPtr.Add ((IntPtr)ptr2, outBuffer.Offset);
				result = Bindings.EOS_AntiCheatServer_ProtectMessage (base.InnerHandle, ref options2, outBuffer2, ref outBytesWritten);
			}
		}
		return result;
	}

	public Result ReceiveMessageFromClient (ref ReceiveMessageFromClientOptions options)
	{
		ReceiveMessageFromClientOptionsInternal options2 = default(ReceiveMessageFromClientOptionsInternal);
		options2.Set (ref options);
		Result result = Bindings.EOS_AntiCheatServer_ReceiveMessageFromClient (base.InnerHandle, ref options2);
		Helper.Dispose (ref options2);
		return result;
	}

	public Result RegisterClient (ref RegisterClientOptions options)
	{
		RegisterClientOptionsInternal options2 = default(RegisterClientOptionsInternal);
		options2.Set (ref options);
		Result result = Bindings.EOS_AntiCheatServer_RegisterClient (base.InnerHandle, ref options2);
		Helper.Dispose (ref options2);
		return result;
	}

	public Result RegisterEvent (ref RegisterEventOptions options)
	{
		RegisterEventOptionsInternal options2 = default(RegisterEventOptionsInternal);
		options2.Set (ref options);
		Result result = Bindings.EOS_AntiCheatServer_RegisterEvent (base.InnerHandle, ref options2);
		Helper.Dispose (ref options2);
		return result;
	}

	public void RemoveNotifyClientActionRequired (ulong notificationId)
	{
		Bindings.EOS_AntiCheatServer_RemoveNotifyClientActionRequired (base.InnerHandle, notificationId);
		Helper.RemoveCallbackByNotificationId (notificationId);
	}

	public void RemoveNotifyClientAuthStatusChanged (ulong notificationId)
	{
		Bindings.EOS_AntiCheatServer_RemoveNotifyClientAuthStatusChanged (base.InnerHandle, notificationId);
		Helper.RemoveCallbackByNotificationId (notificationId);
	}

	public void RemoveNotifyMessageToClient (ulong notificationId)
	{
		Bindings.EOS_AntiCheatServer_RemoveNotifyMessageToClient (base.InnerHandle, notificationId);
		Helper.RemoveCallbackByNotificationId (notificationId);
	}

	public Result SetClientDetails (ref SetClientDetailsOptions options)
	{
		SetClientDetailsOptionsInternal options2 = default(SetClientDetailsOptionsInternal);
		options2.Set (ref options);
		Result result = Bindings.EOS_AntiCheatServer_SetClientDetails (base.InnerHandle, ref options2);
		Helper.Dispose (ref options2);
		return result;
	}

	public Result SetClientNetworkState (ref SetClientNetworkStateOptions options)
	{
		SetClientNetworkStateOptionsInternal options2 = default(SetClientNetworkStateOptionsInternal);
		options2.Set (ref options);
		Result result = Bindings.EOS_AntiCheatServer_SetClientNetworkState (base.InnerHandle, ref options2);
		Helper.Dispose (ref options2);
		return result;
	}

	public Result SetGameSessionId (ref SetGameSessionIdOptions options)
	{
		SetGameSessionIdOptionsInternal options2 = default(SetGameSessionIdOptionsInternal);
		options2.Set (ref options);
		Result result = Bindings.EOS_AntiCheatServer_SetGameSessionId (base.InnerHandle, ref options2);
		Helper.Dispose (ref options2);
		return result;
	}

	public unsafe Result UnprotectMessage (ref UnprotectMessageOptions options, ArraySegment<byte> outBuffer, out uint outBytesWritten)
	{
		Result result;
		fixed (byte* ptr = options.Data.Array) {
			IntPtr data = IntPtr.Add ((IntPtr)ptr, options.Data.Offset);
			UnprotectMessageOptionsInternal options2 = default(UnprotectMessageOptionsInternal);
			options2.m_ApiVersion = 1;
			options2.m_ClientHandle = options.ClientHandle;
			options2.m_Data = data;
			options2.m_DataLengthBytes = (uint)options.Data.Count;
			options2.m_OutBufferSizeBytes = options.OutBufferSizeBytes;
			outBytesWritten = 0u;
			fixed (byte* ptr2 = outBuffer.Array) {
				IntPtr outBuffer2 = IntPtr.Add ((IntPtr)ptr2, outBuffer.Offset);
				result = Bindings.EOS_AntiCheatServer_UnprotectMessage (base.InnerHandle, ref options2, outBuffer2, ref outBytesWritten);
			}
		}
		return result;
	}

	public Result UnregisterClient (ref UnregisterClientOptions options)
	{
		UnregisterClientOptionsInternal options2 = default(UnregisterClientOptionsInternal);
		options2.Set (ref options);
		Result result = Bindings.EOS_AntiCheatServer_UnregisterClient (base.InnerHandle, ref options2);
		Helper.Dispose (ref options2);
		return result;
	}

	[MonoPInvokeCallback (typeof(OnClientActionRequiredCallbackInternal))]
	internal static void OnClientActionRequiredCallbackInternalImplementation (ref OnClientActionRequiredCallbackInfoInternal data)
	{
		if (Helper.TryGetCallback<OnClientActionRequiredCallbackInfoInternal, OnClientActionRequiredCallback, OnClientActionRequiredCallbackInfo> (ref data, out var callback, out var callbackInfo)) {
			callback (ref callbackInfo);
		}
	}

	[MonoPInvokeCallback (typeof(OnClientAuthStatusChangedCallbackInternal))]
	internal static void OnClientAuthStatusChangedCallbackInternalImplementation (ref OnClientAuthStatusChangedCallbackInfoInternal data)
	{
		if (Helper.TryGetCallback<OnClientAuthStatusChangedCallbackInfoInternal, OnClientAuthStatusChangedCallback, OnClientAuthStatusChangedCallbackInfo> (ref data, out var callback, out var callbackInfo)) {
			callback (ref callbackInfo);
		}
	}

	[MonoPInvokeCallback (typeof(OnMessageToClientCallbackInternal))]
	internal static void OnMessageToClientCallbackInternalImplementation (ref OnMessageToClientCallbackInfoInternal data)
	{
		if (Helper.TryGetCallback<OnMessageToClientCallbackInfoInternal, OnMessageToClientCallback, OnMessageToClientCallbackInfo> (ref data, out var callback, out var callbackInfo)) {
			callback (ref callbackInfo);
		}
	}
}
