using System;

using ctr_wp7.ios;

namespace ctr_wp7.game.remotedata
{
    // Token: 0x020000FA RID: 250
    internal abstract class BlockInterface : NSObject
    {
        // Token: 0x0600078F RID: 1935
        public abstract int getType();

        // Token: 0x06000790 RID: 1936
        public abstract NSString getName();

        // Token: 0x06000791 RID: 1937
        public abstract NSString getId();

        // Token: 0x06000792 RID: 1938
        public abstract NSString getUrl();

        // Token: 0x06000793 RID: 1939
        public abstract NSString getText();

        // Token: 0x06000794 RID: 1940
        public abstract NSString getNumber();

        // Token: 0x06000795 RID: 1941
        public abstract bool isImageExists();

        // Token: 0x06000796 RID: 1942
        public abstract bool isImageReady();

        // Token: 0x020000FB RID: 251
        public enum BLOCKTYPE
        {
            // Token: 0x04000D04 RID: 3332
            AD,
            // Token: 0x04000D05 RID: 3333
            EPISODE
        }
    }
}
