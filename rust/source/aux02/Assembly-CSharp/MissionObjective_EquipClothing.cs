using UnityEngine;

[CreateAssetMenu (menuName = "Rust/Missions/OBJECTIVES/Equip Clothing")]
public class MissionObjective_EquipClothing : MissionObjective
{
	public ItemDefinition[] RequiredItems = new ItemDefinition[0];

	public override void ProcessMissionEvent (BasePlayer playerFor, BaseMission.MissionInstance instance, int index, BaseMission.MissionEventType type, BaseMission.MissionEventPayload payload, float amount)
	{
		base.ProcessMissionEvent (playerFor, instance, index, type, payload, amount);
		if (IsCompleted (index, instance) || !CanProgress (index, instance) || type != BaseMission.MissionEventType.CLOTHINGCHANGED) {
			return;
		}
		ItemDefinition[] requiredItems = RequiredItems;
		foreach (ItemDefinition searchFor in requiredItems) {
			if (!playerFor.inventory.containerWear.HasItem (searchFor)) {
				return;
			}
		}
		CompleteObjective (index, instance, playerFor);
		playerFor.MissionDirty ();
	}
}
