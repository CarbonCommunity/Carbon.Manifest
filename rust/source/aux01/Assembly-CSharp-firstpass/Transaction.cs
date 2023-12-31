using System;
using Epic.OnlineServices;
using Epic.OnlineServices.Ecom;

public sealed class Transaction : Handle
{
	public const int TransactionCopyentitlementbyindexApiLatest = 1;

	public const int TransactionGetentitlementscountApiLatest = 1;

	public Transaction ()
	{
	}

	public Transaction (IntPtr innerHandle)
		: base (innerHandle)
	{
	}

	public Result CopyEntitlementByIndex (ref TransactionCopyEntitlementByIndexOptions options, out Entitlement? outEntitlement)
	{
		TransactionCopyEntitlementByIndexOptionsInternal options2 = default(TransactionCopyEntitlementByIndexOptionsInternal);
		options2.Set (ref options);
		IntPtr outEntitlement2 = IntPtr.Zero;
		Result result = Bindings.EOS_Ecom_Transaction_CopyEntitlementByIndex (base.InnerHandle, ref options2, ref outEntitlement2);
		Helper.Dispose (ref options2);
		Helper.Get<EntitlementInternal, Entitlement> (outEntitlement2, out outEntitlement);
		if (outEntitlement.HasValue) {
			Bindings.EOS_Ecom_Entitlement_Release (outEntitlement2);
		}
		return result;
	}

	public uint GetEntitlementsCount (ref TransactionGetEntitlementsCountOptions options)
	{
		TransactionGetEntitlementsCountOptionsInternal options2 = default(TransactionGetEntitlementsCountOptionsInternal);
		options2.Set (ref options);
		uint result = Bindings.EOS_Ecom_Transaction_GetEntitlementsCount (base.InnerHandle, ref options2);
		Helper.Dispose (ref options2);
		return result;
	}

	public Result GetTransactionId (out Utf8String outBuffer)
	{
		int inOutBufferLength = 65;
		IntPtr value = Helper.AddAllocation (inOutBufferLength);
		Result result = Bindings.EOS_Ecom_Transaction_GetTransactionId (base.InnerHandle, value, ref inOutBufferLength);
		Helper.Get (value, out outBuffer);
		Helper.Dispose (ref value);
		return result;
	}

	public void Release ()
	{
		Bindings.EOS_Ecom_Transaction_Release (base.InnerHandle);
	}
}
