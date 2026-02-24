using ctr_wp7.iframework.core;
using ctr_wp7.ios;

namespace ctr_wp7.game.remotedata
{
    internal sealed class BlockHardcode : BlockInterface
    {
        public override int getType()
        {
            return 1;
        }

        public override NSString getName()
        {
            return null;
        }

        public override NSString getId()
        {
            return NSS("HARDCODED_EPISODE");
        }

        public override NSString getUrl()
        {
            return !ApplicationSettings.getString(8).isEqualToString(NSS("zh"))
                ? NSS("vnd.youtube:bj3cbCE56wQ?vndapp=youtube_mobile")
                : NSS("http://v.youku.com/v_show/id_XNDIwMTM4Mjcy.html");
        }

        public override NSString getText()
        {
            return null;
        }

        public override NSString getNumber()
        {
            return NSS("1");
        }

        public override bool isImageExists()
        {
            return false;
        }

        public override bool isImageReady()
        {
            return false;
        }
    }
}
