#define UNITY_ASSERTIONS
using System;
using System.Linq;
using System.Threading.Tasks;
using ConVar;
using Network;
using UnityEngine;
using UnityEngine.Assertions;

public class SteamInventory : EntityComponent<BasePlayer>
{
	private IPlayerItem[] Items;

	public override bool OnRpcMessage (BasePlayer player, uint rpc, Message msg)
	{
		using (TimeWarning.New ("SteamInventory.OnRpcMessage")) {
			if (rpc == 643458331 && player != null) {
				Assert.IsTrue (player.isServer, "SV_RPC Message is using a clientside player!");
				if (Global.developer > 2) {
					Debug.Log (string.Concat ("SV_RPCMessage: ", player, " - UpdateSteamInventory "));
				}
				using (TimeWarning.New ("UpdateSteamInventory")) {
					using (TimeWarning.New ("Conditions")) {
						if (!BaseEntity.RPC_Server.FromOwner.Test (643458331u, "UpdateSteamInventory", GetBaseEntity (), player)) {
							return true;
						}
					}
					try {
						using (TimeWarning.New ("Call")) {
							BaseEntity.RPCMessage rPCMessage = default(BaseEntity.RPCMessage);
							rPCMessage.connection = msg.connection;
							rPCMessage.player = player;
							rPCMessage.read = msg.read;
							BaseEntity.RPCMessage msg2 = rPCMessage;
							UpdateSteamInventory (msg2);
						}
					} catch (Exception exception) {
						Debug.LogException (exception);
						player.Kick ("RPC Error in UpdateSteamInventory");
					}
				}
				return true;
			}
		}
		return base.OnRpcMessage (player, rpc, msg);
	}

	public bool HasItem (int itemid)
	{
		if (base.baseEntity.UnlockAllSkins) {
			return true;
		}
		if (Items == null) {
			return false;
		}
		IPlayerItem[] items = Items;
		foreach (IPlayerItem playerItem in items) {
			if (playerItem.DefinitionId == itemid) {
				return true;
			}
		}
		return false;
	}

	[BaseEntity.RPC_Server]
	[BaseEntity.RPC_Server.FromOwner]
	private async Task UpdateSteamInventory (BaseEntity.RPCMessage msg)
	{
		byte[] data = msg.read.BytesWithSize ();
		if (data == null) {
			Debug.LogWarning ("UpdateSteamInventory: Data is null");
			return;
		}
		IPlayerInventory result = await PlatformService.Instance.DeserializeInventory (data);
		if (result == null) {
			Debug.LogWarning ("UpdateSteamInventory: result is null");
		} else if (base.baseEntity == null) {
			Debug.LogWarning ("UpdateSteamInventory: player is null");
		} else if (!result.BelongsTo (base.baseEntity.userID)) {
			Debug.LogWarning ($"UpdateSteamPlayer: inventory belongs to someone else (userID={base.baseEntity.userID})");
		} else if ((bool)base.gameObject) {
			Items = result.Items.ToArray ();
			result.Dispose ();
		}
	}
}
