using System;
using Epic.OnlineServices;
using Epic.OnlineServices.P2P;

public sealed class P2PInterface : Handle
{
	public const int AcceptconnectionApiLatest = 1;

	public const int AddnotifyincomingpacketqueuefullApiLatest = 1;

	public const int AddnotifypeerconnectionclosedApiLatest = 1;

	public const int AddnotifypeerconnectionestablishedApiLatest = 1;

	public const int AddnotifypeerconnectioninterruptedApiLatest = 1;

	public const int AddnotifypeerconnectionrequestApiLatest = 1;

	public const int ClearpacketqueueApiLatest = 1;

	public const int CloseconnectionApiLatest = 1;

	public const int CloseconnectionsApiLatest = 1;

	public const int GetnattypeApiLatest = 1;

	public const int GetnextreceivedpacketsizeApiLatest = 2;

	public const int GetpacketqueueinfoApiLatest = 1;

	public const int GetportrangeApiLatest = 1;

	public const int GetrelaycontrolApiLatest = 1;

	public const int MaxConnections = 32;

	public const int MaxPacketSize = 1170;

	public const int MaxQueueSizeUnlimited = 0;

	public const int QuerynattypeApiLatest = 1;

	public const int ReceivepacketApiLatest = 2;

	public const int SendpacketApiLatest = 3;

	public const int SetpacketqueuesizeApiLatest = 1;

	public const int SetportrangeApiLatest = 1;

	public const int SetrelaycontrolApiLatest = 1;

	public const int SocketidApiLatest = 1;

	public const int SocketidSocketnameSize = 33;

	public P2PInterface ()
	{
	}

	public P2PInterface (IntPtr innerHandle)
		: base (innerHandle)
	{
	}

	public Result AcceptConnection (ref AcceptConnectionOptions options)
	{
		AcceptConnectionOptionsInternal options2 = default(AcceptConnectionOptionsInternal);
		options2.Set (ref options);
		Result result = Bindings.EOS_P2P_AcceptConnection (base.InnerHandle, ref options2);
		Helper.Dispose (ref options2);
		return result;
	}

	public ulong AddNotifyIncomingPacketQueueFull (ref AddNotifyIncomingPacketQueueFullOptions options, object clientData, OnIncomingPacketQueueFullCallback incomingPacketQueueFullHandler)
	{
		AddNotifyIncomingPacketQueueFullOptionsInternal options2 = default(AddNotifyIncomingPacketQueueFullOptionsInternal);
		options2.Set (ref options);
		IntPtr clientDataAddress = IntPtr.Zero;
		OnIncomingPacketQueueFullCallbackInternal onIncomingPacketQueueFullCallbackInternal = OnIncomingPacketQueueFullCallbackInternalImplementation;
		Helper.AddCallback (out clientDataAddress, clientData, incomingPacketQueueFullHandler, onIncomingPacketQueueFullCallbackInternal);
		ulong num = Bindings.EOS_P2P_AddNotifyIncomingPacketQueueFull (base.InnerHandle, ref options2, clientDataAddress, onIncomingPacketQueueFullCallbackInternal);
		Helper.Dispose (ref options2);
		Helper.AssignNotificationIdToCallback (clientDataAddress, num);
		return num;
	}

	public ulong AddNotifyPeerConnectionClosed (ref AddNotifyPeerConnectionClosedOptions options, object clientData, OnRemoteConnectionClosedCallback connectionClosedHandler)
	{
		AddNotifyPeerConnectionClosedOptionsInternal options2 = default(AddNotifyPeerConnectionClosedOptionsInternal);
		options2.Set (ref options);
		IntPtr clientDataAddress = IntPtr.Zero;
		OnRemoteConnectionClosedCallbackInternal onRemoteConnectionClosedCallbackInternal = OnRemoteConnectionClosedCallbackInternalImplementation;
		Helper.AddCallback (out clientDataAddress, clientData, connectionClosedHandler, onRemoteConnectionClosedCallbackInternal);
		ulong num = Bindings.EOS_P2P_AddNotifyPeerConnectionClosed (base.InnerHandle, ref options2, clientDataAddress, onRemoteConnectionClosedCallbackInternal);
		Helper.Dispose (ref options2);
		Helper.AssignNotificationIdToCallback (clientDataAddress, num);
		return num;
	}

	public ulong AddNotifyPeerConnectionEstablished (ref AddNotifyPeerConnectionEstablishedOptions options, object clientData, OnPeerConnectionEstablishedCallback connectionEstablishedHandler)
	{
		AddNotifyPeerConnectionEstablishedOptionsInternal options2 = default(AddNotifyPeerConnectionEstablishedOptionsInternal);
		options2.Set (ref options);
		IntPtr clientDataAddress = IntPtr.Zero;
		OnPeerConnectionEstablishedCallbackInternal onPeerConnectionEstablishedCallbackInternal = OnPeerConnectionEstablishedCallbackInternalImplementation;
		Helper.AddCallback (out clientDataAddress, clientData, connectionEstablishedHandler, onPeerConnectionEstablishedCallbackInternal);
		ulong num = Bindings.EOS_P2P_AddNotifyPeerConnectionEstablished (base.InnerHandle, ref options2, clientDataAddress, onPeerConnectionEstablishedCallbackInternal);
		Helper.Dispose (ref options2);
		Helper.AssignNotificationIdToCallback (clientDataAddress, num);
		return num;
	}

	public ulong AddNotifyPeerConnectionInterrupted (ref AddNotifyPeerConnectionInterruptedOptions options, object clientData, OnPeerConnectionInterruptedCallback connectionInterruptedHandler)
	{
		AddNotifyPeerConnectionInterruptedOptionsInternal options2 = default(AddNotifyPeerConnectionInterruptedOptionsInternal);
		options2.Set (ref options);
		IntPtr clientDataAddress = IntPtr.Zero;
		OnPeerConnectionInterruptedCallbackInternal onPeerConnectionInterruptedCallbackInternal = OnPeerConnectionInterruptedCallbackInternalImplementation;
		Helper.AddCallback (out clientDataAddress, clientData, connectionInterruptedHandler, onPeerConnectionInterruptedCallbackInternal);
		ulong num = Bindings.EOS_P2P_AddNotifyPeerConnectionInterrupted (base.InnerHandle, ref options2, clientDataAddress, onPeerConnectionInterruptedCallbackInternal);
		Helper.Dispose (ref options2);
		Helper.AssignNotificationIdToCallback (clientDataAddress, num);
		return num;
	}

	public ulong AddNotifyPeerConnectionRequest (ref AddNotifyPeerConnectionRequestOptions options, object clientData, OnIncomingConnectionRequestCallback connectionRequestHandler)
	{
		AddNotifyPeerConnectionRequestOptionsInternal options2 = default(AddNotifyPeerConnectionRequestOptionsInternal);
		options2.Set (ref options);
		IntPtr clientDataAddress = IntPtr.Zero;
		OnIncomingConnectionRequestCallbackInternal onIncomingConnectionRequestCallbackInternal = OnIncomingConnectionRequestCallbackInternalImplementation;
		Helper.AddCallback (out clientDataAddress, clientData, connectionRequestHandler, onIncomingConnectionRequestCallbackInternal);
		ulong num = Bindings.EOS_P2P_AddNotifyPeerConnectionRequest (base.InnerHandle, ref options2, clientDataAddress, onIncomingConnectionRequestCallbackInternal);
		Helper.Dispose (ref options2);
		Helper.AssignNotificationIdToCallback (clientDataAddress, num);
		return num;
	}

	public Result ClearPacketQueue (ref ClearPacketQueueOptions options)
	{
		ClearPacketQueueOptionsInternal options2 = default(ClearPacketQueueOptionsInternal);
		options2.Set (ref options);
		Result result = Bindings.EOS_P2P_ClearPacketQueue (base.InnerHandle, ref options2);
		Helper.Dispose (ref options2);
		return result;
	}

	public Result CloseConnection (ref CloseConnectionOptions options)
	{
		CloseConnectionOptionsInternal options2 = default(CloseConnectionOptionsInternal);
		options2.Set (ref options);
		Result result = Bindings.EOS_P2P_CloseConnection (base.InnerHandle, ref options2);
		Helper.Dispose (ref options2);
		return result;
	}

	public Result CloseConnections (ref CloseConnectionsOptions options)
	{
		CloseConnectionsOptionsInternal options2 = default(CloseConnectionsOptionsInternal);
		options2.Set (ref options);
		Result result = Bindings.EOS_P2P_CloseConnections (base.InnerHandle, ref options2);
		Helper.Dispose (ref options2);
		return result;
	}

	public Result GetNATType (ref GetNATTypeOptions options, out NATType outNATType)
	{
		GetNATTypeOptionsInternal options2 = default(GetNATTypeOptionsInternal);
		options2.Set (ref options);
		outNATType = Helper.GetDefault<NATType> ();
		Result result = Bindings.EOS_P2P_GetNATType (base.InnerHandle, ref options2, ref outNATType);
		Helper.Dispose (ref options2);
		return result;
	}

	public Result GetNextReceivedPacketSize (ref GetNextReceivedPacketSizeOptions options, out uint outPacketSizeBytes)
	{
		GetNextReceivedPacketSizeOptionsInternal options2 = default(GetNextReceivedPacketSizeOptionsInternal);
		options2.Set (ref options);
		outPacketSizeBytes = Helper.GetDefault<uint> ();
		Result result = Bindings.EOS_P2P_GetNextReceivedPacketSize (base.InnerHandle, ref options2, ref outPacketSizeBytes);
		Helper.Dispose (ref options2);
		return result;
	}

	public Result GetPacketQueueInfo (ref GetPacketQueueInfoOptions options, out PacketQueueInfo outPacketQueueInfo)
	{
		GetPacketQueueInfoOptionsInternal options2 = default(GetPacketQueueInfoOptionsInternal);
		options2.Set (ref options);
		PacketQueueInfoInternal outPacketQueueInfo2 = Helper.GetDefault<PacketQueueInfoInternal> ();
		Result result = Bindings.EOS_P2P_GetPacketQueueInfo (base.InnerHandle, ref options2, ref outPacketQueueInfo2);
		Helper.Dispose (ref options2);
		Helper.Get<PacketQueueInfoInternal, PacketQueueInfo> (ref outPacketQueueInfo2, out outPacketQueueInfo);
		return result;
	}

	public Result GetPortRange (ref GetPortRangeOptions options, out ushort outPort, out ushort outNumAdditionalPortsToTry)
	{
		GetPortRangeOptionsInternal options2 = default(GetPortRangeOptionsInternal);
		options2.Set (ref options);
		outPort = Helper.GetDefault<ushort> ();
		outNumAdditionalPortsToTry = Helper.GetDefault<ushort> ();
		Result result = Bindings.EOS_P2P_GetPortRange (base.InnerHandle, ref options2, ref outPort, ref outNumAdditionalPortsToTry);
		Helper.Dispose (ref options2);
		return result;
	}

	public Result GetRelayControl (ref GetRelayControlOptions options, out RelayControl outRelayControl)
	{
		GetRelayControlOptionsInternal options2 = default(GetRelayControlOptionsInternal);
		options2.Set (ref options);
		outRelayControl = Helper.GetDefault<RelayControl> ();
		Result result = Bindings.EOS_P2P_GetRelayControl (base.InnerHandle, ref options2, ref outRelayControl);
		Helper.Dispose (ref options2);
		return result;
	}

	public void QueryNATType (ref QueryNATTypeOptions options, object clientData, OnQueryNATTypeCompleteCallback completionDelegate)
	{
		QueryNATTypeOptionsInternal options2 = default(QueryNATTypeOptionsInternal);
		options2.Set (ref options);
		IntPtr clientDataAddress = IntPtr.Zero;
		OnQueryNATTypeCompleteCallbackInternal onQueryNATTypeCompleteCallbackInternal = OnQueryNATTypeCompleteCallbackInternalImplementation;
		Helper.AddCallback (out clientDataAddress, clientData, completionDelegate, onQueryNATTypeCompleteCallbackInternal);
		Bindings.EOS_P2P_QueryNATType (base.InnerHandle, ref options2, clientDataAddress, onQueryNATTypeCompleteCallbackInternal);
		Helper.Dispose (ref options2);
	}

	public Result ReceivePacket (ref ReceivePacketOptions options, out ProductUserId outPeerId, out SocketId outSocketId, out byte outChannel, ArraySegment<byte> outData, out uint outBytesWritten)
	{
		ReceivePacketOptionsInternal options2 = default(ReceivePacketOptionsInternal);
		options2.Set (ref options);
		IntPtr outPeerId2 = IntPtr.Zero;
		SocketIdInternal outSocketId2 = Helper.GetDefault<SocketIdInternal> ();
		outChannel = Helper.GetDefault<byte> ();
		outBytesWritten = 0u;
		IntPtr value = Helper.AddPinnedBuffer (outData);
		Result result = Bindings.EOS_P2P_ReceivePacket (base.InnerHandle, ref options2, ref outPeerId2, ref outSocketId2, ref outChannel, value, ref outBytesWritten);
		Helper.Dispose (ref options2);
		Helper.Get (outPeerId2, out outPeerId);
		Helper.Get<SocketIdInternal, SocketId> (ref outSocketId2, out outSocketId);
		Helper.Dispose (ref value);
		return result;
	}

	public void RemoveNotifyIncomingPacketQueueFull (ulong notificationId)
	{
		Bindings.EOS_P2P_RemoveNotifyIncomingPacketQueueFull (base.InnerHandle, notificationId);
		Helper.RemoveCallbackByNotificationId (notificationId);
	}

	public void RemoveNotifyPeerConnectionClosed (ulong notificationId)
	{
		Bindings.EOS_P2P_RemoveNotifyPeerConnectionClosed (base.InnerHandle, notificationId);
		Helper.RemoveCallbackByNotificationId (notificationId);
	}

	public void RemoveNotifyPeerConnectionEstablished (ulong notificationId)
	{
		Bindings.EOS_P2P_RemoveNotifyPeerConnectionEstablished (base.InnerHandle, notificationId);
		Helper.RemoveCallbackByNotificationId (notificationId);
	}

	public void RemoveNotifyPeerConnectionInterrupted (ulong notificationId)
	{
		Bindings.EOS_P2P_RemoveNotifyPeerConnectionInterrupted (base.InnerHandle, notificationId);
		Helper.RemoveCallbackByNotificationId (notificationId);
	}

	public void RemoveNotifyPeerConnectionRequest (ulong notificationId)
	{
		Bindings.EOS_P2P_RemoveNotifyPeerConnectionRequest (base.InnerHandle, notificationId);
		Helper.RemoveCallbackByNotificationId (notificationId);
	}

	public Result SendPacket (ref SendPacketOptions options)
	{
		SendPacketOptionsInternal options2 = default(SendPacketOptionsInternal);
		options2.Set (ref options);
		Result result = Bindings.EOS_P2P_SendPacket (base.InnerHandle, ref options2);
		Helper.Dispose (ref options2);
		return result;
	}

	public Result SetPacketQueueSize (ref SetPacketQueueSizeOptions options)
	{
		SetPacketQueueSizeOptionsInternal options2 = default(SetPacketQueueSizeOptionsInternal);
		options2.Set (ref options);
		Result result = Bindings.EOS_P2P_SetPacketQueueSize (base.InnerHandle, ref options2);
		Helper.Dispose (ref options2);
		return result;
	}

	public Result SetPortRange (ref SetPortRangeOptions options)
	{
		SetPortRangeOptionsInternal options2 = default(SetPortRangeOptionsInternal);
		options2.Set (ref options);
		Result result = Bindings.EOS_P2P_SetPortRange (base.InnerHandle, ref options2);
		Helper.Dispose (ref options2);
		return result;
	}

	public Result SetRelayControl (ref SetRelayControlOptions options)
	{
		SetRelayControlOptionsInternal options2 = default(SetRelayControlOptionsInternal);
		options2.Set (ref options);
		Result result = Bindings.EOS_P2P_SetRelayControl (base.InnerHandle, ref options2);
		Helper.Dispose (ref options2);
		return result;
	}

	[MonoPInvokeCallback (typeof(OnIncomingConnectionRequestCallbackInternal))]
	internal static void OnIncomingConnectionRequestCallbackInternalImplementation (ref OnIncomingConnectionRequestInfoInternal data)
	{
		if (Helper.TryGetCallback<OnIncomingConnectionRequestInfoInternal, OnIncomingConnectionRequestCallback, OnIncomingConnectionRequestInfo> (ref data, out var callback, out var callbackInfo)) {
			callback (ref callbackInfo);
		}
	}

	[MonoPInvokeCallback (typeof(OnIncomingPacketQueueFullCallbackInternal))]
	internal static void OnIncomingPacketQueueFullCallbackInternalImplementation (ref OnIncomingPacketQueueFullInfoInternal data)
	{
		if (Helper.TryGetCallback<OnIncomingPacketQueueFullInfoInternal, OnIncomingPacketQueueFullCallback, OnIncomingPacketQueueFullInfo> (ref data, out var callback, out var callbackInfo)) {
			callback (ref callbackInfo);
		}
	}

	[MonoPInvokeCallback (typeof(OnPeerConnectionEstablishedCallbackInternal))]
	internal static void OnPeerConnectionEstablishedCallbackInternalImplementation (ref OnPeerConnectionEstablishedInfoInternal data)
	{
		if (Helper.TryGetCallback<OnPeerConnectionEstablishedInfoInternal, OnPeerConnectionEstablishedCallback, OnPeerConnectionEstablishedInfo> (ref data, out var callback, out var callbackInfo)) {
			callback (ref callbackInfo);
		}
	}

	[MonoPInvokeCallback (typeof(OnPeerConnectionInterruptedCallbackInternal))]
	internal static void OnPeerConnectionInterruptedCallbackInternalImplementation (ref OnPeerConnectionInterruptedInfoInternal data)
	{
		if (Helper.TryGetCallback<OnPeerConnectionInterruptedInfoInternal, OnPeerConnectionInterruptedCallback, OnPeerConnectionInterruptedInfo> (ref data, out var callback, out var callbackInfo)) {
			callback (ref callbackInfo);
		}
	}

	[MonoPInvokeCallback (typeof(OnQueryNATTypeCompleteCallbackInternal))]
	internal static void OnQueryNATTypeCompleteCallbackInternalImplementation (ref OnQueryNATTypeCompleteInfoInternal data)
	{
		if (Helper.TryGetAndRemoveCallback<OnQueryNATTypeCompleteInfoInternal, OnQueryNATTypeCompleteCallback, OnQueryNATTypeCompleteInfo> (ref data, out var callback, out var callbackInfo)) {
			callback (ref callbackInfo);
		}
	}

	[MonoPInvokeCallback (typeof(OnRemoteConnectionClosedCallbackInternal))]
	internal static void OnRemoteConnectionClosedCallbackInternalImplementation (ref OnRemoteConnectionClosedInfoInternal data)
	{
		if (Helper.TryGetCallback<OnRemoteConnectionClosedInfoInternal, OnRemoteConnectionClosedCallback, OnRemoteConnectionClosedInfo> (ref data, out var callback, out var callbackInfo)) {
			callback (ref callbackInfo);
		}
	}
}
