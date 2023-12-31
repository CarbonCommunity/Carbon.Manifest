using UnityEngine;

public static class KeywordUtil
{
	public static void EnsureKeywordState (string keyword, bool state)
	{
		bool flag = Shader.IsKeywordEnabled (keyword);
		if (state && !flag) {
			Shader.EnableKeyword (keyword);
		} else if (!state && flag) {
			Shader.DisableKeyword (keyword);
		}
	}

	public static void EnsureKeywordState (Material mat, string keyword, bool state)
	{
		if (mat != null) {
			bool flag = mat.IsKeywordEnabled (keyword);
			if (state && !flag) {
				mat.EnableKeyword (keyword);
			} else if (!state && flag) {
				mat.DisableKeyword (keyword);
			}
		}
	}
}
