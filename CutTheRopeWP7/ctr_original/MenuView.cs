using ctr_wp7.iframework;
using ctr_wp7.iframework.core;

namespace ctr_wp7.ctr_original
{
    // Token: 0x02000017 RID: 23
    internal class MenuView : View
    {
        // Token: 0x0600011D RID: 285 RVA: 0x00009CCA File Offset: 0x00007ECA
        public override void draw()
        {
            OpenGL.SetWhiteColor();
            OpenGL.glEnable(0);
            OpenGL.glEnable(1);
            OpenGL.glBlendFunc(BlendingFactor.GL_ONE, BlendingFactor.GL_ONE_MINUS_SRC_ALPHA);
            base.preDraw();
            base.postDraw();
            OpenGL.glDisable(0);
            OpenGL.glDisable(1);
        }
    }
}
