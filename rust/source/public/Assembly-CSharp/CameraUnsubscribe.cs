using CompanionServer.Cameras;
using CompanionServer.Handlers;
using ProtoBuf;

public class CameraUnsubscribe : BaseHandler<AppEmpty>
{
	public override void Execute ()
	{
		if (!CameraRenderer.enabled) {
			SendError ("not_enabled");
			return;
		}
		base.Client.EndViewing ();
		SendSuccess ();
	}
}
