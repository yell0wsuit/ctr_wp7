using System;

using ctr_wp7.iframework;
using ctr_wp7.iframework.core;
using ctr_wp7.iframework.visual;

namespace ctr_wp7.game
{
    // Token: 0x0200003C RID: 60
    internal class StartupView : View
    {
        // Token: 0x06000214 RID: 532 RVA: 0x0000DD10 File Offset: 0x0000BF10
        public override void draw()
        {
            OpenGL.glEnable(0);
            OpenGL.glEnable(1);
            OpenGL.glBlendFunc(BlendingFactor.GL_ONE, BlendingFactor.GL_ONE_MINUS_SRC_ALPHA);
            this.preDraw();
            OpenGL.glClearColor(1.0, 1.0, 1.0, 1.0);
            OpenGL.glClear(0);
            float num = (float)Application.sharedResourceMgr().getPercentLoaded();
            Texture2D texture = Application.getTexture(0);
            Rectangle rectangle = FrameworkTypes.MakeRectangle(1.33f, 1.33f, (float)(texture._realWidth - 2), (float)(texture._realHeight - 2));
            if (texture.isWvga())
            {
                GLDrawer.drawImagePart(texture, rectangle, 1f, -25f);
            }
            else
            {
                GLDrawer.drawImagePart(texture, rectangle, 1f, 1f);
            }
            Texture2D texture2 = Application.getTexture(1);
            Rectangle rectangle2 = FrameworkTypes.MakeRectangle(0.0, 0.0, 223.3 * (double)num / 100.0, 15.0);
            GLDrawer.drawImagePart(texture2, rectangle2, 45f, 449f);
            this.postDraw();
            OpenGL.glDisable(0);
        }
    }
}
