using ctr_wp7.iframework.visual;
using ctr_wp7.ios;

namespace ctr_wp7.iframework.core
{
    // Token: 0x02000016 RID: 22
    internal class View : BaseElement
    {
        // Token: 0x06000119 RID: 281 RVA: 0x00009C5D File Offset: 0x00007E5D
        public virtual NSObject initFullscreen()
        {
            if (base.init() != null)
            {
                width = (int)FrameworkTypes.SCREEN_WIDTH;
                height = (int)FrameworkTypes.SCREEN_HEIGHT;
            }
            return this;
        }

        // Token: 0x0600011A RID: 282 RVA: 0x00009C80 File Offset: 0x00007E80
        public override NSObject init()
        {
            return initFullscreen();
        }

        // Token: 0x0600011B RID: 283 RVA: 0x00009C88 File Offset: 0x00007E88
        public override void draw()
        {
            OpenGL.SetWhiteColor();
            OpenGL.glEnable(0);
            OpenGL.glEnable(1);
            OpenGL.glBlendFunc(BlendingFactor.GL_SRC_ALPHA, BlendingFactor.GL_ONE_MINUS_SRC_ALPHA);
            base.preDraw();
            base.postDraw();
            OpenGL.glDisable(0);
            OpenGL.glDisable(1);
        }
    }
}
