using Facepunch.Rust;
using UnityEngine;

public class ItemModStudyBlueprint : ItemMod
{
	public GameObjectRef studyEffect;

	public override void ServerCommand (Item item, string command, BasePlayer player)
	{
		//IL_01dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e2: Unknown result type (might be due to invalid IL or missing references)
		if ((Object)(object)item.GetOwnerPlayer () != (Object)(object)player) {
			bool flag = false;
			foreach (ItemContainer container in player.inventory.loot.containers) {
				if (item.GetRootContainer () == container) {
					flag = true;
					break;
				}
			}
			if (!flag) {
				return;
			}
		}
		if (!(command == "study") || !item.IsBlueprint ()) {
			return;
		}
		ItemDefinition blueprintTargetDef = item.blueprintTargetDef;
		ItemBlueprint blueprint = blueprintTargetDef.Blueprint;
		bool flag2 = player.blueprints.IsUnlocked (blueprintTargetDef);
		if (flag2 && (Object)(object)blueprint != (Object)null && blueprint.additionalUnlocks != null && blueprint.additionalUnlocks.Count > 0) {
			foreach (ItemDefinition additionalUnlock in blueprint.additionalUnlocks) {
				if (!player.blueprints.IsUnlocked (additionalUnlock)) {
					flag2 = false;
				}
			}
		}
		if ((Object)(object)blueprint != (Object)null && blueprint.defaultBlueprint) {
			flag2 = true;
		}
		if (flag2) {
			return;
		}
		Item item2 = item;
		if (item.amount > 1) {
			item2 = item.SplitItem (1);
		}
		item2.UseItem ();
		player.blueprints.Unlock (blueprintTargetDef);
		Analytics.Azure.OnBlueprintLearned (player, blueprintTargetDef, "blueprint");
		if ((Object)(object)blueprint != (Object)null && blueprint.additionalUnlocks != null && blueprint.additionalUnlocks.Count > 0) {
			foreach (ItemDefinition additionalUnlock2 in blueprint.additionalUnlocks) {
				player.blueprints.Unlock (additionalUnlock2);
				Analytics.Azure.OnBlueprintLearned (player, additionalUnlock2, "blueprint");
			}
		}
		if (studyEffect.isValid) {
			Effect.server.Run (studyEffect.resourcePath, player, StringPool.Get ("head"), Vector3.zero, Vector3.zero);
		}
	}
}
