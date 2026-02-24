using System.Collections.Generic;

using ctr_wp7.ctr_commons;
using ctr_wp7.iframework.visual;
using ctr_wp7.ios;

namespace ctr_wp7.iframework.core
{
    internal class RootController : ViewController
    {
        public override NSObject initWithParent(ViewController p)
        {
            if (base.initWithParent(p) != null)
            {
                viewTransition = -1;
                transitionTime = -1f;
                previousView = null;
                transitionDelay = 0.3f;
                screenGrabber = (Grabber)new Grabber().init();
                if (prevScreenImage != null && prevScreenImage.xnaTexture_ != null)
                {
                    prevScreenImage.xnaTexture_.Dispose();
                }
                prevScreenImage = null;
                if (nextScreenImage != null && nextScreenImage.xnaTexture_ != null)
                {
                    nextScreenImage.xnaTexture_.Dispose();
                }
                nextScreenImage = null;
                deactivateCurrentController = false;
            }
            return this;
        }

        public void performTick(float delta)
        {
            lastTime += delta;
            if (transitionTime == -1f)
            {
                currentController.update(delta);
            }
            if (deactivateCurrentController)
            {
                deactivateCurrentController = false;
                currentController.deactivateImmediately();
            }
        }

        public void performDraw()
        {
            if (currentController.activeViewID != -1)
            {
                GLCanvas.beforeRender();
                OpenGL.glPushMatrix();
                applyLandscape();
                if (transitionTime == -1f)
                {
                    currentController.activeView().draw();
                }
                else
                {
                    drawViewTransition();
                    if (lastTime > transitionTime)
                    {
                        transitionTime = -1f;
                        NSREL(prevScreenImage);
                        if (prevScreenImage != null && prevScreenImage.xnaTexture_ != null)
                        {
                            prevScreenImage.xnaTexture_.Dispose();
                        }
                        prevScreenImage = null;
                        NSREL(nextScreenImage);
                        if (nextScreenImage != null && nextScreenImage.xnaTexture_ != null)
                        {
                            nextScreenImage.xnaTexture_.Dispose();
                        }
                        nextScreenImage = null;
                    }
                }
                OpenGL.glPopMatrix();
                GLCanvas.afterRender();
            }
        }

        private static void applyLandscape()
        {
        }

        public virtual void setViewTransition(int transition)
        {
            viewTransition = transition;
        }

        public virtual void setTransitionTime()
        {
            transitionTime = lastTime + transitionDelay;
        }

        private void setViewTransitionDelay(float delay)
        {
            transitionDelay = delay;
        }

        private void drawViewTransition()
        {
            OpenGL.SetWhiteColor();
            OpenGL.glEnable(0);
            OpenGL.glEnable(1);
            OpenGL.glBlendFunc(BlendingFactor.GL_SRC_ALPHA, BlendingFactor.GL_ONE_MINUS_SRC_ALPHA);
            GLCanvas.setDefaultRealProjection();
            switch (viewTransition)
            {
                case 4:
                case 5:
                    {
                        float num = MIN(1.0, (double)((transitionDelay - (transitionTime - lastTime)) / transitionDelay));
                        if ((double)num < 0.5)
                        {
                            if (prevScreenImage != null)
                            {
                                RGBAColor rgbacolor = (viewTransition == 4) ? RGBAColor.MakeRGBA(0.0, 0.0, 0.0, (double)num * 2.0) : RGBAColor.MakeRGBA(1.0, 1.0, 1.0, (double)num * 2.0);
                                Grabber.drawGrabbedImage(prevScreenImage, 0, 0);
                                OpenGL.glDisable(0);
                                OpenGL.glEnable(1);
                                OpenGL.glBlendFunc(BlendingFactor.GL_SRC_ALPHA, BlendingFactor.GL_ONE_MINUS_SRC_ALPHA);
                                GLDrawer.drawSolidRectWOBorder(0f, 0f, REAL_SCREEN_WIDTH, REAL_SCREEN_HEIGHT, rgbacolor);
                                OpenGL.glDisable(1);
                            }
                            else
                            {
                                if (viewTransition == 4)
                                {
                                    OpenGL.glClearColor(0.0, 0.0, 0.0, 1.0);
                                }
                                else
                                {
                                    OpenGL.glClearColor(1.0, 1.0, 1.0, 1.0);
                                }
                                OpenGL.glClear(0);
                            }
                        }
                        else if (nextScreenImage != null)
                        {
                            RGBAColor rgbacolor2 = (viewTransition == 4) ? RGBAColor.MakeRGBA(0.0, 0.0, 0.0, 2.0 - ((double)num * 2.0)) : RGBAColor.MakeRGBA(1.0, 1.0, 1.0, 2.0 - ((double)num * 2.0));
                            Grabber.drawGrabbedImage(nextScreenImage, 0, 0);
                            OpenGL.glDisable(0);
                            OpenGL.glEnable(1);
                            OpenGL.glBlendFunc(BlendingFactor.GL_SRC_ALPHA, BlendingFactor.GL_ONE_MINUS_SRC_ALPHA);
                            GLDrawer.drawSolidRectWOBorder(0f, 0f, REAL_SCREEN_WIDTH, REAL_SCREEN_HEIGHT, rgbacolor2);
                            OpenGL.glDisable(1);
                        }
                        else
                        {
                            if (viewTransition == 4)
                            {
                                OpenGL.glClearColor(0.0, 0.0, 0.0, 1.0);
                            }
                            else
                            {
                                OpenGL.glClearColor(1.0, 1.0, 1.0, 1.0);
                            }
                            OpenGL.glClear(0);
                        }
                        break;
                    }
                case 6:
                    {
                        float num2 = MIN(1.0, (double)((transitionDelay - (transitionTime - lastTime)) / transitionDelay));
                        OpenGL.glColor4f(1.0, 1.0, 1.0, 1.0 - (double)num2);
                        Grabber.drawGrabbedImage(prevScreenImage, 0, 0);
                        OpenGL.glColor4f(1.0, 1.0, 1.0, (double)num2);
                        Grabber.drawGrabbedImage(nextScreenImage, 0, 0);
                        break;
                    }
            }
            applyLandscape();
            OpenGL.glDisable(0);
            OpenGL.glDisable(1);
        }

        public override void activate()
        {
            base.activate();
        }

        private static void runLoop()
        {
        }

        public virtual void onControllerActivated(ViewController c)
        {
            setCurrentController(c);
        }

        public virtual void onControllerDeactivated(ViewController c)
        {
            setCurrentController(null);
        }

        public virtual void onControllerPaused(ViewController c)
        {
            setCurrentController(null);
        }

        public virtual void onControllerUnpaused(ViewController c)
        {
            setCurrentController(c);
        }

        public virtual void onControllerDeactivationRequest(ViewController c)
        {
            deactivateCurrentController = true;
        }

        public virtual void onControllerViewShow(View v)
        {
            if (viewTransition != -1 && previousView != null)
            {
                GLCanvas.setDefaultProjection();
                OpenGL.glClearColor(0.0, 0.0, 0.0, 1.0);
                OpenGL.glClear(0);
                transitionTime = lastTime + transitionDelay;
                applyLandscape();
                currentController.activeView().draw();
                NSREL(nextScreenImage);
                if (nextScreenImage != null && nextScreenImage.xnaTexture_ != null)
                {
                    nextScreenImage.xnaTexture_.Dispose();
                }
                nextScreenImage = Grabber.grab();
                _ = NSRET(nextScreenImage);
                OpenGL.glLoadIdentity();
            }
        }

        public virtual void onControllerViewHide(View v)
        {
            previousView = v;
            if (viewTransition != -1 && previousView != null)
            {
                GLCanvas.setDefaultProjection();
                OpenGL.glClearColor(0.0, 0.0, 0.0, 1.0);
                OpenGL.glClear(0);
                applyLandscape();
                previousView.draw();
                NSREL(prevScreenImage);
                if (prevScreenImage != null && prevScreenImage.xnaTexture_ != null)
                {
                    prevScreenImage.xnaTexture_.Dispose();
                }
                prevScreenImage = Grabber.grab();
                _ = NSRET(prevScreenImage);
                OpenGL.glLoadIdentity();
            }
        }

        public virtual bool isSuspended()
        {
            return suspended;
        }

        public virtual void suspend()
        {
            suspended = true;
        }

        public virtual void resume()
        {
            suspended = false;
        }

        public override bool backButtonPressed()
        {
            return suspended || transitionTime != -1f || currentController.backButtonPressed();
        }

        public override bool menuButtonPressed()
        {
            return suspended || transitionTime != -1f || currentController.menuButtonPressed();
        }

        public override bool touchesBeganwithEvent(List<CTRTouchState> touches)
        {
            return !suspended && (transitionTime != -1f || currentController.touchesBeganwithEvent(touches));
        }

        public override bool touchesMovedwithEvent(List<CTRTouchState> touches)
        {
            return !suspended && (transitionTime != -1f || currentController.touchesMovedwithEvent(touches));
        }

        public override bool touchesEndedwithEvent(List<CTRTouchState> touches)
        {
            return !suspended && (transitionTime != -1f || currentController.touchesEndedwithEvent(touches));
        }

        public override bool touchesCancelledwithEvent(List<CTRTouchState> touches)
        {
            return currentController.touchesCancelledwithEvent(touches);
        }

        public virtual void setCurrentController(ViewController c)
        {
            currentController = c;
        }

        public virtual ViewController getCurrentController()
        {
            return currentController;
        }

        public const int TRANSITION_SLIDE_HORIZONTAL_RIGHT = 0;

        public const int TRANSITION_SLIDE_HORIZONTAL_LEFT = 1;

        public const int TRANSITION_SLIDE_VERTICAL_UP = 2;

        public const int TRANSITION_SLIDE_VERTICAL_DON = 3;

        public const int TRANSITION_FADE_OUT_BLACK = 4;

        public const int TRANSITION_FADE_OUT_WHITE = 5;

        public const int TRANSITION_REVEAL = 6;

        public const int TRANSITIONS_COUNT = 7;

        public const float TRANSITION_DEFAULT_DELAY = 0.3f;

        public int viewTransition;

        public float transitionTime;

        private float transitionDelay;

        private View previousView;

        private Texture2D prevScreenImage;

        private Texture2D nextScreenImage;

        private Grabber screenGrabber;

        private bool deactivateCurrentController;

        private ViewController currentController;

        private float lastTime;

        public bool suspended;
    }
}
