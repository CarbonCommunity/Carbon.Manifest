using System.Runtime.InteropServices;
using Epic.OnlineServices.UI;

[UnmanagedFunctionPointer (CallingConvention.Cdecl)]
internal delegate void OnHideFriendsCallbackInternal (ref HideFriendsCallbackInfoInternal data);
