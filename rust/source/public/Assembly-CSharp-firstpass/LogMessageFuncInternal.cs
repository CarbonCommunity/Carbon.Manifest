using System.Runtime.InteropServices;
using Epic.OnlineServices.Logging;

[UnmanagedFunctionPointer (CallingConvention.Cdecl)]
internal delegate void LogMessageFuncInternal (ref LogMessageInternal message);
