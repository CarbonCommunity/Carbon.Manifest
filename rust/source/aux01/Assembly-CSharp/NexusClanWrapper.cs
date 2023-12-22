using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Facepunch.Extend;
using Facepunch.Nexus;
using Facepunch.Nexus.Models;
using UnityEngine;

public class NexusClanWrapper : IClan
{
	[StructLayout (LayoutKind.Auto)]
	[CompilerGenerated]
	private struct <GetLogs>d__47 : IAsyncStateMachine
	{
		public int <>1__state;

		public AsyncValueTaskMethodBuilder<ClanValueResult<ClanLogs>> <>t__builder;

		public NexusClanWrapper <>4__this;

		public ulong bySteamId;

		public int limit;

		private TaskAwaiter<NexusClanResult<List<ClanLogEntry>>> <>u__1;

		private void MoveNext ()
		{
			//IL_0080: Unknown result type (might be due to invalid IL or missing references)
			//IL_0085: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
			//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
			//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
			//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
			//IL_0126: Unknown result type (might be due to invalid IL or missing references)
			//IL_009c: Unknown result type (might be due to invalid IL or missing references)
			//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
			//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
			//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
			int num = <>1__state;
			NexusClanWrapper nexusClanWrapper = <>4__this;
			ClanValueResult<ClanLogs> result2;
			try {
				TaskAwaiter<NexusClanResult<List<ClanLogEntry>>> awaiter;
				if (num != 0) {
					awaiter = nexusClanWrapper.Internal.GetLogs (NexusClanUtil.GetPlayerId (bySteamId), limit).GetAwaiter ();
					if (!awaiter.IsCompleted) {
						num = (<>1__state = 0);
						<>u__1 = awaiter;
						<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<NexusClanResult<List<ClanLogEntry>>>, <GetLogs>d__47> (ref awaiter, ref this);
						return;
					}
				} else {
					awaiter = <>u__1;
					<>u__1 = default(TaskAwaiter<NexusClanResult<List<ClanLogEntry>>>);
					num = (<>1__state = -1);
				}
				NexusClanResult<List<ClanLogEntry>> result = awaiter.GetResult ();
				List<ClanLogEntry> source = default(List<ClanLogEntry>);
				if (result.IsSuccess && result.TryGetResponse (ref source)) {
					ClanLogs val = default(ClanLogs);
					val.ClanId = nexusClanWrapper.ClanId;
					val.Entries = source.Select (delegate(ClanLogEntry e) {
						//IL_0002: Unknown result type (might be due to invalid IL or missing references)
						//IL_0063: Unknown result type (might be due to invalid IL or missing references)
						ClanLogEntry result3 = default(ClanLogEntry);
						result3.Timestamp = ((ClanLogEntry)(ref e)).Timestamp * 1000;
						result3.EventKey = ((ClanLogEntry)(ref e)).EventKey;
						result3.Arg1 = ((ClanLogEntry)(ref e)).Arg1;
						result3.Arg2 = ((ClanLogEntry)(ref e)).Arg2;
						result3.Arg3 = ((ClanLogEntry)(ref e)).Arg3;
						result3.Arg4 = ((ClanLogEntry)(ref e)).Arg4;
						return result3;
					}).ToList ();
					result2 = ClanValueResult<ClanLogs>.op_Implicit (val);
				} else {
					result2 = ClanValueResult<ClanLogs>.op_Implicit (result.ResultCode.ToClanResult ());
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
	private struct <UpdateLastSeen>d__48 : IAsyncStateMachine
	{
		public int <>1__state;

		public AsyncValueTaskMethodBuilder<ClanResult> <>t__builder;

		public NexusClanWrapper <>4__this;

		public ulong steamId;

		private TaskAwaiter<NexusClanResultCode> <>u__1;

		private void MoveNext ()
		{
			//IL_0074: Unknown result type (might be due to invalid IL or missing references)
			//IL_0079: Unknown result type (might be due to invalid IL or missing references)
			//IL_007e: Unknown result type (might be due to invalid IL or missing references)
			//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
			int num = <>1__state;
			NexusClanWrapper nexusClanWrapper = <>4__this;
			ClanResult result;
			try {
				TaskAwaiter<NexusClanResultCode> awaiter;
				if (num != 0) {
					awaiter = nexusClanWrapper.Internal.UpdateLastSeen (NexusClanUtil.GetPlayerId (steamId)).GetAwaiter ();
					if (!awaiter.IsCompleted) {
						num = (<>1__state = 0);
						<>u__1 = awaiter;
						<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<NexusClanResultCode>, <UpdateLastSeen>d__48> (ref awaiter, ref this);
						return;
					}
				} else {
					awaiter = <>u__1;
					<>u__1 = default(TaskAwaiter<NexusClanResultCode>);
					num = (<>1__state = -1);
				}
				result = awaiter.GetResult ().ToClanResult ();
			} catch (Exception exception) {
				<>1__state = -2;
				<>t__builder.SetException (exception);
				return;
			}
			<>1__state = -2;
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

	[StructLayout (LayoutKind.Auto)]
	[CompilerGenerated]
	private struct <SetMotd>d__49 : IAsyncStateMachine
	{
		public int <>1__state;

		public AsyncValueTaskMethodBuilder<ClanResult> <>t__builder;

		public NexusClanWrapper <>4__this;

		public ulong bySteamId;

		public string newMotd;

		private TaskAwaiter<NexusClanResultCode> <>u__1;

		private void MoveNext ()
		{
			//IL_0136: Unknown result type (might be due to invalid IL or missing references)
			//IL_013b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0140: Unknown result type (might be due to invalid IL or missing references)
			//IL_016a: Unknown result type (might be due to invalid IL or missing references)
			//IL_005c: Unknown result type (might be due to invalid IL or missing references)
			//IL_008a: Unknown result type (might be due to invalid IL or missing references)
			//IL_00af: Unknown result type (might be due to invalid IL or missing references)
			//IL_00df: Unknown result type (might be due to invalid IL or missing references)
			//IL_0042: Unknown result type (might be due to invalid IL or missing references)
			int num = <>1__state;
			NexusClanWrapper nexusClanWrapper = <>4__this;
			ClanResult result;
			try {
				TaskAwaiter<NexusClanResultCode> awaiter;
				if (num == 0) {
					awaiter = <>u__1;
					<>u__1 = default(TaskAwaiter<NexusClanResultCode>);
					num = (<>1__state = -1);
					goto IL_0134;
				}
				if (nexusClanWrapper.CheckRole (bySteamId, (ClanRole r) => r.CanSetMotd)) {
					string playerId = NexusClanUtil.GetPlayerId (bySteamId);
					NexusClan @internal = nexusClanWrapper.Internal;
					ClanVariablesUpdate val = default(ClanVariablesUpdate);
					((ClanVariablesUpdate)(ref val)).Variables = new List<VariableUpdate> (2) {
						new VariableUpdate ("motd", newMotd, (bool?)null, (bool?)null),
						new VariableUpdate ("motd_author", playerId, (bool?)null, (bool?)null)
					};
					((ClanVariablesUpdate)(ref val)).EventKey = "set_motd";
					((ClanVariablesUpdate)(ref val)).Arg1 = playerId;
					((ClanVariablesUpdate)(ref val)).Arg2 = newMotd;
					awaiter = @internal.UpdateVariables (val).GetAwaiter ();
					if (!awaiter.IsCompleted) {
						num = (<>1__state = 0);
						<>u__1 = awaiter;
						<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<NexusClanResultCode>, <SetMotd>d__49> (ref awaiter, ref this);
						return;
					}
					goto IL_0134;
				}
				result = (ClanResult)5;
				goto end_IL_000e;
				IL_0134:
				result = awaiter.GetResult ().ToClanResult ();
				end_IL_000e:;
			} catch (Exception exception) {
				<>1__state = -2;
				<>t__builder.SetException (exception);
				return;
			}
			<>1__state = -2;
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

	[StructLayout (LayoutKind.Auto)]
	[CompilerGenerated]
	private struct <SetLogo>d__50 : IAsyncStateMachine
	{
		public int <>1__state;

		public AsyncValueTaskMethodBuilder<ClanResult> <>t__builder;

		public NexusClanWrapper <>4__this;

		public ulong bySteamId;

		public byte[] newLogo;

		private TaskAwaiter<NexusClanResultCode> <>u__1;

		private void MoveNext ()
		{
			//IL_0109: Unknown result type (might be due to invalid IL or missing references)
			//IL_010e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0113: Unknown result type (might be due to invalid IL or missing references)
			//IL_013d: Unknown result type (might be due to invalid IL or missing references)
			//IL_005c: Unknown result type (might be due to invalid IL or missing references)
			//IL_008f: Unknown result type (might be due to invalid IL or missing references)
			//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
			//IL_0042: Unknown result type (might be due to invalid IL or missing references)
			int num = <>1__state;
			NexusClanWrapper nexusClanWrapper = <>4__this;
			ClanResult result;
			try {
				TaskAwaiter<NexusClanResultCode> awaiter;
				if (num == 0) {
					awaiter = <>u__1;
					<>u__1 = default(TaskAwaiter<NexusClanResultCode>);
					num = (<>1__state = -1);
					goto IL_0107;
				}
				if (nexusClanWrapper.CheckRole (bySteamId, (ClanRole r) => r.CanSetLogo)) {
					string playerId = NexusClanUtil.GetPlayerId (bySteamId);
					NexusClan @internal = nexusClanWrapper.Internal;
					ClanVariablesUpdate val = default(ClanVariablesUpdate);
					((ClanVariablesUpdate)(ref val)).Variables = new List<VariableUpdate> (1) {
						new VariableUpdate ("logo", System.Memory<byte>.op_Implicit (newLogo), (bool?)null, (bool?)null)
					};
					((ClanVariablesUpdate)(ref val)).EventKey = "set_logo";
					((ClanVariablesUpdate)(ref val)).Arg1 = playerId;
					awaiter = @internal.UpdateVariables (val).GetAwaiter ();
					if (!awaiter.IsCompleted) {
						num = (<>1__state = 0);
						<>u__1 = awaiter;
						<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<NexusClanResultCode>, <SetLogo>d__50> (ref awaiter, ref this);
						return;
					}
					goto IL_0107;
				}
				result = (ClanResult)5;
				goto end_IL_000e;
				IL_0107:
				result = awaiter.GetResult ().ToClanResult ();
				end_IL_000e:;
			} catch (Exception exception) {
				<>1__state = -2;
				<>t__builder.SetException (exception);
				return;
			}
			<>1__state = -2;
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

	[StructLayout (LayoutKind.Auto)]
	[CompilerGenerated]
	private struct <SetColor>d__51 : IAsyncStateMachine
	{
		public int <>1__state;

		public AsyncValueTaskMethodBuilder<ClanResult> <>t__builder;

		public NexusClanWrapper <>4__this;

		public ulong bySteamId;

		public Color32 newColor;

		private TaskAwaiter<NexusClanResultCode> <>u__1;

		private void MoveNext ()
		{
			//IL_0129: Unknown result type (might be due to invalid IL or missing references)
			//IL_012e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0133: Unknown result type (might be due to invalid IL or missing references)
			//IL_015d: Unknown result type (might be due to invalid IL or missing references)
			//IL_005c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0071: Unknown result type (might be due to invalid IL or missing references)
			//IL_009d: Unknown result type (might be due to invalid IL or missing references)
			//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
			//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
			//IL_0042: Unknown result type (might be due to invalid IL or missing references)
			int num = <>1__state;
			NexusClanWrapper nexusClanWrapper = <>4__this;
			ClanResult result;
			try {
				TaskAwaiter<NexusClanResultCode> awaiter;
				if (num == 0) {
					awaiter = <>u__1;
					<>u__1 = default(TaskAwaiter<NexusClanResultCode>);
					num = (<>1__state = -1);
					goto IL_0127;
				}
				if (nexusClanWrapper.CheckRole (bySteamId, (ClanRole r) => r.CanSetLogo)) {
					string playerId = NexusClanUtil.GetPlayerId (bySteamId);
					NexusClan @internal = nexusClanWrapper.Internal;
					ClanVariablesUpdate val = default(ClanVariablesUpdate);
					((ClanVariablesUpdate)(ref val)).Variables = new List<VariableUpdate> (1) {
						new VariableUpdate ("color", ColorEx.ToInt32 (newColor).ToString ("G"), (bool?)null, (bool?)null)
					};
					((ClanVariablesUpdate)(ref val)).EventKey = "set_color";
					((ClanVariablesUpdate)(ref val)).Arg1 = playerId;
					((ClanVariablesUpdate)(ref val)).Arg2 = ColorEx.ToHex (newColor);
					awaiter = @internal.UpdateVariables (val).GetAwaiter ();
					if (!awaiter.IsCompleted) {
						num = (<>1__state = 0);
						<>u__1 = awaiter;
						<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<NexusClanResultCode>, <SetColor>d__51> (ref awaiter, ref this);
						return;
					}
					goto IL_0127;
				}
				result = (ClanResult)5;
				goto end_IL_000e;
				IL_0127:
				result = awaiter.GetResult ().ToClanResult ();
				end_IL_000e:;
			} catch (Exception exception) {
				<>1__state = -2;
				<>t__builder.SetException (exception);
				return;
			}
			<>1__state = -2;
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

	[StructLayout (LayoutKind.Auto)]
	[CompilerGenerated]
	private struct <Invite>d__52 : IAsyncStateMachine
	{
		public int <>1__state;

		public AsyncValueTaskMethodBuilder<ClanResult> <>t__builder;

		public NexusClanWrapper <>4__this;

		public ulong steamId;

		public ulong bySteamId;

		private TaskAwaiter<NexusClanResultCode> <>u__1;

		private void MoveNext ()
		{
			//IL_007f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0084: Unknown result type (might be due to invalid IL or missing references)
			//IL_0089: Unknown result type (might be due to invalid IL or missing references)
			//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
			int num = <>1__state;
			NexusClanWrapper nexusClanWrapper = <>4__this;
			ClanResult result;
			try {
				TaskAwaiter<NexusClanResultCode> awaiter;
				if (num != 0) {
					awaiter = nexusClanWrapper.Internal.Invite (NexusClanUtil.GetPlayerId (steamId), NexusClanUtil.GetPlayerId (bySteamId)).GetAwaiter ();
					if (!awaiter.IsCompleted) {
						num = (<>1__state = 0);
						<>u__1 = awaiter;
						<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<NexusClanResultCode>, <Invite>d__52> (ref awaiter, ref this);
						return;
					}
				} else {
					awaiter = <>u__1;
					<>u__1 = default(TaskAwaiter<NexusClanResultCode>);
					num = (<>1__state = -1);
				}
				result = awaiter.GetResult ().ToClanResult ();
			} catch (Exception exception) {
				<>1__state = -2;
				<>t__builder.SetException (exception);
				return;
			}
			<>1__state = -2;
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

	[StructLayout (LayoutKind.Auto)]
	[CompilerGenerated]
	private struct <CancelInvite>d__53 : IAsyncStateMachine
	{
		public int <>1__state;

		public AsyncValueTaskMethodBuilder<ClanResult> <>t__builder;

		public NexusClanWrapper <>4__this;

		public ulong steamId;

		public ulong bySteamId;

		private TaskAwaiter<NexusClanResultCode> <>u__1;

		private void MoveNext ()
		{
			//IL_007f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0084: Unknown result type (might be due to invalid IL or missing references)
			//IL_0089: Unknown result type (might be due to invalid IL or missing references)
			//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
			int num = <>1__state;
			NexusClanWrapper nexusClanWrapper = <>4__this;
			ClanResult result;
			try {
				TaskAwaiter<NexusClanResultCode> awaiter;
				if (num != 0) {
					awaiter = nexusClanWrapper.Internal.CancelInvite (NexusClanUtil.GetPlayerId (steamId), NexusClanUtil.GetPlayerId (bySteamId)).GetAwaiter ();
					if (!awaiter.IsCompleted) {
						num = (<>1__state = 0);
						<>u__1 = awaiter;
						<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<NexusClanResultCode>, <CancelInvite>d__53> (ref awaiter, ref this);
						return;
					}
				} else {
					awaiter = <>u__1;
					<>u__1 = default(TaskAwaiter<NexusClanResultCode>);
					num = (<>1__state = -1);
				}
				result = awaiter.GetResult ().ToClanResult ();
			} catch (Exception exception) {
				<>1__state = -2;
				<>t__builder.SetException (exception);
				return;
			}
			<>1__state = -2;
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

	[StructLayout (LayoutKind.Auto)]
	[CompilerGenerated]
	private struct <AcceptInvite>d__54 : IAsyncStateMachine
	{
		public int <>1__state;

		public AsyncValueTaskMethodBuilder<ClanResult> <>t__builder;

		public NexusClanWrapper <>4__this;

		public ulong steamId;

		private TaskAwaiter<NexusClanResultCode> <>u__1;

		private void MoveNext ()
		{
			//IL_0074: Unknown result type (might be due to invalid IL or missing references)
			//IL_0079: Unknown result type (might be due to invalid IL or missing references)
			//IL_007e: Unknown result type (might be due to invalid IL or missing references)
			//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
			int num = <>1__state;
			NexusClanWrapper nexusClanWrapper = <>4__this;
			ClanResult result;
			try {
				TaskAwaiter<NexusClanResultCode> awaiter;
				if (num != 0) {
					awaiter = nexusClanWrapper.Internal.AcceptInvite (NexusClanUtil.GetPlayerId (steamId)).GetAwaiter ();
					if (!awaiter.IsCompleted) {
						num = (<>1__state = 0);
						<>u__1 = awaiter;
						<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<NexusClanResultCode>, <AcceptInvite>d__54> (ref awaiter, ref this);
						return;
					}
				} else {
					awaiter = <>u__1;
					<>u__1 = default(TaskAwaiter<NexusClanResultCode>);
					num = (<>1__state = -1);
				}
				result = awaiter.GetResult ().ToClanResult ();
			} catch (Exception exception) {
				<>1__state = -2;
				<>t__builder.SetException (exception);
				return;
			}
			<>1__state = -2;
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

	[StructLayout (LayoutKind.Auto)]
	[CompilerGenerated]
	private struct <Kick>d__55 : IAsyncStateMachine
	{
		public int <>1__state;

		public AsyncValueTaskMethodBuilder<ClanResult> <>t__builder;

		public NexusClanWrapper <>4__this;

		public ulong steamId;

		public ulong bySteamId;

		private TaskAwaiter<NexusClanResultCode> <>u__1;

		private void MoveNext ()
		{
			//IL_007f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0084: Unknown result type (might be due to invalid IL or missing references)
			//IL_0089: Unknown result type (might be due to invalid IL or missing references)
			//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
			int num = <>1__state;
			NexusClanWrapper nexusClanWrapper = <>4__this;
			ClanResult result;
			try {
				TaskAwaiter<NexusClanResultCode> awaiter;
				if (num != 0) {
					awaiter = nexusClanWrapper.Internal.Kick (NexusClanUtil.GetPlayerId (steamId), NexusClanUtil.GetPlayerId (bySteamId)).GetAwaiter ();
					if (!awaiter.IsCompleted) {
						num = (<>1__state = 0);
						<>u__1 = awaiter;
						<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<NexusClanResultCode>, <Kick>d__55> (ref awaiter, ref this);
						return;
					}
				} else {
					awaiter = <>u__1;
					<>u__1 = default(TaskAwaiter<NexusClanResultCode>);
					num = (<>1__state = -1);
				}
				result = awaiter.GetResult ().ToClanResult ();
			} catch (Exception exception) {
				<>1__state = -2;
				<>t__builder.SetException (exception);
				return;
			}
			<>1__state = -2;
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

	[StructLayout (LayoutKind.Auto)]
	[CompilerGenerated]
	private struct <SetPlayerRole>d__56 : IAsyncStateMachine
	{
		public int <>1__state;

		public AsyncValueTaskMethodBuilder<ClanResult> <>t__builder;

		public NexusClanWrapper <>4__this;

		public ulong steamId;

		public int newRoleId;

		public ulong bySteamId;

		private TaskAwaiter<NexusClanResultCode> <>u__1;

		private void MoveNext ()
		{
			//IL_0085: Unknown result type (might be due to invalid IL or missing references)
			//IL_008a: Unknown result type (might be due to invalid IL or missing references)
			//IL_008f: Unknown result type (might be due to invalid IL or missing references)
			//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
			int num = <>1__state;
			NexusClanWrapper nexusClanWrapper = <>4__this;
			ClanResult result;
			try {
				TaskAwaiter<NexusClanResultCode> awaiter;
				if (num != 0) {
					awaiter = nexusClanWrapper.Internal.SetPlayerRole (NexusClanUtil.GetPlayerId (steamId), newRoleId, NexusClanUtil.GetPlayerId (bySteamId)).GetAwaiter ();
					if (!awaiter.IsCompleted) {
						num = (<>1__state = 0);
						<>u__1 = awaiter;
						<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<NexusClanResultCode>, <SetPlayerRole>d__56> (ref awaiter, ref this);
						return;
					}
				} else {
					awaiter = <>u__1;
					<>u__1 = default(TaskAwaiter<NexusClanResultCode>);
					num = (<>1__state = -1);
				}
				result = awaiter.GetResult ().ToClanResult ();
			} catch (Exception exception) {
				<>1__state = -2;
				<>t__builder.SetException (exception);
				return;
			}
			<>1__state = -2;
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

	[StructLayout (LayoutKind.Auto)]
	[CompilerGenerated]
	private struct <SetPlayerNotes>d__57 : IAsyncStateMachine
	{
		public int <>1__state;

		public AsyncValueTaskMethodBuilder<ClanResult> <>t__builder;

		public NexusClanWrapper <>4__this;

		public ulong bySteamId;

		public ulong steamId;

		public string notes;

		private TaskAwaiter<NexusClanResultCode> <>u__1;

		private void MoveNext ()
		{
			//IL_0128: Unknown result type (might be due to invalid IL or missing references)
			//IL_012d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0132: Unknown result type (might be due to invalid IL or missing references)
			//IL_015c: Unknown result type (might be due to invalid IL or missing references)
			//IL_006a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0098: Unknown result type (might be due to invalid IL or missing references)
			//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
			//IL_0042: Unknown result type (might be due to invalid IL or missing references)
			int num = <>1__state;
			NexusClanWrapper nexusClanWrapper = <>4__this;
			ClanResult result;
			try {
				TaskAwaiter<NexusClanResultCode> awaiter;
				if (num == 0) {
					awaiter = <>u__1;
					<>u__1 = default(TaskAwaiter<NexusClanResultCode>);
					num = (<>1__state = -1);
					goto IL_0126;
				}
				if (nexusClanWrapper.CheckRole (bySteamId, (ClanRole r) => r.CanSetPlayerNotes)) {
					string playerId = NexusClanUtil.GetPlayerId (steamId);
					string playerId2 = NexusClanUtil.GetPlayerId (bySteamId);
					NexusClan @internal = nexusClanWrapper.Internal;
					ClanVariablesUpdate val = default(ClanVariablesUpdate);
					((ClanVariablesUpdate)(ref val)).Variables = new List<VariableUpdate> (1) {
						new VariableUpdate ("notes", notes, (bool?)null, (bool?)null)
					};
					((ClanVariablesUpdate)(ref val)).EventKey = "set_notes";
					((ClanVariablesUpdate)(ref val)).Arg1 = playerId2;
					((ClanVariablesUpdate)(ref val)).Arg2 = playerId;
					((ClanVariablesUpdate)(ref val)).Arg3 = notes;
					awaiter = @internal.UpdatePlayerVariables (playerId, val).GetAwaiter ();
					if (!awaiter.IsCompleted) {
						num = (<>1__state = 0);
						<>u__1 = awaiter;
						<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<NexusClanResultCode>, <SetPlayerNotes>d__57> (ref awaiter, ref this);
						return;
					}
					goto IL_0126;
				}
				result = (ClanResult)5;
				goto end_IL_000e;
				IL_0126:
				result = awaiter.GetResult ().ToClanResult ();
				end_IL_000e:;
			} catch (Exception exception) {
				<>1__state = -2;
				<>t__builder.SetException (exception);
				return;
			}
			<>1__state = -2;
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

	[StructLayout (LayoutKind.Auto)]
	[CompilerGenerated]
	private struct <CreateRole>d__58 : IAsyncStateMachine
	{
		public int <>1__state;

		public AsyncValueTaskMethodBuilder<ClanResult> <>t__builder;

		public NexusClanWrapper <>4__this;

		public ClanRole role;

		public ulong bySteamId;

		private TaskAwaiter<NexusClanResultCode> <>u__1;

		private void MoveNext ()
		{
			//IL_0018: Unknown result type (might be due to invalid IL or missing references)
			//IL_001d: Unknown result type (might be due to invalid IL or missing references)
			//IL_007f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0084: Unknown result type (might be due to invalid IL or missing references)
			//IL_0089: Unknown result type (might be due to invalid IL or missing references)
			//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
			int num = <>1__state;
			NexusClanWrapper nexusClanWrapper = <>4__this;
			ClanResult result;
			try {
				TaskAwaiter<NexusClanResultCode> awaiter;
				if (num != 0) {
					awaiter = nexusClanWrapper.Internal.CreateRole (role.ToRoleParameters (), NexusClanUtil.GetPlayerId (bySteamId)).GetAwaiter ();
					if (!awaiter.IsCompleted) {
						num = (<>1__state = 0);
						<>u__1 = awaiter;
						<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<NexusClanResultCode>, <CreateRole>d__58> (ref awaiter, ref this);
						return;
					}
				} else {
					awaiter = <>u__1;
					<>u__1 = default(TaskAwaiter<NexusClanResultCode>);
					num = (<>1__state = -1);
				}
				result = awaiter.GetResult ().ToClanResult ();
			} catch (Exception exception) {
				<>1__state = -2;
				<>t__builder.SetException (exception);
				return;
			}
			<>1__state = -2;
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

	[StructLayout (LayoutKind.Auto)]
	[CompilerGenerated]
	private struct <UpdateRole>d__59 : IAsyncStateMachine
	{
		public int <>1__state;

		public AsyncValueTaskMethodBuilder<ClanResult> <>t__builder;

		public NexusClanWrapper <>4__this;

		public ClanRole role;

		public ulong bySteamId;

		private TaskAwaiter<NexusClanResultCode> <>u__1;

		private void MoveNext ()
		{
			//IL_0023: Unknown result type (might be due to invalid IL or missing references)
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			//IL_008a: Unknown result type (might be due to invalid IL or missing references)
			//IL_008f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0094: Unknown result type (might be due to invalid IL or missing references)
			//IL_00be: Unknown result type (might be due to invalid IL or missing references)
			int num = <>1__state;
			NexusClanWrapper nexusClanWrapper = <>4__this;
			ClanResult result;
			try {
				TaskAwaiter<NexusClanResultCode> awaiter;
				if (num != 0) {
					awaiter = nexusClanWrapper.Internal.UpdateRole (role.RoleId, role.ToRoleParameters (), NexusClanUtil.GetPlayerId (bySteamId)).GetAwaiter ();
					if (!awaiter.IsCompleted) {
						num = (<>1__state = 0);
						<>u__1 = awaiter;
						<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<NexusClanResultCode>, <UpdateRole>d__59> (ref awaiter, ref this);
						return;
					}
				} else {
					awaiter = <>u__1;
					<>u__1 = default(TaskAwaiter<NexusClanResultCode>);
					num = (<>1__state = -1);
				}
				result = awaiter.GetResult ().ToClanResult ();
			} catch (Exception exception) {
				<>1__state = -2;
				<>t__builder.SetException (exception);
				return;
			}
			<>1__state = -2;
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

	[StructLayout (LayoutKind.Auto)]
	[CompilerGenerated]
	private struct <SwapRoleRanks>d__60 : IAsyncStateMachine
	{
		public int <>1__state;

		public AsyncValueTaskMethodBuilder<ClanResult> <>t__builder;

		public NexusClanWrapper <>4__this;

		public int roleIdA;

		public int roleIdB;

		public ulong bySteamId;

		private TaskAwaiter<NexusClanResultCode> <>u__1;

		private void MoveNext ()
		{
			//IL_0080: Unknown result type (might be due to invalid IL or missing references)
			//IL_0085: Unknown result type (might be due to invalid IL or missing references)
			//IL_008a: Unknown result type (might be due to invalid IL or missing references)
			//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
			int num = <>1__state;
			NexusClanWrapper nexusClanWrapper = <>4__this;
			ClanResult result;
			try {
				TaskAwaiter<NexusClanResultCode> awaiter;
				if (num != 0) {
					awaiter = nexusClanWrapper.Internal.SwapRoleRanks (roleIdA, roleIdB, NexusClanUtil.GetPlayerId (bySteamId)).GetAwaiter ();
					if (!awaiter.IsCompleted) {
						num = (<>1__state = 0);
						<>u__1 = awaiter;
						<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<NexusClanResultCode>, <SwapRoleRanks>d__60> (ref awaiter, ref this);
						return;
					}
				} else {
					awaiter = <>u__1;
					<>u__1 = default(TaskAwaiter<NexusClanResultCode>);
					num = (<>1__state = -1);
				}
				result = awaiter.GetResult ().ToClanResult ();
			} catch (Exception exception) {
				<>1__state = -2;
				<>t__builder.SetException (exception);
				return;
			}
			<>1__state = -2;
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

	[StructLayout (LayoutKind.Auto)]
	[CompilerGenerated]
	private struct <DeleteRole>d__61 : IAsyncStateMachine
	{
		public int <>1__state;

		public AsyncValueTaskMethodBuilder<ClanResult> <>t__builder;

		public NexusClanWrapper <>4__this;

		public int roleId;

		public ulong bySteamId;

		private TaskAwaiter<NexusClanResultCode> <>u__1;

		private void MoveNext ()
		{
			//IL_007a: Unknown result type (might be due to invalid IL or missing references)
			//IL_007f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0084: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
			int num = <>1__state;
			NexusClanWrapper nexusClanWrapper = <>4__this;
			ClanResult result;
			try {
				TaskAwaiter<NexusClanResultCode> awaiter;
				if (num != 0) {
					awaiter = nexusClanWrapper.Internal.DeleteRole (roleId, NexusClanUtil.GetPlayerId (bySteamId)).GetAwaiter ();
					if (!awaiter.IsCompleted) {
						num = (<>1__state = 0);
						<>u__1 = awaiter;
						<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<NexusClanResultCode>, <DeleteRole>d__61> (ref awaiter, ref this);
						return;
					}
				} else {
					awaiter = <>u__1;
					<>u__1 = default(TaskAwaiter<NexusClanResultCode>);
					num = (<>1__state = -1);
				}
				result = awaiter.GetResult ().ToClanResult ();
			} catch (Exception exception) {
				<>1__state = -2;
				<>t__builder.SetException (exception);
				return;
			}
			<>1__state = -2;
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

	[StructLayout (LayoutKind.Auto)]
	[CompilerGenerated]
	private struct <Disband>d__62 : IAsyncStateMachine
	{
		public int <>1__state;

		public AsyncValueTaskMethodBuilder<ClanResult> <>t__builder;

		public NexusClanWrapper <>4__this;

		public ulong bySteamId;

		private TaskAwaiter<NexusClanResultCode> <>u__1;

		private void MoveNext ()
		{
			//IL_0074: Unknown result type (might be due to invalid IL or missing references)
			//IL_0079: Unknown result type (might be due to invalid IL or missing references)
			//IL_007e: Unknown result type (might be due to invalid IL or missing references)
			//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
			int num = <>1__state;
			NexusClanWrapper nexusClanWrapper = <>4__this;
			ClanResult result;
			try {
				TaskAwaiter<NexusClanResultCode> awaiter;
				if (num != 0) {
					awaiter = nexusClanWrapper.Internal.Disband (NexusClanUtil.GetPlayerId (bySteamId)).GetAwaiter ();
					if (!awaiter.IsCompleted) {
						num = (<>1__state = 0);
						<>u__1 = awaiter;
						<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<NexusClanResultCode>, <Disband>d__62> (ref awaiter, ref this);
						return;
					}
				} else {
					awaiter = <>u__1;
					<>u__1 = default(TaskAwaiter<NexusClanResultCode>);
					num = (<>1__state = -1);
				}
				result = awaiter.GetResult ().ToClanResult ();
			} catch (Exception exception) {
				<>1__state = -2;
				<>t__builder.SetException (exception);
				return;
			}
			<>1__state = -2;
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

	private const int MaxChatScrollback = 20;

	public readonly NexusClan Internal;

	private readonly NexusClanChatCollector _chatCollector;

	private readonly List<ClanRole> _roles;

	private readonly List<ClanMember> _members;

	private readonly List<ClanInvite> _invites;

	private readonly List<ClanChatEntry> _chatHistory;

	public long ClanId => Internal.ClanId;

	public string Name => Internal.Name;

	public long Created => Internal.Created;

	public ulong Creator => NexusClanUtil.GetSteamId (Internal.Creator);

	public string Motd { get; private set; }

	public long MotdTimestamp { get; private set; }

	public ulong MotdAuthor { get; private set; }

	public byte[] Logo { get; private set; }

	public Color32 Color { get; private set; }

	public IReadOnlyList<ClanRole> Roles => _roles;

	public IReadOnlyList<ClanMember> Members => _members;

	public int MaxMemberCount { get; private set; }

	public IReadOnlyList<ClanInvite> Invites => _invites;

	public NexusClanWrapper (NexusClan clan, NexusClanChatCollector chatCollector)
	{
		Internal = clan ?? throw new ArgumentNullException ("clan");
		_chatCollector = chatCollector ?? throw new ArgumentNullException ("chatCollector");
		_roles = new List<ClanRole> ();
		_members = new List<ClanMember> ();
		_invites = new List<ClanInvite> ();
		_chatHistory = new List<ClanChatEntry> (20);
		UpdateValuesInternal ();
	}

	public void UpdateValuesInternal ()
	{
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		Internal.GetMotd (out var motd, out var motdTimestamp, out var motdAuthor);
		Motd = motd;
		MotdTimestamp = motdTimestamp;
		MotdAuthor = motdAuthor;
		Internal.GetBanner (out var logo, out var color);
		Logo = logo;
		Color = color;
		List.Resize<ClanRole> (_roles, Internal.Roles.Count);
		for (int i = 0; i < _roles.Count; i++) {
			_roles [i] = Internal.Roles [i].ToClanRole ();
		}
		List.Resize<ClanMember> (_members, Internal.Members.Count);
		for (int j = 0; j < _members.Count; j++) {
			_members [j] = Internal.Members [j].ToClanMember ();
		}
		MaxMemberCount = Internal.MaxMemberCount;
		List.Resize<ClanInvite> (_invites, Internal.Invites.Count);
		for (int k = 0; k < _invites.Count; k++) {
			_invites [k] = Internal.Invites [k].ToClanInvite ();
		}
	}

	[AsyncStateMachine (typeof(<GetLogs>d__47))]
	public System.Threading.Tasks.ValueTask<ClanValueResult<ClanLogs>> GetLogs (int limit, ulong bySteamId)
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		<GetLogs>d__47 <GetLogs>d__ = default(<GetLogs>d__47);
		<GetLogs>d__.<>4__this = this;
		<GetLogs>d__.limit = limit;
		<GetLogs>d__.bySteamId = bySteamId;
		<GetLogs>d__.<>t__builder = AsyncValueTaskMethodBuilder<ClanValueResult<ClanLogs>>.Create ();
		<GetLogs>d__.<>1__state = -1;
		AsyncValueTaskMethodBuilder<ClanValueResult<ClanLogs>> <>t__builder = <GetLogs>d__.<>t__builder;
		<>t__builder.Start<<GetLogs>d__47> (ref <GetLogs>d__);
		return <GetLogs>d__.<>t__builder.Task;
	}

	[AsyncStateMachine (typeof(<UpdateLastSeen>d__48))]
	public System.Threading.Tasks.ValueTask<ClanResult> UpdateLastSeen (ulong steamId)
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		<UpdateLastSeen>d__48 <UpdateLastSeen>d__ = default(<UpdateLastSeen>d__48);
		<UpdateLastSeen>d__.<>4__this = this;
		<UpdateLastSeen>d__.steamId = steamId;
		<UpdateLastSeen>d__.<>t__builder = AsyncValueTaskMethodBuilder<ClanResult>.Create ();
		<UpdateLastSeen>d__.<>1__state = -1;
		AsyncValueTaskMethodBuilder<ClanResult> <>t__builder = <UpdateLastSeen>d__.<>t__builder;
		<>t__builder.Start<<UpdateLastSeen>d__48> (ref <UpdateLastSeen>d__);
		return <UpdateLastSeen>d__.<>t__builder.Task;
	}

	[AsyncStateMachine (typeof(<SetMotd>d__49))]
	public System.Threading.Tasks.ValueTask<ClanResult> SetMotd (string newMotd, ulong bySteamId)
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		<SetMotd>d__49 <SetMotd>d__ = default(<SetMotd>d__49);
		<SetMotd>d__.<>4__this = this;
		<SetMotd>d__.newMotd = newMotd;
		<SetMotd>d__.bySteamId = bySteamId;
		<SetMotd>d__.<>t__builder = AsyncValueTaskMethodBuilder<ClanResult>.Create ();
		<SetMotd>d__.<>1__state = -1;
		AsyncValueTaskMethodBuilder<ClanResult> <>t__builder = <SetMotd>d__.<>t__builder;
		<>t__builder.Start<<SetMotd>d__49> (ref <SetMotd>d__);
		return <SetMotd>d__.<>t__builder.Task;
	}

	[AsyncStateMachine (typeof(<SetLogo>d__50))]
	public System.Threading.Tasks.ValueTask<ClanResult> SetLogo (byte[] newLogo, ulong bySteamId)
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		<SetLogo>d__50 <SetLogo>d__ = default(<SetLogo>d__50);
		<SetLogo>d__.<>4__this = this;
		<SetLogo>d__.newLogo = newLogo;
		<SetLogo>d__.bySteamId = bySteamId;
		<SetLogo>d__.<>t__builder = AsyncValueTaskMethodBuilder<ClanResult>.Create ();
		<SetLogo>d__.<>1__state = -1;
		AsyncValueTaskMethodBuilder<ClanResult> <>t__builder = <SetLogo>d__.<>t__builder;
		<>t__builder.Start<<SetLogo>d__50> (ref <SetLogo>d__);
		return <SetLogo>d__.<>t__builder.Task;
	}

	[AsyncStateMachine (typeof(<SetColor>d__51))]
	public System.Threading.Tasks.ValueTask<ClanResult> SetColor (Color32 newColor, ulong bySteamId)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		<SetColor>d__51 <SetColor>d__ = default(<SetColor>d__51);
		<SetColor>d__.<>4__this = this;
		<SetColor>d__.newColor = newColor;
		<SetColor>d__.bySteamId = bySteamId;
		<SetColor>d__.<>t__builder = AsyncValueTaskMethodBuilder<ClanResult>.Create ();
		<SetColor>d__.<>1__state = -1;
		AsyncValueTaskMethodBuilder<ClanResult> <>t__builder = <SetColor>d__.<>t__builder;
		<>t__builder.Start<<SetColor>d__51> (ref <SetColor>d__);
		return <SetColor>d__.<>t__builder.Task;
	}

	[AsyncStateMachine (typeof(<Invite>d__52))]
	public System.Threading.Tasks.ValueTask<ClanResult> Invite (ulong steamId, ulong bySteamId)
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		<Invite>d__52 <Invite>d__ = default(<Invite>d__52);
		<Invite>d__.<>4__this = this;
		<Invite>d__.steamId = steamId;
		<Invite>d__.bySteamId = bySteamId;
		<Invite>d__.<>t__builder = AsyncValueTaskMethodBuilder<ClanResult>.Create ();
		<Invite>d__.<>1__state = -1;
		AsyncValueTaskMethodBuilder<ClanResult> <>t__builder = <Invite>d__.<>t__builder;
		<>t__builder.Start<<Invite>d__52> (ref <Invite>d__);
		return <Invite>d__.<>t__builder.Task;
	}

	[AsyncStateMachine (typeof(<CancelInvite>d__53))]
	public System.Threading.Tasks.ValueTask<ClanResult> CancelInvite (ulong steamId, ulong bySteamId)
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		<CancelInvite>d__53 <CancelInvite>d__ = default(<CancelInvite>d__53);
		<CancelInvite>d__.<>4__this = this;
		<CancelInvite>d__.steamId = steamId;
		<CancelInvite>d__.bySteamId = bySteamId;
		<CancelInvite>d__.<>t__builder = AsyncValueTaskMethodBuilder<ClanResult>.Create ();
		<CancelInvite>d__.<>1__state = -1;
		AsyncValueTaskMethodBuilder<ClanResult> <>t__builder = <CancelInvite>d__.<>t__builder;
		<>t__builder.Start<<CancelInvite>d__53> (ref <CancelInvite>d__);
		return <CancelInvite>d__.<>t__builder.Task;
	}

	[AsyncStateMachine (typeof(<AcceptInvite>d__54))]
	public System.Threading.Tasks.ValueTask<ClanResult> AcceptInvite (ulong steamId)
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		<AcceptInvite>d__54 <AcceptInvite>d__ = default(<AcceptInvite>d__54);
		<AcceptInvite>d__.<>4__this = this;
		<AcceptInvite>d__.steamId = steamId;
		<AcceptInvite>d__.<>t__builder = AsyncValueTaskMethodBuilder<ClanResult>.Create ();
		<AcceptInvite>d__.<>1__state = -1;
		AsyncValueTaskMethodBuilder<ClanResult> <>t__builder = <AcceptInvite>d__.<>t__builder;
		<>t__builder.Start<<AcceptInvite>d__54> (ref <AcceptInvite>d__);
		return <AcceptInvite>d__.<>t__builder.Task;
	}

	[AsyncStateMachine (typeof(<Kick>d__55))]
	public System.Threading.Tasks.ValueTask<ClanResult> Kick (ulong steamId, ulong bySteamId)
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		<Kick>d__55 <Kick>d__ = default(<Kick>d__55);
		<Kick>d__.<>4__this = this;
		<Kick>d__.steamId = steamId;
		<Kick>d__.bySteamId = bySteamId;
		<Kick>d__.<>t__builder = AsyncValueTaskMethodBuilder<ClanResult>.Create ();
		<Kick>d__.<>1__state = -1;
		AsyncValueTaskMethodBuilder<ClanResult> <>t__builder = <Kick>d__.<>t__builder;
		<>t__builder.Start<<Kick>d__55> (ref <Kick>d__);
		return <Kick>d__.<>t__builder.Task;
	}

	[AsyncStateMachine (typeof(<SetPlayerRole>d__56))]
	public System.Threading.Tasks.ValueTask<ClanResult> SetPlayerRole (ulong steamId, int newRoleId, ulong bySteamId)
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		<SetPlayerRole>d__56 <SetPlayerRole>d__ = default(<SetPlayerRole>d__56);
		<SetPlayerRole>d__.<>4__this = this;
		<SetPlayerRole>d__.steamId = steamId;
		<SetPlayerRole>d__.newRoleId = newRoleId;
		<SetPlayerRole>d__.bySteamId = bySteamId;
		<SetPlayerRole>d__.<>t__builder = AsyncValueTaskMethodBuilder<ClanResult>.Create ();
		<SetPlayerRole>d__.<>1__state = -1;
		AsyncValueTaskMethodBuilder<ClanResult> <>t__builder = <SetPlayerRole>d__.<>t__builder;
		<>t__builder.Start<<SetPlayerRole>d__56> (ref <SetPlayerRole>d__);
		return <SetPlayerRole>d__.<>t__builder.Task;
	}

	[AsyncStateMachine (typeof(<SetPlayerNotes>d__57))]
	public System.Threading.Tasks.ValueTask<ClanResult> SetPlayerNotes (ulong steamId, string notes, ulong bySteamId)
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		<SetPlayerNotes>d__57 <SetPlayerNotes>d__ = default(<SetPlayerNotes>d__57);
		<SetPlayerNotes>d__.<>4__this = this;
		<SetPlayerNotes>d__.steamId = steamId;
		<SetPlayerNotes>d__.notes = notes;
		<SetPlayerNotes>d__.bySteamId = bySteamId;
		<SetPlayerNotes>d__.<>t__builder = AsyncValueTaskMethodBuilder<ClanResult>.Create ();
		<SetPlayerNotes>d__.<>1__state = -1;
		AsyncValueTaskMethodBuilder<ClanResult> <>t__builder = <SetPlayerNotes>d__.<>t__builder;
		<>t__builder.Start<<SetPlayerNotes>d__57> (ref <SetPlayerNotes>d__);
		return <SetPlayerNotes>d__.<>t__builder.Task;
	}

	[AsyncStateMachine (typeof(<CreateRole>d__58))]
	public System.Threading.Tasks.ValueTask<ClanResult> CreateRole (ClanRole role, ulong bySteamId)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		<CreateRole>d__58 <CreateRole>d__ = default(<CreateRole>d__58);
		<CreateRole>d__.<>4__this = this;
		<CreateRole>d__.role = role;
		<CreateRole>d__.bySteamId = bySteamId;
		<CreateRole>d__.<>t__builder = AsyncValueTaskMethodBuilder<ClanResult>.Create ();
		<CreateRole>d__.<>1__state = -1;
		AsyncValueTaskMethodBuilder<ClanResult> <>t__builder = <CreateRole>d__.<>t__builder;
		<>t__builder.Start<<CreateRole>d__58> (ref <CreateRole>d__);
		return <CreateRole>d__.<>t__builder.Task;
	}

	[AsyncStateMachine (typeof(<UpdateRole>d__59))]
	public System.Threading.Tasks.ValueTask<ClanResult> UpdateRole (ClanRole role, ulong bySteamId)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		<UpdateRole>d__59 <UpdateRole>d__ = default(<UpdateRole>d__59);
		<UpdateRole>d__.<>4__this = this;
		<UpdateRole>d__.role = role;
		<UpdateRole>d__.bySteamId = bySteamId;
		<UpdateRole>d__.<>t__builder = AsyncValueTaskMethodBuilder<ClanResult>.Create ();
		<UpdateRole>d__.<>1__state = -1;
		AsyncValueTaskMethodBuilder<ClanResult> <>t__builder = <UpdateRole>d__.<>t__builder;
		<>t__builder.Start<<UpdateRole>d__59> (ref <UpdateRole>d__);
		return <UpdateRole>d__.<>t__builder.Task;
	}

	[AsyncStateMachine (typeof(<SwapRoleRanks>d__60))]
	public System.Threading.Tasks.ValueTask<ClanResult> SwapRoleRanks (int roleIdA, int roleIdB, ulong bySteamId)
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		<SwapRoleRanks>d__60 <SwapRoleRanks>d__ = default(<SwapRoleRanks>d__60);
		<SwapRoleRanks>d__.<>4__this = this;
		<SwapRoleRanks>d__.roleIdA = roleIdA;
		<SwapRoleRanks>d__.roleIdB = roleIdB;
		<SwapRoleRanks>d__.bySteamId = bySteamId;
		<SwapRoleRanks>d__.<>t__builder = AsyncValueTaskMethodBuilder<ClanResult>.Create ();
		<SwapRoleRanks>d__.<>1__state = -1;
		AsyncValueTaskMethodBuilder<ClanResult> <>t__builder = <SwapRoleRanks>d__.<>t__builder;
		<>t__builder.Start<<SwapRoleRanks>d__60> (ref <SwapRoleRanks>d__);
		return <SwapRoleRanks>d__.<>t__builder.Task;
	}

	[AsyncStateMachine (typeof(<DeleteRole>d__61))]
	public System.Threading.Tasks.ValueTask<ClanResult> DeleteRole (int roleId, ulong bySteamId)
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		<DeleteRole>d__61 <DeleteRole>d__ = default(<DeleteRole>d__61);
		<DeleteRole>d__.<>4__this = this;
		<DeleteRole>d__.roleId = roleId;
		<DeleteRole>d__.bySteamId = bySteamId;
		<DeleteRole>d__.<>t__builder = AsyncValueTaskMethodBuilder<ClanResult>.Create ();
		<DeleteRole>d__.<>1__state = -1;
		AsyncValueTaskMethodBuilder<ClanResult> <>t__builder = <DeleteRole>d__.<>t__builder;
		<>t__builder.Start<<DeleteRole>d__61> (ref <DeleteRole>d__);
		return <DeleteRole>d__.<>t__builder.Task;
	}

	[AsyncStateMachine (typeof(<Disband>d__62))]
	public System.Threading.Tasks.ValueTask<ClanResult> Disband (ulong bySteamId)
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		<Disband>d__62 <Disband>d__ = default(<Disband>d__62);
		<Disband>d__.<>4__this = this;
		<Disband>d__.bySteamId = bySteamId;
		<Disband>d__.<>t__builder = AsyncValueTaskMethodBuilder<ClanResult>.Create ();
		<Disband>d__.<>1__state = -1;
		AsyncValueTaskMethodBuilder<ClanResult> <>t__builder = <Disband>d__.<>t__builder;
		<>t__builder.Start<<Disband>d__62> (ref <Disband>d__);
		return <Disband>d__.<>t__builder.Task;
	}

	public System.Threading.Tasks.ValueTask<ClanValueResult<ClanChatScrollback>> GetChatScrollback ()
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		lock (_chatHistory) {
			ClanChatScrollback val = default(ClanChatScrollback);
			val.ClanId = ClanId;
			val.Entries = _chatHistory.ToList ();
			return new System.Threading.Tasks.ValueTask<ClanValueResult<ClanChatScrollback>> (ClanValueResult<ClanChatScrollback>.op_Implicit (val));
		}
	}

	public System.Threading.Tasks.ValueTask<ClanResult> SendChatMessage (string name, string message, ulong bySteamId)
	{
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		if (!List.TryFindWith<ClanMember, ulong> ((IReadOnlyCollection<ClanMember>)_members, (Func<ClanMember, ulong>)((ClanMember m) => m.SteamId), bySteamId, (IEqualityComparer<ulong>)null).HasValue) {
			return new System.Threading.Tasks.ValueTask<ClanResult> ((ClanResult)0);
		}
		string message2 = default(string);
		if (!ClanValidator.ValidateChatMessage (message, ref message2)) {
			return new System.Threading.Tasks.ValueTask<ClanResult> ((ClanResult)6);
		}
		ClanChatEntry val = default(ClanChatEntry);
		val.SteamId = bySteamId;
		val.Name = name;
		val.Message = message2;
		val.Time = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds ();
		ClanChatEntry entry = val;
		AddScrollback (in entry);
		_chatCollector.OnClanChatMessage (ClanId, entry);
		return new System.Threading.Tasks.ValueTask<ClanResult> ((ClanResult)1);
	}

	public void AddScrollback (in ClanChatEntry entry)
	{
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		lock (_chatHistory) {
			if (_chatHistory.Count >= 20) {
				_chatHistory.RemoveAt (0);
			}
			_chatHistory.Add (entry);
		}
	}

	private bool CheckRole (ulong steamId, Func<ClanRole, bool> roleTest)
	{
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		ClanMember? val = List.TryFindWith<ClanMember, ulong> ((IReadOnlyCollection<ClanMember>)_members, (Func<ClanMember, ulong>)((ClanMember m) => m.SteamId), steamId, (IEqualityComparer<ulong>)null);
		if (!val.HasValue) {
			return false;
		}
		ClanRole? val2 = List.TryFindWith<ClanRole, int> ((IReadOnlyCollection<ClanRole>)_roles, (Func<ClanRole, int>)((ClanRole r) => r.RoleId), val.Value.RoleId, (IEqualityComparer<int>)null);
		if (!val2.HasValue) {
			return false;
		}
		if (val2.Value.Rank != 1) {
			return roleTest (val2.Value);
		}
		return true;
	}
}
