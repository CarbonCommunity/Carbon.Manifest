public class EventScheduleWipeOffset : EventSchedule
{
	[ServerVar (Name = "event_hours_before_wipe")]
	public static float hoursBeforeWipeRealtime = 24f;

	public override void RunSchedule ()
	{
		if (!(WipeTimer.serverinstance == null)) {
			double totalHours = WipeTimer.serverinstance.GetTimeSpanUntilWipe ().TotalHours;
			if (!(totalHours > (double)hoursBeforeWipeRealtime)) {
				base.RunSchedule ();
			}
		}
	}
}
