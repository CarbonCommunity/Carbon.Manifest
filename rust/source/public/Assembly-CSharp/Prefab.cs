using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Prefab : IComparable<Prefab>
{
	public uint ID;

	public string Name;

	public string Folder;

	public GameObject Object;

	public GameManager Manager;

	public PrefabAttribute.Library Attribute;

	public PrefabParameters Parameters;

	public static PrefabAttribute.Library DefaultAttribute => PrefabAttribute.server;

	public static GameManager DefaultManager => GameManager.server;

	public Prefab (string name, GameObject prefab, GameManager manager, PrefabAttribute.Library attribute)
	{
		ID = StringPool.Get (name);
		Name = name;
		Folder = (string.IsNullOrWhiteSpace (name) ? "" : Path.GetDirectoryName (name));
		Object = prefab;
		Manager = manager;
		Attribute = attribute;
		Parameters = (prefab ? prefab.GetComponent<PrefabParameters> () : null);
	}

	public static implicit operator GameObject (Prefab prefab)
	{
		return prefab.Object;
	}

	public int CompareTo (Prefab that)
	{
		if (that == null) {
			return 1;
		}
		PrefabPriority prefabPriority = ((Parameters != null) ? Parameters.Priority : PrefabPriority.Default);
		return ((that.Parameters != null) ? that.Parameters.Priority : PrefabPriority.Default).CompareTo (prefabPriority);
	}

	public bool ApplyTerrainAnchors (ref Vector3 pos, Quaternion rot, Vector3 scale, TerrainAnchorMode mode, SpawnFilter filter = null)
	{
		TerrainAnchor[] anchors = Attribute.FindAll<TerrainAnchor> (ID);
		return Object.transform.ApplyTerrainAnchors (anchors, ref pos, rot, scale, mode, filter);
	}

	public bool ApplyTerrainAnchors (ref Vector3 pos, Quaternion rot, Vector3 scale, SpawnFilter filter = null)
	{
		TerrainAnchor[] anchors = Attribute.FindAll<TerrainAnchor> (ID);
		return Object.transform.ApplyTerrainAnchors (anchors, ref pos, rot, scale, filter);
	}

	public bool ApplyTerrainChecks (Vector3 pos, Quaternion rot, Vector3 scale, SpawnFilter filter = null)
	{
		TerrainCheck[] anchors = Attribute.FindAll<TerrainCheck> (ID);
		return Object.transform.ApplyTerrainChecks (anchors, pos, rot, scale, filter);
	}

	public bool ApplyTerrainFilters (Vector3 pos, Quaternion rot, Vector3 scale, SpawnFilter filter = null)
	{
		TerrainFilter[] filters = Attribute.FindAll<TerrainFilter> (ID);
		return Object.transform.ApplyTerrainFilters (filters, pos, rot, scale, filter);
	}

	public void ApplyTerrainModifiers (Vector3 pos, Quaternion rot, Vector3 scale)
	{
		TerrainModifier[] modifiers = Attribute.FindAll<TerrainModifier> (ID);
		Object.transform.ApplyTerrainModifiers (modifiers, pos, rot, scale);
	}

	public void ApplyTerrainPlacements (Vector3 pos, Quaternion rot, Vector3 scale)
	{
		TerrainPlacement[] placements = Attribute.FindAll<TerrainPlacement> (ID);
		Object.transform.ApplyTerrainPlacements (placements, pos, rot, scale);
	}

	public bool ApplyWaterChecks (Vector3 pos, Quaternion rot, Vector3 scale)
	{
		WaterCheck[] anchors = Attribute.FindAll<WaterCheck> (ID);
		return Object.transform.ApplyWaterChecks (anchors, pos, rot, scale);
	}

	public bool ApplyBoundsChecks (Vector3 pos, Quaternion rot, Vector3 scale, LayerMask rejectOnLayer)
	{
		BoundsCheck[] bounds = Attribute.FindAll<BoundsCheck> (ID);
		BaseEntity component = Object.GetComponent<BaseEntity> ();
		if (component != null) {
			return component.ApplyBoundsChecks (bounds, pos, rot, scale, rejectOnLayer);
		}
		return true;
	}

	public void ApplyDecorComponents (ref Vector3 pos, ref Quaternion rot, ref Vector3 scale)
	{
		DecorComponent[] components = Attribute.FindAll<DecorComponent> (ID);
		Object.transform.ApplyDecorComponents (components, ref pos, ref rot, ref scale);
	}

	public bool CheckEnvironmentVolumes (Vector3 pos, Quaternion rot, Vector3 scale, EnvironmentType type)
	{
		return Object.transform.CheckEnvironmentVolumes (pos, rot, scale, type);
	}

	public bool CheckEnvironmentVolumesInsideTerrain (Vector3 pos, Quaternion rot, Vector3 scale, EnvironmentType type, float padding = 0f)
	{
		return Object.transform.CheckEnvironmentVolumesInsideTerrain (pos, rot, scale, type, padding);
	}

	public bool CheckEnvironmentVolumesOutsideTerrain (Vector3 pos, Quaternion rot, Vector3 scale, EnvironmentType type, float padding = 0f)
	{
		return Object.transform.CheckEnvironmentVolumesOutsideTerrain (pos, rot, scale, type, padding);
	}

	public void ApplySequenceReplacement (List<Prefab> sequence, ref Prefab replacement, Prefab[] possibleReplacements, int pathLength, int pathIndex)
	{
		PathSequence pathSequence = Attribute.Find<PathSequence> (ID);
		if (pathSequence != null) {
			pathSequence.ApplySequenceReplacement (sequence, ref replacement, possibleReplacements, pathLength, pathIndex);
		}
	}

	public GameObject Spawn (Transform transform, bool active = true)
	{
		return Manager.CreatePrefab (Name, transform, active);
	}

	public GameObject Spawn (Vector3 pos, Quaternion rot, bool active = true)
	{
		return Manager.CreatePrefab (Name, pos, rot, active);
	}

	public GameObject Spawn (Vector3 pos, Quaternion rot, Vector3 scale, bool active = true)
	{
		return Manager.CreatePrefab (Name, pos, rot, scale, active);
	}

	public BaseEntity SpawnEntity (Vector3 pos, Quaternion rot, bool active = true)
	{
		return Manager.CreateEntity (Name, pos, rot, active);
	}

	public static Prefab<T> Load<T> (uint id, GameManager manager = null, PrefabAttribute.Library attribute = null) where T : Component
	{
		if (manager == null) {
			manager = DefaultManager;
		}
		if (attribute == null) {
			attribute = DefaultAttribute;
		}
		string text = StringPool.Get (id);
		if (string.IsNullOrWhiteSpace (text)) {
			Debug.LogWarning ($"Could not find path for prefab ID {id}");
			return null;
		}
		GameObject gameObject = manager.FindPrefab (text);
		T component = gameObject.GetComponent<T> ();
		return new Prefab<T> (text, gameObject, component, manager, attribute);
	}

	public static Prefab Load (uint id, GameManager manager = null, PrefabAttribute.Library attribute = null)
	{
		if (manager == null) {
			manager = DefaultManager;
		}
		if (attribute == null) {
			attribute = DefaultAttribute;
		}
		string text = StringPool.Get (id);
		if (string.IsNullOrWhiteSpace (text)) {
			Debug.LogWarning ($"Could not find path for prefab ID {id}");
			return null;
		}
		GameObject prefab = manager.FindPrefab (text);
		return new Prefab (text, prefab, manager, attribute);
	}

	public static Prefab[] Load (string folder, GameManager manager = null, PrefabAttribute.Library attribute = null, bool useProbabilities = true, bool useWorldConfig = true)
	{
		if (string.IsNullOrEmpty (folder)) {
			return null;
		}
		if (manager == null) {
			manager = DefaultManager;
		}
		if (attribute == null) {
			attribute = DefaultAttribute;
		}
		string[] array = FindPrefabNames (folder, useProbabilities, useWorldConfig);
		Prefab[] array2 = new Prefab[array.Length];
		for (int i = 0; i < array2.Length; i++) {
			string text = array [i];
			GameObject prefab = manager.FindPrefab (text);
			array2 [i] = new Prefab (text, prefab, manager, attribute);
		}
		return array2;
	}

	public static Prefab<T>[] Load<T> (string folder, GameManager manager = null, PrefabAttribute.Library attribute = null, bool useProbabilities = true, bool useWorldConfig = true) where T : Component
	{
		if (string.IsNullOrEmpty (folder)) {
			return null;
		}
		return Load<T> (FindPrefabNames (folder, useProbabilities, useWorldConfig), manager, attribute);
	}

	public static Prefab<T>[] Load<T> (string[] names, GameManager manager = null, PrefabAttribute.Library attribute = null) where T : Component
	{
		if (manager == null) {
			manager = DefaultManager;
		}
		if (attribute == null) {
			attribute = DefaultAttribute;
		}
		Prefab<T>[] array = new Prefab<T>[names.Length];
		for (int i = 0; i < array.Length; i++) {
			string text = names [i];
			GameObject gameObject = manager.FindPrefab (text);
			T component = gameObject.GetComponent<T> ();
			array [i] = new Prefab<T> (text, gameObject, component, manager, attribute);
		}
		return array;
	}

	public static Prefab LoadRandom (string folder, ref uint seed, GameManager manager = null, PrefabAttribute.Library attribute = null, bool useProbabilities = true)
	{
		if (string.IsNullOrEmpty (folder)) {
			return null;
		}
		if (manager == null) {
			manager = DefaultManager;
		}
		if (attribute == null) {
			attribute = DefaultAttribute;
		}
		string[] array = FindPrefabNames (folder, useProbabilities);
		if (array.Length == 0) {
			return null;
		}
		string text = array [SeedRandom.Range (ref seed, 0, array.Length)];
		GameObject prefab = manager.FindPrefab (text);
		return new Prefab (text, prefab, manager, attribute);
	}

	public static Prefab<T> LoadRandom<T> (string folder, ref uint seed, GameManager manager = null, PrefabAttribute.Library attribute = null, bool useProbabilities = true) where T : Component
	{
		if (string.IsNullOrEmpty (folder)) {
			return null;
		}
		if (manager == null) {
			manager = DefaultManager;
		}
		if (attribute == null) {
			attribute = DefaultAttribute;
		}
		string[] array = FindPrefabNames (folder, useProbabilities);
		if (array.Length == 0) {
			return null;
		}
		string text = array [SeedRandom.Range (ref seed, 0, array.Length)];
		GameObject gameObject = manager.FindPrefab (text);
		T component = gameObject.GetComponent<T> ();
		return new Prefab<T> (text, gameObject, component, manager, attribute);
	}

	private static string[] FindPrefabNames (string strPrefab, bool useProbabilities = false, bool useWorldConfig = false)
	{
		strPrefab = strPrefab.TrimEnd ('/').ToLower ();
		GameObject[] array = FileSystem.LoadPrefabs (strPrefab + "/");
		List<string> list = new List<string> (array.Length);
		GameObject[] array2 = array;
		foreach (GameObject gameObject in array2) {
			string text = strPrefab + "/" + gameObject.name.ToLower () + ".prefab";
			if (useWorldConfig && !World.Config.IsPrefabAllowed (text)) {
				continue;
			}
			if (!useProbabilities) {
				list.Add (text);
				continue;
			}
			PrefabParameters component = gameObject.GetComponent<PrefabParameters> ();
			int num = ((!component) ? 1 : component.Count);
			for (int j = 0; j < num; j++) {
				list.Add (text);
			}
		}
		list.Sort ();
		return list.ToArray ();
	}
}
