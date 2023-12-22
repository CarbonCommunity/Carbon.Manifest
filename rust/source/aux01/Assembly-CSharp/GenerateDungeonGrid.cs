using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GenerateDungeonGrid : ProceduralComponent
{
	private class PathNode
	{
		public MonumentInfo monument;

		public PathFinder.Node node;
	}

	private class PathSegment
	{
		public PathFinder.Node start;

		public PathFinder.Node end;
	}

	private class PathLink
	{
		public PathLinkSide downwards;

		public PathLinkSide upwards;
	}

	private class PathLinkSide
	{
		public PathLinkSegment origin;

		public List<PathLinkSegment> segments;

		public PathLinkSegment prevSegment {
			get {
				if (segments.Count <= 0) {
					return origin;
				}
				return segments [segments.Count - 1];
			}
		}
	}

	private class PathLinkSegment
	{
		public Vector3 position;

		public Quaternion rotation;

		public Vector3 scale;

		public Prefab<DungeonGridLink> prefab;

		public DungeonGridLink link;

		public Transform downSocket => link.DownSocket;

		public Transform upSocket => link.UpSocket;

		public DungeonGridLinkType downType => link.DownType;

		public DungeonGridLinkType upType => link.UpType;
	}

	public string TunnelFolder = string.Empty;

	public string StationFolder = string.Empty;

	public string TransitionFolder = string.Empty;

	public string LinkFolder = string.Empty;

	public InfrastructureType ConnectionType = InfrastructureType.Tunnel;

	public int CellSize = 216;

	public float LinkHeight = 1.5f;

	public float LinkRadius = 3f;

	public float LinkTransition = 9f;

	private const int MaxDepth = 100000;

	public override bool RunOnCache => true;

	public override void Process (uint seed)
	{
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_026f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0274: Unknown result type (might be due to invalid IL or missing references)
		//IL_0279: Unknown result type (might be due to invalid IL or missing references)
		//IL_027d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0284: Unknown result type (might be due to invalid IL or missing references)
		//IL_083f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0844: Unknown result type (might be due to invalid IL or missing references)
		//IL_0849: Unknown result type (might be due to invalid IL or missing references)
		//IL_0851: Unknown result type (might be due to invalid IL or missing references)
		//IL_0858: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0303: Unknown result type (might be due to invalid IL or missing references)
		//IL_030c: Unknown result type (might be due to invalid IL or missing references)
		//IL_031a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1008: Unknown result type (might be due to invalid IL or missing references)
		//IL_100d: Unknown result type (might be due to invalid IL or missing references)
		//IL_100f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1014: Unknown result type (might be due to invalid IL or missing references)
		//IL_1016: Unknown result type (might be due to invalid IL or missing references)
		//IL_1020: Unknown result type (might be due to invalid IL or missing references)
		//IL_1025: Unknown result type (might be due to invalid IL or missing references)
		//IL_1027: Unknown result type (might be due to invalid IL or missing references)
		//IL_1038: Unknown result type (might be due to invalid IL or missing references)
		//IL_103d: Unknown result type (might be due to invalid IL or missing references)
		//IL_032a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0331: Unknown result type (might be due to invalid IL or missing references)
		//IL_103f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0341: Unknown result type (might be due to invalid IL or missing references)
		//IL_0351: Unknown result type (might be due to invalid IL or missing references)
		//IL_0358: Unknown result type (might be due to invalid IL or missing references)
		//IL_1121: Unknown result type (might be due to invalid IL or missing references)
		//IL_1123: Unknown result type (might be due to invalid IL or missing references)
		//IL_1074: Unknown result type (might be due to invalid IL or missing references)
		//IL_1076: Unknown result type (might be due to invalid IL or missing references)
		//IL_107b: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0504: Unknown result type (might be due to invalid IL or missing references)
		//IL_1091: Unknown result type (might be due to invalid IL or missing references)
		//IL_1093: Unknown result type (might be due to invalid IL or missing references)
		//IL_1095: Unknown result type (might be due to invalid IL or missing references)
		//IL_109a: Unknown result type (might be due to invalid IL or missing references)
		//IL_109c: Unknown result type (might be due to invalid IL or missing references)
		//IL_10a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_10a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_114c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1151: Unknown result type (might be due to invalid IL or missing references)
		//IL_1153: Unknown result type (might be due to invalid IL or missing references)
		//IL_1158: Unknown result type (might be due to invalid IL or missing references)
		//IL_056c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0575: Unknown result type (might be due to invalid IL or missing references)
		//IL_10ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_10bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_10be: Unknown result type (might be due to invalid IL or missing references)
		//IL_10c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_10c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_10ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_10cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0597: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_10de: Unknown result type (might be due to invalid IL or missing references)
		//IL_10e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_10e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_10e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_10ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_042c: Unknown result type (might be due to invalid IL or missing references)
		//IL_042e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0433: Unknown result type (might be due to invalid IL or missing references)
		//IL_0438: Unknown result type (might be due to invalid IL or missing references)
		//IL_043d: Unknown result type (might be due to invalid IL or missing references)
		//IL_044d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0452: Unknown result type (might be due to invalid IL or missing references)
		//IL_0457: Unknown result type (might be due to invalid IL or missing references)
		//IL_045c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0460: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0479: Unknown result type (might be due to invalid IL or missing references)
		//IL_0480: Unknown result type (might be due to invalid IL or missing references)
		//IL_0487: Unknown result type (might be due to invalid IL or missing references)
		//IL_048c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0498: Unknown result type (might be due to invalid IL or missing references)
		//IL_049d: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_062e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0633: Unknown result type (might be due to invalid IL or missing references)
		//IL_0641: Unknown result type (might be due to invalid IL or missing references)
		//IL_0646: Unknown result type (might be due to invalid IL or missing references)
		//IL_064a: Unknown result type (might be due to invalid IL or missing references)
		//IL_064f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0661: Unknown result type (might be due to invalid IL or missing references)
		//IL_0668: Unknown result type (might be due to invalid IL or missing references)
		//IL_066f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0674: Unknown result type (might be due to invalid IL or missing references)
		//IL_0679: Unknown result type (might be due to invalid IL or missing references)
		//IL_067b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0680: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_06be: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_06db: Unknown result type (might be due to invalid IL or missing references)
		//IL_072d: Unknown result type (might be due to invalid IL or missing references)
		//IL_072f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0740: Unknown result type (might be due to invalid IL or missing references)
		//IL_0742: Unknown result type (might be due to invalid IL or missing references)
		//IL_0747: Unknown result type (might be due to invalid IL or missing references)
		//IL_0758: Unknown result type (might be due to invalid IL or missing references)
		//IL_075d: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_119e: Unknown result type (might be due to invalid IL or missing references)
		//IL_11a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_11a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_11ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_11b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_11b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_11b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_11bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_149e: Unknown result type (might be due to invalid IL or missing references)
		//IL_14af: Unknown result type (might be due to invalid IL or missing references)
		//IL_14c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_14d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_14db: Unknown result type (might be due to invalid IL or missing references)
		//IL_14e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_14e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_14ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_14f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_1509: Unknown result type (might be due to invalid IL or missing references)
		//IL_151f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1530: Unknown result type (might be due to invalid IL or missing references)
		//IL_1535: Unknown result type (might be due to invalid IL or missing references)
		//IL_153a: Unknown result type (might be due to invalid IL or missing references)
		//IL_153f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1544: Unknown result type (might be due to invalid IL or missing references)
		//IL_1546: Unknown result type (might be due to invalid IL or missing references)
		//IL_154b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1564: Unknown result type (might be due to invalid IL or missing references)
		//IL_1569: Unknown result type (might be due to invalid IL or missing references)
		//IL_157f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1584: Unknown result type (might be due to invalid IL or missing references)
		//IL_1597: Unknown result type (might be due to invalid IL or missing references)
		//IL_159c: Unknown result type (might be due to invalid IL or missing references)
		//IL_222e: Unknown result type (might be due to invalid IL or missing references)
		//IL_2235: Unknown result type (might be due to invalid IL or missing references)
		//IL_223c: Unknown result type (might be due to invalid IL or missing references)
		//IL_12e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_12e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_12fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_1300: Unknown result type (might be due to invalid IL or missing references)
		//IL_1305: Unknown result type (might be due to invalid IL or missing references)
		//IL_130e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1310: Unknown result type (might be due to invalid IL or missing references)
		//IL_1312: Unknown result type (might be due to invalid IL or missing references)
		//IL_1317: Unknown result type (might be due to invalid IL or missing references)
		//IL_131c: Unknown result type (might be due to invalid IL or missing references)
		//IL_13f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_13fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_1411: Unknown result type (might be due to invalid IL or missing references)
		//IL_1416: Unknown result type (might be due to invalid IL or missing references)
		//IL_141b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1424: Unknown result type (might be due to invalid IL or missing references)
		//IL_1426: Unknown result type (might be due to invalid IL or missing references)
		//IL_1428: Unknown result type (might be due to invalid IL or missing references)
		//IL_142d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1432: Unknown result type (might be due to invalid IL or missing references)
		//IL_15ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_15cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_15d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_15dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_15df: Unknown result type (might be due to invalid IL or missing references)
		//IL_15e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_15f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_15fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_1604: Unknown result type (might be due to invalid IL or missing references)
		//IL_1610: Unknown result type (might be due to invalid IL or missing references)
		//IL_1615: Unknown result type (might be due to invalid IL or missing references)
		//IL_161a: Unknown result type (might be due to invalid IL or missing references)
		//IL_161f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1624: Unknown result type (might be due to invalid IL or missing references)
		//IL_1628: Unknown result type (might be due to invalid IL or missing references)
		//IL_1634: Unknown result type (might be due to invalid IL or missing references)
		//IL_1639: Unknown result type (might be due to invalid IL or missing references)
		//IL_163e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b30: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b32: Unknown result type (might be due to invalid IL or missing references)
		//IL_228b: Unknown result type (might be due to invalid IL or missing references)
		//IL_2292: Unknown result type (might be due to invalid IL or missing references)
		//IL_2299: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b47: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b49: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b50: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b52: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b59: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b5e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b8d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b8f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b91: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b96: Unknown result type (might be due to invalid IL or missing references)
		//IL_16c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ba0: Unknown result type (might be due to invalid IL or missing references)
		//IL_169d: Unknown result type (might be due to invalid IL or missing references)
		//IL_16d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_1bc2: Unknown result type (might be due to invalid IL or missing references)
		//IL_1bc7: Unknown result type (might be due to invalid IL or missing references)
		//IL_1bd0: Unknown result type (might be due to invalid IL or missing references)
		//IL_1bd5: Unknown result type (might be due to invalid IL or missing references)
		//IL_1bd7: Unknown result type (might be due to invalid IL or missing references)
		//IL_1bdc: Unknown result type (might be due to invalid IL or missing references)
		//IL_1bee: Unknown result type (might be due to invalid IL or missing references)
		//IL_1bf5: Unknown result type (might be due to invalid IL or missing references)
		//IL_1bfc: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c08: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c0d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c12: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c17: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c1c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c20: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c2c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c31: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c36: Unknown result type (might be due to invalid IL or missing references)
		//IL_1bae: Unknown result type (might be due to invalid IL or missing references)
		//IL_16ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_16fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_1705: Unknown result type (might be due to invalid IL or missing references)
		//IL_170a: Unknown result type (might be due to invalid IL or missing references)
		//IL_170f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1714: Unknown result type (might be due to invalid IL or missing references)
		//IL_1716: Unknown result type (might be due to invalid IL or missing references)
		//IL_171f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1724: Unknown result type (might be due to invalid IL or missing references)
		//IL_1729: Unknown result type (might be due to invalid IL or missing references)
		//IL_173b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1747: Unknown result type (might be due to invalid IL or missing references)
		//IL_174c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1751: Unknown result type (might be due to invalid IL or missing references)
		//IL_2128: Unknown result type (might be due to invalid IL or missing references)
		//IL_212a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1799: Unknown result type (might be due to invalid IL or missing references)
		//IL_179b: Unknown result type (might be due to invalid IL or missing references)
		//IL_17a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_17a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_17ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_17b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_17b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_17be: Unknown result type (might be due to invalid IL or missing references)
		//IL_17ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_17cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_17d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_17d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_17db: Unknown result type (might be due to invalid IL or missing references)
		//IL_17dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_17df: Unknown result type (might be due to invalid IL or missing references)
		//IL_17e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_17e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_17e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_17ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_17ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_1803: Unknown result type (might be due to invalid IL or missing references)
		//IL_1805: Unknown result type (might be due to invalid IL or missing references)
		//IL_1807: Unknown result type (might be due to invalid IL or missing references)
		//IL_180c: Unknown result type (might be due to invalid IL or missing references)
		//IL_180e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1810: Unknown result type (might be due to invalid IL or missing references)
		//IL_1812: Unknown result type (might be due to invalid IL or missing references)
		//IL_1817: Unknown result type (might be due to invalid IL or missing references)
		//IL_182b: Unknown result type (might be due to invalid IL or missing references)
		//IL_182d: Unknown result type (might be due to invalid IL or missing references)
		//IL_175d: Unknown result type (might be due to invalid IL or missing references)
		//IL_175f: Unknown result type (might be due to invalid IL or missing references)
		//IL_213f: Unknown result type (might be due to invalid IL or missing references)
		//IL_2141: Unknown result type (might be due to invalid IL or missing references)
		//IL_2148: Unknown result type (might be due to invalid IL or missing references)
		//IL_214a: Unknown result type (might be due to invalid IL or missing references)
		//IL_2151: Unknown result type (might be due to invalid IL or missing references)
		//IL_2156: Unknown result type (might be due to invalid IL or missing references)
		//IL_2185: Unknown result type (might be due to invalid IL or missing references)
		//IL_2187: Unknown result type (might be due to invalid IL or missing references)
		//IL_2189: Unknown result type (might be due to invalid IL or missing references)
		//IL_218e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1773: Unknown result type (might be due to invalid IL or missing references)
		//IL_1775: Unknown result type (might be due to invalid IL or missing references)
		//IL_1777: Unknown result type (might be due to invalid IL or missing references)
		//IL_177c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1781: Unknown result type (might be due to invalid IL or missing references)
		//IL_1783: Unknown result type (might be due to invalid IL or missing references)
		//IL_1785: Unknown result type (might be due to invalid IL or missing references)
		//IL_1787: Unknown result type (might be due to invalid IL or missing references)
		//IL_178c: Unknown result type (might be due to invalid IL or missing references)
		//IL_178e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1790: Unknown result type (might be due to invalid IL or missing references)
		//IL_1792: Unknown result type (might be due to invalid IL or missing references)
		//IL_1797: Unknown result type (might be due to invalid IL or missing references)
		//IL_1cb9: Unknown result type (might be due to invalid IL or missing references)
		//IL_1874: Unknown result type (might be due to invalid IL or missing references)
		//IL_1880: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c95: Unknown result type (might be due to invalid IL or missing references)
		//IL_1cca: Unknown result type (might be due to invalid IL or missing references)
		//IL_1897: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ca6: Unknown result type (might be due to invalid IL or missing references)
		//IL_18c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_18cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_18aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_18b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_1cf4: Unknown result type (might be due to invalid IL or missing references)
		//IL_1cfd: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d02: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d07: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d0c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d0e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d17: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d1c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d21: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d33: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d3f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d44: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d49: Unknown result type (might be due to invalid IL or missing references)
		//IL_18e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d91: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d93: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d9c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1da1: Unknown result type (might be due to invalid IL or missing references)
		//IL_1da6: Unknown result type (might be due to invalid IL or missing references)
		//IL_1dab: Unknown result type (might be due to invalid IL or missing references)
		//IL_1dad: Unknown result type (might be due to invalid IL or missing references)
		//IL_1db6: Unknown result type (might be due to invalid IL or missing references)
		//IL_1dc2: Unknown result type (might be due to invalid IL or missing references)
		//IL_1dc7: Unknown result type (might be due to invalid IL or missing references)
		//IL_1dcc: Unknown result type (might be due to invalid IL or missing references)
		//IL_1dd1: Unknown result type (might be due to invalid IL or missing references)
		//IL_1dd3: Unknown result type (might be due to invalid IL or missing references)
		//IL_1dd5: Unknown result type (might be due to invalid IL or missing references)
		//IL_1dd7: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ddc: Unknown result type (might be due to invalid IL or missing references)
		//IL_1dde: Unknown result type (might be due to invalid IL or missing references)
		//IL_1de0: Unknown result type (might be due to invalid IL or missing references)
		//IL_1de2: Unknown result type (might be due to invalid IL or missing references)
		//IL_1de7: Unknown result type (might be due to invalid IL or missing references)
		//IL_1dfb: Unknown result type (might be due to invalid IL or missing references)
		//IL_1dfd: Unknown result type (might be due to invalid IL or missing references)
		//IL_1dff: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e04: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e06: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e08: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e0a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e0f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e23: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e25: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d55: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d57: Unknown result type (might be due to invalid IL or missing references)
		//IL_1912: Unknown result type (might be due to invalid IL or missing references)
		//IL_191e: Unknown result type (might be due to invalid IL or missing references)
		//IL_18f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_1900: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d6b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d6d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d6f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d74: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d79: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d7b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d7d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d7f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d84: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d86: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d88: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d8a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d8f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1935: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e6c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e78: Unknown result type (might be due to invalid IL or missing references)
		//IL_1948: Unknown result type (might be due to invalid IL or missing references)
		//IL_194f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e8f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a00: Unknown result type (might be due to invalid IL or missing references)
		//IL_1968: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ebb: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ec7: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ea2: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ea9: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a0e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1979: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ede: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f0a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f16: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ef1: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ef8: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a52: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a23: Unknown result type (might be due to invalid IL or missing references)
		//IL_1990: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f2d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b0f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b11: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b17: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b19: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b1b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b1d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1aa5: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a75: Unknown result type (might be due to invalid IL or missing references)
		//IL_19c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_19a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f40: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f47: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ac5: Unknown result type (might be due to invalid IL or missing references)
		//IL_19d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ff8: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f60: Unknown result type (might be due to invalid IL or missing references)
		//IL_1afb: Unknown result type (might be due to invalid IL or missing references)
		//IL_1afd: Unknown result type (might be due to invalid IL or missing references)
		//IL_2006: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f71: Unknown result type (might be due to invalid IL or missing references)
		//IL_204a: Unknown result type (might be due to invalid IL or missing references)
		//IL_201b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f88: Unknown result type (might be due to invalid IL or missing references)
		//IL_2107: Unknown result type (might be due to invalid IL or missing references)
		//IL_2109: Unknown result type (might be due to invalid IL or missing references)
		//IL_210f: Unknown result type (might be due to invalid IL or missing references)
		//IL_2111: Unknown result type (might be due to invalid IL or missing references)
		//IL_2113: Unknown result type (might be due to invalid IL or missing references)
		//IL_2115: Unknown result type (might be due to invalid IL or missing references)
		//IL_209d: Unknown result type (might be due to invalid IL or missing references)
		//IL_206d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1fbe: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f9b: Unknown result type (might be due to invalid IL or missing references)
		//IL_20bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_1fd1: Unknown result type (might be due to invalid IL or missing references)
		//IL_20f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_20f5: Unknown result type (might be due to invalid IL or missing references)
		if (World.Cached) {
			TerrainMeta.Path.DungeonGridRoot = HierarchyUtil.GetRoot ("Dungeon");
		} else if (World.Networked) {
			World.Spawn ("Dungeon");
			TerrainMeta.Path.DungeonGridRoot = HierarchyUtil.GetRoot ("Dungeon");
		} else {
			if (ConnectionType == InfrastructureType.Tunnel && !World.Config.BelowGroundRails) {
				return;
			}
			Prefab<DungeonGridCell>[] array = Prefab.Load<DungeonGridCell> ("assets/bundled/prefabs/autospawn/" + TunnelFolder, (GameManager)null, (PrefabAttribute.Library)null, useProbabilities: true, useWorldConfig: false);
			if (array == null || array.Length == 0) {
				return;
			}
			Prefab<DungeonGridCell>[] array2 = Prefab.Load<DungeonGridCell> ("assets/bundled/prefabs/autospawn/" + StationFolder, (GameManager)null, (PrefabAttribute.Library)null, useProbabilities: true, useWorldConfig: false);
			if (array2 == null || array2.Length == 0) {
				return;
			}
			Prefab<DungeonGridCell>[] array3 = Prefab.Load<DungeonGridCell> ("assets/bundled/prefabs/autospawn/" + TransitionFolder, (GameManager)null, (PrefabAttribute.Library)null, useProbabilities: true, useWorldConfig: false);
			if (array3 == null) {
				return;
			}
			Prefab<DungeonGridLink>[] array4 = Prefab.Load<DungeonGridLink> ("assets/bundled/prefabs/autospawn/" + LinkFolder, (GameManager)null, (PrefabAttribute.Library)null, useProbabilities: true, useWorldConfig: false);
			if (array4 == null) {
				return;
			}
			array4 = array4.OrderByDescending ((Prefab<DungeonGridLink> x) => x.Component.Priority).ToArray ();
			List<DungeonGridInfo> list = (Object.op_Implicit ((Object)(object)TerrainMeta.Path) ? TerrainMeta.Path.DungeonGridEntrances : null);
			WorldSpaceGrid<Prefab<DungeonGridCell>> val = new WorldSpaceGrid<Prefab<DungeonGridCell>> (TerrainMeta.Size.x * 2f, (float)CellSize);
			int[,] array5 = new int[val.CellCount, val.CellCount];
			DungeonGridConnectionHash[,] hashmap = new DungeonGridConnectionHash[val.CellCount, val.CellCount];
			PathFinder pathFinder = new PathFinder (array5, diagonals: false);
			int cellCount = val.CellCount;
			int num = 0;
			int num2 = val.CellCount - 1;
			for (int i = 0; i < cellCount; i++) {
				for (int j = 0; j < cellCount; j++) {
					array5 [j, i] = 1;
				}
			}
			List<PathSegment> list2 = new List<PathSegment> ();
			List<PathLink> list3 = new List<PathLink> ();
			List<PathNode> list4 = new List<PathNode> ();
			List<PathNode> unconnectedNodeList = new List<PathNode> ();
			List<PathNode> secondaryNodeList = new List<PathNode> ();
			List<PathFinder.Point> list5 = new List<PathFinder.Point> ();
			List<PathFinder.Point> list6 = new List<PathFinder.Point> ();
			List<PathFinder.Point> list7 = new List<PathFinder.Point> ();
			foreach (DungeonGridInfo item2 in list) {
				DungeonGridInfo entrance = item2;
				TerrainPathConnect[] componentsInChildren = ((Component)entrance).GetComponentsInChildren<TerrainPathConnect> (true);
				foreach (TerrainPathConnect terrainPathConnect in componentsInChildren) {
					if (terrainPathConnect.Type != ConnectionType) {
						continue;
					}
					Vector2i val2 = val.WorldToGridCoords (((Component)terrainPathConnect).transform.position);
					if (array5 [val2.x, val2.y] == int.MaxValue) {
						continue;
					}
					PathFinder.Node stationNode = pathFinder.FindClosestWalkable (new PathFinder.Point (val2.x, val2.y), 1);
					if (stationNode == null) {
						continue;
					}
					Prefab<DungeonGridCell> prefab = ((val2.x > num) ? val [val2.x - 1, val2.y] : null);
					Prefab<DungeonGridCell> prefab2 = ((val2.x < num2) ? val [val2.x + 1, val2.y] : null);
					Prefab<DungeonGridCell> prefab3 = ((val2.y > num) ? val [val2.x, val2.y - 1] : null);
					Prefab<DungeonGridCell> prefab4 = ((val2.y < num2) ? val [val2.x, val2.y + 1] : null);
					Prefab<DungeonGridCell> prefab5 = null;
					float num3 = float.MaxValue;
					array2.Shuffle (ref seed);
					Prefab<DungeonGridCell>[] array6 = array2;
					foreach (Prefab<DungeonGridCell> prefab6 in array6) {
						if ((prefab != null && prefab6.Component.West != prefab.Component.East) || (prefab2 != null && prefab6.Component.East != prefab2.Component.West) || (prefab3 != null && prefab6.Component.South != prefab3.Component.North) || (prefab4 != null && prefab6.Component.North != prefab4.Component.South)) {
							continue;
						}
						DungeonVolume componentInChildren = prefab6.Object.GetComponentInChildren<DungeonVolume> ();
						DungeonVolume componentInChildren2 = ((Component)entrance).GetComponentInChildren<DungeonVolume> ();
						OBB bounds = componentInChildren.GetBounds (val.GridToWorldCoords (val2), Quaternion.identity);
						OBB bounds2 = componentInChildren2.GetBounds (((Component)entrance).transform.position, Quaternion.identity);
						if (!((OBB)(ref bounds)).Intersects2D (bounds2)) {
							DungeonGridLink componentInChildren3 = prefab6.Object.GetComponentInChildren<DungeonGridLink> ();
							Vector3 val3 = val.GridToWorldCoords (new Vector2i (val2.x, val2.y)) + componentInChildren3.UpSocket.localPosition;
							float num4 = Vector3Ex.Magnitude2D (((Component)terrainPathConnect).transform.position - val3);
							if (!(num3 < num4)) {
								prefab5 = prefab6;
								num3 = num4;
							}
						}
					}
					bool isStartPoint;
					if (prefab5 != null) {
						val [val2.x, val2.y] = prefab5;
						array5 [val2.x, val2.y] = int.MaxValue;
						isStartPoint = secondaryNodeList.Count == 0;
						secondaryNodeList.RemoveAll ((PathNode x) => x.node.point == stationNode.point);
						unconnectedNodeList.RemoveAll ((PathNode x) => x.node.point == stationNode.point);
						if (prefab5.Component.West != 0) {
							AddNode (val2.x - 1, val2.y);
						}
						if (prefab5.Component.East != 0) {
							AddNode (val2.x + 1, val2.y);
						}
						if (prefab5.Component.South != 0) {
							AddNode (val2.x, val2.y - 1);
						}
						if (prefab5.Component.North != 0) {
							AddNode (val2.x, val2.y + 1);
						}
						PathLink pathLink = new PathLink ();
						DungeonGridLink componentInChildren4 = ((Component)entrance).gameObject.GetComponentInChildren<DungeonGridLink> ();
						Vector3 position = ((Component)entrance).transform.position;
						Quaternion rotation = ((Component)entrance).transform.rotation;
						Vector3 eulerAngles = ((Quaternion)(ref rotation)).eulerAngles;
						DungeonGridLink componentInChildren5 = prefab5.Object.GetComponentInChildren<DungeonGridLink> ();
						Vector3 position2 = val.GridToWorldCoords (new Vector2i (val2.x, val2.y));
						Vector3 zero = Vector3.zero;
						pathLink.downwards = new PathLinkSide ();
						pathLink.downwards.origin = new PathLinkSegment ();
						pathLink.downwards.origin.position = position;
						pathLink.downwards.origin.rotation = Quaternion.Euler (eulerAngles);
						pathLink.downwards.origin.scale = Vector3.one;
						pathLink.downwards.origin.link = componentInChildren4;
						pathLink.downwards.segments = new List<PathLinkSegment> ();
						pathLink.upwards = new PathLinkSide ();
						pathLink.upwards.origin = new PathLinkSegment ();
						pathLink.upwards.origin.position = position2;
						pathLink.upwards.origin.rotation = Quaternion.Euler (zero);
						pathLink.upwards.origin.scale = Vector3.one;
						pathLink.upwards.origin.link = componentInChildren5;
						pathLink.upwards.segments = new List<PathLinkSegment> ();
						list3.Add (pathLink);
					}
					void AddNode (int x, int y)
					{
						//IL_0059: Unknown result type (might be due to invalid IL or missing references)
						PathFinder.Node node8 = pathFinder.FindClosestWalkable (new PathFinder.Point (x, y), 1);
						if (node8 != null) {
							PathNode item = new PathNode {
								monument = (Object.op_Implicit ((Object)(object)TerrainMeta.Path) ? TerrainMeta.Path.FindClosest (TerrainMeta.Path.Monuments, ((Component)entrance).transform.position) : ((Component)((Component)entrance).transform).GetComponentInParent<MonumentInfo> ()),
								node = node8
							};
							if (isStartPoint) {
								secondaryNodeList.Add (item);
							} else {
								unconnectedNodeList.Add (item);
							}
							DungeonGridConnectionHash dungeonGridConnectionHash4 = hashmap [node8.point.x, node8.point.y];
							DungeonGridConnectionHash dungeonGridConnectionHash5 = hashmap [stationNode.point.x, stationNode.point.y];
							if (node8.point.x > stationNode.point.x) {
								dungeonGridConnectionHash4.West = true;
								dungeonGridConnectionHash5.East = true;
							}
							if (node8.point.x < stationNode.point.x) {
								dungeonGridConnectionHash4.East = true;
								dungeonGridConnectionHash5.West = true;
							}
							if (node8.point.y > stationNode.point.y) {
								dungeonGridConnectionHash4.South = true;
								dungeonGridConnectionHash5.North = true;
							}
							if (node8.point.y < stationNode.point.y) {
								dungeonGridConnectionHash4.North = true;
								dungeonGridConnectionHash5.South = true;
							}
							hashmap [node8.point.x, node8.point.y] = dungeonGridConnectionHash4;
							hashmap [stationNode.point.x, stationNode.point.y] = dungeonGridConnectionHash5;
						}
					}
				}
			}
			while (unconnectedNodeList.Count != 0 || secondaryNodeList.Count != 0) {
				if (unconnectedNodeList.Count == 0) {
					PathNode node3 = secondaryNodeList [0];
					unconnectedNodeList.AddRange (secondaryNodeList.Where ((PathNode x) => (Object)(object)x.monument == (Object)(object)node3.monument));
					secondaryNodeList.RemoveAll ((PathNode x) => (Object)(object)x.monument == (Object)(object)node3.monument);
					Vector2i val4 = val.WorldToGridCoords (((Component)node3.monument).transform.position);
					pathFinder.PushPoint = new PathFinder.Point (val4.x, val4.y);
					pathFinder.PushRadius = 2;
					pathFinder.PushDistance = 2;
					pathFinder.PushMultiplier = 4;
				}
				list7.Clear ();
				list7.AddRange (unconnectedNodeList.Select ((PathNode x) => x.node.point));
				list6.Clear ();
				list6.AddRange (list4.Select ((PathNode x) => x.node.point));
				list6.AddRange (secondaryNodeList.Select ((PathNode x) => x.node.point));
				list6.AddRange (list5);
				PathFinder.Node node4 = pathFinder.FindPathUndirected (list6, list7, 100000);
				if (node4 == null) {
					PathNode node2 = unconnectedNodeList [0];
					secondaryNodeList.AddRange (unconnectedNodeList.Where ((PathNode x) => (Object)(object)x.monument == (Object)(object)node2.monument));
					unconnectedNodeList.RemoveAll ((PathNode x) => (Object)(object)x.monument == (Object)(object)node2.monument);
					secondaryNodeList.Remove (node2);
					list4.Add (node2);
					continue;
				}
				PathSegment segment = new PathSegment ();
				for (PathFinder.Node node5 = node4; node5 != null; node5 = node5.next) {
					if (node5 == node4) {
						segment.start = node5;
					}
					if (node5.next == null) {
						segment.end = node5;
					}
				}
				list2.Add (segment);
				PathNode node = unconnectedNodeList.Find ((PathNode x) => x.node.point == segment.start.point || x.node.point == segment.end.point);
				secondaryNodeList.AddRange (unconnectedNodeList.Where ((PathNode x) => (Object)(object)x.monument == (Object)(object)node.monument));
				unconnectedNodeList.RemoveAll ((PathNode x) => (Object)(object)x.monument == (Object)(object)node.monument);
				secondaryNodeList.Remove (node);
				list4.Add (node);
				PathNode pathNode = secondaryNodeList.Find ((PathNode x) => x.node.point == segment.start.point || x.node.point == segment.end.point);
				if (pathNode != null) {
					secondaryNodeList.Remove (pathNode);
					list4.Add (pathNode);
				}
				for (PathFinder.Node node6 = node4; node6 != null; node6 = node6.next) {
					if (node6 != node4 && node6.next != null) {
						list5.Add (node6.point);
					}
				}
			}
			foreach (PathSegment item3 in list2) {
				PathFinder.Node node7 = item3.start;
				while (node7 != null && node7.next != null) {
					DungeonGridConnectionHash dungeonGridConnectionHash = hashmap [node7.point.x, node7.point.y];
					DungeonGridConnectionHash dungeonGridConnectionHash2 = hashmap [node7.next.point.x, node7.next.point.y];
					if (node7.point.x > node7.next.point.x) {
						dungeonGridConnectionHash.West = true;
						dungeonGridConnectionHash2.East = true;
					}
					if (node7.point.x < node7.next.point.x) {
						dungeonGridConnectionHash.East = true;
						dungeonGridConnectionHash2.West = true;
					}
					if (node7.point.y > node7.next.point.y) {
						dungeonGridConnectionHash.South = true;
						dungeonGridConnectionHash2.North = true;
					}
					if (node7.point.y < node7.next.point.y) {
						dungeonGridConnectionHash.North = true;
						dungeonGridConnectionHash2.South = true;
					}
					hashmap [node7.point.x, node7.point.y] = dungeonGridConnectionHash;
					hashmap [node7.next.point.x, node7.next.point.y] = dungeonGridConnectionHash2;
					node7 = node7.next;
				}
			}
			for (int m = 0; m < val.CellCount; m++) {
				for (int n = 0; n < val.CellCount; n++) {
					if (array5 [m, n] == int.MaxValue) {
						continue;
					}
					DungeonGridConnectionHash dungeonGridConnectionHash3 = hashmap [m, n];
					if (dungeonGridConnectionHash3.Value == 0) {
						continue;
					}
					array.Shuffle (ref seed);
					Prefab<DungeonGridCell>[] array6 = array;
					foreach (Prefab<DungeonGridCell> prefab7 in array6) {
						Prefab<DungeonGridCell> prefab8 = ((m > num) ? val [m - 1, n] : null);
						if (((prefab8 != null) ? ((prefab7.Component.West == prefab8.Component.East) ? 1 : 0) : (dungeonGridConnectionHash3.West ? ((int)prefab7.Component.West) : ((prefab7.Component.West == DungeonGridConnectionType.None) ? 1 : 0))) == 0) {
							continue;
						}
						Prefab<DungeonGridCell> prefab9 = ((m < num2) ? val [m + 1, n] : null);
						if (((prefab9 != null) ? ((prefab7.Component.East == prefab9.Component.West) ? 1 : 0) : (dungeonGridConnectionHash3.East ? ((int)prefab7.Component.East) : ((prefab7.Component.East == DungeonGridConnectionType.None) ? 1 : 0))) == 0) {
							continue;
						}
						Prefab<DungeonGridCell> prefab10 = ((n > num) ? val [m, n - 1] : null);
						if (((prefab10 != null) ? ((prefab7.Component.South == prefab10.Component.North) ? 1 : 0) : (dungeonGridConnectionHash3.South ? ((int)prefab7.Component.South) : ((prefab7.Component.South == DungeonGridConnectionType.None) ? 1 : 0))) == 0) {
							continue;
						}
						Prefab<DungeonGridCell> prefab11 = ((n < num2) ? val [m, n + 1] : null);
						if (((prefab11 != null) ? (prefab7.Component.North == prefab11.Component.South) : (dungeonGridConnectionHash3.North ? ((byte)prefab7.Component.North != 0) : (prefab7.Component.North == DungeonGridConnectionType.None))) && (prefab7.Component.West == DungeonGridConnectionType.None || prefab8 == null || !prefab7.Component.ShouldAvoid (prefab8.ID)) && (prefab7.Component.East == DungeonGridConnectionType.None || prefab9 == null || !prefab7.Component.ShouldAvoid (prefab9.ID)) && (prefab7.Component.South == DungeonGridConnectionType.None || prefab10 == null || !prefab7.Component.ShouldAvoid (prefab10.ID)) && (prefab7.Component.North == DungeonGridConnectionType.None || prefab11 == null || !prefab7.Component.ShouldAvoid (prefab11.ID))) {
							val [m, n] = prefab7;
							bool num5 = prefab8 == null || prefab7.Component.WestVariant == prefab8.Component.EastVariant;
							bool flag = prefab10 == null || prefab7.Component.SouthVariant == prefab10.Component.NorthVariant;
							if (num5 && flag) {
								break;
							}
						}
					}
				}
			}
			Vector3 zero2 = Vector3.zero;
			Vector3 zero3 = Vector3.zero;
			Vector3 val5 = Vector3.up * 10f;
			Vector3 val6 = Vector3.up * (LinkTransition + 1f);
			Vector2i val7 = default(Vector2i);
			do {
				zero3 = zero2;
				for (int num6 = 0; num6 < val.CellCount; num6++) {
					for (int num7 = 0; num7 < val.CellCount; num7++) {
						Prefab<DungeonGridCell> prefab12 = val [num6, num7];
						if (prefab12 != null) {
							((Vector2i)(ref val7))..ctor (num6, num7);
							Vector3 val8 = val.GridToWorldCoords (val7);
							while (!prefab12.CheckEnvironmentVolumesInsideTerrain (zero2 + val8 + val5, Quaternion.identity, Vector3.one, EnvironmentType.Underground) || prefab12.CheckEnvironmentVolumes (zero2 + val8 + val6, Quaternion.identity, Vector3.one, EnvironmentType.Underground | EnvironmentType.Building) || prefab12.CheckEnvironmentVolumes (zero2 + val8, Quaternion.identity, Vector3.one, EnvironmentType.Underground | EnvironmentType.Building)) {
								zero2.y -= 9f;
							}
						}
					}
				}
			} while (zero2 != zero3);
			foreach (PathLink item4 in list3) {
				PathLinkSegment origin = item4.upwards.origin;
				origin.position += zero2;
			}
			Vector2i val9 = default(Vector2i);
			for (int num8 = 0; num8 < val.CellCount; num8++) {
				for (int num9 = 0; num9 < val.CellCount; num9++) {
					Prefab<DungeonGridCell> prefab13 = val [num8, num9];
					if (prefab13 != null) {
						((Vector2i)(ref val9))..ctor (num8, num9);
						Vector3 val10 = val.GridToWorldCoords (val9);
						World.AddPrefab ("Dungeon", prefab13, zero2 + val10, Quaternion.identity, Vector3.one);
					}
				}
			}
			Vector2i val11 = default(Vector2i);
			Vector2i val13 = default(Vector2i);
			for (int num10 = 0; num10 < val.CellCount - 1; num10++) {
				for (int num11 = 0; num11 < val.CellCount - 1; num11++) {
					Prefab<DungeonGridCell> prefab14 = val [num10, num11];
					Prefab<DungeonGridCell> prefab15 = val [num10 + 1, num11];
					Prefab<DungeonGridCell> prefab16 = val [num10, num11 + 1];
					Prefab<DungeonGridCell>[] array6;
					if (prefab14 != null && prefab15 != null && prefab14.Component.EastVariant != prefab15.Component.WestVariant) {
						array3.Shuffle (ref seed);
						array6 = array3;
						foreach (Prefab<DungeonGridCell> prefab17 in array6) {
							if (prefab17.Component.West == prefab14.Component.East && prefab17.Component.East == prefab15.Component.West && prefab17.Component.WestVariant == prefab14.Component.EastVariant && prefab17.Component.EastVariant == prefab15.Component.WestVariant) {
								((Vector2i)(ref val11))..ctor (num10, num11);
								Vector3 val12 = val.GridToWorldCoords (val11) + new Vector3 (val.CellSizeHalf, 0f, 0f);
								World.AddPrefab ("Dungeon", prefab17, zero2 + val12, Quaternion.identity, Vector3.one);
								break;
							}
						}
					}
					if (prefab14 == null || prefab16 == null || prefab14.Component.NorthVariant == prefab16.Component.SouthVariant) {
						continue;
					}
					array3.Shuffle (ref seed);
					array6 = array3;
					foreach (Prefab<DungeonGridCell> prefab18 in array6) {
						if (prefab18.Component.South == prefab14.Component.North && prefab18.Component.North == prefab16.Component.South && prefab18.Component.SouthVariant == prefab14.Component.NorthVariant && prefab18.Component.NorthVariant == prefab16.Component.SouthVariant) {
							((Vector2i)(ref val13))..ctor (num10, num11);
							Vector3 val14 = val.GridToWorldCoords (val13) + new Vector3 (0f, 0f, val.CellSizeHalf);
							World.AddPrefab ("Dungeon", prefab18, zero2 + val14, Quaternion.identity, Vector3.one);
							break;
						}
					}
				}
			}
			foreach (PathLink item5 in list3) {
				Vector3 val15 = item5.upwards.origin.position + item5.upwards.origin.rotation * Vector3.Scale (item5.upwards.origin.upSocket.localPosition, item5.upwards.origin.scale);
				Vector3 val16 = item5.downwards.origin.position + item5.downwards.origin.rotation * Vector3.Scale (item5.downwards.origin.downSocket.localPosition, item5.downwards.origin.scale) - val15;
				Vector3[] array7 = (Vector3[])(object)new Vector3[2] {
					new Vector3 (0f, 1f, 0f),
					new Vector3 (1f, 1f, 1f)
				};
				foreach (Vector3 val17 in array7) {
					int num12 = 0;
					int num13 = 0;
					while (((Vector3)(ref val16)).magnitude > 1f && (num12 < 8 || num13 < 8)) {
						bool flag2 = num12 > 2 && num13 > 2;
						bool flag3 = num12 > 4 && num13 > 4;
						Prefab<DungeonGridLink> prefab19 = null;
						Vector3 val18 = Vector3.zero;
						int num14 = int.MinValue;
						Vector3 position3 = Vector3.zero;
						Quaternion rotation2 = Quaternion.identity;
						PathLinkSegment prevSegment = item5.downwards.prevSegment;
						Vector3 val19 = prevSegment.position + prevSegment.rotation * Vector3.Scale (prevSegment.scale, prevSegment.downSocket.localPosition);
						Quaternion val20 = prevSegment.rotation * prevSegment.downSocket.localRotation;
						Prefab<DungeonGridLink>[] array8 = array4;
						foreach (Prefab<DungeonGridLink> prefab20 in array8) {
							float num15 = SeedRandom.Value (ref seed);
							DungeonGridLink component = prefab20.Component;
							if (prevSegment.downType != component.UpType) {
								continue;
							}
							switch (component.DownType) {
							case DungeonGridLinkType.Elevator:
								if (flag2 || val17.x != 0f || val17.z != 0f) {
									continue;
								}
								break;
							case DungeonGridLinkType.Transition:
								if (val17.x != 0f || val17.z != 0f) {
									continue;
								}
								break;
							}
							int num16 = ((!flag2) ? component.Priority : 0);
							if (num14 > num16) {
								continue;
							}
							Quaternion val21 = val20 * Quaternion.Inverse (component.UpSocket.localRotation);
							Quaternion val22 = val21 * component.DownSocket.localRotation;
							PathLinkSegment prevSegment2 = item5.upwards.prevSegment;
							Quaternion val23 = prevSegment2.rotation * prevSegment2.upSocket.localRotation;
							if (component.Rotation > 0) {
								if (Quaternion.Angle (val23, val22) > (float)component.Rotation) {
									continue;
								}
								Quaternion val24 = val23 * Quaternion.Inverse (val22);
								val21 *= val24;
								val22 *= val24;
							}
							Vector3 val25 = val19 - val21 * component.UpSocket.localPosition;
							Vector3 val26 = val21 * (component.DownSocket.localPosition - component.UpSocket.localPosition);
							Vector3 val27 = val16 + val18;
							Vector3 val28 = val16 + val26;
							float magnitude = ((Vector3)(ref val27)).magnitude;
							float magnitude2 = ((Vector3)(ref val28)).magnitude;
							Vector3 val29 = Vector3.Scale (val27, val17);
							Vector3 val30 = Vector3.Scale (val28, val17);
							float magnitude3 = ((Vector3)(ref val29)).magnitude;
							float magnitude4 = ((Vector3)(ref val30)).magnitude;
							if (val18 != Vector3.zero) {
								if (magnitude3 < magnitude4 || (magnitude3 == magnitude4 && magnitude < magnitude2) || (magnitude3 == magnitude4 && magnitude == magnitude2 && num15 < 0.5f)) {
									continue;
								}
							} else if (magnitude3 <= magnitude4) {
								continue;
							}
							if (Mathf.Abs (val30.x) - Mathf.Abs (val29.x) > 0.01f || (Mathf.Abs (val30.x) > 0.01f && val27.x * val28.x < 0f) || Mathf.Abs (val30.y) - Mathf.Abs (val29.y) > 0.01f || (Mathf.Abs (val30.y) > 0.01f && val27.y * val28.y < 0f) || Mathf.Abs (val30.z) - Mathf.Abs (val29.z) > 0.01f || (Mathf.Abs (val30.z) > 0.01f && val27.z * val28.z < 0f) || (flag2 && val17.x == 0f && val17.z == 0f && component.DownType == DungeonGridLinkType.Default && ((Mathf.Abs (val28.x) > 0.01f && Mathf.Abs (val28.x) < LinkRadius * 2f - 0.1f) || (Mathf.Abs (val28.z) > 0.01f && Mathf.Abs (val28.z) < LinkRadius * 2f - 0.1f)))) {
								continue;
							}
							num14 = num16;
							if (val17.x == 0f && val17.z == 0f) {
								if (!flag2 && Mathf.Abs (val28.y) < LinkTransition - 0.1f) {
									continue;
								}
							} else if ((!flag2 && magnitude4 > 0.01f && (Mathf.Abs (val28.x) < LinkRadius * 2f - 0.1f || Mathf.Abs (val28.z) < LinkRadius * 2f - 0.1f)) || (!flag3 && magnitude4 > 0.01f && (Mathf.Abs (val28.x) < LinkRadius * 1f - 0.1f || Mathf.Abs (val28.z) < LinkRadius * 1f - 0.1f))) {
								continue;
							}
							if (!flag2 || !(magnitude4 < 0.01f) || !(magnitude2 < 0.01f) || !(Quaternion.Angle (val23, val22) > 10f)) {
								prefab19 = prefab20;
								val18 = val26;
								num14 = num16;
								position3 = val25;
								rotation2 = val21;
							}
						}
						if (val18 != Vector3.zero) {
							PathLinkSegment pathLinkSegment = new PathLinkSegment ();
							pathLinkSegment.position = position3;
							pathLinkSegment.rotation = rotation2;
							pathLinkSegment.scale = Vector3.one;
							pathLinkSegment.prefab = prefab19;
							pathLinkSegment.link = prefab19.Component;
							item5.downwards.segments.Add (pathLinkSegment);
							val16 += val18;
						} else {
							num13++;
						}
						if (val17.x > 0f || val17.z > 0f) {
							Prefab<DungeonGridLink> prefab21 = null;
							Vector3 val31 = Vector3.zero;
							int num17 = int.MinValue;
							Vector3 position4 = Vector3.zero;
							Quaternion rotation3 = Quaternion.identity;
							PathLinkSegment prevSegment3 = item5.upwards.prevSegment;
							Vector3 val32 = prevSegment3.position + prevSegment3.rotation * Vector3.Scale (prevSegment3.scale, prevSegment3.upSocket.localPosition);
							Quaternion val33 = prevSegment3.rotation * prevSegment3.upSocket.localRotation;
							array8 = array4;
							foreach (Prefab<DungeonGridLink> prefab22 in array8) {
								float num18 = SeedRandom.Value (ref seed);
								DungeonGridLink component2 = prefab22.Component;
								if (prevSegment3.upType != component2.DownType) {
									continue;
								}
								switch (component2.DownType) {
								case DungeonGridLinkType.Elevator:
									if (flag2 || val17.x != 0f || val17.z != 0f) {
										continue;
									}
									break;
								case DungeonGridLinkType.Transition:
									if (val17.x != 0f || val17.z != 0f) {
										continue;
									}
									break;
								}
								int num19 = ((!flag2) ? component2.Priority : 0);
								if (num17 > num19) {
									continue;
								}
								Quaternion val34 = val33 * Quaternion.Inverse (component2.DownSocket.localRotation);
								Quaternion val35 = val34 * component2.UpSocket.localRotation;
								PathLinkSegment prevSegment4 = item5.downwards.prevSegment;
								Quaternion val36 = prevSegment4.rotation * prevSegment4.downSocket.localRotation;
								if (component2.Rotation > 0) {
									if (Quaternion.Angle (val36, val35) > (float)component2.Rotation) {
										continue;
									}
									Quaternion val37 = val36 * Quaternion.Inverse (val35);
									val34 *= val37;
									val35 *= val37;
								}
								Vector3 val38 = val32 - val34 * component2.DownSocket.localPosition;
								Vector3 val39 = val34 * (component2.UpSocket.localPosition - component2.DownSocket.localPosition);
								Vector3 val40 = val16 - val31;
								Vector3 val41 = val16 - val39;
								float magnitude5 = ((Vector3)(ref val40)).magnitude;
								float magnitude6 = ((Vector3)(ref val41)).magnitude;
								Vector3 val42 = Vector3.Scale (val40, val17);
								Vector3 val43 = Vector3.Scale (val41, val17);
								float magnitude7 = ((Vector3)(ref val42)).magnitude;
								float magnitude8 = ((Vector3)(ref val43)).magnitude;
								if (val31 != Vector3.zero) {
									if (magnitude7 < magnitude8 || (magnitude7 == magnitude8 && magnitude5 < magnitude6) || (magnitude7 == magnitude8 && magnitude5 == magnitude6 && num18 < 0.5f)) {
										continue;
									}
								} else if (magnitude7 <= magnitude8) {
									continue;
								}
								if (Mathf.Abs (val43.x) - Mathf.Abs (val42.x) > 0.01f || (Mathf.Abs (val43.x) > 0.01f && val40.x * val41.x < 0f) || Mathf.Abs (val43.y) - Mathf.Abs (val42.y) > 0.01f || (Mathf.Abs (val43.y) > 0.01f && val40.y * val41.y < 0f) || Mathf.Abs (val43.z) - Mathf.Abs (val42.z) > 0.01f || (Mathf.Abs (val43.z) > 0.01f && val40.z * val41.z < 0f) || (flag2 && val17.x == 0f && val17.z == 0f && component2.UpType == DungeonGridLinkType.Default && ((Mathf.Abs (val41.x) > 0.01f && Mathf.Abs (val41.x) < LinkRadius * 2f - 0.1f) || (Mathf.Abs (val41.z) > 0.01f && Mathf.Abs (val41.z) < LinkRadius * 2f - 0.1f)))) {
									continue;
								}
								num17 = num19;
								if (val17.x == 0f && val17.z == 0f) {
									if (!flag2 && Mathf.Abs (val41.y) < LinkTransition - 0.1f) {
										continue;
									}
								} else if ((!flag2 && magnitude8 > 0.01f && (Mathf.Abs (val41.x) < LinkRadius * 2f - 0.1f || Mathf.Abs (val41.z) < LinkRadius * 2f - 0.1f)) || (!flag3 && magnitude8 > 0.01f && (Mathf.Abs (val41.x) < LinkRadius * 1f - 0.1f || Mathf.Abs (val41.z) < LinkRadius * 1f - 0.1f))) {
									continue;
								}
								if (!flag2 || !(magnitude8 < 0.01f) || !(magnitude6 < 0.01f) || !(Quaternion.Angle (val36, val35) > 10f)) {
									prefab21 = prefab22;
									val31 = val39;
									num17 = num19;
									position4 = val38;
									rotation3 = val34;
								}
							}
							if (val31 != Vector3.zero) {
								PathLinkSegment pathLinkSegment2 = new PathLinkSegment ();
								pathLinkSegment2.position = position4;
								pathLinkSegment2.rotation = rotation3;
								pathLinkSegment2.scale = Vector3.one;
								pathLinkSegment2.prefab = prefab21;
								pathLinkSegment2.link = prefab21.Component;
								item5.upwards.segments.Add (pathLinkSegment2);
								val16 -= val31;
							} else {
								num12++;
							}
						} else {
							num12++;
						}
					}
				}
			}
			foreach (PathLink item6 in list3) {
				foreach (PathLinkSegment segment2 in item6.downwards.segments) {
					World.AddPrefab ("Dungeon", segment2.prefab, segment2.position, segment2.rotation, segment2.scale);
				}
				foreach (PathLinkSegment segment3 in item6.upwards.segments) {
					World.AddPrefab ("Dungeon", segment3.prefab, segment3.position, segment3.rotation, segment3.scale);
				}
			}
			if (Object.op_Implicit ((Object)(object)TerrainMeta.Path)) {
				TerrainMeta.Path.DungeonGridRoot = HierarchyUtil.GetRoot ("Dungeon");
			}
		}
	}
}
