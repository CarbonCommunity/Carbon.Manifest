using System;
using Epic.OnlineServices;

public sealed class ProductUserId : Handle
{
	public const int ProductuseridMaxLength = 32;

	public ProductUserId ()
	{
	}

	public ProductUserId (IntPtr innerHandle)
		: base (innerHandle)
	{
	}

	public static ProductUserId FromString (Utf8String productUserIdString)
	{
		IntPtr to = IntPtr.Zero;
		Helper.Set (productUserIdString, ref to);
		IntPtr from = Bindings.EOS_ProductUserId_FromString (to);
		Helper.Dispose (ref to);
		Helper.Get (from, out ProductUserId to2);
		return to2;
	}

	public static explicit operator ProductUserId (Utf8String value)
	{
		return FromString (value);
	}

	public bool IsValid ()
	{
		Helper.Get (Bindings.EOS_ProductUserId_IsValid (base.InnerHandle), out var to);
		return to;
	}

	public Result ToString (out Utf8String outBuffer)
	{
		int inOutBufferLength = 33;
		IntPtr value = Helper.AddAllocation (inOutBufferLength);
		Result result = Bindings.EOS_ProductUserId_ToString (base.InnerHandle, value, ref inOutBufferLength);
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

	public static explicit operator Utf8String (ProductUserId value)
	{
		Utf8String outBuffer = null;
		if (value != null) {
			value.ToString (out outBuffer);
		}
		return outBuffer;
	}
}
