using ProtoBuf.Nexus;
using Rust.Nexus.Handlers;

public class DestroyBagHandler : BaseNexusRequestHandler<SleepingBagDestroyRequest>
{
	protected override void Handle ()
	{
		SleepingBag.DestroyBag (base.Request.userId, base.Request.sleepingBagId);
	}
}
