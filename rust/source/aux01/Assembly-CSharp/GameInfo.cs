using ConVar;
using UnityEngine;

internal static class GameInfo
{
	internal static bool IsOfficialServer {
		get {
			if (Application.isEditor) {
				return true;
			}
			return Server.official;
		}
	}

	internal static bool HasAchievements => IsOfficialServer;
}
