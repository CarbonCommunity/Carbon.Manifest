using System.Runtime.InteropServices;
using Epic.OnlineServices.Ecom;

[UnmanagedFunctionPointer (CallingConvention.Cdecl)]
internal delegate void OnRedeemEntitlementsCallbackInternal (ref RedeemEntitlementsCallbackInfoInternal data);
