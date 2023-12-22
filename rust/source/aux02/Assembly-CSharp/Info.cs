using CompanionServer.Cameras;
using CompanionServer.Handlers;
using ConVar;
using Facepunch;
using Facepunch.Math;
using ProtoBuf;

public class Info : BasePlayerHandler<AppEmpty>
{
	public override void Execute ()
	{
		AppInfo val = Pool.Get<AppInfo> ();
		val.name = Server.hostname;
		val.headerImage = Server.headerimage;
		val.logoImage = Server.logoimage;
		val.url = Server.url;
		val.map = World.Name;
		val.mapSize = World.Size;
		val.wipeTime = (uint)Epoch.FromDateTime (SaveRestore.SaveCreatedTime.ToUniversalTime ());
		val.players = (uint)BasePlayer.activePlayerList.Count;
		val.maxPlayers = (uint)Server.maxplayers;
		val.queuedPlayers = (uint)SingletonComponent<ServerMgr>.Instance.connectionQueue.Queued;
		val.seed = World.Seed;
		val.camerasEnabled = CameraRenderer.enabled;
		if (NexusServer.Started) {
			int? nexusId = NexusServer.NexusId;
			string zoneKey = NexusServer.ZoneKey;
			if (nexusId.HasValue && zoneKey != null) {
				val.nexus = Nexus.endpoint;
				val.nexusId = nexusId.Value;
				val.nexusZone = zoneKey;
			}
		}
		AppResponse val2 = Pool.Get<AppResponse> ();
		val2.info = val;
		Send (val2);
	}
}
