using ctr_wp7.ios;

namespace ctr_wp7.iframework.visual
{
    // Token: 0x020000CC RID: 204
    internal class FormattedString : NSObject
    {
        // Token: 0x060005ED RID: 1517 RVA: 0x0002CBC1 File Offset: 0x0002ADC1
        public virtual FormattedString initWithStringAndWidth(NSString str, float w)
        {
            if (base.init() != null)
            {
                this.string_ = (NSString)NSObject.NSRET(str);
                this.width = w;
            }
            return this;
        }

        // Token: 0x060005EE RID: 1518 RVA: 0x0002CBE4 File Offset: 0x0002ADE4
        public override void dealloc()
        {
            this.string_ = null;
            base.dealloc();
        }

        // Token: 0x04000B40 RID: 2880
        public NSString string_;

        // Token: 0x04000B41 RID: 2881
        public float width;
    }
}
