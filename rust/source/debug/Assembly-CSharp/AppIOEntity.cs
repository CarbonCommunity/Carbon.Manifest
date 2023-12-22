#define UNITY_ASSERTIONS
using System;
using System.Collections.Generic;
using System.Globalization;
using CompanionServer;
using ConVar;
using Facepunch;
using Network;
using ProtoBuf;
using UnityEngine;
using UnityEngine.Assertions;

public abstract class AppIOEntity : IOEntity
{
	private float _cacheTime;

	private BuildingPrivlidge _cache;

	public abstract AppEntityType Type { get; }

	public virtual bool Value {
		get {
			return false;
		}
		set {
		}
	}

	public override bool OnRpcMessage (BasePlayer player, uint rpc, Message msg)
	{
		using (TimeWarning.New ("AppIOEntity.OnRpcMessage")) {
			if (rpc == 3018927126u && player != null) {
				Assert.IsTrue (player.isServer, "SV_RPC Message is using a clientside player!");
				if (Global.developer > 2) {
					Debug.Log (string.Concat ("SV_RPCMessage: ", player, " - PairWithApp "));
				}
				using (TimeWarning.New ("PairWithApp")) {
					using (TimeWarning.New ("Conditions")) {
						if (!RPC_Server.CallsPerSecond.Test (3018927126u, "PairWithApp", this, player, 5uL)) {
							return true;
						}
						if (!RPC_Server.IsVisible.Test (3018927126u, "PairWithApp", this, player, 3f)) {
							return true;
						}
					}
					try {
						using (TimeWarning.New ("Call")) {
							RPCMessage rPCMessage = default(RPCMessage);
							rPCMessage.connection = msg.connection;
							rPCMessage.player = player;
							rPCMessage.read = msg.read;
							RPCMessage msg2 = rPCMessage;
							PairWithApp (msg2);
						}
					} catch (Exception exception) {
						Debug.LogException (exception);
						player.Kick ("RPC Error in PairWithApp");
					}
				}
				return true;
			}
		}
		return base.OnRpcMessage (player, rpc, msg);
	}

	protected void BroadcastValueChange ()
	{
		if (this.IsValid ()) {
			EntityTarget target = GetTarget ();
			AppBroadcast appBroadcast = Facepunch.Pool.Get<AppBroadcast> ();
			appBroadcast.entityChanged = Facepunch.Pool.Get<AppEntityChanged> ();
			appBroadcast.entityChanged.entityId = net.ID;
			appBroadcast.entityChanged.payload = Facepunch.Pool.Get<AppEntityPayload> ();
			FillEntityPayload (appBroadcast.entityChanged.payload);
			CompanionServer.Server.Broadcast (target, appBroadcast);
		}
	}

	internal virtual void FillEntityPayload (AppEntityPayload payload)
	{
		payload.value = Value;
	}

	public override BuildingPrivlidge GetBuildingPrivilege ()
	{
		if (UnityEngine.Time.realtimeSinceStartup - _cacheTime > 5f) {
			_cache = base.GetBuildingPrivilege ();
			_cacheTime = UnityEngine.Time.realtimeSinceStartup;
		}
		return _cache;
	}

	public EntityTarget GetTarget ()
	{
		return new EntityTarget (net.ID);
	}

	[RPC_Server]
	[RPC_Server.IsVisible (3f)]
	[RPC_Server.CallsPerSecond (5uL)]
	public async void PairWithApp (RPCMessage msg)
	{
		BasePlayer player = msg.player;
		Dictionary<string, string> data = CompanionServer.Util.GetPlayerPairingData (player);
		data.Add ("entityId", net.ID.Value.ToString ("G", CultureInfo.InvariantCulture));
		data.Add ("entityType", ((int)Type).ToString ("G", CultureInfo.InvariantCulture));
		data.Add ("entityName", GetDisplayName ());
		NotificationSendResult result = await CompanionServer.Util.SendPairNotification ("entity", player, GetDisplayName (), "Tap to pair with this device.", data);
		if (result == NotificationSendResult.Sent) {
			OnPairedWithPlayer (msg.player);
		} else {
			player.ClientRPCPlayer (null, player, "HandleCompanionPairingResult", (int)result);
		}
	}

	protected virtual void OnPairedWithPlayer (BasePlayer player)
	{
	}
}
