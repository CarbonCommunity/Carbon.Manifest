using UnityEngine;

public class DropBox : Mailbox
{
	public Transform EyePoint = null;

	public override bool PlayerIsOwner (BasePlayer player)
	{
		return PlayerBehind (player);
	}

	public bool PlayerBehind (BasePlayer player)
	{
		float num = Vector3.Dot (base.transform.forward, (player.transform.position - base.transform.position).normalized);
		return num <= -0.3f && GamePhysics.LineOfSight (player.eyes.position, EyePoint.position, 2162688);
	}

	public bool PlayerInfront (BasePlayer player)
	{
		return Vector3.Dot (base.transform.forward, (player.transform.position - base.transform.position).normalized) >= 0.7f;
	}

	public override bool SupportsChildDeployables ()
	{
		return true;
	}
}
