using System.Runtime.InteropServices;
using Epic.OnlineServices.Connect;

[UnmanagedFunctionPointer (CallingConvention.Cdecl)]
internal delegate void OnTransferDeviceIdAccountCallbackInternal (ref TransferDeviceIdAccountCallbackInfoInternal data);
