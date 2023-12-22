using Epic.OnlineServices;
using Epic.OnlineServices.P2P;

public struct AcceptConnectionOptions
{
	public ProductUserId LocalUserId { get; set; }

	public ProductUserId RemoteUserId { get; set; }

	public SocketId? SocketId { get; set; }
}
