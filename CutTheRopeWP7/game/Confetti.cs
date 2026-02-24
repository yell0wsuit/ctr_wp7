using ctr_wp7.iframework.core;
using ctr_wp7.iframework.visual;

namespace ctr_wp7.game
{
    // Token: 0x02000097 RID: 151
    internal class Confetti : Animation
    {
        // Token: 0x06000485 RID: 1157 RVA: 0x00020B86 File Offset: 0x0001ED86
        public static Confetti Confetti_createWithResID(int r)
        {
            return Confetti.Confetti_create(Application.getTexture(r));
        }

        // Token: 0x06000486 RID: 1158 RVA: 0x00020B93 File Offset: 0x0001ED93
        public static Confetti Confetti_create(Texture2D t)
        {
            return (Confetti)new Confetti().initWithTexture(t);
        }

        // Token: 0x06000487 RID: 1159 RVA: 0x00020BA5 File Offset: 0x0001EDA5
        public override void update(float delta)
        {
            base.update(delta);
            Timeline.updateTimeline(this.ani, delta);
        }

        // Token: 0x040009CD RID: 2509
        public Timeline ani;
    }
}
