using System;
using Epic.OnlineServices;
using Epic.OnlineServices.Mods;

public sealed class ModsInterface : Handle
{
	public const int CopymodinfoApiLatest = 1;

	public const int EnumeratemodsApiLatest = 1;

	public const int InstallmodApiLatest = 1;

	public const int ModIdentifierApiLatest = 1;

	public const int ModinfoApiLatest = 1;

	public const int UninstallmodApiLatest = 1;

	public const int UpdatemodApiLatest = 1;

	public ModsInterface ()
	{
	}

	public ModsInterface (IntPtr innerHandle)
		: base (innerHandle)
	{
	}

	public Result CopyModInfo (ref CopyModInfoOptions options, out ModInfo? outEnumeratedMods)
	{
		CopyModInfoOptionsInternal options2 = default(CopyModInfoOptionsInternal);
		options2.Set (ref options);
		IntPtr outEnumeratedMods2 = IntPtr.Zero;
		Result result = Bindings.EOS_Mods_CopyModInfo (base.InnerHandle, ref options2, ref outEnumeratedMods2);
		Helper.Dispose (ref options2);
		Helper.Get<ModInfoInternal, ModInfo> (outEnumeratedMods2, out outEnumeratedMods);
		if (outEnumeratedMods.HasValue) {
			Bindings.EOS_Mods_ModInfo_Release (outEnumeratedMods2);
		}
		return result;
	}

	public void EnumerateMods (ref EnumerateModsOptions options, object clientData, OnEnumerateModsCallback completionDelegate)
	{
		EnumerateModsOptionsInternal options2 = default(EnumerateModsOptionsInternal);
		options2.Set (ref options);
		IntPtr clientDataAddress = IntPtr.Zero;
		OnEnumerateModsCallbackInternal onEnumerateModsCallbackInternal = OnEnumerateModsCallbackInternalImplementation;
		Helper.AddCallback (out clientDataAddress, clientData, completionDelegate, onEnumerateModsCallbackInternal);
		Bindings.EOS_Mods_EnumerateMods (base.InnerHandle, ref options2, clientDataAddress, onEnumerateModsCallbackInternal);
		Helper.Dispose (ref options2);
	}

	public void InstallMod (ref InstallModOptions options, object clientData, OnInstallModCallback completionDelegate)
	{
		InstallModOptionsInternal options2 = default(InstallModOptionsInternal);
		options2.Set (ref options);
		IntPtr clientDataAddress = IntPtr.Zero;
		OnInstallModCallbackInternal onInstallModCallbackInternal = OnInstallModCallbackInternalImplementation;
		Helper.AddCallback (out clientDataAddress, clientData, completionDelegate, onInstallModCallbackInternal);
		Bindings.EOS_Mods_InstallMod (base.InnerHandle, ref options2, clientDataAddress, onInstallModCallbackInternal);
		Helper.Dispose (ref options2);
	}

	public void UninstallMod (ref UninstallModOptions options, object clientData, OnUninstallModCallback completionDelegate)
	{
		UninstallModOptionsInternal options2 = default(UninstallModOptionsInternal);
		options2.Set (ref options);
		IntPtr clientDataAddress = IntPtr.Zero;
		OnUninstallModCallbackInternal onUninstallModCallbackInternal = OnUninstallModCallbackInternalImplementation;
		Helper.AddCallback (out clientDataAddress, clientData, completionDelegate, onUninstallModCallbackInternal);
		Bindings.EOS_Mods_UninstallMod (base.InnerHandle, ref options2, clientDataAddress, onUninstallModCallbackInternal);
		Helper.Dispose (ref options2);
	}

	public void UpdateMod (ref UpdateModOptions options, object clientData, OnUpdateModCallback completionDelegate)
	{
		UpdateModOptionsInternal options2 = default(UpdateModOptionsInternal);
		options2.Set (ref options);
		IntPtr clientDataAddress = IntPtr.Zero;
		OnUpdateModCallbackInternal onUpdateModCallbackInternal = OnUpdateModCallbackInternalImplementation;
		Helper.AddCallback (out clientDataAddress, clientData, completionDelegate, onUpdateModCallbackInternal);
		Bindings.EOS_Mods_UpdateMod (base.InnerHandle, ref options2, clientDataAddress, onUpdateModCallbackInternal);
		Helper.Dispose (ref options2);
	}

	[MonoPInvokeCallback (typeof(OnEnumerateModsCallbackInternal))]
	internal static void OnEnumerateModsCallbackInternalImplementation (ref EnumerateModsCallbackInfoInternal data)
	{
		if (Helper.TryGetAndRemoveCallback<EnumerateModsCallbackInfoInternal, OnEnumerateModsCallback, EnumerateModsCallbackInfo> (ref data, out var callback, out var callbackInfo)) {
			callback (ref callbackInfo);
		}
	}

	[MonoPInvokeCallback (typeof(OnInstallModCallbackInternal))]
	internal static void OnInstallModCallbackInternalImplementation (ref InstallModCallbackInfoInternal data)
	{
		if (Helper.TryGetAndRemoveCallback<InstallModCallbackInfoInternal, OnInstallModCallback, InstallModCallbackInfo> (ref data, out var callback, out var callbackInfo)) {
			callback (ref callbackInfo);
		}
	}

	[MonoPInvokeCallback (typeof(OnUninstallModCallbackInternal))]
	internal static void OnUninstallModCallbackInternalImplementation (ref UninstallModCallbackInfoInternal data)
	{
		if (Helper.TryGetAndRemoveCallback<UninstallModCallbackInfoInternal, OnUninstallModCallback, UninstallModCallbackInfo> (ref data, out var callback, out var callbackInfo)) {
			callback (ref callbackInfo);
		}
	}

	[MonoPInvokeCallback (typeof(OnUpdateModCallbackInternal))]
	internal static void OnUpdateModCallbackInternalImplementation (ref UpdateModCallbackInfoInternal data)
	{
		if (Helper.TryGetAndRemoveCallback<UpdateModCallbackInfoInternal, OnUpdateModCallback, UpdateModCallbackInfo> (ref data, out var callback, out var callbackInfo)) {
			callback (ref callbackInfo);
		}
	}
}
