using System.Runtime.InteropServices;
using Epic.OnlineServices.Sessions;

[UnmanagedFunctionPointer (CallingConvention.Cdecl)]
internal delegate void OnJoinSessionCallbackInternal (ref JoinSessionCallbackInfoInternal data);
