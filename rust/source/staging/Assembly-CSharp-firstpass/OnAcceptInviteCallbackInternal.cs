using System.Runtime.InteropServices;
using Epic.OnlineServices.Friends;

[UnmanagedFunctionPointer (CallingConvention.Cdecl)]
internal delegate void OnAcceptInviteCallbackInternal (ref AcceptInviteCallbackInfoInternal data);
