using System.Runtime.InteropServices;
using Epic.OnlineServices.Reports;

[UnmanagedFunctionPointer (CallingConvention.Cdecl)]
internal delegate void OnSendPlayerBehaviorReportCompleteCallbackInternal (ref SendPlayerBehaviorReportCompleteCallbackInfoInternal data);
