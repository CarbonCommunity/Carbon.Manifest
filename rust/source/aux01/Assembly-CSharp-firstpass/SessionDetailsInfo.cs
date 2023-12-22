using Epic.OnlineServices;
using Epic.OnlineServices.Sessions;

public struct SessionDetailsInfo
{
	public Utf8String SessionId { get; set; }

	public Utf8String HostAddress { get; set; }

	public uint NumOpenPublicConnections { get; set; }

	public SessionDetailsSettings? Settings { get; set; }

	internal void Set (ref SessionDetailsInfoInternal other)
	{
		SessionId = other.SessionId;
		HostAddress = other.HostAddress;
		NumOpenPublicConnections = other.NumOpenPublicConnections;
		Settings = other.Settings;
	}
}
