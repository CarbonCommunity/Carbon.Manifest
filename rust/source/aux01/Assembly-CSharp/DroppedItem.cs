using System;
using ConVar;
using Facepunch.Rust;
using UnityEngine;

public class DroppedItem : WorldItem
{
	public enum DropReasonEnum
	{
		Unknown,
		Player,
		Death,
		Loot
	}

	[Header ("DroppedItem")]
	public GameObject itemModel;

	private Collider childCollider;

	private Rigidbody rB;

	private const int INTERACTION_ONLY_LAYER = 19;

	[NonSerialized]
	public DropReasonEnum DropReason;

	[NonSerialized]
	public ulong DroppedBy;

	public override float GetNetworkTime ()
	{
		return UnityEngine.Time.fixedTime;
	}

	public override void ServerInit ()
	{
		base.ServerInit ();
		if (GetDespawnDuration () < float.PositiveInfinity) {
			Invoke (IdleDestroy, GetDespawnDuration ());
		}
		ReceiveCollisionMessages (b: true);
	}

	public virtual float GetDespawnDuration ()
	{
		return item?.GetDespawnDuration () ?? Server.itemdespawn;
	}

	public void IdleDestroy ()
	{
		Analytics.Azure.OnItemDespawn (this, item, (int)DropReason, DroppedBy);
		DestroyItem ();
		Kill ();
	}

	public override void OnCollision (Collision collision, BaseEntity hitEntity)
	{
		if (item != null && item.MaxStackable () > 1) {
			DroppedItem droppedItem = hitEntity as DroppedItem;
			if (!(droppedItem == null) && droppedItem.item != null && !(droppedItem.item.info != item.info)) {
				droppedItem.OnDroppedOn (this);
			}
		}
	}

	public void OnDroppedOn (DroppedItem di)
	{
		if (item == null || di.item == null || di.item.info != item.info || (di.item.IsBlueprint () && di.item.blueprintTarget != item.blueprintTarget) || (di.item.hasCondition && di.item.condition != di.item.maxCondition) || (item.hasCondition && item.condition != item.maxCondition)) {
			return;
		}
		if (di.item.info != null) {
			if (di.item.info.amountType == ItemDefinition.AmountType.Genetics) {
				int num = ((di.item.instanceData != null) ? di.item.instanceData.dataInt : (-1));
				int num2 = ((item.instanceData != null) ? item.instanceData.dataInt : (-1));
				if (num != num2) {
					return;
				}
			}
			if ((di.item.info.GetComponent<ItemModSign> () != null && ItemModAssociatedEntity<SignContent>.GetAssociatedEntity (di.item) != null) || (item.info != null && item.info.GetComponent<ItemModSign> () != null && ItemModAssociatedEntity<SignContent>.GetAssociatedEntity (item) != null)) {
				return;
			}
		}
		int num3 = di.item.amount + item.amount;
		if (num3 <= item.MaxStackable () && num3 != 0) {
			if (di.DropReason == DropReasonEnum.Player) {
				DropReason = DropReasonEnum.Player;
			}
			di.DestroyItem ();
			di.Kill ();
			item.amount = num3;
			item.MarkDirty ();
			if (GetDespawnDuration () < float.PositiveInfinity) {
				Invoke (IdleDestroy, GetDespawnDuration ());
			}
			Effect.server.Run ("assets/bundled/prefabs/fx/notice/stack.world.fx.prefab", this, 0u, Vector3.zero, Vector3.zero);
		}
	}

	public override void OnParentChanging (BaseEntity oldParent, BaseEntity newParent)
	{
		base.OnParentChanging (oldParent, newParent);
		if (newParent != null) {
			OnParented ();
		}
		SetCollisionForParent (newParent);
	}

	private void SetCollisionForParent (BaseEntity parent)
	{
		if (!(rB == null)) {
			if (parent.IsValid () && parent.GetComponent<Rigidbody> () != null) {
				rB.collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
			} else {
				rB.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
			}
		}
	}

	internal override void OnParentRemoved ()
	{
		if (rB == null) {
			base.OnParentRemoved ();
			return;
		}
		Vector3 position = base.transform.position;
		Quaternion rotation = base.transform.rotation;
		SetParent (null);
		if (UnityEngine.Physics.Raycast (position + Vector3.up * 2f, Vector3.down, out var hitInfo, 2f, 161546240) && position.y < hitInfo.point.y) {
			position += Vector3.up * 1.5f;
		}
		base.transform.position = position;
		base.transform.rotation = rotation;
		childCollider.gameObject.layer = base.gameObject.layer;
		rB.isKinematic = false;
		rB.useGravity = true;
		rB.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
		rB.WakeUp ();
		if (GetDespawnDuration () < float.PositiveInfinity) {
			Invoke (IdleDestroy, GetDespawnDuration ());
		}
	}

	public void GoKinematic ()
	{
		rB.isKinematic = true;
		if ((bool)childCollider) {
			childCollider.gameObject.layer = 19;
		}
	}

	protected override bool TransformHasMoved ()
	{
		if (base.TransformHasMoved () && !rB.isKinematic) {
			return !rB.IsSleeping ();
		}
		return false;
	}

	public override void PostInitShared ()
	{
		base.PostInitShared ();
		GameObject gameObject = null;
		gameObject = ((item == null || !item.info.worldModelPrefab.isValid) ? UnityEngine.Object.Instantiate (itemModel) : item.info.worldModelPrefab.Instantiate ());
		gameObject.transform.SetParent (base.transform, worldPositionStays: false);
		gameObject.transform.localPosition = Vector3.zero;
		gameObject.transform.localRotation = Quaternion.identity;
		gameObject.SetLayerRecursive (base.gameObject.layer);
		childCollider = gameObject.GetComponentInChildren<Collider> ();
		if ((bool)childCollider) {
			childCollider.enabled = false;
			if (HasParent ()) {
				OnParented ();
			} else {
				childCollider.enabled = true;
			}
		}
		if (base.isServer) {
			WorldModel component = gameObject.GetComponent<WorldModel> ();
			float mass = (component ? component.mass : 1f);
			float drag = 0.1f;
			float angularDrag = 0.1f;
			rB = base.gameObject.AddComponent<Rigidbody> ();
			rB.mass = mass;
			rB.drag = drag;
			rB.angularDrag = angularDrag;
			SetCollisionForParent (GetParentEntity ());
			rB.interpolation = RigidbodyInterpolation.None;
			Renderer[] componentsInChildren = gameObject.GetComponentsInChildren<Renderer> (includeInactive: true);
			for (int i = 0; i < componentsInChildren.Length; i++) {
				componentsInChildren [i].enabled = false;
			}
		}
		if (item != null) {
			PhysicsEffects component2 = base.gameObject.GetComponent<PhysicsEffects> ();
			if (component2 != null) {
				component2.entity = this;
				if (item.info.physImpactSoundDef != null) {
					component2.physImpactSoundDef = item.info.physImpactSoundDef;
				}
			}
		}
		gameObject.SetActive (value: true);
	}

	private void OnParented ()
	{
		if (!(childCollider == null) && (bool)childCollider) {
			childCollider.enabled = false;
			Invoke (EnableCollider, 0.1f);
		}
	}

	private void EnableCollider ()
	{
		if ((bool)childCollider) {
			childCollider.enabled = true;
		}
	}

	public override bool ShouldInheritNetworkGroup ()
	{
		return false;
	}
}
