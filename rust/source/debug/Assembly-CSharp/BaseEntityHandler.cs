using CompanionServer;
using CompanionServer.Handlers;
using UnityEngine;

public abstract class BaseEntityHandler<T> : BaseHandler<T> where T : class
{
	protected AppIOEntity Entity { get; private set; }

	public override void EnterPool ()
	{
		base.EnterPool ();
		Entity = null;
	}

	public override ValidationResult Validate ()
	{
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		ValidationResult validationResult = base.Validate ();
		if (validationResult != 0) {
			return validationResult;
		}
		AppIOEntity appIOEntity = BaseNetworkable.serverEntities.Find (base.Request.entityId) as AppIOEntity;
		if ((Object)(object)appIOEntity == (Object)null) {
			return ValidationResult.NotFound;
		}
		BuildingPrivlidge buildingPrivilege = appIOEntity.GetBuildingPrivilege ();
		if ((Object)(object)buildingPrivilege != (Object)null && !buildingPrivilege.IsAuthed (base.UserId)) {
			return ValidationResult.NotFound;
		}
		Entity = appIOEntity;
		base.Client.Subscribe (new EntityTarget (base.Request.entityId));
		return ValidationResult.Success;
	}
}
