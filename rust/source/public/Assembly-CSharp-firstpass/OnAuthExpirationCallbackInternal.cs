using System.Runtime.InteropServices;
using Epic.OnlineServices.Connect;

[UnmanagedFunctionPointer (CallingConvention.Cdecl)]
internal delegate void OnAuthExpirationCallbackInternal (ref AuthExpirationCallbackInfoInternal data);
