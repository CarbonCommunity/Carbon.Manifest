using System;
using System.Runtime.InteropServices;
using Epic.OnlineServices;
using Epic.OnlineServices.RTCAudio;

[StructLayout (LayoutKind.Sequential, Pack = 8)]
internal struct AudioBeforeRenderCallbackInfoInternal : ICallbackInfoInternal, IGettable<AudioBeforeRenderCallbackInfo>, ISettable<AudioBeforeRenderCallbackInfo>, IDisposable
{
	private IntPtr m_ClientData;

	private IntPtr m_LocalUserId;

	private IntPtr m_RoomName;

	private IntPtr m_Buffer;

	private IntPtr m_ParticipantId;

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

	public AudioBuffer? Buffer {
		get {
			Helper.Get<AudioBufferInternal, AudioBuffer> (m_Buffer, out AudioBuffer? to);
			return to;
		}
		set {
			Helper.Set<AudioBuffer, AudioBufferInternal> (ref value, ref m_Buffer);
		}
	}

	public ProductUserId ParticipantId {
		get {
			Helper.Get (m_ParticipantId, out ProductUserId to);
			return to;
		}
		set {
			Helper.Set (value, ref m_ParticipantId);
		}
	}

	public void Set (ref AudioBeforeRenderCallbackInfo other)
	{
		ClientData = other.ClientData;
		LocalUserId = other.LocalUserId;
		RoomName = other.RoomName;
		Buffer = other.Buffer;
		ParticipantId = other.ParticipantId;
	}

	public void Set (ref AudioBeforeRenderCallbackInfo? other)
	{
		if (other.HasValue) {
			ClientData = other.Value.ClientData;
			LocalUserId = other.Value.LocalUserId;
			RoomName = other.Value.RoomName;
			Buffer = other.Value.Buffer;
			ParticipantId = other.Value.ParticipantId;
		}
	}

	public void Dispose ()
	{
		Helper.Dispose (ref m_ClientData);
		Helper.Dispose (ref m_LocalUserId);
		Helper.Dispose (ref m_RoomName);
		Helper.Dispose (ref m_Buffer);
		Helper.Dispose (ref m_ParticipantId);
	}

	public void Get (out AudioBeforeRenderCallbackInfo output)
	{
		output = default(AudioBeforeRenderCallbackInfo);
		output.Set (ref this);
	}
}
