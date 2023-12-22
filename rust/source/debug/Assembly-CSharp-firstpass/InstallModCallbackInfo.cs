using Epic.OnlineServices;
using Epic.OnlineServices.Mods;

public struct InstallModCallbackInfo : ICallbackInfo
{
	public Result ResultCode { get; set; }

	public EpicAccountId LocalUserId { get; set; }

	public object ClientData { get; set; }

	public ModIdentifier? Mod { get; set; }

	public Result? GetResultCode ()
	{
		return ResultCode;
	}

	internal void Set (ref InstallModCallbackInfoInternal other)
	{
		ResultCode = other.ResultCode;
		LocalUserId = other.LocalUserId;
		ClientData = other.ClientData;
		Mod = other.Mod;
	}
}
