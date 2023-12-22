using System;
using System.IO;
using UnityEngine.Profiling.Memory.Experimental;

[Factory ("memsnap")]
public class MemSnap : ConsoleSystem
{
	private static string NeedProfileFolder ()
	{
		string path = "profile";
		if (!Directory.Exists (path)) {
			return Directory.CreateDirectory (path).FullName;
		}
		return new DirectoryInfo (path).FullName;
	}

	[ClientVar]
	[ServerVar]
	public static void managed (Arg arg)
	{
		string text = NeedProfileFolder ();
		string path = text + "/memdump-" + DateTime.Now.ToString ("MM-dd-yyyy-h-mm-ss") + ".snap";
		MemoryProfiler.TakeSnapshot (path, null, CaptureFlags.ManagedObjects);
	}

	[ClientVar]
	[ServerVar]
	public static void native (Arg arg)
	{
		string text = NeedProfileFolder ();
		string path = text + "/memdump-" + DateTime.Now.ToString ("MM-dd-yyyy-h-mm-ss") + ".snap";
		MemoryProfiler.TakeSnapshot (path, null, CaptureFlags.NativeObjects);
	}

	[ClientVar]
	[ServerVar]
	public static void full (Arg arg)
	{
		string text = NeedProfileFolder ();
		string path = text + "/memdump-" + DateTime.Now.ToString ("MM-dd-yyyy-h-mm-ss") + ".snap";
		MemoryProfiler.TakeSnapshot (path, null, CaptureFlags.ManagedObjects | CaptureFlags.NativeObjects | CaptureFlags.NativeAllocations | CaptureFlags.NativeAllocationSites | CaptureFlags.NativeStackTraces);
	}
}
