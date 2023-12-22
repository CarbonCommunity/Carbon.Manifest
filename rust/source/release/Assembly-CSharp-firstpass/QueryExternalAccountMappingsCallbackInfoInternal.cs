using System;
using System.Runtime.InteropServices;
using Epic.OnlineServices;
using Epic.OnlineServices.Connect;

[StructLayout (LayoutKind.Sequential, Pack = 8)]
internal struct QueryExternalAccountMappingsCallbackInfoInternal : ICallbackInfoInternal, IGettable<QueryExternalAccountMappingsCallbackInfo>, ISettable<QueryExternalAccountMappingsCallbackInfo>, IDisposable
{
	private Result m_ResultCode;

	private IntPtr m_ClientData;

	private IntPtr m_LocalUserId;

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

	public ProductUserId LocalUserId {
		get {
			Helper.Get (m_LocalUserId, out ProductUserId to);
			return to;
		}
		set {
			Helper.Set (value, ref m_LocalUserId);
		}
	}

	public void Set (ref QueryExternalAccountMappingsCallbackInfo other)
	{
		ResultCode = other.ResultCode;
		ClientData = other.ClientData;
		LocalUserId = other.LocalUserId;
	}

	public void Set (ref QueryExternalAccountMappingsCallbackInfo? other)
	{
		if (other.HasValue) {
			ResultCode = other.Value.ResultCode;
			ClientData = other.Value.ClientData;
			LocalUserId = other.Value.LocalUserId;
		}
	}

	public void Dispose ()
	{
		Helper.Dispose (ref m_ClientData);
		Helper.Dispose (ref m_LocalUserId);
	}

	public void Get (out QueryExternalAccountMappingsCallbackInfo output)
	{
		output = default(QueryExternalAccountMappingsCallbackInfo);
		output.Set (ref this);
	}
}
