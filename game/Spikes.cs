using System;
using ctr_wp7.ctr_original;
using ctr_wp7.iframework.core;
using ctr_wp7.iframework.helpers;
using ctr_wp7.iframework.visual;
using ctr_wp7.ios;
using Microsoft.Xna.Framework.Audio;

namespace ctr_wp7.game
{
	// Token: 0x02000050 RID: 80
	internal class Spikes : CTRGameObject, TimelineDelegate, ButtonDelegate
	{
		// Token: 0x06000277 RID: 631 RVA: 0x0000FE58 File Offset: 0x0000E058
		public virtual NSObject initWithPosXYWidthAndAngleToggled(float px, float py, int w, double an, int t)
		{
			int num = -1;
			if (t != -1)
			{
				num = 136 + w - 1;
			}
			else
			{
				switch (w)
				{
				case 1:
					num = 131;
					break;
				case 2:
					num = 130;
					break;
				case 3:
					num = 129;
					break;
				case 4:
					num = 123;
					break;
				case 5:
					num = 135;
					break;
				}
			}
			if (this.initWithTexture(Application.getTexture(num)) == null)
			{
				return null;
			}
			if (t > 0)
			{
				this.doRestoreCutTransparency();
				int num2 = (t - 1) * 2;
				int num3 = 1 + (t - 1) * 2;
				Image image = Image.Image_createWithResIDQuad(140, num2);
				Image image2 = Image.Image_createWithResIDQuad(140, num3);
				image.doRestoreCutTransparency();
				image2.doRestoreCutTransparency();
				this.rotateButton = new Button().initWithUpElementDownElementandID(image, image2, 0);
				this.rotateButton.delegateButtonDelegate = this;
				this.rotateButton.anchor = (this.rotateButton.parentAnchor = 18);
				this.addChild(this.rotateButton);
				Vector quadOffset = Image.getQuadOffset(140, num2);
				Vector quadSize = Image.getQuadSize(140, num2);
				Vector vector = MathHelper.vect(image.texture.preCutSize.x, image.texture.preCutSize.y);
				Vector vector2 = MathHelper.vectSub(vector, MathHelper.vectAdd(quadSize, quadOffset));
				this.rotateButton.setTouchIncreaseLeftRightTopBottom(-quadOffset.x + quadSize.x / 2f, -vector2.x + quadSize.x / 2f, -quadOffset.y + quadSize.y / 2f, -vector2.y + quadSize.y / 2f);
			}
			this.passColorToChilds = false;
			this.spikesNormal = false;
			this.origRotation = (this.rotation = (float)an);
			this.x = px;
			this.y = py;
			this.setToggled(t);
			this.updateRotation();
			if (w == 5)
			{
				this.addAnimationWithIDDelayLoopFirstLast(0, 0.05f, Timeline.LoopType.TIMELINE_REPLAY, 0, 0);
				this.addAnimationWithIDDelayLoopFirstLast(1, 0.05f, Timeline.LoopType.TIMELINE_REPLAY, 1, 4);
				this.doRestoreCutTransparency();
			}
			this.touchIndex = -1;
			return this;
		}

		// Token: 0x06000278 RID: 632 RVA: 0x00010080 File Offset: 0x0000E280
		public virtual void updateRotation()
		{
			float num;
			if (this.electro)
			{
				num = (float)(this.width - 130);
			}
			else
			{
				num = this.texture.quadRects[this.quadToDraw].w;
			}
			num /= 2f;
			this.t1.x = this.x - num;
			this.t2.x = this.x + num;
			this.t1.y = (this.t2.y = this.y - 5f);
			this.b1.x = this.t1.x;
			this.b2.x = this.t2.x;
			this.b1.y = (this.b2.y = this.y + 5f);
			this.angle = (double)MathHelper.DEGREES_TO_RADIANS(this.rotation);
			this.t1 = MathHelper.vectRotateAround(this.t1, this.angle, this.x, this.y);
			this.t2 = MathHelper.vectRotateAround(this.t2, this.angle, this.x, this.y);
			this.b1 = MathHelper.vectRotateAround(this.b1, this.angle, this.x, this.y);
			this.b2 = MathHelper.vectRotateAround(this.b2, this.angle, this.x, this.y);
		}

		// Token: 0x06000279 RID: 633 RVA: 0x00010204 File Offset: 0x0000E404
		public virtual void turnElectroOff()
		{
			this.electroOn = false;
			this.playTimeline(0);
			this.electroTimer = this.offTime;
			if (this.sndElectric != null)
			{
				this.sndElectric.Stop();
				Application.sharedSoundMgr().ClearLooped(this.sndElectric);
				this.sndElectric = null;
			}
		}

		// Token: 0x0600027A RID: 634 RVA: 0x00010255 File Offset: 0x0000E455
		public virtual void turnElectroOn()
		{
			this.electroOn = true;
			this.playTimeline(1);
			this.electroTimer = this.onTime;
			this.sndElectric = CTRSoundMgr._playSoundLooped(38);
		}

		// Token: 0x0600027B RID: 635 RVA: 0x00010280 File Offset: 0x0000E480
		public virtual void rotateSpikes()
		{
			this.spikesNormal = !this.spikesNormal;
			this.removeTimeline(2);
			float num = (float)(this.spikesNormal ? 90 : 0);
			float num2 = this.origRotation + num;
			Timeline timeline = new Timeline().initWithMaxKeyFramesOnTrack(2);
			timeline.addKeyFrame(KeyFrame.makeRotation((int)this.rotation, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, 0f));
			timeline.addKeyFrame(KeyFrame.makeRotation((int)num2, KeyFrame.TransitionType.FRAME_TRANSITION_LINEAR, Math.Abs(num2 - this.rotation) / 90f * 0.3f));
			timeline.delegateTimelineDelegate = this;
			this.addTimelinewithID(timeline, 2);
			this.playTimeline(2);
			this.updateRotationFlag = true;
			this.rotateButton.scaleX = -this.rotateButton.scaleX;
		}

		// Token: 0x0600027C RID: 636 RVA: 0x0001033A File Offset: 0x0000E53A
		public virtual void setToggled(int t)
		{
			this.toggled = t;
		}

		// Token: 0x0600027D RID: 637 RVA: 0x00010343 File Offset: 0x0000E543
		public virtual int getToggled()
		{
			return this.toggled;
		}

		// Token: 0x0600027E RID: 638 RVA: 0x0001034C File Offset: 0x0000E54C
		public override void update(float delta)
		{
			base.update(delta);
			if (this.mover != null || this.updateRotationFlag)
			{
				this.updateRotation();
			}
			if (this.electro)
			{
				if (this.electroOn)
				{
					Mover.moveVariableToTarget(ref this.electroTimer, 0f, 1f, delta);
					if ((double)this.electroTimer == 0.0)
					{
						this.turnElectroOff();
						return;
					}
				}
				else
				{
					Mover.moveVariableToTarget(ref this.electroTimer, 0f, 1f, delta);
					if ((double)this.electroTimer == 0.0)
					{
						this.turnElectroOn();
					}
				}
			}
		}

		// Token: 0x0600027F RID: 639 RVA: 0x000103E5 File Offset: 0x0000E5E5
		public virtual void timelineReachedKeyFramewithIndex(Timeline t, KeyFrame k, int i)
		{
		}

		// Token: 0x06000280 RID: 640 RVA: 0x000103E7 File Offset: 0x0000E5E7
		public virtual void timelineFinished(Timeline t)
		{
			this.updateRotationFlag = false;
		}

		// Token: 0x06000281 RID: 641 RVA: 0x000103F0 File Offset: 0x0000E5F0
		public virtual void onButtonPressed(int n)
		{
			if (n != 0)
			{
				return;
			}
			this.delegateRotateAllSpikesWithID(this.toggled);
			if (this.spikesNormal)
			{
				CTRSoundMgr._playSound(53);
				return;
			}
			CTRSoundMgr._playSound(54);
		}

		// Token: 0x06000282 RID: 642 RVA: 0x0001042C File Offset: 0x0000E62C
		public virtual void timelinereachedKeyFramewithIndex(Timeline t, KeyFrame k, int i)
		{
		}

		// Token: 0x0400086A RID: 2154
		private const float SPIKES_HEIGHT = 10f;

		// Token: 0x0400086B RID: 2155
		private int toggled;

		// Token: 0x0400086C RID: 2156
		public double angle;

		// Token: 0x0400086D RID: 2157
		public Vector t1;

		// Token: 0x0400086E RID: 2158
		public Vector t2;

		// Token: 0x0400086F RID: 2159
		public Vector b1;

		// Token: 0x04000870 RID: 2160
		public Vector b2;

		// Token: 0x04000871 RID: 2161
		public bool electro;

		// Token: 0x04000872 RID: 2162
		public float initialDelay;

		// Token: 0x04000873 RID: 2163
		public float onTime;

		// Token: 0x04000874 RID: 2164
		public float offTime;

		// Token: 0x04000875 RID: 2165
		public bool electroOn;

		// Token: 0x04000876 RID: 2166
		public float electroTimer;

		// Token: 0x04000877 RID: 2167
		private bool updateRotationFlag;

		// Token: 0x04000878 RID: 2168
		private bool spikesNormal;

		// Token: 0x04000879 RID: 2169
		private float origRotation;

		// Token: 0x0400087A RID: 2170
		public Button rotateButton;

		// Token: 0x0400087B RID: 2171
		public int touchIndex;

		// Token: 0x0400087C RID: 2172
		public Spikes.rotateAllSpikesWithID delegateRotateAllSpikesWithID;

		// Token: 0x0400087D RID: 2173
		private SoundEffectInstance sndElectric;

		// Token: 0x02000051 RID: 81
		private enum SPIKES_ANIM
		{
			// Token: 0x0400087F RID: 2175
			SPIKES_ANIM_ELECTRODES_BASE,
			// Token: 0x04000880 RID: 2176
			SPIKES_ANIM_ELECTRODES_ELECTRIC,
			// Token: 0x04000881 RID: 2177
			SPIKES_ANIM_ROTATION_ADJUSTED
		}

		// Token: 0x02000052 RID: 82
		private enum SPIKES_ROTATION
		{
			// Token: 0x04000883 RID: 2179
			SPIKES_ROTATION_BUTTON
		}

		// Token: 0x02000053 RID: 83
		// (Invoke) Token: 0x06000285 RID: 645
		public delegate void rotateAllSpikesWithID(int sid);
	}
}
