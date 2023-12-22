using System;
using Epic.OnlineServices.Platform;

public struct RTCOptions
{
	public IntPtr PlatformSpecificOptions { get; set; }

	internal void Set (ref RTCOptionsInternal other)
	{
		PlatformSpecificOptions = other.PlatformSpecificOptions;
	}
}
