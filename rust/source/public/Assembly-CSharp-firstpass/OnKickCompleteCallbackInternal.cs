using System.Runtime.InteropServices;
using Epic.OnlineServices.RTCAdmin;

[UnmanagedFunctionPointer (CallingConvention.Cdecl)]
internal delegate void OnKickCompleteCallbackInternal (ref KickCompleteCallbackInfoInternal data);
