using System;
using System.Runtime.InteropServices;
using Epic.OnlineServices;
using Epic.OnlineServices.Sessions;

[StructLayout (LayoutKind.Sequential, Pack = 8)]
internal struct SessionDetailsInfoInternal : IGettable<SessionDetailsInfo>, ISettable<SessionDetailsInfo>, IDisposable
{
	private int m_ApiVersion;

	private IntPtr m_SessionId;

	private IntPtr m_HostAddress;

	private uint m_NumOpenPublicConnections;

	private IntPtr m_Settings;

	public Utf8String SessionId {
		get {
			Helper.Get (m_SessionId, out Utf8String to);
			return to;
		}
		set {
			Helper.Set (value, ref m_SessionId);
		}
	}

	public Utf8String HostAddress {
		get {
			Helper.Get (m_HostAddress, out Utf8String to);
			return to;
		}
		set {
			Helper.Set (value, ref m_HostAddress);
		}
	}

	public uint NumOpenPublicConnections {
		get {
			return m_NumOpenPublicConnections;
		}
		set {
			m_NumOpenPublicConnections = value;
		}
	}

	public SessionDetailsSettings? Settings {
		get {
			Helper.Get<SessionDetailsSettingsInternal, SessionDetailsSettings> (m_Settings, out SessionDetailsSettings? to);
			return to;
		}
		set {
			Helper.Set<SessionDetailsSettings, SessionDetailsSettingsInternal> (ref value, ref m_Settings);
		}
	}

	public void Set (ref SessionDetailsInfo other)
	{
		m_ApiVersion = 1;
		SessionId = other.SessionId;
		HostAddress = other.HostAddress;
		NumOpenPublicConnections = other.NumOpenPublicConnections;
		Settings = other.Settings;
	}

	public void Set (ref SessionDetailsInfo? other)
	{
		if (other.HasValue) {
			m_ApiVersion = 1;
			SessionId = other.Value.SessionId;
			HostAddress = other.Value.HostAddress;
			NumOpenPublicConnections = other.Value.NumOpenPublicConnections;
			Settings = other.Value.Settings;
		}
	}

	public void Dispose ()
	{
		Helper.Dispose (ref m_SessionId);
		Helper.Dispose (ref m_HostAddress);
		Helper.Dispose (ref m_Settings);
	}

	public void Get (out SessionDetailsInfo output)
	{
		output = default(SessionDetailsInfo);
		output.Set (ref this);
	}
}
