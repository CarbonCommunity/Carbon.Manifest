using System.Runtime.InteropServices;
using Epic.OnlineServices.Mods;

[UnmanagedFunctionPointer (CallingConvention.Cdecl)]
internal delegate void OnUpdateModCallbackInternal (ref UpdateModCallbackInfoInternal data);
