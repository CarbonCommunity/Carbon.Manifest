using System.Collections.Generic;
using Facepunch;
using UnityEngine;

[CreateAssetMenu (menuName = "Rust/Missions/OBJECTIVES/Deploy")]
public class MissionObjective_DeployItem : MissionObjective
{
	public BaseEntity[] PossibleOptions = new BaseEntity[0];

	public int RequiredAmount = 1;

	public bool PingTutorialTargets;

	private const BasePlayer.PingType PingType = BasePlayer.PingType.Build;

	public override void MissionStarted (int index, BaseMission.MissionInstance instance, BasePlayer forPlayer)
	{
		base.MissionStarted (index, instance, forPlayer);
		instance.objectiveStatuses [index].progressCurrent = 0f;
		instance.objectiveStatuses [index].progressTarget = RequiredAmount;
	}

	public override void ProcessMissionEvent (BasePlayer playerFor, BaseMission.MissionInstance instance, int index, BaseMission.MissionEventType type, BaseMission.MissionEventPayload payload, float amount)
	{
		if (IsCompleted (index, instance) || !CanProgress (index, instance)) {
			return;
		}
		base.ProcessMissionEvent (playerFor, instance, index, type, payload, amount);
		if (type != BaseMission.MissionEventType.DEPLOY) {
			return;
		}
		BaseEntity[] possibleOptions = PossibleOptions;
		for (int i = 0; i < possibleOptions.Length; i++) {
			if (possibleOptions [i].prefabID == payload.UintIdentifier) {
				instance.objectiveStatuses [index].progressCurrent += amount;
				if (PingTutorialTargets) {
					UpdatePings (playerFor);
				}
				if (instance.objectiveStatuses [index].progressCurrent >= instance.objectiveStatuses [index].progressTarget) {
					CompleteObjective (index, instance, playerFor);
				}
				playerFor.MissionDirty ();
				break;
			}
		}
	}

	public override void ObjectiveStarted (BasePlayer playerFor, int index, BaseMission.MissionInstance instance)
	{
		base.ObjectiveStarted (playerFor, index, instance);
		if (PingTutorialTargets) {
			UpdatePings (playerFor);
		}
	}

	private void UpdatePings (BasePlayer playerFor)
	{
		TutorialIsland currentTutorialIsland = playerFor.GetCurrentTutorialIsland ();
		if (!(currentTutorialIsland != null)) {
			return;
		}
		List<TutorialBuildTarget> obj = Pool.GetList<TutorialBuildTarget> ();
		BaseEntity[] possibleOptions = PossibleOptions;
		foreach (BaseEntity baseEntity in possibleOptions) {
			currentTutorialIsland.GetBuildTargets (obj, baseEntity.prefabID);
		}
		foreach (TutorialBuildTarget item in obj) {
			item.UpdateActive (playerFor);
			if (item.gameObject.activeSelf) {
				playerFor.AddPingAtLocation (BasePlayer.PingType.Build, item.transform.position, 86400f, currentTutorialIsland.net.ID);
			} else {
				playerFor.RemovePingAtLocation (BasePlayer.PingType.Build, item.transform.position, 0.5f, currentTutorialIsland.net.ID);
			}
		}
		Pool.FreeList (ref obj);
	}
}
