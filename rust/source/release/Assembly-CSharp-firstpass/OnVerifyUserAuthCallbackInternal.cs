using System.Runtime.InteropServices;
using Epic.OnlineServices.Auth;

[UnmanagedFunctionPointer (CallingConvention.Cdecl)]
internal delegate void OnVerifyUserAuthCallbackInternal (ref VerifyUserAuthCallbackInfoInternal data);
