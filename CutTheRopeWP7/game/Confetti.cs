using ctr_wp7.iframework.core;
using ctr_wp7.iframework.visual;

namespace ctr_wp7.game
{
    internal sealed class Confetti : Animation
    {
        public static Confetti Confetti_createWithResID(int r)
        {
            return Confetti_create(Application.getTexture(r));
        }

        public static Confetti Confetti_create(Texture2D t)
        {
            return (Confetti)new Confetti().initWithTexture(t);
        }

        public override void update(float delta)
        {
            base.update(delta);
            Timeline.updateTimeline(ani, delta);
        }

        public Timeline ani;
    }
}
