using ProtoBuf.Nexus;
using Rust.Nexus.Handlers;

public class PlayerManifestHandler : BaseNexusRequestHandler<PlayerManifestRequest>
{
	protected override void Handle ()
	{
		NexusServer.AddZonePlayerManifest (base.FromZone.Key, base.Request.userIds);
	}
}
