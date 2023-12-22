using UnityEngine;

public class ItemModConversation : ItemMod
{
	public ConversationData conversationData;

	public GameObjectRef conversationEntity;

	public GameObjectRef squakEffect;

	public override void ServerCommand (Item item, string command, BasePlayer player)
	{
		if (command == "squak") {
			if (squakEffect.isValid) {
				Effect.server.Run (squakEffect.resourcePath, player.eyes.position);
			}
			Debug.Log ("Starting conversation");
			BaseEntity baseEntity = GameManager.server.CreateEntity (conversationEntity.resourcePath, player.transform.position + Vector3.up * -2f);
			baseEntity.GetComponent<NPCMissionProvider> ().conversations [0] = conversationData;
			baseEntity.Spawn ();
			baseEntity.Invoke ("Kill", 600f);
		}
	}
}
