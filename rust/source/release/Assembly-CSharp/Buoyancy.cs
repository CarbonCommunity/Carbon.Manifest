using System;
using ConVar;
using UnityEngine;

public class Buoyancy : ListComponent<Buoyancy>, IServerComponent
{
	private struct BuoyancyPointData
	{
		public Vector3 localPosition;

		public Vector3 rootToPoint;

		public Vector3 position;
	}

	public BuoyancyPoint[] points;

	public GameObjectRef[] waterImpacts;

	public Rigidbody rigidBody;

	public float buoyancyScale = 1f;

	public bool doEffects = true;

	public float flowMovementScale = 1f;

	public float requiredSubmergedFraction = 0.5f;

	public bool useUnderwaterDrag;

	[Range (0f, 3f)]
	public float underwaterDrag = 2f;

	[Range (0f, 1f)]
	[Tooltip ("How much this object will ignore the waves system, 0 = flat water, 1 = full waves (default 1)")]
	public float flatWaterLerp = 1f;

	public Action<bool> SubmergedChanged;

	public BaseEntity forEntity;

	[NonSerialized]
	public float submergedFraction;

	private BuoyancyPointData[] pointData;

	private Vector2[] pointPositionArray;

	private Vector2[] pointPositionUVArray;

	private Vector3[] pointShoreVectorArray;

	private float[] pointTerrainHeightArray;

	private float[] pointWaterHeightArray;

	private float defaultDrag;

	private float defaultAngularDrag;

	private float timeInWater;

	public float? ArtificialHeight;

	private BaseVehicle forVehicle;

	private bool hasLocalPlayers;

	private bool hadLocalPlayers;

	public float timeOutOfWater { get; private set; }

	public static string DefaultWaterImpact ()
	{
		return "assets/bundled/prefabs/fx/impacts/physics/water-enter-exit.prefab";
	}

	private void Awake ()
	{
		forVehicle = forEntity as BaseVehicle;
		((FacepunchBehaviour)this).InvokeRandomized ((Action)CheckSleepState, 0.5f, 5f, 1f);
	}

	public void Sleep ()
	{
		if (((Object)(object)forEntity == (Object)null || !forEntity.BuoyancySleep ()) && (Object)(object)rigidBody != (Object)null) {
			rigidBody.Sleep ();
		}
		((Behaviour)this).enabled = false;
	}

	public void Wake ()
	{
		if (((Object)(object)forEntity == (Object)null || !forEntity.BuoyancyWake ()) && (Object)(object)rigidBody != (Object)null) {
			rigidBody.WakeUp ();
		}
		((Behaviour)this).enabled = true;
	}

	public void CheckSleepState ()
	{
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		if ((Object)(object)((Component)this).transform == (Object)null || (Object)(object)rigidBody == (Object)null) {
			return;
		}
		Vector3 position = ((Component)this).transform.position;
		hasLocalPlayers = BaseNetworkable.HasCloseConnections (position, 100f);
		bool flag = rigidBody.IsSleeping () || rigidBody.isKinematic;
		bool flag2 = flag || (!hasLocalPlayers && timeInWater > 6f);
		if ((Object)(object)forVehicle != (Object)null && forVehicle.IsOn ()) {
			flag2 = false;
		}
		if (((Behaviour)this).enabled && flag2) {
			((FacepunchBehaviour)this).Invoke ((Action)Sleep, 0f);
			return;
		}
		if (!((Behaviour)this).enabled && hasLocalPlayers && !hadLocalPlayers) {
			DoCycle (forced: true);
		}
		bool flag3 = !flag || (hasLocalPlayers && submergedFraction > 0f);
		if (!((Behaviour)this).enabled && flag3) {
			((FacepunchBehaviour)this).Invoke ((Action)Wake, 0f);
		}
		hadLocalPlayers = hasLocalPlayers;
	}

	protected void DoCycle (bool forced = false)
	{
		if (!((Behaviour)this).enabled && !forced) {
			return;
		}
		bool num = submergedFraction > 0f;
		BuoyancyFixedUpdate ();
		bool flag = submergedFraction > 0f;
		if (num == flag) {
			return;
		}
		if (useUnderwaterDrag && (Object)(object)rigidBody != (Object)null) {
			if (flag) {
				defaultDrag = rigidBody.drag;
				defaultAngularDrag = rigidBody.angularDrag;
				rigidBody.drag = underwaterDrag;
				rigidBody.angularDrag = underwaterDrag;
			} else {
				rigidBody.drag = defaultDrag;
				rigidBody.angularDrag = defaultAngularDrag;
			}
		}
		if (SubmergedChanged != null) {
			SubmergedChanged (flag);
		}
	}

	public static void Cycle ()
	{
		Buoyancy[] buffer = ListComponent<Buoyancy>.InstanceList.Values.Buffer;
		int count = ListComponent<Buoyancy>.InstanceList.Count;
		for (int i = 0; i < count; i++) {
			buffer [i].DoCycle ();
		}
	}

	public Vector3 GetFlowDirection (Vector2 posUV)
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		if ((Object)(object)TerrainMeta.WaterMap == (Object)null) {
			return Vector3.zero;
		}
		Vector3 normalFast = TerrainMeta.WaterMap.GetNormalFast (posUV);
		float num = Mathf.Clamp01 (Mathf.Abs (normalFast.y));
		normalFast.y = 0f;
		Vector3Ex.FastRenormalize (normalFast, num);
		return normalFast;
	}

	public void EnsurePointsInitialized ()
	{
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		if (points == null || points.Length == 0) {
			Rigidbody component = ((Component)this).GetComponent<Rigidbody> ();
			if ((Object)(object)component != (Object)null) {
				GameObject val = new GameObject ("BuoyancyPoint");
				val.transform.parent = ((Component)component).gameObject.transform;
				val.transform.localPosition = component.centerOfMass;
				BuoyancyPoint buoyancyPoint = val.AddComponent<BuoyancyPoint> ();
				buoyancyPoint.buoyancyForce = component.mass * (0f - Physics.gravity.y);
				buoyancyPoint.buoyancyForce *= 1.32f;
				buoyancyPoint.size = 0.2f;
				points = new BuoyancyPoint[1];
				points [0] = buoyancyPoint;
			}
		}
		if (pointData == null || pointData.Length != points.Length) {
			pointData = new BuoyancyPointData[points.Length];
			pointPositionArray = (Vector2[])(object)new Vector2[points.Length];
			pointPositionUVArray = (Vector2[])(object)new Vector2[points.Length];
			pointShoreVectorArray = (Vector3[])(object)new Vector3[points.Length];
			pointTerrainHeightArray = new float[points.Length];
			pointWaterHeightArray = new float[points.Length];
			for (int i = 0; i < points.Length; i++) {
				Transform transform = ((Component)points [i]).transform;
				pointData [i].localPosition = transform.localPosition;
				pointData [i].rootToPoint = ((Component)this).transform.InverseTransformPoint (transform.position);
			}
		}
	}

	public void BuoyancyFixedUpdate ()
	{
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Unknown result type (might be due to invalid IL or missing references)
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		//IL_018e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0193: Unknown result type (might be due to invalid IL or missing references)
		//IL_019d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_023e: Unknown result type (might be due to invalid IL or missing references)
		//IL_022e: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02db: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0362: Unknown result type (might be due to invalid IL or missing references)
		//IL_0364: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0311: Unknown result type (might be due to invalid IL or missing references)
		//IL_032d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0349: Unknown result type (might be due to invalid IL or missing references)
		//IL_0423: Unknown result type (might be due to invalid IL or missing references)
		//IL_0425: Unknown result type (might be due to invalid IL or missing references)
		//IL_0427: Unknown result type (might be due to invalid IL or missing references)
		//IL_042c: Unknown result type (might be due to invalid IL or missing references)
		if ((Object)(object)TerrainMeta.WaterMap == (Object)null) {
			return;
		}
		EnsurePointsInitialized ();
		if ((Object)(object)rigidBody == (Object)null) {
			return;
		}
		if (buoyancyScale == 0f) {
			((FacepunchBehaviour)this).Invoke ((Action)Sleep, 0f);
			return;
		}
		float time = Time.time;
		float x = TerrainMeta.Position.x;
		float z = TerrainMeta.Position.z;
		float x2 = TerrainMeta.OneOverSize.x;
		float z2 = TerrainMeta.OneOverSize.z;
		Matrix4x4 localToWorldMatrix = ((Component)this).transform.localToWorldMatrix;
		for (int i = 0; i < pointData.Length; i++) {
			Vector3 val = ((Matrix4x4)(ref localToWorldMatrix)).MultiplyPoint3x4 (pointData [i].rootToPoint);
			pointData [i].position = val;
			float num = (val.x - x) * x2;
			float num2 = (val.z - z) * z2;
			pointPositionArray [i] = new Vector2 (val.x, val.z);
			pointPositionUVArray [i] = new Vector2 (num, num2);
		}
		WaterSystem.GetHeightArray (pointPositionArray, pointPositionUVArray, pointShoreVectorArray, pointTerrainHeightArray, pointWaterHeightArray);
		bool flag = flatWaterLerp < 1f;
		int num3 = 0;
		Vector3 val2 = default(Vector3);
		Vector3 val3 = default(Vector3);
		for (int j = 0; j < points.Length; j++) {
			BuoyancyPoint buoyancyPoint = points [j];
			Vector3 position = pointData [j].position;
			Vector3 localPosition = pointData [j].localPosition;
			Vector2 posUV = pointPositionUVArray [j];
			float terrainHeight = pointTerrainHeightArray [j];
			float waterHeight = pointWaterHeightArray [j];
			if (ArtificialHeight.HasValue) {
				waterHeight = ArtificialHeight.Value;
			}
			bool doDeepwaterChecks = !ArtificialHeight.HasValue;
			WaterLevel.WaterInfo buoyancyWaterInfo = WaterLevel.GetBuoyancyWaterInfo (position, posUV, terrainHeight, waterHeight, doDeepwaterChecks, forEntity);
			if (flag && buoyancyWaterInfo.isValid) {
				buoyancyWaterInfo.currentDepth = (buoyancyWaterInfo.surfaceLevel = Mathf.Lerp (Env.oceanlevel, buoyancyWaterInfo.surfaceLevel, flatWaterLerp)) - position.y;
			}
			bool flag2 = false;
			if (position.y < buoyancyWaterInfo.surfaceLevel && buoyancyWaterInfo.isValid) {
				flag2 = true;
				num3++;
				float currentDepth = buoyancyWaterInfo.currentDepth;
				float num4 = Mathf.InverseLerp (0f, buoyancyPoint.size, currentDepth);
				float num5 = 1f + Mathf.PerlinNoise (buoyancyPoint.randomOffset + time * buoyancyPoint.waveFrequency, 0f) * buoyancyPoint.waveScale;
				float num6 = buoyancyPoint.buoyancyForce * buoyancyScale;
				((Vector3)(ref val2))..ctor (0f, num5 * num4 * num6, 0f);
				Vector3 flowDirection = GetFlowDirection (posUV);
				if (flowDirection.y < 0.9999f && flowDirection != Vector3.up) {
					num6 *= 0.25f;
					val2.x += flowDirection.x * num6 * flowMovementScale;
					val2.y += flowDirection.y * num6 * flowMovementScale;
					val2.z += flowDirection.z * num6 * flowMovementScale;
				}
				rigidBody.AddForceAtPosition (val2, position, (ForceMode)0);
			}
			if (buoyancyPoint.doSplashEffects && ((!buoyancyPoint.wasSubmergedLastFrame && flag2) || (!flag2 && buoyancyPoint.wasSubmergedLastFrame)) && doEffects) {
				Vector3 relativePointVelocity = rigidBody.GetRelativePointVelocity (localPosition);
				if (((Vector3)(ref relativePointVelocity)).magnitude > 1f) {
					string strName = ((waterImpacts != null && waterImpacts.Length != 0 && waterImpacts [0].isValid) ? waterImpacts [0].resourcePath : DefaultWaterImpact ());
					((Vector3)(ref val3))..ctor (Random.Range (-0.25f, 0.25f), 0f, Random.Range (-0.25f, 0.25f));
					Effect.server.Run (strName, position + val3, Vector3.up);
					buoyancyPoint.nexSplashTime = Time.time + 0.25f;
				}
			}
			buoyancyPoint.wasSubmergedLastFrame = flag2;
		}
		if (points.Length != 0) {
			submergedFraction = (float)num3 / (float)points.Length;
		}
		if (submergedFraction > requiredSubmergedFraction) {
			timeInWater += Time.fixedDeltaTime;
			timeOutOfWater = 0f;
		} else {
			timeOutOfWater += Time.fixedDeltaTime;
			timeInWater = 0f;
		}
	}
}
