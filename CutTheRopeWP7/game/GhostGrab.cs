using ctr_wp7.iframework;
using ctr_wp7.iframework.core;
using ctr_wp7.iframework.visual;
using ctr_wp7.ios;

namespace ctr_wp7.game
{
    internal sealed class GhostGrab : Grab
    {
        public NSObject initWithPositionXPositionY(float px, float py)
        {
            if (init() != null)
            {
                x = px;
                y = py;
                Image image = Image_createWithResIDQuad(180, 3);
                image.x = x - 20f;
                image.y = y + 2f;
                image.anchor = 18;
                image.doRestoreCutTransparency();
                _ = addChild(image);
                Timeline timeline = new Timeline().initWithMaxKeyFramesOnTrack(5);
                timeline.setTimelineLoopType(Timeline.LoopType.TIMELINE_REPLAY);
                timeline.addKeyFrame(KeyFrame.makeScale(0.4300000071525574, 0.4300000071525574, KeyFrame.TransitionType.FRAME_TRANSITION_IMMEDIATE, 0.0));
                timeline.addKeyFrame(KeyFrame.makeScale(0.465f, 0.465f, KeyFrame.TransitionType.FRAME_TRANSITION_EASE_IN, 0.65f));
                timeline.addKeyFrame(KeyFrame.makeScale(0.5f, 0.5f, KeyFrame.TransitionType.FRAME_TRANSITION_EASE_OUT, 0.65f));
                timeline.addKeyFrame(KeyFrame.makeScale(0.465f, 0.465f, KeyFrame.TransitionType.FRAME_TRANSITION_EASE_IN, 0.65f));
                timeline.addKeyFrame(KeyFrame.makeScale(0.43f, 0.43f, KeyFrame.TransitionType.FRAME_TRANSITION_EASE_OUT, 0.65f));
                timeline.addKeyFrame(KeyFrame.makePos((double)(image.x - 1f), (double)(image.y + 1f), KeyFrame.TransitionType.FRAME_TRANSITION_IMMEDIATE, 0.0));
                timeline.addKeyFrame(KeyFrame.makePos((double)(image.x + 0f), (double)(image.y - 0f), KeyFrame.TransitionType.FRAME_TRANSITION_EASE_IN, 0.6499999761581421));
                timeline.addKeyFrame(KeyFrame.makePos((double)(image.x + 1f), (double)(image.y - 1f), KeyFrame.TransitionType.FRAME_TRANSITION_EASE_OUT, 0.6499999761581421));
                timeline.addKeyFrame(KeyFrame.makePos((double)(image.x - 0f), (double)(image.y + 0f), KeyFrame.TransitionType.FRAME_TRANSITION_EASE_IN, 0.6499999761581421));
                timeline.addKeyFrame(KeyFrame.makePos((double)(image.x - 1f), (double)(image.y + 1f), KeyFrame.TransitionType.FRAME_TRANSITION_EASE_OUT, 0.6499999761581421));
                image.addTimelinewithID(timeline, 0);
                image.playTimeline(0);
                Image image2 = Image_createWithResIDQuad(180, 2);
                image2.x = x + 18f;
                image2.y = y + 8f;
                image2.anchor = 18;
                image2.doRestoreCutTransparency();
                _ = addChild(image2);
                Timeline timeline2 = new Timeline().initWithMaxKeyFramesOnTrack(5);
                timeline2.setTimelineLoopType(Timeline.LoopType.TIMELINE_REPLAY);
                timeline2.addKeyFrame(KeyFrame.makeScale(0.8999999761581421, 0.8999999761581421, KeyFrame.TransitionType.FRAME_TRANSITION_IMMEDIATE, 0.0));
                timeline2.addKeyFrame(KeyFrame.makeScale(0.79999995f, 0.79999995f, KeyFrame.TransitionType.FRAME_TRANSITION_EASE_IN, 0.45f));
                timeline2.addKeyFrame(KeyFrame.makeScale(0.7f, 0.7f, KeyFrame.TransitionType.FRAME_TRANSITION_EASE_OUT, 0.45f));
                timeline2.addKeyFrame(KeyFrame.makeScale(0.79999995f, 0.79999995f, KeyFrame.TransitionType.FRAME_TRANSITION_EASE_IN, 0.45f));
                timeline2.addKeyFrame(KeyFrame.makeScale(0.9f, 0.9f, KeyFrame.TransitionType.FRAME_TRANSITION_EASE_OUT, 0.45f));
                timeline2.addKeyFrame(KeyFrame.makePos((double)(image2.x + 1f), (double)(image2.y + 1f), KeyFrame.TransitionType.FRAME_TRANSITION_IMMEDIATE, 0.0));
                timeline2.addKeyFrame(KeyFrame.makePos((double)(image2.x - 0f), (double)(image2.y - 0f), KeyFrame.TransitionType.FRAME_TRANSITION_EASE_IN, 0.44999998807907104));
                timeline2.addKeyFrame(KeyFrame.makePos((double)(image2.x - 1f), (double)(image2.y - 1f), KeyFrame.TransitionType.FRAME_TRANSITION_EASE_OUT, 0.44999998807907104));
                timeline2.addKeyFrame(KeyFrame.makePos((double)(image2.x + 0f), (double)(image2.y + 0f), KeyFrame.TransitionType.FRAME_TRANSITION_EASE_IN, 0.44999998807907104));
                timeline2.addKeyFrame(KeyFrame.makePos((double)(image2.x + 1f), (double)(image2.y + 1f), KeyFrame.TransitionType.FRAME_TRANSITION_EASE_OUT, 0.44999998807907104));
                image2.addTimelinewithID(timeline2, 0);
                image2.playTimeline(0);
                Image image3 = Image_createWithResIDQuad(180, 0);
                image3.x = x - 5f;
                image3.y = y + 15f;
                image3.anchor = 18;
                image3.doRestoreCutTransparency();
                _ = addChild(image3);
                Timeline timeline3 = new Timeline().initWithMaxKeyFramesOnTrack(5);
                timeline3.setTimelineLoopType(Timeline.LoopType.TIMELINE_REPLAY);
                timeline3.addKeyFrame(KeyFrame.makeScale(1.100000023841858, 1.100000023841858, KeyFrame.TransitionType.FRAME_TRANSITION_IMMEDIATE, 0.0));
                timeline3.addKeyFrame(KeyFrame.makeScale(1f, 1f, KeyFrame.TransitionType.FRAME_TRANSITION_EASE_IN, 0.5f));
                timeline3.addKeyFrame(KeyFrame.makeScale(0.9f, 0.9f, KeyFrame.TransitionType.FRAME_TRANSITION_EASE_OUT, 0.5f));
                timeline3.addKeyFrame(KeyFrame.makeScale(1f, 1f, KeyFrame.TransitionType.FRAME_TRANSITION_EASE_IN, 0.5f));
                timeline3.addKeyFrame(KeyFrame.makeScale(1.1f, 1.1f, KeyFrame.TransitionType.FRAME_TRANSITION_EASE_OUT, 0.5f));
                timeline3.addKeyFrame(KeyFrame.makePos((double)(image3.x - 1f), (double)(image3.y + 1f), KeyFrame.TransitionType.FRAME_TRANSITION_IMMEDIATE, 0.0));
                timeline3.addKeyFrame(KeyFrame.makePos((double)(image3.x + 0f), (double)(image3.y - 0f), KeyFrame.TransitionType.FRAME_TRANSITION_EASE_IN, 0.5));
                timeline3.addKeyFrame(KeyFrame.makePos((double)(image3.x + 1f), (double)(image3.y - 1f), KeyFrame.TransitionType.FRAME_TRANSITION_EASE_OUT, 0.5));
                timeline3.addKeyFrame(KeyFrame.makePos((double)(image3.x - 0f), (double)(image3.y + 0f), KeyFrame.TransitionType.FRAME_TRANSITION_EASE_IN, 0.5));
                timeline3.addKeyFrame(KeyFrame.makePos((double)(image3.x - 1f), (double)(image3.y + 1f), KeyFrame.TransitionType.FRAME_TRANSITION_EASE_OUT, 0.5));
                image3.addTimelinewithID(timeline3, 0);
                image3.playTimeline(0);
            }
            return this;
        }

        public override void drawBack()
        {
        }

        public override void draw()
        {
            if (!visible)
            {
                return;
            }
            preDraw();
            back.color = color;
            OpenGL.glBlendFunc(BlendingFactor.GL_ONE, BlendingFactor.GL_ONE_MINUS_SRC_ALPHA);
            back.draw();
            OpenGL.glBlendFunc(BlendingFactor.GL_SRC_ALPHA, BlendingFactor.GL_ONE_MINUS_SRC_ALPHA);
            OpenGL.glDisable(0);
            if (radius != -1f || hideRadius)
            {
                int pack = ((CTRRootController)Application.sharedRootController()).getPack();
                RGBAColor rgbacolor = pack == 6
                    ? RGBAColor.MakeRGBA(0.4, 0.7, 1.0, (double)(radiusAlpha * color.a))
                    : RGBAColor.MakeRGBA(0.2, 0.5, 0.9, (double)(radiusAlpha * color.a));
                drawGrabCircle(this, x, y, radius, vertexCount, rgbacolor);
            }
            OpenGL.glColor4f(1.0, 1.0, 1.0, 1.0);
            OpenGL.glEnable(0);
            OpenGL.glDisable(0);
            rope?.draw();
            OpenGL.glColor4f(1.0, 1.0, 1.0, 1.0);
            OpenGL.glEnable(0);
            OpenGL.glBlendFunc(BlendingFactor.GL_ONE, BlendingFactor.GL_ONE_MINUS_SRC_ALPHA);
            front.color = color;
            front.draw();
            postDraw();
        }
    }
}
