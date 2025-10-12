using System;
using ctre_wp7.iframework.core;
using ctre_wp7.iframework.visual;

namespace ctre_wp7.game
{
	// Token: 0x02000099 RID: 153
	internal class CandyInGhostBubbleAnimation : Animation
	{
		// Token: 0x0600048C RID: 1164 RVA: 0x00020C64 File Offset: 0x0001EE64
		public static CandyInGhostBubbleAnimation CIGBAnimation_createWithResID(int r)
		{
			return CandyInGhostBubbleAnimation.CIGBAnimation_create(Application.getTexture(r));
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x00020C71 File Offset: 0x0001EE71
		public static CandyInGhostBubbleAnimation CIGBAnimation_create(Texture2D t)
		{
			return (CandyInGhostBubbleAnimation)new CandyInGhostBubbleAnimation().initWithTexture(t);
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x00020C84 File Offset: 0x0001EE84
		public static CandyInGhostBubbleAnimation CIGBAnimation_createWithResIDQuad(int r, int q)
		{
			CandyInGhostBubbleAnimation candyInGhostBubbleAnimation = CandyInGhostBubbleAnimation.CIGBAnimation_createWithResID(r);
			if (candyInGhostBubbleAnimation != null)
			{
				candyInGhostBubbleAnimation.setDrawQuad(q);
			}
			return candyInGhostBubbleAnimation;
		}

		// Token: 0x0600048F RID: 1167 RVA: 0x00020CA4 File Offset: 0x0001EEA4
		public virtual void addSupportingCloudsTimelines()
		{
			this.backCloud = Image.Image_createWithResIDQuad(180, 4);
			this.backCloud.x = this.x + 28f;
			this.backCloud.y = this.y + 8f;
			this.backCloud.anchor = (this.backCloud.parentAnchor = 18);
			this.addChild(this.backCloud);
			Timeline timeline = new Timeline().initWithMaxKeyFramesOnTrack(5);
			timeline.setTimelineLoopType(Timeline.LoopType.TIMELINE_REPLAY);
			timeline.addKeyFrame(KeyFrame.makeScale(0.800000011920929, 0.800000011920929, KeyFrame.TransitionType.FRAME_TRANSITION_IMMEDIATE, 0.0));
			timeline.addKeyFrame(KeyFrame.makeScale(0.78f, 0.78f, KeyFrame.TransitionType.FRAME_TRANSITION_EASE_IN, 0.48f));
			timeline.addKeyFrame(KeyFrame.makeScale(0.76f, 0.76f, KeyFrame.TransitionType.FRAME_TRANSITION_EASE_OUT, 0.48f));
			timeline.addKeyFrame(KeyFrame.makeScale(0.78f, 0.78f, KeyFrame.TransitionType.FRAME_TRANSITION_EASE_IN, 0.48f));
			timeline.addKeyFrame(KeyFrame.makeScale(0.8f, 0.8f, KeyFrame.TransitionType.FRAME_TRANSITION_EASE_OUT, 0.48f));
			timeline.addKeyFrame(KeyFrame.makePos((double)(this.backCloud.x + 1f), (double)(this.backCloud.y + 1f), KeyFrame.TransitionType.FRAME_TRANSITION_IMMEDIATE, 0.0));
			timeline.addKeyFrame(KeyFrame.makePos((double)(this.backCloud.x - 0f), (double)(this.backCloud.y - 0f), KeyFrame.TransitionType.FRAME_TRANSITION_EASE_IN, 0.47999998927116394));
			timeline.addKeyFrame(KeyFrame.makePos((double)(this.backCloud.x - 1f), (double)(this.backCloud.y - 1f), KeyFrame.TransitionType.FRAME_TRANSITION_EASE_OUT, 0.47999998927116394));
			timeline.addKeyFrame(KeyFrame.makePos((double)(this.backCloud.x + 0f), (double)(this.backCloud.y + 0f), KeyFrame.TransitionType.FRAME_TRANSITION_EASE_IN, 0.47999998927116394));
			timeline.addKeyFrame(KeyFrame.makePos((double)(this.backCloud.x + 1f), (double)(this.backCloud.y + 1f), KeyFrame.TransitionType.FRAME_TRANSITION_EASE_OUT, 0.47999998927116394));
			this.backCloud.addTimelinewithID(timeline, 0);
			this.backCloud.playTimeline(0);
			this.backCloud2 = Image.Image_createWithResIDQuad(180, 3);
			this.backCloud2.x = this.x + 22f;
			this.backCloud2.y = this.y + 16f;
			this.backCloud2.anchor = (this.backCloud2.parentAnchor = 18);
			this.addChild(this.backCloud2);
			Timeline timeline2 = new Timeline().initWithMaxKeyFramesOnTrack(5);
			timeline2.setTimelineLoopType(Timeline.LoopType.TIMELINE_REPLAY);
			timeline2.addKeyFrame(KeyFrame.makeScale(0.9300000071525574, 0.9300000071525574, KeyFrame.TransitionType.FRAME_TRANSITION_IMMEDIATE, 0.0));
			timeline2.addKeyFrame(KeyFrame.makeScale(0.96500003f, 0.96500003f, KeyFrame.TransitionType.FRAME_TRANSITION_EASE_IN, 0.4f));
			timeline2.addKeyFrame(KeyFrame.makeScale(1f, 1f, KeyFrame.TransitionType.FRAME_TRANSITION_EASE_OUT, 0.4f));
			timeline2.addKeyFrame(KeyFrame.makeScale(0.96500003f, 0.96500003f, KeyFrame.TransitionType.FRAME_TRANSITION_EASE_IN, 0.4f));
			timeline2.addKeyFrame(KeyFrame.makeScale(0.93f, 0.93f, KeyFrame.TransitionType.FRAME_TRANSITION_EASE_OUT, 0.4f));
			timeline2.addKeyFrame(KeyFrame.makePos((double)(this.backCloud2.x + 1f), (double)(this.backCloud2.y + 1f), KeyFrame.TransitionType.FRAME_TRANSITION_IMMEDIATE, 0.0));
			timeline2.addKeyFrame(KeyFrame.makePos((double)(this.backCloud2.x - 0f), (double)(this.backCloud2.y - 0f), KeyFrame.TransitionType.FRAME_TRANSITION_EASE_IN, 0.4000000059604645));
			timeline2.addKeyFrame(KeyFrame.makePos((double)(this.backCloud2.x - 1f), (double)(this.backCloud2.y - 1f), KeyFrame.TransitionType.FRAME_TRANSITION_EASE_OUT, 0.4000000059604645));
			timeline2.addKeyFrame(KeyFrame.makePos((double)(this.backCloud2.x + 0f), (double)(this.backCloud2.y + 0f), KeyFrame.TransitionType.FRAME_TRANSITION_EASE_IN, 0.4000000059604645));
			timeline2.addKeyFrame(KeyFrame.makePos((double)(this.backCloud2.x + 1f), (double)(this.backCloud2.y + 1f), KeyFrame.TransitionType.FRAME_TRANSITION_EASE_OUT, 0.4000000059604645));
			this.backCloud2.addTimelinewithID(timeline2, 0);
			this.backCloud2.playTimeline(0);
			this.backCloud3 = Image.Image_createWithResIDQuad(180, 3);
			this.backCloud3.x = this.x - 28f;
			this.backCloud3.y = this.y + 5f;
			this.backCloud3.anchor = (this.backCloud3.parentAnchor = 18);
			this.addChild(this.backCloud3);
			Timeline timeline3 = new Timeline().initWithMaxKeyFramesOnTrack(5);
			timeline3.setTimelineLoopType(Timeline.LoopType.TIMELINE_REPLAY);
			timeline3.addKeyFrame(KeyFrame.makeScale(0.33000001311302185, 0.33000001311302185, KeyFrame.TransitionType.FRAME_TRANSITION_IMMEDIATE, 0.0));
			timeline3.addKeyFrame(KeyFrame.makeScale(0.365f, 0.365f, KeyFrame.TransitionType.FRAME_TRANSITION_EASE_IN, 0.43f));
			timeline3.addKeyFrame(KeyFrame.makeScale(0.4f, 0.4f, KeyFrame.TransitionType.FRAME_TRANSITION_EASE_OUT, 0.43f));
			timeline3.addKeyFrame(KeyFrame.makeScale(0.365f, 0.365f, KeyFrame.TransitionType.FRAME_TRANSITION_EASE_IN, 0.43f));
			timeline3.addKeyFrame(KeyFrame.makeScale(0.33f, 0.33f, KeyFrame.TransitionType.FRAME_TRANSITION_EASE_OUT, 0.43f));
			timeline3.addKeyFrame(KeyFrame.makePos((double)(this.backCloud3.x + 1f), (double)(this.backCloud3.y + 1f), KeyFrame.TransitionType.FRAME_TRANSITION_IMMEDIATE, 0.0));
			timeline3.addKeyFrame(KeyFrame.makePos((double)(this.backCloud3.x - 0f), (double)(this.backCloud3.y - 0f), KeyFrame.TransitionType.FRAME_TRANSITION_EASE_IN, 0.4300000071525574));
			timeline3.addKeyFrame(KeyFrame.makePos((double)(this.backCloud3.x - 1f), (double)(this.backCloud3.y - 1f), KeyFrame.TransitionType.FRAME_TRANSITION_EASE_OUT, 0.4300000071525574));
			timeline3.addKeyFrame(KeyFrame.makePos((double)(this.backCloud3.x + 0f), (double)(this.backCloud3.y + 0f), KeyFrame.TransitionType.FRAME_TRANSITION_EASE_IN, 0.4300000071525574));
			timeline3.addKeyFrame(KeyFrame.makePos((double)(this.backCloud3.x + 1f), (double)(this.backCloud3.y + 1f), KeyFrame.TransitionType.FRAME_TRANSITION_EASE_OUT, 0.4300000071525574));
			this.backCloud3.addTimelinewithID(timeline3, 0);
			this.backCloud3.playTimeline(0);
			Image image = Image.Image_createWithResIDQuad(180, 4);
			image.x = this.x - 23f;
			image.y = this.y + 16f;
			image.anchor = (image.parentAnchor = 18);
			image.doRestoreCutTransparency();
			this.addChild(image);
			Timeline timeline4 = new Timeline().initWithMaxKeyFramesOnTrack(5);
			timeline4.setTimelineLoopType(Timeline.LoopType.TIMELINE_REPLAY);
			timeline4.addKeyFrame(KeyFrame.makeScale(0.6000000238418579, 0.6000000238418579, KeyFrame.TransitionType.FRAME_TRANSITION_IMMEDIATE, 0.0));
			timeline4.addKeyFrame(KeyFrame.makeScale(0.565f, 0.565f, KeyFrame.TransitionType.FRAME_TRANSITION_EASE_IN, 0.42f));
			timeline4.addKeyFrame(KeyFrame.makeScale(0.53f, 0.53f, KeyFrame.TransitionType.FRAME_TRANSITION_EASE_OUT, 0.42f));
			timeline4.addKeyFrame(KeyFrame.makeScale(0.565f, 0.565f, KeyFrame.TransitionType.FRAME_TRANSITION_EASE_IN, 0.42f));
			timeline4.addKeyFrame(KeyFrame.makeScale(0.6f, 0.6f, KeyFrame.TransitionType.FRAME_TRANSITION_EASE_OUT, 0.42f));
			timeline4.addKeyFrame(KeyFrame.makePos((double)(image.x - 1f), (double)(image.y + 1f), KeyFrame.TransitionType.FRAME_TRANSITION_IMMEDIATE, 0.0));
			timeline4.addKeyFrame(KeyFrame.makePos((double)(image.x + 0f), (double)(image.y - 0f), KeyFrame.TransitionType.FRAME_TRANSITION_EASE_IN, 0.41999998688697815));
			timeline4.addKeyFrame(KeyFrame.makePos((double)(image.x + 1f), (double)(image.y - 1f), KeyFrame.TransitionType.FRAME_TRANSITION_EASE_OUT, 0.41999998688697815));
			timeline4.addKeyFrame(KeyFrame.makePos((double)(image.x - 0f), (double)(image.y + 0f), KeyFrame.TransitionType.FRAME_TRANSITION_EASE_IN, 0.41999998688697815));
			timeline4.addKeyFrame(KeyFrame.makePos((double)(image.x - 1f), (double)(image.y + 1f), KeyFrame.TransitionType.FRAME_TRANSITION_EASE_OUT, 0.41999998688697815));
			image.addTimelinewithID(timeline4, 0);
			image.playTimeline(0);
			Image image2 = Image.Image_createWithResIDQuad(180, 0);
			image2.x = this.x - 5f;
			image2.y = this.y + 25f;
			image2.anchor = (image2.parentAnchor = 18);
			image2.doRestoreCutTransparency();
			this.addChild(image2);
			Timeline timeline5 = new Timeline().initWithMaxKeyFramesOnTrack(5);
			timeline5.setTimelineLoopType(Timeline.LoopType.TIMELINE_REPLAY);
			timeline5.addKeyFrame(KeyFrame.makeScale(0.9300000071525574, 0.9300000071525574, KeyFrame.TransitionType.FRAME_TRANSITION_IMMEDIATE, 0.0));
			timeline5.addKeyFrame(KeyFrame.makeScale(0.96500003f, 0.96500003f, KeyFrame.TransitionType.FRAME_TRANSITION_EASE_IN, 0.47f));
			timeline5.addKeyFrame(KeyFrame.makeScale(1f, 1f, KeyFrame.TransitionType.FRAME_TRANSITION_EASE_OUT, 0.47f));
			timeline5.addKeyFrame(KeyFrame.makeScale(0.96500003f, 0.96500003f, KeyFrame.TransitionType.FRAME_TRANSITION_EASE_IN, 0.47f));
			timeline5.addKeyFrame(KeyFrame.makeScale(0.93f, 0.93f, KeyFrame.TransitionType.FRAME_TRANSITION_EASE_OUT, 0.47f));
			timeline5.addKeyFrame(KeyFrame.makePos((double)(image2.x + 1f), (double)(image2.y - 1f), KeyFrame.TransitionType.FRAME_TRANSITION_IMMEDIATE, 0.0));
			timeline5.addKeyFrame(KeyFrame.makePos((double)(image2.x - 0f), (double)(image2.y + 0f), KeyFrame.TransitionType.FRAME_TRANSITION_EASE_IN, 0.4699999988079071));
			timeline5.addKeyFrame(KeyFrame.makePos((double)(image2.x - 1f), (double)(image2.y + 1f), KeyFrame.TransitionType.FRAME_TRANSITION_EASE_OUT, 0.4699999988079071));
			timeline5.addKeyFrame(KeyFrame.makePos((double)(image2.x + 0f), (double)(image2.y - 0f), KeyFrame.TransitionType.FRAME_TRANSITION_EASE_IN, 0.4699999988079071));
			timeline5.addKeyFrame(KeyFrame.makePos((double)(image2.x + 1f), (double)(image2.y - 1f), KeyFrame.TransitionType.FRAME_TRANSITION_EASE_OUT, 0.4699999988079071));
			timeline5.addKeyFrame(KeyFrame.makeRotation(350.0, KeyFrame.TransitionType.FRAME_TRANSITION_IMMEDIATE, 0.0));
			timeline5.addKeyFrame(KeyFrame.makeRotation(350.0, KeyFrame.TransitionType.FRAME_TRANSITION_IMMEDIATE, 0.0));
			timeline5.addKeyFrame(KeyFrame.makeRotation(350.0, KeyFrame.TransitionType.FRAME_TRANSITION_IMMEDIATE, 0.0));
			image2.addTimelinewithID(timeline5, 0);
			image2.playTimeline(0);
		}

		// Token: 0x06000490 RID: 1168 RVA: 0x000217EC File Offset: 0x0001F9EC
		public override void dealloc()
		{
			this.backCloud = null;
			this.backCloud2 = null;
			this.backCloud3 = null;
			base.dealloc();
		}

		// Token: 0x040009D0 RID: 2512
		public Image backCloud;

		// Token: 0x040009D1 RID: 2513
		public Image backCloud2;

		// Token: 0x040009D2 RID: 2514
		public Image backCloud3;
	}
}
