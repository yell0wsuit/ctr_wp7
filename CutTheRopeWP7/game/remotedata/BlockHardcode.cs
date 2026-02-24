using ctr_wp7.iframework.core;
using ctr_wp7.ios;

namespace ctr_wp7.game.remotedata
{
    // Token: 0x020000FD RID: 253
    internal sealed class BlockHardcode : BlockInterface
    {
        // Token: 0x060007A3 RID: 1955 RVA: 0x0003C35A File Offset: 0x0003A55A
        public override int getType()
        {
            return 1;
        }

        // Token: 0x060007A4 RID: 1956 RVA: 0x0003C35D File Offset: 0x0003A55D
        public override NSString getName()
        {
            return null;
        }

        // Token: 0x060007A5 RID: 1957 RVA: 0x0003C360 File Offset: 0x0003A560
        public override NSString getId()
        {
            return NSS("HARDCODED_EPISODE");
        }

        // Token: 0x060007A6 RID: 1958 RVA: 0x0003C36C File Offset: 0x0003A56C
        public override NSString getUrl()
        {
            if (!Application.sharedAppSettings().getString(8).isEqualToString(NSS("zh")))
            {
                return NSS("vnd.youtube:bj3cbCE56wQ?vndapp=youtube_mobile");
            }
            return NSS("http://v.youku.com/v_show/id_XNDIwMTM4Mjcy.html");
        }

        // Token: 0x060007A7 RID: 1959 RVA: 0x0003C3AC File Offset: 0x0003A5AC
        public override NSString getText()
        {
            return null;
        }

        // Token: 0x060007A8 RID: 1960 RVA: 0x0003C3AF File Offset: 0x0003A5AF
        public override NSString getNumber()
        {
            return NSS("1");
        }

        // Token: 0x060007A9 RID: 1961 RVA: 0x0003C3BB File Offset: 0x0003A5BB
        public override bool isImageExists()
        {
            return false;
        }

        // Token: 0x060007AA RID: 1962 RVA: 0x0003C3BE File Offset: 0x0003A5BE
        public override bool isImageReady()
        {
            return false;
        }
    }
}
