using Epic.OnlineServices;
using Epic.OnlineServices.KWS;

public struct PermissionsUpdateReceivedCallbackInfo : ICallbackInfo
{
	public object ClientData { get; set; }

	public ProductUserId LocalUserId { get; set; }

	public Result? GetResultCode ()
	{
		return null;
	}

	internal void Set (ref PermissionsUpdateReceivedCallbackInfoInternal other)
	{
		ClientData = other.ClientData;
		LocalUserId = other.LocalUserId;
	}
}
