using System.Runtime.InteropServices;
using Epic.OnlineServices.Achievements;

[UnmanagedFunctionPointer (CallingConvention.Cdecl)]
internal delegate void OnQueryPlayerAchievementsCompleteCallbackInternal (ref OnQueryPlayerAchievementsCompleteCallbackInfoInternal data);
