using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Epic.OnlineServices;

public sealed class Helper
{
	private struct Allocation
	{
		public int Size { get; private set; }

		public object Cache { get; private set; }

		public bool? IsArrayItemAllocated { get; private set; }

		public Allocation (int size, object cache, bool? isArrayItemAllocated = null)
		{
			Size = size;
			Cache = cache;
			IsArrayItemAllocated = isArrayItemAllocated;
		}
	}

	private struct PinnedBuffer
	{
		public GCHandle Handle { get; private set; }

		public int RefCount { get; set; }

		public PinnedBuffer (GCHandle handle)
		{
			Handle = handle;
			RefCount = 1;
		}
	}

	private class DelegateHolder
	{
		public Delegate Public { get; private set; }

		public Delegate Private { get; private set; }

		public Delegate[] StructDelegates { get; private set; }

		public ulong? NotificationId { get; set; }

		public DelegateHolder (Delegate publicDelegate, Delegate privateDelegate, params Delegate[] structDelegates)
		{
			Public = publicDelegate;
			Private = privateDelegate;
			StructDelegates = structDelegates;
		}
	}

	private static Dictionary<long, Allocation> s_Allocations = new Dictionary<long, Allocation> ();

	private static Dictionary<long, PinnedBuffer> s_PinnedBuffers = new Dictionary<long, PinnedBuffer> ();

	private static Dictionary<IntPtr, DelegateHolder> s_Callbacks = new Dictionary<IntPtr, DelegateHolder> ();

	private static Dictionary<string, DelegateHolder> s_StaticCallbacks = new Dictionary<string, DelegateHolder> ();

	private static long s_LastClientDataId = 0L;

	private static Dictionary<IntPtr, object> s_ClientDatas = new Dictionary<IntPtr, object> ();

	internal static void AddCallback (out IntPtr clientDataAddress, object clientData, Delegate publicDelegate, Delegate privateDelegate, params Delegate[] structDelegates)
	{
		lock (s_Callbacks) {
			clientDataAddress = AddClientData (clientData);
			s_Callbacks.Add (clientDataAddress, new DelegateHolder (publicDelegate, privateDelegate, structDelegates));
		}
	}

	private static void RemoveCallback (IntPtr clientDataAddress)
	{
		lock (s_Callbacks) {
			s_Callbacks.Remove (clientDataAddress);
			RemoveClientData (clientDataAddress);
		}
	}

	internal static bool TryGetAndRemoveCallback<TCallbackInfoInternal, TCallback, TCallbackInfo> (ref TCallbackInfoInternal callbackInfoInternal, out TCallback callback, out TCallbackInfo callbackInfo) where TCallbackInfoInternal : struct, ICallbackInfoInternal, IGettable<TCallbackInfo> where TCallback : class where TCallbackInfo : struct, ICallbackInfo
	{
		Get<TCallbackInfoInternal, TCallbackInfo> (ref callbackInfoInternal, out callbackInfo, out var clientDataAddress);
		callback = null;
		lock (s_Callbacks) {
			if (s_Callbacks.TryGetValue (clientDataAddress, out var value)) {
				callback = value.Public as TCallback;
				if (callback != null) {
					if (!value.NotificationId.HasValue && callbackInfo.GetResultCode ().HasValue && Common.IsOperationComplete (callbackInfo.GetResultCode ().Value)) {
						RemoveCallback (clientDataAddress);
					}
					return true;
				}
			}
		}
		return false;
	}

	internal static bool TryGetStructCallback<TCallbackInfoInternal, TCallback, TCallbackInfo> (ref TCallbackInfoInternal callbackInfoInternal, out TCallback callback, out TCallbackInfo callbackInfo) where TCallbackInfoInternal : struct, ICallbackInfoInternal, IGettable<TCallbackInfo> where TCallback : class where TCallbackInfo : struct
	{
		Get<TCallbackInfoInternal, TCallbackInfo> (ref callbackInfoInternal, out callbackInfo, out var clientDataAddress);
		callback = null;
		lock (s_Callbacks) {
			if (s_Callbacks.TryGetValue (clientDataAddress, out var value)) {
				callback = value.StructDelegates.FirstOrDefault ((Delegate structDelegate) => structDelegate.GetType () == typeof(TCallback)) as TCallback;
				if (callback != null) {
					return true;
				}
			}
		}
		return false;
	}

	internal static void RemoveCallbackByNotificationId (ulong notificationId)
	{
		lock (s_Callbacks) {
			RemoveCallback (s_Callbacks.SingleOrDefault ((KeyValuePair<IntPtr, DelegateHolder> pair) => pair.Value.NotificationId.HasValue && pair.Value.NotificationId == notificationId).Key);
		}
	}

	internal static void AddStaticCallback (string key, Delegate publicDelegate, Delegate privateDelegate)
	{
		lock (s_StaticCallbacks) {
			s_StaticCallbacks.Remove (key);
			s_StaticCallbacks.Add (key, new DelegateHolder (publicDelegate, privateDelegate));
		}
	}

	internal static bool TryGetStaticCallback<TCallback> (string key, out TCallback callback) where TCallback : class
	{
		callback = null;
		lock (s_StaticCallbacks) {
			if (s_StaticCallbacks.TryGetValue (key, out var value)) {
				callback = value.Public as TCallback;
				if (callback != null) {
					return true;
				}
			}
		}
		return false;
	}

	internal static void AssignNotificationIdToCallback (IntPtr clientDataAddress, ulong notificationId)
	{
		if (notificationId == 0L) {
			RemoveCallback (clientDataAddress);
			return;
		}
		lock (s_Callbacks) {
			if (s_Callbacks.TryGetValue (clientDataAddress, out var value)) {
				value.NotificationId = notificationId;
			}
		}
	}

	private static IntPtr AddClientData (object clientData)
	{
		lock (s_ClientDatas) {
			IntPtr intPtr = new IntPtr (++s_LastClientDataId);
			s_ClientDatas.Add (intPtr, clientData);
			return intPtr;
		}
	}

	private static void RemoveClientData (IntPtr clientDataAddress)
	{
		lock (s_ClientDatas) {
			s_ClientDatas.Remove (clientDataAddress);
		}
	}

	private static object GetClientData (IntPtr clientDataAddress)
	{
		lock (s_ClientDatas) {
			s_ClientDatas.TryGetValue (clientDataAddress, out var value);
			return value;
		}
	}

	private static void Convert<THandle> (IntPtr from, out THandle to) where THandle : Handle, new()
	{
		to = null;
		if (from != IntPtr.Zero) {
			to = new THandle ();
			to.InnerHandle = from;
		}
	}

	private static void Convert (Handle from, out IntPtr to)
	{
		to = IntPtr.Zero;
		if (from != null) {
			to = from.InnerHandle;
		}
	}

	private static void Convert (byte[] from, out string to)
	{
		to = null;
		if (from != null) {
			to = Encoding.ASCII.GetString (from.Take (GetAnsiStringLength (from)).ToArray ());
		}
	}

	private static void Convert (string from, out byte[] to, int fromLength)
	{
		if (from == null) {
			from = "";
		}
		to = Encoding.ASCII.GetBytes (new string (from.Take (fromLength).ToArray ()).PadRight (fromLength, '\0'));
	}

	private static void Convert<TArray> (TArray[] from, out int to)
	{
		to = 0;
		if (from != null) {
			to = from.Length;
		}
	}

	private static void Convert<TArray> (TArray[] from, out uint to)
	{
		to = 0u;
		if (from != null) {
			to = (uint)from.Length;
		}
	}

	private static void Convert<TArray> (ArraySegment<TArray> from, out int to)
	{
		to = from.Count;
	}

	private static void Convert<T> (ArraySegment<T> from, out uint to)
	{
		to = (uint)from.Count;
	}

	private static void Convert (int from, out bool to)
	{
		to = from != 0;
	}

	private static void Convert (bool from, out int to)
	{
		to = (from ? 1 : 0);
	}

	private static void Convert (DateTimeOffset? from, out long to)
	{
		to = -1L;
		if (from.HasValue) {
			DateTime dateTime = new DateTime (1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
			long num = (from.Value.UtcDateTime - dateTime).Ticks / 10000000;
			to = num;
		}
	}

	private static void Convert (long from, out DateTimeOffset? to)
	{
		to = null;
		if (from >= 0) {
			DateTime dateTime = new DateTime (1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
			long num = from * 10000000;
			to = new DateTimeOffset (dateTime.Ticks + num, TimeSpan.Zero);
		}
	}

	internal static void Get<TArray> (TArray[] from, out int to)
	{
		Convert (from, out to);
	}

	internal static void Get<TArray> (TArray[] from, out uint to)
	{
		Convert (from, out to);
	}

	internal static void Get<TArray> (ArraySegment<TArray> from, out uint to)
	{
		Convert (from, out to);
	}

	internal static void Get<TTo> (IntPtr from, out TTo to) where TTo : Handle, new()
	{
		Convert<TTo> (from, out to);
	}

	internal static void Get<TFrom, TTo> (ref TFrom from, out TTo to) where TFrom : struct, IGettable<TTo> where TTo : struct
	{
		from.Get (out to);
	}

	internal static void Get (int from, out bool to)
	{
		Convert (from, out to);
	}

	internal static void Get (bool from, out int to)
	{
		Convert (from, out to);
	}

	internal static void Get (long from, out DateTimeOffset? to)
	{
		Convert (from, out to);
	}

	internal static void Get<TTo> (IntPtr from, out TTo[] to, int arrayLength, bool isArrayItemAllocated)
	{
		GetAllocation<TTo> (from, out to, arrayLength, isArrayItemAllocated);
	}

	internal static void Get<TTo> (IntPtr from, out TTo[] to, uint arrayLength, bool isArrayItemAllocated)
	{
		GetAllocation<TTo> (from, out to, (int)arrayLength, isArrayItemAllocated);
	}

	internal static void Get<TTo> (IntPtr from, out TTo[] to, int arrayLength)
	{
		GetAllocation<TTo> (from, out to, arrayLength, !typeof(TTo).IsValueType);
	}

	internal static void Get<TTo> (IntPtr from, out TTo[] to, uint arrayLength)
	{
		GetAllocation<TTo> (from, out to, (int)arrayLength, !typeof(TTo).IsValueType);
	}

	internal static void Get (IntPtr from, out ArraySegment<byte> to, uint arrayLength)
	{
		to = default(ArraySegment<byte>);
		if (arrayLength != 0) {
			byte[] array = new byte[arrayLength];
			Marshal.Copy (from, array, 0, (int)arrayLength);
			to = new ArraySegment<byte> (array);
		}
	}

	internal static void GetHandle<THandle> (IntPtr from, out THandle[] to, uint arrayLength) where THandle : Handle, new()
	{
		GetAllocation<THandle> (from, out to, (int)arrayLength);
	}

	internal static void Get<TFrom, TTo> (TFrom[] from, out TTo[] to) where TFrom : struct, IGettable<TTo> where TTo : struct
	{
		to = GetDefault<TTo[]> ();
		if (from != null) {
			to = new TTo[from.Length];
			for (int i = 0; i < from.Length; i++) {
				from [i].Get (out to [i]);
			}
		}
	}

	internal static void Get<TFrom, TTo> (IntPtr from, out TTo[] to, int arrayLength) where TFrom : struct, IGettable<TTo> where TTo : struct
	{
		Get (from, out TFrom[] to2, arrayLength);
		Get (to2, out to);
	}

	internal static void Get<TFrom, TTo> (IntPtr from, out TTo[] to, uint arrayLength) where TFrom : struct, IGettable<TTo> where TTo : struct
	{
		Get<TFrom, TTo> (from, out to, (int)arrayLength);
	}

	internal static void Get<TTo> (IntPtr from, out TTo? to) where TTo : struct
	{
		GetAllocation (from, out to);
	}

	internal static void Get (byte[] from, out string to)
	{
		Convert (from, out to);
	}

	internal static void Get (IntPtr from, out object to)
	{
		to = GetClientData (from);
	}

	internal static void Get (IntPtr from, out Utf8String to)
	{
		GetAllocation (from, out to);
	}

	internal static void Get<T, TEnum> (T from, out T to, TEnum currentEnum, TEnum expectedEnum)
	{
		to = GetDefault<T> ();
		if ((int)(object)currentEnum == (int)(object)expectedEnum) {
			to = from;
		}
	}

	internal static void Get<TFrom, TTo, TEnum> (ref TFrom from, out TTo to, TEnum currentEnum, TEnum expectedEnum) where TFrom : struct, IGettable<TTo> where TTo : struct
	{
		to = GetDefault<TTo> ();
		if ((int)(object)currentEnum == (int)(object)expectedEnum) {
			Get<TFrom, TTo> (ref from, out to);
		}
	}

	internal static void Get<TEnum> (int from, out bool? to, TEnum currentEnum, TEnum expectedEnum)
	{
		to = GetDefault<bool?> ();
		if ((int)(object)currentEnum == (int)(object)expectedEnum) {
			Convert (from, out var to2);
			to = to2;
		}
	}

	internal static void Get<TFrom, TEnum> (TFrom from, out TFrom? to, TEnum currentEnum, TEnum expectedEnum) where TFrom : struct
	{
		to = GetDefault<TFrom?> ();
		if ((int)(object)currentEnum == (int)(object)expectedEnum) {
			to = from;
		}
	}

	internal static void Get<TFrom, TEnum> (IntPtr from, out TFrom to, TEnum currentEnum, TEnum expectedEnum) where TFrom : Handle, new()
	{
		to = GetDefault<TFrom> ();
		if ((int)(object)currentEnum == (int)(object)expectedEnum) {
			Get (from, out to);
		}
	}

	internal static void Get<TEnum> (IntPtr from, out IntPtr? to, TEnum currentEnum, TEnum expectedEnum)
	{
		to = GetDefault<IntPtr?> ();
		if ((int)(object)currentEnum == (int)(object)expectedEnum) {
			Get (from, out to);
		}
	}

	internal static void Get<TEnum> (IntPtr from, out Utf8String to, TEnum currentEnum, TEnum expectedEnum)
	{
		to = GetDefault<Utf8String> ();
		if ((int)(object)currentEnum == (int)(object)expectedEnum) {
			Get (from, out to);
		}
	}

	internal static void Get<TFrom, TTo> (IntPtr from, out TTo to) where TFrom : struct, IGettable<TTo> where TTo : struct
	{
		to = GetDefault<TTo> ();
		Get (from, out TFrom? to2);
		if (to2.HasValue) {
			to2.Value.Get (out to);
		}
	}

	internal static void Get<TFrom, TTo> (IntPtr from, out TTo? to) where TFrom : struct, IGettable<TTo> where TTo : struct
	{
		to = GetDefault<TTo?> ();
		Get (from, out TFrom? to2);
		if (to2.HasValue) {
			to2.Value.Get (out var other);
			to = other;
		}
	}

	internal static void Get<TFrom, TTo> (ref TFrom from, out TTo to, out IntPtr clientDataAddress) where TFrom : struct, ICallbackInfoInternal, IGettable<TTo> where TTo : struct
	{
		from.Get (out to);
		clientDataAddress = from.ClientDataAddress;
	}

	public static int GetAllocationCount ()
	{
		return s_Allocations.Count + s_PinnedBuffers.Aggregate (0, (int acc, KeyValuePair<long, PinnedBuffer> x) => acc + x.Value.RefCount) + s_Callbacks.Count + s_ClientDatas.Count;
	}

	internal static void Copy (byte[] from, IntPtr to)
	{
		if (from != null && to != IntPtr.Zero) {
			Marshal.Copy (from, 0, to, from.Length);
		}
	}

	internal static void Copy (ArraySegment<byte> from, IntPtr to)
	{
		if (from.Count != 0 && to != IntPtr.Zero) {
			Marshal.Copy (from.Array, from.Offset, to, from.Count);
		}
	}

	internal static void Dispose (ref IntPtr value)
	{
		RemoveAllocation (ref value);
		RemovePinnedBuffer (ref value);
	}

	internal static void Dispose<TDisposable> (ref TDisposable disposable) where TDisposable : IDisposable
	{
		if (typeof(TDisposable).IsValueType || disposable != null) {
			disposable.Dispose ();
		}
	}

	internal static void Dispose<TEnum> (ref IntPtr value, TEnum currentEnum, TEnum expectedEnum)
	{
		if ((int)(object)currentEnum == (int)(object)expectedEnum) {
			Dispose (ref value);
		}
	}

	private static int GetAnsiStringLength (byte[] bytes)
	{
		int num = 0;
		for (int i = 0; i < bytes.Length && bytes [i] != 0; i++) {
			num++;
		}
		return num;
	}

	private static int GetAnsiStringLength (IntPtr address)
	{
		int i;
		for (i = 0; Marshal.ReadByte (address, i) != 0; i++) {
		}
		return i;
	}

	internal static T GetDefault<T> ()
	{
		return default(T);
	}

	private static void GetAllocation<T> (IntPtr source, out T target)
	{
		target = GetDefault<T> ();
		if (source == IntPtr.Zero) {
			return;
		}
		if (TryGetAllocationCache (source, out var cache) && cache != null) {
			if (!(cache.GetType () == typeof(T))) {
				throw new CachedTypeAllocationException (source, cache.GetType (), typeof(T));
			}
			target = (T)cache;
		} else {
			target = (T)Marshal.PtrToStructure (source, typeof(T));
		}
	}

	private static void GetAllocation<T> (IntPtr source, out T? target) where T : struct
	{
		target = GetDefault<T?> ();
		if (source == IntPtr.Zero) {
			return;
		}
		if (TryGetAllocationCache (source, out var cache) && cache != null) {
			if (!(cache.GetType () == typeof(T))) {
				throw new CachedTypeAllocationException (source, cache.GetType (), typeof(T));
			}
			target = (T?)cache;
		} else {
			target = (T?)Marshal.PtrToStructure (source, typeof(T));
		}
	}

	private static void GetAllocation<THandle> (IntPtr source, out THandle[] target, int arrayLength) where THandle : Handle, new()
	{
		target = null;
		if (source == IntPtr.Zero) {
			return;
		}
		if (TryGetAllocationCache (source, out var cache) && cache != null) {
			if (!(cache.GetType () == typeof(THandle[]))) {
				throw new CachedTypeAllocationException (source, cache.GetType (), typeof(THandle[]));
			}
			Array array = (Array)cache;
			if (array.Length != arrayLength) {
				throw new CachedArrayAllocationException (source, array.Length, arrayLength);
			}
			target = array as THandle[];
		} else {
			int num = Marshal.SizeOf (typeof(IntPtr));
			List<THandle> list = new List<THandle> ();
			for (int i = 0; i < arrayLength; i++) {
				Convert<THandle> (Marshal.ReadIntPtr (new IntPtr (source.ToInt64 () + i * num)), out var to);
				list.Add (to);
			}
			target = list.ToArray ();
		}
	}

	private static void GetAllocation<T> (IntPtr from, out T[] to, int arrayLength, bool isArrayItemAllocated)
	{
		to = null;
		if (from == IntPtr.Zero) {
			return;
		}
		if (TryGetAllocationCache (from, out var cache) && cache != null) {
			if (cache.GetType () == typeof(T[])) {
				Array array = (Array)cache;
				if (array.Length == arrayLength) {
					to = array as T[];
					return;
				}
				throw new CachedArrayAllocationException (from, array.Length, arrayLength);
			}
			throw new CachedTypeAllocationException (from, cache.GetType (), typeof(T[]));
		}
		int num = ((!isArrayItemAllocated) ? Marshal.SizeOf (typeof(T)) : Marshal.SizeOf (typeof(IntPtr)));
		List<T> list = new List<T> ();
		for (int i = 0; i < arrayLength; i++) {
			IntPtr intPtr = new IntPtr (from.ToInt64 () + i * num);
			if (isArrayItemAllocated) {
				intPtr = Marshal.ReadIntPtr (intPtr);
			}
			GetAllocation (intPtr, out T target);
			list.Add (target);
		}
		to = list.ToArray ();
	}

	private static void GetAllocation (IntPtr source, out Utf8String target)
	{
		target = null;
		if (!(source == IntPtr.Zero)) {
			int ansiStringLength = GetAnsiStringLength (source);
			byte[] array = new byte[ansiStringLength + 1];
			Marshal.Copy (source, array, 0, ansiStringLength + 1);
			target = new Utf8String (array);
		}
	}

	internal static IntPtr AddAllocation (int size)
	{
		if (size == 0) {
			return IntPtr.Zero;
		}
		IntPtr intPtr = Marshal.AllocHGlobal (size);
		Marshal.WriteByte (intPtr, 0, 0);
		lock (s_Allocations) {
			s_Allocations.Add ((long)intPtr, new Allocation (size, null));
			return intPtr;
		}
	}

	internal static IntPtr AddAllocation (uint size)
	{
		return AddAllocation ((int)size);
	}

	private static IntPtr AddAllocation<T> (int size, T cache)
	{
		if (size == 0 || cache == null) {
			return IntPtr.Zero;
		}
		IntPtr intPtr = Marshal.AllocHGlobal (size);
		Marshal.StructureToPtr (cache, intPtr, fDeleteOld: false);
		lock (s_Allocations) {
			s_Allocations.Add ((long)intPtr, new Allocation (size, cache));
			return intPtr;
		}
	}

	private static IntPtr AddAllocation<T> (int size, T[] cache, bool? isArrayItemAllocated)
	{
		if (size == 0 || cache == null) {
			return IntPtr.Zero;
		}
		IntPtr intPtr = Marshal.AllocHGlobal (size);
		Marshal.WriteByte (intPtr, 0, 0);
		lock (s_Allocations) {
			s_Allocations.Add ((long)intPtr, new Allocation (size, cache, isArrayItemAllocated));
			return intPtr;
		}
	}

	private static IntPtr AddAllocation<T> (T[] array, bool isArrayItemAllocated)
	{
		if (array == null) {
			return IntPtr.Zero;
		}
		int num = ((!isArrayItemAllocated) ? Marshal.SizeOf (typeof(T)) : Marshal.SizeOf (typeof(IntPtr)));
		IntPtr result = AddAllocation (array.Length * num, array, isArrayItemAllocated);
		for (int i = 0; i < array.Length; i++) {
			T val = (T)array.GetValue (i);
			if (isArrayItemAllocated) {
				IntPtr to;
				if (typeof(T) == typeof(Utf8String)) {
					to = AddPinnedBuffer ((Utf8String)(object)val);
				} else if (typeof(T).BaseType == typeof(Handle)) {
					Convert ((Handle)(object)val, out to);
				} else {
					to = AddAllocation (Marshal.SizeOf (typeof(T)), val);
				}
				Marshal.StructureToPtr (ptr: new IntPtr (result.ToInt64 () + i * num), structure: to, fDeleteOld: false);
			} else {
				IntPtr ptr2 = new IntPtr (result.ToInt64 () + i * num);
				Marshal.StructureToPtr (val, ptr2, fDeleteOld: false);
			}
		}
		return result;
	}

	private static void RemoveAllocation (ref IntPtr address)
	{
		if (address == IntPtr.Zero) {
			return;
		}
		Allocation value;
		lock (s_Allocations) {
			if (!s_Allocations.TryGetValue ((long)address, out value)) {
				return;
			}
			s_Allocations.Remove ((long)address);
		}
		if (value.IsArrayItemAllocated.HasValue) {
			int num = ((!value.IsArrayItemAllocated.Value) ? Marshal.SizeOf (value.Cache.GetType ().GetElementType ()) : Marshal.SizeOf (typeof(IntPtr)));
			Array array = value.Cache as Array;
			for (int i = 0; i < array.Length; i++) {
				if (value.IsArrayItemAllocated.Value) {
					IntPtr ptr = new IntPtr (address.ToInt64 () + i * num);
					ptr = Marshal.ReadIntPtr (ptr);
					Dispose (ref ptr);
					continue;
				}
				object value2 = array.GetValue (i);
				if (value2 is IDisposable && value2 is IDisposable disposable) {
					disposable.Dispose ();
				}
			}
		}
		if (value.Cache is IDisposable && value.Cache is IDisposable disposable2) {
			disposable2.Dispose ();
		}
		Marshal.FreeHGlobal (address);
		address = IntPtr.Zero;
	}

	private static bool TryGetAllocationCache (IntPtr address, out object cache)
	{
		cache = null;
		lock (s_Allocations) {
			if (s_Allocations.TryGetValue ((long)address, out var value)) {
				cache = value.Cache;
				return true;
			}
		}
		return false;
	}

	private static IntPtr AddPinnedBuffer (byte[] buffer, int offset)
	{
		if (buffer == null) {
			return IntPtr.Zero;
		}
		GCHandle handle = GCHandle.Alloc (buffer, GCHandleType.Pinned);
		long num = (long)Marshal.UnsafeAddrOfPinnedArrayElement (buffer, offset);
		lock (s_PinnedBuffers) {
			if (s_PinnedBuffers.ContainsKey (num)) {
				PinnedBuffer value = s_PinnedBuffers [num];
				value.RefCount++;
				s_PinnedBuffers [num] = value;
			} else {
				s_PinnedBuffers.Add (num, new PinnedBuffer (handle));
			}
			return (IntPtr)num;
		}
	}

	private static IntPtr AddPinnedBuffer (Utf8String str)
	{
		if (str == null || str.Bytes == null) {
			return IntPtr.Zero;
		}
		return AddPinnedBuffer (str.Bytes, 0);
	}

	internal static IntPtr AddPinnedBuffer (ArraySegment<byte> array)
	{
		return AddPinnedBuffer (array.Array, array.Offset);
	}

	private static void RemovePinnedBuffer (ref IntPtr address)
	{
		if (address == IntPtr.Zero) {
			return;
		}
		long key = (long)address;
		lock (s_PinnedBuffers) {
			if (s_PinnedBuffers.TryGetValue (key, out var value)) {
				value.RefCount--;
				if (value.RefCount == 0) {
					s_PinnedBuffers.Remove (key);
					value.Handle.Free ();
				} else {
					s_PinnedBuffers [key] = value;
				}
			}
		}
		address = IntPtr.Zero;
	}

	internal static void Set<T> (ref T from, ref T to) where T : struct
	{
		to = from;
	}

	internal static void Set (object from, ref IntPtr to)
	{
		RemoveClientData (to);
		to = AddClientData (from);
	}

	internal static void Set (Utf8String from, ref IntPtr to)
	{
		Dispose (ref to);
		to = AddPinnedBuffer (from);
	}

	internal static void Set (Handle from, ref IntPtr to)
	{
		Convert (from, out to);
	}

	internal static void Set<T> (T? from, ref IntPtr to) where T : struct
	{
		Dispose (ref to);
		to = AddAllocation (Marshal.SizeOf (typeof(T)), from);
	}

	internal static void Set<T> (T[] from, ref IntPtr to, bool isArrayItemAllocated)
	{
		Dispose (ref to);
		to = AddAllocation (from, isArrayItemAllocated);
	}

	internal static void Set (ArraySegment<byte> from, ref IntPtr to, out uint arrayLength)
	{
		to = AddPinnedBuffer (from);
		Get (from, out arrayLength);
	}

	internal static void Set<T> (T[] from, ref IntPtr to)
	{
		Set (from, ref to, !typeof(T).IsValueType);
	}

	internal static void Set<T> (T[] from, ref IntPtr to, bool isArrayItemAllocated, out int arrayLength)
	{
		Set (from, ref to, isArrayItemAllocated);
		Get (from, out arrayLength);
	}

	internal static void Set<T> (T[] from, ref IntPtr to, bool isArrayItemAllocated, out uint arrayLength)
	{
		Set (from, ref to, isArrayItemAllocated);
		Get (from, out arrayLength);
	}

	internal static void Set<T> (T[] from, ref IntPtr to, out int arrayLength)
	{
		Set (from, ref to, !typeof(T).IsValueType, out arrayLength);
	}

	internal static void Set<T> (T[] from, ref IntPtr to, out uint arrayLength)
	{
		Set (from, ref to, !typeof(T).IsValueType, out arrayLength);
	}

	internal static void Set (DateTimeOffset? from, ref long to)
	{
		Convert (from, out to);
	}

	internal static void Set (bool from, ref int to)
	{
		Convert (from, out to);
	}

	internal static void Set (string from, ref byte[] to, int stringLength)
	{
		Convert (from, out to, stringLength);
	}

	internal static void Set<T, TEnum> (T from, ref T to, TEnum fromEnum, ref TEnum toEnum, IDisposable disposable = null)
	{
		if (from != null) {
			Dispose (ref disposable);
			to = from;
			toEnum = fromEnum;
		}
	}

	internal static void Set<TFrom, TEnum, TTo> (ref TFrom from, ref TTo to, TEnum fromEnum, ref TEnum toEnum, IDisposable disposable = null) where TFrom : struct where TTo : struct, ISettable<TFrom>
	{
		Dispose (ref disposable);
		Set (ref from, ref to);
		toEnum = fromEnum;
	}

	internal static void Set<T, TEnum> (T? from, ref T to, TEnum fromEnum, ref TEnum toEnum, IDisposable disposable = null) where T : struct
	{
		if (from.HasValue) {
			Dispose (ref disposable);
			T from2 = from.Value;
			Helper.Set<T> (ref from2, ref to);
			toEnum = fromEnum;
		}
	}

	internal static void Set<TEnum> (Handle from, ref IntPtr to, TEnum fromEnum, ref TEnum toEnum, IDisposable disposable = null)
	{
		if (from != null) {
			Dispose (ref to);
			Dispose (ref disposable);
			Set (from, ref to);
			toEnum = fromEnum;
		}
	}

	internal static void Set<TEnum> (Utf8String from, ref IntPtr to, TEnum fromEnum, ref TEnum toEnum, IDisposable disposable = null)
	{
		if (from != null) {
			Dispose (ref to);
			Dispose (ref disposable);
			Set (from, ref to);
			toEnum = fromEnum;
		}
	}

	internal static void Set<TEnum> (bool? from, ref int to, TEnum fromEnum, ref TEnum toEnum, IDisposable disposable = null)
	{
		if (from.HasValue) {
			Dispose (ref disposable);
			Set (from.Value, ref to);
			toEnum = fromEnum;
		}
	}

	internal static void Set<TFrom, TIntermediate> (ref TFrom from, ref IntPtr to) where TFrom : struct where TIntermediate : struct, ISettable<TFrom>
	{
		TIntermediate cache = new TIntermediate ();
		cache.Set (ref from);
		Dispose (ref to);
		to = AddAllocation (Marshal.SizeOf (typeof(TIntermediate)), cache);
	}

	internal static void Set<TFrom, TIntermediate> (ref TFrom? from, ref IntPtr to) where TFrom : struct where TIntermediate : struct, ISettable<TFrom>
	{
		Dispose (ref to);
		if (from.HasValue) {
			TIntermediate cache = default(TIntermediate);
			TFrom other = from.Value;
			cache.Set (ref other);
			to = AddAllocation (Marshal.SizeOf (typeof(TIntermediate)), cache);
		}
	}

	internal static void Set<TFrom, TTo> (ref TFrom from, ref TTo to) where TFrom : struct where TTo : struct, ISettable<TFrom>
	{
		to.Set (ref from);
	}

	internal static void Set<TFrom, TIntermediate> (ref TFrom[] from, ref IntPtr to, out int arrayLength) where TFrom : struct where TIntermediate : struct, ISettable<TFrom>
	{
		arrayLength = 0;
		if (from != null) {
			TIntermediate[] array = new TIntermediate[from.Length];
			for (int i = 0; i < from.Length; i++) {
				array [i].Set (ref from [i]);
			}
			Set (array, ref to);
			Get (from, out arrayLength);
		}
	}

	internal static void Set<TFrom, TIntermediate> (ref TFrom[] from, ref IntPtr to, out uint arrayLength) where TFrom : struct where TIntermediate : struct, ISettable<TFrom>
	{
		Set<TFrom, TIntermediate> (ref from, ref to, out int arrayLength2);
		arrayLength = (uint)arrayLength2;
	}
}
