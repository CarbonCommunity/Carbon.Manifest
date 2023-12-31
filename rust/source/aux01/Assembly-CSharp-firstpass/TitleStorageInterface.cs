using System;
using Epic.OnlineServices;
using Epic.OnlineServices.TitleStorage;

public sealed class TitleStorageInterface : Handle
{
	public const int CopyfilemetadataatindexoptionsApiLatest = 1;

	public const int CopyfilemetadatabyfilenameoptionsApiLatest = 1;

	public const int DeletecacheoptionsApiLatest = 1;

	public const int FilemetadataApiLatest = 2;

	public const int FilenameMaxLengthBytes = 64;

	public const int GetfilemetadatacountoptionsApiLatest = 1;

	public const int QueryfilelistoptionsApiLatest = 1;

	public const int QueryfileoptionsApiLatest = 1;

	public const int ReadfileoptionsApiLatest = 1;

	public TitleStorageInterface ()
	{
	}

	public TitleStorageInterface (IntPtr innerHandle)
		: base (innerHandle)
	{
	}

	public Result CopyFileMetadataAtIndex (ref CopyFileMetadataAtIndexOptions options, out FileMetadata? outMetadata)
	{
		CopyFileMetadataAtIndexOptionsInternal options2 = default(CopyFileMetadataAtIndexOptionsInternal);
		options2.Set (ref options);
		IntPtr outMetadata2 = IntPtr.Zero;
		Result result = Bindings.EOS_TitleStorage_CopyFileMetadataAtIndex (base.InnerHandle, ref options2, ref outMetadata2);
		Helper.Dispose (ref options2);
		Helper.Get<FileMetadataInternal, FileMetadata> (outMetadata2, out outMetadata);
		if (outMetadata.HasValue) {
			Bindings.EOS_TitleStorage_FileMetadata_Release (outMetadata2);
		}
		return result;
	}

	public Result CopyFileMetadataByFilename (ref CopyFileMetadataByFilenameOptions options, out FileMetadata? outMetadata)
	{
		CopyFileMetadataByFilenameOptionsInternal options2 = default(CopyFileMetadataByFilenameOptionsInternal);
		options2.Set (ref options);
		IntPtr outMetadata2 = IntPtr.Zero;
		Result result = Bindings.EOS_TitleStorage_CopyFileMetadataByFilename (base.InnerHandle, ref options2, ref outMetadata2);
		Helper.Dispose (ref options2);
		Helper.Get<FileMetadataInternal, FileMetadata> (outMetadata2, out outMetadata);
		if (outMetadata.HasValue) {
			Bindings.EOS_TitleStorage_FileMetadata_Release (outMetadata2);
		}
		return result;
	}

	public Result DeleteCache (ref DeleteCacheOptions options, object clientData, OnDeleteCacheCompleteCallback completionCallback)
	{
		DeleteCacheOptionsInternal options2 = default(DeleteCacheOptionsInternal);
		options2.Set (ref options);
		IntPtr clientDataAddress = IntPtr.Zero;
		OnDeleteCacheCompleteCallbackInternal onDeleteCacheCompleteCallbackInternal = OnDeleteCacheCompleteCallbackInternalImplementation;
		Helper.AddCallback (out clientDataAddress, clientData, completionCallback, onDeleteCacheCompleteCallbackInternal);
		Result result = Bindings.EOS_TitleStorage_DeleteCache (base.InnerHandle, ref options2, clientDataAddress, onDeleteCacheCompleteCallbackInternal);
		Helper.Dispose (ref options2);
		return result;
	}

	public uint GetFileMetadataCount (ref GetFileMetadataCountOptions options)
	{
		GetFileMetadataCountOptionsInternal options2 = default(GetFileMetadataCountOptionsInternal);
		options2.Set (ref options);
		uint result = Bindings.EOS_TitleStorage_GetFileMetadataCount (base.InnerHandle, ref options2);
		Helper.Dispose (ref options2);
		return result;
	}

	public void QueryFile (ref QueryFileOptions options, object clientData, OnQueryFileCompleteCallback completionCallback)
	{
		QueryFileOptionsInternal options2 = default(QueryFileOptionsInternal);
		options2.Set (ref options);
		IntPtr clientDataAddress = IntPtr.Zero;
		OnQueryFileCompleteCallbackInternal onQueryFileCompleteCallbackInternal = OnQueryFileCompleteCallbackInternalImplementation;
		Helper.AddCallback (out clientDataAddress, clientData, completionCallback, onQueryFileCompleteCallbackInternal);
		Bindings.EOS_TitleStorage_QueryFile (base.InnerHandle, ref options2, clientDataAddress, onQueryFileCompleteCallbackInternal);
		Helper.Dispose (ref options2);
	}

	public void QueryFileList (ref QueryFileListOptions options, object clientData, OnQueryFileListCompleteCallback completionCallback)
	{
		QueryFileListOptionsInternal options2 = default(QueryFileListOptionsInternal);
		options2.Set (ref options);
		IntPtr clientDataAddress = IntPtr.Zero;
		OnQueryFileListCompleteCallbackInternal onQueryFileListCompleteCallbackInternal = OnQueryFileListCompleteCallbackInternalImplementation;
		Helper.AddCallback (out clientDataAddress, clientData, completionCallback, onQueryFileListCompleteCallbackInternal);
		Bindings.EOS_TitleStorage_QueryFileList (base.InnerHandle, ref options2, clientDataAddress, onQueryFileListCompleteCallbackInternal);
		Helper.Dispose (ref options2);
	}

	public TitleStorageFileTransferRequest ReadFile (ref ReadFileOptions options, object clientData, OnReadFileCompleteCallback completionCallback)
	{
		ReadFileOptionsInternal options2 = default(ReadFileOptionsInternal);
		options2.Set (ref options);
		IntPtr clientDataAddress = IntPtr.Zero;
		OnReadFileCompleteCallbackInternal onReadFileCompleteCallbackInternal = OnReadFileCompleteCallbackInternalImplementation;
		Helper.AddCallback (out clientDataAddress, clientData, completionCallback, onReadFileCompleteCallbackInternal, options.ReadFileDataCallback, ReadFileOptionsInternal.ReadFileDataCallback, options.FileTransferProgressCallback, ReadFileOptionsInternal.FileTransferProgressCallback);
		IntPtr from = Bindings.EOS_TitleStorage_ReadFile (base.InnerHandle, ref options2, clientDataAddress, onReadFileCompleteCallbackInternal);
		Helper.Dispose (ref options2);
		Helper.Get (from, out TitleStorageFileTransferRequest to);
		return to;
	}

	[MonoPInvokeCallback (typeof(OnDeleteCacheCompleteCallbackInternal))]
	internal static void OnDeleteCacheCompleteCallbackInternalImplementation (ref DeleteCacheCallbackInfoInternal data)
	{
		if (Helper.TryGetAndRemoveCallback<DeleteCacheCallbackInfoInternal, OnDeleteCacheCompleteCallback, DeleteCacheCallbackInfo> (ref data, out var callback, out var callbackInfo)) {
			callback (ref callbackInfo);
		}
	}

	[MonoPInvokeCallback (typeof(OnFileTransferProgressCallbackInternal))]
	internal static void OnFileTransferProgressCallbackInternalImplementation (ref FileTransferProgressCallbackInfoInternal data)
	{
		if (Helper.TryGetStructCallback<FileTransferProgressCallbackInfoInternal, OnFileTransferProgressCallback, FileTransferProgressCallbackInfo> (ref data, out var callback, out var _)) {
			Helper.Get<FileTransferProgressCallbackInfoInternal, FileTransferProgressCallbackInfo> (ref data, out var to);
			callback (ref to);
		}
	}

	[MonoPInvokeCallback (typeof(OnQueryFileCompleteCallbackInternal))]
	internal static void OnQueryFileCompleteCallbackInternalImplementation (ref QueryFileCallbackInfoInternal data)
	{
		if (Helper.TryGetAndRemoveCallback<QueryFileCallbackInfoInternal, OnQueryFileCompleteCallback, QueryFileCallbackInfo> (ref data, out var callback, out var callbackInfo)) {
			callback (ref callbackInfo);
		}
	}

	[MonoPInvokeCallback (typeof(OnQueryFileListCompleteCallbackInternal))]
	internal static void OnQueryFileListCompleteCallbackInternalImplementation (ref QueryFileListCallbackInfoInternal data)
	{
		if (Helper.TryGetAndRemoveCallback<QueryFileListCallbackInfoInternal, OnQueryFileListCompleteCallback, QueryFileListCallbackInfo> (ref data, out var callback, out var callbackInfo)) {
			callback (ref callbackInfo);
		}
	}

	[MonoPInvokeCallback (typeof(OnReadFileCompleteCallbackInternal))]
	internal static void OnReadFileCompleteCallbackInternalImplementation (ref ReadFileCallbackInfoInternal data)
	{
		if (Helper.TryGetAndRemoveCallback<ReadFileCallbackInfoInternal, OnReadFileCompleteCallback, ReadFileCallbackInfo> (ref data, out var callback, out var callbackInfo)) {
			callback (ref callbackInfo);
		}
	}

	[MonoPInvokeCallback (typeof(OnReadFileDataCallbackInternal))]
	internal static ReadResult OnReadFileDataCallbackInternalImplementation (ref ReadFileDataCallbackInfoInternal data)
	{
		if (Helper.TryGetStructCallback<ReadFileDataCallbackInfoInternal, OnReadFileDataCallback, ReadFileDataCallbackInfo> (ref data, out var callback, out var _)) {
			Helper.Get<ReadFileDataCallbackInfoInternal, ReadFileDataCallbackInfo> (ref data, out var to);
			return callback (ref to);
		}
		return Helper.GetDefault<ReadResult> ();
	}
}
