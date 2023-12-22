using System;
using System.Runtime.InteropServices;
using Epic.OnlineServices;
using Epic.OnlineServices.Auth;

[StructLayout (LayoutKind.Sequential, Pack = 8)]
internal struct IOSLoginOptionsInternal : ISettable<IOSLoginOptions>, IDisposable
{
	private int m_ApiVersion;

	private IntPtr m_Credentials;

	private AuthScopeFlags m_ScopeFlags;

	public IOSCredentials? Credentials {
		set {
			Helper.Set<IOSCredentials, IOSCredentialsInternal> (ref value, ref m_Credentials);
		}
	}

	public AuthScopeFlags ScopeFlags {
		set {
			m_ScopeFlags = value;
		}
	}

	public void Set (ref IOSLoginOptions other)
	{
		m_ApiVersion = 2;
		Credentials = other.Credentials;
		ScopeFlags = other.ScopeFlags;
	}

	public void Set (ref IOSLoginOptions? other)
	{
		if (other.HasValue) {
			m_ApiVersion = 2;
			Credentials = other.Value.Credentials;
			ScopeFlags = other.Value.ScopeFlags;
		}
	}

	public void Dispose ()
	{
		Helper.Dispose (ref m_Credentials);
	}
}
