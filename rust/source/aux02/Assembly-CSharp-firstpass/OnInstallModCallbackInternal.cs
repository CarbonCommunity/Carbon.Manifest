using System.Runtime.InteropServices;
using Epic.OnlineServices.Mods;

[UnmanagedFunctionPointer (CallingConvention.Cdecl)]
internal delegate void OnInstallModCallbackInternal (ref InstallModCallbackInfoInternal data);
