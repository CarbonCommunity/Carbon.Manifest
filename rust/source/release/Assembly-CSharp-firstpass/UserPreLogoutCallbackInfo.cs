using Epic.OnlineServices;
using Epic.OnlineServices.IntegratedPlatform;

public struct UserPreLogoutCallbackInfo : ICallbackInfo
{
	public object ClientData { get; set; }

	public Utf8String PlatformType { get; set; }

	public Utf8String LocalPlatformUserId { get; set; }

	public EpicAccountId AccountId { get; set; }

	public ProductUserId ProductUserId { get; set; }

	public Result? GetResultCode ()
	{
		return null;
	}

	internal void Set (ref UserPreLogoutCallbackInfoInternal other)
	{
		ClientData = other.ClientData;
		PlatformType = other.PlatformType;
		LocalPlatformUserId = other.LocalPlatformUserId;
		AccountId = other.AccountId;
		ProductUserId = other.ProductUserId;
	}
}
