using System;
using Epic.OnlineServices;

public sealed class TitleStorageFileTransferRequest : Handle
{
	public TitleStorageFileTransferRequest ()
	{
	}

	public TitleStorageFileTransferRequest (IntPtr innerHandle)
		: base (innerHandle)
	{
	}

	public Result CancelRequest ()
	{
		return Bindings.EOS_TitleStorageFileTransferRequest_CancelRequest (base.InnerHandle);
	}

	public Result GetFileRequestState ()
	{
		return Bindings.EOS_TitleStorageFileTransferRequest_GetFileRequestState (base.InnerHandle);
	}

	public Result GetFilename (out Utf8String outStringBuffer)
	{
		int outStringLength = 64;
		IntPtr value = Helper.AddAllocation (outStringLength);
		Result result = Bindings.EOS_TitleStorageFileTransferRequest_GetFilename (base.InnerHandle, (uint)outStringLength, value, ref outStringLength);
		Helper.Get (value, out outStringBuffer);
		Helper.Dispose (ref value);
		return result;
	}

	public void Release ()
	{
		Bindings.EOS_TitleStorageFileTransferRequest_Release (base.InnerHandle);
	}
}
