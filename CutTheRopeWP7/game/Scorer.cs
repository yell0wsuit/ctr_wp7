using ctr_wp7.ctr_original;
using ctr_wp7.iframework.core;
using ctr_wp7.ios;

namespace ctr_wp7.game
{
    internal sealed class Scorer
    {
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
            GamePage.PostLeaderboard(level, boxScore);
        }

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
                GamePage.AwardAchievement(name.ToString());
            }
        }

        public static void activateScorerUIAtProfile()
        {
        }
    }
}
