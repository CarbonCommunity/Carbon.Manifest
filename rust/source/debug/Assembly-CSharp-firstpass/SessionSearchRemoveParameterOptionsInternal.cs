using System;
using System.Runtime.InteropServices;
using Epic.OnlineServices;
using Epic.OnlineServices.Sessions;

[StructLayout (LayoutKind.Sequential, Pack = 8)]
internal struct SessionSearchRemoveParameterOptionsInternal : ISettable<SessionSearchRemoveParameterOptions>, IDisposable
{
	private int m_ApiVersion;

	private IntPtr m_Key;

	private ComparisonOp m_ComparisonOp;

	public Utf8String Key {
		set {
			Helper.Set (value, ref m_Key);
		}
	}

	public ComparisonOp ComparisonOp {
		set {
			m_ComparisonOp = value;
		}
	}

	public void Set (ref SessionSearchRemoveParameterOptions other)
	{
		m_ApiVersion = 1;
		Key = other.Key;
		ComparisonOp = other.ComparisonOp;
	}

	public void Set (ref SessionSearchRemoveParameterOptions? other)
	{
		if (other.HasValue) {
			m_ApiVersion = 1;
			Key = other.Value.Key;
			ComparisonOp = other.Value.ComparisonOp;
		}
	}

	public void Dispose ()
	{
		Helper.Dispose (ref m_Key);
	}
}
