using System;
using System.Runtime.InteropServices;
using Epic.OnlineServices.PlayerDataStorage;

[UnmanagedFunctionPointer (CallingConvention.Cdecl)]
internal delegate WriteResult OnWriteFileDataCallbackInternal (ref WriteFileDataCallbackInfoInternal data, IntPtr outDataBuffer, ref uint outDataWritten);
