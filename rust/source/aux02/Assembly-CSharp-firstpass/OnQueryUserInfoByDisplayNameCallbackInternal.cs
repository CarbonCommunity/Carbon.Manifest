using System.Runtime.InteropServices;
using Epic.OnlineServices.UserInfo;

[UnmanagedFunctionPointer (CallingConvention.Cdecl)]
internal delegate void OnQueryUserInfoByDisplayNameCallbackInternal (ref QueryUserInfoByDisplayNameCallbackInfoInternal data);
