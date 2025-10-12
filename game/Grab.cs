using System;
using ctre_wp7.ctr_original;
using ctre_wp7.iframework;
using ctre_wp7.iframework.core;
using ctre_wp7.iframework.helpers;
using ctre_wp7.iframework.visual;
using ctre_wp7.ios;

namespace ctre_wp7.game
{
	// Token: 0x020000C2 RID: 194
	internal class Grab : GameObject
	{
		// Token: 0x0600059E RID: 1438 RVA: 0x0002A728 File Offset: 0x00028928
		public override NSObject init()
		{
			if (base.init() != null)
			{
				this.rope = null;
				this.wheelOperating = -1;
				CTRRootController ctrrootController = (CTRRootController)Application.sharedRootController();
				this.baloon = ctrrootController.isSurvival();
			}
			return this;
		}

		// Token: 0x0600059F RID: 1439 RVA: 0x0002A764 File Offset: 0x00028964
		public override void update(float delta)
		{
			base.update(delta);
			if (this.launcher && this.rope != null)
			{
				this.rope.bungeeAnchor.pos = MathHelper.vect(this.x, this.y);
				this.rope.bungeeAnchor.pin = this.rope.bungeeAnchor.pos;
				if (this.launcherIncreaseSpeed)
				{
					if (Mover.moveVariableToTarget(ref this.launcherSpeed, 200f, 30f, delta))
					{
						this.launcherIncreaseSpeed = false;
					}
				}
				else if (Mover.moveVariableToTarget(ref this.launcherSpeed, 130f, 30f, delta))
				{
					this.launcherIncreaseSpeed = true;
				}
				this.mover.setMoveSpeed(this.launcherSpeed);
			}
			if (this.hideRadius)
			{
				this.radiusAlpha -= 1.5f * delta;
				if ((double)this.radiusAlpha <= 0.0)
				{
					this.radius = -1f;
					this.hideRadius = false;
				}
			}
			if (this.wheel && this.wheelDirty)
			{
				float num;
				if (this.rope != null)
				{
					num = (float)this.rope.getLength() * 0.7f;
				}
				else
				{
					num = 0f;
				}
				if (num == 0f)
				{
					this.wheelImage2.scaleX = (this.wheelImage2.scaleY = 0f);
				}
				else
				{
					this.wheelImage2.scaleX = (this.wheelImage2.scaleY = (float)Math.Max(0.0, Math.Min(1.2, 1.0 - (double)num / 700.0)));
				}
			}
			if (this.bee != null)
			{
				Vector vector = this.mover.path[this.mover.targetPoint];
				Vector pos = this.mover.pos;
				Vector vector2 = MathHelper.vectSub(vector, pos);
				float num2 = 0f;
				if (Math.Abs(vector2.x) > 15f)
				{
					num2 = ((vector2.x > 0f) ? 10f : (-10f));
				}
				Mover.moveVariableToTarget(ref this.bee.rotation, num2, 60f, delta);
			}
		}

		// Token: 0x060005A0 RID: 1440 RVA: 0x0002A9A8 File Offset: 0x00028BA8
		public override void draw()
		{
			base.preDraw();
			OpenGL.glEnable(0);
			Bungee bungee = this.rope;
			if (this.wheel)
			{
				this.wheelHighlight.visible = this.wheelOperating != -1;
				this.wheelImage3.visible = this.wheelOperating == -1;
				OpenGL.glBlendFunc(BlendingFactor.GL_ONE, BlendingFactor.GL_ONE_MINUS_SRC_ALPHA);
				this.wheelImage.draw();
			}
			OpenGL.glBlendFunc(BlendingFactor.GL_SRC_ALPHA, BlendingFactor.GL_ONE_MINUS_SRC_ALPHA);
			OpenGL.glDisable(0);
			if (bungee != null)
			{
				bungee.draw();
			}
			OpenGL.SetWhiteColor();
			OpenGL.glEnable(0);
			if ((double)this.moveLength <= 0.0)
			{
				this.front.draw();
			}
			else if (this.moverDragging != -1)
			{
				this.grabMoverHighlight.draw();
			}
			else
			{
				this.grabMover.draw();
			}
			if (this.wheel)
			{
				this.wheelImage2.draw();
			}
			base.postDraw();
		}

		// Token: 0x060005A1 RID: 1441 RVA: 0x0002AA93 File Offset: 0x00028C93
		public override void dealloc()
		{
			if (this.vertices != null)
			{
				this.vertices = null;
			}
			this.destroyRope();
			base.dealloc();
		}

		// Token: 0x060005A2 RID: 1442 RVA: 0x0002AAB0 File Offset: 0x00028CB0
		public virtual void setRope(Bungee r)
		{
			NSObject.NSREL(this.rope);
			this.rope = r;
			this.radius = -1f;
			if (this.hasSpider)
			{
				this.shouldActivate = true;
			}
		}

		// Token: 0x060005A3 RID: 1443 RVA: 0x0002AAE0 File Offset: 0x00028CE0
		public virtual void setRadius(float r)
		{
			this.radius = r;
			if (this.radius == -1f)
			{
				int num = MathHelper.RND_RANGE(125, 126);
				this.back = Image.Image_createWithResIDQuad(num, 0);
				this.back.doRestoreCutTransparency();
				this.back.anchor = (this.back.parentAnchor = 18);
				this.front = Image.Image_createWithResIDQuad(num, 1);
				this.front.anchor = (this.front.parentAnchor = 18);
				this.addChild(this.back);
				this.addChild(this.front);
				this.back.visible = false;
				this.front.visible = false;
			}
			else
			{
				this.back = Image.Image_createWithResIDQuad(122, 0);
				this.back.doRestoreCutTransparency();
				this.back.anchor = (this.back.parentAnchor = 18);
				this.front = Image.Image_createWithResIDQuad(122, 1);
				this.front.anchor = (this.front.parentAnchor = 18);
				this.addChild(this.back);
				this.addChild(this.front);
				this.back.visible = false;
				this.front.visible = false;
				this.radiusAlpha = 1f;
				this.hideRadius = false;
				this.vertexCount = (int)Math.Max(16f, this.radius);
				if (this.vertexCount % 2 != 0)
				{
					this.vertexCount++;
				}
				this.vertices = new float[this.vertexCount * 2];
				GLDrawer.calcCircle(this.x, this.y, this.radius, this.vertexCount, this.vertices);
			}
			if (this.wheel)
			{
				this.wheelImage = Image.Image_createWithResIDQuad(134, 0);
				this.wheelImage.anchor = (this.wheelImage.parentAnchor = 18);
				this.addChild(this.wheelImage);
				this.wheelImage.visible = false;
				this.wheelImage2 = Image.Image_createWithResIDQuad(134, 1);
				this.wheelImage2.passTransformationsToChilds = false;
				this.wheelHighlight = Image.Image_createWithResIDQuad(134, 2);
				this.wheelHighlight.anchor = (this.wheelHighlight.parentAnchor = 18);
				this.wheelImage2.addChild(this.wheelHighlight);
				this.wheelImage3 = Image.Image_createWithResIDQuad(134, 3);
				this.wheelImage3.anchor = (this.wheelImage3.parentAnchor = (this.wheelImage2.anchor = (this.wheelImage2.parentAnchor = 18)));
				this.wheelImage2.addChild(this.wheelImage3);
				this.addChild(this.wheelImage2);
				this.wheelImage2.visible = false;
				this.wheelDirty = true;
			}
		}

		// Token: 0x060005A4 RID: 1444 RVA: 0x0002ADD8 File Offset: 0x00028FD8
		public virtual void setMoveLengthVerticalOffset(float l, bool v, float o)
		{
			this.moveLength = l;
			this.moveVertical = v;
			this.moveOffset = o;
			if ((double)this.moveLength > 0.0)
			{
				this.moveBackground = HorizontallyTiledImage.HorizontallyTiledImage_createWithResID(142);
				this.moveBackground.setTileHorizontallyLeftCenterRight(0, 2, 1);
				this.moveBackground.width = (int)((double)l + 37.0);
				this.moveBackground.rotationCenterX = (float)(-(float)Math.Round((double)this.moveBackground.width / 2.0) + 17.0);
				this.moveBackground.x = -17f;
				this.grabMoverHighlight = Image.Image_createWithResIDQuad(142, 3);
				this.grabMoverHighlight.visible = false;
				this.grabMoverHighlight.anchor = (this.grabMoverHighlight.parentAnchor = 18);
				this.addChild(this.grabMoverHighlight);
				this.grabMover = Image.Image_createWithResIDQuad(142, 4);
				this.grabMover.visible = false;
				this.grabMover.anchor = (this.grabMover.parentAnchor = 18);
				this.addChild(this.grabMover);
				this.grabMover.addChild(this.moveBackground);
				if (this.moveVertical)
				{
					this.moveBackground.rotation = 90f;
					this.moveBackground.y = -this.moveOffset;
					this.minMoveValue = this.y - this.moveOffset;
					this.maxMoveValue = this.y + (this.moveLength - this.moveOffset);
					this.grabMover.rotation = 90f;
					this.grabMoverHighlight.rotation = 90f;
				}
				else
				{
					this.minMoveValue = this.x - this.moveOffset;
					this.maxMoveValue = this.x + (this.moveLength - this.moveOffset);
					this.moveBackground.x += -this.moveOffset;
				}
				this.moveBackground.anchor = 19;
				this.moveBackground.x += this.x;
				this.moveBackground.y += this.y;
				this.moveBackground.visible = false;
			}
			this.moverDragging = -1;
		}

		// Token: 0x060005A5 RID: 1445 RVA: 0x0002B034 File Offset: 0x00029234
		public virtual void setBee()
		{
			this.bee = Image.Image_createWithResIDQuad(148, 1);
			this.bee.blendingMode = 1;
			this.bee.doRestoreCutTransparency();
			this.bee.parentAnchor = 18;
			Animation animation = Animation.Animation_createWithResID(148);
			animation.parentAnchor = (animation.anchor = 9);
			animation.doRestoreCutTransparency();
			animation.addAnimationDelayLoopFirstLast(0.03f, Timeline.LoopType.TIMELINE_PING_PONG, 2, 4);
			animation.playTimeline(0);
			animation.jumpTo(MathHelper.RND_RANGE(0, 2));
			this.bee.addChild(animation);
			Vector quadOffset = Image.getQuadOffset(148, 0);
			this.bee.x = -quadOffset.x;
			this.bee.y = -quadOffset.y;
			this.bee.rotationCenterX = quadOffset.x - (float)(this.bee.width / 2);
			this.bee.rotationCenterY = quadOffset.y - (float)(this.bee.height / 2);
			this.bee.scaleX = (this.bee.scaleY = 0.7692308f);
			this.addChild(this.bee);
		}

		// Token: 0x060005A6 RID: 1446 RVA: 0x0002B168 File Offset: 0x00029368
		public virtual void setSpider(bool s)
		{
			this.hasSpider = s;
			this.shouldActivate = false;
			this.spiderActive = false;
			this.spider = Animation.Animation_createWithResID(94);
			this.spider.doRestoreCutTransparency();
			this.spider.anchor = 18;
			this.spider.x = this.x;
			this.spider.y = this.y;
			this.spider.visible = false;
			this.spider.addAnimationWithIDDelayLoopFirstLast(0, 0.05f, Timeline.LoopType.TIMELINE_NO_LOOP, 0, 6);
			this.spider.setDelayatIndexforAnimation(0.4f, 5, 0);
			this.spider.addAnimationWithIDDelayLoopFirstLast(1, 0.1f, Timeline.LoopType.TIMELINE_REPLAY, 7, 10);
			this.spider.switchToAnimationatEndOfAnimationDelay(1, 0, 0.05f);
			this.addChild(this.spider);
		}

		// Token: 0x060005A7 RID: 1447 RVA: 0x0002B238 File Offset: 0x00029438
		public virtual void setLauncher()
		{
			this.launcher = true;
			this.launcherIncreaseSpeed = true;
			this.launcherSpeed = 130f;
			Mover mover = new Mover().initWithPathCapacityMoveSpeedRotateSpeed(100, this.launcherSpeed, 0f);
			mover.setPathFromStringandStart(new NSString("RC30"), MathHelper.vect(this.x, this.y));
			this.setMover(mover);
			mover.start();
		}

		// Token: 0x060005A8 RID: 1448 RVA: 0x0002B2A4 File Offset: 0x000294A4
		public virtual void destroyRope()
		{
			NSObject.NSREL(this.rope);
			this.rope = null;
		}

		// Token: 0x060005A9 RID: 1449 RVA: 0x0002B2B8 File Offset: 0x000294B8
		public virtual void reCalcCircle()
		{
			GLDrawer.calcCircle(this.x, this.y, this.radius, this.vertexCount, this.vertices);
		}

		// Token: 0x060005AA RID: 1450 RVA: 0x0002B2E0 File Offset: 0x000294E0
		public virtual void drawBack()
		{
			if ((double)this.moveLength > 0.0)
			{
				this.moveBackground.draw();
			}
			else
			{
				this.back.draw();
			}
			OpenGL.glDisable(0);
			if (this.radius != -1f || this.hideRadius)
			{
				RGBAColor rgbacolor = RGBAColor.MakeRGBA(0.2, 0.5, 0.9, (double)this.radiusAlpha);
				this.drawGrabCircle(this, this.x, this.y, this.radius, this.vertexCount, rgbacolor);
			}
			OpenGL.SetWhiteColor();
			OpenGL.glEnable(0);
		}

		// Token: 0x060005AB RID: 1451 RVA: 0x0002B385 File Offset: 0x00029585
		public virtual void drawSpider()
		{
			this.spider.draw();
		}

		// Token: 0x060005AC RID: 1452 RVA: 0x0002B394 File Offset: 0x00029594
		public virtual void updateSpider(float delta)
		{
			if (this.hasSpider && this.shouldActivate)
			{
				this.shouldActivate = false;
				this.spiderActive = true;
				CTRSoundMgr._playSound(43);
				this.spider.playTimeline(0);
			}
			if (this.hasSpider && this.spiderActive)
			{
				if (this.spider.getCurrentTimelineIndex() != 0)
				{
					this.spiderPos += delta * 45f;
				}
				float num = 0f;
				bool flag = false;
				if (this.rope != null)
				{
					int i = 0;
					while (i < this.rope.drawPtsCount)
					{
						Vector vector = MathHelper.vect(this.rope.drawPts[i], this.rope.drawPts[i + 1]);
						Vector vector2 = MathHelper.vect(this.rope.drawPts[i + 2], this.rope.drawPts[i + 3]);
						float num2 = Math.Max(20f, MathHelper.vectDistance(vector, vector2));
						if (this.spiderPos >= num && (this.spiderPos < num + num2 || i > this.rope.drawPtsCount - 3))
						{
							float num3 = this.spiderPos - num;
							Vector vector3 = MathHelper.vectSub(vector2, vector);
							vector3 = MathHelper.vectMult(vector3, num3 / num2);
							this.spider.x = vector.x + vector3.x;
							this.spider.y = vector.y + vector3.y;
							if (i > this.rope.drawPtsCount - 3)
							{
								flag = true;
							}
							if (this.spider.getCurrentTimelineIndex() != 0)
							{
								this.spider.rotation = MathHelper.RADIANS_TO_DEGREES(MathHelper.vectAngleNormalized(vector3)) + 270f;
								break;
							}
							break;
						}
						else
						{
							num += num2;
							i += 2;
						}
					}
				}
				if (flag)
				{
					this.spiderPos = -1f;
				}
			}
		}

		// Token: 0x060005AD RID: 1453 RVA: 0x0002B565 File Offset: 0x00029765
		public virtual void handleWheelTouch(Vector v)
		{
			this.lastWheelTouch = v;
		}

		// Token: 0x060005AE RID: 1454 RVA: 0x0002B570 File Offset: 0x00029770
		public virtual void handleWheelRotate(Vector v)
		{
			if (this.lastWheelTouch.x - v.x == 0f && this.lastWheelTouch.y - v.y == 0f)
			{
				return;
			}
			CTRSoundMgr._playSound(46);
			float num = this.getRotateAngleForStartEndCenter(this.lastWheelTouch, v, MathHelper.vect(this.x, this.y));
			this.wheelImage2.rotation += num;
			this.wheelImage3.rotation += num;
			this.wheelHighlight.rotation += num;
			num = ((num > 0f) ? MathHelper.MIN((double)MathHelper.MAX(1.0, (double)num), 2.0) : MathHelper.MAX((double)MathHelper.MIN(-1.0, (double)num), -2.0));
			if (this.rope != null)
			{
				float num2 = (float)this.rope.getLength();
				if (num > 0f)
				{
					if ((double)num2 < 500.0)
					{
						this.rope.roll(num);
					}
				}
				else if (num != 0f && this.rope.parts.Count > 3)
				{
					this.rope.rollBack(-num);
				}
				this.wheelDirty = true;
			}
			this.lastWheelTouch = v;
		}

		// Token: 0x060005AF RID: 1455 RVA: 0x0002B6CC File Offset: 0x000298CC
		public virtual float getRotateAngleForStartEndCenter(Vector v1, Vector v2, Vector c)
		{
			Vector vector = MathHelper.vectSub(v1, c);
			Vector vector2 = MathHelper.vectSub(v2, c);
			float num = MathHelper.vectAngleNormalized(vector2) - MathHelper.vectAngleNormalized(vector);
			return MathHelper.RADIANS_TO_DEGREES(num);
		}

		// Token: 0x060005B0 RID: 1456 RVA: 0x0002B700 File Offset: 0x00029900
		protected void drawGrabCircle(Grab s, float x, float y, float radius, int vertexCount, RGBAColor color)
		{
			OpenGL.glColor4f(color.r, color.g, color.b, color.a);
			OpenGL.glDisableClientState(0);
			OpenGL.glEnableClientState(13);
			OpenGL.glColorPointer_setAdditive(s.vertexCount * 8);
			OpenGL.glVertexPointer_setAdditive(2, 5, 0, s.vertexCount * 16);
			for (int i = 0; i < s.vertexCount; i += 2)
			{
				GLDrawer.drawAntialiasedLine(s.vertices[i * 2], s.vertices[i * 2 + 1], s.vertices[i * 2 + 2], s.vertices[i * 2 + 3], 1f, color);
			}
			OpenGL.glDrawArrays(8, 0, 8);
			OpenGL.glEnableClientState(0);
			OpenGL.glDisableClientState(13);
		}

		// Token: 0x04000AF0 RID: 2800
		private const float SPIDER_SPEED = 45f;

		// Token: 0x04000AF1 RID: 2801
		public Image back;

		// Token: 0x04000AF2 RID: 2802
		public Image front;

		// Token: 0x04000AF3 RID: 2803
		public Image dot;

		// Token: 0x04000AF4 RID: 2804
		public Bungee rope;

		// Token: 0x04000AF5 RID: 2805
		public float radius;

		// Token: 0x04000AF6 RID: 2806
		public float radiusAlpha;

		// Token: 0x04000AF7 RID: 2807
		public bool hideRadius;

		// Token: 0x04000AF8 RID: 2808
		public float[] vertices;

		// Token: 0x04000AF9 RID: 2809
		public int vertexCount;

		// Token: 0x04000AFA RID: 2810
		public bool wheel;

		// Token: 0x04000AFB RID: 2811
		public Image wheelHighlight;

		// Token: 0x04000AFC RID: 2812
		public Image wheelImage;

		// Token: 0x04000AFD RID: 2813
		public Image wheelImage2;

		// Token: 0x04000AFE RID: 2814
		public Image wheelImage3;

		// Token: 0x04000AFF RID: 2815
		public int wheelOperating;

		// Token: 0x04000B00 RID: 2816
		public Vector lastWheelTouch;

		// Token: 0x04000B01 RID: 2817
		public float moveLength;

		// Token: 0x04000B02 RID: 2818
		public bool moveVertical;

		// Token: 0x04000B03 RID: 2819
		public float moveOffset;

		// Token: 0x04000B04 RID: 2820
		public HorizontallyTiledImage moveBackground;

		// Token: 0x04000B05 RID: 2821
		public Image grabMoverHighlight;

		// Token: 0x04000B06 RID: 2822
		public Image grabMover;

		// Token: 0x04000B07 RID: 2823
		public int moverDragging;

		// Token: 0x04000B08 RID: 2824
		public float minMoveValue;

		// Token: 0x04000B09 RID: 2825
		public float maxMoveValue;

		// Token: 0x04000B0A RID: 2826
		public bool hasSpider;

		// Token: 0x04000B0B RID: 2827
		public bool spiderActive;

		// Token: 0x04000B0C RID: 2828
		public Animation spider;

		// Token: 0x04000B0D RID: 2829
		public float spiderPos;

		// Token: 0x04000B0E RID: 2830
		public bool shouldActivate;

		// Token: 0x04000B0F RID: 2831
		public bool wheelDirty;

		// Token: 0x04000B10 RID: 2832
		public bool launcher;

		// Token: 0x04000B11 RID: 2833
		public float launcherSpeed;

		// Token: 0x04000B12 RID: 2834
		public bool launcherIncreaseSpeed;

		// Token: 0x04000B13 RID: 2835
		public bool baloon;

		// Token: 0x04000B14 RID: 2836
		public float initial_rotation;

		// Token: 0x04000B15 RID: 2837
		public float initial_x;

		// Token: 0x04000B16 RID: 2838
		public float initial_y;

		// Token: 0x04000B17 RID: 2839
		public RotatedCircle initial_rotatedCircle;

		// Token: 0x04000B18 RID: 2840
		public Image bee;

		// Token: 0x020000C3 RID: 195
		private enum SPIDER_ANI
		{
			// Token: 0x04000B1A RID: 2842
			SPIDER_START_ANI,
			// Token: 0x04000B1B RID: 2843
			SPIDER_WALK_ANI,
			// Token: 0x04000B1C RID: 2844
			SPIDER_BUSTED_ANI,
			// Token: 0x04000B1D RID: 2845
			SPIDER_CATCH_ANI
		}
	}
}
