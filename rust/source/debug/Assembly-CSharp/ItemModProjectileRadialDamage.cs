using System.Collections.Generic;
using Facepunch;
using Rust;
using UnityEngine;

public class ItemModProjectileRadialDamage : ItemModProjectileMod
{
	public float radius = 0.5f;

	public DamageTypeEntry damage;

	public GameObjectRef effect;

	public bool ignoreHitObject = true;

	public override void ServerProjectileHit (HitInfo info)
	{
		if (effect.isValid) {
			Effect.server.Run (effect.resourcePath, info.HitPositionWorld, info.HitNormalWorld);
		}
		List<BaseCombatEntity> obj = Pool.GetList<BaseCombatEntity> ();
		List<BaseCombatEntity> obj2 = Pool.GetList<BaseCombatEntity> ();
		Vis.Entities (info.HitPositionWorld, radius, obj2, 1237003025);
		foreach (BaseCombatEntity item in obj2) {
			if (!item.isServer || obj.Contains (item) || (item == info.HitEntity && ignoreHitObject)) {
				continue;
			}
			Vector3 vector = item.CenterPoint ();
			Vector3 vector2 = item.ClosestPoint (info.HitPositionWorld);
			float num = Vector3.Distance (vector2, info.HitPositionWorld);
			float num2 = num / radius;
			if (!(num2 > 1f)) {
				float num3 = 1f - num2;
				if (item.IsVisibleAndCanSee (info.HitPositionWorld - info.ProjectileVelocity.normalized * 0.1f) && item.IsVisibleAndCanSee (info.HitPositionWorld - (vector2 - info.HitPositionWorld).normalized * 0.1f)) {
					obj.Add (item);
					item.OnAttacked (new HitInfo (info.Initiator, item, damage.type, damage.amount * num3));
				}
			}
		}
		Pool.FreeList (ref obj);
		Pool.FreeList (ref obj2);
	}
}
