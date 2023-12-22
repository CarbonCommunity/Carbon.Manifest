using Epic.OnlineServices.Platform;

public struct WindowsRTCOptions
{
	public WindowsRTCOptionsPlatformSpecificOptions? PlatformSpecificOptions { get; set; }

	internal void Set (ref WindowsRTCOptionsInternal other)
	{
		PlatformSpecificOptions = other.PlatformSpecificOptions;
	}
}
