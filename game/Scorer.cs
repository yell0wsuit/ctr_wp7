using System;
using ctre_wp7.ctr_original;
using ctre_wp7.iframework.core;
using ctre_wp7.ios;

namespace ctre_wp7.game
{
	// Token: 0x0200008B RID: 139
	internal class Scorer
	{
		// Token: 0x06000400 RID: 1024 RVA: 0x0001C974 File Offset: 0x0001AB74
		public static void postLeaderboardResultforLaderboardIdlowestValFirstforGameCenter(int boxScore, int level, bool islowestValFirstforGameCenter)
		{
			if (CTRPreferences.isLiteVersion())
			{
				return;
			}
			if (App.NeedsUpdate || App.UpdateHandled)
			{
				return;
			}
			GamePage.MainPage.PostLeaderboard(level, boxScore);
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x0001C999 File Offset: 0x0001AB99
		public static void postAchievementName(NSString name)
		{
			if (!Preferences._getBooleanForKey(name.ToString()))
			{
				if (CTRPreferences.isLiteVersion())
				{
					return;
				}
				if (App.NeedsUpdate || App.UpdateHandled)
				{
					return;
				}
				GamePage.MainPage.AwardAchievement(name.ToString());
			}
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x0001C9CF File Offset: 0x0001ABCF
		public static void activateScorerUIAtProfile()
		{
		}
	}
}
