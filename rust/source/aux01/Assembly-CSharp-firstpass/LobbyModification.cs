using System;
using Epic.OnlineServices;
using Epic.OnlineServices.Lobby;

public sealed class LobbyModification : Handle
{
	public const int LobbymodificationAddattributeApiLatest = 1;

	public const int LobbymodificationAddmemberattributeApiLatest = 1;

	public const int LobbymodificationMaxAttributeLength = 64;

	public const int LobbymodificationMaxAttributes = 64;

	public const int LobbymodificationRemoveattributeApiLatest = 1;

	public const int LobbymodificationRemovememberattributeApiLatest = 1;

	public const int LobbymodificationSetbucketidApiLatest = 1;

	public const int LobbymodificationSetinvitesallowedApiLatest = 1;

	public const int LobbymodificationSetmaxmembersApiLatest = 1;

	public const int LobbymodificationSetpermissionlevelApiLatest = 1;

	public LobbyModification ()
	{
	}

	public LobbyModification (IntPtr innerHandle)
		: base (innerHandle)
	{
	}

	public Result AddAttribute (ref LobbyModificationAddAttributeOptions options)
	{
		LobbyModificationAddAttributeOptionsInternal options2 = default(LobbyModificationAddAttributeOptionsInternal);
		options2.Set (ref options);
		Result result = Bindings.EOS_LobbyModification_AddAttribute (base.InnerHandle, ref options2);
		Helper.Dispose (ref options2);
		return result;
	}

	public Result AddMemberAttribute (ref LobbyModificationAddMemberAttributeOptions options)
	{
		LobbyModificationAddMemberAttributeOptionsInternal options2 = default(LobbyModificationAddMemberAttributeOptionsInternal);
		options2.Set (ref options);
		Result result = Bindings.EOS_LobbyModification_AddMemberAttribute (base.InnerHandle, ref options2);
		Helper.Dispose (ref options2);
		return result;
	}

	public void Release ()
	{
		Bindings.EOS_LobbyModification_Release (base.InnerHandle);
	}

	public Result RemoveAttribute (ref LobbyModificationRemoveAttributeOptions options)
	{
		LobbyModificationRemoveAttributeOptionsInternal options2 = default(LobbyModificationRemoveAttributeOptionsInternal);
		options2.Set (ref options);
		Result result = Bindings.EOS_LobbyModification_RemoveAttribute (base.InnerHandle, ref options2);
		Helper.Dispose (ref options2);
		return result;
	}

	public Result RemoveMemberAttribute (ref LobbyModificationRemoveMemberAttributeOptions options)
	{
		LobbyModificationRemoveMemberAttributeOptionsInternal options2 = default(LobbyModificationRemoveMemberAttributeOptionsInternal);
		options2.Set (ref options);
		Result result = Bindings.EOS_LobbyModification_RemoveMemberAttribute (base.InnerHandle, ref options2);
		Helper.Dispose (ref options2);
		return result;
	}

	public Result SetBucketId (ref LobbyModificationSetBucketIdOptions options)
	{
		LobbyModificationSetBucketIdOptionsInternal options2 = default(LobbyModificationSetBucketIdOptionsInternal);
		options2.Set (ref options);
		Result result = Bindings.EOS_LobbyModification_SetBucketId (base.InnerHandle, ref options2);
		Helper.Dispose (ref options2);
		return result;
	}

	public Result SetInvitesAllowed (ref LobbyModificationSetInvitesAllowedOptions options)
	{
		LobbyModificationSetInvitesAllowedOptionsInternal options2 = default(LobbyModificationSetInvitesAllowedOptionsInternal);
		options2.Set (ref options);
		Result result = Bindings.EOS_LobbyModification_SetInvitesAllowed (base.InnerHandle, ref options2);
		Helper.Dispose (ref options2);
		return result;
	}

	public Result SetMaxMembers (ref LobbyModificationSetMaxMembersOptions options)
	{
		LobbyModificationSetMaxMembersOptionsInternal options2 = default(LobbyModificationSetMaxMembersOptionsInternal);
		options2.Set (ref options);
		Result result = Bindings.EOS_LobbyModification_SetMaxMembers (base.InnerHandle, ref options2);
		Helper.Dispose (ref options2);
		return result;
	}

	public Result SetPermissionLevel (ref LobbyModificationSetPermissionLevelOptions options)
	{
		LobbyModificationSetPermissionLevelOptionsInternal options2 = default(LobbyModificationSetPermissionLevelOptionsInternal);
		options2.Set (ref options);
		Result result = Bindings.EOS_LobbyModification_SetPermissionLevel (base.InnerHandle, ref options2);
		Helper.Dispose (ref options2);
		return result;
	}
}
