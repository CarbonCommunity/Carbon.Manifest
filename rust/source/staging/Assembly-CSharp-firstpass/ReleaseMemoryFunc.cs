using System;
using System.Runtime.InteropServices;

[UnmanagedFunctionPointer (CallingConvention.Cdecl)]
public delegate void ReleaseMemoryFunc (IntPtr pointer);
