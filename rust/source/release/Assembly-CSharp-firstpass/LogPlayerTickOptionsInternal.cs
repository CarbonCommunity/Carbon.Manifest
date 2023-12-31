using System;
using System.Runtime.InteropServices;
using Epic.OnlineServices.AntiCheatCommon;

[StructLayout (LayoutKind.Sequential, Pack = 8)]
internal struct LogPlayerTickOptionsInternal
{
	public int m_ApiVersion;

	public IntPtr m_PlayerHandle;

	public IntPtr m_PlayerPosition;

	public IntPtr m_PlayerViewRotation;

	public int m_IsPlayerViewZoomed;

	public float m_PlayerHealth;

	public AntiCheatCommonPlayerMovementState m_PlayerMovementState;
}
