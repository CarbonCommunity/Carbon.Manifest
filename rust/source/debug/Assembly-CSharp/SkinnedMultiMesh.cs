using System;
using System.Collections.Generic;
using Facepunch;
using UnityEngine;

public class SkinnedMultiMesh : MonoBehaviour
{
	public struct Part
	{
		public Wearable wearable;

		public GameObject gameObject;

		public string name;

		public Item item;
	}

	public bool shadowOnly;

	internal bool IsVisible = true;

	public bool eyesView = false;

	public Skeleton skeleton;

	public SkeletonSkinLod skeletonSkinLod;

	public List<Part> parts = new List<Part> ();

	[NonSerialized]
	public List<Part> createdParts = new List<Part> ();

	[NonSerialized]
	public long lastBuildHash = 0L;

	[NonSerialized]
	public MaterialPropertyBlock sharedPropertyBlock;

	[NonSerialized]
	public MaterialPropertyBlock hairPropertyBlock;

	public float skinNumber = 0f;

	public float meshNumber = 0f;

	public float hairNumber = 0f;

	public int skinType = 0;

	public SkinSetCollection SkinCollection;

	public List<Renderer> Renderers { get; } = new List<Renderer> (32);

}
