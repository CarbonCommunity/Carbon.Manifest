using System.Runtime.InteropServices;
using Epic.OnlineServices.PlayerDataStorage;

[UnmanagedFunctionPointer (CallingConvention.Cdecl)]
internal delegate ReadResult OnReadFileDataCallbackInternal (ref ReadFileDataCallbackInfoInternal data);
