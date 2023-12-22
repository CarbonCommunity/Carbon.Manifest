using Epic.OnlineServices.Platform;

public struct GetDesktopCrossplayStatusInfo
{
	public DesktopCrossplayStatus Status { get; set; }

	public int ServiceInitResult { get; set; }

	internal void Set (ref GetDesktopCrossplayStatusInfoInternal other)
	{
		Status = other.Status;
		ServiceInitResult = other.ServiceInitResult;
	}
}
