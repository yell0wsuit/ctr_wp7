using ctr_wp7.game;
using ctr_wp7.iframework.core;
using ctr_wp7.iframework.visual;

namespace ctr_wp7.Specials
{
    // Token: 0x02000045 RID: 69
    internal class Factory
    {
        // Token: 0x06000241 RID: 577 RVA: 0x0000EFD0 File Offset: 0x0000D1D0
        public static void showProcessingOnViewwithTouchesBlocking(View v, bool b)
        {
            Processing processing = (Processing)new Processing().initWithTouchesBlocking(b);
            processing.setName("processing");
            _ = v.addChild(processing);
        }

        // Token: 0x06000242 RID: 578 RVA: 0x0000F004 File Offset: 0x0000D204
        public static void hideProcessingFromView(View v)
        {
            BaseElement childWithName = v.getChildWithName("processing");
            if (childWithName != null)
            {
                v.removeChild(childWithName);
            }
        }
    }
}
