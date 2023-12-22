using ProtoBuf.Nexus;
using Rust.Nexus.Handlers;
using UnityEngine;

public class ClanChatBatchHandler : BaseNexusRequestHandler<ClanChatBatchRequest>
{
	protected override void Handle ()
	{
		if (!(ClanManager.ServerInstance.Backend is NexusClanBackend nexusClanBackend)) {
			Debug.LogError ((object)"Received a clan chat batch but this server isn't using the nexus clan backend!");
		} else {
			nexusClanBackend.HandleClanChatBatch (base.Request);
		}
	}
}
