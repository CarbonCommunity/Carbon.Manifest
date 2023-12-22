using CompanionServer.Cameras;
using CompanionServer.Handlers;
using ConVar;
using Facepunch;
using Facepunch.Math;
using ProtoBuf;

public class Info : BaseHandler<AppEmpty>
{
	public override void Execute ()
	{
		AppInfo appInfo = Facepunch.Pool.Get<AppInfo> ();
		appInfo.name = Server.hostname;
		appInfo.headerImage = Server.headerimage;
		appInfo.logoImage = Server.logoimage;
		appInfo.url = Server.url;
		appInfo.map = World.Name;
		appInfo.mapSize = World.Size;
		appInfo.wipeTime = (uint)Epoch.FromDateTime (SaveRestore.SaveCreatedTime.ToUniversalTime ());
		appInfo.players = (uint)BasePlayer.activePlayerList.Count;
		appInfo.maxPlayers = (uint)Server.maxplayers;
		appInfo.queuedPlayers = (uint)SingletonComponent<ServerMgr>.Instance.connectionQueue.Queued;
		appInfo.seed = World.Seed;
		appInfo.camerasEnabled = CameraRenderer.enabled;
		AppResponse appResponse = Facepunch.Pool.Get<AppResponse> ();
		appResponse.info = appInfo;
		Send (appResponse);
	}
}
