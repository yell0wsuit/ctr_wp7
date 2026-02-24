using ctr_wp7.iframework;
using ctr_wp7.iframework.core;

internal sealed class CoppaView : View
{
    public override void draw()
    {
        OpenGL.SetWhiteColor();
        OpenGL.glEnable(0);
        OpenGL.glEnable(1);
        OpenGL.glBlendFunc(BlendingFactor.GL_ONE, BlendingFactor.GL_ONE_MINUS_SRC_ALPHA);
        preDraw();
        postDraw();
        OpenGL.glDisable(0);
        OpenGL.glDisable(1);
    }
}
