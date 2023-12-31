using System;
using Epic.OnlineServices;

public sealed class ContinuanceToken : Handle
{
	public ContinuanceToken ()
	{
	}

	public ContinuanceToken (IntPtr innerHandle)
		: base (innerHandle)
	{
	}

	public Result ToString (out Utf8String outBuffer)
	{
		int inOutBufferLength = 1024;
		IntPtr value = Helper.AddAllocation (inOutBufferLength);
		Result result = Bindings.EOS_ContinuanceToken_ToString (base.InnerHandle, value, ref inOutBufferLength);
		Helper.Get (value, out outBuffer);
		Helper.Dispose (ref value);
		return result;
	}

	public override string ToString ()
	{
		ToString (out var outBuffer);
		return outBuffer;
	}

	public override string ToString (string format, IFormatProvider formatProvider)
	{
		if (format != null) {
			return string.Format (format, ToString ());
		}
		return ToString ();
	}

	public static explicit operator Utf8String (ContinuanceToken value)
	{
		Utf8String outBuffer = null;
		if (value != null) {
			value.ToString (out outBuffer);
		}
		return outBuffer;
	}
}
