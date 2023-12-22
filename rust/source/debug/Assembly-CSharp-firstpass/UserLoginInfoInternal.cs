using System;
using System.Runtime.InteropServices;
using Epic.OnlineServices;
using Epic.OnlineServices.Connect;

[StructLayout (LayoutKind.Sequential, Pack = 8)]
internal struct UserLoginInfoInternal : IGettable<UserLoginInfo>, ISettable<UserLoginInfo>, IDisposable
{
	private int m_ApiVersion;

	private IntPtr m_DisplayName;

	public Utf8String DisplayName {
		get {
			Helper.Get (m_DisplayName, out Utf8String to);
			return to;
		}
		set {
			Helper.Set (value, ref m_DisplayName);
		}
	}

	public void Set (ref UserLoginInfo other)
	{
		m_ApiVersion = 1;
		DisplayName = other.DisplayName;
	}

	public void Set (ref UserLoginInfo? other)
	{
		if (other.HasValue) {
			m_ApiVersion = 1;
			DisplayName = other.Value.DisplayName;
		}
	}

	public void Dispose ()
	{
		Helper.Dispose (ref m_DisplayName);
	}

	public void Get (out UserLoginInfo output)
	{
		output = default(UserLoginInfo);
		output.Set (ref this);
	}
}
