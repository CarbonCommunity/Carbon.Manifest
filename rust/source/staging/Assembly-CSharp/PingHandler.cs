using ConVar;
using Facepunch;
using ProtoBuf.Nexus;
using Rust.Nexus.Handlers;

public class PingHandler : BaseNexusRequestHandler<PingRequest>
{
	protected override void Handle ()
	{
		Response val = BaseNexusRequestHandler<PingRequest>.NewResponse ();
		val.ping = Pool.Get<PingResponse> ();
		val.ping.players = BasePlayer.activePlayerList.Count;
		val.ping.maxPlayers = Server.maxplayers;
		val.ping.queuedPlayers = SingletonComponent<ServerMgr>.Instance.connectionQueue.Queued;
		SendSuccess (val);
	}
}
