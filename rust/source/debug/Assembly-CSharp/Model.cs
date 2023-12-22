#define ENABLE_PROFILER
using Facepunch;
using UnityEngine;
using UnityEngine.Profiling;

public class Model : MonoBehaviour, IPrefabPreProcess
{
	public SphereCollider collision;

	public Transform rootBone;

	public Transform headBone;

	public Transform eyeBone;

	public Animator animator;

	public Skeleton skeleton;

	[HideInInspector]
	public Transform[] boneTransforms = null;

	[HideInInspector]
	public string[] boneNames = null;

	internal BoneDictionary boneDict = null;

	internal int skin;

	protected void OnEnable ()
	{
		skin = -1;
	}

	public void BuildBoneDictionary ()
	{
		if (boneDict == null) {
			boneDict = new BoneDictionary (base.transform, boneTransforms, boneNames);
		}
	}

	public int GetSkin ()
	{
		return skin;
	}

	private Transform FindBoneInternal (string name)
	{
		BuildBoneDictionary ();
		Profiler.BeginSample ("Model.FindBoneInternal");
		Transform result = boneDict.FindBone (name, defaultToRoot: false);
		Profiler.EndSample ();
		return result;
	}

	public Transform FindBone (string name)
	{
		BuildBoneDictionary ();
		Transform result = rootBone;
		if (string.IsNullOrEmpty (name)) {
			return result;
		}
		Profiler.BeginSample ("Model.FindBone");
		result = boneDict.FindBone (name);
		Profiler.EndSample ();
		return result;
	}

	public Transform FindBone (uint hash)
	{
		BuildBoneDictionary ();
		Transform result = rootBone;
		if (hash == 0) {
			return result;
		}
		Profiler.BeginSample ("Model.FindBone");
		result = boneDict.FindBone (hash);
		Profiler.EndSample ();
		return result;
	}

	public uint FindBoneID (Transform transform)
	{
		BuildBoneDictionary ();
		Profiler.BeginSample ("Model.FindBoneID");
		uint result = boneDict.FindBoneID (transform);
		Profiler.EndSample ();
		return result;
	}

	public Transform[] GetBones ()
	{
		BuildBoneDictionary ();
		return boneDict.transforms;
	}

	public Transform FindClosestBone (Vector3 worldPos)
	{
		Transform result = rootBone;
		float num = float.MaxValue;
		Profiler.BeginSample ("Model.FindClosestBone");
		for (int i = 0; i < boneTransforms.Length; i++) {
			Transform transform = boneTransforms [i];
			if (!(transform == null)) {
				float num2 = Vector3.Distance (transform.position, worldPos);
				if (!(num2 >= num)) {
					result = transform;
					num = num2;
				}
			}
		}
		Profiler.EndSample ();
		return result;
	}

	public void PreProcess (IPrefabProcessor process, GameObject rootObj, string name, bool serverside, bool clientside, bool bundling)
	{
		if (!(this == null)) {
			if (animator == null) {
				animator = GetComponent<Animator> ();
			}
			if (rootBone == null) {
				rootBone = base.transform;
			}
			boneTransforms = rootBone.GetComponentsInChildren<Transform> (includeInactive: true);
			boneNames = new string[boneTransforms.Length];
			for (int i = 0; i < boneTransforms.Length; i++) {
				boneNames [i] = boneTransforms [i].name;
			}
		}
	}
}
