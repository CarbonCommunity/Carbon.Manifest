using System.Runtime.InteropServices;
using Epic.OnlineServices.Leaderboards;

[UnmanagedFunctionPointer (CallingConvention.Cdecl)]
internal delegate void OnQueryLeaderboardRanksCompleteCallbackInternal (ref OnQueryLeaderboardRanksCompleteCallbackInfoInternal data);
