using System.Runtime.InteropServices;
using Epic.OnlineServices.Presence;

[UnmanagedFunctionPointer (CallingConvention.Cdecl)]
internal delegate void OnQueryPresenceCompleteCallbackInternal (ref QueryPresenceCallbackInfoInternal data);
