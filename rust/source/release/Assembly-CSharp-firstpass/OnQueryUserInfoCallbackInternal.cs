using System.Runtime.InteropServices;
using Epic.OnlineServices.UserInfo;

[UnmanagedFunctionPointer (CallingConvention.Cdecl)]
internal delegate void OnQueryUserInfoCallbackInternal (ref QueryUserInfoCallbackInfoInternal data);
