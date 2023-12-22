using CompanionServer.Handlers;
using Facepunch;
using ProtoBuf;

public class EntityInfo : BaseEntityHandler<AppEmpty>
{
	public override void Execute ()
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		AppEntityInfo val = Pool.Get<AppEntityInfo> ();
		val.type = base.Entity.Type;
		val.payload = Pool.Get<AppEntityPayload> ();
		base.Entity.FillEntityPayload (val.payload);
		AppResponse val2 = Pool.Get<AppResponse> ();
		val2.entityInfo = val;
		Send (val2);
	}
}
