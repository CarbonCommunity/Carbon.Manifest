using System.Runtime.InteropServices;
using Epic.OnlineServices.IntegratedPlatform;

[UnmanagedFunctionPointer (CallingConvention.Cdecl)]
internal delegate IntegratedPlatformPreLogoutAction OnUserPreLogoutCallbackInternal (ref UserPreLogoutCallbackInfoInternal data);
