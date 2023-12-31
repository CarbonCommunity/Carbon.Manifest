using System;
using System.Collections.Generic;
using Instancing;

[Serializable]
public class InstancedPrefabConfig
{
	public uint PrefabId;

	public List<InstancedMeshConfig> Meshes = new List<InstancedMeshConfig> ();

	public InstancedPrefabConfig (uint prefabId)
	{
		PrefabId = prefabId;
	}
}
