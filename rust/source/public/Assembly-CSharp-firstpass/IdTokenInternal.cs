using System;
using System.Runtime.InteropServices;
using Epic.OnlineServices;
using Epic.OnlineServices.Auth;

[StructLayout (LayoutKind.Sequential, Pack = 8)]
internal struct IdTokenInternal : IGettable<IdToken>, ISettable<IdToken>, IDisposable
{
	private int m_ApiVersion;

	private IntPtr m_AccountId;

	private IntPtr m_JsonWebToken;

	public EpicAccountId AccountId {
		get {
			Helper.Get (m_AccountId, out EpicAccountId to);
			return to;
		}
		set {
			Helper.Set (value, ref m_AccountId);
		}
	}

	public Utf8String JsonWebToken {
		get {
			Helper.Get (m_JsonWebToken, out Utf8String to);
			return to;
		}
		set {
			Helper.Set (value, ref m_JsonWebToken);
		}
	}

	public void Set (ref IdToken other)
	{
		m_ApiVersion = 1;
		AccountId = other.AccountId;
		JsonWebToken = other.JsonWebToken;
	}

	public void Set (ref IdToken? other)
	{
		if (other.HasValue) {
			m_ApiVersion = 1;
			AccountId = other.Value.AccountId;
			JsonWebToken = other.Value.JsonWebToken;
		}
	}

	public void Dispose ()
	{
		Helper.Dispose (ref m_AccountId);
		Helper.Dispose (ref m_JsonWebToken);
	}

	public void Get (out IdToken output)
	{
		output = default(IdToken);
		output.Set (ref this);
	}
}
