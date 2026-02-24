using ctr_wp7.iframework;
using ctr_wp7.iframework.core;
using ctr_wp7.iframework.visual;

namespace ctr_wp7.game
{
    // Token: 0x020000DE RID: 222
    internal sealed class LoadingView : View
    {
        // Token: 0x06000679 RID: 1657 RVA: 0x00031A80 File Offset: 0x0002FC80
        public override void draw()
        {
            OpenGL.glEnable(0);
            OpenGL.glEnable(1);
            OpenGL.glBlendFunc(BlendingFactor.GL_ONE, BlendingFactor.GL_ONE_MINUS_SRC_ALPHA);
            preDraw();
            CTRRootController ctrrootController = (CTRRootController)Application.sharedRootController();
            int num = 216 + ctrrootController.getPack();
            float num2 = Application.sharedResourceMgr().getPercentLoaded();
            Texture2D texture = Application.getTexture(num);
            OpenGL.glColor4f(0.85, 0.85, 0.85, 1.0);
            OpenGL.glPushMatrix();
            OpenGL.glTranslatef(-SCREEN_OFFSET_X, -SCREEN_OFFSET_Y, 0f);
            OpenGL.glScalef(SCREEN_BG_SCALE_X, SCREEN_BG_SCALE_Y, 1f);
            GLDrawer.drawImageQuad(texture, 0, 0.33, 0.0);
            OpenGL.glPushMatrix();
            OpenGL.glTranslatef(240.0, 240.0, 0.0);
            OpenGL.glRotatef(180.0, 0.0, 0.0, 1.0);
            OpenGL.glTranslatef(-240.0, -240.0, 0.0);
            GLDrawer.drawImageQuad(texture, 0, 159.67, 0.5);
            OpenGL.glPopMatrix();
            Texture2D texture2 = Application.getTexture(7);
            if (!game)
            {
                OpenGL.glEnable(4);
                OpenGL.setScissorRectangle(0.0, (double)-(double)SCREEN_OFFSET_Y, SCREEN_WIDTH, SCREEN_BG_SCALE_Y * 500.0 * (double)num2 / 100.0);
            }
            OpenGL.SetWhiteColor();
            if (game || num2 > 0f)
            {
                GLDrawer.drawImageQuad(texture2, 0, 141.0, 25.0);
                GLDrawer.drawImageQuad(texture2, 1, 159.0, 25.0);
            }
            if (!game)
            {
                OpenGL.glDisable(4);
            }
            if (game)
            {
                float num3 = (float)(600.0 * (double)num2 / 100.0);
                GLDrawer.drawImageQuad(texture2, 2, -3.0, (double)(350f - num3));
            }
            else
            {
                float num4 = (float)(500.0 * (double)num2 / 100.0);
                GLDrawer.drawImageQuad(texture2, 3, 92.0, (double)num4 - 50.0);
            }
            OpenGL.glPopMatrix();
            postDraw();
            OpenGL.SetWhiteColor();
            OpenGL.glDisable(0);
            OpenGL.glDisable(1);
        }

        // Token: 0x04000BEF RID: 3055
        public bool game;
    }
}
