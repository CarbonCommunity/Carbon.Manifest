using System.Runtime.InteropServices;
using Epic.OnlineServices.KWS;

[UnmanagedFunctionPointer (CallingConvention.Cdecl)]
internal delegate void OnQueryPermissionsCallbackInternal (ref QueryPermissionsCallbackInfoInternal data);
