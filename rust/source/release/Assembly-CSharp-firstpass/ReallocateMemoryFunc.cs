using System;
using System.Runtime.InteropServices;

[UnmanagedFunctionPointer (CallingConvention.Cdecl)]
public delegate IntPtr ReallocateMemoryFunc (IntPtr pointer, UIntPtr sizeInBytes, UIntPtr alignment);
