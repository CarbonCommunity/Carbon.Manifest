using System.Runtime.InteropServices;
using Epic.OnlineServices.KWS;

[UnmanagedFunctionPointer (CallingConvention.Cdecl)]
internal delegate void OnQueryAgeGateCallbackInternal (ref QueryAgeGateCallbackInfoInternal data);
