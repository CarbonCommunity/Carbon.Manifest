using CompanionServer;
using CompanionServer.Handlers;
using Facepunch;
using ProtoBuf;

public class TeamInfo : BasePlayerHandler<AppEmpty>
{
	public override void Execute ()
	{
		RelationshipManager.PlayerTeam playerTeam = RelationshipManager.ServerInstance.FindPlayersTeam (base.UserId);
		AppTeamInfo teamInfo = ((playerTeam == null) ? base.Player.GetAppTeamInfo (base.UserId) : playerTeam.GetAppTeamInfo (base.UserId));
		AppResponse val = Pool.Get<AppResponse> ();
		val.teamInfo = teamInfo;
		Send (val);
	}
}
