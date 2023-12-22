#define ENABLE_PROFILER
using UnityEngine;
using UnityEngine.Profiling;

public class EntityCollisionMessage : EntityComponent<BaseEntity>
{
	private void OnCollisionEnter (Collision collision)
	{
		if (base.baseEntity == null || base.baseEntity.IsDestroyed) {
			return;
		}
		Profiler.BeginSample ("GetEntity");
		BaseEntity baseEntity = collision.GetEntity ();
		Profiler.EndSample ();
		if (baseEntity == base.baseEntity) {
			return;
		}
		if (baseEntity != null) {
			if (baseEntity.IsDestroyed) {
				return;
			}
			if (base.baseEntity.isServer) {
				baseEntity = baseEntity.ToServer<BaseEntity> ();
			}
		}
		Profiler.BeginSample ("baseEntity.OnCollision");
		base.baseEntity.OnCollision (collision, baseEntity);
		Profiler.EndSample ();
	}
}
