using ConVar;

public class DebrisEntity : BaseCombatEntity
{
	public float DebrisDespawnOverride = 0f;

	public override void ServerInit ()
	{
		ResetRemovalTime ();
		base.ServerInit ();
	}

	public void RemoveCorpse ()
	{
		Kill ();
	}

	public void ResetRemovalTime (float dur)
	{
		using (TimeWarning.New ("ResetRemovalTime")) {
			if (IsInvoking (RemoveCorpse)) {
				CancelInvoke (RemoveCorpse);
			}
			Invoke (RemoveCorpse, dur);
		}
	}

	public float GetRemovalTime ()
	{
		return (DebrisDespawnOverride > 0f) ? DebrisDespawnOverride : Server.debrisdespawn;
	}

	public void ResetRemovalTime ()
	{
		ResetRemovalTime (GetRemovalTime ());
	}

	public override string Categorize ()
	{
		return "debris";
	}
}
