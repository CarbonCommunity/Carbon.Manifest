using System.Runtime.InteropServices;
using Epic.OnlineServices.AntiCheatCommon;

[UnmanagedFunctionPointer (CallingConvention.Cdecl)]
internal delegate void OnMessageToPeerCallbackInternal (ref OnMessageToClientCallbackInfoInternal data);
