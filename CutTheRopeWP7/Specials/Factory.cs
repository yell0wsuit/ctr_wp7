using ctr_wp7.game;
using ctr_wp7.iframework.core;
using ctr_wp7.iframework.visual;

namespace ctr_wp7.Specials
{
    internal sealed class Factory
    {
        public static void showProcessingOnViewwithTouchesBlocking(View v, bool b)
        {
            Processing processing = (Processing)new Processing().initWithTouchesBlocking(b);
            processing.setName("processing");
            _ = v.addChild(processing);
        }

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
