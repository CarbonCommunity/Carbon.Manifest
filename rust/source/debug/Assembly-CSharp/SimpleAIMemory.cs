using System.Collections.Generic;
using ConVar;
using Rust.AI;
using UnityEngine;
using UnityEngine.Profiling;

public class SimpleAIMemory
{
	public struct SeenInfo
	{
		public BaseEntity Entity;

		public Vector3 Position;

		public float Timestamp;

		public float Danger;
	}

	public static HashSet<BasePlayer> PlayerIgnoreList = new HashSet<BasePlayer> ();

	public List<SeenInfo> All = new List<SeenInfo> ();

	public List<BaseEntity> Players = new List<BaseEntity> ();

	public HashSet<BaseEntity> LOS = new HashSet<BaseEntity> ();

	public List<BaseEntity> Targets = new List<BaseEntity> ();

	public List<BaseEntity> Threats = new List<BaseEntity> ();

	public List<BaseEntity> Friendlies = new List<BaseEntity> ();

	public void SetKnown (BaseEntity ent, BaseEntity owner, AIBrainSenses brainSenses)
	{
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bf: Unknown result type (might be due to invalid IL or missing references)
		Profiler.BeginSample ("SimpleAIMemory.SetKnown");
		IAISenses iAISenses = owner as IAISenses;
		BasePlayer basePlayer = ent as BasePlayer;
		if ((Object)(object)basePlayer != (Object)null && PlayerIgnoreList.Contains (basePlayer)) {
			return;
		}
		bool flag = false;
		if (iAISenses != null && iAISenses.IsThreat (ent)) {
			flag = true;
			if (brainSenses != null) {
				brainSenses.LastThreatTimestamp = Time.realtimeSinceStartup;
			}
		}
		for (int i = 0; i < All.Count; i++) {
			if ((Object)(object)All [i].Entity == (Object)(object)ent) {
				SeenInfo value = All [i];
				value.Position = ((Component)ent).transform.position;
				value.Timestamp = Mathf.Max (Time.realtimeSinceStartup, value.Timestamp);
				All [i] = value;
				Profiler.EndSample ();
				return;
			}
		}
		if ((Object)(object)basePlayer != (Object)null) {
			if (AI.ignoreplayers && !basePlayer.IsNpc) {
				Profiler.EndSample ();
				return;
			}
			Players.Add (ent);
		}
		if (iAISenses != null) {
			if (iAISenses.IsTarget (ent)) {
				Targets.Add (ent);
			}
			if (iAISenses.IsFriendly (ent)) {
				Friendlies.Add (ent);
			}
			if (flag) {
				Threats.Add (ent);
			}
		}
		All.Add (new SeenInfo {
			Entity = ent,
			Position = ((Component)ent).transform.position,
			Timestamp = Time.realtimeSinceStartup
		});
		Profiler.EndSample ();
	}

	public void SetLOS (BaseEntity ent, bool flag)
	{
		if (!((Object)(object)ent == (Object)null)) {
			if (flag) {
				LOS.Add (ent);
			} else {
				LOS.Remove (ent);
			}
		}
	}

	public bool IsLOS (BaseEntity ent)
	{
		return LOS.Contains (ent);
	}

	public bool IsPlayerKnown (BasePlayer player)
	{
		return Players.Contains (player);
	}

	public void Forget (float secondsOld)
	{
		for (int i = 0; i < All.Count; i++) {
			if (!(Time.realtimeSinceStartup - All [i].Timestamp >= secondsOld)) {
				continue;
			}
			BaseEntity entity = All [i].Entity;
			if ((Object)(object)entity != (Object)null) {
				if (entity is BasePlayer) {
					Players.Remove (entity);
				}
				Targets.Remove (entity);
				Threats.Remove (entity);
				Friendlies.Remove (entity);
				LOS.Remove (entity);
			}
			All.RemoveAt (i);
			i--;
		}
	}

	public static void AddIgnorePlayer (BasePlayer player)
	{
		if (!PlayerIgnoreList.Contains (player)) {
			PlayerIgnoreList.Add (player);
		}
	}

	public static void RemoveIgnorePlayer (BasePlayer player)
	{
		PlayerIgnoreList.Remove (player);
	}

	public static void ClearIgnoredPlayers ()
	{
		PlayerIgnoreList.Clear ();
	}

	public static string GetIgnoredPlayers ()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
		TextTable val = new TextTable ();
		val.AddColumns (new string[2] { "Name", "Steam ID" });
		foreach (BasePlayer playerIgnore in PlayerIgnoreList) {
			val.AddRow (new string[2] {
				playerIgnore.displayName,
				playerIgnore.userID.ToString ()
			});
		}
		return ((object)val).ToString ();
	}
}
