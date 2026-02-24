using System.Collections.Generic;

using ctr_wp7.iframework.core;
using ctr_wp7.iframework.visual;

namespace ctr_wp7.game
{
    // Token: 0x02000054 RID: 84
    internal class Sock : CTRGameObject
    {
        // Token: 0x06000288 RID: 648 RVA: 0x00010436 File Offset: 0x0000E636
        public static Sock Sock_create(Texture2D t)
        {
            return (Sock)new Sock().initWithTexture(t);
        }

        // Token: 0x06000289 RID: 649 RVA: 0x00010448 File Offset: 0x0000E648
        public static Sock Sock_createWithResID(int r)
        {
            return Sock_create(Application.getTexture(r));
        }

        // Token: 0x0600028A RID: 650 RVA: 0x00010458 File Offset: 0x0000E658
        public static Sock Sock_createWithResIDQuad(int r, int q)
        {
            Sock sock = Sock_create(Application.getTexture(r));
            sock.setDrawQuad(q);
            return sock;
        }

        // Token: 0x0600028B RID: 651 RVA: 0x00010479 File Offset: 0x0000E679
        public override void update(float delta)
        {
            base.update(delta);
            if (mover != null)
            {
                updateRotation();
            }
        }

        // Token: 0x0600028C RID: 652 RVA: 0x00010490 File Offset: 0x0000E690
        public override void draw()
        {
            Timeline currentTimeline = light.getCurrentTimeline();
            if (currentTimeline != null && currentTimeline.state == Timeline.TimelineState.TIMELINE_STOPPED)
            {
                light.visible = false;
            }
            base.draw();
        }

        // Token: 0x0600028D RID: 653 RVA: 0x000104C8 File Offset: 0x0000E6C8
        public void createAnimations()
        {
            light = Animation_createWithResID(145);
            light.anchor = 34;
            light.parentAnchor = 10;
            light.y = 90f;
            light.x = 0f;
            Animation animation = light;
            int num = 0;
            float num2 = 0.05f;
            Timeline.LoopType loopType = Timeline.LoopType.TIMELINE_NO_LOOP;
            int num3 = 4;
            int num4 = 2;
            List<int> list = new List<int>();
            list.Add(3);
            list.Add(4);
            list.Add(4);
            animation.addAnimationWithIDDelayLoopCountSequence(num, num2, loopType, num3, num4, list);
            light.doRestoreCutTransparency();
            light.visible = false;
            addChild(light);
        }

        // Token: 0x0600028E RID: 654 RVA: 0x00010574 File Offset: 0x0000E774
        public void updateRotation()
        {
            t1.x = x - 15f;
            t2.x = x + 15f;
            t1.y = (t2.y = y);
            b1.x = t1.x;
            b2.x = t2.x;
            b1.y = (b2.y = y + 1f);
            angle = (double)DEGREES_TO_RADIANS(rotation);
            t1 = vectRotateAround(t1, angle, x, y);
            t2 = vectRotateAround(t2, angle, x, y);
            b1 = vectRotateAround(b1, angle, x, y);
            b2 = vectRotateAround(b2, angle, x, y);
        }

        // Token: 0x04000884 RID: 2180
        public const float SOCK_IDLE_TIMOUT = 0.8f;

        // Token: 0x04000885 RID: 2181
        public static int SOCK_RECEIVING = 0;

        // Token: 0x04000886 RID: 2182
        public static int SOCK_THROWING = 1;

        // Token: 0x04000887 RID: 2183
        public static int SOCK_IDLE = 2;

        // Token: 0x04000888 RID: 2184
        public int group;

        // Token: 0x04000889 RID: 2185
        public double angle;

        // Token: 0x0400088A RID: 2186
        public Vector t1;

        // Token: 0x0400088B RID: 2187
        public Vector t2;

        // Token: 0x0400088C RID: 2188
        public Vector b1;

        // Token: 0x0400088D RID: 2189
        public Vector b2;

        // Token: 0x0400088E RID: 2190
        public float idleTimeout;

        // Token: 0x0400088F RID: 2191
        public Animation light;
    }
}
