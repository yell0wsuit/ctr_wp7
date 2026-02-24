using System;
using System.Collections.Generic;
using ctr_wp7.ctr_original;
using ctr_wp7.iframework.core;
using ctr_wp7.iframework.helpers;

namespace ctr_wp7.iframework.visual
{
	// Token: 0x020000CF RID: 207
	internal class ScrollableContainer : BaseElement
	{
		// Token: 0x06000603 RID: 1539 RVA: 0x0002D8B0 File Offset: 0x0002BAB0
		public void provideScrollPosMaxScrollPosScrollCoeff(ref Vector sp, ref Vector mp, ref Vector sc)
		{
			sp = this.getScroll();
			mp = this.getMaxScroll();
			float num = (float)this.container.width / (float)this.width;
			float num2 = (float)this.container.height / (float)this.height;
			sc = MathHelper.vect(num, num2);
		}

		// Token: 0x06000604 RID: 1540 RVA: 0x0002D90C File Offset: 0x0002BB0C
		public override int addChildwithID(BaseElement c, int i)
		{
			int num = this.container.addChildwithID(c, i);
			c.parentAnchor = 9;
			return num;
		}

		// Token: 0x06000605 RID: 1541 RVA: 0x0002D930 File Offset: 0x0002BB30
		public override int addChild(BaseElement c)
		{
			c.parentAnchor = 9;
			return this.container.addChild(c);
		}

		// Token: 0x06000606 RID: 1542 RVA: 0x0002D946 File Offset: 0x0002BB46
		public override void removeChildWithID(int i)
		{
			this.container.removeChildWithID(i);
		}

		// Token: 0x06000607 RID: 1543 RVA: 0x0002D954 File Offset: 0x0002BB54
		public override void removeChild(BaseElement c)
		{
			this.container.removeChild(c);
		}

		// Token: 0x06000608 RID: 1544 RVA: 0x0002D962 File Offset: 0x0002BB62
		public override BaseElement getChild(int i)
		{
			return this.container.getChild(i);
		}

		// Token: 0x06000609 RID: 1545 RVA: 0x0002D970 File Offset: 0x0002BB70
		public override int childsCount()
		{
			return this.container.childsCount();
		}

		// Token: 0x0600060A RID: 1546 RVA: 0x0002D980 File Offset: 0x0002BB80
		public override void draw()
		{
			float x = this.container.x;
			float y = this.container.y;
			this.container.x = (float)Math.Round((double)this.container.x);
			this.container.y = (float)Math.Round((double)this.container.y);
			base.preDraw();
			OpenGL.glEnable(4);
			OpenGL.setScissorRectangle(this.drawX, this.drawY, (float)this.width, (float)this.height);
			this.postDraw();
			OpenGL.glDisable(4);
			this.container.x = x;
			this.container.y = y;
		}

		// Token: 0x0600060B RID: 1547 RVA: 0x0002DA30 File Offset: 0x0002BC30
		public override void postDraw()
		{
			if (!this.passTransformationsToChilds)
			{
				BaseElement.restoreTransformations(this);
			}
			this.container.preDraw();
			if (!this.container.passTransformationsToChilds)
			{
				BaseElement.restoreTransformations(this.container);
			}
			Dictionary<int, BaseElement> childs = this.container.getChilds();
			int i = 0;
			int count = childs.Count;
			while (i < count)
			{
				BaseElement baseElement = childs[i];
				float drawX = baseElement.drawX;
				float drawY = baseElement.drawY;
				if (baseElement != null && baseElement.visible && MathHelper.rectInRect(drawX, drawY, drawX + (float)baseElement.width, drawY + (float)baseElement.height, this.drawX, this.drawY, this.drawX + (float)this.width, this.drawY + (float)this.height))
				{
					baseElement.draw();
				}
				else
				{
					BaseElement.calculateTopLeft(baseElement);
				}
				i++;
			}
			if (this.container.passTransformationsToChilds)
			{
				BaseElement.restoreTransformations(this.container);
			}
			if (this.passTransformationsToChilds)
			{
				BaseElement.restoreTransformations(this);
			}
		}

		// Token: 0x0600060C RID: 1548 RVA: 0x0002DB2C File Offset: 0x0002BD2C
		public override void update(float delta)
		{
			base.update(delta);
			delta = this.fixedDelta;
			this.targetPoint = MathHelper.vectZero;
			if ((double)this.touchTimer > 0.0)
			{
				this.touchTimer -= delta;
				if ((double)this.touchTimer <= 0.0)
				{
					this.touchTimer = 0f;
					this.passTouches = true;
					if (!this.movingByInertion && !this.movingToSpoint && base.onTouchDownXY(this.savedTouch.x, this.savedTouch.y))
					{
						return;
					}
				}
			}
			if ((double)this.touchReleaseTimer > 0.0)
			{
				this.touchReleaseTimer -= delta;
				if ((double)this.touchReleaseTimer <= 0.0)
				{
					this.touchReleaseTimer = 0f;
					if (base.onTouchUpXY(this.savedTouch.x, this.savedTouch.y))
					{
						return;
					}
				}
			}
			if (this.touchState == ScrollableContainer.TOUCH_STATE.TOUCH_STATE_UP)
			{
				if (this.shouldBounceHorizontally)
				{
					if ((double)this.container.x > 0.0)
					{
						float num = (float)(50.0 + (double)Math.Abs(this.container.x) * 5.0);
						this.moveToPointDeltaSpeed(MathHelper.vect(0f, this.container.y), delta, num);
					}
					else if (this.container.x < (float)(-(float)this.container.width + this.width) && (double)this.container.x < 0.0)
					{
						float num2 = (float)(50.0 + (double)Math.Abs((float)(-(float)this.container.width + this.width) - this.container.x) * 5.0);
						this.moveToPointDeltaSpeed(MathHelper.vect((float)(-(float)this.container.width + this.width), this.container.y), delta, num2);
					}
				}
				if (this.shouldBounceVertically)
				{
					if ((double)this.container.y > 0.0)
					{
						this.moveToPointDeltaSpeed(MathHelper.vect(this.container.x, 0f), delta, (float)(50.0 + (double)Math.Abs(this.container.y) * 5.0));
					}
					else if (this.container.y < (float)(-(float)this.container.height + this.height) && (double)this.container.y < 0.0)
					{
						this.moveToPointDeltaSpeed(MathHelper.vect(this.container.x, (float)(-(float)this.container.height + this.height)), delta, (float)(50.0 + (double)Math.Abs((float)(-(float)this.container.height + this.height) - this.container.y) * 5.0));
					}
				}
			}
			if (this.movingToSpoint)
			{
				Vector vector = this.spoints[this.targetSpoint];
				this.moveToPointDeltaSpeed(vector, delta, (float)Math.Max(100.0, (double)MathHelper.vectDistance(vector, MathHelper.vect(this.container.x, this.container.y)) * 4.0 * (double)this.spointMoveMultiplier));
				if (this.container.x == vector.x && this.container.y == vector.y)
				{
					if (this.delegateScrollableContainerProtocol != null)
					{
						this.delegateScrollableContainerProtocol.scrollableContainerreachedScrollPoint(this, this.targetSpoint);
					}
					this.movingToSpoint = false;
					this.targetSpoint = -1;
					this.lastTargetSpoint = -1;
					this.move = MathHelper.vectZero;
				}
			}
			else if (this.canSkipScrollPoints && this.spointsNum > 0 && !MathHelper.vectEqual(this.move, MathHelper.vectZero) && (double)MathHelper.vectLength(this.move) < 150.0 && this.targetSpoint == -1)
			{
				this.startMovingToSpointInDirection(this.move);
			}
			if (!MathHelper.vectEqual(this.move, MathHelper.vectZero))
			{
				MathHelper.vectEqual(this.targetPoint, MathHelper.vectZero);
				MathHelper.vect(this.container.x, this.container.y);
				Vector vector2 = MathHelper.vectMult(MathHelper.vectNeg(this.move), 2f);
				this.move = MathHelper.vectAdd(this.move, MathHelper.vectMult(vector2, delta));
				Vector vector3 = MathHelper.vectMult(this.move, delta);
				if ((double)Math.Abs(vector3.x) < 0.2)
				{
					vector3.x = 0f;
					this.move.x = 0f;
				}
				if ((double)Math.Abs(vector3.y) < 0.2)
				{
					vector3.y = 0f;
					this.move.y = 0f;
				}
				this.moveContainerBy(vector3);
			}
			if ((double)this.inertiaTimeoutLeft > 0.0)
			{
				this.inertiaTimeoutLeft -= delta;
			}
		}

		// Token: 0x0600060D RID: 1549 RVA: 0x0002E074 File Offset: 0x0002C274
		public override void show()
		{
			this.touchTimer = 0f;
			this.passTouches = false;
			this.touchReleaseTimer = 0f;
			this.move = MathHelper.vectZero;
			if (this.resetScrollOnShow)
			{
				this.setScroll(MathHelper.vectZero);
			}
		}

		// Token: 0x0600060E RID: 1550 RVA: 0x0002E0B4 File Offset: 0x0002C2B4
		public override bool onTouchDownXY(float tx, float ty)
		{
			if (!MathHelper.pointInRect(tx, ty, this.drawX, this.drawY, (float)this.width, (float)this.height))
			{
				return false;
			}
			if (this.touchPassTimeout == 0f)
			{
				bool flag = base.onTouchDownXY(tx, ty);
				if (this.dontHandleTouchDownsHandledByChilds && flag)
				{
					return true;
				}
			}
			else
			{
				this.touchTimer = this.touchPassTimeout;
				this.savedTouch = MathHelper.vect(tx, ty);
				this.totalDrag = MathHelper.vectZero;
				this.passTouches = false;
			}
			this.touchState = ScrollableContainer.TOUCH_STATE.TOUCH_STATE_DOWN;
			this.movingByInertion = false;
			this.movingToSpoint = false;
			this.targetSpoint = -1;
			this.dragStart = MathHelper.vect(tx, ty);
			return true;
		}

		// Token: 0x0600060F RID: 1551 RVA: 0x0002E160 File Offset: 0x0002C360
		public override bool onTouchMoveXY(float tx, float ty)
		{
			if (MenuController.ep != null)
			{
				return false;
			}
			if (tx == -10000f && ty == -10000f)
			{
				return false;
			}
			if (this.touchPassTimeout == 0f || this.passTouches)
			{
				bool flag = base.onTouchMoveXY(tx, ty);
				if (this.dontHandleTouchMovesHandledByChilds && flag)
				{
					return true;
				}
			}
			Vector vector = MathHelper.vect(tx, ty);
			if (MathHelper.vectEqualApproximately(this.dragStart, vector, 5f))
			{
				return false;
			}
			if (MathHelper.vectEqual(this.dragStart, ScrollableContainer.impossibleTouch) && !MathHelper.pointInRect(tx, ty, this.drawX, this.drawY, (float)this.width, (float)this.height))
			{
				return false;
			}
			this.touchState = ScrollableContainer.TOUCH_STATE.TOUCH_STATE_MOVING;
			if (!MathHelper.vectEqual(this.dragStart, ScrollableContainer.impossibleTouch))
			{
				Vector vector2 = MathHelper.vectSub(vector, this.dragStart);
				this.dragStart = vector;
				vector2.x = MathHelper.FIT_TO_BOUNDARIES(vector2.x, -this.maxTouchMoveLength, this.maxTouchMoveLength);
				vector2.y = MathHelper.FIT_TO_BOUNDARIES(vector2.y, -this.maxTouchMoveLength, this.maxTouchMoveLength);
				this.totalDrag = MathHelper.vectAdd(this.totalDrag, vector2);
				if (((double)this.touchTimer > 0.0 || this.untouchChildsOnMove) && MathHelper.vectLength(this.totalDrag) > this.touchMoveIgnoreLength)
				{
					this.touchTimer = 0f;
					this.passTouches = false;
					base.onTouchUpXY(-1f, -1f);
				}
				if (this.container.width <= this.width)
				{
					vector2.x = 0f;
				}
				if (this.container.height <= this.height)
				{
					vector2.y = 0f;
				}
				if (this.shouldBounceHorizontally && ((double)this.container.x > 0.0 || this.container.x < (float)(-(float)this.container.width + this.width)))
				{
					vector2.x /= 2f;
				}
				if (this.shouldBounceVertically && ((double)this.container.y > 0.0 || this.container.y < (float)(-(float)this.container.height + this.height)))
				{
					vector2.y /= 2f;
				}
				this.staticMove = this.moveContainerBy(vector2);
				this.move = MathHelper.vectZero;
				this.inertiaTimeoutLeft = this.inertiaTimeout;
				return true;
			}
			return false;
		}

		// Token: 0x06000610 RID: 1552 RVA: 0x0002E3E4 File Offset: 0x0002C5E4
		public override bool onTouchUpXY(float tx, float ty)
		{
			if (this.touchPassTimeout == 0f || this.passTouches)
			{
				bool flag = base.onTouchUpXY(tx, ty);
				if (this.dontHandleTouchUpsHandledByChilds && flag)
				{
					return true;
				}
			}
			if ((double)this.touchTimer > 0.0 && ((!this.movingByInertion && !this.movingToSpoint) || this.targetSpoint == this.spointsNum - 1 || CTRPreferences.isLiteVersion()))
			{
				bool flag2 = base.onTouchDownXY(this.savedTouch.x, this.savedTouch.y);
				this.touchReleaseTimer = 0.2f;
				this.touchTimer = 0f;
				if (this.dontHandleTouchDownsHandledByChilds && flag2)
				{
					return true;
				}
			}
			if (this.touchState == ScrollableContainer.TOUCH_STATE.TOUCH_STATE_UP)
			{
				return false;
			}
			this.touchState = ScrollableContainer.TOUCH_STATE.TOUCH_STATE_UP;
			if ((double)this.inertiaTimeoutLeft > 0.0)
			{
				float num = this.inertiaTimeoutLeft / this.inertiaTimeout;
				this.move = MathHelper.vectMult(this.staticMove, (float)((double)num * 50.0));
				this.movingByInertion = true;
			}
			if (this.spointsNum > 0)
			{
				if (!this.canSkipScrollPoints)
				{
					if (this.minAutoScrollToSpointLength != -1f && MathHelper.vectLength(this.move) > this.minAutoScrollToSpointLength)
					{
						this.startMovingToSpointInDirection(this.move);
					}
					else
					{
						this.startMovingToSpointInDirection(MathHelper.vectZero);
					}
				}
				else if (MathHelper.vectEqual(this.move, MathHelper.vectZero))
				{
					this.startMovingToSpointInDirection(MathHelper.vectZero);
				}
			}
			this.dragStart = ScrollableContainer.impossibleTouch;
			return true;
		}

		// Token: 0x06000611 RID: 1553 RVA: 0x0002E560 File Offset: 0x0002C760
		public override void dealloc()
		{
			this.spoints = null;
			base.dealloc();
		}

		// Token: 0x06000612 RID: 1554 RVA: 0x0002E570 File Offset: 0x0002C770
		public virtual ScrollableContainer initWithWidthHeightContainer(float w, float h, BaseElement c)
		{
			if (base.init() != null)
			{
				float num = (float)Application.sharedAppSettings().getInt(5);
				this.fixedDelta = (float)(1.0 / (double)num);
				this.spoints = null;
				this.spointsNum = -1;
				this.spointsCapacity = -1;
				this.targetSpoint = -1;
				this.lastTargetSpoint = -1;
				this.deaccelerationSpeed = 3f;
				this.inertiaTimeout = 0.1f;
				this.scrollToPointDuration = 0.35f;
				this.canSkipScrollPoints = false;
				this.shouldBounceHorizontally = false;
				this.shouldBounceVertically = false;
				this.touchMoveIgnoreLength = 0f;
				this.maxTouchMoveLength = 40f;
				this.touchPassTimeout = 0.5f;
				this.minAutoScrollToSpointLength = -1f;
				this.resetScrollOnShow = true;
				this.untouchChildsOnMove = false;
				this.dontHandleTouchDownsHandledByChilds = false;
				this.dontHandleTouchMovesHandledByChilds = false;
				this.dontHandleTouchUpsHandledByChilds = false;
				this.touchTimer = 0f;
				this.passTouches = false;
				this.touchReleaseTimer = 0f;
				this.move = MathHelper.vectZero;
				this.container = c;
				this.width = (int)w;
				this.height = (int)h;
				this.container.parentAnchor = 9;
				this.container.parent = this;
				this.childs[0] = this.container;
				this.dragStart = ScrollableContainer.impossibleTouch;
				this.touchState = ScrollableContainer.TOUCH_STATE.TOUCH_STATE_UP;
			}
			return this;
		}

		// Token: 0x06000613 RID: 1555 RVA: 0x0002E6D0 File Offset: 0x0002C8D0
		public virtual ScrollableContainer initWithWidthHeightContainerWidthHeight(float w, float h, float cw, float ch)
		{
			this.container = (BaseElement)new BaseElement().init();
			this.container.width = (int)cw;
			this.container.height = (int)ch;
			this.initWithWidthHeightContainer(w, h, this.container);
			return this;
		}

		// Token: 0x06000614 RID: 1556 RVA: 0x0002E71D File Offset: 0x0002C91D
		public virtual void turnScrollPointsOnWithCapacity(int n)
		{
			this.spointsCapacity = n;
			this.spoints = new Vector[this.spointsCapacity];
			this.spointsNum = 0;
		}

		// Token: 0x06000615 RID: 1557 RVA: 0x0002E73E File Offset: 0x0002C93E
		public virtual int addScrollPointAtXY(double sx, double sy)
		{
			return this.addScrollPointAtXY((float)sx, (float)sy);
		}

		// Token: 0x06000616 RID: 1558 RVA: 0x0002E74A File Offset: 0x0002C94A
		public virtual int addScrollPointAtXY(float sx, float sy)
		{
			this.addScrollPointAtXYwithID(sx, sy, this.spointsNum);
			return this.spointsNum - 1;
		}

		// Token: 0x06000617 RID: 1559 RVA: 0x0002E762 File Offset: 0x0002C962
		public virtual void addScrollPointAtXYwithID(float sx, float sy, int i)
		{
			this.spoints[i] = MathHelper.vect(-sx, -sy);
			if (i > this.spointsNum - 1)
			{
				this.spointsNum = i + 1;
			}
		}

		// Token: 0x06000618 RID: 1560 RVA: 0x0002E792 File Offset: 0x0002C992
		public virtual int getTotalScrollPoints()
		{
			return this.spointsNum;
		}

		// Token: 0x06000619 RID: 1561 RVA: 0x0002E79A File Offset: 0x0002C99A
		public virtual Vector getScrollPoint(int i)
		{
			return this.spoints[i];
		}

		// Token: 0x0600061A RID: 1562 RVA: 0x0002E7AD File Offset: 0x0002C9AD
		public virtual Vector getScroll()
		{
			return MathHelper.vect(-this.container.x, -this.container.y);
		}

		// Token: 0x0600061B RID: 1563 RVA: 0x0002E7CC File Offset: 0x0002C9CC
		public virtual Vector getMaxScroll()
		{
			return MathHelper.vect((float)(this.container.width - this.width), (float)(this.container.height - this.height));
		}

		// Token: 0x0600061C RID: 1564 RVA: 0x0002E7FC File Offset: 0x0002C9FC
		public virtual void setScroll(Vector s)
		{
			this.move = MathHelper.vectZero;
			this.container.x = -s.x;
			this.container.y = -s.y;
			this.movingToSpoint = false;
			this.targetSpoint = -1;
			this.lastTargetSpoint = -1;
		}

		// Token: 0x0600061D RID: 1565 RVA: 0x0002E850 File Offset: 0x0002CA50
		public virtual void placeToScrollPoint(int sp)
		{
			this.move = MathHelper.vectZero;
			this.container.x = this.spoints[sp].x;
			this.container.y = this.spoints[sp].y;
			this.movingToSpoint = false;
			this.targetSpoint = -1;
			this.lastTargetSpoint = sp;
			if (this.delegateScrollableContainerProtocol != null)
			{
				this.delegateScrollableContainerProtocol.scrollableContainerreachedScrollPoint(this, sp);
			}
		}

		// Token: 0x0600061E RID: 1566 RVA: 0x0002E8CA File Offset: 0x0002CACA
		public virtual void moveToScrollPointmoveMultiplier(int sp, double m)
		{
			this.moveToScrollPointmoveMultiplier(sp, (float)m);
		}

		// Token: 0x0600061F RID: 1567 RVA: 0x0002E8D5 File Offset: 0x0002CAD5
		public virtual void moveToScrollPointmoveMultiplier(int sp, float m)
		{
			this.movingToSpoint = true;
			this.movingByInertion = false;
			this.spointMoveMultiplier = m;
			this.targetSpoint = sp;
			this.lastTargetSpoint = this.targetSpoint;
		}

		// Token: 0x06000620 RID: 1568 RVA: 0x0002E900 File Offset: 0x0002CB00
		public virtual void calculateNearsetScrollPointInDirection(Vector d)
		{
			this.spointMoveDirection = d;
			int num = -1;
			float num2 = 9999999f;
			float num3 = MathHelper.angleTo0_360(MathHelper.RADIANS_TO_DEGREES(MathHelper.vectAngleNormalized(d)));
			Vector vector = MathHelper.vect(this.container.x, this.container.y);
			for (int i = 0; i < this.spointsNum; i++)
			{
				if ((double)this.spoints[i].x <= 0.0 && (this.spoints[i].x >= (float)(-(float)this.container.width + this.width) || (double)this.spoints[i].x >= 0.0) && (double)this.spoints[i].y <= 0.0 && (this.spoints[i].y >= (float)(-(float)this.container.height + this.height) || (double)this.spoints[i].y >= 0.0))
				{
					float num4 = MathHelper.vectDistance(this.spoints[i], vector);
					if (!MathHelper.vectEqual(d, MathHelper.vectZero))
					{
						float num5 = MathHelper.angleTo0_360(MathHelper.RADIANS_TO_DEGREES(MathHelper.vectAngleNormalized(MathHelper.vectSub(this.spoints[i], vector))));
						if (Math.Abs(num5 - num3) > 90f)
						{
							goto IL_0187;
						}
					}
					if (num4 < num2)
					{
						num = i;
						num2 = num4;
					}
				}
				IL_0187:;
			}
			if (num == -1 && !MathHelper.vectEqual(d, MathHelper.vectZero))
			{
				this.calculateNearsetScrollPointInDirection(MathHelper.vectZero);
				return;
			}
			this.targetSpoint = num;
			if (!this.canSkipScrollPoints && this.targetSpoint != this.lastTargetSpoint)
			{
				this.movingByInertion = false;
			}
			if (this.lastTargetSpoint != this.targetSpoint && this.targetSpoint != -1 && this.delegateScrollableContainerProtocol != null)
			{
				this.delegateScrollableContainerProtocol.scrollableContainerchangedTargetScrollPoint(this, this.targetSpoint);
			}
			float num6 = MathHelper.angleTo0_360(MathHelper.RADIANS_TO_DEGREES(MathHelper.vectAngleNormalized(this.move)));
			float num7 = MathHelper.angleTo0_360(MathHelper.RADIANS_TO_DEGREES(MathHelper.vectAngleNormalized(MathHelper.vectSub(this.spoints[this.targetSpoint], vector))));
			if (Math.Abs(MathHelper.angleTo0_360(num6 - num7)) < 90f)
			{
				this.spointMoveMultiplier = (float)Math.Max(1.0, (double)MathHelper.vectLength(this.move) / 500.0);
			}
			else
			{
				this.spointMoveMultiplier = 0.5f;
			}
			this.lastTargetSpoint = this.targetSpoint;
		}

		// Token: 0x06000621 RID: 1569 RVA: 0x0002EBB8 File Offset: 0x0002CDB8
		public virtual Vector moveContainerBy(Vector off)
		{
			float num = this.container.x + off.x;
			float num2 = this.container.y + off.y;
			if (!this.shouldBounceHorizontally)
			{
				num = (float)Math.Min((double)Math.Max((float)(-(float)this.container.width + this.width), num), 0.0);
			}
			if (!this.shouldBounceVertically)
			{
				num2 = (float)Math.Min((double)Math.Max((float)(-(float)this.container.height + this.height), num2), 0.0);
			}
			Vector vector = MathHelper.vectSub(MathHelper.vect(num, num2), MathHelper.vect(this.container.x, this.container.y));
			this.container.x = num;
			this.container.y = num2;
			return vector;
		}

		// Token: 0x06000622 RID: 1570 RVA: 0x0002EC94 File Offset: 0x0002CE94
		public virtual void moveToPointDeltaSpeed(Vector tsp, float delta, float speed)
		{
			Vector vector = MathHelper.vectSub(tsp, MathHelper.vect(this.container.x, this.container.y));
			vector = MathHelper.vectNormalize(vector);
			vector = MathHelper.vectMult(vector, speed);
			Mover.moveVariableToTarget(ref this.container.x, tsp.x, Math.Abs(vector.x), delta);
			Mover.moveVariableToTarget(ref this.container.y, tsp.y, Math.Abs(vector.y), delta);
			this.targetPoint = tsp;
			this.move = MathHelper.vectZero;
		}

		// Token: 0x06000623 RID: 1571 RVA: 0x0002ED30 File Offset: 0x0002CF30
		public virtual void startMovingToSpointInDirection(Vector d)
		{
			this.movingToSpoint = true;
			this.targetSpoint = (this.lastTargetSpoint = -1);
			this.calculateNearsetScrollPointInDirection(d);
		}

		// Token: 0x04000B4D RID: 2893
		private const double DEFAULT_BOUNCE_MOVEMENT_DIVIDE = 2.0;

		// Token: 0x04000B4E RID: 2894
		private const double DEFAULT_BOUNCE_DURATION = 0.1;

		// Token: 0x04000B4F RID: 2895
		private const double DEFAULT_DEACCELERATION = 3.0;

		// Token: 0x04000B50 RID: 2896
		private const double DEFAULT_INERTIAL_TIMEOUT = 0.1;

		// Token: 0x04000B51 RID: 2897
		private const double DEFAULT_SCROLL_TO_POINT_DURATION = 0.35;

		// Token: 0x04000B52 RID: 2898
		private const double MIN_SCROLL_POINTS_MOVE = 50.0;

		// Token: 0x04000B53 RID: 2899
		private const double CALC_NEAREST_DEFAULT_TIMEOUT = 0.02;

		// Token: 0x04000B54 RID: 2900
		private const double DEFAULT_MAX_TOUCH_MOVE_LENGTH = 40.0;

		// Token: 0x04000B55 RID: 2901
		private const double DEFAULT_TOUCH_PASS_TIMEOUT = 0.5;

		// Token: 0x04000B56 RID: 2902
		private const double AUTO_RELEASE_TOUCH_TIMEOUT = 0.2;

		// Token: 0x04000B57 RID: 2903
		private const double MOVE_APPROXIMATION = 0.2;

		// Token: 0x04000B58 RID: 2904
		private const float MOVE_TOUCH_APPROXIMATION = 5f;

		// Token: 0x04000B59 RID: 2905
		public ScrollableContainerProtocol delegateScrollableContainerProtocol;

		// Token: 0x04000B5A RID: 2906
		private static readonly Vector impossibleTouch = new Vector(-1000f, -1000f);

		// Token: 0x04000B5B RID: 2907
		private BaseElement container;

		// Token: 0x04000B5C RID: 2908
		private Vector dragStart;

		// Token: 0x04000B5D RID: 2909
		private Vector staticMove;

		// Token: 0x04000B5E RID: 2910
		private Vector move;

		// Token: 0x04000B5F RID: 2911
		private bool movingByInertion;

		// Token: 0x04000B60 RID: 2912
		private float inertiaTimeoutLeft;

		// Token: 0x04000B61 RID: 2913
		private bool movingToSpoint;

		// Token: 0x04000B62 RID: 2914
		private int targetSpoint;

		// Token: 0x04000B63 RID: 2915
		private int lastTargetSpoint;

		// Token: 0x04000B64 RID: 2916
		private float spointMoveMultiplier;

		// Token: 0x04000B65 RID: 2917
		private Vector[] spoints;

		// Token: 0x04000B66 RID: 2918
		private int spointsNum;

		// Token: 0x04000B67 RID: 2919
		private int spointsCapacity;

		// Token: 0x04000B68 RID: 2920
		private Vector spointMoveDirection;

		// Token: 0x04000B69 RID: 2921
		private Vector targetPoint;

		// Token: 0x04000B6A RID: 2922
		private ScrollableContainer.TOUCH_STATE touchState;

		// Token: 0x04000B6B RID: 2923
		private float touchTimer;

		// Token: 0x04000B6C RID: 2924
		private float touchReleaseTimer;

		// Token: 0x04000B6D RID: 2925
		private Vector savedTouch;

		// Token: 0x04000B6E RID: 2926
		private Vector totalDrag;

		// Token: 0x04000B6F RID: 2927
		private bool passTouches;

		// Token: 0x04000B70 RID: 2928
		private float fixedDelta;

		// Token: 0x04000B71 RID: 2929
		private float deaccelerationSpeed;

		// Token: 0x04000B72 RID: 2930
		private float inertiaTimeout;

		// Token: 0x04000B73 RID: 2931
		private float scrollToPointDuration;

		// Token: 0x04000B74 RID: 2932
		public bool canSkipScrollPoints;

		// Token: 0x04000B75 RID: 2933
		public bool shouldBounceHorizontally;

		// Token: 0x04000B76 RID: 2934
		public bool shouldBounceVertically;

		// Token: 0x04000B77 RID: 2935
		public float touchMoveIgnoreLength;

		// Token: 0x04000B78 RID: 2936
		private float maxTouchMoveLength;

		// Token: 0x04000B79 RID: 2937
		private float touchPassTimeout;

		// Token: 0x04000B7A RID: 2938
		public bool resetScrollOnShow;

		// Token: 0x04000B7B RID: 2939
		private bool dontHandleTouchDownsHandledByChilds;

		// Token: 0x04000B7C RID: 2940
		private bool dontHandleTouchMovesHandledByChilds;

		// Token: 0x04000B7D RID: 2941
		private bool dontHandleTouchUpsHandledByChilds;

		// Token: 0x04000B7E RID: 2942
		public bool untouchChildsOnMove;

		// Token: 0x04000B7F RID: 2943
		public float minAutoScrollToSpointLength;

		// Token: 0x020000D0 RID: 208
		private enum TOUCH_STATE
		{
			// Token: 0x04000B81 RID: 2945
			TOUCH_STATE_UP,
			// Token: 0x04000B82 RID: 2946
			TOUCH_STATE_DOWN,
			// Token: 0x04000B83 RID: 2947
			TOUCH_STATE_MOVING
		}
	}
}
