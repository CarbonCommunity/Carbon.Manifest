using System.Runtime.InteropServices;
using Epic.OnlineServices.Lobby;

[UnmanagedFunctionPointer (CallingConvention.Cdecl)]
internal delegate void OnJoinLobbyByIdCallbackInternal (ref JoinLobbyByIdCallbackInfoInternal data);
