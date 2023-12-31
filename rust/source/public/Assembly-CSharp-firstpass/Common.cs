using System;
using Epic.OnlineServices;

public sealed class Common
{
	public const ulong InvalidNotificationid = 0uL;

	public static readonly Utf8String IptUnknown = (string)null;

	public const int OptEpic = 100;

	public const int OptSteam = 4000;

	public const int OptUnknown = 0;

	public const int PagequeryApiLatest = 1;

	public const int PagequeryMaxcountDefault = 10;

	public const int PagequeryMaxcountMaximum = 100;

	public const int PaginationApiLatest = 1;

	public static bool IsOperationComplete (Result result)
	{
		Helper.Get (Bindings.EOS_EResult_IsOperationComplete (result), out var to);
		return to;
	}

	public static Utf8String ToString (Result result)
	{
		Helper.Get (Bindings.EOS_EResult_ToString (result), out Utf8String to);
		return to;
	}

	public static Result ToString (ArraySegment<byte> byteArray, out Utf8String outBuffer)
	{
		IntPtr to = IntPtr.Zero;
		Helper.Set (byteArray, ref to, out var arrayLength);
		uint inOutBufferLength = 1024u;
		IntPtr value = Helper.AddAllocation (inOutBufferLength);
		Result result = Bindings.EOS_ByteArray_ToString (to, arrayLength, value, ref inOutBufferLength);
		Helper.Dispose (ref to);
		Helper.Get (value, out outBuffer);
		Helper.Dispose (ref value);
		return result;
	}

	public static Utf8String ToString (ArraySegment<byte> byteArray)
	{
		ToString (byteArray, out var outBuffer);
		return outBuffer;
	}
}
