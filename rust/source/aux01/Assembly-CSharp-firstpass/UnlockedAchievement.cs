using System;
using Epic.OnlineServices;
using Epic.OnlineServices.Achievements;

public struct UnlockedAchievement
{
	public Utf8String AchievementId { get; set; }

	public DateTimeOffset? UnlockTime { get; set; }

	internal void Set (ref UnlockedAchievementInternal other)
	{
		AchievementId = other.AchievementId;
		UnlockTime = other.UnlockTime;
	}
}
