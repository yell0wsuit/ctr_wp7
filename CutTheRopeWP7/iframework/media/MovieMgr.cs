using ctr_wp7.ios;

using Microsoft.Xna.Framework.Media;

namespace ctr_wp7.iframework.media
{
    internal sealed class MovieMgr : NSObject
    {
        public void playURL(NSString moviePath, bool mute)
        {
            bool flag = !MediaPlayer.GameHasControl && MediaPlayer.State == MediaState.Playing;
            GamePage.PlayMovie(moviePath, mute, delegateMovieMgrDelegate, flag);
        }

        public NSString url;

        public MovieMgrDelegate delegateMovieMgrDelegate;
    }
}
