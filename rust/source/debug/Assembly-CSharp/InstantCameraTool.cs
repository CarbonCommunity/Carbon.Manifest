using System;
using System.Collections.Generic;
using ConVar;
using Facepunch;
using Network;
using UnityEngine;
using UnityEngine.Assertions;

public class InstantCameraTool : HeldEntity
{
	public ItemDefinition photoItem;

	public GameObjectRef screenshotEffect;

	public SoundDefinition startPhotoSoundDef;

	public SoundDefinition finishPhotoSoundDef;

	[Range (640f, 1920f)]
	public int resolutionX = 640;

	[Range (480f, 1080f)]
	public int resolutionY = 480;

	[Range (10f, 100f)]
	public int quality = 75;

	[Range (0f, 5f)]
	public float cooldownSeconds = 3f;

	private TimeSince _sinceLastPhoto;

	private bool hasSentAchievement = false;

	public const string PhotographPlayerAchievement = "SUMMER_PAPARAZZI";

	public override bool OnRpcMessage (BasePlayer player, uint rpc, Message msg)
	{
		TimeWarning val = TimeWarning.New ("InstantCameraTool.OnRpcMessage", 0);
		try {
			if (rpc == 3122234259u && (Object)(object)player != (Object)null) {
				Assert.IsTrue (player.isServer, "SV_RPC Message is using a clientside player!");
				if (Global.developer > 2) {
					Debug.Log ((object)string.Concat ("SV_RPCMessage: ", player, " - TakePhoto "));
				}
				TimeWarning val2 = TimeWarning.New ("TakePhoto", 0);
				try {
					TimeWarning val3 = TimeWarning.New ("Conditions", 0);
					try {
						if (!RPC_Server.CallsPerSecond.Test (3122234259u, "TakePhoto", this, player, 3uL)) {
							return true;
						}
						if (!RPC_Server.FromOwner.Test (3122234259u, "TakePhoto", this, player)) {
							return true;
						}
					} finally {
						((IDisposable)val3)?.Dispose ();
					}
					try {
						TimeWarning val4 = TimeWarning.New ("Call", 0);
						try {
							RPCMessage rPCMessage = default(RPCMessage);
							rPCMessage.connection = msg.connection;
							rPCMessage.player = player;
							rPCMessage.read = msg.read;
							RPCMessage msg2 = rPCMessage;
							TakePhoto (msg2);
						} finally {
							((IDisposable)val4)?.Dispose ();
						}
					} catch (Exception ex) {
						Debug.LogException (ex);
						player.Kick ("RPC Error in TakePhoto");
					}
				} finally {
					((IDisposable)val2)?.Dispose ();
				}
				return true;
			}
		} finally {
			((IDisposable)val)?.Dispose ();
		}
		return base.OnRpcMessage (player, rpc, msg);
	}

	[RPC_Server]
	[RPC_Server.FromOwner]
	[RPC_Server.CallsPerSecond (3uL)]
	private void TakePhoto (RPCMessage msg)
	{
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_019f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0185: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0203: Unknown result type (might be due to invalid IL or missing references)
		//IL_0208: Unknown result type (might be due to invalid IL or missing references)
		//IL_0211: Unknown result type (might be due to invalid IL or missing references)
		//IL_0213: Unknown result type (might be due to invalid IL or missing references)
		//IL_021a: Unknown result type (might be due to invalid IL or missing references)
		//IL_021f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0272: Unknown result type (might be due to invalid IL or missing references)
		BasePlayer player = msg.player;
		Item item = GetItem ();
		if ((Object)(object)player == (Object)null || item == null || item.condition <= 0f) {
			return;
		}
		byte[] array = msg.read.BytesWithSize (10485760u);
		if (array.Length > 102400 || !ImageProcessing.IsValidJPG (array, resolutionX, resolutionY)) {
			return;
		}
		Item item2 = ItemManager.Create (photoItem, 1, 0uL);
		if (item2 == null) {
			Debug.LogError ((object)"Failed to create photo item");
			return;
		}
		if (!((NetworkableId)(ref item2.instanceData.subEntity)).IsValid) {
			item2.Remove ();
			Debug.LogError ((object)"Photo has no sub-entity");
			return;
		}
		BaseNetworkable baseNetworkable = BaseNetworkable.serverEntities.Find (item2.instanceData.subEntity);
		if ((Object)(object)baseNetworkable == (Object)null) {
			item2.Remove ();
			Debug.LogError ((object)"Sub-entity was not found");
			return;
		}
		if (!(baseNetworkable is PhotoEntity photoEntity)) {
			item2.Remove ();
			Debug.LogError ((object)"Sub-entity is not a photo");
			return;
		}
		photoEntity.SetImageData (player.userID, array);
		if (!player.inventory.GiveItem (item2)) {
			item2.Drop (player.GetDropPosition (), player.GetDropVelocity ());
		}
		Effect effect = new Effect (screenshotEffect.resourcePath, ((Component)this).transform.position, ((Component)this).transform.forward, msg.connection);
		EffectNetwork.Send (effect);
		if (!hasSentAchievement && !string.IsNullOrEmpty ("SUMMER_PAPARAZZI")) {
			Vector3 position = GetOwnerPlayer ().eyes.position;
			Vector3 val = GetOwnerPlayer ().eyes.HeadForward ();
			List<BasePlayer> list = Pool.GetList<BasePlayer> ();
			Vis.Entities (position + val * 5f, 5f, list, 131072, (QueryTriggerInteraction)2);
			foreach (BasePlayer item3 in list) {
				if (item3.isServer && (Object)(object)item3 != (Object)(object)GetOwnerPlayer () && item3.IsVisible (GetOwnerPlayer ().eyes.position)) {
					hasSentAchievement = true;
					GetOwnerPlayer ().GiveAchievement ("SUMMER_PAPARAZZI");
					break;
				}
			}
			Pool.FreeList<BasePlayer> (ref list);
		}
		item.LoseCondition (1f);
	}

	public override void OnDeployed (BaseEntity parent, BasePlayer deployedBy, Item fromItem)
	{
		base.OnDeployed (parent, deployedBy, fromItem);
		hasSentAchievement = false;
	}
}
