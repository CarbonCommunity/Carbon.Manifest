using System.Runtime.InteropServices;
using Epic.OnlineServices.KWS;

[UnmanagedFunctionPointer (CallingConvention.Cdecl)]
internal delegate void OnPermissionsUpdateReceivedCallbackInternal (ref PermissionsUpdateReceivedCallbackInfoInternal data);
