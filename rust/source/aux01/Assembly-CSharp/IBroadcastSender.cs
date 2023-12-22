using System.Collections.Generic;

public interface IBroadcastSender<TTarget, TMessage> where TTarget : class
{
	void BroadcastTo (List<TTarget> targets, TMessage message);
}
