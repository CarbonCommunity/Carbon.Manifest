using System.Runtime.InteropServices;
using Epic.OnlineServices.CustomInvites;

[UnmanagedFunctionPointer (CallingConvention.Cdecl)]
internal delegate void OnRequestToJoinRejectedCallbackInternal (ref OnRequestToJoinRejectedCallbackInfoInternal data);
