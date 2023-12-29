using System.Runtime.InteropServices;
using Epic.OnlineServices.Sessions;

[UnmanagedFunctionPointer (CallingConvention.Cdecl)]
internal delegate void OnQueryInvitesCallbackInternal (ref QueryInvitesCallbackInfoInternal data);
