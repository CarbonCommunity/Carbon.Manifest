using System.Runtime.InteropServices;
using Epic.OnlineServices.KWS;

[UnmanagedFunctionPointer (CallingConvention.Cdecl)]
internal delegate void OnRequestPermissionsCallbackInternal (ref RequestPermissionsCallbackInfoInternal data);
