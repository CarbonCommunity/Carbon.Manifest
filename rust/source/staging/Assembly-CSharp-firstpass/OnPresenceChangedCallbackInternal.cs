using System.Runtime.InteropServices;
using Epic.OnlineServices.Presence;

[UnmanagedFunctionPointer (CallingConvention.Cdecl)]
internal delegate void OnPresenceChangedCallbackInternal (ref PresenceChangedCallbackInfoInternal data);
