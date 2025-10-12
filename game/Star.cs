using System;
using ctre_wp7.iframework;
using ctre_wp7.iframework.core;
using ctre_wp7.iframework.helpers;
using ctre_wp7.iframework.visual;
using ctre_wp7.ios;

namespace ctre_wp7.game
{
	// Token: 0x02000084 RID: 132
	internal class Star : CTRGameObject
	{
		// Token: 0x060003DE RID: 990 RVA: 0x00018A01 File Offset: 0x00016C01
		public static Star Star_create(Texture2D t)
		{
			return (Star)new Star().initWithTexture(t);
		}

		// Token: 0x060003DF RID: 991 RVA: 0x00018A13 File Offset: 0x00016C13
		public static Star Star_createWithResID(int r)
		{
			return Star.Star_create(Application.getTexture(r));
		}

		// Token: 0x060003E0 RID: 992 RVA: 0x00018A20 File Offset: 0x00016C20
		public static Star Star_createWithResIDQuad(int r, int q)
		{
			Star star = Star.Star_create(Application.getTexture(r));
			star.setDrawQuad(q);
			return star;
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x00018A41 File Offset: 0x00016C41
		public override NSObject init()
		{
			if (base.init() != null)
			{
				this.timedAnim = null;
			}
			return this;
		}

		// Token: 0x060003E2 RID: 994 RVA: 0x00018A54 File Offset: 0x00016C54
		public override void update(float delta)
		{
			if ((double)this.timeout > 0.0 && (double)this.time > 0.0)
			{
				Mover.moveVariableToTarget(ref this.time, 0f, 1f, delta);
			}
			base.update(delta);
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x00018AA3 File Offset: 0x00016CA3
		public override void draw()
		{
			if (this.timedAnim != null)
			{
				this.timedAnim.draw();
			}
			base.draw();
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x00018AC0 File Offset: 0x00016CC0
		public virtual void createAnimations()
		{
			if ((double)this.timeout > 0.0)
			{
				this.timedAnim = Animation.Animation_createWithResID(127);
				this.timedAnim.anchor = (this.timedAnim.parentAnchor = 18);
				float num = this.timeout / 37f;
				this.timedAnim.addAnimationWithIDDelayLoopFirstLast(0, num, Timeline.LoopType.TIMELINE_NO_LOOP, 19, 55);
				this.timedAnim.playTimeline(0);
				this.time = this.timeout;
				this.timedAnim.visible = false;
				this.addChild(this.timedAnim);
				Timeline timeline = new Timeline().initWithMaxKeyFramesOnTrack(2);
				timeline.addKeyFrame(KeyFrame.makeColor(RGBAColor.solidOpaqueRGBA, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.0));
				timeline.addKeyFrame(KeyFrame.makeColor(RGBAColor.transparentRGBA, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.5));
				this.timedAnim.addTimelinewithID(timeline, 1);
				Timeline timeline2 = new Timeline().initWithMaxKeyFramesOnTrack(2);
				timeline2.addKeyFrame(KeyFrame.makeScale(1.0, 1.0, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.0));
				timeline2.addKeyFrame(KeyFrame.makeScale(0.0, 0.0, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.25));
				timeline2.addKeyFrame(KeyFrame.makeColor(RGBAColor.solidOpaqueRGBA, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.0));
				timeline2.addKeyFrame(KeyFrame.makeColor(RGBAColor.transparentRGBA, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.25));
				this.addTimelinewithID(timeline2, 1);
			}
			this.bb = new Rectangle(22f, 20f, 30f, 30f);
			Timeline timeline3 = new Timeline().initWithMaxKeyFramesOnTrack(5);
			timeline3.addKeyFrame(KeyFrame.makePos((int)this.x, (int)this.y, KeyFrame.TransitionType.FRAME_TRANSITION_EASE_IN, 0f));
			timeline3.addKeyFrame(KeyFrame.makePos((int)this.x, (int)this.y - 3, KeyFrame.TransitionType.FRAME_TRANSITION_EASE_OUT, 0.5f));
			timeline3.addKeyFrame(KeyFrame.makePos((int)this.x, (int)this.y, KeyFrame.TransitionType.FRAME_TRANSITION_EASE_IN, 0.5f));
			timeline3.addKeyFrame(KeyFrame.makePos((int)this.x, (int)this.y + 3, KeyFrame.TransitionType.FRAME_TRANSITION_EASE_OUT, 0.5f));
			timeline3.addKeyFrame(KeyFrame.makePos((int)this.x, (int)this.y, KeyFrame.TransitionType.FRAME_TRANSITION_EASE_IN, 0.5f));
			timeline3.setTimelineLoopType(Timeline.LoopType.TIMELINE_REPLAY);
			this.addTimelinewithID(timeline3, 0);
			this.playTimeline(0);
			Timeline.updateTimeline(timeline3, (float)((double)MathHelper.RND_RANGE(0, 20) / 10.0));
			Animation animation = Animation.Animation_createWithResID(127);
			animation.doRestoreCutTransparency();
			animation.addAnimationDelayLoopFirstLast(0.05f, Timeline.LoopType.TIMELINE_REPLAY, 1, 18);
			animation.playTimeline(0);
			Timeline.updateTimeline(animation.getTimeline(0), (float)((double)MathHelper.RND_RANGE(0, 20) / 10.0));
			animation.anchor = (animation.parentAnchor = 18);
			this.addChild(animation);
		}

		// Token: 0x0400094E RID: 2382
		public float time;

		// Token: 0x0400094F RID: 2383
		public float timeout;

		// Token: 0x04000950 RID: 2384
		public Animation timedAnim;
	}
}
