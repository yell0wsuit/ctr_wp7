using ctr_wp7.ios;
using ctr_wp7.remotedata.cartoons;

namespace ctr_wp7.game.remotedata
{
    // Token: 0x020000FC RID: 252
    internal class BlockInternet : BlockInterface
    {
        // Token: 0x06000798 RID: 1944 RVA: 0x0003C21B File Offset: 0x0003A41B
        public virtual NSObject initWithJObject(Block pBlock)
        {
            if (base.init() != null)
            {
                jblock = pBlock;
            }
            return this;
        }

        // Token: 0x06000799 RID: 1945 RVA: 0x0003C230 File Offset: 0x0003A430
        public override int getType()
        {
            string type = jblock.getType();
            if (type == "adblock")
            {
                return 0;
            }
            return 1;
        }

        // Token: 0x0600079A RID: 1946 RVA: 0x0003C25C File Offset: 0x0003A45C
        public override NSString getName()
        {
            string name = jblock.getName();
            return (name == null) ? null : NSObject.NSS(name);
        }

        // Token: 0x0600079B RID: 1947 RVA: 0x0003C284 File Offset: 0x0003A484
        public override NSString getId()
        {
            string id = jblock.getId();
            return NSObject.NSS(id);
        }

        // Token: 0x0600079C RID: 1948 RVA: 0x0003C2A8 File Offset: 0x0003A4A8
        public override NSString getUrl()
        {
            string url = jblock.getUrl();
            return NSObject.NSS(url);
        }

        // Token: 0x0600079D RID: 1949 RVA: 0x0003C2CC File Offset: 0x0003A4CC
        public override NSString getText()
        {
            string text = jblock.getText();
            return NSObject.NSS(text);
        }

        // Token: 0x0600079E RID: 1950 RVA: 0x0003C2F0 File Offset: 0x0003A4F0
        public override NSString getNumber()
        {
            string number = jblock.getNumber();
            return NSObject.NSS(number);
        }

        // Token: 0x0600079F RID: 1951 RVA: 0x0003C314 File Offset: 0x0003A514
        public override bool isImageExists()
        {
            return jblock.isImageExists();
        }

        // Token: 0x060007A0 RID: 1952 RVA: 0x0003C330 File Offset: 0x0003A530
        public override bool isImageReady()
        {
            return jblock.isImageReady();
        }

        // Token: 0x060007A1 RID: 1953 RVA: 0x0003C34A File Offset: 0x0003A54A
        public override void dealloc()
        {
            base.dealloc();
        }

        // Token: 0x04000D06 RID: 3334
        protected Block jblock;
    }
}
