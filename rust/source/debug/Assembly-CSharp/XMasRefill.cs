using System;
using System.Collections.Generic;
using ConVar;
using Network;
using UnityEngine;

public class XMasRefill : BaseEntity
{
	public GameObjectRef[] giftPrefabs;

	public List<BasePlayer> goodKids;

	public List<Stocking> stockings;

	public AudioSource bells;

	public override bool OnRpcMessage (BasePlayer player, uint rpc, Message msg)
	{
		TimeWarning val = TimeWarning.New ("XMasRefill.OnRpcMessage", 0);
		try {
		} finally {
			((IDisposable)val)?.Dispose ();
		}
		return base.OnRpcMessage (player, rpc, msg);
	}

	public float GiftRadius ()
	{
		return XMas.spawnRange;
	}

	public int GiftsPerPlayer ()
	{
		return XMas.giftsPerPlayer;
	}

	public int GiftSpawnAttempts ()
	{
		return XMas.giftsPerPlayer * XMas.spawnAttempts;
	}

	public override void ServerInit ()
	{
		base.ServerInit ();
		if (!XMas.enabled) {
			((FacepunchBehaviour)this).Invoke ((Action)RemoveMe, 0.1f);
			return;
		}
		goodKids = ((BasePlayer.activePlayerList != null) ? new List<BasePlayer> ((IEnumerable<BasePlayer>)BasePlayer.activePlayerList) : new List<BasePlayer> ());
		stockings = ((Stocking.stockings != null) ? new List<Stocking> ((IEnumerable<Stocking>)Stocking.stockings.Values) : new List<Stocking> ());
		((FacepunchBehaviour)this).Invoke ((Action)RemoveMe, 60f);
		((FacepunchBehaviour)this).InvokeRepeating ((Action)DistributeLoot, 3f, 0.02f);
		((FacepunchBehaviour)this).Invoke ((Action)SendBells, 0.5f);
	}

	public void SendBells ()
	{
		ClientRPC (null, "PlayBells");
	}

	public void RemoveMe ()
	{
		if (goodKids.Count == 0 && stockings.Count == 0) {
			Kill ();
		} else {
			((FacepunchBehaviour)this).Invoke ((Action)RemoveMe, 60f);
		}
	}

	public void DistributeLoot ()
	{
		if (goodKids.Count > 0) {
			BasePlayer basePlayer = null;
			foreach (BasePlayer goodKid in goodKids) {
				if (!goodKid.IsSleeping () && !goodKid.IsWounded () && goodKid.IsAlive ()) {
					basePlayer = goodKid;
					break;
				}
			}
			if (Object.op_Implicit ((Object)(object)basePlayer)) {
				DistributeGiftsForPlayer (basePlayer);
				goodKids.Remove (basePlayer);
			}
		}
		if (stockings.Count > 0) {
			Stocking stocking = stockings [0];
			if ((Object)(object)stocking != (Object)null) {
				stocking.SpawnLoot ();
			}
			stockings.RemoveAt (0);
		}
	}

	protected bool DropToGround (ref Vector3 pos)
	{
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		int num = 1235288065;
		int num2 = 8454144;
		if (Object.op_Implicit ((Object)(object)TerrainMeta.TopologyMap) && ((uint)TerrainMeta.TopologyMap.GetTopology (pos) & 0x14080u) != 0) {
			return false;
		}
		if (Object.op_Implicit ((Object)(object)TerrainMeta.HeightMap) && Object.op_Implicit ((Object)(object)TerrainMeta.Collision) && !TerrainMeta.Collision.GetIgnore (pos)) {
			float height = TerrainMeta.HeightMap.GetHeight (pos);
			pos.y = Mathf.Max (pos.y, height);
		}
		if (!TransformUtil.GetGroundInfo (pos, out var hitOut, 80f, LayerMask.op_Implicit (num))) {
			return false;
		}
		int num3 = 1 << ((Component)((RaycastHit)(ref hitOut)).transform).gameObject.layer;
		if ((num3 & num2) == 0) {
			return false;
		}
		pos = ((RaycastHit)(ref hitOut)).point;
		return true;
	}

	public bool DistributeGiftsForPlayer (BasePlayer player)
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		int num = GiftsPerPlayer ();
		int num2 = GiftSpawnAttempts ();
		for (int i = 0; i < num2; i++) {
			if (num <= 0) {
				break;
			}
			Vector2 val = Random.insideUnitCircle * GiftRadius ();
			Vector3 pos = ((Component)player).transform.position + new Vector3 (val.x, 10f, val.y);
			Quaternion rot = Quaternion.Euler (0f, Random.Range (0f, 360f), 0f);
			if (DropToGround (ref pos)) {
				string resourcePath = giftPrefabs [Random.Range (0, giftPrefabs.Length)].resourcePath;
				BaseEntity baseEntity = GameManager.server.CreateEntity (resourcePath, pos, rot);
				if (Object.op_Implicit ((Object)(object)baseEntity)) {
					baseEntity.Spawn ();
					num--;
				}
			}
		}
		return true;
	}
}
