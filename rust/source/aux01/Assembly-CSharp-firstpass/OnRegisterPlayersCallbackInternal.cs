using System.Runtime.InteropServices;
using Epic.OnlineServices.Sessions;

[UnmanagedFunctionPointer (CallingConvention.Cdecl)]
internal delegate void OnRegisterPlayersCallbackInternal (ref RegisterPlayersCallbackInfoInternal data);
