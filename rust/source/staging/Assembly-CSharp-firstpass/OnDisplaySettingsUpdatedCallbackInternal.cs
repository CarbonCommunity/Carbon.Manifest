using System.Runtime.InteropServices;
using Epic.OnlineServices.UI;

[UnmanagedFunctionPointer (CallingConvention.Cdecl)]
internal delegate void OnDisplaySettingsUpdatedCallbackInternal (ref OnDisplaySettingsUpdatedCallbackInfoInternal data);
