using System.Runtime.InteropServices;
using Epic.OnlineServices.ProgressionSnapshot;

[UnmanagedFunctionPointer (CallingConvention.Cdecl)]
internal delegate void OnSubmitSnapshotCallbackInternal (ref SubmitSnapshotCallbackInfoInternal data);
