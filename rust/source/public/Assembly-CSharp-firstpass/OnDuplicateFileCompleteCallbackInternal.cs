using System.Runtime.InteropServices;
using Epic.OnlineServices.PlayerDataStorage;

[UnmanagedFunctionPointer (CallingConvention.Cdecl)]
internal delegate void OnDuplicateFileCompleteCallbackInternal (ref DuplicateFileCallbackInfoInternal data);
