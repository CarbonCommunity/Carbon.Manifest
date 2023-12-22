using System;
using System.Runtime.InteropServices;
using Epic.OnlineServices;
using Epic.OnlineServices.Auth;

[StructLayout (LayoutKind.Sequential, Pack = 8)]
internal struct LoginOptionsInternal : ISettable<LoginOptions>, IDisposable
{
	private int m_ApiVersion;

	private IntPtr m_Credentials;

	private AuthScopeFlags m_ScopeFlags;

	public Credentials? Credentials {
		set {
			Helper.Set<Credentials, CredentialsInternal> (ref value, ref m_Credentials);
		}
	}

	public AuthScopeFlags ScopeFlags {
		set {
			m_ScopeFlags = value;
		}
	}

	public void Set (ref LoginOptions other)
	{
		m_ApiVersion = 2;
		Credentials = other.Credentials;
		ScopeFlags = other.ScopeFlags;
	}

	public void Set (ref LoginOptions? other)
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
