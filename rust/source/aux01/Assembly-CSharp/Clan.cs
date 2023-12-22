using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using ConVar;
using Facepunch.Extend;
using UnityEngine;

[Factory ("clan")]
public class Clan : ConsoleSystem
{
	[StructLayout (LayoutKind.Auto)]
	[CompilerGenerated]
	private struct <GetPlayerClan>d__2 : IAsyncStateMachine
	{
		public int <>1__state;

		public AsyncValueTaskMethodBuilder<IClan> <>t__builder;

		public BasePlayer player;

		private ValueTaskAwaiter<ClanValueResult<IClan>> <>u__1;

		private void MoveNext ()
		{
			//IL_005b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0060: Unknown result type (might be due to invalid IL or missing references)
			//IL_0067: Unknown result type (might be due to invalid IL or missing references)
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			//IL_002d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0078: Unknown result type (might be due to invalid IL or missing references)
			//IL_007d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0041: Unknown result type (might be due to invalid IL or missing references)
			//IL_0042: Unknown result type (might be due to invalid IL or missing references)
			//IL_0087: Unknown result type (might be due to invalid IL or missing references)
			//IL_0088: Unknown result type (might be due to invalid IL or missing references)
			//IL_008e: Invalid comparison between Unknown and I4
			int num = <>1__state;
			IClan result2;
			try {
				ValueTaskAwaiter<ClanValueResult<IClan>> awaiter;
				if (num != 0) {
					awaiter = ClanManager.ServerInstance.Backend.GetByMember (player.userID).GetAwaiter ();
					if (!awaiter.IsCompleted) {
						num = (<>1__state = 0);
						<>u__1 = awaiter;
						<>t__builder.AwaitUnsafeOnCompleted<ValueTaskAwaiter<ClanValueResult<IClan>>, <GetPlayerClan>d__2> (ref awaiter, ref this);
						return;
					}
				} else {
					awaiter = <>u__1;
					<>u__1 = default(ValueTaskAwaiter<ClanValueResult<IClan>>);
					num = (<>1__state = -1);
				}
				ClanValueResult<IClan> result = awaiter.GetResult ();
				if (!result.IsSuccess) {
					string msg = (((int)result.Result == 3) ? "You're not in a clan!" : "Failed to find your clan!");
					player.ConsoleMessage (msg);
					result2 = null;
				} else {
					result2 = result.Value;
				}
			} catch (Exception exception) {
				<>1__state = -2;
				<>t__builder.SetException (exception);
				return;
			}
			<>1__state = -2;
			<>t__builder.SetResult (result2);
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

	[StructLayout (LayoutKind.Auto)]
	[CompilerGenerated]
	private struct <GetClanByID>d__3 : IAsyncStateMachine
	{
		public int <>1__state;

		public AsyncValueTaskMethodBuilder<IClan> <>t__builder;

		public long clanId;

		public BasePlayer player;

		private ValueTaskAwaiter<ClanValueResult<IClan>> <>u__1;

		private void MoveNext ()
		{
			//IL_0056: Unknown result type (might be due to invalid IL or missing references)
			//IL_005b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0062: Unknown result type (might be due to invalid IL or missing references)
			//IL_0023: Unknown result type (might be due to invalid IL or missing references)
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			//IL_0073: Unknown result type (might be due to invalid IL or missing references)
			//IL_0078: Unknown result type (might be due to invalid IL or missing references)
			//IL_003c: Unknown result type (might be due to invalid IL or missing references)
			//IL_003d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0082: Unknown result type (might be due to invalid IL or missing references)
			//IL_0083: Unknown result type (might be due to invalid IL or missing references)
			//IL_0089: Invalid comparison between Unknown and I4
			//IL_009b: Unknown result type (might be due to invalid IL or missing references)
			//IL_009c: Unknown result type (might be due to invalid IL or missing references)
			int num = <>1__state;
			IClan result2;
			try {
				ValueTaskAwaiter<ClanValueResult<IClan>> awaiter;
				if (num != 0) {
					awaiter = ClanManager.ServerInstance.Backend.Get (clanId).GetAwaiter ();
					if (!awaiter.IsCompleted) {
						num = (<>1__state = 0);
						<>u__1 = awaiter;
						<>t__builder.AwaitUnsafeOnCompleted<ValueTaskAwaiter<ClanValueResult<IClan>>, <GetClanByID>d__3> (ref awaiter, ref this);
						return;
					}
				} else {
					awaiter = <>u__1;
					<>u__1 = default(ValueTaskAwaiter<ClanValueResult<IClan>>);
					num = (<>1__state = -1);
				}
				ClanValueResult<IClan> result = awaiter.GetResult ();
				if (!result.IsSuccess) {
					string text = (((int)result.Result == 4) ? $"Clan with ID {clanId} was not found!" : $"Failed to get the clan with ID {clanId} ({result.Result})!");
					if ((Object)(object)player != (Object)null) {
						player.ConsoleMessage (text);
					} else {
						Debug.Log ((object)text);
					}
					result2 = null;
				} else {
					result2 = result.Value;
				}
			} catch (Exception exception) {
				<>1__state = -2;
				<>t__builder.SetException (exception);
				return;
			}
			<>1__state = -2;
			<>t__builder.SetResult (result2);
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

	[ServerVar (Help = "Maximum number of members each clan can have (local backend only!)")]
	public static int maxMemberCount = 100;

	[ServerVar (Help = "Prints info about a clan given its ID")]
	public static void Info (Arg arg)
	{
		if ((Object)(object)ClanManager.ServerInstance == (Object)null) {
			arg.ReplyWith ("ClanManager is null!");
			return;
		}
		long clanId = arg.GetLong (0, 0L);
		if (clanId == 0L) {
			BasePlayer basePlayer = arg.Player ();
			if ((Object)(object)basePlayer == (Object)null) {
				arg.ReplyWith ("Usage: clan.info <clanID>");
			} else {
				SendClanInfoPlayer (basePlayer);
			}
		} else {
			SendClanInfoConsole (clanId);
		}
		static string FormatClan (IClan clan)
		{
			//IL_006e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0073: Unknown result type (might be due to invalid IL or missing references)
			//IL_0099: Unknown result type (might be due to invalid IL or missing references)
			//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
			//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
			//IL_0101: Unknown result type (might be due to invalid IL or missing references)
			StringBuilder stringBuilder = new StringBuilder ();
			stringBuilder.AppendLine ($"Clan ID: {clan.ClanId}");
			stringBuilder.AppendLine ("Name: " + clan.Name);
			stringBuilder.AppendLine ("MoTD: " + clan.Motd);
			stringBuilder.AppendLine ("Members:");
			foreach (ClanMember member in clan.Members) {
				ClanRole? val2 = List.TryFindWith<ClanRole, int> ((IReadOnlyCollection<ClanRole>)clan.Roles, (Func<ClanRole, int>)((ClanRole r) => r.RoleId), member.RoleId, (IEqualityComparer<int>)null);
				string text = SingletonComponent<ServerMgr>.Instance.persistance.GetPlayerName (member.SteamId);
				if (text == null) {
					ulong steamId = member.SteamId;
					text = steamId.ToString ("G");
				}
				string text2 = text;
				stringBuilder.AppendLine ("  " + text2 + " (" + val2?.Name + ")");
			}
			return stringBuilder.ToString ();
		}
		static async void SendClanInfoConsole (long id)
		{
			try {
				IClan val = await GetClanByID (id);
				if (val != null) {
					Debug.Log ((object)FormatClan (val));
				}
			} catch (Exception ex) {
				Debug.LogException (ex);
			}
		}
		async void SendClanInfoPlayer (BasePlayer player)
		{
			_ = 1;
			try {
				IClan val3 = ((clanId != 0L) ? (await GetClanByID (clanId)) : (await GetPlayerClan (player)));
				IClan val4 = val3;
				if (val4 != null) {
					string msg = FormatClan (val4);
					player.ConsoleMessage (msg);
				}
			} catch (Exception ex2) {
				Debug.LogException (ex2);
				player.ConsoleMessage (ex2.ToString ());
			}
		}
	}

	[AsyncStateMachine (typeof(<GetPlayerClan>d__2))]
	private static System.Threading.Tasks.ValueTask<IClan> GetPlayerClan (BasePlayer player)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		<GetPlayerClan>d__2 <GetPlayerClan>d__ = default(<GetPlayerClan>d__2);
		<GetPlayerClan>d__.player = player;
		<GetPlayerClan>d__.<>t__builder = AsyncValueTaskMethodBuilder<IClan>.Create ();
		<GetPlayerClan>d__.<>1__state = -1;
		AsyncValueTaskMethodBuilder<IClan> <>t__builder = <GetPlayerClan>d__.<>t__builder;
		<>t__builder.Start<<GetPlayerClan>d__2> (ref <GetPlayerClan>d__);
		return <GetPlayerClan>d__.<>t__builder.Task;
	}

	[AsyncStateMachine (typeof(<GetClanByID>d__3))]
	private static System.Threading.Tasks.ValueTask<IClan> GetClanByID (long clanId, BasePlayer player = null)
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		<GetClanByID>d__3 <GetClanByID>d__ = default(<GetClanByID>d__3);
		<GetClanByID>d__.clanId = clanId;
		<GetClanByID>d__.player = player;
		<GetClanByID>d__.<>t__builder = AsyncValueTaskMethodBuilder<IClan>.Create ();
		<GetClanByID>d__.<>1__state = -1;
		AsyncValueTaskMethodBuilder<IClan> <>t__builder = <GetClanByID>d__.<>t__builder;
		<>t__builder.Start<<GetClanByID>d__3> (ref <GetClanByID>d__);
		return <GetClanByID>d__.<>t__builder.Task;
	}
}
