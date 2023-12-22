using UnityEngine;

[CreateAssetMenu (menuName = "Rust/Missions/OBJECTIVES/MountMissionEntity")]
public class MissionObjective_MountMissionEntity : MissionObjective
{
	public string targetIdentifier;

	public bool shouldUpdateMissionLocation = true;

	public override void ObjectiveStarted (BasePlayer playerFor, int index, BaseMission.MissionInstance instance)
	{
		base.ObjectiveStarted (playerFor, index, instance);
		instance.GetMissionEntity (targetIdentifier, playerFor);
	}

	public override void ProcessMissionEvent (BasePlayer playerFor, BaseMission.MissionInstance instance, int index, BaseMission.MissionEventType type, BaseMission.MissionEventPayload payload, float amount)
	{
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		base.ProcessMissionEvent (playerFor, instance, index, type, payload, amount);
		if (type != BaseMission.MissionEventType.MOUNT_ENTITY || IsCompleted (index, instance) || !CanProgress (index, instance)) {
			return;
		}
		MissionEntity missionEntity = instance.GetMissionEntity (targetIdentifier, playerFor);
		if ((Object)(object)missionEntity == (Object)null) {
			FailObjective (index, instance, playerFor);
			return;
		}
		BaseEntity entity = missionEntity.GetEntity ();
		if (!entity.IsValid ()) {
			FailObjective (index, instance, playerFor);
			return;
		}
		EntityRef<BaseMountable> entityRef = default(EntityRef<BaseMountable>);
		entityRef.uid = payload.NetworkIdentifier;
		EntityRef<BaseMountable> entityRef2 = entityRef;
		BaseMountable baseMountable = entityRef2.Get (serverside: true);
		if (baseMountable.IsValid ()) {
			BaseVehicle baseVehicle = baseMountable.VehicleParent ();
			if (baseMountable.EqualNetID ((BaseNetworkable)entity) || ((Object)(object)baseVehicle != (Object)null && baseVehicle.EqualNetID ((BaseNetworkable)entity))) {
				CompleteObjective (index, instance, playerFor);
			}
		}
	}

	public override void Think (int index, BaseMission.MissionInstance instance, BasePlayer assignee, float delta)
	{
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		base.Think (index, instance, assignee, delta);
		if (!IsStarted (index, instance) || IsCompleted (index, instance)) {
			return;
		}
		ref RealTimeSince sinceLastThink = ref instance.objectiveStatuses [index].sinceLastThink;
		if (RealTimeSince.op_Implicit (sinceLastThink) < 1f) {
			return;
		}
		sinceLastThink = RealTimeSince.op_Implicit (0f);
		MissionEntity missionEntity = instance.GetMissionEntity (targetIdentifier, assignee);
		if ((Object)(object)missionEntity == (Object)null) {
			FailObjective (index, instance, assignee);
		} else if (shouldUpdateMissionLocation) {
			Vector3 position = ((Component)missionEntity).transform.position;
			if (position != instance.missionLocation) {
				instance.missionLocation = position;
				assignee.MissionDirty ();
			}
		}
	}
}
