using System;
using Epic.OnlineServices;
using Epic.OnlineServices.ProgressionSnapshot;

public sealed class ProgressionSnapshotInterface : Handle
{
	public const int AddprogressionApiLatest = 1;

	public const int BeginsnapshotApiLatest = 1;

	public const int DeletesnapshotApiLatest = 1;

	public const int EndsnapshotApiLatest = 1;

	public const int InvalidProgressionsnapshotid = 0;

	public const int SubmitsnapshotApiLatest = 1;

	public ProgressionSnapshotInterface ()
	{
	}

	public ProgressionSnapshotInterface (IntPtr innerHandle)
		: base (innerHandle)
	{
	}

	public Result AddProgression (ref AddProgressionOptions options)
	{
		AddProgressionOptionsInternal options2 = default(AddProgressionOptionsInternal);
		options2.Set (ref options);
		Result result = Bindings.EOS_ProgressionSnapshot_AddProgression (base.InnerHandle, ref options2);
		Helper.Dispose (ref options2);
		return result;
	}

	public Result BeginSnapshot (ref BeginSnapshotOptions options, out uint outSnapshotId)
	{
		BeginSnapshotOptionsInternal options2 = default(BeginSnapshotOptionsInternal);
		options2.Set (ref options);
		outSnapshotId = Helper.GetDefault<uint> ();
		Result result = Bindings.EOS_ProgressionSnapshot_BeginSnapshot (base.InnerHandle, ref options2, ref outSnapshotId);
		Helper.Dispose (ref options2);
		return result;
	}

	public void DeleteSnapshot (ref DeleteSnapshotOptions options, object clientData, OnDeleteSnapshotCallback completionDelegate)
	{
		DeleteSnapshotOptionsInternal options2 = default(DeleteSnapshotOptionsInternal);
		options2.Set (ref options);
		IntPtr clientDataAddress = IntPtr.Zero;
		OnDeleteSnapshotCallbackInternal onDeleteSnapshotCallbackInternal = OnDeleteSnapshotCallbackInternalImplementation;
		Helper.AddCallback (out clientDataAddress, clientData, completionDelegate, onDeleteSnapshotCallbackInternal);
		Bindings.EOS_ProgressionSnapshot_DeleteSnapshot (base.InnerHandle, ref options2, clientDataAddress, onDeleteSnapshotCallbackInternal);
		Helper.Dispose (ref options2);
	}

	public Result EndSnapshot (ref EndSnapshotOptions options)
	{
		EndSnapshotOptionsInternal options2 = default(EndSnapshotOptionsInternal);
		options2.Set (ref options);
		Result result = Bindings.EOS_ProgressionSnapshot_EndSnapshot (base.InnerHandle, ref options2);
		Helper.Dispose (ref options2);
		return result;
	}

	public void SubmitSnapshot (ref SubmitSnapshotOptions options, object clientData, OnSubmitSnapshotCallback completionDelegate)
	{
		SubmitSnapshotOptionsInternal options2 = default(SubmitSnapshotOptionsInternal);
		options2.Set (ref options);
		IntPtr clientDataAddress = IntPtr.Zero;
		OnSubmitSnapshotCallbackInternal onSubmitSnapshotCallbackInternal = OnSubmitSnapshotCallbackInternalImplementation;
		Helper.AddCallback (out clientDataAddress, clientData, completionDelegate, onSubmitSnapshotCallbackInternal);
		Bindings.EOS_ProgressionSnapshot_SubmitSnapshot (base.InnerHandle, ref options2, clientDataAddress, onSubmitSnapshotCallbackInternal);
		Helper.Dispose (ref options2);
	}

	[MonoPInvokeCallback (typeof(OnDeleteSnapshotCallbackInternal))]
	internal static void OnDeleteSnapshotCallbackInternalImplementation (ref DeleteSnapshotCallbackInfoInternal data)
	{
		if (Helper.TryGetAndRemoveCallback<DeleteSnapshotCallbackInfoInternal, OnDeleteSnapshotCallback, DeleteSnapshotCallbackInfo> (ref data, out var callback, out var callbackInfo)) {
			callback (ref callbackInfo);
		}
	}

	[MonoPInvokeCallback (typeof(OnSubmitSnapshotCallbackInternal))]
	internal static void OnSubmitSnapshotCallbackInternalImplementation (ref SubmitSnapshotCallbackInfoInternal data)
	{
		if (Helper.TryGetAndRemoveCallback<SubmitSnapshotCallbackInfoInternal, OnSubmitSnapshotCallback, SubmitSnapshotCallbackInfo> (ref data, out var callback, out var callbackInfo)) {
			callback (ref callbackInfo);
		}
	}
}
