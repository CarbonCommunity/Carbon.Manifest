using System.Runtime.InteropServices;
using Epic.OnlineServices.Lobby;

[UnmanagedFunctionPointer (CallingConvention.Cdecl)]
internal delegate void OnLobbyInviteReceivedCallbackInternal (ref LobbyInviteReceivedCallbackInfoInternal data);
