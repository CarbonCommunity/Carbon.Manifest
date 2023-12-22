using Epic.OnlineServices;
using Epic.OnlineServices.RTCAudio;

public struct InputDeviceInformation
{
	public bool DefaultDevice { get; set; }

	public Utf8String DeviceId { get; set; }

	public Utf8String DeviceName { get; set; }

	internal void Set (ref InputDeviceInformationInternal other)
	{
		DefaultDevice = other.DefaultDevice;
		DeviceId = other.DeviceId;
		DeviceName = other.DeviceName;
	}
}
