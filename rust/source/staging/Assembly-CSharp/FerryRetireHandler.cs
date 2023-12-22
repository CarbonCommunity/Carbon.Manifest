using ProtoBuf.Nexus;
using Rust.Nexus.Handlers;
using UnityEngine;

public class FerryRetireHandler : BaseNexusRequestHandler<FerryRetireRequest>
{
	protected override void Handle ()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		NexusFerry nexusFerry = NexusFerry.Get (base.Request.entityId, base.Request.timestamp);
		if ((Object)(object)nexusFerry != (Object)null) {
			nexusFerry.Retire ();
		}
		SendSuccess ();
	}
}
