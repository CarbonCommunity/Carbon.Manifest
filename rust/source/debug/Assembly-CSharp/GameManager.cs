#define ENABLE_PROFILER
using ConVar;
using Facepunch;
using Rust;
using UnityEngine;
using UnityEngine.Profiling;
using UnityEngine.SceneManagement;

public class GameManager
{
	public static GameManager server = new GameManager (clientside: false, serverside: true);

	internal PrefabPreProcess preProcessed;

	internal PrefabPoolCollection pool;

	private bool Clientside;

	private bool Serverside;

	public void Reset ()
	{
		pool.Clear ();
	}

	public GameManager (bool clientside, bool serverside)
	{
		Clientside = clientside;
		Serverside = serverside;
		preProcessed = new PrefabPreProcess (clientside, serverside);
		pool = new PrefabPoolCollection ();
	}

	public GameObject FindPrefab (uint prefabID)
	{
		string text = StringPool.Get (prefabID);
		if (string.IsNullOrEmpty (text)) {
			return null;
		}
		return FindPrefab (text);
	}

	public GameObject FindPrefab (BaseEntity ent)
	{
		if (ent == null) {
			return null;
		}
		return FindPrefab (ent.PrefabName);
	}

	public GameObject FindPrefab (string strPrefab)
	{
		Profiler.BeginSample ("FindPrefab");
		Profiler.BeginSample ("FindProcessed");
		GameObject gameObject = preProcessed.Find (strPrefab);
		if (gameObject != null) {
			Profiler.EndSample ();
			Profiler.EndSample ();
			return gameObject;
		}
		Profiler.EndSample ();
		Profiler.BeginSample ("LoadFromResources");
		gameObject = FileSystem.LoadPrefab (strPrefab);
		if (gameObject == null) {
			Profiler.EndSample ();
			Profiler.EndSample ();
			return null;
		}
		Profiler.EndSample ();
		Profiler.BeginSample ("PrefabPreProcess.Process");
		preProcessed.Process (strPrefab, gameObject);
		Profiler.EndSample ();
		Profiler.EndSample ();
		GameObject gameObject2 = preProcessed.Find (strPrefab);
		return (gameObject2 != null) ? gameObject2 : gameObject;
	}

	public GameObject CreatePrefab (string strPrefab, Vector3 pos, Quaternion rot, Vector3 scale, bool active = true)
	{
		Profiler.BeginSample ("GameManager.CreatePrefab");
		GameObject gameObject = Instantiate (strPrefab, pos, rot);
		if ((bool)gameObject) {
			gameObject.transform.localScale = scale;
			if (active) {
				gameObject.AwakeFromInstantiate ();
			}
		}
		Profiler.EndSample ();
		return gameObject;
	}

	public GameObject CreatePrefab (string strPrefab, Vector3 pos, Quaternion rot, bool active = true)
	{
		Profiler.BeginSample ("GameManager.CreatePrefab");
		GameObject gameObject = Instantiate (strPrefab, pos, rot);
		if ((bool)gameObject && active) {
			gameObject.AwakeFromInstantiate ();
		}
		Profiler.EndSample ();
		return gameObject;
	}

	public GameObject CreatePrefab (string strPrefab, bool active = true)
	{
		Profiler.BeginSample ("GameManager.CreatePrefab");
		GameObject gameObject = Instantiate (strPrefab, Vector3.zero, Quaternion.identity);
		if ((bool)gameObject && active) {
			gameObject.AwakeFromInstantiate ();
		}
		Profiler.EndSample ();
		return gameObject;
	}

	public GameObject CreatePrefab (string strPrefab, Transform parent, bool active = true)
	{
		Profiler.BeginSample ("GameManager.CreatePrefab");
		GameObject gameObject = Instantiate (strPrefab, parent.position, parent.rotation);
		if ((bool)gameObject) {
			gameObject.transform.SetParent (parent, worldPositionStays: false);
			gameObject.Identity ();
			if (active) {
				gameObject.AwakeFromInstantiate ();
			}
		}
		Profiler.EndSample ();
		return gameObject;
	}

	public BaseEntity CreateEntity (string strPrefab, Vector3 pos = default(Vector3), Quaternion rot = default(Quaternion), bool startActive = true)
	{
		if (string.IsNullOrEmpty (strPrefab)) {
			return null;
		}
		GameObject gameObject = CreatePrefab (strPrefab, pos, rot, startActive);
		if (gameObject == null) {
			return null;
		}
		Profiler.BeginSample ("GetComponent");
		BaseEntity component = gameObject.GetComponent<BaseEntity> ();
		Profiler.EndSample ();
		if (component == null) {
			Debug.LogError ("CreateEntity called on a prefab that isn't an entity! " + strPrefab);
			Object.Destroy (gameObject);
			return null;
		}
		if (component.CompareTag ("CannotBeCreated")) {
			Debug.LogWarning ("CreateEntity called on a prefab that has the CannotBeCreated tag set. " + strPrefab);
			Object.Destroy (gameObject);
			return null;
		}
		return component;
	}

	private GameObject Instantiate (string strPrefab, Vector3 pos, Quaternion rot)
	{
		Profiler.BeginSample (strPrefab);
		Profiler.BeginSample ("String.ToLower");
		if (!strPrefab.IsLower ()) {
			Debug.LogWarning ("Converting prefab name to lowercase: " + strPrefab);
			strPrefab = strPrefab.ToLower ();
		}
		Profiler.EndSample ();
		GameObject gameObject = FindPrefab (strPrefab);
		if (!gameObject) {
			Debug.LogError ("Couldn't find prefab \"" + strPrefab + "\"");
			Profiler.EndSample ();
			return null;
		}
		Profiler.BeginSample ("PrefabPool.Pop");
		GameObject gameObject2 = pool.Pop (StringPool.Get (strPrefab), pos, rot);
		Profiler.EndSample ();
		if (gameObject2 == null) {
			Profiler.BeginSample ("GameObject.Instantiate");
			gameObject2 = Facepunch.Instantiate.GameObject (gameObject, pos, rot);
			gameObject2.name = strPrefab;
			Profiler.EndSample ();
		} else {
			gameObject2.transform.localScale = gameObject.transform.localScale;
		}
		if (!Clientside && Serverside && gameObject2.transform.parent == null) {
			SceneManager.MoveGameObjectToScene (gameObject2, Rust.Server.EntityScene);
		}
		Profiler.EndSample ();
		return gameObject2;
	}

	public static void Destroy (Component component, float delay = 0f)
	{
		BaseEntity ent = component as BaseEntity;
		if (ent.IsValid ()) {
			Debug.LogError ("Trying to destroy an entity without killing it first: " + component.name);
		}
		Profiler.BeginSample ("Component.Destroy");
		Object.Destroy (component, delay);
		Profiler.EndSample ();
	}

	public static void Destroy (GameObject instance, float delay = 0f)
	{
		if ((bool)instance) {
			Profiler.BeginSample ("GetComponent");
			BaseEntity component = instance.GetComponent<BaseEntity> ();
			Profiler.EndSample ();
			if (component.IsValid ()) {
				Debug.LogError ("Trying to destroy an entity without killing it first: " + instance.name);
			}
			Profiler.BeginSample ("GameObject.Destroy");
			Object.Destroy (instance, delay);
			Profiler.EndSample ();
		}
	}

	public static void DestroyImmediate (Component component, bool allowDestroyingAssets = false)
	{
		BaseEntity ent = component as BaseEntity;
		if (ent.IsValid ()) {
			Debug.LogError ("Trying to destroy an entity without killing it first: " + component.name);
		}
		Profiler.BeginSample ("Component.DestroyImmediate");
		Object.DestroyImmediate (component, allowDestroyingAssets);
		Profiler.EndSample ();
	}

	public static void DestroyImmediate (GameObject instance, bool allowDestroyingAssets = false)
	{
		Profiler.BeginSample ("GetComponent");
		BaseEntity component = instance.GetComponent<BaseEntity> ();
		Profiler.EndSample ();
		if (component.IsValid ()) {
			Debug.LogError ("Trying to destroy an entity without killing it first: " + instance.name);
		}
		Profiler.BeginSample ("GameObject.DestroyImmediate");
		Object.DestroyImmediate (instance, allowDestroyingAssets);
		Profiler.EndSample ();
	}

	public void Retire (GameObject instance)
	{
		if (!instance) {
			return;
		}
		using (TimeWarning.New ("GameManager.Retire")) {
			Profiler.BeginSample ("GetComponent");
			BaseEntity component = instance.GetComponent<BaseEntity> ();
			Profiler.EndSample ();
			if (component.IsValid ()) {
				Debug.LogError ("Trying to retire an entity without killing it first: " + instance.name);
			}
			if (!Rust.Application.isQuitting && ConVar.Pool.enabled && instance.SupportsPooling ()) {
				Profiler.BeginSample ("PrefabPool.Push");
				pool.Push (instance);
				Profiler.EndSample ();
			} else {
				Profiler.BeginSample ("GameObject.Destroy");
				Object.Destroy (instance);
				Profiler.EndSample ();
			}
		}
	}
}
