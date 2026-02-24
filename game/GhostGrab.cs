using System;
using ctr_wp7.iframework;
using ctr_wp7.iframework.core;
using ctr_wp7.iframework.visual;
using ctr_wp7.ios;

namespace ctr_wp7.game
{
	// Token: 0x020000C4 RID: 196
	internal class GhostGrab : Grab
	{
		// Token: 0x060005B2 RID: 1458 RVA: 0x0002B7C4 File Offset: 0x000299C4
		public virtual NSObject initWithPositionXPositionY(float px, float py)
		{
			if (base.init() != null)
			{
				this.x = px;
				this.y = py;
				Image image = Image.Image_createWithResIDQuad(180, 3);
				image.x = this.x - 20f;
				image.y = this.y + 2f;
				image.anchor = 18;
				image.doRestoreCutTransparency();
				this.addChild(image);
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
				Image image2 = Image.Image_createWithResIDQuad(180, 2);
				image2.x = this.x + 18f;
				image2.y = this.y + 8f;
				image2.anchor = 18;
				image2.doRestoreCutTransparency();
				this.addChild(image2);
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
				Image image3 = Image.Image_createWithResIDQuad(180, 0);
				image3.x = this.x - 5f;
				image3.y = this.y + 15f;
				image3.anchor = 18;
				image3.doRestoreCutTransparency();
				this.addChild(image3);
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

		// Token: 0x060005B3 RID: 1459 RVA: 0x0002BDBC File Offset: 0x00029FBC
		public override void drawBack()
		{
		}

		// Token: 0x060005B4 RID: 1460 RVA: 0x0002BDC0 File Offset: 0x00029FC0
		public override void draw()
		{
			if (!this.visible)
			{
				return;
			}
			this.preDraw();
			this.back.color = this.color;
			OpenGL.glBlendFunc(BlendingFactor.GL_ONE, BlendingFactor.GL_ONE_MINUS_SRC_ALPHA);
			this.back.draw();
			OpenGL.glBlendFunc(BlendingFactor.GL_SRC_ALPHA, BlendingFactor.GL_ONE_MINUS_SRC_ALPHA);
			OpenGL.glDisable(0);
			if (this.radius != -1f || this.hideRadius)
			{
				int pack = ((CTRRootController)Application.sharedRootController()).getPack();
				RGBAColor rgbacolor;
				if (pack == 6)
				{
					rgbacolor = RGBAColor.MakeRGBA(0.4, 0.7, 1.0, (double)(this.radiusAlpha * this.color.a));
				}
				else
				{
					rgbacolor = RGBAColor.MakeRGBA(0.2, 0.5, 0.9, (double)(this.radiusAlpha * this.color.a));
				}
				base.drawGrabCircle(this, this.x, this.y, this.radius, this.vertexCount, rgbacolor);
			}
			OpenGL.glColor4f(1.0, 1.0, 1.0, 1.0);
			OpenGL.glEnable(0);
			OpenGL.glDisable(0);
			if (this.rope != null)
			{
				this.rope.draw();
			}
			OpenGL.glColor4f(1.0, 1.0, 1.0, 1.0);
			OpenGL.glEnable(0);
			OpenGL.glBlendFunc(BlendingFactor.GL_ONE, BlendingFactor.GL_ONE_MINUS_SRC_ALPHA);
			this.front.color = this.color;
			this.front.draw();
			this.postDraw();
		}
	}
}
