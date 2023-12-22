using System;
using ConVar;
using Facepunch;
using Network;
using ProtoBuf;
using Rust;
using UnityEngine;
using UnityEngine.Assertions;

public class PoweredRemoteControlEntity : IOEntity, IRemoteControllable
{
	public string rcIdentifier = "";

	public Transform viewEyes;

	public GameObjectRef IDPanelPrefab;

	public RemoteControllableControls rcControls;

	public bool isStatic;

	public bool appendEntityIDToIdentifier;

	public virtual bool RequiresMouse => false;

	public virtual float MaxRange => 10000f;

	public RemoteControllableControls RequiredControls => rcControls;

	public bool CanPing => EntityCanPing;

	protected virtual bool EntityCanPing => false;

	public virtual bool CanAcceptInput => false;

	public int ViewerCount { get; private set; }

	public CameraViewerId? ControllingViewerId { get; private set; }

	public bool IsBeingControlled {
		get {
			if (ViewerCount > 0) {
				return ControllingViewerId.HasValue;
			}
			return false;
		}
	}

	public override bool OnRpcMessage (BasePlayer player, uint rpc, Message msg)
	{
		TimeWarning val = TimeWarning.New ("PoweredRemoteControlEntity.OnRpcMessage", 0);
		try {
			if (rpc == 1053317251 && (Object)(object)player != (Object)null) {
				Assert.IsTrue (player.isServer, "SV_RPC Message is using a clientside player!");
				if (Global.developer > 2) {
					Debug.Log ((object)string.Concat ("SV_RPCMessage: ", player, " - Server_SetID "));
				}
				TimeWarning val2 = TimeWarning.New ("Server_SetID", 0);
				try {
					TimeWarning val3 = TimeWarning.New ("Conditions", 0);
					try {
						if (!RPC_Server.MaxDistance.Test (1053317251u, "Server_SetID", this, player, 3f)) {
							return true;
						}
					} finally {
						((IDisposable)val3)?.Dispose ();
					}
					try {
						val3 = TimeWarning.New ("Call", 0);
						try {
							RPCMessage rPCMessage = default(RPCMessage);
							rPCMessage.connection = msg.connection;
							rPCMessage.player = player;
							rPCMessage.read = msg.read;
							RPCMessage msg2 = rPCMessage;
							Server_SetID (msg2);
						} finally {
							((IDisposable)val3)?.Dispose ();
						}
					} catch (Exception ex) {
						Debug.LogException (ex);
						player.Kick ("RPC Error in Server_SetID");
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

	public bool IsStatic ()
	{
		return isStatic;
	}

	public override void UpdateHasPower (int inputAmount, int inputSlot)
	{
		base.UpdateHasPower (inputAmount, inputSlot);
		UpdateRCAccess (IsPowered ());
	}

	public void UpdateRCAccess (bool isOnline)
	{
		if (isOnline) {
			RemoteControlEntity.InstallControllable (this);
		} else {
			RemoteControlEntity.RemoveControllable (this);
		}
	}

	public override void Spawn ()
	{
		base.Spawn ();
		string text = "#ID";
		if (IsStatic () && rcIdentifier.Contains (text)) {
			int length = rcIdentifier.IndexOf (text);
			_ = text.Length;
			string text2 = rcIdentifier.Substring (0, length);
			text2 += ((object)(NetworkableId)(ref net.ID)).ToString ();
			UpdateIdentifier (text2);
		}
	}

	public virtual bool InitializeControl (CameraViewerId viewerID)
	{
		ViewerCount++;
		if (CanAcceptInput && !ControllingViewerId.HasValue) {
			ControllingViewerId = viewerID;
			return true;
		}
		return !CanAcceptInput;
	}

	public virtual void StopControl (CameraViewerId viewerID)
	{
		ViewerCount--;
		if (ControllingViewerId == viewerID) {
			ControllingViewerId = null;
		}
	}

	public virtual void UserInput (InputState inputState, CameraViewerId viewerID)
	{
	}

	public Transform GetEyes ()
	{
		return viewEyes;
	}

	public virtual float GetFovScale ()
	{
		return 1f;
	}

	public virtual bool CanControl (ulong playerID)
	{
		if (!IsPowered ()) {
			return IsStatic ();
		}
		return true;
	}

	public BaseEntity GetEnt ()
	{
		return this;
	}

	public virtual void RCSetup ()
	{
	}

	public virtual void RCShutdown ()
	{
		if (base.isServer) {
			RemoteControlEntity.RemoveControllable (this);
		}
	}

	[RPC_Server]
	[RPC_Server.MaxDistance (3f)]
	public void Server_SetID (RPCMessage msg)
	{
		if (IsStatic ()) {
			return;
		}
		BasePlayer player = msg.player;
		if (!CanChangeID (player)) {
			return;
		}
		string text = msg.read.String (256);
		if (string.IsNullOrEmpty (text) || ComputerStation.IsValidIdentifier (text)) {
			string text2 = msg.read.String (256);
			if (ComputerStation.IsValidIdentifier (text2) && text == GetIdentifier ()) {
				UpdateIdentifier (text2);
			}
		}
	}

	public override bool CanUseNetworkCache (Connection connection)
	{
		if (IsStatic ()) {
			return base.CanUseNetworkCache (connection);
		}
		return false;
	}

	public override void Save (SaveInfo info)
	{
		base.Save (info);
		if (info.forDisk || IsStatic () || CanChangeID (info.forConnection?.player as BasePlayer)) {
			info.msg.rcEntity = Pool.Get<RCEntity> ();
			info.msg.rcEntity.identifier = GetIdentifier ();
		}
	}

	public override void Load (LoadInfo info)
	{
		base.Load (info);
		if (info.msg.rcEntity != null && ComputerStation.IsValidIdentifier (info.msg.rcEntity.identifier)) {
			UpdateIdentifier (info.msg.rcEntity.identifier);
		}
	}

	public void UpdateIdentifier (string newID, bool clientSend = false)
	{
		_ = rcIdentifier;
		if (base.isServer) {
			if (!RemoteControlEntity.IDInUse (newID)) {
				rcIdentifier = newID;
			}
			if (!Application.isLoadingSave) {
				SendNetworkUpdate ();
			}
		}
	}

	public string GetIdentifier ()
	{
		return rcIdentifier;
	}

	public override void InitShared ()
	{
		base.InitShared ();
		RCSetup ();
	}

	public override void DestroyShared ()
	{
		RCShutdown ();
		base.DestroyShared ();
	}

	protected bool CanChangeID (BasePlayer player)
	{
		if ((Object)(object)player != (Object)null && player.CanBuild ()) {
			return player.IsBuildingAuthed ();
		}
		return false;
	}
}
