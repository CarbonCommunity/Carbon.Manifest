using System.Runtime.InteropServices;
using Epic.OnlineServices.IntegratedPlatform;

[UnmanagedFunctionPointer (CallingConvention.Cdecl)]
internal delegate void OnUserLoginStatusChangedCallbackInternal (ref UserLoginStatusChangedCallbackInfoInternal data);
