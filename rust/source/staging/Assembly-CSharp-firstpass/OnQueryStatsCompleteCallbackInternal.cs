using System.Runtime.InteropServices;
using Epic.OnlineServices.Stats;

[UnmanagedFunctionPointer (CallingConvention.Cdecl)]
internal delegate void OnQueryStatsCompleteCallbackInternal (ref OnQueryStatsCompleteCallbackInfoInternal data);
