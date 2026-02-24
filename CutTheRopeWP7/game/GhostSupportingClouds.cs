using ctr_wp7.iframework.core;
using ctr_wp7.iframework.visual;
using ctr_wp7.ios;

namespace ctr_wp7.game
{
    // Token: 0x0200003F RID: 63
    internal sealed class GhostSupportingClouds : BaseElement
    {
        // Token: 0x0600021C RID: 540 RVA: 0x0000DF34 File Offset: 0x0000C134
        public NSObject initWithPositionSupportedElement(Vector pos, int supEl)
        {
            if (init() != null)
            {
                Image image = Image.Image_createWithResIDQuad(180, 1);
                Image image2 = Image.Image_createWithResIDQuad(180, 0);
                switch (supEl)
                {
                    case 0:
                        image.x = pos.x + 10f;
                        image.y = pos.y + 10f;
                        image2.x = pos.x - 10f;
                        image2.y = pos.y + 10f;
                        break;
                    case 1:
                        image.x = pos.x + 10f;
                        image.y = pos.y + 10f;
                        image2.x = pos.x - 10f;
                        image2.y = pos.y + 10f;
                        break;
                    case 2:
                        image.x = pos.x + 20f;
                        image.y = pos.y + 20f;
                        image2.x = pos.x - 20f;
                        image2.y = pos.y + 20f;
                        break;
                }
                image.anchor = 18;
                image.doRestoreCutTransparency();
                _ = addChild(image);
                Timeline timeline = new Timeline().initWithMaxKeyFramesOnTrack(3);
                timeline.setTimelineLoopType(Timeline.LoopType.TIMELINE_REPLAY);
                timeline.addKeyFrame(KeyFrame.makeScale(1.1, 1.1, KeyFrame.TransitionType.FRAME_TRANSITION_IMMEDIATE, 0.0));
                timeline.addKeyFrame(KeyFrame.makeScale(0.9, 0.9, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 1.2));
                timeline.addKeyFrame(KeyFrame.makeScale(1.1, 1.1, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 1.2));
                timeline.addKeyFrame(KeyFrame.makePos((double)(image.x + 2f), (double)(image.y + 2f), KeyFrame.TransitionType.FRAME_TRANSITION_IMMEDIATE, 0.0));
                timeline.addKeyFrame(KeyFrame.makePos((double)(image.x - 2f), (double)(image.y - 2f), KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 1.2));
                timeline.addKeyFrame(KeyFrame.makePos((double)(image.x + 2f), (double)(image.y + 2f), KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 1.2));
                image.addTimelinewithID(timeline, 1);
                image.playTimeline(1);
                image2.anchor = 18;
                image2.doRestoreCutTransparency();
                _ = addChild(image2);
                Timeline timeline2 = new Timeline().initWithMaxKeyFramesOnTrack(3);
                timeline2.setTimelineLoopType(Timeline.LoopType.TIMELINE_REPLAY);
                timeline2.addKeyFrame(KeyFrame.makeScale(1.1, 1.1, KeyFrame.TransitionType.FRAME_TRANSITION_IMMEDIATE, 0.0));
                timeline2.addKeyFrame(KeyFrame.makeScale(0.9, 0.9, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 1.2));
                timeline2.addKeyFrame(KeyFrame.makeScale(1.1, 1.1, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 1.2));
                timeline2.addKeyFrame(KeyFrame.makePos((double)(image2.x - 2f), (double)(image2.y + 2f), KeyFrame.TransitionType.FRAME_TRANSITION_IMMEDIATE, 0.0));
                timeline2.addKeyFrame(KeyFrame.makePos((double)(image2.x + 2f), (double)(image2.y - 2f), KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 1.2));
                timeline2.addKeyFrame(KeyFrame.makePos((double)(image2.x - 2f), (double)(image2.y + 2f), KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 1.2));
                image2.addTimelinewithID(timeline2, 0);
                image2.playTimeline(0);
            }
            return this;
        }
    }
}
