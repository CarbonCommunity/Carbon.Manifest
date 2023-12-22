using System;
using System.IO;
using Facepunch;
using UnityEngine;

public class RandomUsernames
{
	[Serializable]
	private class DataFile
	{
		public string[] RandomUsernames;
	}

	private static string[] All = null;

	public static string Get (int v)
	{
		if (All == null) {
			string path = Path.Combine (UnityEngine.Application.streamingAssetsPath, "RandomUsernames.json");
			string json = File.ReadAllText (path);
			DataFile dataFile = JsonUtility.FromJson<DataFile> (json);
			All = dataFile.RandomUsernames;
		}
		if (v < 0) {
			v *= -1;
		}
		v %= All.Length;
		return All [v];
	}

	public static string Get (ulong v)
	{
		return Get ((int)(v % int.MaxValue));
	}
}
