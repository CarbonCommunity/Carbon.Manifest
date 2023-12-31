using System.Collections.Generic;
using UnityEngine.Serialization;

public class PlacePowerlineObjects : ProceduralComponent
{
	public PathList.BasicObject[] Start;

	public PathList.BasicObject[] End;

	public PathList.SideObject[] Side;

	[FormerlySerializedAs ("PowerlineObjects")]
	public PathList.PathObject[] Path;

	public override void Process (uint seed)
	{
		List<PathList> powerlines = TerrainMeta.Path.Powerlines;
		if (World.Networked) {
			foreach (PathList item in powerlines) {
				World.Spawn (item.Name, "assets/bundled/prefabs/autospawn/");
			}
			return;
		}
		foreach (PathList item2 in powerlines) {
			PathList.BasicObject[] start = Start;
			foreach (PathList.BasicObject obj in start) {
				item2.TrimStart (obj);
			}
			PathList.BasicObject[] end = End;
			foreach (PathList.BasicObject obj2 in end) {
				item2.TrimEnd (obj2);
			}
			PathList.BasicObject[] start2 = Start;
			foreach (PathList.BasicObject obj3 in start2) {
				item2.SpawnStart (ref seed, obj3);
			}
			PathList.BasicObject[] end2 = End;
			foreach (PathList.BasicObject obj4 in end2) {
				item2.SpawnEnd (ref seed, obj4);
			}
			PathList.PathObject[] path = Path;
			foreach (PathList.PathObject obj5 in path) {
				item2.SpawnAlong (ref seed, obj5);
			}
			PathList.SideObject[] side = Side;
			foreach (PathList.SideObject obj6 in side) {
				item2.SpawnSide (ref seed, obj6);
			}
			item2.ResetTrims ();
		}
	}
}
