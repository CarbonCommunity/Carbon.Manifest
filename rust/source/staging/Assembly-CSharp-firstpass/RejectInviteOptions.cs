using Epic.OnlineServices;

public struct RejectInviteOptions
{
	public ProductUserId LocalUserId { get; set; }

	public Utf8String InviteId { get; set; }
}
