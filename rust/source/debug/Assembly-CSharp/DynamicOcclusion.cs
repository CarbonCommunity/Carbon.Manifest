#define UNITY_ASSERTIONS
using System;
using UnityEngine;
using VLB;

[ExecuteInEditMode]
[DisallowMultipleComponent]
[RequireComponent (typeof(VolumetricLightBeam))]
[HelpURL ("http://saladgamer.com/vlb-doc/comp-dynocclusion/")]
public class DynamicOcclusion : MonoBehaviour
{
	private enum Direction
	{
		Up,
		Right,
		Down,
		Left
	}

	public LayerMask layerMask = -1;

	public float minOccluderArea = 0f;

	public int waitFrameCount = 3;

	public float minSurfaceRatio = 0.5f;

	public float maxSurfaceDot = 0.25f;

	public PlaneAlignment planeAlignment = PlaneAlignment.Surface;

	public float planeOffset = 0.1f;

	private VolumetricLightBeam m_Master = null;

	private int m_FrameCountToWait = 0;

	private float m_RangeMultiplier = 1f;

	private uint m_PrevNonSubHitDirectionId = 0u;

	private void OnValidate ()
	{
		minOccluderArea = Mathf.Max (minOccluderArea, 0f);
		waitFrameCount = Mathf.Clamp (waitFrameCount, 1, 60);
	}

	private void OnEnable ()
	{
		m_Master = GetComponent<VolumetricLightBeam> ();
		Debug.Assert (m_Master);
	}

	private void OnDisable ()
	{
		SetHitNull ();
	}

	private void Start ()
	{
		if (Application.isPlaying) {
			TriggerZone component = GetComponent<TriggerZone> ();
			if ((bool)component) {
				m_RangeMultiplier = Mathf.Max (1f, component.rangeMultiplier);
			}
		}
	}

	private void LateUpdate ()
	{
		if (m_FrameCountToWait <= 0) {
			ProcessRaycasts ();
			m_FrameCountToWait = waitFrameCount;
		}
		m_FrameCountToWait--;
	}

	private Vector3 GetRandomVectorAround (Vector3 direction, float angleDiff)
	{
		float num = angleDiff * 0.5f;
		return Quaternion.Euler (UnityEngine.Random.Range (0f - num, num), UnityEngine.Random.Range (0f - num, num), UnityEngine.Random.Range (0f - num, num)) * direction;
	}

	private RaycastHit GetBestHit (Vector3 rayPos, Vector3 rayDir)
	{
		RaycastHit[] array = Physics.RaycastAll (rayPos, rayDir, m_Master.fadeEnd * m_RangeMultiplier, layerMask.value);
		int num = -1;
		float num2 = float.MaxValue;
		for (int i = 0; i < array.Length; i++) {
			if (!array [i].collider.isTrigger && array [i].collider.bounds.GetMaxArea2D () >= minOccluderArea && array [i].distance < num2) {
				num2 = array [i].distance;
				num = i;
			}
		}
		if (num != -1) {
			return array [num];
		}
		return default(RaycastHit);
	}

	private Vector3 GetDirection (uint dirInt)
	{
		dirInt %= (uint)Enum.GetValues (typeof(Direction)).Length;
		return dirInt switch {
			0u => base.transform.up, 
			1u => base.transform.right, 
			2u => -base.transform.up, 
			3u => -base.transform.right, 
			_ => Vector3.zero, 
		};
	}

	private bool IsHitValid (RaycastHit hit)
	{
		if ((bool)hit.collider) {
			float num = Vector3.Dot (hit.normal, -base.transform.forward);
			return num >= maxSurfaceDot;
		}
		return false;
	}

	private void ProcessRaycasts ()
	{
		RaycastHit hit = GetBestHit (base.transform.position, base.transform.forward);
		if (IsHitValid (hit)) {
			if (minSurfaceRatio > 0.5f) {
				for (uint num = 0u; num < (uint)Enum.GetValues (typeof(Direction)).Length; num++) {
					Vector3 direction = GetDirection (num + m_PrevNonSubHitDirectionId);
					Vector3 vector = base.transform.position + direction * m_Master.coneRadiusStart * (minSurfaceRatio * 2f - 1f);
					Vector3 vector2 = base.transform.position + base.transform.forward * m_Master.fadeEnd + direction * m_Master.coneRadiusEnd * (minSurfaceRatio * 2f - 1f);
					RaycastHit bestHit = GetBestHit (vector, vector2 - vector);
					if (IsHitValid (bestHit)) {
						if (bestHit.distance > hit.distance) {
							hit = bestHit;
						}
						continue;
					}
					m_PrevNonSubHitDirectionId = num;
					SetHitNull ();
					return;
				}
			}
			SetHit (hit);
		} else {
			SetHitNull ();
		}
	}

	private void SetHit (RaycastHit hit)
	{
		PlaneAlignment planeAlignment = this.planeAlignment;
		if (planeAlignment != 0 && planeAlignment == PlaneAlignment.Beam) {
			SetClippingPlane (new Plane (-base.transform.forward, hit.point));
		} else {
			SetClippingPlane (new Plane (hit.normal, hit.point));
		}
	}

	private void SetHitNull ()
	{
		SetClippingPlaneOff ();
	}

	private void SetClippingPlane (Plane planeWS)
	{
		planeWS = planeWS.TranslateCustom (planeWS.normal * planeOffset);
		m_Master.SetClippingPlane (planeWS);
	}

	private void SetClippingPlaneOff ()
	{
		m_Master.SetClippingPlaneOff ();
	}
}
