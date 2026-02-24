using ctr_wp7.iframework.core;
using ctr_wp7.iframework.helpers;
using ctr_wp7.iframework.visual;
using ctr_wp7.ios;

namespace ctr_wp7.game
{
    // Token: 0x02000011 RID: 17
    internal class Bouncer : CTRGameObject
    {
        // Token: 0x06000102 RID: 258 RVA: 0x00008501 File Offset: 0x00006701
        private static Bouncer Bouncer_create(Texture2D t)
        {
            return (Bouncer)new Bouncer().initWithTexture(t);
        }

        // Token: 0x06000103 RID: 259 RVA: 0x00008513 File Offset: 0x00006713
        private static Bouncer Bouncer_createWithResID(int r)
        {
            return Bouncer.Bouncer_create(Application.getTexture(r));
        }

        // Token: 0x06000104 RID: 260 RVA: 0x00008520 File Offset: 0x00006720
        private static Bouncer Bouncer_createWithResIDQuad(int r, int q)
        {
            Bouncer bouncer = Bouncer.Bouncer_create(Application.getTexture(r));
            bouncer.setDrawQuad(q);
            return bouncer;
        }

        // Token: 0x06000105 RID: 261 RVA: 0x00008544 File Offset: 0x00006744
        public virtual NSObject initWithPosXYWidthAndAngle(float px, float py, int w, double an)
        {
            int num = -1;
            switch (w)
            {
                case 1:
                    num = 146;
                    break;
                case 2:
                    num = 147;
                    break;
            }
            if (initWithTexture(Application.getTexture(num)) == null)
            {
                return null;
            }
            rotation = (float)an;
            x = px;
            y = py;
            updateRotation();
            int num2 = base.addAnimationDelayLoopFirstLast(0.04f, Timeline.LoopType.TIMELINE_NO_LOOP, 0, 4);
            Timeline timeline = getTimeline(num2);
            timeline.addKeyFrame(KeyFrame.makeSingleAction(this, "ACTION_SET_DRAWQUAD", 0, 0, 0.04f));
            return this;
        }

        // Token: 0x06000106 RID: 262 RVA: 0x000085D2 File Offset: 0x000067D2
        public override void update(float delta)
        {
            base.update(delta);
            if (mover != null)
            {
                updateRotation();
            }
        }

        // Token: 0x06000107 RID: 263 RVA: 0x000085EC File Offset: 0x000067EC
        public virtual void updateRotation()
        {
            t1.x = x - (float)(width / 2);
            t2.x = x + (float)(width / 2);
            t1.y = (t2.y = (float)((double)y - 5.0));
            b1.x = t1.x;
            b2.x = t2.x;
            b1.y = (b2.y = (float)((double)y + 5.0));
            angle = MathHelper.DEGREES_TO_RADIANS(rotation);
            t1 = MathHelper.vectRotateAround(t1, (double)angle, x, y);
            t2 = MathHelper.vectRotateAround(t2, (double)angle, x, y);
            b1 = MathHelper.vectRotateAround(b1, (double)angle, x, y);
            b2 = MathHelper.vectRotateAround(b2, (double)angle, x, y);
        }

        // Token: 0x0400073B RID: 1851
        private const float BOUNCER_HEIGHT = 10f;

        // Token: 0x0400073C RID: 1852
        public float angle;

        // Token: 0x0400073D RID: 1853
        public Vector t1;

        // Token: 0x0400073E RID: 1854
        public Vector t2;

        // Token: 0x0400073F RID: 1855
        public Vector b1;

        // Token: 0x04000740 RID: 1856
        public Vector b2;

        // Token: 0x04000741 RID: 1857
        public bool skip;
    }
}
