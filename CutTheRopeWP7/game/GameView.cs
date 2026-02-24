using ctr_wp7.iframework;
using ctr_wp7.iframework.core;
using ctr_wp7.iframework.visual;
using ctr_wp7.ios;

namespace ctr_wp7.game
{
    // Token: 0x020000B8 RID: 184
    internal class GameView : View
    {
        // Token: 0x06000534 RID: 1332 RVA: 0x00026118 File Offset: 0x00024318
        public override NSObject initFullscreen()
        {
            if (base.initFullscreen() == null)
            {
                return null;
            }
            videoAdLoading = false;
            loadingText = new Text().initWithFont(Application.getFont(5));
            loadingText.setAlignment(2);
            if (LANGUAGE == Language.LANG_KO)
            {
                loadingText.setStringandWidth(Application.getString(1310752), 200.0);
            }
            else
            {
                loadingText.setStringandWidth(Application.getString(1310752), 300.0);
            }
            loadingText.anchor = loadingText.parentAnchor = 34;
            loadingText.visible = false;
            _ = addChildwithID(loadingText, 6);
            skipAd = (AdSkipper)new AdSkipper().init();
            _ = addChildwithID(skipAd, 7);
            return this;
        }

        // Token: 0x06000535 RID: 1333 RVA: 0x000261F7 File Offset: 0x000243F7
        public override void show()
        {
            base.show();
        }

        // Token: 0x06000536 RID: 1334 RVA: 0x000261FF File Offset: 0x000243FF
        public override void hide()
        {
            base.hide();
        }

        // Token: 0x06000537 RID: 1335 RVA: 0x00026208 File Offset: 0x00024408
        public override void draw()
        {
            int num = childsCount();
            for (int i = 0; i < num; i++)
            {
                BaseElement child = getChild(i);
                if (child.visible)
                {
                    int num2 = i;
                    if (num2 == 4)
                    {
                        OpenGL.glDisable(0);
                        OpenGL.glEnable(1);
                        OpenGL.glBlendFunc(BlendingFactor.GL_SRC_ALPHA, BlendingFactor.GL_ONE_MINUS_SRC_ALPHA);
                        GLDrawer.drawSolidRectWOBorder(-SCREEN_OFFSET_X, -SCREEN_OFFSET_Y, SCREEN_WIDTH_EXPANDED, SCREEN_HEIGHT_EXPANDED, RGBAColor.MakeRGBA(0.1, 0.1, 0.1, 0.5));
                        OpenGL.SetWhiteColor();
                        OpenGL.glEnable(0);
                    }
                    child.draw();
                }
            }
            if (videoAdLoading || skipAd.active)
            {
                OpenGL.glDisable(0);
                OpenGL.glEnable(1);
                OpenGL.glBlendFunc(BlendingFactor.GL_ONE, BlendingFactor.GL_ONE_MINUS_SRC_ALPHA);
                GLDrawer.drawSolidRectWOBorder(-SCREEN_OFFSET_X, -SCREEN_OFFSET_Y, SCREEN_WIDTH_EXPANDED, SCREEN_HEIGHT_EXPANDED, RGBAColor.MakeRGBA(0.0, 0.0, 0.0, 0.5));
                OpenGL.SetWhiteColor();
                OpenGL.glEnable(0);
                if (videoAdLoading)
                {
                    loadingText.draw();
                }
                if (skipAd.active && (double)skipAd.timerNoDraw >= 2.5)
                {
                    skipAd.draw();
                }
            }
            GameScene gameScene = (GameScene)getChild(0);
            if ((double)gameScene.dimTime > 0.0)
            {
                float num3 = gameScene.dimTime / 0.15f;
                if (gameScene.restartState == 0)
                {
                    num3 = 1f - num3;
                }
                OpenGL.glDisable(0);
                OpenGL.glEnable(1);
                OpenGL.glBlendFunc(BlendingFactor.GL_SRC_ALPHA, BlendingFactor.GL_ONE_MINUS_SRC_ALPHA);
                GLDrawer.drawSolidRectWOBorder(-SCREEN_OFFSET_X, -SCREEN_OFFSET_Y, SCREEN_WIDTH_EXPANDED, SCREEN_HEIGHT_EXPANDED, RGBAColor.MakeRGBA(1.0, 1.0, 1.0, (double)num3));
                OpenGL.SetWhiteColor();
                OpenGL.glEnable(0);
            }
            gameScene.drawDrawing();
        }

        // Token: 0x06000538 RID: 1336 RVA: 0x00026422 File Offset: 0x00024622
        public override void dealloc()
        {
            base.dealloc();
        }

        // Token: 0x06000539 RID: 1337 RVA: 0x0002642A File Offset: 0x0002462A
        public void setJSkipper(object skipper)
        {
            skipAd.setJskipper(skipper);
        }

        // Token: 0x0600053A RID: 1338 RVA: 0x00026438 File Offset: 0x00024638
        public void unsetJSkipper()
        {
            skipAd.freeJskipper();
        }

        // Token: 0x04000A7F RID: 2687
        public const int VIEW_ELEMENT_GAME_SCENE = 0;

        // Token: 0x04000A80 RID: 2688
        public const int VIEW_ELEMENT_PAUSE_BUTTON = 1;

        // Token: 0x04000A81 RID: 2689
        public const int VIEW_ELEMENT_RESTART_BUTTON = 2;

        // Token: 0x04000A82 RID: 2690
        public const int VIEW_ELEMENT_NEXT_BUTTON = 3;

        // Token: 0x04000A83 RID: 2691
        public const int VIEW_ELEMENT_PAUSE_MENU = 4;

        // Token: 0x04000A84 RID: 2692
        public const int VIEW_ELEMENT_RESULTS = 5;

        // Token: 0x04000A85 RID: 2693
        public const int VIEW_ELEMENT_AD_DIM = 6;

        // Token: 0x04000A86 RID: 2694
        public const int VIEW_ELEMENT_AD_SKIP_BUTTON = 7;

        // Token: 0x04000A87 RID: 2695
        private Text loadingText;

        // Token: 0x04000A88 RID: 2696
        public bool videoAdLoading;

        // Token: 0x04000A89 RID: 2697
        private AdSkipper skipAd;
    }
}
