using System.Runtime.InteropServices;
using Epic.OnlineServices.AntiCheatCommon;

[UnmanagedFunctionPointer (CallingConvention.Cdecl)]
internal delegate void OnClientActionRequiredCallbackInternal (ref OnClientActionRequiredCallbackInfoInternal data);
