using Facepunch;

public class Manifest
{
	[ClientVar]
	[ServerVar]
	public static object PrintManifest ()
	{
		return Application.Manifest;
	}

	[ClientVar]
	[ServerVar]
	public static object PrintManifestRaw ()
	{
		return Manifest.Contents;
	}
}
