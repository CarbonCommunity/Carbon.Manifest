using CompanionServer.Handlers;
using ProtoBuf;

public class SetSubscription : BaseEntityHandler<AppFlag>
{
	public override void Execute ()
	{
		if (base.Entity is ISubscribable subscribable) {
			if (base.Proto.value) {
				if (subscribable.AddSubscription (base.UserId)) {
					SendSuccess ();
				} else {
					SendError ("too_many_subscribers");
				}
			} else {
				subscribable.RemoveSubscription (base.UserId);
			}
			SendSuccess ();
		} else {
			SendError ("wrong_type");
		}
	}
}
