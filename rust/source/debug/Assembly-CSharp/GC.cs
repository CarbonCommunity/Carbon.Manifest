using System;
using UnityEngine;

public class GC : MonoBehaviour, IClientComponent
{
	public static bool Enabled => true;

	public static void Collect ()
	{
		GC.Collect ();
	}

	public static long GetTotalMemory ()
	{
		return GC.GetTotalMemory (forceFullCollection: false) / 1048576;
	}

	public static int CollectionCount ()
	{
		return GC.CollectionCount (0);
	}
}
