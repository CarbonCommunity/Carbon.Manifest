using System;
using Epic.OnlineServices.Auth;

public struct IOSCredentialsSystemAuthCredentialsOptions
{
	public IntPtr PresentationContextProviding { get; set; }

	internal void Set (ref IOSCredentialsSystemAuthCredentialsOptionsInternal other)
	{
		PresentationContextProviding = other.PresentationContextProviding;
	}
}
