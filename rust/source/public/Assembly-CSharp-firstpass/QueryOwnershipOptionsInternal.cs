using System;
using System.Runtime.InteropServices;
using Epic.OnlineServices;
using Epic.OnlineServices.Ecom;

[StructLayout (LayoutKind.Sequential, Pack = 8)]
internal struct QueryOwnershipOptionsInternal : ISettable<QueryOwnershipOptions>, IDisposable
{
	private int m_ApiVersion;

	private IntPtr m_LocalUserId;

	private IntPtr m_CatalogItemIds;

	private uint m_CatalogItemIdCount;

	private IntPtr m_CatalogNamespace;

	public EpicAccountId LocalUserId {
		set {
			Helper.Set (value, ref m_LocalUserId);
		}
	}

	public Utf8String[] CatalogItemIds {
		set {
			Helper.Set (value, ref m_CatalogItemIds, out m_CatalogItemIdCount);
		}
	}

	public Utf8String CatalogNamespace {
		set {
			Helper.Set (value, ref m_CatalogNamespace);
		}
	}

	public void Set (ref QueryOwnershipOptions other)
	{
		m_ApiVersion = 2;
		LocalUserId = other.LocalUserId;
		CatalogItemIds = other.CatalogItemIds;
		CatalogNamespace = other.CatalogNamespace;
	}

	public void Set (ref QueryOwnershipOptions? other)
	{
		if (other.HasValue) {
			m_ApiVersion = 2;
			LocalUserId = other.Value.LocalUserId;
			CatalogItemIds = other.Value.CatalogItemIds;
			CatalogNamespace = other.Value.CatalogNamespace;
		}
	}

	public void Dispose ()
	{
		Helper.Dispose (ref m_LocalUserId);
		Helper.Dispose (ref m_CatalogItemIds);
		Helper.Dispose (ref m_CatalogNamespace);
	}
}
