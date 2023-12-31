using System;
using Epic.OnlineServices;
using Epic.OnlineServices.Ecom;

public sealed class EcomInterface : Handle
{
	public const int CatalogitemApiLatest = 1;

	public const int CatalogitemEntitlementendtimestampUndefined = -1;

	public const int CatalogofferApiLatest = 5;

	public const int CatalogofferEffectivedatetimestampUndefined = -1;

	public const int CatalogofferExpirationtimestampUndefined = -1;

	public const int CatalogofferReleasedatetimestampUndefined = -1;

	public const int CatalogreleaseApiLatest = 1;

	public const int CheckoutApiLatest = 1;

	public const int CheckoutMaxEntries = 10;

	public const int CheckoutentryApiLatest = 1;

	public const int CopyentitlementbyidApiLatest = 2;

	public const int CopyentitlementbyindexApiLatest = 1;

	public const int CopyentitlementbynameandindexApiLatest = 1;

	public const int CopyitembyidApiLatest = 1;

	public const int CopyitemimageinfobyindexApiLatest = 1;

	public const int CopyitemreleasebyindexApiLatest = 1;

	public const int CopylastredeemedentitlementbyindexApiLatest = 1;

	public const int CopyofferbyidApiLatest = 3;

	public const int CopyofferbyindexApiLatest = 3;

	public const int CopyofferimageinfobyindexApiLatest = 1;

	public const int CopyofferitembyindexApiLatest = 1;

	public const int CopytransactionbyidApiLatest = 1;

	public const int CopytransactionbyindexApiLatest = 1;

	public const int EntitlementApiLatest = 2;

	public const int EntitlementEndtimestampUndefined = -1;

	public const int EntitlementidMaxLength = 32;

	public const int GetentitlementsbynamecountApiLatest = 1;

	public const int GetentitlementscountApiLatest = 1;

	public const int GetitemimageinfocountApiLatest = 1;

	public const int GetitemreleasecountApiLatest = 1;

	public const int GetlastredeemedentitlementscountApiLatest = 1;

	public const int GetoffercountApiLatest = 1;

	public const int GetofferimageinfocountApiLatest = 1;

	public const int GetofferitemcountApiLatest = 1;

	public const int GettransactioncountApiLatest = 1;

	public const int ItemownershipApiLatest = 1;

	public const int KeyimageinfoApiLatest = 1;

	public const int QueryentitlementsApiLatest = 2;

	public const int QueryentitlementsMaxEntitlementIds = 256;

	public const int QueryentitlementtokenApiLatest = 1;

	public const int QueryentitlementtokenMaxEntitlementIds = 32;

	public const int QueryoffersApiLatest = 1;

	public const int QueryownershipApiLatest = 2;

	public const int QueryownershipMaxCatalogIds = 400;

	public const int QueryownershipMaxSandboxIds = 10;

	public const int QueryownershipbysandboxidsoptionsApiLatest = 1;

	public const int QueryownershiptokenApiLatest = 2;

	public const int QueryownershiptokenMaxCatalogitemIds = 32;

	public const int RedeementitlementsApiLatest = 2;

	public const int RedeementitlementsMaxIds = 32;

	public const int TransactionidMaximumLength = 64;

	public EcomInterface ()
	{
	}

	public EcomInterface (IntPtr innerHandle)
		: base (innerHandle)
	{
	}

	public void Checkout (ref CheckoutOptions options, object clientData, OnCheckoutCallback completionDelegate)
	{
		CheckoutOptionsInternal options2 = default(CheckoutOptionsInternal);
		options2.Set (ref options);
		IntPtr clientDataAddress = IntPtr.Zero;
		OnCheckoutCallbackInternal onCheckoutCallbackInternal = OnCheckoutCallbackInternalImplementation;
		Helper.AddCallback (out clientDataAddress, clientData, completionDelegate, onCheckoutCallbackInternal);
		Bindings.EOS_Ecom_Checkout (base.InnerHandle, ref options2, clientDataAddress, onCheckoutCallbackInternal);
		Helper.Dispose (ref options2);
	}

	public Result CopyEntitlementById (ref CopyEntitlementByIdOptions options, out Entitlement? outEntitlement)
	{
		CopyEntitlementByIdOptionsInternal options2 = default(CopyEntitlementByIdOptionsInternal);
		options2.Set (ref options);
		IntPtr outEntitlement2 = IntPtr.Zero;
		Result result = Bindings.EOS_Ecom_CopyEntitlementById (base.InnerHandle, ref options2, ref outEntitlement2);
		Helper.Dispose (ref options2);
		Helper.Get<EntitlementInternal, Entitlement> (outEntitlement2, out outEntitlement);
		if (outEntitlement.HasValue) {
			Bindings.EOS_Ecom_Entitlement_Release (outEntitlement2);
		}
		return result;
	}

	public Result CopyEntitlementByIndex (ref CopyEntitlementByIndexOptions options, out Entitlement? outEntitlement)
	{
		CopyEntitlementByIndexOptionsInternal options2 = default(CopyEntitlementByIndexOptionsInternal);
		options2.Set (ref options);
		IntPtr outEntitlement2 = IntPtr.Zero;
		Result result = Bindings.EOS_Ecom_CopyEntitlementByIndex (base.InnerHandle, ref options2, ref outEntitlement2);
		Helper.Dispose (ref options2);
		Helper.Get<EntitlementInternal, Entitlement> (outEntitlement2, out outEntitlement);
		if (outEntitlement.HasValue) {
			Bindings.EOS_Ecom_Entitlement_Release (outEntitlement2);
		}
		return result;
	}

	public Result CopyEntitlementByNameAndIndex (ref CopyEntitlementByNameAndIndexOptions options, out Entitlement? outEntitlement)
	{
		CopyEntitlementByNameAndIndexOptionsInternal options2 = default(CopyEntitlementByNameAndIndexOptionsInternal);
		options2.Set (ref options);
		IntPtr outEntitlement2 = IntPtr.Zero;
		Result result = Bindings.EOS_Ecom_CopyEntitlementByNameAndIndex (base.InnerHandle, ref options2, ref outEntitlement2);
		Helper.Dispose (ref options2);
		Helper.Get<EntitlementInternal, Entitlement> (outEntitlement2, out outEntitlement);
		if (outEntitlement.HasValue) {
			Bindings.EOS_Ecom_Entitlement_Release (outEntitlement2);
		}
		return result;
	}

	public Result CopyItemById (ref CopyItemByIdOptions options, out CatalogItem? outItem)
	{
		CopyItemByIdOptionsInternal options2 = default(CopyItemByIdOptionsInternal);
		options2.Set (ref options);
		IntPtr outItem2 = IntPtr.Zero;
		Result result = Bindings.EOS_Ecom_CopyItemById (base.InnerHandle, ref options2, ref outItem2);
		Helper.Dispose (ref options2);
		Helper.Get<CatalogItemInternal, CatalogItem> (outItem2, out outItem);
		if (outItem.HasValue) {
			Bindings.EOS_Ecom_CatalogItem_Release (outItem2);
		}
		return result;
	}

	public Result CopyItemImageInfoByIndex (ref CopyItemImageInfoByIndexOptions options, out KeyImageInfo? outImageInfo)
	{
		CopyItemImageInfoByIndexOptionsInternal options2 = default(CopyItemImageInfoByIndexOptionsInternal);
		options2.Set (ref options);
		IntPtr outImageInfo2 = IntPtr.Zero;
		Result result = Bindings.EOS_Ecom_CopyItemImageInfoByIndex (base.InnerHandle, ref options2, ref outImageInfo2);
		Helper.Dispose (ref options2);
		Helper.Get<KeyImageInfoInternal, KeyImageInfo> (outImageInfo2, out outImageInfo);
		if (outImageInfo.HasValue) {
			Bindings.EOS_Ecom_KeyImageInfo_Release (outImageInfo2);
		}
		return result;
	}

	public Result CopyItemReleaseByIndex (ref CopyItemReleaseByIndexOptions options, out CatalogRelease? outRelease)
	{
		CopyItemReleaseByIndexOptionsInternal options2 = default(CopyItemReleaseByIndexOptionsInternal);
		options2.Set (ref options);
		IntPtr outRelease2 = IntPtr.Zero;
		Result result = Bindings.EOS_Ecom_CopyItemReleaseByIndex (base.InnerHandle, ref options2, ref outRelease2);
		Helper.Dispose (ref options2);
		Helper.Get<CatalogReleaseInternal, CatalogRelease> (outRelease2, out outRelease);
		if (outRelease.HasValue) {
			Bindings.EOS_Ecom_CatalogRelease_Release (outRelease2);
		}
		return result;
	}

	public Result CopyLastRedeemedEntitlementByIndex (ref CopyLastRedeemedEntitlementByIndexOptions options, out Utf8String outRedeemedEntitlementId)
	{
		CopyLastRedeemedEntitlementByIndexOptionsInternal options2 = default(CopyLastRedeemedEntitlementByIndexOptionsInternal);
		options2.Set (ref options);
		int inOutRedeemedEntitlementIdLength = 33;
		IntPtr value = Helper.AddAllocation (inOutRedeemedEntitlementIdLength);
		Result result = Bindings.EOS_Ecom_CopyLastRedeemedEntitlementByIndex (base.InnerHandle, ref options2, value, ref inOutRedeemedEntitlementIdLength);
		Helper.Dispose (ref options2);
		Helper.Get (value, out outRedeemedEntitlementId);
		Helper.Dispose (ref value);
		return result;
	}

	public Result CopyOfferById (ref CopyOfferByIdOptions options, out CatalogOffer? outOffer)
	{
		CopyOfferByIdOptionsInternal options2 = default(CopyOfferByIdOptionsInternal);
		options2.Set (ref options);
		IntPtr outOffer2 = IntPtr.Zero;
		Result result = Bindings.EOS_Ecom_CopyOfferById (base.InnerHandle, ref options2, ref outOffer2);
		Helper.Dispose (ref options2);
		Helper.Get<CatalogOfferInternal, CatalogOffer> (outOffer2, out outOffer);
		if (outOffer.HasValue) {
			Bindings.EOS_Ecom_CatalogOffer_Release (outOffer2);
		}
		return result;
	}

	public Result CopyOfferByIndex (ref CopyOfferByIndexOptions options, out CatalogOffer? outOffer)
	{
		CopyOfferByIndexOptionsInternal options2 = default(CopyOfferByIndexOptionsInternal);
		options2.Set (ref options);
		IntPtr outOffer2 = IntPtr.Zero;
		Result result = Bindings.EOS_Ecom_CopyOfferByIndex (base.InnerHandle, ref options2, ref outOffer2);
		Helper.Dispose (ref options2);
		Helper.Get<CatalogOfferInternal, CatalogOffer> (outOffer2, out outOffer);
		if (outOffer.HasValue) {
			Bindings.EOS_Ecom_CatalogOffer_Release (outOffer2);
		}
		return result;
	}

	public Result CopyOfferImageInfoByIndex (ref CopyOfferImageInfoByIndexOptions options, out KeyImageInfo? outImageInfo)
	{
		CopyOfferImageInfoByIndexOptionsInternal options2 = default(CopyOfferImageInfoByIndexOptionsInternal);
		options2.Set (ref options);
		IntPtr outImageInfo2 = IntPtr.Zero;
		Result result = Bindings.EOS_Ecom_CopyOfferImageInfoByIndex (base.InnerHandle, ref options2, ref outImageInfo2);
		Helper.Dispose (ref options2);
		Helper.Get<KeyImageInfoInternal, KeyImageInfo> (outImageInfo2, out outImageInfo);
		if (outImageInfo.HasValue) {
			Bindings.EOS_Ecom_KeyImageInfo_Release (outImageInfo2);
		}
		return result;
	}

	public Result CopyOfferItemByIndex (ref CopyOfferItemByIndexOptions options, out CatalogItem? outItem)
	{
		CopyOfferItemByIndexOptionsInternal options2 = default(CopyOfferItemByIndexOptionsInternal);
		options2.Set (ref options);
		IntPtr outItem2 = IntPtr.Zero;
		Result result = Bindings.EOS_Ecom_CopyOfferItemByIndex (base.InnerHandle, ref options2, ref outItem2);
		Helper.Dispose (ref options2);
		Helper.Get<CatalogItemInternal, CatalogItem> (outItem2, out outItem);
		if (outItem.HasValue) {
			Bindings.EOS_Ecom_CatalogItem_Release (outItem2);
		}
		return result;
	}

	public Result CopyTransactionById (ref CopyTransactionByIdOptions options, out Transaction outTransaction)
	{
		CopyTransactionByIdOptionsInternal options2 = default(CopyTransactionByIdOptionsInternal);
		options2.Set (ref options);
		IntPtr outTransaction2 = IntPtr.Zero;
		Result result = Bindings.EOS_Ecom_CopyTransactionById (base.InnerHandle, ref options2, ref outTransaction2);
		Helper.Dispose (ref options2);
		Helper.Get (outTransaction2, out outTransaction);
		return result;
	}

	public Result CopyTransactionByIndex (ref CopyTransactionByIndexOptions options, out Transaction outTransaction)
	{
		CopyTransactionByIndexOptionsInternal options2 = default(CopyTransactionByIndexOptionsInternal);
		options2.Set (ref options);
		IntPtr outTransaction2 = IntPtr.Zero;
		Result result = Bindings.EOS_Ecom_CopyTransactionByIndex (base.InnerHandle, ref options2, ref outTransaction2);
		Helper.Dispose (ref options2);
		Helper.Get (outTransaction2, out outTransaction);
		return result;
	}

	public uint GetEntitlementsByNameCount (ref GetEntitlementsByNameCountOptions options)
	{
		GetEntitlementsByNameCountOptionsInternal options2 = default(GetEntitlementsByNameCountOptionsInternal);
		options2.Set (ref options);
		uint result = Bindings.EOS_Ecom_GetEntitlementsByNameCount (base.InnerHandle, ref options2);
		Helper.Dispose (ref options2);
		return result;
	}

	public uint GetEntitlementsCount (ref GetEntitlementsCountOptions options)
	{
		GetEntitlementsCountOptionsInternal options2 = default(GetEntitlementsCountOptionsInternal);
		options2.Set (ref options);
		uint result = Bindings.EOS_Ecom_GetEntitlementsCount (base.InnerHandle, ref options2);
		Helper.Dispose (ref options2);
		return result;
	}

	public uint GetItemImageInfoCount (ref GetItemImageInfoCountOptions options)
	{
		GetItemImageInfoCountOptionsInternal options2 = default(GetItemImageInfoCountOptionsInternal);
		options2.Set (ref options);
		uint result = Bindings.EOS_Ecom_GetItemImageInfoCount (base.InnerHandle, ref options2);
		Helper.Dispose (ref options2);
		return result;
	}

	public uint GetItemReleaseCount (ref GetItemReleaseCountOptions options)
	{
		GetItemReleaseCountOptionsInternal options2 = default(GetItemReleaseCountOptionsInternal);
		options2.Set (ref options);
		uint result = Bindings.EOS_Ecom_GetItemReleaseCount (base.InnerHandle, ref options2);
		Helper.Dispose (ref options2);
		return result;
	}

	public uint GetLastRedeemedEntitlementsCount (ref GetLastRedeemedEntitlementsCountOptions options)
	{
		GetLastRedeemedEntitlementsCountOptionsInternal options2 = default(GetLastRedeemedEntitlementsCountOptionsInternal);
		options2.Set (ref options);
		uint result = Bindings.EOS_Ecom_GetLastRedeemedEntitlementsCount (base.InnerHandle, ref options2);
		Helper.Dispose (ref options2);
		return result;
	}

	public uint GetOfferCount (ref GetOfferCountOptions options)
	{
		GetOfferCountOptionsInternal options2 = default(GetOfferCountOptionsInternal);
		options2.Set (ref options);
		uint result = Bindings.EOS_Ecom_GetOfferCount (base.InnerHandle, ref options2);
		Helper.Dispose (ref options2);
		return result;
	}

	public uint GetOfferImageInfoCount (ref GetOfferImageInfoCountOptions options)
	{
		GetOfferImageInfoCountOptionsInternal options2 = default(GetOfferImageInfoCountOptionsInternal);
		options2.Set (ref options);
		uint result = Bindings.EOS_Ecom_GetOfferImageInfoCount (base.InnerHandle, ref options2);
		Helper.Dispose (ref options2);
		return result;
	}

	public uint GetOfferItemCount (ref GetOfferItemCountOptions options)
	{
		GetOfferItemCountOptionsInternal options2 = default(GetOfferItemCountOptionsInternal);
		options2.Set (ref options);
		uint result = Bindings.EOS_Ecom_GetOfferItemCount (base.InnerHandle, ref options2);
		Helper.Dispose (ref options2);
		return result;
	}

	public uint GetTransactionCount (ref GetTransactionCountOptions options)
	{
		GetTransactionCountOptionsInternal options2 = default(GetTransactionCountOptionsInternal);
		options2.Set (ref options);
		uint result = Bindings.EOS_Ecom_GetTransactionCount (base.InnerHandle, ref options2);
		Helper.Dispose (ref options2);
		return result;
	}

	public void QueryEntitlementToken (ref QueryEntitlementTokenOptions options, object clientData, OnQueryEntitlementTokenCallback completionDelegate)
	{
		QueryEntitlementTokenOptionsInternal options2 = default(QueryEntitlementTokenOptionsInternal);
		options2.Set (ref options);
		IntPtr clientDataAddress = IntPtr.Zero;
		OnQueryEntitlementTokenCallbackInternal onQueryEntitlementTokenCallbackInternal = OnQueryEntitlementTokenCallbackInternalImplementation;
		Helper.AddCallback (out clientDataAddress, clientData, completionDelegate, onQueryEntitlementTokenCallbackInternal);
		Bindings.EOS_Ecom_QueryEntitlementToken (base.InnerHandle, ref options2, clientDataAddress, onQueryEntitlementTokenCallbackInternal);
		Helper.Dispose (ref options2);
	}

	public void QueryEntitlements (ref QueryEntitlementsOptions options, object clientData, OnQueryEntitlementsCallback completionDelegate)
	{
		QueryEntitlementsOptionsInternal options2 = default(QueryEntitlementsOptionsInternal);
		options2.Set (ref options);
		IntPtr clientDataAddress = IntPtr.Zero;
		OnQueryEntitlementsCallbackInternal onQueryEntitlementsCallbackInternal = OnQueryEntitlementsCallbackInternalImplementation;
		Helper.AddCallback (out clientDataAddress, clientData, completionDelegate, onQueryEntitlementsCallbackInternal);
		Bindings.EOS_Ecom_QueryEntitlements (base.InnerHandle, ref options2, clientDataAddress, onQueryEntitlementsCallbackInternal);
		Helper.Dispose (ref options2);
	}

	public void QueryOffers (ref QueryOffersOptions options, object clientData, OnQueryOffersCallback completionDelegate)
	{
		QueryOffersOptionsInternal options2 = default(QueryOffersOptionsInternal);
		options2.Set (ref options);
		IntPtr clientDataAddress = IntPtr.Zero;
		OnQueryOffersCallbackInternal onQueryOffersCallbackInternal = OnQueryOffersCallbackInternalImplementation;
		Helper.AddCallback (out clientDataAddress, clientData, completionDelegate, onQueryOffersCallbackInternal);
		Bindings.EOS_Ecom_QueryOffers (base.InnerHandle, ref options2, clientDataAddress, onQueryOffersCallbackInternal);
		Helper.Dispose (ref options2);
	}

	public void QueryOwnership (ref QueryOwnershipOptions options, object clientData, OnQueryOwnershipCallback completionDelegate)
	{
		QueryOwnershipOptionsInternal options2 = default(QueryOwnershipOptionsInternal);
		options2.Set (ref options);
		IntPtr clientDataAddress = IntPtr.Zero;
		OnQueryOwnershipCallbackInternal onQueryOwnershipCallbackInternal = OnQueryOwnershipCallbackInternalImplementation;
		Helper.AddCallback (out clientDataAddress, clientData, completionDelegate, onQueryOwnershipCallbackInternal);
		Bindings.EOS_Ecom_QueryOwnership (base.InnerHandle, ref options2, clientDataAddress, onQueryOwnershipCallbackInternal);
		Helper.Dispose (ref options2);
	}

	public void QueryOwnershipBySandboxIds (ref QueryOwnershipBySandboxIdsOptions options, object clientData, OnQueryOwnershipBySandboxIdsCallback completionDelegate)
	{
		QueryOwnershipBySandboxIdsOptionsInternal options2 = default(QueryOwnershipBySandboxIdsOptionsInternal);
		options2.Set (ref options);
		IntPtr clientDataAddress = IntPtr.Zero;
		OnQueryOwnershipBySandboxIdsCallbackInternal onQueryOwnershipBySandboxIdsCallbackInternal = OnQueryOwnershipBySandboxIdsCallbackInternalImplementation;
		Helper.AddCallback (out clientDataAddress, clientData, completionDelegate, onQueryOwnershipBySandboxIdsCallbackInternal);
		Bindings.EOS_Ecom_QueryOwnershipBySandboxIds (base.InnerHandle, ref options2, clientDataAddress, onQueryOwnershipBySandboxIdsCallbackInternal);
		Helper.Dispose (ref options2);
	}

	public void QueryOwnershipToken (ref QueryOwnershipTokenOptions options, object clientData, OnQueryOwnershipTokenCallback completionDelegate)
	{
		QueryOwnershipTokenOptionsInternal options2 = default(QueryOwnershipTokenOptionsInternal);
		options2.Set (ref options);
		IntPtr clientDataAddress = IntPtr.Zero;
		OnQueryOwnershipTokenCallbackInternal onQueryOwnershipTokenCallbackInternal = OnQueryOwnershipTokenCallbackInternalImplementation;
		Helper.AddCallback (out clientDataAddress, clientData, completionDelegate, onQueryOwnershipTokenCallbackInternal);
		Bindings.EOS_Ecom_QueryOwnershipToken (base.InnerHandle, ref options2, clientDataAddress, onQueryOwnershipTokenCallbackInternal);
		Helper.Dispose (ref options2);
	}

	public void RedeemEntitlements (ref RedeemEntitlementsOptions options, object clientData, OnRedeemEntitlementsCallback completionDelegate)
	{
		RedeemEntitlementsOptionsInternal options2 = default(RedeemEntitlementsOptionsInternal);
		options2.Set (ref options);
		IntPtr clientDataAddress = IntPtr.Zero;
		OnRedeemEntitlementsCallbackInternal onRedeemEntitlementsCallbackInternal = OnRedeemEntitlementsCallbackInternalImplementation;
		Helper.AddCallback (out clientDataAddress, clientData, completionDelegate, onRedeemEntitlementsCallbackInternal);
		Bindings.EOS_Ecom_RedeemEntitlements (base.InnerHandle, ref options2, clientDataAddress, onRedeemEntitlementsCallbackInternal);
		Helper.Dispose (ref options2);
	}

	[MonoPInvokeCallback (typeof(OnCheckoutCallbackInternal))]
	internal static void OnCheckoutCallbackInternalImplementation (ref CheckoutCallbackInfoInternal data)
	{
		if (Helper.TryGetAndRemoveCallback<CheckoutCallbackInfoInternal, OnCheckoutCallback, CheckoutCallbackInfo> (ref data, out var callback, out var callbackInfo)) {
			callback (ref callbackInfo);
		}
	}

	[MonoPInvokeCallback (typeof(OnQueryEntitlementTokenCallbackInternal))]
	internal static void OnQueryEntitlementTokenCallbackInternalImplementation (ref QueryEntitlementTokenCallbackInfoInternal data)
	{
		if (Helper.TryGetAndRemoveCallback<QueryEntitlementTokenCallbackInfoInternal, OnQueryEntitlementTokenCallback, QueryEntitlementTokenCallbackInfo> (ref data, out var callback, out var callbackInfo)) {
			callback (ref callbackInfo);
		}
	}

	[MonoPInvokeCallback (typeof(OnQueryEntitlementsCallbackInternal))]
	internal static void OnQueryEntitlementsCallbackInternalImplementation (ref QueryEntitlementsCallbackInfoInternal data)
	{
		if (Helper.TryGetAndRemoveCallback<QueryEntitlementsCallbackInfoInternal, OnQueryEntitlementsCallback, QueryEntitlementsCallbackInfo> (ref data, out var callback, out var callbackInfo)) {
			callback (ref callbackInfo);
		}
	}

	[MonoPInvokeCallback (typeof(OnQueryOffersCallbackInternal))]
	internal static void OnQueryOffersCallbackInternalImplementation (ref QueryOffersCallbackInfoInternal data)
	{
		if (Helper.TryGetAndRemoveCallback<QueryOffersCallbackInfoInternal, OnQueryOffersCallback, QueryOffersCallbackInfo> (ref data, out var callback, out var callbackInfo)) {
			callback (ref callbackInfo);
		}
	}

	[MonoPInvokeCallback (typeof(OnQueryOwnershipBySandboxIdsCallbackInternal))]
	internal static void OnQueryOwnershipBySandboxIdsCallbackInternalImplementation (ref QueryOwnershipBySandboxIdsCallbackInfoInternal data)
	{
		if (Helper.TryGetAndRemoveCallback<QueryOwnershipBySandboxIdsCallbackInfoInternal, OnQueryOwnershipBySandboxIdsCallback, QueryOwnershipBySandboxIdsCallbackInfo> (ref data, out var callback, out var callbackInfo)) {
			callback (ref callbackInfo);
		}
	}

	[MonoPInvokeCallback (typeof(OnQueryOwnershipCallbackInternal))]
	internal static void OnQueryOwnershipCallbackInternalImplementation (ref QueryOwnershipCallbackInfoInternal data)
	{
		if (Helper.TryGetAndRemoveCallback<QueryOwnershipCallbackInfoInternal, OnQueryOwnershipCallback, QueryOwnershipCallbackInfo> (ref data, out var callback, out var callbackInfo)) {
			callback (ref callbackInfo);
		}
	}

	[MonoPInvokeCallback (typeof(OnQueryOwnershipTokenCallbackInternal))]
	internal static void OnQueryOwnershipTokenCallbackInternalImplementation (ref QueryOwnershipTokenCallbackInfoInternal data)
	{
		if (Helper.TryGetAndRemoveCallback<QueryOwnershipTokenCallbackInfoInternal, OnQueryOwnershipTokenCallback, QueryOwnershipTokenCallbackInfo> (ref data, out var callback, out var callbackInfo)) {
			callback (ref callbackInfo);
		}
	}

	[MonoPInvokeCallback (typeof(OnRedeemEntitlementsCallbackInternal))]
	internal static void OnRedeemEntitlementsCallbackInternalImplementation (ref RedeemEntitlementsCallbackInfoInternal data)
	{
		if (Helper.TryGetAndRemoveCallback<RedeemEntitlementsCallbackInfoInternal, OnRedeemEntitlementsCallback, RedeemEntitlementsCallbackInfo> (ref data, out var callback, out var callbackInfo)) {
			callback (ref callbackInfo);
		}
	}
}
