using System.Runtime.InteropServices;
using Epic.OnlineServices.Friends;

[UnmanagedFunctionPointer (CallingConvention.Cdecl)]
internal delegate void OnSendInviteCallbackInternal (ref SendInviteCallbackInfoInternal data);
