using System;
using Epic.OnlineServices;
using Epic.OnlineServices.Sessions;

public sealed class SessionModification : Handle
{
	public const int SessionmodificationAddattributeApiLatest = 1;

	public const int SessionmodificationMaxSessionAttributeLength = 64;

	public const int SessionmodificationMaxSessionAttributes = 64;

	public const int SessionmodificationMaxSessionidoverrideLength = 64;

	public const int SessionmodificationMinSessionidoverrideLength = 16;

	public const int SessionmodificationRemoveattributeApiLatest = 1;

	public const int SessionmodificationSetbucketidApiLatest = 1;

	public const int SessionmodificationSethostaddressApiLatest = 1;

	public const int SessionmodificationSetinvitesallowedApiLatest = 1;

	public const int SessionmodificationSetjoininprogressallowedApiLatest = 1;

	public const int SessionmodificationSetmaxplayersApiLatest = 1;

	public const int SessionmodificationSetpermissionlevelApiLatest = 1;

	public SessionModification ()
	{
	}

	public SessionModification (IntPtr innerHandle)
		: base (innerHandle)
	{
	}

	public Result AddAttribute (ref SessionModificationAddAttributeOptions options)
	{
		SessionModificationAddAttributeOptionsInternal options2 = default(SessionModificationAddAttributeOptionsInternal);
		options2.Set (ref options);
		Result result = Bindings.EOS_SessionModification_AddAttribute (base.InnerHandle, ref options2);
		Helper.Dispose (ref options2);
		return result;
	}

	public void Release ()
	{
		Bindings.EOS_SessionModification_Release (base.InnerHandle);
	}

	public Result RemoveAttribute (ref SessionModificationRemoveAttributeOptions options)
	{
		SessionModificationRemoveAttributeOptionsInternal options2 = default(SessionModificationRemoveAttributeOptionsInternal);
		options2.Set (ref options);
		Result result = Bindings.EOS_SessionModification_RemoveAttribute (base.InnerHandle, ref options2);
		Helper.Dispose (ref options2);
		return result;
	}

	public Result SetBucketId (ref SessionModificationSetBucketIdOptions options)
	{
		SessionModificationSetBucketIdOptionsInternal options2 = default(SessionModificationSetBucketIdOptionsInternal);
		options2.Set (ref options);
		Result result = Bindings.EOS_SessionModification_SetBucketId (base.InnerHandle, ref options2);
		Helper.Dispose (ref options2);
		return result;
	}

	public Result SetHostAddress (ref SessionModificationSetHostAddressOptions options)
	{
		SessionModificationSetHostAddressOptionsInternal options2 = default(SessionModificationSetHostAddressOptionsInternal);
		options2.Set (ref options);
		Result result = Bindings.EOS_SessionModification_SetHostAddress (base.InnerHandle, ref options2);
		Helper.Dispose (ref options2);
		return result;
	}

	public Result SetInvitesAllowed (ref SessionModificationSetInvitesAllowedOptions options)
	{
		SessionModificationSetInvitesAllowedOptionsInternal options2 = default(SessionModificationSetInvitesAllowedOptionsInternal);
		options2.Set (ref options);
		Result result = Bindings.EOS_SessionModification_SetInvitesAllowed (base.InnerHandle, ref options2);
		Helper.Dispose (ref options2);
		return result;
	}

	public Result SetJoinInProgressAllowed (ref SessionModificationSetJoinInProgressAllowedOptions options)
	{
		SessionModificationSetJoinInProgressAllowedOptionsInternal options2 = default(SessionModificationSetJoinInProgressAllowedOptionsInternal);
		options2.Set (ref options);
		Result result = Bindings.EOS_SessionModification_SetJoinInProgressAllowed (base.InnerHandle, ref options2);
		Helper.Dispose (ref options2);
		return result;
	}

	public Result SetMaxPlayers (ref SessionModificationSetMaxPlayersOptions options)
	{
		SessionModificationSetMaxPlayersOptionsInternal options2 = default(SessionModificationSetMaxPlayersOptionsInternal);
		options2.Set (ref options);
		Result result = Bindings.EOS_SessionModification_SetMaxPlayers (base.InnerHandle, ref options2);
		Helper.Dispose (ref options2);
		return result;
	}

	public Result SetPermissionLevel (ref SessionModificationSetPermissionLevelOptions options)
	{
		SessionModificationSetPermissionLevelOptionsInternal options2 = default(SessionModificationSetPermissionLevelOptionsInternal);
		options2.Set (ref options);
		Result result = Bindings.EOS_SessionModification_SetPermissionLevel (base.InnerHandle, ref options2);
		Helper.Dispose (ref options2);
		return result;
	}
}
