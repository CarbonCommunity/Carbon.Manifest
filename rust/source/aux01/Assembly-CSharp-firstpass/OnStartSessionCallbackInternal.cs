using System.Runtime.InteropServices;
using Epic.OnlineServices.Sessions;

[UnmanagedFunctionPointer (CallingConvention.Cdecl)]
internal delegate void OnStartSessionCallbackInternal (ref StartSessionCallbackInfoInternal data);
