using System;
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
				this.possibleStatesMask = possibleStateMask | 1;
				this.ghostState = 1;
				this.bouncerAngle = bouncerAngle;
				this.grabRadius = grabRadius;
				this.gsBubbles = bubbles;
				this.gsBungees = bungees;
				this.gsBouncers = bouncers;
				this.x = position.x;
				this.y = position.y;
				this.ghostImage = (BaseElement)new BaseElement().init();
				this.addChild(this.ghostImage);
				this.morphingBubbles = (GhostMorphingParticles)new GhostMorphingParticles().initWithTotalParticles(7);
				this.morphingBubbles.x = position.x;
				this.morphingBubbles.y = position.y;
				this.addChild(this.morphingBubbles);
				Timeline timeline = new Timeline().initWithMaxKeyFramesOnTrack(2);
				timeline.addKeyFrame(KeyFrame.makeColor(RGBAColor.transparentRGBA, KeyFrame.TransitionType.FRAME_TRANSITION_IMMEDIATE, 0.0));
				timeline.addKeyFrame(KeyFrame.makeColor(RGBAColor.solidOpaqueRGBA, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.36f));
				this.ghostImage.addTimelinewithID(timeline, 10);
				this.ghostImage.playTimeline(10);
				Timeline timeline2 = new Timeline().initWithMaxKeyFramesOnTrack(2);
				timeline2.addKeyFrame(KeyFrame.makeColor(RGBAColor.solidOpaqueRGBA, KeyFrame.TransitionType.FRAME_TRANSITION_IMMEDIATE, 0.0));
				timeline2.addKeyFrame(KeyFrame.makeColor(RGBAColor.transparentRGBA, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.16));
				this.ghostImage.addTimelinewithID(timeline2, 11);
				this.ghostImageBody = Image.Image_createWithResIDQuad(182, 0);
				this.ghostImageBody.x = position.x;
				this.ghostImageBody.y = position.y;
				this.ghostImageBody.anchor = 18;
				this.ghostImageBody.doRestoreCutTransparency();
				this.ghostImage.addChild(this.ghostImageBody);
				float rnd_0_ = MathHelper.RND_0_1;
				Timeline timeline3 = new Timeline().initWithMaxKeyFramesOnTrack(2);
				timeline3.addKeyFrame(KeyFrame.makePos((double)this.x, (double)this.y, KeyFrame.TransitionType.FRAME_TRANSITION_IMMEDIATE, 0.0));
				timeline3.addKeyFrame(KeyFrame.makePos((double)this.x, (double)this.y - 3.0, KeyFrame.TransitionType.FRAME_TRANSITION_EASE_OUT, (double)rnd_0_));
				timeline3.delegateTimelineDelegate = this;
				this.ghostImageBody.addTimelinewithID(timeline3, 13);
				this.ghostImageBody.playTimeline(13);
				this.ghostImageFace = Image.Image_createWithResIDQuad(182, 1);
				this.ghostImageFace.x = position.x;
				this.ghostImageFace.y = position.y;
				this.ghostImageFace.anchor = 18;
				this.ghostImageFace.doRestoreCutTransparency();
				this.ghostImage.addChild(this.ghostImageFace);
				Timeline timeline4 = new Timeline().initWithMaxKeyFramesOnTrack(2);
				timeline4.addKeyFrame(KeyFrame.makePos((double)this.x, (double)this.y, KeyFrame.TransitionType.FRAME_TRANSITION_IMMEDIATE, 0.0));
				timeline4.addKeyFrame(KeyFrame.makePos((double)this.x, (double)this.y - 2.0, KeyFrame.TransitionType.FRAME_TRANSITION_EASE_OUT, (double)rnd_0_ + 0.005));
				timeline4.delegateTimelineDelegate = this;
				this.ghostImageFace.addTimelinewithID(timeline4, 13);
				this.ghostImageFace.playTimeline(13);
				this.bubble = null;
				this.grab = null;
				this.bouncer = null;
				this.cyclingEnabled = true;
				this.candyBreak = false;
			}
			return this;
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00009454 File Offset: 0x00007654
		public override void dealloc()
		{
			this.bubble = null;
			this.grab = null;
			this.bouncer = null;
			this.ghostImageBody = null;
			this.ghostImageFace = null;
			this.ghostImage = null;
			this.morphingBubbles = null;
			this.morphingCloud = null;
			base.dealloc();
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00009494 File Offset: 0x00007694
		public override void update(float delta)
		{
			if (this.bubble != null && this.bubble.getCurrentTimelineIndex() == 11 && this.bubble.getCurrentTimeline().state == Timeline.TimelineState.TIMELINE_STOPPED)
			{
				this.gsBubbles.Remove(this.bubble);
				this.bubble = null;
			}
			if (this.bouncer != null && this.bouncer.getCurrentTimelineIndex() == 11 && this.bouncer.getCurrentTimeline().state == Timeline.TimelineState.TIMELINE_STOPPED)
			{
				this.gsBouncers.Remove(this.bouncer);
				this.bouncer = null;
			}
			if (this.grab != null && this.grab.getCurrentTimelineIndex() == 11 && this.grab.getCurrentTimeline().state == Timeline.TimelineState.TIMELINE_STOPPED)
			{
				this.grab.destroyRope();
				this.gsBungees.Remove(this.grab);
				this.grab = null;
			}
			base.update(delta);
			if (this.grab != null && this.grab.rope != null && this.grab.rope.cut != -1 && this.grab.getCurrentTimelineIndex() == 10)
			{
				this.cyclingEnabled = true;
				this.resetToState(1);
			}
		}

		// Token: 0x06000113 RID: 275 RVA: 0x000095C0 File Offset: 0x000077C0
		public virtual void resetToState(int newState)
		{
			if ((newState & this.possibleStatesMask) == 0)
			{
				return;
			}
			this.ghostState = newState;
			Timeline timeline = new Timeline().initWithMaxKeyFramesOnTrack(2);
			timeline.addKeyFrame(KeyFrame.makeColor(RGBAColor.solidOpaqueRGBA, KeyFrame.TransitionType.FRAME_TRANSITION_IMMEDIATE, 0.0));
			timeline.addKeyFrame(KeyFrame.makeColor(RGBAColor.transparentRGBA, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.16));
			timeline.delegateTimelineDelegate = this;
			if (this.bubble != null)
			{
				if (this.bubble.getCurrentTimelineIndex() == 11)
				{
					this.gsBubbles.Remove(this.bubble);
					this.bubble = null;
				}
				else
				{
					this.bubble.addTimelinewithID(timeline, 11);
					this.bubble.playTimeline(11);
					this.bubble.popped = true;
				}
			}
			if (this.grab != null)
			{
				Bungee rope = this.grab.rope;
				if (rope != null)
				{
					this.grab.rope.forceWhite = true;
					rope.cutTime = 0.36f;
					if (rope.cut == -1)
					{
						rope.cut = 0;
					}
				}
				if (this.grab.getCurrentTimelineIndex() == 11)
				{
					this.grab.destroyRope();
					this.gsBungees.Remove(this.grab);
					this.grab = null;
				}
				else
				{
					this.grab.addTimelinewithID(timeline, 11);
					this.grab.playTimeline(11);
				}
			}
			if (this.bouncer != null)
			{
				if (this.bouncer.getCurrentTimelineIndex() == 11)
				{
					this.gsBouncers.Remove(this.bouncer);
					this.bouncer = null;
				}
				else
				{
					this.bouncer.addTimelinewithID(timeline, 11);
					this.bouncer.playTimeline(11);
				}
			}
			if (this.ghostImage.getCurrentTimelineIndex() == 10)
			{
				this.ghostImage.playTimeline(11);
			}
			Timeline timeline2 = new Timeline().initWithMaxKeyFramesOnTrack(2);
			timeline2.addKeyFrame(KeyFrame.makeColor(RGBAColor.transparentRGBA, KeyFrame.TransitionType.FRAME_TRANSITION_IMMEDIATE, 0.0));
			timeline2.addKeyFrame(KeyFrame.makeColor(RGBAColor.solidOpaqueRGBA, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.36f));
			int num = this.ghostState;
			switch (num)
			{
			case 1:
				this.ghostImage.playTimeline(10);
				break;
			case 2:
			{
				GhostBubble ghostBubble = GhostBubble.createWithResIDQuad(124, MathHelper.RND_RANGE(1, 3));
				ghostBubble.doRestoreCutTransparency();
				ghostBubble.bb = FrameworkTypes.MakeRectangle(0.0, 0.0, 57.0, 57.0);
				ghostBubble.x = this.x;
				ghostBubble.y = this.y;
				ghostBubble.anchor = 18;
				ghostBubble.popped = false;
				Image image = Image.Image_createWithResIDQuad(124, 0);
				image.doRestoreCutTransparency();
				image.parentAnchor = (image.anchor = 18);
				ghostBubble.addChild(image);
				this.bubble = ghostBubble;
				this.gsBubbles.Add(ghostBubble);
				ghostBubble.passColorToChilds = true;
				ghostBubble.addTimelinewithID(timeline2, 10);
				ghostBubble.playTimeline(10);
				ghostBubble.addSupportingCloudsTimelines();
				break;
			}
			case 3:
				break;
			case 4:
				this.grab = (Grab)new GhostGrab().initWithPositionXPositionY(this.x, this.y);
				this.grab.wheel = false;
				this.grab.spider = null;
				this.grab.setRadius(this.grabRadius);
				this.gsBungees.Add(this.grab);
				this.grab.addTimelinewithID(timeline2, 10);
				this.grab.playTimeline(10);
				break;
			default:
				if (num == 8)
				{
					this.bouncer = (Bouncer)new GhostBouncer().initWithPosXYWidthAndAngle(this.x, this.y, 1, (double)this.bouncerAngle);
					this.gsBouncers.Add(this.bouncer);
					this.bouncer.addTimelinewithID(timeline2, 10);
					this.bouncer.playTimeline(10);
				}
				break;
			}
			this.morphingBubbles.startSystem(7);
			CTRSoundMgr._playSound(60);
		}

		// Token: 0x06000114 RID: 276 RVA: 0x000099AC File Offset: 0x00007BAC
		public virtual void resetToNextState()
		{
			int num = this.ghostState;
			do
			{
				num <<= 1;
				if (num == 16)
				{
					num = 2;
				}
			}
			while ((num & this.possibleStatesMask) == 0);
			this.resetToState(num);
		}

		// Token: 0x06000115 RID: 277 RVA: 0x000099DC File Offset: 0x00007BDC
		public override bool onTouchDownXY(float tx, float ty)
		{
			float num = MathHelper.vectLength(MathHelper.vectSub(MathHelper.vect(tx, ty), MathHelper.vect(this.x, this.y)));
			if (this.cyclingEnabled && !this.candyBreak && num < 40f)
			{
				this.resetToNextState();
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
			if (t.element == this.ghostImageFace)
			{
				Timeline timeline = new Timeline().initWithMaxKeyFramesOnTrack(5);
				timeline.setTimelineLoopType(Timeline.LoopType.TIMELINE_REPLAY);
				timeline.addKeyFrame(KeyFrame.makePos((double)this.x, (double)this.y - 2.0, KeyFrame.TransitionType.FRAME_TRANSITION_IMMEDIATE, 0.0));
				timeline.addKeyFrame(KeyFrame.makePos((double)this.x, (double)this.y, KeyFrame.TransitionType.FRAME_TRANSITION_EASE_IN, 0.38));
				timeline.addKeyFrame(KeyFrame.makePos((double)this.x, (double)this.y + 2.0, KeyFrame.TransitionType.FRAME_TRANSITION_EASE_OUT, 0.38));
				timeline.addKeyFrame(KeyFrame.makePos((double)this.x, (double)this.y, KeyFrame.TransitionType.FRAME_TRANSITION_EASE_IN, 0.38));
				timeline.addKeyFrame(KeyFrame.makePos((double)this.x, (double)this.y - 2.0, KeyFrame.TransitionType.FRAME_TRANSITION_EASE_OUT, 0.38));
				this.ghostImageFace.addTimelinewithID(timeline, 12);
				this.ghostImageFace.playTimeline(12);
			}
			if (t.element == this.ghostImageBody)
			{
				Timeline timeline2 = new Timeline().initWithMaxKeyFramesOnTrack(5);
				timeline2.addKeyFrame(KeyFrame.makePos((double)this.x, (double)this.y - 3.0, KeyFrame.TransitionType.FRAME_TRANSITION_IMMEDIATE, 0.0));
				timeline2.addKeyFrame(KeyFrame.makePos((double)this.x, (double)this.y, KeyFrame.TransitionType.FRAME_TRANSITION_EASE_IN, 0.38));
				timeline2.addKeyFrame(KeyFrame.makePos((double)this.x, (double)this.y + 3.0, KeyFrame.TransitionType.FRAME_TRANSITION_EASE_OUT, 0.38));
				timeline2.addKeyFrame(KeyFrame.makePos((double)this.x, (double)this.y, KeyFrame.TransitionType.FRAME_TRANSITION_EASE_IN, 0.38));
				timeline2.addKeyFrame(KeyFrame.makePos((double)this.x, (double)this.y - 3.0, KeyFrame.TransitionType.FRAME_TRANSITION_EASE_OUT, 0.38));
				timeline2.setTimelineLoopType(Timeline.LoopType.TIMELINE_REPLAY);
				this.ghostImageBody.addTimelinewithID(timeline2, 12);
				this.ghostImageBody.playTimeline(12);
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
