using System.Collections.Generic;
using CompanionServer;
using ConVar;
using Facepunch;

public static class ClanPushNotifications
{
	public static async void SendClanAnnouncement (IClan clan, ulong ignorePlayer)
	{
		if (ClanUtility.Timestamp () - clan.MotdTimestamp < 300000) {
			return;
		}
		List<ulong> steamIds = Pool.GetList<ulong> ();
		foreach (ClanMember member in clan.Members) {
			steamIds.Add (member.SteamId);
		}
		Dictionary<string, string> serverPairingData = Util.GetServerPairingData ();
		serverPairingData.Add ("type", "clan");
		serverPairingData.Add ("fromId", ignorePlayer.ToString ("G"));
		await NotificationList.SendNotificationTo (steamIds, NotificationChannel.ClanAnnouncement, "[" + clan.Name + "] Announcement was updated", ConVar.Server.hostname, serverPairingData);
		Pool.FreeList<ulong> (ref steamIds);
	}
}
