using ctr_wp7.iframework;
using ctr_wp7.iframework.core;
using ctr_wp7.ios;

namespace ctr_wp7.game.remotedata
{
    internal sealed class VideoDataManager
    {
        public static void initVideoDataManager()
        {
            videoDataMgr = new ctr_wp7.remotedata.cartoons.VideoDataManager();
        }

        public static void init()
        {
            if (videoDataMgr != null)
            {
                string text = "ctr";
                NSString @string = ApplicationSettings.getString(8);
                if (@string.isEqualToString("zh"))
                {
                    text += "_zh";
                }
                videoDataMgr.initWith(text, (int)FrameworkTypes.CHOOSE3(0.0, 1.0, 2.0));
            }
        }

        public static BlockConfig getBlockConfig()
        {
            if (videoDataMgr != null)
            {
                ctr_wp7.remotedata.cartoons.BlockConfig blockConfig = videoDataMgr.getBlockConfig();
                return (BlockConfig)new BlockConfig().initWithJObject(blockConfig);
            }
            return null;
        }

        public static int getLastActivated()
        {
            return lastActivated;
        }

        public static void setLastActivated(int last)
        {
            lastActivated = last;
        }

        public static void request()
        {
            if (videoDataMgr != null)
            {
                string text = "ctr";
                NSString @string = ApplicationSettings.getString(8);
                if (@string.isEqualToString("zh"))
                {
                }
            }
        }

        public static void clear()
        {
            videoDataMgr?.clear();
        }

        private static int lastActivated = -1;

        private static ctr_wp7.remotedata.cartoons.VideoDataManager videoDataMgr;
    }
}
