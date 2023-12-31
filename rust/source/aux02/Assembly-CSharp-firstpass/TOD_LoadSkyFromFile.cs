using UnityEngine;

public class TOD_LoadSkyFromFile : MonoBehaviour
{
	public TOD_Sky sky;

	public TextAsset textAsset;

	protected void Start ()
	{
		if (!sky) {
			sky = TOD_Sky.Instance;
		}
		if ((bool)textAsset) {
			sky.LoadParameters (textAsset.text);
		}
	}
}
