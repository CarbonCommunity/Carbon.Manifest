using System.Runtime.InteropServices;
using Epic.OnlineServices.RTC;

[UnmanagedFunctionPointer (CallingConvention.Cdecl)]
internal delegate void OnRoomStatisticsUpdatedCallbackInternal (ref RoomStatisticsUpdatedInfoInternal data);
