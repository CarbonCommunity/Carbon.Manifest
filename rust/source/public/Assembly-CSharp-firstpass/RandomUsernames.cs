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

	private static string[] All;

	public static string Get (int v)
	{
		if (All == null) {
			All = JsonUtility.FromJson<DataFile> (File.ReadAllText (Path.Combine (UnityEngine.Application.streamingAssetsPath, "RandomUsernames.json"))).RandomUsernames;
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
