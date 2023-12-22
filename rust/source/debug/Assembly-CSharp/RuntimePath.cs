#define ENABLE_PROFILER
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;

public class RuntimePath : IAIPath
{
	private List<IAIPathSpeedZone> speedZones = new List<IAIPathSpeedZone> ();

	private List<IAIPathInterestNode> interestNodes = new List<IAIPathInterestNode> ();

	public IAIPathNode[] Nodes { get; set; } = new IAIPathNode[0];


	public IEnumerable<IAIPathSpeedZone> SpeedZones => speedZones;

	public IEnumerable<IAIPathInterestNode> InterestNodes => interestNodes;

	public IAIPathNode GetClosestToPoint (Vector3 point)
	{
		Profiler.BeginSample ("RuntimePath.GetClosestToPoint");
		IAIPathNode result = Nodes [0];
		float num = float.PositiveInfinity;
		IAIPathNode[] nodes = Nodes;
		foreach (IAIPathNode iAIPathNode in nodes) {
			float sqrMagnitude = (point - iAIPathNode.Position).sqrMagnitude;
			if (sqrMagnitude < num) {
				num = sqrMagnitude;
				result = iAIPathNode;
			}
		}
		Profiler.EndSample ();
		return result;
	}

	public void GetNodesNear (Vector3 point, ref List<IAIPathNode> nearNodes, float dist = 10f)
	{
		Profiler.BeginSample ("RuntimePath.GetNodesNear");
		IAIPathNode[] nodes = Nodes;
		foreach (IAIPathNode iAIPathNode in nodes) {
			if ((Vector3Ex.XZ (point) - Vector3Ex.XZ (iAIPathNode.Position)).sqrMagnitude <= dist * dist) {
				nearNodes.Add (iAIPathNode);
			}
		}
		Profiler.EndSample ();
	}

	public IAIPathInterestNode GetRandomInterestNodeAwayFrom (Vector3 from, float dist = 10f)
	{
		IAIPathInterestNode iAIPathInterestNode = null;
		int num = 0;
		while (iAIPathInterestNode == null && num < 20) {
			iAIPathInterestNode = interestNodes [Random.Range (0, interestNodes.Count)];
			if ((iAIPathInterestNode.Position - from).sqrMagnitude < dist * dist) {
				iAIPathInterestNode = null;
				num++;
				continue;
			}
			break;
		}
		if (iAIPathInterestNode == null) {
			Debug.LogError ("Returning default interest zone");
			iAIPathInterestNode = interestNodes [0];
		}
		return iAIPathInterestNode;
	}

	public void AddInterestNode (IAIPathInterestNode interestNode)
	{
		if (!interestNodes.Contains (interestNode)) {
			interestNodes.Add (interestNode);
		}
	}

	public void AddSpeedZone (IAIPathSpeedZone speedZone)
	{
		if (!speedZones.Contains (speedZone)) {
			speedZones.Add (speedZone);
		}
	}
}
