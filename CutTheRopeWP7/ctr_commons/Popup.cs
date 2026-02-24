using ctr_wp7.iframework;
using ctr_wp7.iframework.core;
using ctr_wp7.iframework.visual;
using ctr_wp7.ios;

namespace ctr_wp7.ctr_commons
{
    // Token: 0x0200006A RID: 106
    internal class Popup : BaseElement, TimelineDelegate
    {
        // Token: 0x0600032A RID: 810 RVA: 0x00014674 File Offset: 0x00012874
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

        // Token: 0x0600032B RID: 811 RVA: 0x000147C0 File Offset: 0x000129C0
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

        // Token: 0x0600032C RID: 812 RVA: 0x00014845 File Offset: 0x00012A45
        public virtual void timelinereachedKeyFramewithIndex(Timeline t, KeyFrame k, int i)
        {
        }

        // Token: 0x0600032D RID: 813 RVA: 0x00014848 File Offset: 0x00012A48
        public virtual void timelineFinished(Timeline t)
        {
            View view = (View)parent;
            view?.removeChild(this);
        }

        // Token: 0x0600032E RID: 814 RVA: 0x0001486B File Offset: 0x00012A6B
        public virtual void showPopup()
        {
            isShow = true;
            playTimeline(0);
        }

        // Token: 0x0600032F RID: 815 RVA: 0x0001487B File Offset: 0x00012A7B
        public virtual void hidePopup()
        {
            isShow = false;
            playTimeline(1);
        }

        // Token: 0x06000330 RID: 816 RVA: 0x0001488B File Offset: 0x00012A8B
        public override bool onTouchDownXY(float tx, float ty)
        {
            if (isShow)
            {
                _ = base.onTouchDownXY(tx, ty);
            }
            return true;
        }

        // Token: 0x06000331 RID: 817 RVA: 0x0001489F File Offset: 0x00012A9F
        public override bool onTouchUpXY(float tx, float ty)
        {
            if (isShow)
            {
                _ = base.onTouchUpXY(tx, ty);
            }
            return true;
        }

        // Token: 0x06000332 RID: 818 RVA: 0x000148B3 File Offset: 0x00012AB3
        public override bool onTouchMoveXY(float tx, float ty)
        {
            if (isShow)
            {
                _ = base.onTouchMoveXY(tx, ty);
            }
            return true;
        }

        // Token: 0x040008D8 RID: 2264
        private Image imageBackground;

        // Token: 0x040008D9 RID: 2265
        private bool isShow;

        // Token: 0x0200006B RID: 107
        private enum POPUP
        {
            // Token: 0x040008DB RID: 2267
            POPUP_SHOW_ANIM,
            // Token: 0x040008DC RID: 2268
            POPUP_HIDE_ANIM
        }
    }
}
