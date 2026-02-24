using ctr_wp7.ios;

using Microsoft.Xna.Framework.Media;

namespace ctr_wp7.iframework.media
{
    // Token: 0x0200004F RID: 79
    internal class MovieMgr : NSObject
    {
        // Token: 0x06000275 RID: 629 RVA: 0x0000FE1C File Offset: 0x0000E01C
        public void playURL(NSString moviePath, bool mute)
        {
            bool flag = !MediaPlayer.GameHasControl && MediaPlayer.State == MediaState.Playing;
            GamePage.MainPage.PlayMovie(moviePath, mute, delegateMovieMgrDelegate, flag);
        }

        // Token: 0x04000868 RID: 2152
        public NSString url;

        // Token: 0x04000869 RID: 2153
        public MovieMgrDelegate delegateMovieMgrDelegate;
    }
}
