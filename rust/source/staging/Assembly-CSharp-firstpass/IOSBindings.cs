using System;
using System.Runtime.InteropServices;
using Epic.OnlineServices.Auth;

public static class IOSBindings
{
	[DllImport ("EOSSDK-Win64-Shipping")]
	internal static extern void EOS_Auth_Login (IntPtr handle, ref IOSLoginOptionsInternal options, IntPtr clientData, OnLoginCallbackInternal completionDelegate);
}
