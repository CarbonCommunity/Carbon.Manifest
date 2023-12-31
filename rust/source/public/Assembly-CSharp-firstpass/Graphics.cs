using System;
using System.Runtime.InteropServices;
using System.Security;
using UnityEngine;

[SuppressUnmanagedCodeSecurity]
public static class Graphics
{
	public static class BufferReadback
	{
		[DllImport ("Renderer", EntryPoint = "GPU_BufferReadback_CreateForTexture")]
		public static extern IntPtr CreateForTexture (IntPtr tex, uint width, uint height, uint format);

		[DllImport ("Renderer", EntryPoint = "GPU_BufferReadback_CreateForBuffer")]
		public static extern IntPtr CreateForBuffer (IntPtr buf, uint size);

		[DllImport ("Renderer", EntryPoint = "GPU_BufferReadback_Destroy")]
		public static extern void Destroy (IntPtr inst);

		[DllImport ("Renderer", EntryPoint = "GPU_BufferReadback_IssueRead")]
		public static extern void IssueRead (IntPtr inst);

		[DllImport ("Renderer", EntryPoint = "GPU_BufferReadback_GetDataSize")]
		public static extern uint GetDataSize (IntPtr inst);

		[DllImport ("Renderer", EntryPoint = "GPU_BufferReadback_GetDataStride")]
		public static extern uint GetDataStride (IntPtr inst);

		[DllImport ("Renderer", EntryPoint = "GPU_BufferReadback_GetData")]
		public static extern void GetData (IntPtr inst, ref byte data);

		[DllImport ("Renderer", EntryPoint = "GPU_BufferReadback_GetData")]
		public static extern void GetData (IntPtr inst, ref Color32 data);

		[DllImport ("Renderer", EntryPoint = "GPU_BufferReadback_GetData")]
		public static extern void GetData (IntPtr inst, ref float data);
	}

	[DllImport ("Renderer")]
	public static extern IntPtr GetRenderEventFunc ();
}
