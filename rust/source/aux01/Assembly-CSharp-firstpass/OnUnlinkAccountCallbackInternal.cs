using System.Runtime.InteropServices;
using Epic.OnlineServices.Connect;

[UnmanagedFunctionPointer (CallingConvention.Cdecl)]
internal delegate void OnUnlinkAccountCallbackInternal (ref UnlinkAccountCallbackInfoInternal data);
