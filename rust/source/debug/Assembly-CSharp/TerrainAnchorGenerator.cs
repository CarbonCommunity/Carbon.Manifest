using UnityEngine;

public class TerrainAnchorGenerator : MonoBehaviour, IEditorComponent
{
	public float PlacementRadius = 32f;

	public float PlacementPadding = 0f;

	public float PlacementFade = 16f;

	public float PlacementDistance = 8f;

	public float AnchorExtentsMin = 8f;

	public float AnchorExtentsMax = 16f;

	public float AnchorOffsetMin = 0f;

	public float AnchorOffsetMax = 0f;
}
