using Epic.OnlineServices;
using Epic.OnlineServices.Auth;

public struct LoginCallbackInfo : ICallbackInfo
{
	public Result ResultCode { get; set; }

	public object ClientData { get; set; }

	public EpicAccountId LocalUserId { get; set; }

	public PinGrantInfo? PinGrantInfo { get; set; }

	public ContinuanceToken ContinuanceToken { get; set; }

	public AccountFeatureRestrictedInfo? AccountFeatureRestrictedInfo { get; set; }

	public EpicAccountId SelectedAccountId { get; set; }

	public Result? GetResultCode ()
	{
		return ResultCode;
	}

	internal void Set (ref LoginCallbackInfoInternal other)
	{
		ResultCode = other.ResultCode;
		ClientData = other.ClientData;
		LocalUserId = other.LocalUserId;
		PinGrantInfo = other.PinGrantInfo;
		ContinuanceToken = other.ContinuanceToken;
		AccountFeatureRestrictedInfo = other.AccountFeatureRestrictedInfo;
		SelectedAccountId = other.SelectedAccountId;
	}
}
