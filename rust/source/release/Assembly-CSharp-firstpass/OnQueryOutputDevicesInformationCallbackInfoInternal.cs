using System;
using System.Runtime.InteropServices;
using Epic.OnlineServices;
using Epic.OnlineServices.RTCAudio;

[StructLayout (LayoutKind.Sequential, Pack = 8)]
internal struct OnQueryOutputDevicesInformationCallbackInfoInternal : ICallbackInfoInternal, IGettable<OnQueryOutputDevicesInformationCallbackInfo>, ISettable<OnQueryOutputDevicesInformationCallbackInfo>, IDisposable
{
	private Result m_ResultCode;

	private IntPtr m_ClientData;

	public Result ResultCode {
		get {
			return m_ResultCode;
		}
		set {
			m_ResultCode = value;
		}
	}

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

	public void Set (ref OnQueryOutputDevicesInformationCallbackInfo other)
	{
		ResultCode = other.ResultCode;
		ClientData = other.ClientData;
	}

	public void Set (ref OnQueryOutputDevicesInformationCallbackInfo? other)
	{
		if (other.HasValue) {
			ResultCode = other.Value.ResultCode;
			ClientData = other.Value.ClientData;
		}
	}

	public void Dispose ()
	{
		Helper.Dispose (ref m_ClientData);
	}

	public void Get (out OnQueryOutputDevicesInformationCallbackInfo output)
	{
		output = default(OnQueryOutputDevicesInformationCallbackInfo);
		output.Set (ref this);
	}
}
