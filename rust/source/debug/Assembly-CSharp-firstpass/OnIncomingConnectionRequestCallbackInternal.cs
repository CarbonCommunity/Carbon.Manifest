using System.Runtime.InteropServices;
using Epic.OnlineServices.P2P;

[UnmanagedFunctionPointer (CallingConvention.Cdecl)]
internal delegate void OnIncomingConnectionRequestCallbackInternal (ref OnIncomingConnectionRequestInfoInternal data);
