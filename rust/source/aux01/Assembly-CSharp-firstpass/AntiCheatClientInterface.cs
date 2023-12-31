using System;
using Epic.OnlineServices;
using Epic.OnlineServices.AntiCheatClient;
using Epic.OnlineServices.AntiCheatCommon;

public sealed class AntiCheatClientInterface : Handle
{
	private static byte[] PollStatusStaticBuffer = new byte[256];

	public const int AddexternalintegritycatalogApiLatest = 1;

	public const int AddnotifyclientintegrityviolatedApiLatest = 1;

	public const int AddnotifymessagetopeerApiLatest = 1;

	public const int AddnotifymessagetoserverApiLatest = 1;

	public const int AddnotifypeeractionrequiredApiLatest = 1;

	public const int AddnotifypeerauthstatuschangedApiLatest = 1;

	public const int BeginsessionApiLatest = 3;

	public const int EndsessionApiLatest = 1;

	public const int GetprotectmessageoutputlengthApiLatest = 1;

	public IntPtr PeerSelf = (IntPtr)(-1);

	public const int PollstatusApiLatest = 1;

	public const int ProtectmessageApiLatest = 1;

	public const int ReceivemessagefrompeerApiLatest = 1;

	public const int ReceivemessagefromserverApiLatest = 1;

	public const int RegisterpeerApiLatest = 3;

	public const int RegisterpeerMaxAuthenticationtimeout = 120;

	public const int RegisterpeerMinAuthenticationtimeout = 40;

	public const int UnprotectmessageApiLatest = 1;

	public const int UnregisterpeerApiLatest = 1;

	public AntiCheatClientInterface ()
	{
	}

	public AntiCheatClientInterface (IntPtr innerHandle)
		: base (innerHandle)
	{
	}

	public Result AddExternalIntegrityCatalog (ref AddExternalIntegrityCatalogOptions options)
	{
		AddExternalIntegrityCatalogOptionsInternal options2 = default(AddExternalIntegrityCatalogOptionsInternal);
		options2.Set (ref options);
		Result result = Bindings.EOS_AntiCheatClient_AddExternalIntegrityCatalog (base.InnerHandle, ref options2);
		Helper.Dispose (ref options2);
		return result;
	}

	public ulong AddNotifyClientIntegrityViolated (ref AddNotifyClientIntegrityViolatedOptions options, object clientData, OnClientIntegrityViolatedCallback notificationFn)
	{
		AddNotifyClientIntegrityViolatedOptionsInternal options2 = default(AddNotifyClientIntegrityViolatedOptionsInternal);
		options2.Set (ref options);
		IntPtr clientDataAddress = IntPtr.Zero;
		OnClientIntegrityViolatedCallbackInternal onClientIntegrityViolatedCallbackInternal = OnClientIntegrityViolatedCallbackInternalImplementation;
		Helper.AddCallback (out clientDataAddress, clientData, notificationFn, onClientIntegrityViolatedCallbackInternal);
		ulong num = Bindings.EOS_AntiCheatClient_AddNotifyClientIntegrityViolated (base.InnerHandle, ref options2, clientDataAddress, onClientIntegrityViolatedCallbackInternal);
		Helper.Dispose (ref options2);
		Helper.AssignNotificationIdToCallback (clientDataAddress, num);
		return num;
	}

	public ulong AddNotifyMessageToPeer (ref AddNotifyMessageToPeerOptions options, object clientData, OnMessageToPeerCallback notificationFn)
	{
		AddNotifyMessageToPeerOptionsInternal options2 = default(AddNotifyMessageToPeerOptionsInternal);
		options2.Set (ref options);
		IntPtr clientDataAddress = IntPtr.Zero;
		OnMessageToPeerCallbackInternal onMessageToPeerCallbackInternal = OnMessageToPeerCallbackInternalImplementation;
		Helper.AddCallback (out clientDataAddress, clientData, notificationFn, onMessageToPeerCallbackInternal);
		ulong num = Bindings.EOS_AntiCheatClient_AddNotifyMessageToPeer (base.InnerHandle, ref options2, clientDataAddress, onMessageToPeerCallbackInternal);
		Helper.Dispose (ref options2);
		Helper.AssignNotificationIdToCallback (clientDataAddress, num);
		return num;
	}

	public ulong AddNotifyMessageToServer (ref AddNotifyMessageToServerOptions options, object clientData, OnMessageToServerCallback notificationFn)
	{
		AddNotifyMessageToServerOptionsInternal options2 = default(AddNotifyMessageToServerOptionsInternal);
		options2.Set (ref options);
		IntPtr clientDataAddress = IntPtr.Zero;
		OnMessageToServerCallbackInternal onMessageToServerCallbackInternal = OnMessageToServerCallbackInternalImplementation;
		Helper.AddCallback (out clientDataAddress, clientData, notificationFn, onMessageToServerCallbackInternal);
		ulong num = Bindings.EOS_AntiCheatClient_AddNotifyMessageToServer (base.InnerHandle, ref options2, clientDataAddress, onMessageToServerCallbackInternal);
		Helper.Dispose (ref options2);
		Helper.AssignNotificationIdToCallback (clientDataAddress, num);
		return num;
	}

	public ulong AddNotifyPeerActionRequired (ref AddNotifyPeerActionRequiredOptions options, object clientData, OnPeerActionRequiredCallback notificationFn)
	{
		AddNotifyPeerActionRequiredOptionsInternal options2 = default(AddNotifyPeerActionRequiredOptionsInternal);
		options2.Set (ref options);
		IntPtr clientDataAddress = IntPtr.Zero;
		OnPeerActionRequiredCallbackInternal onPeerActionRequiredCallbackInternal = OnPeerActionRequiredCallbackInternalImplementation;
		Helper.AddCallback (out clientDataAddress, clientData, notificationFn, onPeerActionRequiredCallbackInternal);
		ulong num = Bindings.EOS_AntiCheatClient_AddNotifyPeerActionRequired (base.InnerHandle, ref options2, clientDataAddress, onPeerActionRequiredCallbackInternal);
		Helper.Dispose (ref options2);
		Helper.AssignNotificationIdToCallback (clientDataAddress, num);
		return num;
	}

	public ulong AddNotifyPeerAuthStatusChanged (ref AddNotifyPeerAuthStatusChangedOptions options, object clientData, OnPeerAuthStatusChangedCallback notificationFn)
	{
		AddNotifyPeerAuthStatusChangedOptionsInternal options2 = default(AddNotifyPeerAuthStatusChangedOptionsInternal);
		options2.Set (ref options);
		IntPtr clientDataAddress = IntPtr.Zero;
		OnPeerAuthStatusChangedCallbackInternal onPeerAuthStatusChangedCallbackInternal = OnPeerAuthStatusChangedCallbackInternalImplementation;
		Helper.AddCallback (out clientDataAddress, clientData, notificationFn, onPeerAuthStatusChangedCallbackInternal);
		ulong num = Bindings.EOS_AntiCheatClient_AddNotifyPeerAuthStatusChanged (base.InnerHandle, ref options2, clientDataAddress, onPeerAuthStatusChangedCallbackInternal);
		Helper.Dispose (ref options2);
		Helper.AssignNotificationIdToCallback (clientDataAddress, num);
		return num;
	}

	public Result BeginSession (ref BeginSessionOptions options)
	{
		BeginSessionOptionsInternal options2 = default(BeginSessionOptionsInternal);
		options2.Set (ref options);
		Result result = Bindings.EOS_AntiCheatClient_BeginSession (base.InnerHandle, ref options2);
		Helper.Dispose (ref options2);
		return result;
	}

	public Result EndSession (ref EndSessionOptions options)
	{
		EndSessionOptionsInternal options2 = default(EndSessionOptionsInternal);
		options2.Set (ref options);
		Result result = Bindings.EOS_AntiCheatClient_EndSession (base.InnerHandle, ref options2);
		Helper.Dispose (ref options2);
		return result;
	}

	public Result GetProtectMessageOutputLength (ref GetProtectMessageOutputLengthOptions options, out uint outBufferSizeBytes)
	{
		GetProtectMessageOutputLengthOptionsInternal options2 = default(GetProtectMessageOutputLengthOptionsInternal);
		options2.Set (ref options);
		outBufferSizeBytes = Helper.GetDefault<uint> ();
		Result result = Bindings.EOS_AntiCheatClient_GetProtectMessageOutputLength (base.InnerHandle, ref options2, ref outBufferSizeBytes);
		Helper.Dispose (ref options2);
		return result;
	}

	public unsafe Result PollStatus (ref PollStatusOptions options, out AntiCheatClientViolationType outViolationType, out Utf8String outMessage)
	{
		outMessage = Utf8String.EmptyString;
		PollStatusOptionsInternal disposable = default(PollStatusOptionsInternal);
		disposable.Set (ref options);
		outViolationType = Helper.GetDefault<AntiCheatClientViolationType> ();
		_ = options.OutMessageLength;
		Result num;
		fixed (byte* value = PollStatusStaticBuffer) {
			num = Bindings.EOS_AntiCheatClient_PollStatus (outMessage: new IntPtr (value), handle: base.InnerHandle, options: ref disposable, outViolationType: ref outViolationType);
			if (num == Result.Success) {
				outMessage = new Utf8String (PollStatusStaticBuffer);
			}
		}
		Helper.Dispose (ref disposable);
		return num;
	}

	public unsafe Result ProtectMessage (ref ProtectMessageOptions options, ArraySegment<byte> outBuffer, out uint outBytesWritten)
	{
		Result result;
		fixed (byte* ptr = options.Data.Array) {
			IntPtr data = IntPtr.Add ((IntPtr)ptr, options.Data.Offset);
			ProtectMessageOptionsInternal options2 = default(ProtectMessageOptionsInternal);
			options2.m_ApiVersion = 1;
			options2.m_Data = data;
			options2.m_DataLengthBytes = (uint)options.Data.Count;
			options2.m_OutBufferSizeBytes = options.OutBufferSizeBytes;
			outBytesWritten = 0u;
			fixed (byte* ptr2 = outBuffer.Array) {
				IntPtr outBuffer2 = IntPtr.Add ((IntPtr)ptr2, outBuffer.Offset);
				result = Bindings.EOS_AntiCheatClient_ProtectMessage (base.InnerHandle, ref options2, outBuffer2, ref outBytesWritten);
			}
		}
		return result;
	}

	public Result ReceiveMessageFromPeer (ref ReceiveMessageFromPeerOptions options)
	{
		ReceiveMessageFromPeerOptionsInternal options2 = default(ReceiveMessageFromPeerOptionsInternal);
		options2.Set (ref options);
		Result result = Bindings.EOS_AntiCheatClient_ReceiveMessageFromPeer (base.InnerHandle, ref options2);
		Helper.Dispose (ref options2);
		return result;
	}

	public Result ReceiveMessageFromServer (ref ReceiveMessageFromServerOptions options)
	{
		ReceiveMessageFromServerOptionsInternal options2 = default(ReceiveMessageFromServerOptionsInternal);
		options2.Set (ref options);
		Result result = Bindings.EOS_AntiCheatClient_ReceiveMessageFromServer (base.InnerHandle, ref options2);
		Helper.Dispose (ref options2);
		return result;
	}

	public Result RegisterPeer (ref RegisterPeerOptions options)
	{
		RegisterPeerOptionsInternal options2 = default(RegisterPeerOptionsInternal);
		options2.Set (ref options);
		Result result = Bindings.EOS_AntiCheatClient_RegisterPeer (base.InnerHandle, ref options2);
		Helper.Dispose (ref options2);
		return result;
	}

	public void RemoveNotifyClientIntegrityViolated (ulong notificationId)
	{
		Bindings.EOS_AntiCheatClient_RemoveNotifyClientIntegrityViolated (base.InnerHandle, notificationId);
		Helper.RemoveCallbackByNotificationId (notificationId);
	}

	public void RemoveNotifyMessageToPeer (ulong notificationId)
	{
		Bindings.EOS_AntiCheatClient_RemoveNotifyMessageToPeer (base.InnerHandle, notificationId);
		Helper.RemoveCallbackByNotificationId (notificationId);
	}

	public void RemoveNotifyMessageToServer (ulong notificationId)
	{
		Bindings.EOS_AntiCheatClient_RemoveNotifyMessageToServer (base.InnerHandle, notificationId);
		Helper.RemoveCallbackByNotificationId (notificationId);
	}

	public void RemoveNotifyPeerActionRequired (ulong notificationId)
	{
		Bindings.EOS_AntiCheatClient_RemoveNotifyPeerActionRequired (base.InnerHandle, notificationId);
		Helper.RemoveCallbackByNotificationId (notificationId);
	}

	public void RemoveNotifyPeerAuthStatusChanged (ulong notificationId)
	{
		Bindings.EOS_AntiCheatClient_RemoveNotifyPeerAuthStatusChanged (base.InnerHandle, notificationId);
		Helper.RemoveCallbackByNotificationId (notificationId);
	}

	public unsafe Result UnprotectMessage (ref UnprotectMessageOptions options, ArraySegment<byte> outBuffer, out uint outBytesWritten)
	{
		Result result;
		fixed (byte* ptr = options.Data.Array) {
			IntPtr data = IntPtr.Add ((IntPtr)ptr, options.Data.Offset);
			UnprotectMessageOptionsInternal options2 = default(UnprotectMessageOptionsInternal);
			options2.m_ApiVersion = 1;
			options2.m_Data = data;
			options2.m_DataLengthBytes = (uint)options.Data.Count;
			options2.m_OutBufferSizeBytes = options.OutBufferSizeBytes;
			outBytesWritten = 0u;
			fixed (byte* ptr2 = outBuffer.Array) {
				IntPtr outBuffer2 = IntPtr.Add ((IntPtr)ptr2, outBuffer.Offset);
				result = Bindings.EOS_AntiCheatClient_UnprotectMessage (base.InnerHandle, ref options2, outBuffer2, ref outBytesWritten);
			}
		}
		return result;
	}

	public Result UnregisterPeer (ref UnregisterPeerOptions options)
	{
		UnregisterPeerOptionsInternal options2 = default(UnregisterPeerOptionsInternal);
		options2.Set (ref options);
		Result result = Bindings.EOS_AntiCheatClient_UnregisterPeer (base.InnerHandle, ref options2);
		Helper.Dispose (ref options2);
		return result;
	}

	[MonoPInvokeCallback (typeof(OnClientIntegrityViolatedCallbackInternal))]
	internal static void OnClientIntegrityViolatedCallbackInternalImplementation (ref OnClientIntegrityViolatedCallbackInfoInternal data)
	{
		if (Helper.TryGetAndRemoveCallback<OnClientIntegrityViolatedCallbackInfoInternal, OnClientIntegrityViolatedCallback, OnClientIntegrityViolatedCallbackInfo> (ref data, out var callback, out var callbackInfo)) {
			callback (ref callbackInfo);
		}
	}

	[MonoPInvokeCallback (typeof(OnMessageToPeerCallbackInternal))]
	internal static void OnMessageToPeerCallbackInternalImplementation (ref OnMessageToClientCallbackInfoInternal data)
	{
		if (Helper.TryGetAndRemoveCallback<OnMessageToClientCallbackInfoInternal, OnMessageToPeerCallback, OnMessageToClientCallbackInfo> (ref data, out var callback, out var callbackInfo)) {
			callback (ref callbackInfo);
		}
	}

	[MonoPInvokeCallback (typeof(OnMessageToServerCallbackInternal))]
	internal static void OnMessageToServerCallbackInternalImplementation (ref OnMessageToServerCallbackInfoInternal data)
	{
		if (Helper.TryGetAndRemoveCallback<OnMessageToServerCallbackInfoInternal, OnMessageToServerCallback, OnMessageToServerCallbackInfo> (ref data, out var callback, out var callbackInfo)) {
			callback (ref callbackInfo);
		}
	}

	[MonoPInvokeCallback (typeof(OnPeerActionRequiredCallbackInternal))]
	internal static void OnPeerActionRequiredCallbackInternalImplementation (ref OnClientActionRequiredCallbackInfoInternal data)
	{
		if (Helper.TryGetAndRemoveCallback<OnClientActionRequiredCallbackInfoInternal, OnPeerActionRequiredCallback, OnClientActionRequiredCallbackInfo> (ref data, out var callback, out var callbackInfo)) {
			callback (ref callbackInfo);
		}
	}

	[MonoPInvokeCallback (typeof(OnPeerAuthStatusChangedCallbackInternal))]
	internal static void OnPeerAuthStatusChangedCallbackInternalImplementation (ref OnClientAuthStatusChangedCallbackInfoInternal data)
	{
		if (Helper.TryGetAndRemoveCallback<OnClientAuthStatusChangedCallbackInfoInternal, OnPeerAuthStatusChangedCallback, OnClientAuthStatusChangedCallbackInfo> (ref data, out var callback, out var callbackInfo)) {
			callback (ref callbackInfo);
		}
	}
}
