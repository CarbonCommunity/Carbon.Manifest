using System.Runtime.InteropServices;
using Epic.OnlineServices.RTCAudio;

[UnmanagedFunctionPointer (CallingConvention.Cdecl)]
internal delegate void OnAudioBeforeSendCallbackInternal (ref AudioBeforeSendCallbackInfoInternal data);
