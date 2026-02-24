using ctr_wp7.iframework;
using ctr_wp7.iframework.core;
using ctr_wp7.iframework.visual;
using ctr_wp7.ios;

namespace ctr_wp7.game
{
    internal sealed class GameView : View
    {
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

        public override void show()
        {
            base.show();
        }

        public override void hide()
        {
            base.hide();
        }

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
                if (skipAd.active && skipAd.timerNoDraw >= 2.5)
                {
                    skipAd.draw();
                }
            }
            GameScene gameScene = (GameScene)getChild(0);
            if (gameScene.dimTime > 0.0)
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
            GameScene.drawDrawing();
        }

        public override void dealloc()
        {
            base.dealloc();
        }

        public void setJSkipper(object skipper)
        {
            skipAd.setJskipper(skipper);
        }

        public void unsetJSkipper()
        {
            skipAd.freeJskipper();
        }

        public const int VIEW_ELEMENT_GAME_SCENE = 0;

        public const int VIEW_ELEMENT_PAUSE_BUTTON = 1;

        public const int VIEW_ELEMENT_RESTART_BUTTON = 2;

        public const int VIEW_ELEMENT_NEXT_BUTTON = 3;

        public const int VIEW_ELEMENT_PAUSE_MENU = 4;

        public const int VIEW_ELEMENT_RESULTS = 5;

        public const int VIEW_ELEMENT_AD_DIM = 6;

        public const int VIEW_ELEMENT_AD_SKIP_BUTTON = 7;

        private Text loadingText;

        public bool videoAdLoading;

        private AdSkipper skipAd;
    }
}
