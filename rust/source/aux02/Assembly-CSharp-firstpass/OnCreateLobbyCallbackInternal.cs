using System.Runtime.InteropServices;
using Epic.OnlineServices.Lobby;

[UnmanagedFunctionPointer (CallingConvention.Cdecl)]
internal delegate void OnCreateLobbyCallbackInternal (ref CreateLobbyCallbackInfoInternal data);
