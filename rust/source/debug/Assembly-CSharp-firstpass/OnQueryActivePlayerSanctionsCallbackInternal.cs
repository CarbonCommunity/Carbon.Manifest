using System.Runtime.InteropServices;
using Epic.OnlineServices.Sanctions;

[UnmanagedFunctionPointer (CallingConvention.Cdecl)]
internal delegate void OnQueryActivePlayerSanctionsCallbackInternal (ref QueryActivePlayerSanctionsCallbackInfoInternal data);
