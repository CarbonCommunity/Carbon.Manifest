using System.Collections.Generic;
using Facepunch;
using UnityEngine;

public class SurveyCharge : TimedExplosive
{
	public GameObjectRef craterPrefab;

	public GameObjectRef craterPrefab_Oil;

	public override void Explode ()
	{
		base.Explode ();
		if (WaterLevel.Test (base.transform.position, waves: true, volumes: true, this)) {
			return;
		}
		ResourceDepositManager.ResourceDeposit orCreate = ResourceDepositManager.GetOrCreate (base.transform.position);
		if (orCreate == null || Time.realtimeSinceStartup - orCreate.lastSurveyTime < 10f) {
			return;
		}
		orCreate.lastSurveyTime = Time.realtimeSinceStartup;
		if (!TransformUtil.GetGroundInfo (base.transform.position, out var hitOut, 0.3f, 8388608)) {
			return;
		}
		Vector3 point = hitOut.point;
		Vector3 normal = hitOut.normal;
		List<SurveyCrater> obj = Pool.GetList<SurveyCrater> ();
		Vis.Entities (base.transform.position, 10f, obj, 1);
		bool flag = obj.Count > 0;
		Pool.FreeList (ref obj);
		if (flag) {
			return;
		}
		bool flag2 = false;
		bool flag3 = false;
		foreach (ResourceDepositManager.ResourceDeposit.ResourceDepositEntry resource in orCreate._resources) {
			if (resource.spawnType == ResourceDepositManager.ResourceDeposit.surveySpawnType.ITEM && !resource.isLiquid && resource.amount >= 1000) {
				int num = Mathf.Clamp (Mathf.CeilToInt (2.5f / resource.workNeeded * 10f), 0, 5);
				int iAmount = 1;
				flag2 = true;
				if (resource.isLiquid) {
					flag3 = true;
				}
				for (int i = 0; i < num; i++) {
					Item item = ItemManager.Create (resource.type, iAmount, 0uL);
					float aimCone = 20f;
					Vector3 modifiedAimConeDirection = AimConeUtil.GetModifiedAimConeDirection (aimCone, Vector3.up);
					BaseEntity baseEntity = item.Drop (base.transform.position + Vector3.up * 1f, GetInheritedDropVelocity () + modifiedAimConeDirection * Random.Range (5f, 10f), Random.rotation);
					baseEntity.SetAngularVelocity (Random.rotation.eulerAngles * 5f);
				}
			}
		}
		if (flag2) {
			string strPrefab = (flag3 ? craterPrefab_Oil.resourcePath : craterPrefab.resourcePath);
			BaseEntity baseEntity2 = GameManager.server.CreateEntity (strPrefab, point, Quaternion.identity);
			if ((bool)baseEntity2) {
				baseEntity2.Spawn ();
			}
		}
	}
}
