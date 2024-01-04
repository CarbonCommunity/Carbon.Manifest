using UnityEngine;

[CreateAssetMenu (menuName = "Rust/Missions/OBJECTIVES/Upgrade Building Block")]
public class MissionObjective_UpgradeBuildingBlock : MissionObjective
{
	public bool ShouldPingBlocksLesstThanTargetGrade;

	public BasePlayer.PingType PingType;

	public BuildingGrade.Enum TargetGrade;

	public int RequiredCount = 6;

	public override void MissionStarted (int index, BaseMission.MissionInstance instance, BasePlayer forPlayer)
	{
		base.MissionStarted (index, instance, forPlayer);
		instance.objectiveStatuses [index].progressCurrent = 0f;
		instance.objectiveStatuses [index].progressTarget = RequiredCount;
	}

	public override void ProcessMissionEvent (BasePlayer playerFor, BaseMission.MissionInstance instance, int index, BaseMission.MissionEventType type, BaseMission.MissionEventPayload payload, float amount)
	{
		base.ProcessMissionEvent (playerFor, instance, index, type, payload, amount);
		if (type == BaseMission.MissionEventType.UPGRADE_BUILDING_GRADE && !IsCompleted (index, instance) && CanProgress (index, instance) && payload.IntIdentifier >= (int)TargetGrade) {
			instance.objectiveStatuses [index].progressCurrent += 1f;
			if (instance.objectiveStatuses [index].progressCurrent >= (float)RequiredCount) {
				CompleteObjective (index, instance, playerFor);
			}
			playerFor.MissionDirty ();
		}
	}
}
