using ctr_wp7.iframework.media;
using ctr_wp7.ios;

namespace ctr_wp7
{
    internal sealed class GamePage
    {
        public static GamePage MainPage { get; } = new();

        internal static void PlayMovie(NSString path, bool mute, MovieMgrDelegate delegateMovieMgr, bool resumeMusicAfterOnVideoEnds)
        {
            delegateMovieMgr?.moviePlaybackFinished(path);
        }

        public static void AwardAchievement(string name)
        {
        }

        public static void PostLeaderboard(int pack, int score)
        {
        }
    }
}
