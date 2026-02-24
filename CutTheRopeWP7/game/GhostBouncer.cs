using System;

using ctr_wp7.iframework;
using ctr_wp7.iframework.visual;
using ctr_wp7.ios;

namespace ctr_wp7.game
{
    internal sealed class GhostBouncer : Bouncer
    {
        public override NSObject initWithPosXYWidthAndAngle(float px, float py, int w, double an)
        {
            if (base.initWithPosXYWidthAndAngle(px, py, w, an) != null)
            {
                backCloud2 = Image_createWithResIDQuad(180, 2);
                float num = (float)Math.Sqrt(925.0);
                backCloud2.x = x + (num * (float)Math.Cos((double)DEGREES_TO_RADIANS((float)(170.0 + an))));
                backCloud2.y = y + (num * (float)Math.Sin((double)DEGREES_TO_RADIANS((float)(170.0 + an))));
                backCloud2.anchor = 18;
                backCloud2.visible = false;
                _ = addChild(backCloud2);
                Timeline timeline = new Timeline().initWithMaxKeyFramesOnTrack(5);
                timeline.setTimelineLoopType(Timeline.LoopType.TIMELINE_REPLAY);
                timeline.addKeyFrame(KeyFrame.makeScale(0.699999988079071, 0.699999988079071, KeyFrame.TransitionType.FRAME_TRANSITION_IMMEDIATE, 0.0));
                timeline.addKeyFrame(KeyFrame.makeScale(0.55f, 0.55f, KeyFrame.TransitionType.FRAME_TRANSITION_EASE_IN, 0.35f));
                timeline.addKeyFrame(KeyFrame.makeScale(0.4f, 0.4f, KeyFrame.TransitionType.FRAME_TRANSITION_EASE_OUT, 0.35f));
                timeline.addKeyFrame(KeyFrame.makeScale(0.55f, 0.55f, KeyFrame.TransitionType.FRAME_TRANSITION_EASE_IN, 0.35f));
                timeline.addKeyFrame(KeyFrame.makeScale(0.7f, 0.7f, KeyFrame.TransitionType.FRAME_TRANSITION_EASE_OUT, 0.35f));
                timeline.addKeyFrame(KeyFrame.makePos((double)(backCloud2.x + 1f), (double)(backCloud2.y + 1f), KeyFrame.TransitionType.FRAME_TRANSITION_IMMEDIATE, 0.0));
                timeline.addKeyFrame(KeyFrame.makePos((double)(backCloud2.x - 0f), (double)(backCloud2.y - 0f), KeyFrame.TransitionType.FRAME_TRANSITION_EASE_IN, 0.3499999940395355));
                timeline.addKeyFrame(KeyFrame.makePos((double)(backCloud2.x - 1f), (double)(backCloud2.y - 1f), KeyFrame.TransitionType.FRAME_TRANSITION_EASE_OUT, 0.3499999940395355));
                timeline.addKeyFrame(KeyFrame.makePos((double)(backCloud2.x + 0f), (double)(backCloud2.y + 0f), KeyFrame.TransitionType.FRAME_TRANSITION_EASE_IN, 0.3499999940395355));
                timeline.addKeyFrame(KeyFrame.makePos((double)(backCloud2.x + 1f), (double)(backCloud2.y + 1f), KeyFrame.TransitionType.FRAME_TRANSITION_EASE_OUT, 0.3499999940395355));
                backCloud2.addTimelinewithID(timeline, 0);
                backCloud2.playTimeline(0);
                backCloud = Image_createWithResIDQuad(180, 2);
                float num2 = (float)Math.Sqrt(925.0);
                backCloud.x = x + (num2 * (float)Math.Cos((double)DEGREES_TO_RADIANS((float)(10.0 + an))));
                backCloud.y = y + (num2 * (float)Math.Sin((double)DEGREES_TO_RADIANS((float)(10.0 + an))));
                backCloud.anchor = 18;
                backCloud.visible = false;
                _ = addChild(backCloud);
                Timeline timeline2 = new Timeline().initWithMaxKeyFramesOnTrack(5);
                timeline2.setTimelineLoopType(Timeline.LoopType.TIMELINE_REPLAY);
                timeline2.addKeyFrame(KeyFrame.makeScale(0.8999999761581421, 0.8999999761581421, KeyFrame.TransitionType.FRAME_TRANSITION_IMMEDIATE, 0.0));
                timeline2.addKeyFrame(KeyFrame.makeScale(0.79999995f, 0.79999995f, KeyFrame.TransitionType.FRAME_TRANSITION_EASE_IN, 0.39f));
                timeline2.addKeyFrame(KeyFrame.makeScale(0.7f, 0.7f, KeyFrame.TransitionType.FRAME_TRANSITION_EASE_OUT, 0.39f));
                timeline2.addKeyFrame(KeyFrame.makeScale(0.79999995f, 0.79999995f, KeyFrame.TransitionType.FRAME_TRANSITION_EASE_IN, 0.39f));
                timeline2.addKeyFrame(KeyFrame.makeScale(0.9f, 0.9f, KeyFrame.TransitionType.FRAME_TRANSITION_EASE_OUT, 0.39f));
                timeline2.addKeyFrame(KeyFrame.makePos((double)(backCloud.x + 1f), (double)(backCloud.y + 1f), KeyFrame.TransitionType.FRAME_TRANSITION_IMMEDIATE, 0.0));
                timeline2.addKeyFrame(KeyFrame.makePos((double)(backCloud.x - 0f), (double)(backCloud.y - 0f), KeyFrame.TransitionType.FRAME_TRANSITION_EASE_IN, 0.38999998569488525));
                timeline2.addKeyFrame(KeyFrame.makePos((double)(backCloud.x - 1f), (double)(backCloud.y - 1f), KeyFrame.TransitionType.FRAME_TRANSITION_EASE_OUT, 0.38999998569488525));
                timeline2.addKeyFrame(KeyFrame.makePos((double)(backCloud.x + 0f), (double)(backCloud.y + 0f), KeyFrame.TransitionType.FRAME_TRANSITION_EASE_IN, 0.38999998569488525));
                timeline2.addKeyFrame(KeyFrame.makePos((double)(backCloud.x + 1f), (double)(backCloud.y + 1f), KeyFrame.TransitionType.FRAME_TRANSITION_EASE_OUT, 0.38999998569488525));
                backCloud.addTimelinewithID(timeline2, 0);
                backCloud.playTimeline(0);
                Image image = Image_createWithResIDQuad(180, 1);
                image.x = x + 20f;
                image.y = y + 20f;
                image.anchor = 18;
                image.doRestoreCutTransparency();
                _ = addChild(image);
                Timeline timeline3 = new Timeline().initWithMaxKeyFramesOnTrack(5);
                timeline3.setTimelineLoopType(Timeline.LoopType.TIMELINE_REPLAY);
                timeline3.addKeyFrame(KeyFrame.makeScale(1.100000023841858, 1.100000023841858, KeyFrame.TransitionType.FRAME_TRANSITION_IMMEDIATE, 0.0));
                timeline3.addKeyFrame(KeyFrame.makeScale(1f, 1f, KeyFrame.TransitionType.FRAME_TRANSITION_EASE_IN, 0.45f));
                timeline3.addKeyFrame(KeyFrame.makeScale(0.9f, 0.9f, KeyFrame.TransitionType.FRAME_TRANSITION_EASE_OUT, 0.45f));
                timeline3.addKeyFrame(KeyFrame.makeScale(1f, 1f, KeyFrame.TransitionType.FRAME_TRANSITION_EASE_IN, 0.45f));
                timeline3.addKeyFrame(KeyFrame.makeScale(1.1f, 1.1f, KeyFrame.TransitionType.FRAME_TRANSITION_EASE_OUT, 0.45f));
                timeline3.addKeyFrame(KeyFrame.makePos((double)(image.x + 1f), (double)(image.y + 1f), KeyFrame.TransitionType.FRAME_TRANSITION_IMMEDIATE, 0.0));
                timeline3.addKeyFrame(KeyFrame.makePos((double)(image.x - 0f), (double)(image.y - 0f), KeyFrame.TransitionType.FRAME_TRANSITION_EASE_IN, 0.44999998807907104));
                timeline3.addKeyFrame(KeyFrame.makePos((double)(image.x - 1f), (double)(image.y - 1f), KeyFrame.TransitionType.FRAME_TRANSITION_EASE_OUT, 0.44999998807907104));
                timeline3.addKeyFrame(KeyFrame.makePos((double)(image.x + 0f), (double)(image.y + 0f), KeyFrame.TransitionType.FRAME_TRANSITION_EASE_IN, 0.44999998807907104));
                timeline3.addKeyFrame(KeyFrame.makePos((double)(image.x + 1f), (double)(image.y + 1f), KeyFrame.TransitionType.FRAME_TRANSITION_EASE_OUT, 0.44999998807907104));
                image.addTimelinewithID(timeline3, 0);
                image.playTimeline(0);
                Image image2 = Image_createWithResIDQuad(180, 0);
                image2.x = x - 15f;
                image2.y = y + 20f;
                image2.anchor = 18;
                image2.doRestoreCutTransparency();
                _ = addChild(image2);
                Timeline timeline4 = new Timeline().initWithMaxKeyFramesOnTrack(5);
                timeline4.setTimelineLoopType(Timeline.LoopType.TIMELINE_REPLAY);
                timeline4.addKeyFrame(KeyFrame.makeScale(1.100000023841858, 1.100000023841858, KeyFrame.TransitionType.FRAME_TRANSITION_IMMEDIATE, 0.0));
                timeline4.addKeyFrame(KeyFrame.makeScale(1f, 1f, KeyFrame.TransitionType.FRAME_TRANSITION_EASE_IN, 0.5f));
                timeline4.addKeyFrame(KeyFrame.makeScale(0.9f, 0.9f, KeyFrame.TransitionType.FRAME_TRANSITION_EASE_OUT, 0.5f));
                timeline4.addKeyFrame(KeyFrame.makeScale(1f, 1f, KeyFrame.TransitionType.FRAME_TRANSITION_EASE_IN, 0.5f));
                timeline4.addKeyFrame(KeyFrame.makeScale(1.1f, 1.1f, KeyFrame.TransitionType.FRAME_TRANSITION_EASE_OUT, 0.5f));
                timeline4.addKeyFrame(KeyFrame.makePos((double)(image2.x - 1f), (double)(image2.y + 1f), KeyFrame.TransitionType.FRAME_TRANSITION_IMMEDIATE, 0.0));
                timeline4.addKeyFrame(KeyFrame.makePos((double)(image2.x + 0f), (double)(image2.y - 0f), KeyFrame.TransitionType.FRAME_TRANSITION_EASE_IN, 0.5));
                timeline4.addKeyFrame(KeyFrame.makePos((double)(image2.x + 1f), (double)(image2.y - 1f), KeyFrame.TransitionType.FRAME_TRANSITION_EASE_OUT, 0.5));
                timeline4.addKeyFrame(KeyFrame.makePos((double)(image2.x - 0f), (double)(image2.y + 0f), KeyFrame.TransitionType.FRAME_TRANSITION_EASE_IN, 0.5));
                timeline4.addKeyFrame(KeyFrame.makePos((double)(image2.x - 1f), (double)(image2.y + 1f), KeyFrame.TransitionType.FRAME_TRANSITION_EASE_OUT, 0.5));
                image2.addTimelinewithID(timeline4, 0);
                image2.playTimeline(0);
            }
            return this;
        }

        public override void playTimeline(int t)
        {
            if (getCurrentTimelineIndex() == 11)
            {
                return;
            }
            if (t != 11 && getCurrentTimelineIndex() == 10 && getCurrentTimeline().state != Timeline.TimelineState.TIMELINE_STOPPED)
            {
                color = RGBAColor.solidOpaqueRGBA;
            }
            base.playTimeline(t);
        }

        public override void draw()
        {
            backCloud.draw();
            backCloud2.draw();
            base.draw();
        }

        public override void dealloc()
        {
            backCloud = null;
            backCloud2 = null;
            base.dealloc();
        }

        public Image backCloud;

        public Image backCloud2;
    }
}
