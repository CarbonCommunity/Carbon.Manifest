using System.Runtime.InteropServices;
using Epic.OnlineServices.P2P;

[UnmanagedFunctionPointer (CallingConvention.Cdecl)]
internal delegate void OnPeerConnectionEstablishedCallbackInternal (ref OnPeerConnectionEstablishedInfoInternal data);
