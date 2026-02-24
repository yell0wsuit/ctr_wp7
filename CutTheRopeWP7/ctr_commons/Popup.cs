using ctr_wp7.iframework;
using ctr_wp7.iframework.core;
using ctr_wp7.iframework.visual;
using ctr_wp7.ios;

namespace ctr_wp7.ctr_commons
{
    internal class Popup : BaseElement, TimelineDelegate
    {
        public override NSObject init()
        {
            if (base.init() != null)
            {
                Timeline timeline = new Timeline().initWithMaxKeyFramesOnTrack(4);
                timeline.addKeyFrame(KeyFrame.makeScale(0.0, 0.0, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.0));
                timeline.addKeyFrame(KeyFrame.makeScale(1.1, 1.1, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.3));
                timeline.addKeyFrame(KeyFrame.makeScale(0.9, 0.9, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.1));
                timeline.addKeyFrame(KeyFrame.makeScale(1.0, 1.0, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.2));
                _ = addTimeline(timeline);
                timeline = new Timeline().initWithMaxKeyFramesOnTrack(2);
                timeline.addKeyFrame(KeyFrame.makeScale(1.0, 1.0, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.0));
                timeline.addKeyFrame(KeyFrame.makeScale(0.0, 0.0, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.3));
                width = (int)SCREEN_WIDTH;
                height = (int)SCREEN_HEIGHT;
                _ = addTimeline(timeline);
                timeline.delegateTimelineDelegate = this;
            }
            return this;
        }

        public override void draw()
        {
            OpenGL.glEnable(1);
            OpenGL.glDisable(0);
            OpenGL.glBlendFunc(BlendingFactor.GL_ONE, BlendingFactor.GL_ONE_MINUS_SRC_ALPHA);
            GLDrawer.drawSolidRectWOBorder(-SCREEN_OFFSET_X, -SCREEN_OFFSET_Y, SCREEN_WIDTH_EXPANDED, SCREEN_HEIGHT_EXPANDED, RGBAColor.MakeRGBA(0.0, 0.0, 0.0, 0.5));
            OpenGL.glEnable(0);
            OpenGL.SetWhiteColor();
            base.preDraw();
            base.postDraw();
            OpenGL.glDisable(1);
        }

        public virtual void timelinereachedKeyFramewithIndex(Timeline t, KeyFrame k, int i)
        {
        }

        public virtual void timelineFinished(Timeline t)
        {
            View view = (View)parent;
            view?.removeChild(this);
        }

        public virtual void showPopup()
        {
            isShow = true;
            playTimeline(0);
        }

        public virtual void hidePopup()
        {
            isShow = false;
            playTimeline(1);
        }

        public override bool onTouchDownXY(float tx, float ty)
        {
            if (isShow)
            {
                _ = base.onTouchDownXY(tx, ty);
            }
            return true;
        }

        public override bool onTouchUpXY(float tx, float ty)
        {
            if (isShow)
            {
                _ = base.onTouchUpXY(tx, ty);
            }
            return true;
        }

        public override bool onTouchMoveXY(float tx, float ty)
        {
            if (isShow)
            {
                _ = base.onTouchMoveXY(tx, ty);
            }
            return true;
        }

        private readonly Image imageBackground;

        private bool isShow;

        private enum POPUP
        {
            POPUP_SHOW_ANIM,
            POPUP_HIDE_ANIM
        }
    }
}
