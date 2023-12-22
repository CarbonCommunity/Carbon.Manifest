using System;
using Epic.OnlineServices.UI;

public struct PrePresentOptions
{
	public IntPtr PlatformSpecificData { get; set; }

	internal void Set (ref PrePresentOptionsInternal other)
	{
		PlatformSpecificData = other.PlatformSpecificData;
	}
}
