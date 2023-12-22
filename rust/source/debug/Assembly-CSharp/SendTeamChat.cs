using CompanionServer.Handlers;
using ConVar;
using Facepunch.Extend;
using ProtoBuf;

public class SendTeamChat : BaseHandler<AppSendMessage>
{
	protected override double TokenCost => 2.0;

	public override void Execute ()
	{
		string text = base.Proto.message?.Trim ();
		if (string.IsNullOrWhiteSpace (text)) {
			SendSuccess ();
			return;
		}
		text = StringExtensions.Truncate (text, 256, "…");
		string username = base.Player?.displayName ?? SingletonComponent<ServerMgr>.Instance.persistance.GetPlayerName (base.UserId) ?? "[unknown]";
		if (Chat.sayAs (Chat.ChatChannel.Team, base.UserId, username, text, base.Player)) {
			SendSuccess ();
		} else {
			SendError ("message_not_sent");
		}
	}
}
