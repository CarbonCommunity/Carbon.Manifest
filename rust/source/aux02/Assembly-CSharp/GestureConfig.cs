using UnityEngine;

[CreateAssetMenu (menuName = "Rust/Gestures/Gesture Config")]
public class GestureConfig : ScriptableObject
{
	public enum GestureType
	{
		Player,
		NPC,
		Cinematic
	}

	public enum PlayerModelLayer
	{
		UpperBody = 3,
		FullBody
	}

	public enum MovementCapabilities
	{
		FullMovement,
		NoMovement
	}

	public enum AnimationType
	{
		OneShot,
		Loop
	}

	public enum ViewMode
	{
		FirstPerson,
		ThirdPerson
	}

	public enum GestureActionType
	{
		None,
		ShowNameTag,
		DanceAchievement
	}

	[ReadOnly]
	public uint gestureId;

	public string gestureCommand;

	public string convarName;

	public Translate.Phrase gestureName;

	public Sprite icon;

	public int order = 1;

	public float duration = 1.5f;

	public bool canCancel = true;

	[Header ("Player model setup")]
	public PlayerModelLayer playerModelLayer = PlayerModelLayer.UpperBody;

	public GestureType gestureType;

	public bool hideHeldEntity = true;

	public bool canDuckDuringGesture;

	public bool hideInWheel;

	public bool hasViewmodelAnimation = true;

	public MovementCapabilities movementMode;

	public AnimationType animationType;

	public BasePlayer.CameraMode viewMode;

	public bool useRootMotion;

	public bool forceForwardRotation;

	[Header ("Ownership")]
	public GestureActionType actionType;

	public bool forceUnlock;

	public SteamDLCItem dlcItem;

	public SteamInventoryItem inventoryItem;

	public bool IsOwnedBy (BasePlayer player)
	{
		if (forceUnlock) {
			return true;
		}
		if (gestureType == GestureType.NPC) {
			return player.IsNpc;
		}
		if (gestureType == GestureType.Cinematic) {
			return player.IsAdmin;
		}
		if (dlcItem != null && dlcItem.CanUse (player)) {
			return true;
		}
		if (inventoryItem != null && player.blueprints.steamInventory.HasItem (inventoryItem.id)) {
			return true;
		}
		return false;
	}

	public bool CanBeUsedBy (BasePlayer player)
	{
		if (player.isMounted) {
			if (playerModelLayer == PlayerModelLayer.FullBody) {
				return false;
			}
			if (player.GetMounted ().allowedGestures == BaseMountable.MountGestureType.None) {
				return false;
			}
		}
		if (player.IsSwimming () && playerModelLayer == PlayerModelLayer.FullBody) {
			return false;
		}
		if (playerModelLayer == PlayerModelLayer.FullBody && player.modelState.ducked) {
			return false;
		}
		return true;
	}
}
