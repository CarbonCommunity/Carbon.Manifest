using Epic.OnlineServices;
using Epic.OnlineServices.Connect;

public struct UserLoginInfo
{
	public Utf8String DisplayName { get; set; }

	internal void Set (ref UserLoginInfoInternal other)
	{
		DisplayName = other.DisplayName;
	}
}
