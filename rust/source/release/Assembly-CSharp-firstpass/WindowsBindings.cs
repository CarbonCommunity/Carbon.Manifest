using System;
using System.Runtime.InteropServices;
using Epic.OnlineServices.Platform;

public static class WindowsBindings
{
	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern IntPtr EOS_Platform_Create (ref WindowsOptionsInternal options);
}
