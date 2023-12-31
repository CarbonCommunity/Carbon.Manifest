using System;
using Epic.OnlineServices;
using Epic.OnlineServices.Reports;

public sealed class ReportsInterface : Handle
{
	public const int ReportcontextMaxLength = 4096;

	public const int ReportmessageMaxLength = 512;

	public const int SendplayerbehaviorreportApiLatest = 2;

	public ReportsInterface ()
	{
	}

	public ReportsInterface (IntPtr innerHandle)
		: base (innerHandle)
	{
	}

	public void SendPlayerBehaviorReport (ref SendPlayerBehaviorReportOptions options, object clientData, OnSendPlayerBehaviorReportCompleteCallback completionDelegate)
	{
		SendPlayerBehaviorReportOptionsInternal options2 = default(SendPlayerBehaviorReportOptionsInternal);
		options2.Set (ref options);
		IntPtr clientDataAddress = IntPtr.Zero;
		OnSendPlayerBehaviorReportCompleteCallbackInternal onSendPlayerBehaviorReportCompleteCallbackInternal = OnSendPlayerBehaviorReportCompleteCallbackInternalImplementation;
		Helper.AddCallback (out clientDataAddress, clientData, completionDelegate, onSendPlayerBehaviorReportCompleteCallbackInternal);
		Bindings.EOS_Reports_SendPlayerBehaviorReport (base.InnerHandle, ref options2, clientDataAddress, onSendPlayerBehaviorReportCompleteCallbackInternal);
		Helper.Dispose (ref options2);
	}

	[MonoPInvokeCallback (typeof(OnSendPlayerBehaviorReportCompleteCallbackInternal))]
	internal static void OnSendPlayerBehaviorReportCompleteCallbackInternalImplementation (ref SendPlayerBehaviorReportCompleteCallbackInfoInternal data)
	{
		if (Helper.TryGetAndRemoveCallback<SendPlayerBehaviorReportCompleteCallbackInfoInternal, OnSendPlayerBehaviorReportCompleteCallback, SendPlayerBehaviorReportCompleteCallbackInfo> (ref data, out var callback, out var callbackInfo)) {
			callback (ref callbackInfo);
		}
	}
}
