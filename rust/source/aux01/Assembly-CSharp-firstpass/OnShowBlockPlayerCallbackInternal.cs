using System.Runtime.InteropServices;
using Epic.OnlineServices.UI;

[UnmanagedFunctionPointer (CallingConvention.Cdecl)]
internal delegate void OnShowBlockPlayerCallbackInternal (ref OnShowBlockPlayerCallbackInfoInternal data);
