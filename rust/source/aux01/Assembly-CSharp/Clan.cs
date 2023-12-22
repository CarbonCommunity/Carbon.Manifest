using System;
using System.Text;
using System.Threading.Tasks;
using Facepunch.Extend;
using UnityEngine;

[Factory ("clan")]
public class Clan : ConsoleSystem
{
	[ServerVar (Help = "Maximum number of members each clan can have (local backend only!)")]
	public static int maxMemberCount = 100;

	[ServerVar (Help = "Prints info about a clan given its ID")]
	public static void Info (Arg arg)
	{
		if (ClanManager.ServerInstance == null) {
			arg.ReplyWith ("ClanManager is null!");
			return;
		}
		long clanId = arg.GetLong (0, 0L);
		if (clanId == 0L) {
			BasePlayer basePlayer = arg.Player ();
			if (basePlayer == null) {
				arg.ReplyWith ("Usage: clan.info <clanID>");
			} else {
				SendClanInfoPlayer (basePlayer);
			}
		} else {
			SendClanInfoConsole (clanId);
		}
		static string FormatClan (IClan clan)
		{
			StringBuilder stringBuilder = new StringBuilder ();
			stringBuilder.AppendLine ($"Clan ID: {clan.ClanId}");
			stringBuilder.AppendLine ("Name: " + clan.Name);
			stringBuilder.AppendLine ("MoTD: " + clan.Motd);
			stringBuilder.AppendLine ("Members:");
			foreach (ClanMember member in clan.Members) {
				ClanRole? clanRole = clan.Roles.TryFindWith ((ClanRole r) => r.RoleId, member.RoleId);
				string text = SingletonComponent<ServerMgr>.Instance.persistance.GetPlayerName (member.SteamId);
				if (text == null) {
					ulong steamId = member.SteamId;
					text = steamId.ToString ("G");
				}
				string text2 = text;
				stringBuilder.AppendLine ("  " + text2 + " (" + clanRole?.Name + ")");
			}
			return stringBuilder.ToString ();
		}
		static async void SendClanInfoConsole (long id)
		{
			try {
				IClan clan2 = await GetClanByID (id);
				if (clan2 != null) {
					Debug.Log (FormatClan (clan2));
				}
			} catch (Exception exception) {
				Debug.LogException (exception);
			}
		}
		async void SendClanInfoPlayer (BasePlayer player)
		{
			_ = 1;
			try {
				IClan clan3 = ((clanId != 0L) ? (await GetClanByID (clanId)) : (await GetPlayerClan (player)));
				IClan clan4 = clan3;
				if (clan4 != null) {
					string msg = FormatClan (clan4);
					player.ConsoleMessage (msg);
				}
			} catch (Exception ex) {
				Debug.LogException (ex);
				player.ConsoleMessage (ex.ToString ());
			}
		}
	}

	private static async ValueTask<IClan> GetPlayerClan (BasePlayer player)
	{
		ClanValueResult<IClan> clanValueResult = await ClanManager.ServerInstance.Backend.GetByMember (player.userID);
		if (!clanValueResult.IsSuccess) {
			string msg = ((clanValueResult.Result == ClanResult.NoClan) ? "You're not in a clan!" : "Failed to find your clan!");
			player.ConsoleMessage (msg);
			return null;
		}
		return clanValueResult.Value;
	}

	private static async ValueTask<IClan> GetClanByID (long clanId, BasePlayer player = null)
	{
		ClanValueResult<IClan> clanValueResult = await ClanManager.ServerInstance.Backend.Get (clanId);
		if (!clanValueResult.IsSuccess) {
			string text = ((clanValueResult.Result == ClanResult.NotFound) ? $"Clan with ID {clanId} was not found!" : $"Failed to get the clan with ID {clanId} ({clanValueResult.Result})!");
			if (player != null) {
				player.ConsoleMessage (text);
			} else {
				Debug.Log (text);
			}
			return null;
		}
		return clanValueResult.Value;
	}
}
