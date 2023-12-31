using UnityEngine;

public class MovementSounds : MonoBehaviour
{
	public SoundDefinition waterMovementDef;

	public float waterMovementFadeInSpeed = 1f;

	public float waterMovementFadeOutSpeed = 1f;

	public SoundDefinition enterWaterSmall;

	public SoundDefinition enterWaterMedium;

	public SoundDefinition enterWaterLarge;

	private Sound waterMovement;

	private SoundModulation.Modulator waterGainMod;

	public bool inWater = false;

	public float waterLevel = 0f;

	public bool mute = false;
}
