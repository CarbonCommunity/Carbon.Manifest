using System.Runtime.InteropServices;
using Epic.OnlineServices.RTCAudio;

[UnmanagedFunctionPointer (CallingConvention.Cdecl)]
internal delegate void OnUpdateSendingCallbackInternal (ref UpdateSendingCallbackInfoInternal data);
