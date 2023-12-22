using System;
using Epic.OnlineServices;
using Epic.OnlineServices.IntegratedPlatform;

public sealed class IntegratedPlatformInterface
{
	public const int CreateintegratedplatformoptionscontainerApiLatest = 1;

	public static readonly Utf8String IptSteam = "STEAM";

	public const int OptionsApiLatest = 1;

	public const int SteamOptionsApiLatest = 2;

	public static Result CreateIntegratedPlatformOptionsContainer (ref CreateIntegratedPlatformOptionsContainerOptions options, out IntegratedPlatformOptionsContainer outIntegratedPlatformOptionsContainerHandle)
	{
		CreateIntegratedPlatformOptionsContainerOptionsInternal options2 = default(CreateIntegratedPlatformOptionsContainerOptionsInternal);
		options2.Set (ref options);
		IntPtr outIntegratedPlatformOptionsContainerHandle2 = IntPtr.Zero;
		Result result = Bindings.EOS_IntegratedPlatform_CreateIntegratedPlatformOptionsContainer (ref options2, ref outIntegratedPlatformOptionsContainerHandle2);
		Helper.Dispose (ref options2);
		Helper.Get (outIntegratedPlatformOptionsContainerHandle2, out outIntegratedPlatformOptionsContainerHandle);
		return result;
	}
}
