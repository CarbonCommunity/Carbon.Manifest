using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using CompanionServer;
using ConVar;
using Facepunch.Extend;
using Steamworks;
using UnityEngine;

[Factory ("app")]
public class App : ConsoleSystem
{
	[StructLayout (LayoutKind.Auto)]
	[CompilerGenerated]
	private struct <GetPublicIPAsync>d__20 : IAsyncStateMachine
	{
		public int <>1__state;

		public AsyncValueTaskMethodBuilder<string> <>t__builder;

		private Stopwatch <timer>5__2;

		private TaskAwaiter <>u__1;

		private void MoveNext ()
		{
			int num = <>1__state;
			string result;
			try {
				if (num != 0) {
					<timer>5__2 = null;
					goto IL_0014;
				}
				TaskAwaiter awaiter = <>u__1;
				<>u__1 = default(TaskAwaiter);
				num = (<>1__state = -1);
				goto IL_00cc;
				IL_00cc:
				awaiter.GetResult ();
				goto IL_0014;
				IL_0014:
				bool num2 = <timer>5__2 != null && <timer>5__2.Elapsed.TotalMinutes > 2.0;
				string publicIP = GetPublicIP ();
				if (!num2 && (string.IsNullOrWhiteSpace (publicIP) || !(publicIP != "0.0.0.0"))) {
					if (<timer>5__2 == null) {
						<timer>5__2 = Stopwatch.StartNew ();
					}
					awaiter = Task.Delay (10000).GetAwaiter ();
					if (!awaiter.IsCompleted) {
						num = (<>1__state = 0);
						<>u__1 = awaiter;
						<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter, <GetPublicIPAsync>d__20> (ref awaiter, ref this);
						return;
					}
					goto IL_00cc;
				}
				result = publicIP;
			} catch (Exception exception) {
				<>1__state = -2;
				<timer>5__2 = null;
				<>t__builder.SetException (exception);
				return;
			}
			<>1__state = -2;
			<timer>5__2 = null;
			<>t__builder.SetResult (result);
		}

		void IAsyncStateMachine.MoveNext ()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			this.MoveNext ();
		}

		[DebuggerHidden]
		private void SetStateMachine (IAsyncStateMachine stateMachine)
		{
			<>t__builder.SetStateMachine (stateMachine);
		}

		void IAsyncStateMachine.SetStateMachine (IAsyncStateMachine stateMachine)
		{
			//ILSpy generated this explicit interface implementation from .override directive in SetStateMachine
			this.SetStateMachine (stateMachine);
		}
	}

	[ServerVar]
	public static string listenip = "";

	[ServerVar]
	public static int port;

	[ServerVar]
	public static string publicip = "";

	[ServerVar (Help = "Disables updating entirely - emergency use only")]
	public static bool update = true;

	[ServerVar (Help = "Enables sending push notifications")]
	public static bool notifications = true;

	[ServerVar (Help = "Max number of queued messages - set to 0 to disable message processing")]
	public static int queuelimit = 100;

	[ReplicatedVar (Default = "")]
	public static string serverid = "";

	[ServerVar (Help = "Cooldown time before alarms can send another notification (in seconds)")]
	public static float alarmcooldown = 30f;

	[ServerVar]
	public static int maxconnections = 500;

	[ServerVar]
	public static int maxconnectionsperip = 5;

	[ServerVar]
	public static int maxmessagesize = 1048576;

	[ServerUserVar]
	public static async void pair (Arg arg)
	{
		BasePlayer basePlayer = arg.Player ();
		if (!((Object)(object)basePlayer == (Object)null)) {
			Dictionary<string, string> playerPairingData = Util.GetPlayerPairingData (basePlayer);
			NotificationSendResult notificationSendResult = await Util.SendPairNotification ("server", basePlayer, StringExtensions.Truncate (ConVar.Server.hostname, 128, (string)null), "Tap to pair with this server.", playerPairingData);
			arg.ReplyWith ((notificationSendResult == NotificationSendResult.Sent) ? "Sent pairing notification." : notificationSendResult.ToErrorMessage ());
		}
	}

	[ServerUserVar]
	public static void regeneratetoken (Arg arg)
	{
		BasePlayer basePlayer = arg.Player ();
		if (!((Object)(object)basePlayer == (Object)null)) {
			SingletonComponent<ServerMgr>.Instance.persistance.RegenerateAppToken (basePlayer.userID);
			arg.ReplyWith ("Regenerated Rust+ token");
		}
	}

	[ServerVar]
	public static void info (Arg arg)
	{
		if (!CompanionServer.Server.IsEnabled) {
			arg.ReplyWith ("Companion server is not enabled");
			return;
		}
		Listener listener = CompanionServer.Server.Listener;
		arg.ReplyWith ($"Server ID: {serverid}\nListening on: {listener.Address}:{listener.Port}\nApp connects to: {GetPublicIP ()}:{port}");
	}

	[ServerVar (Help = "Retry initializing the Rust+ companion server if it previously failed")]
	public static void retry_initialize (Arg arg)
	{
		if (CompanionServer.Server.IsEnabled) {
			arg.ReplyWith ("Companion server is already initialized!");
			return;
		}
		if (port < 0) {
			arg.ReplyWith ("Companion server port is invalid, cannot initialize companion server");
			return;
		}
		BaseGameMode activeGameMode = BaseGameMode.GetActiveGameMode (serverside: true);
		if ((Object)(object)activeGameMode != (Object)null && !activeGameMode.rustPlus) {
			arg.ReplyWith ("Companion server is disabled by gamemode, cannot initialize companion server");
			return;
		}
		arg.ReplyWith ("Trying to initialize companion server...");
		CompanionServer.Server.Initialize ();
	}

	[ServerVar]
	public static void resetlimiter (Arg arg)
	{
		CompanionServer.Server.Listener?.Limiter?.Clear ();
	}

	[ServerVar]
	public static void connections (Arg arg)
	{
		string text = CompanionServer.Server.Listener?.Limiter?.ToString () ?? "Not available";
		arg.ReplyWith (text);
	}

	[ServerVar]
	public static void appban (Arg arg)
	{
		ulong uLong = arg.GetULong (0, 0uL);
		if (uLong == 0L) {
			arg.ReplyWith ("Usage: app.appban <steamID64>");
			return;
		}
		string text = (SingletonComponent<ServerMgr>.Instance.persistance.SetAppTokenLocked (uLong, locked: true) ? $"Banned {uLong} from using the companion app" : $"{uLong} is already banned from using the companion app");
		arg.ReplyWith (text);
	}

	[ServerVar]
	public static void appunban (Arg arg)
	{
		ulong uLong = arg.GetULong (0, 0uL);
		if (uLong == 0L) {
			arg.ReplyWith ("Usage: app.appunban <steamID64>");
			return;
		}
		string text = (SingletonComponent<ServerMgr>.Instance.persistance.SetAppTokenLocked (uLong, locked: false) ? $"Unbanned {uLong}, they can use the companion app again" : $"{uLong} is not banned from using the companion app");
		arg.ReplyWith (text);
	}

	public static IPAddress GetListenIP ()
	{
		if (!string.IsNullOrWhiteSpace (listenip)) {
			if (!IPAddress.TryParse (listenip, out var address) || address.AddressFamily != AddressFamily.InterNetwork) {
				Debug.LogError ((object)("Invalid app.listenip: " + listenip));
				return IPAddress.Any;
			}
			return address;
		}
		return IPAddress.Any;
	}

	[AsyncStateMachine (typeof(<GetPublicIPAsync>d__20))]
	public static ValueTask<string> GetPublicIPAsync ()
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		<GetPublicIPAsync>d__20 <GetPublicIPAsync>d__ = default(<GetPublicIPAsync>d__20);
		<GetPublicIPAsync>d__.<>t__builder = AsyncValueTaskMethodBuilder<string>.Create ();
		<GetPublicIPAsync>d__.<>1__state = -1;
		<GetPublicIPAsync>d__.<>t__builder.Start<<GetPublicIPAsync>d__20> (ref <GetPublicIPAsync>d__);
		return <GetPublicIPAsync>d__.<>t__builder.Task;
	}

	public static string GetPublicIP ()
	{
		if (!string.IsNullOrWhiteSpace (publicip) && IPAddress.TryParse (publicip, out var address) && address.AddressFamily == AddressFamily.InterNetwork) {
			return publicip;
		}
		return SteamServer.PublicIp.ToString ();
	}
}
