#define ENABLE_PROFILER
using System.Collections.Generic;
using ConVar;
using Facepunch;
using UnityEngine;
using UnityEngine.Profiling;

public class GroundWatch : BaseMonoBehaviour, IServerComponent
{
	public Vector3 groundPosition = Vector3.zero;

	public LayerMask layers = 161546240;

	public float radius = 0.1f;

	[Header ("Whitelist")]
	public BaseEntity[] whitelist;

	private int fails = 0;

	private void OnDrawGizmosSelected ()
	{
		Gizmos.matrix = base.transform.localToWorldMatrix;
		Gizmos.color = Color.green;
		Gizmos.DrawSphere (groundPosition, radius);
	}

	public static void PhysicsChanged (GameObject obj)
	{
		if (obj == null) {
			return;
		}
		Collider component = obj.GetComponent<Collider> ();
		if (!component) {
			return;
		}
		Bounds bounds = component.bounds;
		List<BaseEntity> obj2 = Facepunch.Pool.GetList<BaseEntity> ();
		Vis.Entities (bounds.center, bounds.extents.magnitude + 1f, obj2, 2263296);
		foreach (BaseEntity item in obj2) {
			if (!item.IsDestroyed && !item.isClient && !(item is BuildingBlock)) {
				Profiler.BeginSample ("BroadcastMessage OnPhysicsNeighbourChanged");
				item.BroadcastMessage ("OnPhysicsNeighbourChanged", SendMessageOptions.DontRequireReceiver);
				Profiler.EndSample ();
			}
		}
		Facepunch.Pool.FreeList (ref obj2);
	}

	public static void PhysicsChanged (Vector3 origin, float radius, int layerMask)
	{
		List<BaseEntity> obj = Facepunch.Pool.GetList<BaseEntity> ();
		Vis.Entities (origin, radius, obj, layerMask);
		foreach (BaseEntity item in obj) {
			if (!item.IsDestroyed && !item.isClient && !(item is BuildingBlock)) {
				Profiler.BeginSample ("BroadcastMessage OnPhysicsNeighbourChanged - Deployables");
				item.BroadcastMessage ("OnPhysicsNeighbourChanged", SendMessageOptions.DontRequireReceiver);
				Profiler.EndSample ();
			}
		}
		Facepunch.Pool.FreeList (ref obj);
	}

	private void OnPhysicsNeighbourChanged ()
	{
		if (!OnGround ()) {
			fails++;
			if (fails >= ConVar.Physics.groundwatchfails) {
				BaseEntity baseEntity = base.gameObject.ToBaseEntity ();
				if ((bool)baseEntity) {
					baseEntity.transform.BroadcastMessage ("OnGroundMissing", SendMessageOptions.DontRequireReceiver);
				}
			} else {
				if (ConVar.Physics.groundwatchdebug) {
					Debug.Log ("GroundWatch retry: " + fails);
				}
				Invoke (OnPhysicsNeighbourChanged, ConVar.Physics.groundwatchdelay);
			}
		} else {
			fails = 0;
		}
	}

	private bool OnGround ()
	{
		BaseEntity component = GetComponent<BaseEntity> ();
		if ((bool)component) {
			if (component.HasParent ()) {
				return true;
			}
			Construction construction = PrefabAttribute.server.Find<Construction> (component.prefabID);
			if ((bool)construction) {
				Socket_Base[] allSockets = construction.allSockets;
				foreach (Socket_Base socket_Base in allSockets) {
					SocketMod[] socketMods = socket_Base.socketMods;
					foreach (SocketMod socketMod in socketMods) {
						SocketMod_AreaCheck socketMod_AreaCheck = socketMod as SocketMod_AreaCheck;
						if ((bool)socketMod_AreaCheck && socketMod_AreaCheck.wantsInside && !socketMod_AreaCheck.DoCheck (component.transform.position, component.transform.rotation, component)) {
							if (ConVar.Physics.groundwatchdebug) {
								Debug.Log ("GroundWatch failed: " + socketMod_AreaCheck.hierachyName);
							}
							return false;
						}
					}
				}
			}
		}
		List<Collider> obj = Facepunch.Pool.GetList<Collider> ();
		Vis.Colliders (base.transform.TransformPoint (groundPosition), radius, obj, layers);
		foreach (Collider item in obj) {
			BaseEntity baseEntity = item.gameObject.ToBaseEntity ();
			if ((bool)baseEntity && (baseEntity == component || baseEntity.IsDestroyed || baseEntity.isClient)) {
				continue;
			}
			if (whitelist != null && whitelist.Length != 0) {
				bool flag = false;
				BaseEntity[] array = whitelist;
				foreach (BaseEntity baseEntity2 in array) {
					if (baseEntity.prefabID == baseEntity2.prefabID) {
						flag = true;
						break;
					}
				}
				if (!flag) {
					continue;
				}
			}
			DecayEntity decayEntity = component as DecayEntity;
			DecayEntity decayEntity2 = baseEntity as DecayEntity;
			if ((bool)decayEntity && decayEntity.buildingID != 0 && (bool)decayEntity2 && decayEntity2.buildingID != 0 && decayEntity.buildingID != decayEntity2.buildingID) {
				continue;
			}
			Facepunch.Pool.FreeList (ref obj);
			return true;
		}
		if (ConVar.Physics.groundwatchdebug) {
			Debug.Log ("GroundWatch failed: Legacy radius check");
		}
		Facepunch.Pool.FreeList (ref obj);
		return false;
	}
}
