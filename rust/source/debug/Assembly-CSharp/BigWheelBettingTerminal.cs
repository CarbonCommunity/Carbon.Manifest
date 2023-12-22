using System;
using Network;
using UnityEngine;

public class BigWheelBettingTerminal : StorageContainer
{
	public BigWheelGame bigWheel;

	public Vector3 seatedPlayerOffset = Vector3.forward;

	public float offsetCheckRadius = 0.4f;

	public SoundDefinition winSound;

	public SoundDefinition loseSound;

	[NonSerialized]
	public BasePlayer lastPlayer;

	public override bool OnRpcMessage (BasePlayer player, uint rpc, Message msg)
	{
		using (TimeWarning.New ("BigWheelBettingTerminal.OnRpcMessage")) {
		}
		return base.OnRpcMessage (player, rpc, msg);
	}

	public new void OnDrawGizmos ()
	{
		Gizmos.color = Color.yellow;
		Vector3 center = base.transform.TransformPoint (seatedPlayerOffset);
		Gizmos.DrawSphere (center, offsetCheckRadius);
		base.OnDrawGizmos ();
	}

	public bool IsPlayerValid (BasePlayer player)
	{
		if (!player.isMounted || !(player.GetMounted () is BaseChair)) {
			return false;
		}
		Vector3 b = base.transform.TransformPoint (seatedPlayerOffset);
		float num = Vector3Ex.Distance2D (player.transform.position, b);
		if (num > offsetCheckRadius) {
			return false;
		}
		return true;
	}

	public override bool PlayerOpenLoot (BasePlayer player, string panelToOpen = "", bool doPositionChecks = true)
	{
		if (!IsPlayerValid (player)) {
			return false;
		}
		bool flag = base.PlayerOpenLoot (player, panelToOpen);
		if (flag) {
			lastPlayer = player;
		}
		return flag;
	}

	public bool TrySetBigWheel (BigWheelGame newWheel)
	{
		if (base.isClient) {
			return false;
		}
		if (bigWheel != null && bigWheel != newWheel) {
			float num = Vector3.SqrMagnitude (bigWheel.transform.position - base.transform.position);
			float num2 = Vector3.SqrMagnitude (newWheel.transform.position - base.transform.position);
			if (num2 >= num) {
				return false;
			}
			bigWheel.RemoveTerminal (this);
		}
		bigWheel = newWheel;
		return true;
	}
}
