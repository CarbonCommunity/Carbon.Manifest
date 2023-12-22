using UnityEngine;

public class SocketMod_WaterDepth : SocketMod
{
	public float MinimumWaterDepth = 2f;

	public float MaximumWaterDepth = 4f;

	public bool AllowWaterVolumes = false;

	public static Translate.Phrase TooDeepPhrase = new Translate.Phrase ("error_toodeep", "Water is too deep");

	public static Translate.Phrase TooShallowPhrase = new Translate.Phrase ("error_shallow", "Water is too shallow");

	public override bool DoCheck (Construction.Placement place)
	{
		Vector3 pos = place.position + place.rotation * worldPosition;
		WaterLevel.WaterInfo waterInfo = WaterLevel.GetWaterInfo (pos, waves: false, AllowWaterVolumes, null, noEarlyExit: true);
		if (waterInfo.overallDepth > MinimumWaterDepth && waterInfo.overallDepth < MaximumWaterDepth) {
			return true;
		}
		if (waterInfo.overallDepth <= MinimumWaterDepth) {
			Construction.lastPlacementError = TooShallowPhrase.translated;
		} else {
			Construction.lastPlacementError = TooDeepPhrase.translated;
		}
		return false;
	}
}
