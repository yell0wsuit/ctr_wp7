using ctr_wp7.iframework;
using ctr_wp7.iframework.core;
using ctr_wp7.iframework.visual;
using ctr_wp7.ios;

namespace ctr_wp7.game
{
    internal sealed class Processing : RectangleElement, TimelineDelegate
    {
        private static NSObject createWithLoading()
        {
            return new Processing().initWithLoading(true);
        }

        public NSObject initWithLoading(bool loading)
        {
            if (init() != null)
            {
                width = (int)SCREEN_WIDTH_EXPANDED;
                height = (int)SCREEN_HEIGHT_EXPANDED + 1;
                x = -SCREEN_OFFSET_X;
                y = -SCREEN_OFFSET_Y;
                blendingMode = 0;
                if (loading)
                {
                    Image image = Image.Image_createWithResIDQuad(76, 0);
                    Timeline timeline = new Timeline().initWithMaxKeyFramesOnTrack(2);
                    timeline.addKeyFrame(KeyFrame.makeRotation(0, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0f));
                    timeline.addKeyFrame(KeyFrame.makeRotation(360, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 1f));
                    timeline.setTimelineLoopType(Timeline.LoopType.TIMELINE_REPLAY);
                    _ = image.addTimeline(timeline);
                    image.playTimeline(0);
                    Text text = Text.createWithFontandString(5, Application.getString(1310774));
                    HBox hbox = new HBox().initWithOffsetAlignHeight(10f, 16, image.height);
                    hbox.parentAnchor = hbox.anchor = 18;
                    _ = addChild(hbox);
                    _ = hbox.addChild(image);
                    _ = hbox.addChild(text);
                }
                Timeline timeline2 = new Timeline().initWithMaxKeyFramesOnTrack(2);
                timeline2.addKeyFrame(KeyFrame.makeColor(RGBAColor.transparentRGBA, KeyFrame.TransitionType.FRAME_TRANSITION_IMMEDIATE, 0f));
                timeline2.addKeyFrame(KeyFrame.makeColor(RGBAColor.MakeRGBA(0.0, 0.0, 0.0, 0.4), KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.2));
                _ = addTimeline(timeline2);
                timeline2 = new Timeline().initWithMaxKeyFramesOnTrack(2);
                timeline2.delegateTimelineDelegate = this;
                timeline2.addKeyFrame(KeyFrame.makeColor(RGBAColor.MakeRGBA(0.0, 0.0, 0.0, 0.4), KeyFrame.TransitionType.FRAME_TRANSITION_IMMEDIATE, 0f));
                timeline2.addKeyFrame(KeyFrame.makeColor(RGBAColor.transparentRGBA, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.2));
                _ = addTimeline(timeline2);
                playTimeline(0);
            }
            return this;
        }

        public NSObject initWithTouchesBlocking(bool b)
        {
            if (init() != null)
            {
                width = (int)SCREEN_WIDTH;
                height = (int)SCREEN_HEIGHT;
                blendingMode = 0;
                Image image = Image.Image_createWithResIDQuad(76, 0);
                Timeline timeline = new Timeline().initWithMaxKeyFramesOnTrack(2);
                timeline.addKeyFrame(KeyFrame.makeRotation(0, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0f));
                timeline.addKeyFrame(KeyFrame.makeRotation(360, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 1f));
                timeline.setTimelineLoopType(Timeline.LoopType.TIMELINE_REPLAY);
                _ = image.addTimeline(timeline);
                image.playTimeline(0);
                Text text = Text.createWithFontandString(5, Application.getString(1310752));
                HBox hbox = new HBox().initWithOffsetAlignHeight(10f, 16, image.height);
                hbox.parentAnchor = hbox.anchor = 18;
                _ = addChild(hbox);
                _ = hbox.addChild(image);
                if (LANGUAGE == Language.LANG_IT)
                {
                    text = new Text().initWithFont(Application.getFont(5));
                    text.setStringandWidth(Application.getString(1310752), 120f);
                    hbox.x -= 15f;
                }
                _ = hbox.addChild(text);
                blockTouches = b;
                color = RGBAColor.transparentRGBA;
                if (blockTouches)
                {
                    Timeline timeline2 = new Timeline().initWithMaxKeyFramesOnTrack(2);
                    timeline2.addKeyFrame(KeyFrame.makeColor(RGBAColor.transparentRGBA, KeyFrame.TransitionType.FRAME_TRANSITION_IMMEDIATE, 0f));
                    timeline2.addKeyFrame(KeyFrame.makeColor(RGBAColor.MakeRGBA(0.0, 0.0, 0.0, 0.4), KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.2));
                    _ = addTimeline(timeline2);
                    playTimeline(0);
                }
            }
            return this;
        }

        public override bool onTouchDownXY(float tx, float ty)
        {
            bool flag = base.onTouchDownXY(tx, ty);
            return blockTouches || flag;
        }

        public override bool onTouchUpXY(float tx, float ty)
        {
            bool flag = base.onTouchUpXY(tx, ty);
            return blockTouches || flag;
        }

        public override bool onTouchMoveXY(float tx, float ty)
        {
            bool flag = base.onTouchMoveXY(tx, ty);
            return blockTouches || flag;
        }

        public override void playTimeline(int t)
        {
            if (t == 0)
            {
                setEnabled(true);
            }
            base.playTimeline(t);
        }

        public void timelineFinished(Timeline t)
        {
            setEnabled(false);
        }

        public void timelinereachedKeyFramewithIndex(Timeline t, KeyFrame k, int i)
        {
        }

        private bool blockTouches = true;
    }
}
