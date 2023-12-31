#define UNITY_ASSERTIONS
using System;
using ConVar;
using Facepunch;
using Facepunch.Rust;
using Network;
using ProtoBuf;
using Rust;
using UnityEngine;
using UnityEngine.Assertions;

public class BaseFishingRod : HeldEntity
{
	public class UpdateFishingRod : ObjectWorkQueue<BaseFishingRod>
	{
		protected override void RunJob (BaseFishingRod entity)
		{
			if (ShouldAdd (entity)) {
				entity.CatchProcessBudgeted ();
			}
		}

		protected override bool ShouldAdd (BaseFishingRod entity)
		{
			return base.ShouldAdd (entity) && entity.IsValid ();
		}
	}

	public enum CatchState
	{
		None,
		Aiming,
		Waiting,
		Catching,
		Caught
	}

	[Flags]
	public enum FishState
	{
		PullingLeft = 1,
		PullingRight = 2,
		PullingBack = 4
	}

	public enum FailReason
	{
		UserRequested,
		BadAngle,
		TensionBreak,
		Unequipped,
		TimeOut,
		Success,
		NoWaterFound,
		Obstructed,
		NoLure,
		TooShallow,
		TooClose,
		TooFarAway,
		PlayerMoved
	}

	public static UpdateFishingRod updateFishingRodQueue = new UpdateFishingRod ();

	private FishLookup fishLookup = null;

	private TimeUntil nextFishStateChange;

	private TimeSince fishCatchDuration;

	private float strainTimer = 0f;

	private const float strainMax = 6f;

	private TimeSince lastStrainUpdate;

	private TimeUntil catchTime;

	private TimeSince lastSightCheck;

	private Vector3 playerStartPosition;

	private WaterBody surfaceBody;

	private ItemDefinition lureUsed = null;

	private ItemDefinition currentFishTarget = null;

	private ItemModFishable fishableModifier = null;

	private ItemModFishable lastFish = null;

	private bool inQueue = false;

	[ServerVar]
	public static bool ForceSuccess = false;

	[ServerVar]
	public static bool ForceFail = false;

	[ServerVar]
	public static bool ImmediateHook = false;

	public GameObjectRef FishingBobberRef;

	public float FishCatchDistance = 0.5f;

	public LineRenderer ReelLineRenderer = null;

	public Transform LineRendererWorldStartPos;

	private FishState currentFishState = (FishState)0;

	private EntityRef<FishingBobber> currentBobber;

	public float ConditionLossOnSuccess = 0.02f;

	public float ConditionLossOnFail = 0.04f;

	public float GlobalStrainSpeedMultiplier = 1f;

	public float MaxCastDistance = 10f;

	public const Flags Straining = Flags.Reserved1;

	public ItemModFishable ForceFish = null;

	public static Flags PullingLeftFlag = Flags.Reserved6;

	public static Flags PullingRightFlag = Flags.Reserved7;

	public static Flags ReelingInFlag = Flags.Reserved8;

	public GameObjectRef BobberPreview = null;

	public SoundDefinition onLineSoundDef;

	public SoundDefinition strainSoundDef;

	public AnimationCurve strainGainCurve;

	public SoundDefinition tensionBreakSoundDef;

	public CatchState CurrentState { get; private set; } = CatchState.None;


	public override bool OnRpcMessage (BasePlayer player, uint rpc, Message msg)
	{
		using (TimeWarning.New ("BaseFishingRod.OnRpcMessage")) {
			if (rpc == 4237324865u && player != null) {
				Assert.IsTrue (player.isServer, "SV_RPC Message is using a clientside player!");
				if (ConVar.Global.developer > 2) {
					Debug.Log (string.Concat ("SV_RPCMessage: ", player, " - Server_Cancel "));
				}
				using (TimeWarning.New ("Server_Cancel")) {
					using (TimeWarning.New ("Conditions")) {
						if (!RPC_Server.IsActiveItem.Test (4237324865u, "Server_Cancel", this, player)) {
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
							Server_Cancel (msg2);
						}
					} catch (Exception exception) {
						Debug.LogException (exception);
						player.Kick ("RPC Error in Server_Cancel");
					}
				}
				return true;
			}
			if (rpc == 4238539495u && player != null) {
				Assert.IsTrue (player.isServer, "SV_RPC Message is using a clientside player!");
				if (ConVar.Global.developer > 2) {
					Debug.Log (string.Concat ("SV_RPCMessage: ", player, " - Server_RequestCast "));
				}
				using (TimeWarning.New ("Server_RequestCast")) {
					using (TimeWarning.New ("Conditions")) {
						if (!RPC_Server.IsActiveItem.Test (4238539495u, "Server_RequestCast", this, player)) {
							return true;
						}
					}
					try {
						using (TimeWarning.New ("Call")) {
							RPCMessage rPCMessage = default(RPCMessage);
							rPCMessage.connection = msg.connection;
							rPCMessage.player = player;
							rPCMessage.read = msg.read;
							RPCMessage msg3 = rPCMessage;
							Server_RequestCast (msg3);
						}
					} catch (Exception exception2) {
						Debug.LogException (exception2);
						player.Kick ("RPC Error in Server_RequestCast");
					}
				}
				return true;
			}
		}
		return base.OnRpcMessage (player, rpc, msg);
	}

	[RPC_Server]
	[RPC_Server.IsActiveItem]
	private void Server_RequestCast (RPCMessage msg)
	{
		Vector3 pos = msg.read.Vector3 ();
		BasePlayer ownerPlayer = GetOwnerPlayer ();
		Item currentLure = GetCurrentLure ();
		if (currentLure == null) {
			FailedCast (FailReason.NoLure);
			return;
		}
		if (!EvaluateFishingPosition (ref pos, ownerPlayer, out var reason, out surfaceBody)) {
			FailedCast (reason);
			return;
		}
		FishingBobber component = base.gameManager.CreateEntity (FishingBobberRef.resourcePath, base.transform.position + Vector3.up * 2.8f + ownerPlayer.eyes.BodyForward () * 1.8f, GetOwnerPlayer ().ServerRotation).GetComponent<FishingBobber> ();
		component.transform.forward = GetOwnerPlayer ().eyes.BodyForward ();
		component.Spawn ();
		component.InitialiseBobber (ownerPlayer, surfaceBody, pos);
		lureUsed = currentLure.info;
		currentLure.UseItem ();
		if (fishLookup == null) {
			fishLookup = PrefabAttribute.server.Find<FishLookup> (prefabID);
		}
		currentFishTarget = fishLookup.GetFish (component.transform.position, surfaceBody, lureUsed, out fishableModifier, lastFish);
		lastFish = fishableModifier;
		currentBobber.Set (component);
		ClientRPC (null, "Client_ReceiveCastPoint", component.net.ID);
		ownerPlayer.SignalBroadcast (Signal.Attack);
		catchTime = (ImmediateHook ? 0f : UnityEngine.Random.Range (10f, 20f));
		catchTime = (float)catchTime * fishableModifier.CatchWaitTimeMultiplier;
		float val = (lureUsed.TryGetComponent<ItemModCompostable> (out var component2) ? component2.BaitValue : 0f);
		val = Mathx.RemapValClamped (val, 0f, 20f, 1f, 10f);
		catchTime = Mathf.Clamp ((float)catchTime - val, 3f, 20f);
		playerStartPosition = ownerPlayer.transform.position;
		SetFlag (Flags.Busy, b: true);
		CurrentState = CatchState.Waiting;
		InvokeRepeating (CatchProcess, 0f, 0f);
		inQueue = false;
	}

	private void FailedCast (FailReason reason)
	{
		CurrentState = CatchState.None;
		ClientRPC (null, "Client_ResetLine", (int)reason);
	}

	private void CatchProcess ()
	{
		if (!inQueue) {
			inQueue = true;
			updateFishingRodQueue.Add (this);
		}
	}

	private void CatchProcessBudgeted ()
	{
		inQueue = false;
		FishingBobber fishingBobber = currentBobber.Get (serverside: true);
		BasePlayer ownerPlayer = GetOwnerPlayer ();
		if (ownerPlayer == null || ownerPlayer.IsSleeping () || ownerPlayer.IsWounded () || ownerPlayer.IsDead () || fishingBobber == null) {
			Server_Cancel (FailReason.UserRequested);
			return;
		}
		Vector3 position = ownerPlayer.transform.position;
		float num = Vector3.Angle ((fishingBobber.transform.position.WithY (0f) - position.WithY (0f)).normalized, ownerPlayer.eyes.HeadForward ().WithY (0f));
		float num2 = Vector3.Distance (position, fishingBobber.transform.position.WithY (position.y));
		if (num > ((num2 > 1.2f) ? 60f : 180f)) {
			Server_Cancel (FailReason.BadAngle);
			return;
		}
		if (num2 > 1.2f && (float)lastSightCheck > 0.4f) {
			if (!GamePhysics.LineOfSight (ownerPlayer.eyes.position, fishingBobber.transform.position, 1218511105)) {
				Server_Cancel (FailReason.Obstructed);
				return;
			}
			lastSightCheck = 0f;
		}
		float num3 = Vector3.Distance (position, fishingBobber.transform.position);
		if (num3 > MaxCastDistance * 2f) {
			Server_Cancel (FailReason.TooFarAway);
			return;
		}
		float num4 = Vector3.Distance (playerStartPosition, position);
		if (num4 > 1f) {
			Server_Cancel (FailReason.PlayerMoved);
			return;
		}
		if (CurrentState == CatchState.Waiting) {
			if ((float)catchTime < 0f) {
				ClientRPC (null, "Client_HookedSomething");
				CurrentState = CatchState.Catching;
				fishingBobber.SetFlag (Flags.Reserved1, b: true);
				nextFishStateChange = 0f;
				fishCatchDuration = 0f;
				strainTimer = 0f;
			}
			return;
		}
		FishState fishState = currentFishState;
		if ((float)nextFishStateChange < 0f) {
			float num5 = Mathx.RemapValClamped (fishingBobber.TireAmount, 0f, 20f, 0f, 1f);
			if (currentFishState != 0) {
				currentFishState = (FishState)0;
				nextFishStateChange = UnityEngine.Random.Range (2f, 4f) * (num5 + 1f);
			} else {
				nextFishStateChange = UnityEngine.Random.Range (3f, 7f) * (1f - num5);
				int num6 = UnityEngine.Random.Range (0, 100);
				if (num6 < 50) {
					currentFishState = FishState.PullingLeft;
				} else {
					currentFishState = FishState.PullingRight;
				}
				num6 = UnityEngine.Random.Range (0, 100);
				if (num6 > 60 && Vector3.Distance (fishingBobber.transform.position, ownerPlayer.transform.position) < MaxCastDistance - 2f) {
					currentFishState |= FishState.PullingBack;
				}
			}
		}
		if ((float)fishCatchDuration > 120f) {
			Server_Cancel (FailReason.TimeOut);
			return;
		}
		bool flag = ownerPlayer.serverInput.IsDown (BUTTON.RIGHT);
		bool flag2 = ownerPlayer.serverInput.IsDown (BUTTON.LEFT);
		bool flag3 = HasReelInInput (ownerPlayer.serverInput);
		if (flag2 && flag) {
			flag2 = (flag = false);
		}
		UpdateFlags (flag2, flag, flag3);
		if (CurrentState == CatchState.Waiting) {
			flag = (flag2 = (flag3 = false));
		}
		if (flag2 && !AllowPullInDirection (-ownerPlayer.eyes.HeadRight (), fishingBobber.transform.position)) {
			flag2 = false;
		}
		if (flag && !AllowPullInDirection (ownerPlayer.eyes.HeadRight (), fishingBobber.transform.position)) {
			flag = false;
		}
		fishingBobber.ServerMovementUpdate (flag2, flag, flag3, ref currentFishState, position, fishableModifier);
		bool flag4 = false;
		float num7 = 0f;
		if (flag3 || flag2 || flag) {
			flag4 = true;
			num7 = 0.5f;
		}
		if (currentFishState != 0 && flag4) {
			if (currentFishState.Contains (FishState.PullingBack) && flag3) {
				num7 = 1.5f;
			} else if ((currentFishState.Contains (FishState.PullingLeft) || currentFishState.Contains (FishState.PullingRight)) && flag3) {
				num7 = 1.2f;
			} else if (currentFishState.Contains (FishState.PullingLeft) && flag) {
				num7 = 0.8f;
			} else if (currentFishState.Contains (FishState.PullingRight) && flag2) {
				num7 = 0.8f;
			}
		}
		if (flag3 && currentFishState != 0) {
			num7 += 1f;
		}
		num7 *= fishableModifier.StrainModifier * GlobalStrainSpeedMultiplier;
		if (flag4) {
			strainTimer += UnityEngine.Time.deltaTime * num7;
		} else {
			strainTimer = Mathf.MoveTowards (strainTimer, 0f, UnityEngine.Time.deltaTime * 1.5f);
		}
		float num8 = strainTimer / 6f;
		SetFlag (Flags.Reserved1, flag4 && num8 > 0.25f);
		if ((float)lastStrainUpdate > 0.4f || fishState != currentFishState) {
			ClientRPC (null, "Client_UpdateFishState", (int)currentFishState, num8);
			lastStrainUpdate = 0f;
		}
		if (strainTimer > 7f || ForceFail) {
			Server_Cancel (FailReason.TensionBreak);
		} else {
			if (!(num2 <= FishCatchDistance) && !ForceSuccess) {
				return;
			}
			CurrentState = CatchState.Caught;
			if (currentFishTarget != null) {
				Item item = ItemManager.Create (currentFishTarget, 1, 0uL);
				ownerPlayer.GiveItem (item, GiveItemReason.Crafted);
				if (currentFishTarget.shortname == "skull.human") {
					item.name = RandomUsernames.Get (UnityEngine.Random.Range (0, 1000));
				}
				if (GameInfo.HasAchievements && !string.IsNullOrEmpty (fishableModifier.SteamStatName)) {
					ownerPlayer.stats.Add (fishableModifier.SteamStatName, 1);
					ownerPlayer.stats.Save (forceSteamSave: true);
					fishLookup.CheckCatchAllAchievement (ownerPlayer);
				}
			}
			Analytics.Server.FishCaught (currentFishTarget);
			ClientRPC (null, "Client_OnCaughtFish", currentFishTarget.itemid);
			ownerPlayer.SignalBroadcast (Signal.Alt_Attack);
			Invoke (ResetLine, 6f);
			fishingBobber.Kill ();
			currentBobber.Set (null);
			CancelInvoke (CatchProcess);
		}
	}

	private void ResetLine ()
	{
		Server_Cancel (FailReason.Success);
	}

	[RPC_Server]
	[RPC_Server.IsActiveItem]
	private void Server_Cancel (RPCMessage msg)
	{
		if (CurrentState != CatchState.Caught) {
			Server_Cancel (FailReason.UserRequested);
		}
	}

	private void Server_Cancel (FailReason reason)
	{
		if (GetItem () != null) {
			GetItem ().LoseCondition ((reason == FailReason.Success) ? ConditionLossOnSuccess : ConditionLossOnFail);
		}
		SetFlag (Flags.Busy, b: false);
		UpdateFlags ();
		CancelInvoke (CatchProcess);
		CurrentState = CatchState.None;
		SetFlag (Flags.Reserved1, b: false);
		FishingBobber fishingBobber = currentBobber.Get (serverside: true);
		if (fishingBobber != null) {
			fishingBobber.Kill ();
			currentBobber.Set (null);
		}
		ClientRPC (null, "Client_ResetLine", (int)reason);
	}

	public override void OnHeldChanged ()
	{
		base.OnHeldChanged ();
		if (CurrentState != 0) {
			Server_Cancel (FailReason.Unequipped);
		}
	}

	public override void Save (SaveInfo info)
	{
		base.Save (info);
		if (currentBobber.IsSet && info.msg.simpleUID == null) {
			info.msg.simpleUID = Facepunch.Pool.Get<SimpleUID> ();
			info.msg.simpleUID.uid = currentBobber.uid;
		}
	}

	private void UpdateFlags (bool inputLeft = false, bool inputRight = false, bool back = false)
	{
		SetFlag (PullingLeftFlag, CurrentState == CatchState.Catching && inputLeft);
		SetFlag (PullingRightFlag, CurrentState == CatchState.Catching && inputRight);
		SetFlag (ReelingInFlag, CurrentState == CatchState.Catching && back);
	}

	public override void Load (LoadInfo info)
	{
		base.Load (info);
		if ((!base.isServer || !info.fromDisk) && info.msg.simpleUID != null) {
			currentBobber.uid = info.msg.simpleUID.uid;
		}
	}

	public override bool BlocksGestures ()
	{
		return CurrentState != CatchState.None;
	}

	private bool AllowPullInDirection (Vector3 worldDirection, Vector3 bobberPosition)
	{
		Vector3 position = base.transform.position;
		Vector3 vector = bobberPosition.WithY (position.y);
		return Vector3.Dot (worldDirection, (vector - position).normalized) < 0f;
	}

	private bool EvaluateFishingPosition (ref Vector3 pos, BasePlayer ply, out FailReason reason, out WaterBody waterBody)
	{
		RaycastHit hitInfo;
		bool flag = GamePhysics.Trace (new Ray (pos + Vector3.up, Vector3.down), 0f, out hitInfo, 1.5f, 16);
		if (flag) {
			waterBody = hitInfo.GetWaterBody ();
			pos.y = hitInfo.point.y;
		} else {
			waterBody = null;
		}
		if (!flag) {
			reason = FailReason.NoWaterFound;
			return false;
		}
		float num = Vector3.Distance (ply.transform.position.WithY (pos.y), pos);
		if (num < 5f) {
			reason = FailReason.TooClose;
			return false;
		}
		if (!GamePhysics.LineOfSight (ply.eyes.position, pos, 1218652417)) {
			reason = FailReason.Obstructed;
			return false;
		}
		Vector3 p = pos + Vector3.up * 2f;
		if (!GamePhysics.LineOfSight (ply.eyes.position, p, 1218652417)) {
			reason = FailReason.Obstructed;
			return false;
		}
		Vector3 position = ply.transform.position;
		position.y = pos.y;
		float num2 = Vector3.Distance (pos, position);
		Vector3 p2 = pos + (position - pos).normalized * (num2 - FishCatchDistance);
		if (!GamePhysics.LineOfSight (pos, p2, 1218652417)) {
			reason = FailReason.Obstructed;
			return false;
		}
		Vector3 pos2 = Vector3.Lerp (pos, ply.transform.position.WithY (pos.y), 0.95f);
		float overallWaterDepth = WaterLevel.GetOverallWaterDepth (pos2, waves: true, volumes: false, null, noEarlyExit: true);
		if (overallWaterDepth < 0.1f && ply.eyes.position.y > 0f) {
			reason = FailReason.TooShallow;
			return false;
		}
		overallWaterDepth = WaterLevel.GetOverallWaterDepth (pos, waves: true, volumes: false, null, noEarlyExit: true);
		if (overallWaterDepth < 0.3f && ply.eyes.position.y > 0f) {
			reason = FailReason.TooShallow;
			return false;
		}
		Vector3 p3 = Vector3.MoveTowards (ply.transform.position.WithY (pos.y), pos, 1f);
		if (!GamePhysics.LineOfSight (ply.eyes.position, p3, 1218652417)) {
			reason = FailReason.Obstructed;
			return false;
		}
		reason = FailReason.Success;
		return true;
	}

	private Item GetCurrentLure ()
	{
		if (GetItem () == null) {
			return null;
		}
		if (GetItem ().contents == null) {
			return null;
		}
		return GetItem ().contents.GetSlot (0);
	}

	private bool HasReelInInput (InputState state)
	{
		return state.IsDown (BUTTON.BACKWARD) || state.IsDown (BUTTON.FIRE_PRIMARY);
	}
}
