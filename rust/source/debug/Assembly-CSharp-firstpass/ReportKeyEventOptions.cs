using System;
using Epic.OnlineServices.UI;

public struct ReportKeyEventOptions
{
	public IntPtr PlatformSpecificInputData { get; set; }

	internal void Set (ref ReportKeyEventOptionsInternal other)
	{
		PlatformSpecificInputData = other.PlatformSpecificInputData;
	}
}
