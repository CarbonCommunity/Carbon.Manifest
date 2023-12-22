#define ENABLE_PROFILER
using System;
using System.Collections.Generic;
using System.Diagnostics;
using CompanionServer;
using CompanionServer.Cameras;
using Facepunch;
using Facepunch.Extend;
using UnityEngine.Profiling;

public class CameraRendererManager : SingletonComponent<CameraRendererManager>
{
	private readonly Stack<CameraRenderTask> _taskPool = new Stack<CameraRenderTask> ();

	private int _tasksTaken;

	private int _tasksReturned;

	private int _tasksCreated;

	private readonly Stopwatch _stopwatch = new Stopwatch ();

	private readonly List<CameraRenderer> _renderers = new List<CameraRenderer> ();

	private int _renderIndex;

	private int _completeIndex;

	protected override void OnDestroy ()
	{
		base.OnDestroy ();
		foreach (CameraRenderer renderer in _renderers) {
			renderer.Reset ();
		}
		_renderers.Clear ();
		CameraRenderTask.FreeCachedSamplePositions ();
		while (_taskPool.Count > 0) {
			CameraRenderTask cameraRenderTask = _taskPool.Pop ();
			cameraRenderTask.Dispose ();
		}
	}

	public void StartRendering (IRemoteControllable rc)
	{
		if (rc == null || rc.IsUnityNull ()) {
			throw new ArgumentNullException ("rc");
		}
		Profiler.BeginSample ("CameraRendererManager.StartRendering");
		CameraRenderer cameraRenderer = _renderers.FindWith ((CameraRenderer r) => r.rc, rc);
		if (cameraRenderer == null) {
			CameraRenderer cameraRenderer2 = Pool.Get<CameraRenderer> ();
			_renderers.Add (cameraRenderer2);
			cameraRenderer2.Init (rc);
		}
		Profiler.EndSample ();
	}

	public void Tick ()
	{
		if (CameraRenderer.enabled) {
			Profiler.BeginSample ("CameraRendererManager.Tick");
			DispatchRenderers ();
			CompleteRenderers ();
			CleanupRenderers ();
			Profiler.EndSample ();
		}
	}

	public CameraRenderTask BorrowTask ()
	{
		if (_taskPool.Count > 0) {
			_tasksTaken++;
			return _taskPool.Pop ();
		}
		_tasksCreated++;
		return new CameraRenderTask ();
	}

	public void ReturnTask (ref CameraRenderTask task)
	{
		if (task != null) {
			task.Reset ();
			_tasksReturned++;
			_taskPool.Push (task);
			task = null;
		}
	}

	[ServerVar]
	public static void pool_stats (ConsoleSystem.Arg arg)
	{
		CameraRendererManager instance = SingletonComponent<CameraRendererManager>.Instance;
		if (instance == null) {
			arg.ReplyWith ("Camera renderer manager is null!");
			return;
		}
		arg.ReplyWith ($"Active renderers: {instance._renderers.Count}\nTasks in pool: {instance._taskPool.Count}\nTasks taken: {instance._tasksTaken}\nTasks returned: {instance._tasksReturned}\nTasks created: {instance._tasksCreated}");
	}

	private void DispatchRenderers ()
	{
		Profiler.BeginSample ("DispatchRenders");
		List<CameraRenderer> obj = Pool.GetList<CameraRenderer> ();
		int count = _renderers.Count;
		for (int i = 0; i < count; i++) {
			if (_renderIndex >= count) {
				_renderIndex = 0;
			}
			CameraRenderer cameraRenderer = _renderers [_renderIndex++];
			if (cameraRenderer.CanRender ()) {
				obj.Add (cameraRenderer);
				if (obj.Count >= CameraRenderer.maxRendersPerFrame) {
					break;
				}
			}
		}
		if (obj.Count > 0) {
			int maxSampleCount = CameraRenderer.maxRaysPerFrame / obj.Count;
			foreach (CameraRenderer item in obj) {
				item.Render (maxSampleCount);
			}
		}
		Pool.FreeList (ref obj);
		Profiler.EndSample ();
	}

	private void CompleteRenderers ()
	{
		Profiler.BeginSample ("CompleteRenderers");
		_stopwatch.Restart ();
		int count = _renderers.Count;
		for (int i = 0; i < count; i++) {
			if (_completeIndex >= count) {
				_completeIndex = 0;
			}
			CameraRenderer cameraRenderer = _renderers [_completeIndex++];
			if (cameraRenderer.state == CameraRendererState.Rendering) {
				cameraRenderer.CompleteRender ();
				if (_stopwatch.Elapsed.TotalMilliseconds >= (double)CameraRenderer.completionFrameBudgetMs) {
					break;
				}
			}
		}
		Profiler.EndSample ();
	}

	private void CleanupRenderers ()
	{
		Profiler.BeginSample ("CleanupRenderers");
		List<CameraRenderer> obj = Pool.GetList<CameraRenderer> ();
		foreach (CameraRenderer renderer in _renderers) {
			if (renderer.state == CameraRendererState.Invalid) {
				obj.Add (renderer);
			}
		}
		_renderers.RemoveAll ((CameraRenderer r) => r.state == CameraRendererState.Invalid);
		foreach (CameraRenderer item in obj) {
			CameraRenderer obj2 = item;
			Pool.Free (ref obj2);
		}
		Pool.FreeList (ref obj);
		Profiler.EndSample ();
	}
}
