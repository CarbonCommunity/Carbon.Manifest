using System.Runtime.InteropServices;
using Epic.OnlineServices.UserInfo;

[UnmanagedFunctionPointer (CallingConvention.Cdecl)]
internal delegate void OnQueryUserInfoByExternalAccountCallbackInternal (ref QueryUserInfoByExternalAccountCallbackInfoInternal data);
