using CompanionServer;
using CompanionServer.Cameras;
using CompanionServer.Handlers;
using Facepunch;
using ProtoBuf;
using UnityEngine;

public class CameraSubscribe : BaseHandler<AppCameraSubscribe>
{
	public override void Execute ()
	{
		if (!CameraRenderer.enabled) {
			SendError ("not_enabled");
			return;
		}
		CameraRendererManager instance = SingletonComponent<CameraRendererManager>.Instance;
		if (instance == null) {
			SendError ("server_error");
			return;
		}
		if (string.IsNullOrEmpty (base.Proto.cameraId)) {
			base.Client.EndViewing ();
			SendError ("invalid_id");
			return;
		}
		if (!base.Player.IsValid ()) {
			base.Client.EndViewing ();
			SendError ("no_player");
			return;
		}
		if (base.Player.IsConnected) {
			base.Client.EndViewing ();
			SendError ("player_online");
			return;
		}
		IRemoteControllable remoteControllable = RemoteControlEntity.FindByID (base.Proto.cameraId);
		if (remoteControllable == null || !remoteControllable.CanControl (base.UserId)) {
			base.Client.EndViewing ();
			SendError ("not_found");
			return;
		}
		if (remoteControllable is CCTV_RC cCTV_RC && cCTV_RC.IsStatic ()) {
			base.Client.EndViewing ();
			SendError ("access_denied");
			return;
		}
		BaseEntity ent = remoteControllable.GetEnt ();
		if (!ent.IsValid ()) {
			base.Client.EndViewing ();
			SendError ("not_found");
			return;
		}
		float num = Vector3.Distance (base.Player.transform.position, ent.transform.position);
		if (num >= remoteControllable.MaxRange) {
			base.Client.EndViewing ();
			SendError ("not_found");
			return;
		}
		if (!base.Client.BeginViewing (remoteControllable)) {
			base.Client.EndViewing ();
			SendError ("not_found");
			return;
		}
		instance.StartRendering (remoteControllable);
		AppResponse appResponse = Pool.Get<AppResponse> ();
		AppCameraInfo appCameraInfo = Pool.Get<AppCameraInfo> ();
		appCameraInfo.width = CameraRenderer.width;
		appCameraInfo.height = CameraRenderer.height;
		appCameraInfo.nearPlane = CameraRenderer.nearPlane;
		appCameraInfo.farPlane = CameraRenderer.farPlane;
		appCameraInfo.controlFlags = (int)(base.Client.IsControllingCamera ? remoteControllable.RequiredControls : RemoteControllableControls.None);
		appResponse.cameraSubscribeInfo = appCameraInfo;
		Send (appResponse);
	}
}
