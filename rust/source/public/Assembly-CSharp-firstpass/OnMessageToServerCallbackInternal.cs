using System.Runtime.InteropServices;
using Epic.OnlineServices.AntiCheatClient;

[UnmanagedFunctionPointer (CallingConvention.Cdecl)]
internal delegate void OnMessageToServerCallbackInternal (ref OnMessageToServerCallbackInfoInternal data);
