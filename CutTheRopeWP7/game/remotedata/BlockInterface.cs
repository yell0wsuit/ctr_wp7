using ctr_wp7.ios;

namespace ctr_wp7.game.remotedata
{
    internal abstract class BlockInterface : NSObject
    {
        public abstract int getType();

        public abstract NSString getName();

        public abstract NSString getId();

        public abstract NSString getUrl();

        public abstract NSString getText();

        public abstract NSString getNumber();

        public abstract bool isImageExists();

        public abstract bool isImageReady();

        public enum BLOCKTYPE
        {
            AD,
            EPISODE
        }
    }
}
