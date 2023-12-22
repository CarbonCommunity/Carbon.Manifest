using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public sealed class PropertySheetFactory
{
	private readonly Dictionary<Shader, PropertySheet> m_Sheets;

	public PropertySheetFactory ()
	{
		m_Sheets = new Dictionary<Shader, PropertySheet> ();
	}

	[Obsolete ("Use PropertySheet.Get(Shader) with a direct reference to the Shader instead.")]
	public PropertySheet Get (string shaderName)
	{
		Shader val = Shader.Find (shaderName);
		if ((Object)(object)val == (Object)null) {
			throw new ArgumentException ($"Invalid shader ({shaderName})");
		}
		return Get (val);
	}

	public PropertySheet Get (Shader shader)
	{
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Expected O, but got Unknown
		if ((Object)(object)shader == (Object)null) {
			throw new ArgumentException ($"Invalid shader ({shader})");
		}
		if (m_Sheets.TryGetValue (shader, out var value)) {
			return value;
		}
		string name = ((Object)shader).name;
		value = new PropertySheet (new Material (shader) {
			name = $"PostProcess - {name.Substring (name.LastIndexOf ('/') + 1)}",
			hideFlags = (HideFlags)52
		});
		m_Sheets.Add (shader, value);
		return value;
	}

	public void Release ()
	{
		foreach (PropertySheet value in m_Sheets.Values) {
			value.Release ();
		}
		m_Sheets.Clear ();
	}
}
