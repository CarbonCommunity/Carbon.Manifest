using Rust;
using UnityEngine;

[RequireComponent (typeof(Collider))]
public class TriggerParent : TriggerBase, IServerComponent
{
	[Tooltip ("Deparent if the parented entity clips into an obstacle")]
	[SerializeField]
	private bool doClippingCheck;

	[Tooltip ("If deparenting via clipping, this will be used (if assigned) to also move the entity to a valid dismount position")]
	public BaseMountable associatedMountable;

	[Tooltip ("Needed if the player might dismount inside the trigger and the trigger might be moving. Being mounting inside the trigger lets them dismount in local trigger-space, which means client and server will sync up.Otherwise the client/server delay can have them dismounting into invalid space.")]
	public bool parentMountedPlayers;

	[Tooltip ("Sleepers don't have all the checks (e.g. clipping) that awake players get. If that might be a problem,sleeper parenting can be disabled. You'll need an associatedMountable though so that the sleeper can be dismounted.")]
	public bool parentSleepers = true;

	public bool ParentNPCPlayers;

	[Tooltip ("If the player is already parented to something else, they'll switch over to another parent only if this is true")]
	public bool overrideOtherTriggers;

	[Tooltip ("Requires associatedMountable to be set. Prevents players entering the trigger if there's something between their feet and the bottom of the parent trigger")]
	public bool checkForObjUnderFeet;

	public const int CLIP_CHECK_MASK = 1218511105;

	protected Collider triggerCollider;

	protected float triggerHeight;

	private BasePlayer killPlayerTemp;

	internal override GameObject InterestedInObject (GameObject obj)
	{
		obj = base.InterestedInObject (obj);
		if (obj == null) {
			return null;
		}
		BaseEntity baseEntity = obj.ToBaseEntity ();
		if (baseEntity == null) {
			return null;
		}
		if (baseEntity.isClient) {
			return null;
		}
		return baseEntity.gameObject;
	}

	internal override void OnEntityEnter (BaseEntity ent)
	{
		if (!(ent is NPCPlayer) || ParentNPCPlayers) {
			if (ShouldParent (ent)) {
				Parent (ent);
			}
			base.OnEntityEnter (ent);
			if (entityContents != null && entityContents.Count == 1) {
				InvokeRepeating (OnTick, 0f, 0f);
			}
		}
	}

	internal override void OnEntityLeave (BaseEntity ent)
	{
		base.OnEntityLeave (ent);
		if (entityContents == null || entityContents.Count == 0) {
			CancelInvoke (OnTick);
		}
		BasePlayer basePlayer = ent.ToPlayer ();
		if (!parentSleepers || !(basePlayer != null) || !basePlayer.IsSleeping ()) {
			Unparent (ent);
		}
	}

	public virtual bool ShouldParent (BaseEntity ent, bool bypassOtherTriggerCheck = false)
	{
		if (!ent.canTriggerParent) {
			return false;
		}
		if (!bypassOtherTriggerCheck && !overrideOtherTriggers) {
			BaseEntity parentEntity = ent.GetParentEntity ();
			if (parentEntity.IsValid () && parentEntity != base.gameObject.ToBaseEntity ()) {
				return false;
			}
		}
		if (ent.FindTrigger<TriggerParentExclusion> () != null) {
			return false;
		}
		if (doClippingCheck && IsClipping (ent) && !(ent is BaseCorpse)) {
			return false;
		}
		if (checkForObjUnderFeet && HasObjUnderFeet (ent)) {
			return false;
		}
		BasePlayer basePlayer = ent.ToPlayer ();
		if (basePlayer != null) {
			if (basePlayer.IsSwimming ()) {
				return false;
			}
			if (!parentMountedPlayers && basePlayer.isMounted) {
				return false;
			}
			if (!parentSleepers && basePlayer.IsSleeping ()) {
				return false;
			}
		}
		return true;
	}

	protected void Parent (BaseEntity ent)
	{
		BaseEntity baseEntity = base.gameObject.ToBaseEntity ();
		if (!(ent.GetParentEntity () == baseEntity) && !(baseEntity.GetParentEntity () == ent)) {
			ent.SetParent (base.gameObject.ToBaseEntity (), worldPositionStays: true, sendImmediate: true);
		}
	}

	protected void Unparent (BaseEntity ent)
	{
		if (ent.GetParentEntity () != base.gameObject.ToBaseEntity ()) {
			return;
		}
		if (ent.IsValid () && !ent.IsDestroyed) {
			TriggerParent triggerParent = ent.FindSuitableParent ();
			if (triggerParent != null && triggerParent.gameObject.ToBaseEntity ().IsValid ()) {
				triggerParent.Parent (ent);
				return;
			}
		}
		ent.SetParent (null, worldPositionStays: true, sendImmediate: true);
		BasePlayer basePlayer = ent.ToPlayer ();
		if (!(basePlayer != null)) {
			return;
		}
		basePlayer.PauseFlyHackDetection (5f);
		basePlayer.PauseSpeedHackDetection (5f);
		basePlayer.PauseVehicleNoClipDetection (5f);
		if (associatedMountable != null && ((doClippingCheck && IsClipping (ent)) || basePlayer.IsSleeping ())) {
			if (associatedMountable.GetDismountPosition (basePlayer, out var res)) {
				basePlayer.MovePosition (res);
				basePlayer.SendNetworkUpdateImmediate ();
				basePlayer.ClientRPCPlayer (null, basePlayer, "ForcePositionTo", res);
			} else {
				killPlayerTemp = basePlayer;
				Invoke (KillPlayerDelayed, 0f);
			}
		}
	}

	private void KillPlayerDelayed ()
	{
		if (killPlayerTemp.IsValid () && !killPlayerTemp.IsDead ()) {
			killPlayerTemp.Hurt (1000f, DamageType.Suicide, killPlayerTemp, useProtection: false);
		}
		killPlayerTemp = null;
	}

	private void OnTick ()
	{
		if (entityContents == null) {
			return;
		}
		BaseEntity baseEntity = base.gameObject.ToBaseEntity ();
		if (!baseEntity.IsValid () || baseEntity.IsDestroyed) {
			return;
		}
		foreach (BaseEntity entityContent in entityContents) {
			if (entityContent.IsValid () && !entityContent.IsDestroyed) {
				if (ShouldParent (entityContent)) {
					Parent (entityContent);
				} else {
					Unparent (entityContent);
				}
			}
		}
	}

	protected virtual bool IsClipping (BaseEntity ent)
	{
		return GamePhysics.CheckOBB (ent.WorldSpaceBounds (), 1218511105, QueryTriggerInteraction.Ignore);
	}

	private bool HasObjUnderFeet (BaseEntity ent)
	{
		Vector3 origin = ent.PivotPoint () + ent.transform.up * 0.1f;
		float maxDistance = triggerHeight + 0.1f;
		if (GamePhysics.Trace (new Ray (origin, -base.transform.up), 0f, out var hitInfo, maxDistance, 429990145, QueryTriggerInteraction.Ignore, ent) && hitInfo.collider != null) {
			BaseEntity toFind = base.gameObject.ToBaseEntity ();
			BaseEntity baseEntity = hitInfo.collider.ToBaseEntity ();
			if (baseEntity == null || !baseEntity.HasEntityInParents (toFind)) {
				return true;
			}
		}
		return false;
	}
}
