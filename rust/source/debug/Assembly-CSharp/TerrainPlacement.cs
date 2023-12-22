using System;
using UnityEngine;

public abstract class TerrainPlacement : PrefabAttribute
{
	[ReadOnly]
	public Vector3 size = Vector3.zero;

	[ReadOnly]
	public Vector3 extents = Vector3.zero;

	[ReadOnly]
	public Vector3 offset = Vector3.zero;

	public bool HeightMap = true;

	public bool AlphaMap = true;

	public bool WaterMap = false;

	[InspectorFlags]
	public TerrainSplat.Enum SplatMask = (TerrainSplat.Enum)0;

	[InspectorFlags]
	public TerrainBiome.Enum BiomeMask = (TerrainBiome.Enum)0;

	[InspectorFlags]
	public TerrainTopology.Enum TopologyMask = (TerrainTopology.Enum)0;

	[HideInInspector]
	public Texture2DRef heightmap = null;

	[HideInInspector]
	public Texture2DRef splatmap0 = null;

	[HideInInspector]
	public Texture2DRef splatmap1 = null;

	[HideInInspector]
	public Texture2DRef alphamap = null;

	[HideInInspector]
	public Texture2DRef biomemap = null;

	[HideInInspector]
	public Texture2DRef topologymap = null;

	[HideInInspector]
	public Texture2DRef watermap = null;

	[HideInInspector]
	public Texture2DRef blendmap = null;

	public void Apply (Matrix4x4 localToWorld, Matrix4x4 worldToLocal)
	{
		if (ShouldHeight ()) {
			ApplyHeight (localToWorld, worldToLocal);
		}
		if (ShouldSplat ()) {
			ApplySplat (localToWorld, worldToLocal);
		}
		if (ShouldAlpha ()) {
			ApplyAlpha (localToWorld, worldToLocal);
		}
		if (ShouldBiome ()) {
			ApplyBiome (localToWorld, worldToLocal);
		}
		if (ShouldTopology ()) {
			ApplyTopology (localToWorld, worldToLocal);
		}
		if (ShouldWater ()) {
			ApplyWater (localToWorld, worldToLocal);
		}
	}

	protected bool ShouldHeight ()
	{
		return heightmap.isValid && HeightMap;
	}

	protected bool ShouldSplat (int id = -1)
	{
		return splatmap0.isValid && splatmap1.isValid && ((uint)SplatMask & (uint)id) != 0;
	}

	protected bool ShouldAlpha ()
	{
		return alphamap.isValid && AlphaMap;
	}

	protected bool ShouldBiome (int id = -1)
	{
		return biomemap.isValid && ((uint)BiomeMask & (uint)id) != 0;
	}

	protected bool ShouldTopology (int id = -1)
	{
		return topologymap.isValid && ((uint)TopologyMask & (uint)id) != 0;
	}

	protected bool ShouldWater ()
	{
		return watermap.isValid && WaterMap;
	}

	protected abstract void ApplyHeight (Matrix4x4 localToWorld, Matrix4x4 worldToLocal);

	protected abstract void ApplySplat (Matrix4x4 localToWorld, Matrix4x4 worldToLocal);

	protected abstract void ApplyAlpha (Matrix4x4 localToWorld, Matrix4x4 worldToLocal);

	protected abstract void ApplyBiome (Matrix4x4 localToWorld, Matrix4x4 worldToLocal);

	protected abstract void ApplyTopology (Matrix4x4 localToWorld, Matrix4x4 worldToLocal);

	protected abstract void ApplyWater (Matrix4x4 localToWorld, Matrix4x4 worldToLocal);

	protected override Type GetIndexedType ()
	{
		return typeof(TerrainPlacement);
	}
}
