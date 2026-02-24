using System;

using ctr_wp7.ctr_commons;
using ctr_wp7.iframework.core;
using ctr_wp7.iframework.helpers;
using ctr_wp7.iframework.visual;
using ctr_wp7.ios;

namespace ctr_wp7.Specials
{
    // Token: 0x0200005D RID: 93
    internal class LiftScrollbar : Image
    {
        // Token: 0x060002CA RID: 714 RVA: 0x00011FD4 File Offset: 0x000101D4
        public static LiftScrollbar createWithResIDBackQuadLiftQuadLiftQuadPressed(int resID, int bq, int lq, int lqp)
        {
            return (LiftScrollbar)new LiftScrollbar().initWithResIDBackQuadLiftQuadLiftQuadPressed(resID, bq, lq, lqp);
        }

        // Token: 0x060002CB RID: 715 RVA: 0x00011FEC File Offset: 0x000101EC
        public virtual NSObject initWithResIDBackQuadLiftQuadLiftQuadPressed(int resID, int bq, int lq, int lqp)
        {
            if (base.initWithTexture(Application.getTexture(resID)) != null)
            {
                this.setDrawQuad(bq);
                Image image = Image.Image_createWithResIDQuad(resID, lq);
                Image image2 = Image.Image_createWithResIDQuad(resID, lqp);
                this.lift = (Lift)new Lift().initWithUpElementDownElementandID(image, image2, 0);
                this.lift.parentAnchor = 10;
                this.lift.anchor = 18;
                this.lift.maxY = (float)this.height;
                this.lift.liftDelegate = new Lift.PercentXY(this.percentXY);
                this.addChild(this.lift);
            }
            return this;
        }

        // Token: 0x060002CC RID: 716 RVA: 0x0001208C File Offset: 0x0001028C
        public void percentXY(float px, float py)
        {
            Vector maxScroll = this.container.getMaxScroll();
            this.container.setScroll(MathHelper.vect(maxScroll.x * px, maxScroll.y * py));
        }

        // Token: 0x060002CD RID: 717 RVA: 0x000120C8 File Offset: 0x000102C8
        public override void update(float delta)
        {
            base.update(delta);
            Vector scroll = this.container.getScroll();
            Vector maxScroll = this.container.getMaxScroll();
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
            this.lift.x = (this.lift.maxX - this.lift.minX) * num + this.lift.minX;
            this.lift.y = (this.lift.maxY - this.lift.minY) * num2 + this.lift.minY;
        }

        // Token: 0x060002CE RID: 718 RVA: 0x0001219C File Offset: 0x0001039C
        public override void dealloc()
        {
            this.container = null;
            base.dealloc();
        }

        // Token: 0x060002CF RID: 719 RVA: 0x000121AC File Offset: 0x000103AC
        public override bool onTouchUpXY(float tx, float ty)
        {
            bool flag = base.onTouchUpXY(tx, ty);
            this.container.startMovingToSpointInDirection(MathHelper.vectZero);
            return flag;
        }

        // Token: 0x040008B8 RID: 2232
        private Lift lift;

        // Token: 0x040008B9 RID: 2233
        public ScrollableContainer container;
    }
}
