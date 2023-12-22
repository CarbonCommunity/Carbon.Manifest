using UnityEngine;

[ExecuteInEditMode]
public class ParticleScaler : MonoBehaviour
{
	public float particleScale = 1f;

	public bool alsoScaleGameobject = true;

	private void Start ()
	{
	}

	private void Update ()
	{
	}

	private void ScaleShurikenSystems (float scaleFactor)
	{
	}

	private void ScaleLegacySystems (float scaleFactor)
	{
	}

	private void ScaleTrailRenderers (float scaleFactor)
	{
		TrailRenderer[] componentsInChildren = GetComponentsInChildren<TrailRenderer> ();
		TrailRenderer[] array = componentsInChildren;
		foreach (TrailRenderer trailRenderer in array) {
			trailRenderer.startWidth *= scaleFactor;
			trailRenderer.endWidth *= scaleFactor;
		}
	}
}
