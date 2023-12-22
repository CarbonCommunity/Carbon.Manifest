using UnityEngine;

[Factory ("physics")]
public class Physics : ConsoleSystem
{
	private const float baseGravity = -9.81f;

	[ServerVar (Help = "Send effects to clients when physics objects collide")]
	public static bool sendeffects = true;

	[ServerVar]
	public static bool groundwatchdebug = false;

	[ServerVar]
	public static int groundwatchfails = 1;

	[ServerVar]
	public static float groundwatchdelay = 0.1f;

	[ClientVar]
	[ServerVar]
	public static bool batchsynctransforms = true;

	[ServerVar]
	public static float bouncethreshold {
		get {
			return Physics.bounceThreshold;
		}
		set {
			Physics.bounceThreshold = value;
		}
	}

	[ServerVar]
	public static float sleepthreshold {
		get {
			return Physics.sleepThreshold;
		}
		set {
			Physics.sleepThreshold = value;
		}
	}

	[ServerVar (Help = "The default solver iteration count permitted for any rigid bodies (default 7). Must be positive")]
	public static int solveriterationcount {
		get {
			return Physics.defaultSolverIterations;
		}
		set {
			Physics.defaultSolverIterations = value;
		}
	}

	[ServerVar (Help = "Gravity multiplier")]
	public static float gravity {
		get {
			return Physics.gravity.y / -9.81f;
		}
		set {
			Physics.gravity = new Vector3 (0f, value * -9.81f, 0f);
		}
	}

	[ClientVar (ClientAdmin = true)]
	[ServerVar (Help = "The amount of physics steps per second")]
	public static float steps {
		get {
			return 1f / Time.fixedDeltaTime;
		}
		set {
			if (value < 10f) {
				value = 10f;
			}
			if (value > 60f) {
				value = 60f;
			}
			Time.fixedDeltaTime = 1f / value;
		}
	}

	[ClientVar (ClientAdmin = true)]
	[ServerVar (Help = "The slowest physics steps will operate")]
	public static float minsteps {
		get {
			return 1f / Time.maximumDeltaTime;
		}
		set {
			if (value < 1f) {
				value = 1f;
			}
			if (value > 60f) {
				value = 60f;
			}
			Time.maximumDeltaTime = 1f / value;
		}
	}

	[ClientVar]
	[ServerVar]
	public static bool autosynctransforms {
		get {
			return Physics.autoSyncTransforms;
		}
		set {
			Physics.autoSyncTransforms = value;
		}
	}
}
