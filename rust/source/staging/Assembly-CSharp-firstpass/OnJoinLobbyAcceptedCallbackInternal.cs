using System.Runtime.InteropServices;
using Epic.OnlineServices.Lobby;

[UnmanagedFunctionPointer (CallingConvention.Cdecl)]
internal delegate void OnJoinLobbyAcceptedCallbackInternal (ref JoinLobbyAcceptedCallbackInfoInternal data);
