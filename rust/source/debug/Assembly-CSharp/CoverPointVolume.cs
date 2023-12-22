#define ENABLE_PROFILER
using System.Collections.Generic;
using Rust.Ai;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Profiling;

public class CoverPointVolume : MonoBehaviour, IServerComponent
{
	internal enum CoverType
	{
		None,
		Partial,
		Full
	}

	public float DefaultCoverPointScore = 1f;

	public float CoverPointRayLength = 1f;

	public LayerMask CoverLayerMask;

	public Transform BlockerGroup;

	public Transform ManualCoverPointGroup;

	[ServerVar (Help = "cover_point_sample_step_size defines the size of the steps we do horizontally for the cover point volume's cover point generation (smaller steps gives more accurate cover points, but at a higher processing cost). (default: 6.0)")]
	public static float cover_point_sample_step_size = 6f;

	[ServerVar (Help = "cover_point_sample_step_height defines the height of the steps we do vertically for the cover point volume's cover point generation (smaller steps gives more accurate cover points, but at a higher processing cost). (default: 2.0)")]
	public static float cover_point_sample_step_height = 2f;

	public readonly List<CoverPoint> CoverPoints = new List<CoverPoint> ();

	private readonly List<CoverPointBlockerVolume> _coverPointBlockers = new List<CoverPointBlockerVolume> ();

	private float _dynNavMeshBuildCompletionTime = -1f;

	private int _genAttempts = 0;

	private Bounds bounds = new Bounds (Vector3.zero, Vector3.zero);

	public bool repeat => true;

	public float? ExecuteUpdate (float deltaTime, float nextInterval)
	{
		if (CoverPoints.Count == 0) {
			if (_dynNavMeshBuildCompletionTime < 0f) {
				if (SingletonComponent<DynamicNavMesh>.Instance == null || !SingletonComponent<DynamicNavMesh>.Instance.enabled || !SingletonComponent<DynamicNavMesh>.Instance.IsBuilding) {
					_dynNavMeshBuildCompletionTime = Time.realtimeSinceStartup;
				}
			} else if (_genAttempts < 4 && Time.realtimeSinceStartup - _dynNavMeshBuildCompletionTime > 0.25f) {
				GenerateCoverPoints (null);
				if (CoverPoints.Count != 0) {
					return null;
				}
				_dynNavMeshBuildCompletionTime = Time.realtimeSinceStartup;
				_genAttempts++;
				if (_genAttempts >= 4) {
					Object.Destroy (base.gameObject);
					return null;
				}
			}
		}
		return 1f + Random.value * 2f;
	}

	[ContextMenu ("Clear Cover Points")]
	private void ClearCoverPoints ()
	{
		CoverPoints.Clear ();
		_coverPointBlockers.Clear ();
	}

	public Bounds GetBounds ()
	{
		if (Mathf.Approximately (bounds.center.sqrMagnitude, 0f)) {
			bounds = new Bounds (base.transform.position, base.transform.localScale);
		}
		return bounds;
	}

	[ContextMenu ("Pre-Generate Cover Points")]
	public void PreGenerateCoverPoints ()
	{
		GenerateCoverPoints (null);
	}

	[ContextMenu ("Convert to Manual Cover Points")]
	public void ConvertToManualCoverPoints ()
	{
		foreach (CoverPoint coverPoint in CoverPoints) {
			ManualCoverPoint manualCoverPoint = new GameObject ("MCP").AddComponent<ManualCoverPoint> ();
			manualCoverPoint.transform.localPosition = Vector3.zero;
			manualCoverPoint.transform.position = coverPoint.Position;
			manualCoverPoint.Normal = coverPoint.Normal;
			manualCoverPoint.NormalCoverType = coverPoint.NormalCoverType;
			manualCoverPoint.Volume = this;
		}
	}

	public void GenerateCoverPoints (Transform coverPointGroup)
	{
		Profiler.BeginSample ("CoverPointVolume.GenerateCoverPoints");
		float realtimeSinceStartup = Time.realtimeSinceStartup;
		ClearCoverPoints ();
		if (ManualCoverPointGroup == null) {
			ManualCoverPointGroup = coverPointGroup;
		}
		if (ManualCoverPointGroup == null) {
			ManualCoverPointGroup = base.transform;
		}
		if (ManualCoverPointGroup.childCount > 0) {
			ManualCoverPoint[] componentsInChildren = ManualCoverPointGroup.GetComponentsInChildren<ManualCoverPoint> ();
			ManualCoverPoint[] array = componentsInChildren;
			foreach (ManualCoverPoint manualCoverPoint in array) {
				CoverPoint item = manualCoverPoint.ToCoverPoint (this);
				CoverPoints.Add (item);
			}
		}
		if (_coverPointBlockers.Count == 0 && BlockerGroup != null) {
			CoverPointBlockerVolume[] componentsInChildren2 = BlockerGroup.GetComponentsInChildren<CoverPointBlockerVolume> ();
			if (componentsInChildren2 != null && componentsInChildren2.Length != 0) {
				_coverPointBlockers.AddRange (componentsInChildren2);
			}
		}
		if (CoverPoints.Count != 0) {
			return;
		}
		Profiler.BeginSample ("CoverPointVolume.SamplePosition");
		NavMeshHit hit;
		bool flag = NavMesh.SamplePosition (base.transform.position, out hit, base.transform.localScale.y * cover_point_sample_step_height, -1);
		Profiler.EndSample ();
		if (flag) {
			Vector3 position = base.transform.position;
			Vector3 vector = base.transform.lossyScale * 0.5f;
			for (float num = position.x - vector.x + 1f; num < position.x + vector.x - 1f; num += cover_point_sample_step_size) {
				for (float num2 = position.z - vector.z + 1f; num2 < position.z + vector.z - 1f; num2 += cover_point_sample_step_size) {
					for (float num3 = position.y - vector.y; num3 < position.y + vector.y; num3 += cover_point_sample_step_height) {
						Vector3 sourcePosition = new Vector3 (num, num3, num2);
						Profiler.BeginSample ("CoverPointVolume.FindClosestEdge");
						NavMeshHit hit2;
						bool flag2 = NavMesh.FindClosestEdge (sourcePosition, out hit2, hit.mask);
						Profiler.EndSample ();
						if (!flag2) {
							continue;
						}
						hit2.position = new Vector3 (hit2.position.x, hit2.position.y + 0.5f, hit2.position.z);
						bool flag3 = true;
						foreach (CoverPoint coverPoint2 in CoverPoints) {
							if ((coverPoint2.Position - hit2.position).sqrMagnitude < cover_point_sample_step_size * cover_point_sample_step_size) {
								flag3 = false;
								break;
							}
						}
						if (flag3) {
							Profiler.BeginSample ("CoverPointVolume.CalculateCoverPoint");
							CoverPoint coverPoint = CalculateCoverPoint (hit2);
							Profiler.EndSample ();
							if (coverPoint != null) {
								CoverPoints.Add (coverPoint);
							}
						}
					}
				}
			}
		}
		Profiler.EndSample ();
	}

	private CoverPoint CalculateCoverPoint (NavMeshHit info)
	{
		RaycastHit rayHit;
		CoverType coverType = ProvidesCoverInDir (new Ray (info.position, -info.normal), CoverPointRayLength, out rayHit);
		if (coverType == CoverType.None) {
			return null;
		}
		Profiler.BeginSample ("CoverPointVolume.InstantiateCoverPoint");
		CoverPoint coverPoint = new CoverPoint (this, DefaultCoverPointScore) {
			Position = info.position,
			Normal = -info.normal
		};
		Profiler.EndSample ();
		switch (coverType) {
		case CoverType.Full:
			coverPoint.NormalCoverType = CoverPoint.CoverType.Full;
			break;
		case CoverType.Partial:
			coverPoint.NormalCoverType = CoverPoint.CoverType.Partial;
			break;
		}
		return coverPoint;
	}

	internal CoverType ProvidesCoverInDir (Ray ray, float maxDistance, out RaycastHit rayHit)
	{
		Profiler.BeginSample ("CoverPointVolume.ProvideCoverInDir");
		rayHit = default(RaycastHit);
		if (ray.origin.IsNaNOrInfinity ()) {
			Profiler.EndSample ();
			return CoverType.None;
		}
		if (ray.direction.IsNaNOrInfinity ()) {
			Profiler.EndSample ();
			return CoverType.None;
		}
		if (ray.direction == Vector3.zero) {
			Profiler.EndSample ();
			return CoverType.None;
		}
		ray.origin += PlayerEyes.EyeOffset;
		if (Physics.Raycast (ray.origin, ray.direction, out rayHit, maxDistance, CoverLayerMask)) {
			Profiler.EndSample ();
			return CoverType.Full;
		}
		ray.origin += PlayerEyes.DuckOffset;
		if (Physics.Raycast (ray.origin, ray.direction, out rayHit, maxDistance, CoverLayerMask)) {
			Profiler.EndSample ();
			return CoverType.Partial;
		}
		Profiler.EndSample ();
		return CoverType.None;
	}

	public bool Contains (Vector3 point)
	{
		return new Bounds (base.transform.position, base.transform.localScale).Contains (point);
	}
}
