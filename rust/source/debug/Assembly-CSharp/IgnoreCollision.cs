using UnityEngine;

public class IgnoreCollision : MonoBehaviour
{
	public Collider collider = null;

	protected void OnTriggerEnter (Collider other)
	{
		Debug.Log ("IgnoreCollision: " + collider.gameObject.name + " + " + other.gameObject.name);
		Physics.IgnoreCollision (other, collider, ignore: true);
	}
}
