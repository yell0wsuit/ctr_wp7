using ctre_wp7.iframework.media;
using ctre_wp7.ios;

namespace ctre_wp7
{
    internal sealed class GamePage
    {
        public static GamePage MainPage { get; } = new();

        internal void PlayMovie(NSString path, bool mute, MovieMgrDelegate delegateMovieMgr, bool resumeMusicAfterOnVideoEnds)
        {
            delegateMovieMgr?.moviePlaybackFinished(path);
        }

        public void AwardAchievement(string name)
        {
        }

        public void PostLeaderboard(int pack, int score)
        {
        }
    }
}
