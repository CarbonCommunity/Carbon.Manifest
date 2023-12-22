using UnityEngine;

[CreateAssetMenu (menuName = "Rust/Missions/OBJECTIVES/Move")]
public class MissionObjective_Move : MissionObjective
{
	public string positionName = "default";

	public float distForCompletion = 3f;

	public bool use2D = false;

	public override void ObjectiveStarted (BasePlayer playerFor, int index, BaseMission.MissionInstance instance)
	{
		base.ObjectiveStarted (playerFor, index, instance);
		instance.missionLocation = instance.GetMissionPoint (positionName, playerFor);
		playerFor.MissionDirty ();
	}

	public override void Think (int index, BaseMission.MissionInstance instance, BasePlayer assignee, float delta)
	{
		base.Think (index, instance, assignee, delta);
		if (ShouldThink (index, instance)) {
			Vector3 missionPoint = instance.GetMissionPoint (positionName, assignee);
			float num = (use2D ? Vector3Ex.Distance2D (missionPoint, assignee.transform.position) : Vector3.Distance (missionPoint, assignee.transform.position));
			if (num <= distForCompletion) {
				CompleteObjective (index, instance, assignee);
				assignee.MissionDirty ();
			}
		}
	}
}
