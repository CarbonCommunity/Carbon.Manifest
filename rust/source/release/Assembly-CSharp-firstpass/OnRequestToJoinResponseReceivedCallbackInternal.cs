using System.Runtime.InteropServices;
using Epic.OnlineServices.CustomInvites;

[UnmanagedFunctionPointer (CallingConvention.Cdecl)]
internal delegate void OnRequestToJoinResponseReceivedCallbackInternal (ref RequestToJoinResponseReceivedCallbackInfoInternal data);
