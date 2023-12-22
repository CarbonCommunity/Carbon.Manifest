using UnityEngine;

public class ParticleDisableOnParentDestroy : MonoBehaviour, IOnParentDestroying
{
	public float destroyAfterSeconds = 0f;

	public void OnParentDestroying ()
	{
		base.transform.parent = null;
		GetComponent<ParticleSystem> ().enableEmission = false;
		if (destroyAfterSeconds > 0f) {
			GameManager.Destroy (base.gameObject, destroyAfterSeconds);
		}
	}
}
