using System.Collections.Generic;
using Facepunch;

public class BaseResourceExtractor : BaseCombatEntity
{
	public bool canExtractLiquid = false;

	public bool canExtractSolid = true;

	public override void ServerInit ()
	{
		base.ServerInit ();
		if (base.isClient) {
			return;
		}
		List<SurveyCrater> obj = Pool.GetList<SurveyCrater> ();
		Vis.Entities (base.transform.position, 3f, obj, 1);
		foreach (SurveyCrater item in obj) {
			if (item.isServer) {
				item.Kill ();
			}
		}
		Pool.FreeList (ref obj);
	}
}
