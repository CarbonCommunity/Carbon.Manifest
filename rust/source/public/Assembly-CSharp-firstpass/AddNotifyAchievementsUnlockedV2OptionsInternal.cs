using System;
using System.Runtime.InteropServices;
using Epic.OnlineServices;
using Epic.OnlineServices.Achievements;

[StructLayout (LayoutKind.Sequential, Pack = 8)]
internal struct AddNotifyAchievementsUnlockedV2OptionsInternal : ISettable<AddNotifyAchievementsUnlockedV2Options>, IDisposable
{
	private int m_ApiVersion;

	public void Set (ref AddNotifyAchievementsUnlockedV2Options other)
	{
		m_ApiVersion = 2;
	}

	public void Set (ref AddNotifyAchievementsUnlockedV2Options? other)
	{
		if (other.HasValue) {
			m_ApiVersion = 2;
		}
	}

	public void Dispose ()
	{
	}
}
