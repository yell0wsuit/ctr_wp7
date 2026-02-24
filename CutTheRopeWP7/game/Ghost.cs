using System.Collections.Generic;

using ctr_wp7.ctr_original;
using ctr_wp7.iframework;
using ctr_wp7.iframework.core;
using ctr_wp7.iframework.helpers;
using ctr_wp7.iframework.visual;

namespace ctr_wp7.game
{
    // Token: 0x02000015 RID: 21
    internal class Ghost : BaseElement, TimelineDelegate
    {
        // Token: 0x06000110 RID: 272 RVA: 0x000090F8 File Offset: 0x000072F8
        public virtual Ghost initWithPositionPossibleStatesMaskGrabRadiusBouncerAngleBubblesBungeesBouncers(Vector position, int possibleStateMask, float grabRadius, float bouncerAngle, List<Bubble> bubbles, List<Grab> bungees, List<Bouncer> bouncers)
        {
            if (base.init() != null)
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
                addChild(ghostImage);
                morphingBubbles = (GhostMorphingParticles)new GhostMorphingParticles().initWithTotalParticles(7);
                morphingBubbles.x = position.x;
                morphingBubbles.y = position.y;
                addChild(morphingBubbles);
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
                ghostImage.addChild(ghostImageBody);
                float rnd_0_ = RND_0_1;
                Timeline timeline3 = new Timeline().initWithMaxKeyFramesOnTrack(2);
                timeline3.addKeyFrame(KeyFrame.makePos((double)x, (double)y, KeyFrame.TransitionType.FRAME_TRANSITION_IMMEDIATE, 0.0));
                timeline3.addKeyFrame(KeyFrame.makePos((double)x, (double)y - 3.0, KeyFrame.TransitionType.FRAME_TRANSITION_EASE_OUT, (double)rnd_0_));
                timeline3.delegateTimelineDelegate = this;
                ghostImageBody.addTimelinewithID(timeline3, 13);
                ghostImageBody.playTimeline(13);
                ghostImageFace = Image.Image_createWithResIDQuad(182, 1);
                ghostImageFace.x = position.x;
                ghostImageFace.y = position.y;
                ghostImageFace.anchor = 18;
                ghostImageFace.doRestoreCutTransparency();
                ghostImage.addChild(ghostImageFace);
                Timeline timeline4 = new Timeline().initWithMaxKeyFramesOnTrack(2);
                timeline4.addKeyFrame(KeyFrame.makePos((double)x, (double)y, KeyFrame.TransitionType.FRAME_TRANSITION_IMMEDIATE, 0.0));
                timeline4.addKeyFrame(KeyFrame.makePos((double)x, (double)y - 2.0, KeyFrame.TransitionType.FRAME_TRANSITION_EASE_OUT, (double)rnd_0_ + 0.005));
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

        // Token: 0x06000111 RID: 273 RVA: 0x00009454 File Offset: 0x00007654
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

        // Token: 0x06000112 RID: 274 RVA: 0x00009494 File Offset: 0x00007694
        public override void update(float delta)
        {
            if (bubble != null && bubble.getCurrentTimelineIndex() == 11 && bubble.getCurrentTimeline().state == Timeline.TimelineState.TIMELINE_STOPPED)
            {
                gsBubbles.Remove(bubble);
                bubble = null;
            }
            if (bouncer != null && bouncer.getCurrentTimelineIndex() == 11 && bouncer.getCurrentTimeline().state == Timeline.TimelineState.TIMELINE_STOPPED)
            {
                gsBouncers.Remove(bouncer);
                bouncer = null;
            }
            if (grab != null && grab.getCurrentTimelineIndex() == 11 && grab.getCurrentTimeline().state == Timeline.TimelineState.TIMELINE_STOPPED)
            {
                grab.destroyRope();
                gsBungees.Remove(grab);
                grab = null;
            }
            base.update(delta);
            if (grab != null && grab.rope != null && grab.rope.cut != -1 && grab.getCurrentTimelineIndex() == 10)
            {
                cyclingEnabled = true;
                resetToState(1);
            }
        }

        // Token: 0x06000113 RID: 275 RVA: 0x000095C0 File Offset: 0x000077C0
        public virtual void resetToState(int newState)
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
                    gsBubbles.Remove(bubble);
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
                    gsBungees.Remove(grab);
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
                    gsBouncers.Remove(bouncer);
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
                        image.parentAnchor = (image.anchor = 18);
                        ghostBubble.addChild(image);
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
                        bouncer = (Bouncer)new GhostBouncer().initWithPosXYWidthAndAngle(x, y, 1, (double)bouncerAngle);
                        gsBouncers.Add(bouncer);
                        bouncer.addTimelinewithID(timeline2, 10);
                        bouncer.playTimeline(10);
                    }
                    break;
            }
            morphingBubbles.startSystem(7);
            CTRSoundMgr._playSound(60);
        }

        // Token: 0x06000114 RID: 276 RVA: 0x000099AC File Offset: 0x00007BAC
        public virtual void resetToNextState()
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

        // Token: 0x06000115 RID: 277 RVA: 0x000099DC File Offset: 0x00007BDC
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

        // Token: 0x06000116 RID: 278 RVA: 0x00009A2D File Offset: 0x00007C2D
        public virtual void timelinereachedKeyFramewithIndex(Timeline t, KeyFrame k, int i)
        {
        }

        // Token: 0x06000117 RID: 279 RVA: 0x00009A30 File Offset: 0x00007C30
        public virtual void timelineFinished(Timeline t)
        {
            if (t.element == ghostImageFace)
            {
                Timeline timeline = new Timeline().initWithMaxKeyFramesOnTrack(5);
                timeline.setTimelineLoopType(Timeline.LoopType.TIMELINE_REPLAY);
                timeline.addKeyFrame(KeyFrame.makePos((double)x, (double)y - 2.0, KeyFrame.TransitionType.FRAME_TRANSITION_IMMEDIATE, 0.0));
                timeline.addKeyFrame(KeyFrame.makePos((double)x, (double)y, KeyFrame.TransitionType.FRAME_TRANSITION_EASE_IN, 0.38));
                timeline.addKeyFrame(KeyFrame.makePos((double)x, (double)y + 2.0, KeyFrame.TransitionType.FRAME_TRANSITION_EASE_OUT, 0.38));
                timeline.addKeyFrame(KeyFrame.makePos((double)x, (double)y, KeyFrame.TransitionType.FRAME_TRANSITION_EASE_IN, 0.38));
                timeline.addKeyFrame(KeyFrame.makePos((double)x, (double)y - 2.0, KeyFrame.TransitionType.FRAME_TRANSITION_EASE_OUT, 0.38));
                ghostImageFace.addTimelinewithID(timeline, 12);
                ghostImageFace.playTimeline(12);
            }
            if (t.element == ghostImageBody)
            {
                Timeline timeline2 = new Timeline().initWithMaxKeyFramesOnTrack(5);
                timeline2.addKeyFrame(KeyFrame.makePos((double)x, (double)y - 3.0, KeyFrame.TransitionType.FRAME_TRANSITION_IMMEDIATE, 0.0));
                timeline2.addKeyFrame(KeyFrame.makePos((double)x, (double)y, KeyFrame.TransitionType.FRAME_TRANSITION_EASE_IN, 0.38));
                timeline2.addKeyFrame(KeyFrame.makePos((double)x, (double)y + 3.0, KeyFrame.TransitionType.FRAME_TRANSITION_EASE_OUT, 0.38));
                timeline2.addKeyFrame(KeyFrame.makePos((double)x, (double)y, KeyFrame.TransitionType.FRAME_TRANSITION_EASE_IN, 0.38));
                timeline2.addKeyFrame(KeyFrame.makePos((double)x, (double)y - 3.0, KeyFrame.TransitionType.FRAME_TRANSITION_EASE_OUT, 0.38));
                timeline2.setTimelineLoopType(Timeline.LoopType.TIMELINE_REPLAY);
                ghostImageBody.addTimelinewithID(timeline2, 12);
                ghostImageBody.playTimeline(12);
            }
        }

        // Token: 0x0400074A RID: 1866
        private const float GHOST_MORPHING_APPEAR_TIME = 0.36f;

        // Token: 0x0400074B RID: 1867
        private const double GHOST_MORPHING_DISAPPEAR_TIME = 0.16;

        // Token: 0x0400074C RID: 1868
        private const int GHOST_MORPHING_BUBBLES_COUNT = 7;

        // Token: 0x0400074D RID: 1869
        private const float GHOST_TOUCH_RADIUS = 40f;

        // Token: 0x0400074E RID: 1870
        public int ghostState;

        // Token: 0x0400074F RID: 1871
        public Bubble bubble;

        // Token: 0x04000750 RID: 1872
        public Grab grab;

        // Token: 0x04000751 RID: 1873
        public Bouncer bouncer;

        // Token: 0x04000752 RID: 1874
        public bool cyclingEnabled;

        // Token: 0x04000753 RID: 1875
        public float grabRadius;

        // Token: 0x04000754 RID: 1876
        public bool candyBreak;

        // Token: 0x04000755 RID: 1877
        public int possibleStatesMask;

        // Token: 0x04000756 RID: 1878
        public float bouncerAngle;

        // Token: 0x04000757 RID: 1879
        public BaseElement ghostImage;

        // Token: 0x04000758 RID: 1880
        public Image ghostImageBody;

        // Token: 0x04000759 RID: 1881
        public Image ghostImageFace;

        // Token: 0x0400075A RID: 1882
        public List<Bubble> gsBubbles;

        // Token: 0x0400075B RID: 1883
        public List<Grab> gsBungees;

        // Token: 0x0400075C RID: 1884
        public List<Bouncer> gsBouncers;

        // Token: 0x0400075D RID: 1885
        public GhostMorphingParticles morphingBubbles;

        // Token: 0x0400075E RID: 1886
        public GhostMorphingCloud morphingCloud;
    }
}
