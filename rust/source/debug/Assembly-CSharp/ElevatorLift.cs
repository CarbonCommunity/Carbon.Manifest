using System;
using ConVar;
using Network;
using UnityEngine;
using UnityEngine.Assertions;

public class ElevatorLift : BaseCombatEntity
{
	public GameObject DescendingHurtTrigger = null;

	public GameObject MovementCollider = null;

	public Transform UpButtonPoint = null;

	public Transform DownButtonPoint = null;

	public TriggerNotify VehicleTrigger = null;

	public GameObjectRef LiftArrivalScreenBounce = null;

	public SoundDefinition liftMovementLoopDef;

	public SoundDefinition liftMovementStartDef;

	public SoundDefinition liftMovementStopDef;

	public SoundDefinition liftMovementAccentSoundDef;

	public GameObjectRef liftButtonPressedEffect = null;

	public float movementAccentMinInterval = 0.75f;

	public float movementAccentMaxInterval = 3f;

	private Sound liftMovementLoopSound;

	private float nextMovementAccent;

	public Vector3 lastPosition;

	private const Flags PressedUp = Flags.Reserved1;

	private const Flags PressedDown = Flags.Reserved2;

	private Elevator owner => GetParentEntity () as Elevator;

	public override bool OnRpcMessage (BasePlayer player, uint rpc, Message msg)
	{
		TimeWarning val = TimeWarning.New ("ElevatorLift.OnRpcMessage", 0);
		try {
			if (rpc == 4061236510u && (Object)(object)player != (Object)null) {
				Assert.IsTrue (player.isServer, "SV_RPC Message is using a clientside player!");
				if (Global.developer > 2) {
					Debug.Log ((object)string.Concat ("SV_RPCMessage: ", player, " - Server_RaiseLowerFloor "));
				}
				TimeWarning val2 = TimeWarning.New ("Server_RaiseLowerFloor", 0);
				try {
					TimeWarning val3 = TimeWarning.New ("Conditions", 0);
					try {
						if (!RPC_Server.IsVisible.Test (4061236510u, "Server_RaiseLowerFloor", this, player, 3f)) {
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
							Server_RaiseLowerFloor (msg2);
						} finally {
							((IDisposable)val4)?.Dispose ();
						}
					} catch (Exception ex) {
						Debug.LogException (ex);
						player.Kick ("RPC Error in Server_RaiseLowerFloor");
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

	public override void ServerInit ()
	{
		base.ServerInit ();
		ToggleHurtTrigger (state: false);
	}

	public void ToggleHurtTrigger (bool state)
	{
		if ((Object)(object)DescendingHurtTrigger != (Object)null) {
			DescendingHurtTrigger.SetActive (state);
		}
	}

	[RPC_Server]
	[RPC_Server.IsVisible (3f)]
	public void Server_RaiseLowerFloor (RPCMessage msg)
	{
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		if (CanMove ()) {
			Elevator.Direction direction = (Elevator.Direction)msg.read.Int32 ();
			bool goTopBottom = msg.read.Bit ();
			SetFlag ((direction == Elevator.Direction.Up) ? Flags.Reserved1 : Flags.Reserved2, b: true);
			owner.Server_RaiseLowerElevator (direction, goTopBottom);
			((FacepunchBehaviour)this).Invoke ((Action)ClearDirection, 0.7f);
			if (liftButtonPressedEffect.isValid) {
				Effect.server.Run (liftButtonPressedEffect.resourcePath, ((Component)this).transform.position, Vector3.up);
			}
		}
	}

	private void ClearDirection ()
	{
		SetFlag (Flags.Reserved1, b: false);
		SetFlag (Flags.Reserved2, b: false);
	}

	public override void Hurt (HitInfo info)
	{
		if (HasParent () && GetParentEntity () is BaseCombatEntity baseCombatEntity) {
			baseCombatEntity.Hurt (info);
		}
	}

	public override void AdminKill ()
	{
		if (HasParent ()) {
			GetParentEntity ().AdminKill ();
		} else {
			base.AdminKill ();
		}
	}

	public override void PostServerLoad ()
	{
		base.PostServerLoad ();
		ClearDirection ();
	}

	public bool CanMove ()
	{
		if (VehicleTrigger.HasContents && VehicleTrigger.entityContents != null) {
			foreach (BaseEntity entityContent in VehicleTrigger.entityContents) {
				if (entityContent is Drone) {
					continue;
				}
				return false;
			}
		}
		return true;
	}

	public virtual void NotifyNewFloor (int newFloor, int totalFloors)
	{
	}

	public void ToggleMovementCollider (bool state)
	{
		if ((Object)(object)MovementCollider != (Object)null) {
			MovementCollider.SetActive (state);
		}
	}
}
