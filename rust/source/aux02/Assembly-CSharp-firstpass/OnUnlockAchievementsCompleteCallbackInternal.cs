using System.Runtime.InteropServices;
using Epic.OnlineServices.Achievements;

[UnmanagedFunctionPointer (CallingConvention.Cdecl)]
internal delegate void OnUnlockAchievementsCompleteCallbackInternal (ref OnUnlockAchievementsCompleteCallbackInfoInternal data);
