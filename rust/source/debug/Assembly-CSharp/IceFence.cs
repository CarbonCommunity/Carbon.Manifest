using UnityEngine;

public class IceFence : GraveyardFence
{
	public GameObject[] styles;

	private bool init = false;

	public AdaptMeshToTerrain snowMesh;

	public int GetStyleFromID ()
	{
		uint seed = (uint)net.ID.Value;
		return SeedRandom.Range (ref seed, 0, styles.Length);
	}

	public override void ServerInit ()
	{
		base.ServerInit ();
		InitStyle ();
		UpdatePillars ();
	}

	public void InitStyle ()
	{
		if (!init) {
			SetStyle (GetStyleFromID ());
		}
	}

	public void SetStyle (int style)
	{
		GameObject[] array = styles;
		foreach (GameObject gameObject in array) {
			gameObject.gameObject.SetActive (value: false);
		}
		styles [style].gameObject.SetActive (value: true);
	}

	public override void UpdatePillars ()
	{
		base.UpdatePillars ();
	}
}
