using UnityEngine;
using UnityEngine.Rendering;

public class InstancedCommandBufferManager
{
	private CommandBuffer normalCommandBuffer;

	private CommandBuffer shadowCommandBuffer;

	public void OnCameraEnabled (Camera camera)
	{
	}

	public void OnCameraDisabled (Camera camera)
	{
	}

	public void UpdateCameraHook ()
	{
	}
}
