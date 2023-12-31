using System;
using Facepunch;
using Facepunch.Models;
using UnityEngine;

public class Integration : BaseIntegration
{
	public override string PublicKey => "j0VF6sNnzn9rwt9qTZtI02zTYK8PRdN1";

	public override string Bucket => "Server";

	public event Action OnManifestUpdated;

	public override void OnManifestFile (Facepunch.Models.Manifest manifest)
	{
		manifest.ExceptionReportingUrl = "https://83df169465e84da091c1a3cd2fbffeee:3671b903f9a840ecb68411cf946ab9b6@sentry.io/51080";
		this.OnManifestUpdated?.Invoke ();
	}

	public override bool ShouldReportException (string message, string stackTrace, LogType type)
	{
		if (!base.ShouldReportException (message, stackTrace, type)) {
			return false;
		}
		if (message.StartsWith ("[EAC] ")) {
			return false;
		}
		if (message.StartsWith ("FMOD ")) {
			return false;
		}
		if (message.StartsWith ("<RI.Hid>")) {
			return false;
		}
		if (message.StartsWith ("Platform does not support compute")) {
			return false;
		}
		if (message.StartsWith ("Initializing Microsoft Media Foundation failed")) {
			return false;
		}
		if (message.StartsWith ("Failed getting load state of FSB.")) {
			return false;
		}
		if (message.StartsWith ("Sound could not be played. FMOD Error")) {
			return false;
		}
		if (message.StartsWith ("C:\\buildslave\\")) {
			return false;
		}
		if (message.StartsWith ("Error: Cannot create FMOD::Sound instance")) {
			return false;
		}
		if (message.StartsWith ("OutOfMemoryException")) {
			return false;
		}
		if (message.StartsWith ("NotImplementedException")) {
			return false;
		}
		if (message.StartsWith ("HttpException")) {
			return false;
		}
		if (message.StartsWith ("Could not allocate memory")) {
			return false;
		}
		if (message.EndsWith ("an active agent that has been placed on a NavMesh.")) {
			return false;
		}
		if (message.StartsWith ("Unsupported texture format - Texture2D::EncodeTo")) {
			return false;
		}
		if (message.StartsWith ("Dimensions of color surface does not match dimensions of depth surface")) {
			return false;
		}
		return true;
	}
}
