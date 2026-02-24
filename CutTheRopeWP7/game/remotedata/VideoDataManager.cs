using ctr_wp7.iframework;
using ctr_wp7.iframework.core;
using ctr_wp7.ios;

namespace ctr_wp7.game.remotedata
{
    // Token: 0x020000DA RID: 218
    internal class VideoDataManager
    {
        // Token: 0x06000653 RID: 1619 RVA: 0x00030952 File Offset: 0x0002EB52
        public static void initVideoDataManager()
        {
            videoDataMgr = new ctr_wp7.remotedata.cartoons.VideoDataManager();
        }

        // Token: 0x06000654 RID: 1620 RVA: 0x00030960 File Offset: 0x0002EB60
        public static void init()
        {
            if (videoDataMgr != null)
            {
                string text = "ctr";
                NSString @string = Application.sharedAppSettings().getString(8);
                if (@string.isEqualToString("zh"))
                {
                    text += "_zh";
                }
                videoDataMgr.initWith(text, (int)FrameworkTypes.CHOOSE3(0.0, 1.0, 2.0));
            }
        }

        // Token: 0x06000655 RID: 1621 RVA: 0x000309CC File Offset: 0x0002EBCC
        public static BlockConfig getBlockConfig()
        {
            if (videoDataMgr != null)
            {
                ctr_wp7.remotedata.cartoons.BlockConfig blockConfig = videoDataMgr.getBlockConfig();
                return (BlockConfig)new BlockConfig().initWithJObject(blockConfig);
            }
            return null;
        }

        // Token: 0x06000656 RID: 1622 RVA: 0x000309FF File Offset: 0x0002EBFF
        public static int getLastActivated()
        {
            return lastActivated;
        }

        // Token: 0x06000657 RID: 1623 RVA: 0x00030A06 File Offset: 0x0002EC06
        public static void setLastActivated(int last)
        {
            lastActivated = last;
        }

        // Token: 0x06000658 RID: 1624 RVA: 0x00030A10 File Offset: 0x0002EC10
        public static void request()
        {
            if (videoDataMgr != null)
            {
                string text = "ctr";
                NSString @string = Application.sharedAppSettings().getString(8);
                if (@string.isEqualToString("zh"))
                {
                }
            }
        }

        // Token: 0x06000659 RID: 1625 RVA: 0x00030A4F File Offset: 0x0002EC4F
        public static void clear()
        {
            if (videoDataMgr != null)
            {
                videoDataMgr.clear();
            }
        }

        // Token: 0x04000BC4 RID: 3012
        protected static int lastActivated = -1;

        // Token: 0x04000BC5 RID: 3013
        protected static ctr_wp7.remotedata.cartoons.VideoDataManager videoDataMgr;
    }
}
