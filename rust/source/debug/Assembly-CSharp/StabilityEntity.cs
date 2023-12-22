using System;
using System.Collections.Generic;
using ConVar;
using Facepunch;
using ProtoBuf;
using Rust;
using UnityEngine;
using UnityEngine.Profiling;

public class StabilityEntity : DecayEntity
{
	public class StabilityCheckWorkQueue : ObjectWorkQueue<StabilityEntity>
	{
		protected override void RunJob (StabilityEntity entity)
		{
			if (((ObjectWorkQueue<StabilityEntity>)this).ShouldAdd (entity)) {
				Profiler.BeginSample ("StabilityCheck");
				entity.StabilityCheck ();
				Profiler.EndSample ();
			}
		}

		protected override bool ShouldAdd (StabilityEntity entity)
		{
			if (!ConVar.Server.stability) {
				return false;
			}
			if (!entity.IsValid ()) {
				return false;
			}
			if (!entity.isServer) {
				return false;
			}
			return true;
		}
	}

	public class UpdateSurroundingsQueue : ObjectWorkQueue<Bounds>
	{
		protected override void RunJob (Bounds bounds)
		{
			//IL_001a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0021: Unknown result type (might be due to invalid IL or missing references)
			//IL_0026: Unknown result type (might be due to invalid IL or missing references)
			if (!ConVar.Server.stability) {
				return;
			}
			List<BaseEntity> list = Pool.GetList<BaseEntity> ();
			Vector3 center = ((Bounds)(ref bounds)).center;
			Vector3 extents = ((Bounds)(ref bounds)).extents;
			Vis.Entities (center, ((Vector3)(ref extents)).magnitude + 1f, list, 69372162, (QueryTriggerInteraction)2);
			foreach (BaseEntity item in list) {
				if (!item.IsDestroyed && !item.isClient) {
					if (item is StabilityEntity) {
						Profiler.BeginSample ("StabilityEntity.OnPhysicsNeighbourChanged");
						(item as StabilityEntity).OnPhysicsNeighbourChanged ();
						Profiler.EndSample ();
					} else {
						Profiler.BeginSample ("BroadcastMessage OnPhysicsNeighbourChanged");
						((Component)item).BroadcastMessage ("OnPhysicsNeighbourChanged", (SendMessageOptions)1);
						Profiler.EndSample ();
					}
				}
			}
			Pool.FreeList<BaseEntity> (ref list);
		}
	}

	private class Support
	{
		public StabilityEntity parent = null;

		public EntityLink link = null;

		public float factor = 1f;

		public Support (StabilityEntity parent, EntityLink link, float factor)
		{
			this.parent = parent;
			this.link = link;
			this.factor = factor;
		}

		public StabilityEntity SupportEntity (StabilityEntity ignoreEntity = null)
		{
			StabilityEntity stabilityEntity = null;
			for (int i = 0; i < link.connections.Count; i++) {
				StabilityEntity stabilityEntity2 = link.connections [i].owner as StabilityEntity;
				Socket_Base socket = link.connections [i].socket;
				if (!((Object)(object)stabilityEntity2 == (Object)null) && !((Object)(object)stabilityEntity2 == (Object)(object)parent) && !((Object)(object)stabilityEntity2 == (Object)(object)ignoreEntity) && !stabilityEntity2.isClient && !stabilityEntity2.IsDestroyed && !(socket is ConstructionSocket { femaleNoStability: not false }) && ((Object)(object)stabilityEntity == (Object)null || stabilityEntity2.cachedDistanceFromGround < stabilityEntity.cachedDistanceFromGround)) {
					stabilityEntity = stabilityEntity2;
				}
			}
			return stabilityEntity;
		}
	}

	public static StabilityCheckWorkQueue stabilityCheckQueue = new StabilityCheckWorkQueue ();

	public static UpdateSurroundingsQueue updateSurroundingsQueue = new UpdateSurroundingsQueue ();

	public bool grounded = false;

	[NonSerialized]
	public float cachedStability = 0f;

	[NonSerialized]
	public int cachedDistanceFromGround = int.MaxValue;

	private List<Support> supports = null;

	private int stabilityStrikes = 0;

	private bool dirty = false;

	public override void Save (SaveInfo info)
	{
		base.Save (info);
		Profiler.BeginSample ("StabilityEntity.Save");
		info.msg.stabilityEntity = Pool.Get<StabilityEntity> ();
		info.msg.stabilityEntity.stability = cachedStability;
		info.msg.stabilityEntity.distanceFromGround = cachedDistanceFromGround;
		Profiler.EndSample ();
	}

	public override void Load (LoadInfo info)
	{
		base.Load (info);
		if (info.msg.stabilityEntity != null) {
			cachedStability = info.msg.stabilityEntity.stability;
			cachedDistanceFromGround = info.msg.stabilityEntity.distanceFromGround;
			if (cachedStability <= 0f) {
				cachedStability = 0f;
			}
			if (cachedDistanceFromGround <= 0) {
				cachedDistanceFromGround = int.MaxValue;
			}
		}
	}

	public override void ResetState ()
	{
		base.ResetState ();
		cachedStability = 0f;
		cachedDistanceFromGround = int.MaxValue;
		if (base.isServer) {
			supports = null;
			stabilityStrikes = 0;
			dirty = false;
		}
	}

	public void InitializeSupports ()
	{
		supports = new List<Support> ();
		if (grounded || HasParent ()) {
			return;
		}
		Profiler.BeginSample ("InitializeSupports");
		List<EntityLink> entityLinks = GetEntityLinks ();
		for (int i = 0; i < entityLinks.Count; i++) {
			EntityLink entityLink = entityLinks [i];
			if (entityLink.IsMale ()) {
				if (entityLink.socket is StabilitySocket) {
					supports.Add (new Support (this, entityLink, (entityLink.socket as StabilitySocket).support));
				}
				if (entityLink.socket is ConstructionSocket) {
					supports.Add (new Support (this, entityLink, (entityLink.socket as ConstructionSocket).support));
				}
			}
		}
		Profiler.EndSample ();
	}

	public int DistanceFromGround (StabilityEntity ignoreEntity = null)
	{
		if (grounded || HasParent ()) {
			return 1;
		}
		if (supports == null) {
			return 1;
		}
		if ((Object)(object)ignoreEntity == (Object)null) {
			ignoreEntity = this;
		}
		int num = int.MaxValue;
		for (int i = 0; i < supports.Count; i++) {
			Support support = supports [i];
			StabilityEntity stabilityEntity = support.SupportEntity (ignoreEntity);
			if (!((Object)(object)stabilityEntity == (Object)null)) {
				int num2 = stabilityEntity.CachedDistanceFromGround (ignoreEntity);
				if (num2 != int.MaxValue) {
					num = Mathf.Min (num, num2 + 1);
				}
			}
		}
		return num;
	}

	public float SupportValue (StabilityEntity ignoreEntity = null)
	{
		if (grounded || HasParent ()) {
			return 1f;
		}
		if (supports == null) {
			return 1f;
		}
		if ((Object)(object)ignoreEntity == (Object)null) {
			ignoreEntity = this;
		}
		float num = 0f;
		for (int i = 0; i < supports.Count; i++) {
			Support support = supports [i];
			StabilityEntity stabilityEntity = support.SupportEntity (ignoreEntity);
			if (!((Object)(object)stabilityEntity == (Object)null)) {
				float num2 = stabilityEntity.CachedSupportValue (ignoreEntity);
				if (num2 != 0f) {
					num += num2 * support.factor;
				}
			}
		}
		return Mathf.Clamp01 (num);
	}

	public int CachedDistanceFromGround (StabilityEntity ignoreEntity = null)
	{
		if (grounded || HasParent ()) {
			return 1;
		}
		if (supports == null) {
			return 1;
		}
		if ((Object)(object)ignoreEntity == (Object)null) {
			ignoreEntity = this;
		}
		int num = int.MaxValue;
		for (int i = 0; i < supports.Count; i++) {
			Support support = supports [i];
			StabilityEntity stabilityEntity = support.SupportEntity (ignoreEntity);
			if (!((Object)(object)stabilityEntity == (Object)null)) {
				int num2 = stabilityEntity.cachedDistanceFromGround;
				if (num2 != int.MaxValue) {
					num = Mathf.Min (num, num2 + 1);
				}
			}
		}
		return num;
	}

	public float CachedSupportValue (StabilityEntity ignoreEntity = null)
	{
		if (grounded || HasParent ()) {
			return 1f;
		}
		if (supports == null) {
			return 1f;
		}
		if ((Object)(object)ignoreEntity == (Object)null) {
			ignoreEntity = this;
		}
		float num = 0f;
		for (int i = 0; i < supports.Count; i++) {
			Support support = supports [i];
			StabilityEntity stabilityEntity = support.SupportEntity (ignoreEntity);
			if (!((Object)(object)stabilityEntity == (Object)null)) {
				float num2 = stabilityEntity.cachedStability;
				if (num2 != 0f) {
					num += num2 * support.factor;
				}
			}
		}
		return Mathf.Clamp01 (num);
	}

	public void StabilityCheck ()
	{
		if (base.IsDestroyed) {
			return;
		}
		if (supports == null) {
			InitializeSupports ();
		}
		bool flag = false;
		Profiler.BeginSample ("DistanceFromGround");
		int num = DistanceFromGround ();
		Profiler.EndSample ();
		if (num != cachedDistanceFromGround) {
			cachedDistanceFromGround = num;
			flag = true;
		}
		Profiler.BeginSample ("SupportValue");
		float num2 = SupportValue ();
		Profiler.EndSample ();
		if (Mathf.Abs (cachedStability - num2) > Stability.accuracy) {
			cachedStability = num2;
			flag = true;
		}
		if (flag) {
			dirty = true;
			Profiler.BeginSample ("CacheInvalidate");
			UpdateConnectedEntities ();
			UpdateStability ();
			Profiler.EndSample ();
		} else if (dirty) {
			dirty = false;
			SendNetworkUpdate ();
		}
		if (num2 < Stability.collapse) {
			if (stabilityStrikes < Stability.strikes) {
				Profiler.BeginSample ("StabilityStrike");
				UpdateStability ();
				stabilityStrikes++;
				Profiler.EndSample ();
			} else {
				Profiler.BeginSample ("Kill");
				Kill (DestroyMode.Gib);
				Profiler.EndSample ();
			}
		} else {
			stabilityStrikes = 0;
		}
	}

	public void UpdateStability ()
	{
		((ObjectWorkQueue<StabilityEntity>)stabilityCheckQueue).Add (this);
	}

	public void UpdateSurroundingEntities ()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		UpdateSurroundingsQueue obj = updateSurroundingsQueue;
		OBB val = WorldSpaceBounds ();
		((ObjectWorkQueue<Bounds>)obj).Add (((OBB)(ref val)).ToBounds ());
	}

	public void UpdateConnectedEntities ()
	{
		List<EntityLink> entityLinks = GetEntityLinks ();
		for (int i = 0; i < entityLinks.Count; i++) {
			EntityLink entityLink = entityLinks [i];
			if (!entityLink.IsFemale ()) {
				continue;
			}
			for (int j = 0; j < entityLink.connections.Count; j++) {
				StabilityEntity stabilityEntity = entityLink.connections [j].owner as StabilityEntity;
				if (!((Object)(object)stabilityEntity == (Object)null) && !stabilityEntity.isClient && !stabilityEntity.IsDestroyed) {
					stabilityEntity.UpdateStability ();
				}
			}
		}
	}

	protected void OnPhysicsNeighbourChanged ()
	{
		if (!base.IsDestroyed) {
			StabilityCheck ();
		}
	}

	protected void DebugNudge ()
	{
		StabilityCheck ();
	}

	public override void ServerInit ()
	{
		base.ServerInit ();
		if (!Application.isLoadingSave) {
			UpdateStability ();
		}
	}

	internal override void DoServerDestroy ()
	{
		base.DoServerDestroy ();
		UpdateSurroundingEntities ();
	}
}
