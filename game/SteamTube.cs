using System;
using System.Collections.Generic;
using ctr_wp7.ctr_original;
using ctr_wp7.iframework.core;
using ctr_wp7.iframework.helpers;
using ctr_wp7.iframework.visual;
using ctr_wp7.ios;

namespace ctr_wp7.game
{
	// Token: 0x020000C0 RID: 192
	internal class SteamTube : BaseElement, TimelineDelegate
	{
		// Token: 0x06000591 RID: 1425 RVA: 0x00029EFF File Offset: 0x000280FF
		public virtual void timelinereachedKeyFramewithIndex(Timeline t, KeyFrame k, int i)
		{
		}

		// Token: 0x06000592 RID: 1426 RVA: 0x00029F04 File Offset: 0x00028104
		public SteamTube initWithPositionAngle(Vector position, float angle)
		{
			if (base.init() != null)
			{
				if (this.dd == null)
				{
					this.dd = (DelayedDispatcher)new DelayedDispatcher().init();
				}
				this.x = position.x;
				this.y = position.y;
				this.rotation = angle;
				this.anchor = 18;
				this.steamBack = null;
				this.steamFront = null;
				this.phase = 0f;
				this.steamState = 0;
				this.tube = Image.Image_createWithResIDQuad(184, 0);
				this.tube.x = position.x;
				this.tube.y = position.y;
				this.tube.anchor = 10;
				this.addChild(this.tube);
				this.valve = Image.Image_createWithResIDQuad(184, 1);
				this.valve.x = position.x;
				this.valve.y = position.y + 27f;
				this.valve.anchor = 18;
				this.addChild(this.valve);
				this.steamBack = (BaseElement)new BaseElement().init();
				this.steamFront = (BaseElement)new BaseElement().init();
				this.addChild(this.steamBack);
				this.addChild(this.steamFront);
				this.adjustSteam();
				Timeline timeline = new Timeline().initWithMaxKeyFramesOnTrack(2);
				timeline.addKeyFrame(KeyFrame.makeRotation(0, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0f));
				timeline.addKeyFrame(KeyFrame.makeRotation(180.0, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.5499999999999999));
				this.valve.addTimelinewithID(timeline, 0);
				timeline = new Timeline().initWithMaxKeyFramesOnTrack(2);
				timeline.addKeyFrame(KeyFrame.makeRotation(0, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0f));
				timeline.addKeyFrame(KeyFrame.makeRotation(-180.0, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0.5499999999999999));
				this.valve.addTimelinewithID(timeline, 1);
			}
			return this;
		}

		// Token: 0x06000593 RID: 1427 RVA: 0x0002A103 File Offset: 0x00028303
		public void drawBack()
		{
			this.preDraw();
			this.tube.draw();
			this.valve.draw();
			this.steamBack.draw();
			BaseElement.restoreTransformations(this);
		}

		// Token: 0x06000594 RID: 1428 RVA: 0x0002A132 File Offset: 0x00028332
		public void drawFront()
		{
			this.preDraw();
			this.steamFront.draw();
			BaseElement.restoreTransformations(this);
		}

		// Token: 0x06000595 RID: 1429 RVA: 0x0002A14C File Offset: 0x0002834C
		public float getCurrentHeightModulated()
		{
			float currentHeight = this.getCurrentHeight();
			return currentHeight + 1f * MathHelper.sinf(6f * this.phase);
		}

		// Token: 0x06000596 RID: 1430 RVA: 0x0002A17B File Offset: 0x0002837B
		public override void update(float delta)
		{
			base.update(delta);
			this.dd.update(delta);
			this.phase += delta;
		}

		// Token: 0x06000597 RID: 1431 RVA: 0x0002A19E File Offset: 0x0002839E
		public override void dealloc()
		{
			this.tube = null;
			this.valve = null;
			this.steamBack = null;
			this.steamFront = null;
			this.dd = null;
			base.dealloc();
		}

		// Token: 0x06000598 RID: 1432 RVA: 0x0002A1CC File Offset: 0x000283CC
		public override bool onTouchDownXY(float tx, float ty)
		{
			Vector vector = MathHelper.vectAdd(MathHelper.vect(this.x, this.y), MathHelper.vectRotate(MathHelper.vect(0f, 28f), (double)MathHelper.DEGREES_TO_RADIANS(this.rotation)));
			float num = MathHelper.vectLength(MathHelper.vectSub(MathHelper.vect(tx, ty), vector));
			if (num < 30f)
			{
				int num2 = 0;
				switch (this.steamState)
				{
				case 0:
					this.steamState++;
					num2 = 0;
					CTRSoundMgr._playSound(62);
					break;
				case 1:
					this.steamState++;
					num2 = 0;
					CTRSoundMgr._playSound(61);
					break;
				case 2:
					this.steamState = 0;
					num2 = 1;
					CTRSoundMgr._playSound(63);
					break;
				}
				this.adjustSteam();
				if (this.valve.getTimeline(0).state != Timeline.TimelineState.TIMELINE_PLAYING && this.valve.getTimeline(1).state != Timeline.TimelineState.TIMELINE_PLAYING)
				{
					this.valve.playTimeline(num2);
				}
				return true;
			}
			return false;
		}

		// Token: 0x06000599 RID: 1433 RVA: 0x0002A2CC File Offset: 0x000284CC
		public virtual void timelineFinished(Timeline t)
		{
			BaseElement element = t.element;
			element.parent.removeChild(element);
		}

		// Token: 0x0600059A RID: 1434 RVA: 0x0002A2EC File Offset: 0x000284EC
		private float getCurrentHeight()
		{
			float num = 0f;
			switch (this.steamState)
			{
			case 0:
				num = 32.9f;
				break;
			case 1:
				num = 94f;
				break;
			case 2:
				num = 141f;
				break;
			}
			return num;
		}

		// Token: 0x0600059B RID: 1435 RVA: 0x0002A334 File Offset: 0x00028534
		private void adjustSteam()
		{
			this.phase = 0f;
			if (this.steamBack != null)
			{
				Dictionary<int, BaseElement> childs = this.steamBack.getChilds();
				foreach (KeyValuePair<int, BaseElement> keyValuePair in childs)
				{
					BaseElement value = keyValuePair.Value;
					if (value != null)
					{
						value.getTimeline(0).setTimelineLoopType(Timeline.LoopType.TIMELINE_NO_LOOP);
					}
				}
			}
			if (this.steamFront != null)
			{
				Dictionary<int, BaseElement> childs2 = this.steamFront.getChilds();
				foreach (KeyValuePair<int, BaseElement> keyValuePair2 in childs2)
				{
					BaseElement value2 = keyValuePair2.Value;
					if (value2 != null)
					{
						value2.getTimeline(0).setTimelineLoopType(Timeline.LoopType.TIMELINE_NO_LOOP);
					}
				}
			}
			if (this.steamState != 3)
			{
				this.steamBack.anchor = (this.steamBack.parentAnchor = 18);
				this.steamFront.anchor = (this.steamFront.parentAnchor = 18);
				int num = 7;
				if (this.steamState == 1)
				{
					num = 14;
				}
				if (this.steamState == 2)
				{
					num = 20;
				}
				for (int i = 0; i < num; i++)
				{
					int num2 = 0;
					int num3 = 0;
					switch (i % 3)
					{
					case 0:
						num2 = 24;
						num3 = 34;
						break;
					case 1:
						num2 = 13;
						num3 = 23;
						break;
					case 2:
						num2 = 2;
						num3 = 12;
						break;
					}
					float num4 = 0.6f;
					float num5 = num4 / (float)(num3 - num2 + 1);
					float num6 = -this.getCurrentHeight();
					num6 *= 1f + 0.1f * MathHelper.RND_MINUS1_1;
					if (this.steamState == 1 && (i % 3 == 1 || i % 3 == 2))
					{
						num6 *= 0.95f;
					}
					if (this.steamState == 2 && (i % 3 == 1 || i % 3 == 2))
					{
						num6 *= 0.94f;
					}
					float num7 = 1f;
					if (i % 3 == 0)
					{
						num7 = 0f;
					}
					else if (i % 3 == 1)
					{
						num7 *= (float)this.steamState;
					}
					else if (i % 3 == 2)
					{
						num7 *= (float)(-(float)this.steamState);
					}
					Animation animation = Animation.Animation_createWithResID(184);
					animation.doRestoreCutTransparency();
					animation.addAnimationDelayLoopFirstLast(num5, Timeline.LoopType.TIMELINE_REPLAY, num2, num3);
					animation.anchor = (animation.parentAnchor = 18);
					Timeline timeline = new Timeline().initWithMaxKeyFramesOnTrack(2);
					timeline.addKeyFrame(KeyFrame.makePos(0.0, 0.0, KeyFrame.TransitionType.FRAME_TRANSITION_IMMEDIATE, 0.0));
					timeline.addKeyFrame(KeyFrame.makePos((double)num7, (double)num6, KeyFrame.TransitionType.FRAME_TRANSITION_EASE_OUT, (double)num4));
					timeline.addKeyFrame(KeyFrame.makeScale(1.0, 1.0, KeyFrame.TransitionType.FRAME_TRANSITION_IMMEDIATE, 0.0));
					timeline.addKeyFrame(KeyFrame.makeScale(1.5, 1.5, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, (double)num4));
					timeline.setTimelineLoopType(Timeline.LoopType.TIMELINE_REPLAY);
					timeline.delegateTimelineDelegate = this;
					BaseElement baseElement = new BaseElement();
					baseElement.init();
					baseElement.addTimelinewithID(timeline, 0);
					this.dd.callObjectSelectorParamafterDelay(new DelayedDispatcher.DispatchFunc(this.startPuffFloatingAndAnimation), baseElement, num4 * (float)i / (float)num);
					baseElement.addChild(animation);
					baseElement.anchor = (baseElement.parentAnchor = 18);
					baseElement.setEnabled(false);
					if (i % 3 == 0)
					{
						this.steamBack.addChild(baseElement);
					}
					else
					{
						this.steamFront.addChild(baseElement);
					}
				}
			}
		}

		// Token: 0x0600059C RID: 1436 RVA: 0x0002A6E8 File Offset: 0x000288E8
		private void startPuffFloatingAndAnimation(NSObject param)
		{
			BaseElement baseElement = (BaseElement)param;
			baseElement.setEnabled(true);
			baseElement.playTimeline(0);
			BaseElement child = baseElement.getChild(baseElement.childsCount() - 1);
			child.playTimeline(0);
		}

		// Token: 0x04000AE4 RID: 2788
		private const int STEAM_TUBE_TOUCH_RADIUS = 30;

		// Token: 0x04000AE5 RID: 2789
		private const double PUFF_LIFETIME = 0.6;

		// Token: 0x04000AE6 RID: 2790
		public int steamState;

		// Token: 0x04000AE7 RID: 2791
		private DelayedDispatcher dd;

		// Token: 0x04000AE8 RID: 2792
		private Image tube;

		// Token: 0x04000AE9 RID: 2793
		private Image valve;

		// Token: 0x04000AEA RID: 2794
		private BaseElement steamBack;

		// Token: 0x04000AEB RID: 2795
		private BaseElement steamFront;

		// Token: 0x04000AEC RID: 2796
		private float phase;

		// Token: 0x020000C1 RID: 193
		private enum STEAM_TUBE_VALVE
		{
			// Token: 0x04000AEE RID: 2798
			STEAM_TUBE_VALVE_ROTATION_CW,
			// Token: 0x04000AEF RID: 2799
			STEAM_TUBE_VALVE_ROTATION_CCW
		}
	}
}
