using UnityEngine;

public class CinematicScenePlaybackEntity : BaseEntity
{
	public Animator RootAnimator;

	public GameObjectRef CinematicUI;

	public float Duration = 10f;

	public GameObject DebugRoot;

	public bool ShowDebugRoot;

	private BasePlayer currentPlayer;

	public void SignalKillPlayer ()
	{
		if (base.isServer && currentPlayer != null) {
			TutorialIsland currentTutorialIsland = currentPlayer.GetCurrentTutorialIsland ();
			if (currentTutorialIsland != null) {
				currentTutorialIsland.OnPlayerCompletedTutorial (currentPlayer);
			}
		}
	}

	public void AssignPlayer (BasePlayer bp)
	{
		currentPlayer = bp;
	}

	public override void ServerInit ()
	{
		base.ServerInit ();
		Invoke (Timeout, Duration);
	}

	private void Timeout ()
	{
		Kill ();
	}
}
