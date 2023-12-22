using System.Collections.Generic;
using UnityEngine;

public class PowerLineWireSpan : MonoBehaviour
{
	public GameObjectRef wirePrefab;

	public Transform start;

	public Transform end;

	public float WireLength;

	public List<PowerLineWireConnection> connections = new List<PowerLineWireConnection> ();

	public void Init (PowerLineWire wire)
	{
		if ((bool)start && (bool)end) {
			WireLength = Vector3.Distance (start.position, end.position);
			for (int i = 0; i < connections.Count; i++) {
				Vector3 vector = start.TransformPoint (connections [i].outOffset);
				Vector3 vector2 = end.TransformPoint (connections [i].inOffset);
				WireLength = (vector - vector2).magnitude;
				GameObject gameObject = wirePrefab.Instantiate (base.transform);
				gameObject.name = "WIRE";
				gameObject.transform.position = Vector3.Lerp (vector, vector2, 0.5f);
				gameObject.transform.LookAt (vector2);
				gameObject.transform.localScale = new Vector3 (1f, 1f, Vector3.Distance (vector, vector2));
				gameObject.SetActive (value: true);
			}
		}
	}
}
