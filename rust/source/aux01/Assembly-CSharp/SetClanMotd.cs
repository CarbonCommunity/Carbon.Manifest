using CompanionServer.Handlers;
using ProtoBuf;

public class SetClanMotd : BaseClanHandler<AppSendMessage>
{
	public override async void Execute ()
	{
		if (!ClanValidator.ValidateMotd (base.Proto.message, out var motd)) {
			SendError ("invalid_motd");
			return;
		}
		IClan clan = await GetClan ();
		if (clan == null) {
			SendError ("no_clan");
			return;
		}
		ClanResult clanResult = await clan.SetMotd (motd, base.UserId);
		if (clanResult == ClanResult.Success) {
			SendSuccess ();
			ClanPushNotifications.SendClanAnnouncement (clan, base.UserId);
		} else {
			SendError (clanResult);
		}
	}
}
