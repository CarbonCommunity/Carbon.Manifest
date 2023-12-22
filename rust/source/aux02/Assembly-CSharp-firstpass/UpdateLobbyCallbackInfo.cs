using Epic.OnlineServices;
using Epic.OnlineServices.Lobby;

public struct UpdateLobbyCallbackInfo : ICallbackInfo
{
	public Result ResultCode { get; set; }

	public object ClientData { get; set; }

	public Utf8String LobbyId { get; set; }

	public Result? GetResultCode ()
	{
		return ResultCode;
	}

	internal void Set (ref UpdateLobbyCallbackInfoInternal other)
	{
		ResultCode = other.ResultCode;
		ClientData = other.ClientData;
		LobbyId = other.LobbyId;
	}
}
