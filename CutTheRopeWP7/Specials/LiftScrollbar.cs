using ctr_wp7.ctr_commons;
using ctr_wp7.iframework.core;
using ctr_wp7.iframework.visual;
using ctr_wp7.ios;

namespace ctr_wp7.Specials
{
    internal sealed class LiftScrollbar : Image
    {
        public static LiftScrollbar createWithResIDBackQuadLiftQuadLiftQuadPressed(int resID, int bq, int lq, int lqp)
        {
            return (LiftScrollbar)new LiftScrollbar().initWithResIDBackQuadLiftQuadLiftQuadPressed(resID, bq, lq, lqp);
        }

        public NSObject initWithResIDBackQuadLiftQuadLiftQuadPressed(int resID, int bq, int lq, int lqp)
        {
            if (initWithTexture(Application.getTexture(resID)) != null)
            {
                setDrawQuad(bq);
                Image image = Image_createWithResIDQuad(resID, lq);
                Image image2 = Image_createWithResIDQuad(resID, lqp);
                lift = (Lift)new Lift().initWithUpElementDownElementandID(image, image2, 0);
                lift.parentAnchor = 10;
                lift.anchor = 18;
                lift.maxY = height;
                lift.liftDelegate = new Lift.PercentXY(percentXY);
                _ = addChild(lift);
            }
            return this;
        }

        public void percentXY(float px, float py)
        {
            Vector maxScroll = container.getMaxScroll();
            container.setScroll(vect(maxScroll.x * px, maxScroll.y * py));
        }

        public override void update(float delta)
        {
            base.update(delta);
            Vector scroll = container.getScroll();
            Vector maxScroll = container.getMaxScroll();
            float num = 0f;
            float num2 = 0f;
            if (maxScroll.x != 0f)
            {
                num = scroll.x / maxScroll.x;
            }
            if (maxScroll.y != 0f)
            {
                num2 = scroll.y / maxScroll.y;
            }
            lift.x = ((lift.maxX - lift.minX) * num) + lift.minX;
            lift.y = ((lift.maxY - lift.minY) * num2) + lift.minY;
        }

        public override void dealloc()
        {
            container = null;
            base.dealloc();
        }

        public override bool onTouchUpXY(float tx, float ty)
        {
            bool flag = base.onTouchUpXY(tx, ty);
            container.startMovingToSpointInDirection(vectZero);
            return flag;
        }

        private Lift lift;

        public ScrollableContainer container;
    }
}
