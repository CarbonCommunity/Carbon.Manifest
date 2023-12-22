using System;
using ConVar;
using Rust;
using UnityEngine;

public class SupplyDrop : LootContainer
{
	public GameObjectRef parachutePrefab;

	private const Flags FlagNightLight = Flags.Reserved1;

	private BaseEntity parachute;

	public override void ServerInit ()
	{
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		base.ServerInit ();
		if (!Application.isLoadingSave) {
			if (parachutePrefab.isValid) {
				parachute = GameManager.server.CreateEntity (parachutePrefab.resourcePath);
			}
			if (Object.op_Implicit ((Object)(object)parachute)) {
				parachute.SetParent (this, "parachute_attach");
				parachute.Spawn ();
			}
		}
		isLootable = false;
		((FacepunchBehaviour)this).Invoke ((Action)MakeLootable, 300f);
		((FacepunchBehaviour)this).InvokeRepeating ((Action)CheckNightLight, 0f, 30f);
	}

	protected override void OnChildAdded (BaseEntity child)
	{
		base.OnChildAdded (child);
		if (base.isServer && Application.isLoadingSave) {
			if ((Object)(object)parachute != (Object)null) {
				Debug.LogWarning ((object)"More than one child entity was added to SupplyDrop! Expected only the parachute.", (Object)(object)this);
			}
			parachute = child;
		}
	}

	private void RemoveParachute ()
	{
		if (Object.op_Implicit ((Object)(object)parachute)) {
			parachute.Kill ();
			parachute = null;
		}
	}

	public void MakeLootable ()
	{
		isLootable = true;
	}

	private void OnCollisionEnter (Collision collision)
	{
		if (((1 << ((Component)collision.collider).gameObject.layer) & 0x40A10111) > 0) {
			RemoveParachute ();
			MakeLootable ();
		}
	}

	private void CheckNightLight ()
	{
		SetFlag (Flags.Reserved1, Env.time > 20f || Env.time < 7f);
	}
}
