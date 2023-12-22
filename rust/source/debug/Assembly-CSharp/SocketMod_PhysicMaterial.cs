using UnityEngine;

public class SocketMod_PhysicMaterial : SocketMod
{
	public PhysicMaterial[] ValidMaterials;

	private PhysicMaterial foundMaterial = null;

	public override bool DoCheck (Construction.Placement place)
	{
		if (Physics.Raycast (place.position + place.rotation.eulerAngles.normalized * 0.5f, -place.rotation.eulerAngles.normalized, out var hitInfo, 1f, 161546240, QueryTriggerInteraction.Ignore)) {
			foundMaterial = hitInfo.collider.GetMaterialAt (hitInfo.point);
			PhysicMaterial[] validMaterials = ValidMaterials;
			foreach (PhysicMaterial physicMaterial in validMaterials) {
				if (physicMaterial == foundMaterial) {
					return true;
				}
			}
		}
		return false;
	}
}
