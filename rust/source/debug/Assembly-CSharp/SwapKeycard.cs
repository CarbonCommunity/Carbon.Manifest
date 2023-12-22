using UnityEngine;

public class SwapKeycard : MonoBehaviour
{
	public GameObject[] accessLevels;

	public void UpdateAccessLevel (int level)
	{
		GameObject[] array = accessLevels;
		foreach (GameObject gameObject in array) {
			gameObject.SetActive (value: false);
		}
		accessLevels [level - 1].SetActive (value: true);
	}

	public void SetRootActive (int index)
	{
		for (int i = 0; i < accessLevels.Length; i++) {
			accessLevels [i].SetActive (i == index);
		}
	}
}
