using System.Collections.Generic;

using ctr_wp7.ctr_original;
using ctr_wp7.iframework;
using ctr_wp7.iframework.core;
using ctr_wp7.iframework.visual;

namespace ctr_wp7.game
{
    internal sealed class Ghost : BaseElement, TimelineDelegate
    {
        public Ghost initWithPositionPossibleStatesMaskGrabRadiusBouncerAngleBubblesBungeesBouncers(Vector position, int possibleStateMask, float grabRadius, float bouncerAngle, List<Bubble> bubbles, List<Grab> bungees, List<Bouncer> bouncers)
        {
            if (init() != null)
            {
                possibleStatesMask = possibleStateMask | 1;
                ghostState = 1;
                this.bouncerAngle = bouncerAngle;
                this.grabRadius = grabRadius;
                gsBubbles = bubbles;
                gsBungees = bungees;
                gsBouncers = bouncers;
                x = position.x;
                y = position.y;
                ghostImage = (BaseElement)new BaseElement().init();
                _ = addChild(ghostImage);
                morphingBubbles = (GhostMorphingParticles)new GhostMorphingParticles().initWithTotalParticles(7);
                morphingBubbles.x = position.x;
                morphingBubbles.y = position.y;
                _ = addChild(morphingBubbles);
                Timeline timeline = new Timeline().initWithMaxKeyFramesOnTrack(2);
                timeline.addKeyFrame(KeyFrame.makeColor(RGBAColor.transparentRGBA, KeyFrame.TransitionType.FRAME_TRANSITION_IMMEDIATE, 0.0));
                timeline.addKeyFrame(KeyFrame.makeColor(RGBAColor.solidOpaqueRGBA, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.36f));
                ghostImage.addTimelinewithID(timeline, 10);
                ghostImage.playTimeline(10);
                Timeline timeline2 = new Timeline().initWithMaxKeyFramesOnTrack(2);
                timeline2.addKeyFrame(KeyFrame.makeColor(RGBAColor.solidOpaqueRGBA, KeyFrame.TransitionType.FRAME_TRANSITION_IMMEDIATE, 0.0));
                timeline2.addKeyFrame(KeyFrame.makeColor(RGBAColor.transparentRGBA, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.16));
                ghostImage.addTimelinewithID(timeline2, 11);
                ghostImageBody = Image.Image_createWithResIDQuad(182, 0);
                ghostImageBody.x = position.x;
                ghostImageBody.y = position.y;
                ghostImageBody.anchor = 18;
                ghostImageBody.doRestoreCutTransparency();
                _ = ghostImage.addChild(ghostImageBody);
                float rnd_0_ = RND_0_1;
                Timeline timeline3 = new Timeline().initWithMaxKeyFramesOnTrack(2);
                timeline3.addKeyFrame(KeyFrame.makePos(x, y, KeyFrame.TransitionType.FRAME_TRANSITION_IMMEDIATE, 0.0));
                timeline3.addKeyFrame(KeyFrame.makePos(x, y - 3.0, KeyFrame.TransitionType.FRAME_TRANSITION_EASE_OUT, (double)rnd_0_));
                timeline3.delegateTimelineDelegate = this;
                ghostImageBody.addTimelinewithID(timeline3, 13);
                ghostImageBody.playTimeline(13);
                ghostImageFace = Image.Image_createWithResIDQuad(182, 1);
                ghostImageFace.x = position.x;
                ghostImageFace.y = position.y;
                ghostImageFace.anchor = 18;
                ghostImageFace.doRestoreCutTransparency();
                _ = ghostImage.addChild(ghostImageFace);
                Timeline timeline4 = new Timeline().initWithMaxKeyFramesOnTrack(2);
                timeline4.addKeyFrame(KeyFrame.makePos(x, y, KeyFrame.TransitionType.FRAME_TRANSITION_IMMEDIATE, 0.0));
                timeline4.addKeyFrame(KeyFrame.makePos(x, y - 2.0, KeyFrame.TransitionType.FRAME_TRANSITION_EASE_OUT, (double)rnd_0_ + 0.005));
                timeline4.delegateTimelineDelegate = this;
                ghostImageFace.addTimelinewithID(timeline4, 13);
                ghostImageFace.playTimeline(13);
                bubble = null;
                grab = null;
                bouncer = null;
                cyclingEnabled = true;
                candyBreak = false;
            }
            return this;
        }

        public override void dealloc()
        {
            bubble = null;
            grab = null;
            bouncer = null;
            ghostImageBody = null;
            ghostImageFace = null;
            ghostImage = null;
            morphingBubbles = null;
            morphingCloud = null;
            base.dealloc();
        }

        public override void update(float delta)
        {
            if (bubble != null && bubble.getCurrentTimelineIndex() == 11 && bubble.getCurrentTimeline().state == Timeline.TimelineState.TIMELINE_STOPPED)
            {
                _ = gsBubbles.Remove(bubble);
                bubble = null;
            }
            if (bouncer != null && bouncer.getCurrentTimelineIndex() == 11 && bouncer.getCurrentTimeline().state == Timeline.TimelineState.TIMELINE_STOPPED)
            {
                _ = gsBouncers.Remove(bouncer);
                bouncer = null;
            }
            if (grab != null && grab.getCurrentTimelineIndex() == 11 && grab.getCurrentTimeline().state == Timeline.TimelineState.TIMELINE_STOPPED)
            {
                grab.destroyRope();
                _ = gsBungees.Remove(grab);
                grab = null;
            }
            base.update(delta);
            if (grab != null && grab.rope != null && grab.rope.cut != -1 && grab.getCurrentTimelineIndex() == 10)
            {
                cyclingEnabled = true;
                resetToState(1);
            }
        }

        public void resetToState(int newState)
        {
            if ((newState & possibleStatesMask) == 0)
            {
                return;
            }
            ghostState = newState;
            Timeline timeline = new Timeline().initWithMaxKeyFramesOnTrack(2);
            timeline.addKeyFrame(KeyFrame.makeColor(RGBAColor.solidOpaqueRGBA, KeyFrame.TransitionType.FRAME_TRANSITION_IMMEDIATE, 0.0));
            timeline.addKeyFrame(KeyFrame.makeColor(RGBAColor.transparentRGBA, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.16));
            timeline.delegateTimelineDelegate = this;
            if (bubble != null)
            {
                if (bubble.getCurrentTimelineIndex() == 11)
                {
                    _ = gsBubbles.Remove(bubble);
                    bubble = null;
                }
                else
                {
                    bubble.addTimelinewithID(timeline, 11);
                    bubble.playTimeline(11);
                    bubble.popped = true;
                }
            }
            if (grab != null)
            {
                Bungee rope = grab.rope;
                if (rope != null)
                {
                    grab.rope.forceWhite = true;
                    rope.cutTime = 0.36f;
                    if (rope.cut == -1)
                    {
                        rope.cut = 0;
                    }
                }
                if (grab.getCurrentTimelineIndex() == 11)
                {
                    grab.destroyRope();
                    _ = gsBungees.Remove(grab);
                    grab = null;
                }
                else
                {
                    grab.addTimelinewithID(timeline, 11);
                    grab.playTimeline(11);
                }
            }
            if (bouncer != null)
            {
                if (bouncer.getCurrentTimelineIndex() == 11)
                {
                    _ = gsBouncers.Remove(bouncer);
                    bouncer = null;
                }
                else
                {
                    bouncer.addTimelinewithID(timeline, 11);
                    bouncer.playTimeline(11);
                }
            }
            if (ghostImage.getCurrentTimelineIndex() == 10)
            {
                ghostImage.playTimeline(11);
            }
            Timeline timeline2 = new Timeline().initWithMaxKeyFramesOnTrack(2);
            timeline2.addKeyFrame(KeyFrame.makeColor(RGBAColor.transparentRGBA, KeyFrame.TransitionType.FRAME_TRANSITION_IMMEDIATE, 0.0));
            timeline2.addKeyFrame(KeyFrame.makeColor(RGBAColor.solidOpaqueRGBA, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.36f));
            int num = ghostState;
            switch (num)
            {
                case 1:
                    ghostImage.playTimeline(10);
                    break;
                case 2:
                    {
                        GhostBubble ghostBubble = GhostBubble.createWithResIDQuad(124, RND_RANGE(1, 3));
                        ghostBubble.doRestoreCutTransparency();
                        ghostBubble.bb = MakeRectangle(0.0, 0.0, 57.0, 57.0);
                        ghostBubble.x = x;
                        ghostBubble.y = y;
                        ghostBubble.anchor = 18;
                        ghostBubble.popped = false;
                        Image image = Image.Image_createWithResIDQuad(124, 0);
                        image.doRestoreCutTransparency();
                        image.parentAnchor = image.anchor = 18;
                        _ = ghostBubble.addChild(image);
                        bubble = ghostBubble;
                        gsBubbles.Add(ghostBubble);
                        ghostBubble.passColorToChilds = true;
                        ghostBubble.addTimelinewithID(timeline2, 10);
                        ghostBubble.playTimeline(10);
                        ghostBubble.addSupportingCloudsTimelines();
                        break;
                    }
                case 3:
                    break;
                case 4:
                    grab = (Grab)new GhostGrab().initWithPositionXPositionY(x, y);
                    grab.wheel = false;
                    grab.spider = null;
                    grab.setRadius(grabRadius);
                    gsBungees.Add(grab);
                    grab.addTimelinewithID(timeline2, 10);
                    grab.playTimeline(10);
                    break;
                default:
                    if (num == 8)
                    {
                        bouncer = (Bouncer)new GhostBouncer().initWithPosXYWidthAndAngle(x, y, 1, bouncerAngle);
                        gsBouncers.Add(bouncer);
                        bouncer.addTimelinewithID(timeline2, 10);
                        bouncer.playTimeline(10);
                    }
                    break;
            }
            morphingBubbles.startSystem(7);
            CTRSoundMgr._playSound(60);
        }

        public void resetToNextState()
        {
            int num = ghostState;
            do
            {
                num <<= 1;
                if (num == 16)
                {
                    num = 2;
                }
            }
            while ((num & possibleStatesMask) == 0);
            resetToState(num);
        }

        public override bool onTouchDownXY(float tx, float ty)
        {
            float num = vectLength(vectSub(vect(tx, ty), vect(x, y)));
            if (cyclingEnabled && !candyBreak && num < 40f)
            {
                resetToNextState();
                return true;
            }
            return false;
        }

        public void timelinereachedKeyFramewithIndex(Timeline t, KeyFrame k, int i)
        {
        }

        public void timelineFinished(Timeline t)
        {
            if (t.element == ghostImageFace)
            {
                Timeline timeline = new Timeline().initWithMaxKeyFramesOnTrack(5);
                timeline.setTimelineLoopType(Timeline.LoopType.TIMELINE_REPLAY);
                timeline.addKeyFrame(KeyFrame.makePos(x, y - 2.0, KeyFrame.TransitionType.FRAME_TRANSITION_IMMEDIATE, 0.0));
                timeline.addKeyFrame(KeyFrame.makePos(x, y, KeyFrame.TransitionType.FRAME_TRANSITION_EASE_IN, 0.38));
                timeline.addKeyFrame(KeyFrame.makePos(x, y + 2.0, KeyFrame.TransitionType.FRAME_TRANSITION_EASE_OUT, 0.38));
                timeline.addKeyFrame(KeyFrame.makePos(x, y, KeyFrame.TransitionType.FRAME_TRANSITION_EASE_IN, 0.38));
                timeline.addKeyFrame(KeyFrame.makePos(x, y - 2.0, KeyFrame.TransitionType.FRAME_TRANSITION_EASE_OUT, 0.38));
                ghostImageFace.addTimelinewithID(timeline, 12);
                ghostImageFace.playTimeline(12);
            }
            if (t.element == ghostImageBody)
            {
                Timeline timeline2 = new Timeline().initWithMaxKeyFramesOnTrack(5);
                timeline2.addKeyFrame(KeyFrame.makePos(x, y - 3.0, KeyFrame.TransitionType.FRAME_TRANSITION_IMMEDIATE, 0.0));
                timeline2.addKeyFrame(KeyFrame.makePos(x, y, KeyFrame.TransitionType.FRAME_TRANSITION_EASE_IN, 0.38));
                timeline2.addKeyFrame(KeyFrame.makePos(x, y + 3.0, KeyFrame.TransitionType.FRAME_TRANSITION_EASE_OUT, 0.38));
                timeline2.addKeyFrame(KeyFrame.makePos(x, y, KeyFrame.TransitionType.FRAME_TRANSITION_EASE_IN, 0.38));
                timeline2.addKeyFrame(KeyFrame.makePos(x, y - 3.0, KeyFrame.TransitionType.FRAME_TRANSITION_EASE_OUT, 0.38));
                timeline2.setTimelineLoopType(Timeline.LoopType.TIMELINE_REPLAY);
                ghostImageBody.addTimelinewithID(timeline2, 12);
                ghostImageBody.playTimeline(12);
            }
        }

        private const float GHOST_MORPHING_APPEAR_TIME = 0.36f;

        private const double GHOST_MORPHING_DISAPPEAR_TIME = 0.16;

        private const int GHOST_MORPHING_BUBBLES_COUNT = 7;

        private const float GHOST_TOUCH_RADIUS = 40f;

        public int ghostState;

        public Bubble bubble;

        public Grab grab;

        public Bouncer bouncer;

        public bool cyclingEnabled;

        public float grabRadius;

        public bool candyBreak;

        public int possibleStatesMask;

        public float bouncerAngle;

        public BaseElement ghostImage;

        public Image ghostImageBody;

        public Image ghostImageFace;

        public List<Bubble> gsBubbles;

        public List<Grab> gsBungees;

        public List<Bouncer> gsBouncers;

        public GhostMorphingParticles morphingBubbles;

        public GhostMorphingCloud morphingCloud;
    }
}
