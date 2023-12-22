using System;
using System.Runtime.InteropServices;
using Epic.OnlineServices;
using Epic.OnlineServices.AntiCheatCommon;
using Epic.OnlineServices.AntiCheatServer;

[StructLayout (LayoutKind.Sequential, Pack = 8)]
internal struct RegisterClientOptionsInternal : ISettable<RegisterClientOptions>, IDisposable
{
	private int m_ApiVersion;

	private IntPtr m_ClientHandle;

	private AntiCheatCommonClientType m_ClientType;

	private AntiCheatCommonClientPlatform m_ClientPlatform;

	private IntPtr m_AccountId;

	private IntPtr m_IpAddress;

	public IntPtr ClientHandle {
		set {
			m_ClientHandle = value;
		}
	}

	public AntiCheatCommonClientType ClientType {
		set {
			m_ClientType = value;
		}
	}

	public AntiCheatCommonClientPlatform ClientPlatform {
		set {
			m_ClientPlatform = value;
		}
	}

	public Utf8String AccountId {
		set {
			Helper.Set (value, ref m_AccountId);
		}
	}

	public Utf8String IpAddress {
		set {
			Helper.Set (value, ref m_IpAddress);
		}
	}

	public void Set (ref RegisterClientOptions other)
	{
		m_ApiVersion = 1;
		ClientHandle = other.ClientHandle;
		ClientType = other.ClientType;
		ClientPlatform = other.ClientPlatform;
		AccountId = other.AccountId;
		IpAddress = other.IpAddress;
	}

	public void Set (ref RegisterClientOptions? other)
	{
		if (other.HasValue) {
			m_ApiVersion = 1;
			ClientHandle = other.Value.ClientHandle;
			ClientType = other.Value.ClientType;
			ClientPlatform = other.Value.ClientPlatform;
			AccountId = other.Value.AccountId;
			IpAddress = other.Value.IpAddress;
		}
	}

	public void Dispose ()
	{
		Helper.Dispose (ref m_ClientHandle);
		Helper.Dispose (ref m_AccountId);
		Helper.Dispose (ref m_IpAddress);
	}
}
