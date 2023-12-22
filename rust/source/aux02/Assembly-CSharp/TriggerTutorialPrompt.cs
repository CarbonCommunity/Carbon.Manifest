public class TriggerTutorialPrompt : TriggerBase
{
	public enum CustomCloseAction
	{
		None,
		OpenInventory
	}

	public TutorialFullScreenHelpInfo ToDisplay;

	public BaseMission RequiredMission;

	public int RequiredMissionStageActive = -1;

	public bool OnlyShowOnce;

	public CustomCloseAction CloseAction;

	public bool DebugConditions;
}
