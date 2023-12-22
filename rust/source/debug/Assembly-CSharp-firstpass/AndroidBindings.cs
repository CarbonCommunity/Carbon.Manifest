using System.Runtime.InteropServices;
using Epic.OnlineServices;
using Epic.OnlineServices.Platform;

public static class AndroidBindings
{
	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern Result EOS_Initialize (ref AndroidInitializeOptionsInternal options);
}
