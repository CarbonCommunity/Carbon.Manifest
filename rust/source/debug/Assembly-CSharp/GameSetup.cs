#define ENABLE_PROFILER
using System.Collections;
using ConVar;
using Network;
using Rust;
using UnityEngine;
using UnityEngine.Profiling;
using UnityEngine.SceneManagement;

public class GameSetup : MonoBehaviour
{
	public static bool RunOnce = false;

	public bool startServer = true;

	public string clientConnectCommand = "client.connect 127.0.0.1:28015";

	public bool loadMenu = true;

	public bool loadLevel = false;

	public string loadLevelScene = "";

	public bool loadSave = false;

	public string loadSaveFile = "";

	public string initializationCommands = "";

	protected void Awake ()
	{
		if (RunOnce) {
			GameManager.Destroy (base.gameObject);
			return;
		}
		GameManifest.Load ();
		GameManifest.LoadAssets ();
		RunOnce = true;
		Profiler.BeginSample ("Bootstrap.Initialization");
		if (Bootstrap.needsSetup) {
			Bootstrap.Init_Tier0 ();
			Bootstrap.Init_Systems ();
			Bootstrap.Init_Config ();
		}
		Profiler.EndSample ();
		if (initializationCommands.Length > 0) {
			string[] array = initializationCommands.Split (';');
			string[] array2 = array;
			foreach (string text in array2) {
				ConsoleSystem.Run (ConsoleSystem.Option.Server, text.Trim ());
			}
		}
		StartCoroutine (DoGameSetup ());
	}

	private IEnumerator DoGameSetup ()
	{
		Rust.Application.isLoading = true;
		TerrainMeta.InitNoTerrain ();
		ItemManager.Initialize ();
		LevelManager.CurrentLevelName = SceneManager.GetActiveScene ().name;
		if (loadLevel && !string.IsNullOrEmpty (loadLevelScene)) {
			Network.Net.sv.Reset ();
			ConVar.Server.level = loadLevelScene;
			LoadingScreen.Update ("LOADING SCENE");
			Profiler.BeginSample ("DoGameSetup.loadLevelScene");
			UnityEngine.Application.LoadLevelAdditive (loadLevelScene);
			Profiler.EndSample ();
			LoadingScreen.Update (loadLevelScene.ToUpper () + " LOADED");
		}
		if (startServer) {
			yield return StartCoroutine (StartServer ());
		}
		yield return null;
		Rust.Application.isLoading = false;
	}

	private IEnumerator StartServer ()
	{
		Profiler.BeginSample ("DoGameSetup.StartServer.GarbageCollect.collect");
		ConVar.GC.collect ();
		ConVar.GC.unload ();
		Profiler.EndSample ();
		yield return CoroutineEx.waitForEndOfFrame;
		yield return CoroutineEx.waitForEndOfFrame;
		yield return StartCoroutine (Bootstrap.StartServer (loadSave, loadSaveFile, allowOutOfDateSaves: true));
	}
}
