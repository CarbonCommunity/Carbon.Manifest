using System;
using System.Runtime.InteropServices;

[UnmanagedFunctionPointer (CallingConvention.Cdecl)]
public delegate IntPtr AllocateMemoryFunc (UIntPtr sizeInBytes, UIntPtr alignment);
