using System;
using System.Runtime.InteropServices;
using Epic.OnlineServices;
using Epic.OnlineServices.RTCAudio;

[StructLayout (LayoutKind.Sequential, Pack = 8)]
internal struct AudioOutputStateCallbackInfoInternal : ICallbackInfoInternal, IGettable<AudioOutputStateCallbackInfo>, ISettable<AudioOutputStateCallbackInfo>, IDisposable
{
	private IntPtr m_ClientData;

	private IntPtr m_LocalUserId;

	private IntPtr m_RoomName;

	private RTCAudioOutputStatus m_Status;

	public object ClientData {
		get {
			Helper.Get (m_ClientData, out object to);
			return to;
		}
		set {
			Helper.Set (value, ref m_ClientData);
		}
	}

	public IntPtr ClientDataAddress => m_ClientData;

	public ProductUserId LocalUserId {
		get {
			Helper.Get (m_LocalUserId, out ProductUserId to);
			return to;
		}
		set {
			Helper.Set (value, ref m_LocalUserId);
		}
	}

	public Utf8String RoomName {
		get {
			Helper.Get (m_RoomName, out Utf8String to);
			return to;
		}
		set {
			Helper.Set (value, ref m_RoomName);
		}
	}

	public RTCAudioOutputStatus Status {
		get {
			return m_Status;
		}
		set {
			m_Status = value;
		}
	}

	public void Set (ref AudioOutputStateCallbackInfo other)
	{
		ClientData = other.ClientData;
		LocalUserId = other.LocalUserId;
		RoomName = other.RoomName;
		Status = other.Status;
	}

	public void Set (ref AudioOutputStateCallbackInfo? other)
	{
		if (other.HasValue) {
			ClientData = other.Value.ClientData;
			LocalUserId = other.Value.LocalUserId;
			RoomName = other.Value.RoomName;
			Status = other.Value.Status;
		}
	}

	public void Dispose ()
	{
		Helper.Dispose (ref m_ClientData);
		Helper.Dispose (ref m_LocalUserId);
		Helper.Dispose (ref m_RoomName);
	}

	public void Get (out AudioOutputStateCallbackInfo output)
	{
		output = default(AudioOutputStateCallbackInfo);
		output.Set (ref this);
	}
}
