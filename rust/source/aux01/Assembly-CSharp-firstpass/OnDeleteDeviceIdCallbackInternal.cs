using System.Runtime.InteropServices;
using Epic.OnlineServices.Connect;

[UnmanagedFunctionPointer (CallingConvention.Cdecl)]
internal delegate void OnDeleteDeviceIdCallbackInternal (ref DeleteDeviceIdCallbackInfoInternal data);
