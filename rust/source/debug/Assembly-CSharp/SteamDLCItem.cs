using UnityEngine;

[CreateAssetMenu (menuName = "Rust/Steam DLC Item")]
public class SteamDLCItem : ScriptableObject
{
	public int id;

	public Phrase dlcName;

	public int dlcAppID;

	public bool bypassLicenseCheck = false;

	public bool HasLicense (ulong steamid)
	{
		if (bypassLicenseCheck) {
			return true;
		}
		if (!PlatformService.Instance.IsValid) {
			return false;
		}
		return PlatformService.Instance.PlayerOwnsDownloadableContent (steamid, dlcAppID);
	}

	public bool CanUse (BasePlayer player)
	{
		if (player.isServer) {
			return HasLicense (player.userID) || player.userID < 10000000;
		}
		return false;
	}
}
