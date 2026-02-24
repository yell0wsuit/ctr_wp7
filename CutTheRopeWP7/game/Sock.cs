using System.Collections.Generic;

using ctr_wp7.iframework.core;
using ctr_wp7.iframework.visual;

namespace ctr_wp7.game
{
    internal sealed class Sock : CTRGameObject
    {
        public static Sock Sock_create(Texture2D t)
        {
            return (Sock)new Sock().initWithTexture(t);
        }

        public static Sock Sock_createWithResID(int r)
        {
            return Sock_create(Application.getTexture(r));
        }

        public static Sock Sock_createWithResIDQuad(int r, int q)
        {
            Sock sock = Sock_create(Application.getTexture(r));
            sock.setDrawQuad(q);
            return sock;
        }

        public override void update(float delta)
        {
            base.update(delta);
            if (mover != null)
            {
                updateRotation();
            }
        }

        public override void draw()
        {
            Timeline currentTimeline = light.getCurrentTimeline();
            if (currentTimeline != null && currentTimeline.state == Timeline.TimelineState.TIMELINE_STOPPED)
            {
                light.visible = false;
            }
            base.draw();
        }

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
            List<int> list = [3, 4, 4];
            animation.addAnimationWithIDDelayLoopCountSequence(num, num2, loopType, num3, num4, list);
            light.doRestoreCutTransparency();
            light.visible = false;
            _ = addChild(light);
        }

        public void updateRotation()
        {
            t1.x = x - 15f;
            t2.x = x + 15f;
            t1.y = t2.y = y;
            b1.x = t1.x;
            b2.x = t2.x;
            b1.y = b2.y = y + 1f;
            angle = DEGREES_TO_RADIANS(rotation);
            t1 = vectRotateAround(t1, angle, x, y);
            t2 = vectRotateAround(t2, angle, x, y);
            b1 = vectRotateAround(b1, angle, x, y);
            b2 = vectRotateAround(b2, angle, x, y);
        }

        public const float SOCK_IDLE_TIMOUT = 0.8f;

        public static int SOCK_RECEIVING;

        public static int SOCK_THROWING = 1;

        public static int SOCK_IDLE = 2;

        public int group;

        public double angle;

        public Vector t1;

        public Vector t2;

        public Vector b1;

        public Vector b2;

        public float idleTimeout;

        public Animation light;
    }
}
