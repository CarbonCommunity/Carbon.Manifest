using System;
using Epic.OnlineServices;
using Epic.OnlineServices.Sessions;

public sealed class SessionDetails : Handle
{
	public const int SessiondetailsAttributeApiLatest = 1;

	public const int SessiondetailsCopyinfoApiLatest = 1;

	public const int SessiondetailsCopysessionattributebyindexApiLatest = 1;

	public const int SessiondetailsCopysessionattributebykeyApiLatest = 1;

	public const int SessiondetailsGetsessionattributecountApiLatest = 1;

	public const int SessiondetailsInfoApiLatest = 1;

	public const int SessiondetailsSettingsApiLatest = 3;

	public SessionDetails ()
	{
	}

	public SessionDetails (IntPtr innerHandle)
		: base (innerHandle)
	{
	}

	public Result CopyInfo (ref SessionDetailsCopyInfoOptions options, out SessionDetailsInfo? outSessionInfo)
	{
		SessionDetailsCopyInfoOptionsInternal options2 = default(SessionDetailsCopyInfoOptionsInternal);
		options2.Set (ref options);
		IntPtr outSessionInfo2 = IntPtr.Zero;
		Result result = Bindings.EOS_SessionDetails_CopyInfo (base.InnerHandle, ref options2, ref outSessionInfo2);
		Helper.Dispose (ref options2);
		Helper.Get<SessionDetailsInfoInternal, SessionDetailsInfo> (outSessionInfo2, out outSessionInfo);
		if (outSessionInfo.HasValue) {
			Bindings.EOS_SessionDetails_Info_Release (outSessionInfo2);
		}
		return result;
	}

	public Result CopySessionAttributeByIndex (ref SessionDetailsCopySessionAttributeByIndexOptions options, out SessionDetailsAttribute? outSessionAttribute)
	{
		SessionDetailsCopySessionAttributeByIndexOptionsInternal options2 = default(SessionDetailsCopySessionAttributeByIndexOptionsInternal);
		options2.Set (ref options);
		IntPtr outSessionAttribute2 = IntPtr.Zero;
		Result result = Bindings.EOS_SessionDetails_CopySessionAttributeByIndex (base.InnerHandle, ref options2, ref outSessionAttribute2);
		Helper.Dispose (ref options2);
		Helper.Get<SessionDetailsAttributeInternal, SessionDetailsAttribute> (outSessionAttribute2, out outSessionAttribute);
		if (outSessionAttribute.HasValue) {
			Bindings.EOS_SessionDetails_Attribute_Release (outSessionAttribute2);
		}
		return result;
	}

	public Result CopySessionAttributeByKey (ref SessionDetailsCopySessionAttributeByKeyOptions options, out SessionDetailsAttribute? outSessionAttribute)
	{
		SessionDetailsCopySessionAttributeByKeyOptionsInternal options2 = default(SessionDetailsCopySessionAttributeByKeyOptionsInternal);
		options2.Set (ref options);
		IntPtr outSessionAttribute2 = IntPtr.Zero;
		Result result = Bindings.EOS_SessionDetails_CopySessionAttributeByKey (base.InnerHandle, ref options2, ref outSessionAttribute2);
		Helper.Dispose (ref options2);
		Helper.Get<SessionDetailsAttributeInternal, SessionDetailsAttribute> (outSessionAttribute2, out outSessionAttribute);
		if (outSessionAttribute.HasValue) {
			Bindings.EOS_SessionDetails_Attribute_Release (outSessionAttribute2);
		}
		return result;
	}

	public uint GetSessionAttributeCount (ref SessionDetailsGetSessionAttributeCountOptions options)
	{
		SessionDetailsGetSessionAttributeCountOptionsInternal options2 = default(SessionDetailsGetSessionAttributeCountOptionsInternal);
		options2.Set (ref options);
		uint result = Bindings.EOS_SessionDetails_GetSessionAttributeCount (base.InnerHandle, ref options2);
		Helper.Dispose (ref options2);
		return result;
	}

	public void Release ()
	{
		Bindings.EOS_SessionDetails_Release (base.InnerHandle);
	}
}
