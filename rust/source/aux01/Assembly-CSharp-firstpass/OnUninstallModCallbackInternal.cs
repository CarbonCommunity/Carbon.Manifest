using System.Runtime.InteropServices;
using Epic.OnlineServices.Mods;

[UnmanagedFunctionPointer (CallingConvention.Cdecl)]
internal delegate void OnUninstallModCallbackInternal (ref UninstallModCallbackInfoInternal data);
