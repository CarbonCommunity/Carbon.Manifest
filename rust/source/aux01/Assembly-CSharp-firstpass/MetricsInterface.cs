using System;
using Epic.OnlineServices;
using Epic.OnlineServices.Metrics;

public sealed class MetricsInterface : Handle
{
	public const int BeginplayersessionApiLatest = 1;

	public const int EndplayersessionApiLatest = 1;

	public MetricsInterface ()
	{
	}

	public MetricsInterface (IntPtr innerHandle)
		: base (innerHandle)
	{
	}

	public Result BeginPlayerSession (ref BeginPlayerSessionOptions options)
	{
		BeginPlayerSessionOptionsInternal options2 = default(BeginPlayerSessionOptionsInternal);
		options2.Set (ref options);
		Result result = Bindings.EOS_Metrics_BeginPlayerSession (base.InnerHandle, ref options2);
		Helper.Dispose (ref options2);
		return result;
	}

	public Result EndPlayerSession (ref EndPlayerSessionOptions options)
	{
		EndPlayerSessionOptionsInternal options2 = default(EndPlayerSessionOptionsInternal);
		options2.Set (ref options);
		Result result = Bindings.EOS_Metrics_EndPlayerSession (base.InnerHandle, ref options2);
		Helper.Dispose (ref options2);
		return result;
	}
}
