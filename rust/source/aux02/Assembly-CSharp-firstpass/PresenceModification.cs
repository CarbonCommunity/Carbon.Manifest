using System;
using Epic.OnlineServices;
using Epic.OnlineServices.Presence;

public sealed class PresenceModification : Handle
{
	public const int PresencemodificationDatarecordidApiLatest = 1;

	public const int PresencemodificationDeletedataApiLatest = 1;

	public const int PresencemodificationJoininfoMaxLength = 255;

	public const int PresencemodificationSetdataApiLatest = 1;

	public const int PresencemodificationSetjoininfoApiLatest = 1;

	public const int PresencemodificationSetrawrichtextApiLatest = 1;

	public const int PresencemodificationSetstatusApiLatest = 1;

	public PresenceModification ()
	{
	}

	public PresenceModification (IntPtr innerHandle)
		: base (innerHandle)
	{
	}

	public Result DeleteData (ref PresenceModificationDeleteDataOptions options)
	{
		PresenceModificationDeleteDataOptionsInternal options2 = default(PresenceModificationDeleteDataOptionsInternal);
		options2.Set (ref options);
		Result result = Bindings.EOS_PresenceModification_DeleteData (base.InnerHandle, ref options2);
		Helper.Dispose (ref options2);
		return result;
	}

	public void Release ()
	{
		Bindings.EOS_PresenceModification_Release (base.InnerHandle);
	}

	public Result SetData (ref PresenceModificationSetDataOptions options)
	{
		PresenceModificationSetDataOptionsInternal options2 = default(PresenceModificationSetDataOptionsInternal);
		options2.Set (ref options);
		Result result = Bindings.EOS_PresenceModification_SetData (base.InnerHandle, ref options2);
		Helper.Dispose (ref options2);
		return result;
	}

	public Result SetJoinInfo (ref PresenceModificationSetJoinInfoOptions options)
	{
		PresenceModificationSetJoinInfoOptionsInternal options2 = default(PresenceModificationSetJoinInfoOptionsInternal);
		options2.Set (ref options);
		Result result = Bindings.EOS_PresenceModification_SetJoinInfo (base.InnerHandle, ref options2);
		Helper.Dispose (ref options2);
		return result;
	}

	public Result SetRawRichText (ref PresenceModificationSetRawRichTextOptions options)
	{
		PresenceModificationSetRawRichTextOptionsInternal options2 = default(PresenceModificationSetRawRichTextOptionsInternal);
		options2.Set (ref options);
		Result result = Bindings.EOS_PresenceModification_SetRawRichText (base.InnerHandle, ref options2);
		Helper.Dispose (ref options2);
		return result;
	}

	public Result SetStatus (ref PresenceModificationSetStatusOptions options)
	{
		PresenceModificationSetStatusOptionsInternal options2 = default(PresenceModificationSetStatusOptionsInternal);
		options2.Set (ref options);
		Result result = Bindings.EOS_PresenceModification_SetStatus (base.InnerHandle, ref options2);
		Helper.Dispose (ref options2);
		return result;
	}
}
