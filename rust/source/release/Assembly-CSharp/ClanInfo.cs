using CompanionServer.Handlers;
using Facepunch;
using ProtoBuf;

public class ClanInfo : BaseClanHandler<AppEmpty>
{
	public override async void Execute ()
	{
		IClan clan = await GetClan ();
		if (clan == null) {
			SendError ("no_clan");
			return;
		}
		AppClanInfo appClanInfo = Pool.Get<AppClanInfo> ();
		appClanInfo.clanInfo = clan.ToProto ();
		AppResponse appResponse = Pool.Get<AppResponse> ();
		appResponse.clanInfo = appClanInfo;
		Send (appResponse);
	}
}
