using System.Runtime.InteropServices;
using Epic.OnlineServices.AntiCheatClient;

[UnmanagedFunctionPointer (CallingConvention.Cdecl)]
internal delegate void OnClientIntegrityViolatedCallbackInternal (ref OnClientIntegrityViolatedCallbackInfoInternal data);
