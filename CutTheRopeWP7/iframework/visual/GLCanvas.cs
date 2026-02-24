using System.Collections.Generic;

using ctr_wp7.ctr_commons;
using ctr_wp7.ios;

namespace ctr_wp7.iframework.visual
{
    internal sealed class GLCanvas : NSObject
    {
        public GLCanvas initWithFrame(Rectangle frame)
        {
            _ = init();
            return this;
        }

        public void show()
        {
            destroyFramebuffer();
            _ = createFramebuffer();
        }

        public static void hide()
        {
            destroyFramebuffer();
        }

        public static void beforeRender()
        {
            setDefaultProjection();
            OpenGL.glDisable(1);
            OpenGL.glEnableClientState(11);
            OpenGL.glEnableClientState(12);
        }

        public static void afterRender()
        {
        }

        public void initFPSMeterWithFont(FontGeneric font)
        {
            fpsFont = font;
            fpsText = new Text().initWithFont(fpsFont);
        }

        public void drawFPS(int fps)
        {
            if (fpsText == null || fpsFont == null)
            {
                return;
            }
            NSString nsstring = NSS(fps.ToString());
            fpsText.setString(nsstring);
            OpenGL.SetWhiteColor();
            OpenGL.glEnable(0);
            OpenGL.glEnable(1);
            OpenGL.glBlendFunc(BlendingFactor.GL_SRC_ALPHA, BlendingFactor.GL_ONE_MINUS_SRC_ALPHA);
            fpsText.x = 5f;
            fpsText.y = 5f;
            fpsText.draw();
            OpenGL.glDisable(1);
            OpenGL.glDisable(0);
        }

        public bool createFramebuffer()
        {
            backingWidth = (int)SCREEN_WIDTH;
            backingHeight = (int)SCREEN_HEIGHT;
            setDefaultProjection();
            OpenGL.glEnableClientState(11);
            OpenGL.glEnableClientState(12);
            return true;
        }

        public static void setDefaultProjection()
        {
            OpenGL.glViewport(0.0, 0.0, REAL_SCREEN_WIDTH, REAL_SCREEN_HEIGHT);
            OpenGL.glMatrixMode(15);
            OpenGL.glLoadIdentity();
            OpenGL.glOrthof((double)-(double)SCREEN_OFFSET_X, (double)(SCREEN_WIDTH + SCREEN_OFFSET_X), (double)(SCREEN_HEIGHT + SCREEN_OFFSET_Y), (double)-(double)SCREEN_OFFSET_Y, -1.0, 1.0);
            OpenGL.glMatrixMode(14);
            OpenGL.glLoadIdentity();
        }

        public static void setDefaultRealProjection()
        {
            OpenGL.glViewport(0.0, 0.0, REAL_SCREEN_WIDTH, REAL_SCREEN_HEIGHT);
            OpenGL.glMatrixMode(15);
            OpenGL.glLoadIdentity();
            OpenGL.glOrthof(0.0, REAL_SCREEN_WIDTH, REAL_SCREEN_HEIGHT, 0.0, -1.0, 1.0);
            OpenGL.glMatrixMode(14);
            OpenGL.glLoadIdentity();
        }

        public static void destroyFramebuffer()
        {
        }

        public void touchesBeganwithEvent(List<CTRTouchState> touches)
        {
            if (touchDelegate != null)
            {
                _ = touchDelegate.touchesBeganwithEvent(touches);
            }
        }

        public void touchesMovedwithEvent(List<CTRTouchState> touches)
        {
            if (touchDelegate != null)
            {
                _ = touchDelegate.touchesMovedwithEvent(touches);
            }
        }

        public void touchesEndedwithEvent(List<CTRTouchState> touches)
        {
            if (touchDelegate != null)
            {
                _ = touchDelegate.touchesEndedwithEvent(touches);
            }
        }

        public void touchesCancelledwithEvent(List<CTRTouchState> touches)
        {
            if (touchDelegate != null)
            {
                _ = touchDelegate.touchesCancelledwithEvent(touches);
            }
        }

        public bool backButtonPressed()
        {
            return touchDelegate != null && touchDelegate.backButtonPressed();
        }

        public bool menuButtonPressed()
        {
            return touchDelegate != null && touchDelegate.menuButtonPressed();
        }

        public override void dealloc()
        {
            NSREL(fpsFont);
            NSREL(fpsText);
            hide();
        }

        public int backingWidth;

        public int backingHeight;

        public uint viewRenderbuffer;

        public uint viewFramebuffer;

        public uint depthRenderbuffer;

        public FontGeneric fpsFont;

        public Text fpsText;

        public TouchDelegate touchDelegate;
    }
}
