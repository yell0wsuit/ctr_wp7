using ctr_wp7.ios;

namespace ctr_wp7.iframework.visual
{
    internal sealed class FormattedString : NSObject
    {
        public FormattedString initWithStringAndWidth(NSString str, float w)
        {
            if (init() != null)
            {
                string_ = (NSString)NSRET(str);
                width = w;
            }
            return this;
        }

        public override void dealloc()
        {
            string_ = null;
            base.dealloc();
        }

        public NSString string_;

        public float width;
    }
}
