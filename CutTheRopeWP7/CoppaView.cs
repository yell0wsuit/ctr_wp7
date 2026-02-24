using ctr_wp7.iframework;
using ctr_wp7.iframework.core;

// Token: 0x02000040 RID: 64
internal class CoppaView : View
{
    // Token: 0x0600021E RID: 542 RVA: 0x0000E2E7 File Offset: 0x0000C4E7
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
