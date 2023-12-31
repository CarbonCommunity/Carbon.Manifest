using System;
using System.IO;
using Epic.OnlineServices;
using Epic.OnlineServices.Connect;
using Epic.OnlineServices.Logging;
using Epic.OnlineServices.Platform;
using UnityEngine;

public class EOS
{
	private static string productName = "Rust";

	private static string productVersion = "1.0";

	private static string productId = "429c2212ad284866aee071454c2125b5";

	private static string sandboxId = "ec47bae0651a4765a063c1e83ec41b34";

	private static string deploymentId = "76796531e86443548754600511f42e9e";

	public static PlatformInterface Interface;

	public static LogLevel LogLevel = LogLevel.Warning;

	private static StreamWriter logWriter = null;

	private static void OnLogMessage (ref LogMessage logMessage)
	{
		if (logWriter != null) {
			logWriter.Write (DateTime.Now.TimeOfDay.ToString ("hh\\:mm\\:ss"));
			logWriter.Write (' ');
			logWriter.WriteLine (logMessage.Message);
		} else {
			DebugEx.Log (logMessage.Message);
		}
	}

	public static void Initialize (bool isServer, string clientId, string clientSecret, string logFile = null)
	{
		if (Interface != null) {
			throw new Exception ("[EOS] Duplicate initialize");
		}
		InitializeOptions initializeOptions = default(InitializeOptions);
		initializeOptions.ProductName = productName;
		initializeOptions.ProductVersion = productVersion;
		InitializeOptions options = initializeOptions;
		Result result = PlatformInterface.Initialize (ref options);
		if (result != 0) {
			throw new Exception ("[EOS] Failed to initialize platform: " + result);
		}
		if (logFile != null) {
			logWriter = new StreamWriter (logFile, append: false);
			logWriter.AutoFlush = true;
		}
		LoggingInterface.SetLogLevel (LogCategory.AllCategories, LogLevel);
		LoggingInterface.SetCallback (OnLogMessage);
		Options options2 = default(Options);
		options2.IsServer = isServer;
		options2.ProductId = productId;
		options2.SandboxId = sandboxId;
		options2.DeploymentId = deploymentId;
		options2.ClientCredentials = new ClientCredentials {
			ClientId = clientId,
			ClientSecret = clientSecret
		};
		options2.Flags = PlatformFlags.DisableOverlay;
		Options options3 = options2;
		Interface = PlatformInterface.Create (ref options3);
		if (Interface == null) {
			throw new Exception ("[EOS] Failed to create platform");
		}
	}

	public static void AddAuthExpirationCallback (OnAuthExpirationCallback callback)
	{
		if (Interface == null) {
			throw new Exception ("[EOS] Attempting to add auth expiration callback without initialization");
		}
		AddNotifyAuthExpirationOptions options = default(AddNotifyAuthExpirationOptions);
		ConnectInterface connectInterface = Interface.GetConnectInterface ();
		if (connectInterface == null) {
			throw new Exception ("[EOS] Failed to get connect interface");
		}
		connectInterface.AddNotifyAuthExpiration (ref options, null, callback);
	}

	public static void Login (string steamLoginCredentialToken, OnLoginCallback callback)
	{
		if (Interface == null) {
			throw new Exception ("[EOS] Attempting to login without initialization");
		}
		ExternalCredentialType type = ExternalCredentialType.SteamAppTicket;
		LoginOptions loginOptions = default(LoginOptions);
		loginOptions.Credentials = new Credentials {
			Type = type,
			Token = steamLoginCredentialToken
		};
		LoginOptions options = loginOptions;
		ConnectInterface connectInterface = Interface.GetConnectInterface ();
		if (connectInterface == null) {
			throw new Exception ("[EOS] Failed to get connect interface");
		}
		connectInterface.Login (ref options, null, callback);
	}

	public static void CreateUser (ContinuanceToken token, OnCreateUserCallback callback)
	{
		if (Interface == null) {
			throw new Exception ("[EOS] Attempting to create user without initialization");
		}
		CreateUserOptions createUserOptions = default(CreateUserOptions);
		createUserOptions.ContinuanceToken = token;
		CreateUserOptions options = createUserOptions;
		ConnectInterface connectInterface = Interface.GetConnectInterface ();
		if (connectInterface == null) {
			throw new Exception ("[EOS] Failed to get connect interface");
		}
		connectInterface.CreateUser (ref options, null, callback);
	}

	public static Result CopyIdToken (ProductUserId account, out IdToken? token)
	{
		if (Interface == null) {
			throw new Exception ("[EOS] Attempting to create user without initialization");
		}
		CopyIdTokenOptions copyIdTokenOptions = default(CopyIdTokenOptions);
		copyIdTokenOptions.LocalUserId = account;
		CopyIdTokenOptions options = copyIdTokenOptions;
		ConnectInterface connectInterface = Interface.GetConnectInterface ();
		if (connectInterface == null) {
			throw new Exception ("[EOS] Failed to get connect interface");
		}
		return connectInterface.CopyIdToken (ref options, out token);
	}

	public static void VerifyIdToken (IntPtr client, IdToken token, OnVerifyIdTokenCallback callback)
	{
		if (Interface == null) {
			throw new Exception ("[EOS] Attempting to create user without initialization");
		}
		VerifyIdTokenOptions verifyIdTokenOptions = default(VerifyIdTokenOptions);
		verifyIdTokenOptions.IdToken = token;
		VerifyIdTokenOptions options = verifyIdTokenOptions;
		ConnectInterface connectInterface = Interface.GetConnectInterface ();
		if (connectInterface == null) {
			throw new Exception ("[EOS] Failed to get connect interface");
		}
		connectInterface.VerifyIdToken (ref options, client, callback);
	}

	public static void Shutdown ()
	{
		if (Interface != null) {
			Interface.Release ();
			Interface = null;
			PlatformInterface.Shutdown ();
		}
	}

	public static void Tick ()
	{
		if (Interface != null) {
			Interface.Tick ();
		}
	}
}
