using System;
using System.Runtime.InteropServices;
using Epic.OnlineServices;
using Epic.OnlineServices.ProgressionSnapshot;

[StructLayout (LayoutKind.Sequential, Pack = 8)]
internal struct EndSnapshotOptionsInternal : ISettable<EndSnapshotOptions>, IDisposable
{
	private int m_ApiVersion;

	private uint m_SnapshotId;

	public uint SnapshotId {
		set {
			m_SnapshotId = value;
		}
	}

	public void Set (ref EndSnapshotOptions other)
	{
		m_ApiVersion = 1;
		SnapshotId = other.SnapshotId;
	}

	public void Set (ref EndSnapshotOptions? other)
	{
		if (other.HasValue) {
			m_ApiVersion = 1;
			SnapshotId = other.Value.SnapshotId;
		}
	}

	public void Dispose ()
	{
	}
}
