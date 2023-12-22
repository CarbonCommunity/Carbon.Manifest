using Epic.OnlineServices;
using Epic.OnlineServices.Connect;

public struct CreateUserCallbackInfo : ICallbackInfo
{
	public Result ResultCode { get; set; }

	public object ClientData { get; set; }

	public ProductUserId LocalUserId { get; set; }

	public Result? GetResultCode ()
	{
		return ResultCode;
	}

	internal void Set (ref CreateUserCallbackInfoInternal other)
	{
		ResultCode = other.ResultCode;
		ClientData = other.ClientData;
		LocalUserId = other.LocalUserId;
	}
}
