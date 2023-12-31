using System;
using UnityEngine;

public class Socket_Base : PrefabAttribute
{
	[Serializable]
	public class OccupiedSocketCheck
	{
		public Socket_Base Socket;

		public bool FemaleDummy;
	}

	public bool male = true;

	public bool maleDummy = false;

	public bool female = false;

	public bool femaleDummy = false;

	public bool femaleNoStability = false;

	public bool monogamous = false;

	[NonSerialized]
	public Vector3 position;

	[NonSerialized]
	public Quaternion rotation;

	private Type cachedType;

	public Vector3 selectSize = new Vector3 (2f, 0.1f, 2f);

	public Vector3 selectCenter = new Vector3 (0f, 0f, 1f);

	[ReadOnly]
	public string socketName;

	[NonSerialized]
	public SocketMod[] socketMods;

	public OccupiedSocketCheck[] checkOccupiedSockets;

	public Socket_Base ()
	{
		cachedType = GetType ();
	}

	public Vector3 GetSelectPivot (Vector3 position, Quaternion rotation)
	{
		return position + rotation * worldPosition;
	}

	public OBB GetSelectBounds (Vector3 position, Quaternion rotation)
	{
		return new OBB (position + rotation * worldPosition, Vector3.one, rotation * worldRotation, new Bounds (selectCenter, selectSize));
	}

	protected override Type GetIndexedType ()
	{
		return typeof(Socket_Base);
	}

	protected override void AttributeSetup (GameObject rootObj, string name, bool serverside, bool clientside, bool bundling)
	{
		base.AttributeSetup (rootObj, name, serverside, clientside, bundling);
		position = base.transform.position;
		rotation = base.transform.rotation;
		socketMods = GetComponentsInChildren<SocketMod> (includeInactive: true);
		SocketMod[] array = socketMods;
		foreach (SocketMod socketMod in array) {
			socketMod.baseSocket = this;
		}
	}

	public virtual bool TestTarget (Construction.Target target)
	{
		return target.socket != null;
	}

	public virtual bool IsCompatible (Socket_Base socket)
	{
		if (socket == null) {
			return false;
		}
		if (!socket.male && !male) {
			return false;
		}
		if (!socket.female && !female) {
			return false;
		}
		return socket.cachedType == cachedType;
	}

	public virtual bool CanConnect (Vector3 position, Quaternion rotation, Socket_Base socket, Vector3 socketPosition, Quaternion socketRotation)
	{
		return IsCompatible (socket);
	}

	public virtual Construction.Placement DoPlacement (Construction.Target target)
	{
		Quaternion quaternion = Quaternion.LookRotation (target.normal, Vector3.up) * Quaternion.Euler (target.rotation);
		Vector3 vector = target.position;
		vector -= quaternion * position;
		Construction.Placement placement = new Construction.Placement ();
		placement.rotation = quaternion;
		placement.position = vector;
		return placement;
	}

	public virtual bool CheckSocketMods (Construction.Placement placement)
	{
		SocketMod[] array = socketMods;
		foreach (SocketMod socketMod in array) {
			socketMod.ModifyPlacement (placement);
		}
		SocketMod[] array2 = socketMods;
		foreach (SocketMod socketMod2 in array2) {
			if (!socketMod2.DoCheck (placement)) {
				if (socketMod2.FailedPhrase.IsValid ()) {
					Construction.lastPlacementError = "Failed Check: (" + socketMod2.FailedPhrase.translated + ")";
				}
				return false;
			}
		}
		return true;
	}
}
