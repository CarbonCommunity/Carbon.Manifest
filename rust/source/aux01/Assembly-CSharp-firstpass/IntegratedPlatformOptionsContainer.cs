using System;
using Epic.OnlineServices;
using Epic.OnlineServices.IntegratedPlatform;

public sealed class IntegratedPlatformOptionsContainer : Handle
{
	public const int IntegratedplatformoptionscontainerAddApiLatest = 1;

	public IntegratedPlatformOptionsContainer ()
	{
	}

	public IntegratedPlatformOptionsContainer (IntPtr innerHandle)
		: base (innerHandle)
	{
	}

	public Result Add (ref IntegratedPlatformOptionsContainerAddOptions inOptions)
	{
		IntegratedPlatformOptionsContainerAddOptionsInternal inOptions2 = default(IntegratedPlatformOptionsContainerAddOptionsInternal);
		inOptions2.Set (ref inOptions);
		Result result = Bindings.EOS_IntegratedPlatformOptionsContainer_Add (base.InnerHandle, ref inOptions2);
		Helper.Dispose (ref inOptions2);
		return result;
	}

	public void Release ()
	{
		Bindings.EOS_IntegratedPlatformOptionsContainer_Release (base.InnerHandle);
	}
}
