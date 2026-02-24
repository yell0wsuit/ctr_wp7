using ctr_wp7.iframework.core;
using ctr_wp7.iframework.visual;
using ctr_wp7.ios;

namespace ctr_wp7.game
{
    internal class Bouncer : CTRGameObject
    {
        private static Bouncer Bouncer_create(Texture2D t)
        {
            return (Bouncer)new Bouncer().initWithTexture(t);
        }

        private static Bouncer Bouncer_createWithResID(int r)
        {
            return Bouncer_create(Application.getTexture(r));
        }

        private static Bouncer Bouncer_createWithResIDQuad(int r, int q)
        {
            Bouncer bouncer = Bouncer_create(Application.getTexture(r));
            bouncer.setDrawQuad(q);
            return bouncer;
        }

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
            int num2 = addAnimationDelayLoopFirstLast(0.04f, Timeline.LoopType.TIMELINE_NO_LOOP, 0, 4);
            Timeline timeline = getTimeline(num2);
            timeline.addKeyFrame(KeyFrame.makeSingleAction(this, "ACTION_SET_DRAWQUAD", 0, 0, 0.04f));
            return this;
        }

        public override void update(float delta)
        {
            base.update(delta);
            if (mover != null)
            {
                updateRotation();
            }
        }

        public virtual void updateRotation()
        {
            t1.x = x - (width / 2);
            t2.x = x + (width / 2);
            t1.y = t2.y = (float)(y - 5.0);
            b1.x = t1.x;
            b2.x = t2.x;
            b1.y = b2.y = (float)(y + 5.0);
            angle = DEGREES_TO_RADIANS(rotation);
            t1 = vectRotateAround(t1, angle, x, y);
            t2 = vectRotateAround(t2, angle, x, y);
            b1 = vectRotateAround(b1, angle, x, y);
            b2 = vectRotateAround(b2, angle, x, y);
        }

        private const float BOUNCER_HEIGHT = 10f;

        public float angle;

        public Vector t1;

        public Vector t2;

        public Vector b1;

        public Vector b2;

        public bool skip;
    }
}
