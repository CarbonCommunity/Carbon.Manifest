using System;
using System.Collections.Generic;
using Facepunch;
using Rust;
using UnityEngine;

public class MissionPoint : MonoBehaviour
{
	public enum MissionPointEnum
	{
		EasyMonument = 1,
		MediumMonument = 2,
		HardMonument = 4,
		Item_Hidespot = 8,
		Underwater = 0x80
	}

	public bool dropToGround = true;

	public const int COUNT = 8;

	public const int EVERYTHING = -1;

	public const int NOTHING = 0;

	public const int EASY_MONUMENT = 1;

	public const int MED_MONUMENT = 2;

	public const int HARD_MONUMENT = 4;

	public const int ITEM_HIDESPOT = 8;

	public const int UNDERWATER = 128;

	public const int EASY_MONUMENT_IDX = 0;

	public const int MED_MONUMENT_IDX = 1;

	public const int HARD_MONUMENT_IDX = 2;

	public const int ITEM_HIDESPOT_IDX = 3;

	public const int FOREST_IDX = 4;

	public const int ROADSIDE_IDX = 5;

	public const int BEACH = 6;

	public const int UNDERWATER_IDX = 7;

	private static Dictionary<int, int> type2index = new Dictionary<int, int> {
		{ 1, 0 },
		{ 2, 1 },
		{ 4, 2 },
		{ 8, 3 },
		{ 128, 7 }
	};

	public static List<MissionPoint> all = new List<MissionPoint> ();

	[InspectorFlags]
	public MissionPointEnum Flags = (MissionPointEnum)(-1);

	public static int TypeToIndex (int id)
	{
		return type2index [id];
	}

	public static int IndexToType (int idx)
	{
		return 1 << idx;
	}

	public void Awake ()
	{
		all.Add (this);
	}

	private void Start ()
	{
		if (dropToGround) {
			((FacepunchBehaviour)SingletonComponent<InvokeHandler>.Instance).Invoke ((Action)DropToGround, 0.5f);
		}
	}

	private void DropToGround ()
	{
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		if (Application.isLoading) {
			((FacepunchBehaviour)SingletonComponent<InvokeHandler>.Instance).Invoke ((Action)DropToGround, 0.5f);
			return;
		}
		Vector3 position = ((Component)this).transform.position;
		((Component)this).transform.DropToGround ();
	}

	public void OnDisable ()
	{
		if (all.Contains (this)) {
			all.Remove (this);
		}
	}

	public virtual Vector3 GetPosition ()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		return ((Component)this).transform.position;
	}

	public virtual Quaternion GetRotation ()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		return ((Component)this).transform.rotation;
	}

	public static bool GetMissionPoints (ref List<MissionPoint> points, Vector3 near, float minDistance, float maxDistance, int flags, int exclusionFlags)
	{
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		List<MissionPoint> list = Pool.GetList<MissionPoint> ();
		foreach (MissionPoint item in all) {
			if (((uint)item.Flags & (uint)flags) != (uint)flags || (exclusionFlags != 0 && ((uint)item.Flags & (uint)exclusionFlags) != 0)) {
				continue;
			}
			float num = Vector3.Distance (((Component)item).transform.position, near);
			if (!(num <= maxDistance) || !(num > minDistance)) {
				continue;
			}
			if (BaseMission.blockedPoints.Count > 0) {
				bool flag = false;
				foreach (Vector3 blockedPoint in BaseMission.blockedPoints) {
					if (Vector3.Distance (blockedPoint, ((Component)item).transform.position) < 5f) {
						flag = true;
						break;
					}
				}
				if (flag) {
					continue;
				}
			}
			list.Add (item);
		}
		if (list.Count == 0) {
			return false;
		}
		foreach (MissionPoint item2 in list) {
			points.Add (item2);
		}
		Pool.FreeList<MissionPoint> (ref list);
		return true;
	}
}
