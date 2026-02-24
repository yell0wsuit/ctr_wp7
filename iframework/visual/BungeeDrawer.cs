using System;
using ctr_wp7.game;
using ctr_wp7.iframework.core;
using ctr_wp7.iframework.helpers;
using ctr_wp7.iframework.sfe;

namespace ctr_wp7.iframework.visual
{
	// Token: 0x020000D7 RID: 215
	internal class BungeeDrawer : BaseElement
	{
		// Token: 0x06000643 RID: 1603 RVA: 0x000302F8 File Offset: 0x0002E4F8
		public override void draw()
		{
			base.preDraw();
			OpenGL.glDisable(0);
			OpenGL.glBlendFunc(BlendingFactor.GL_SRC_ALPHA, BlendingFactor.GL_ONE_MINUS_SRC_ALPHA);
			this.bungee.draw();
			OpenGL.SetWhiteColor();
			OpenGL.glBlendFunc(BlendingFactor.GL_ONE, BlendingFactor.GL_ONE_MINUS_SRC_ALPHA);
			OpenGL.glEnable(0);
			base.postDraw();
		}

		// Token: 0x06000644 RID: 1604 RVA: 0x00030348 File Offset: 0x0002E548
		public override void update(float delta)
		{
			base.update(delta);
			float num = 0.025f;
			Vector quadCenter = Image.getQuadCenter(77, 8);
			this.bungee.bungeeAnchor.pos = MathHelper.vectAdd(MathHelper.vect(this.parent.drawX, this.parent.drawY), quadCenter);
			this.bungee.bungeeAnchor.pin = this.bungee.bungeeAnchor.pos;
			float num2 = 25f;
			this.bungee.tail.applyImpulseDelta(MathHelper.vect(-this.bungee.tail.v.x / num2, -this.bungee.tail.v.y / num2), num);
			this.bungee.update(num);
			this.bungee.tail.update(num);
			float num3 = 1f - this.parent.y / (float)PromoBanner.BANNER_OFFSET;
			this.fadeElement.color.a = 0.4f * num3;
			this.fadeElement.setEnabled(this.parent.y != (float)PromoBanner.BANNER_OFFSET);
			if (this.down)
			{
				this.bungee.tail.pos = this.tailPos;
				if (this.bungee.relaxed != 0)
				{
					ConstraintedPoint constraintedPoint = this.bungee.parts[0];
					ConstraintedPoint constraintedPoint2 = this.bungee.parts[1];
					float num4 = MathHelper.vectDistance(constraintedPoint.pos, constraintedPoint2.pos);
					Vector vector = MathHelper.vectSub(this.bungee.tail.pos, this.bungee.bungeeAnchor.pos);
					float num5 = 1f;
					float num6 = (num4 - 30f) * num5;
					if ((double)Math.Abs(vector.y) > 20.0)
					{
						if (this.tailPos.y > this.bungee.bungeeAnchor.pos.y)
						{
							this.parent.y += num6;
						}
						else
						{
							this.parent.y -= num6;
						}
						this.parent.y = MathHelper.FIT_TO_BOUNDARIES(this.parent.y, (float)PromoBanner.BANNER_OFFSET, 100f);
						return;
					}
					if ((double)num4 > 45.0)
					{
						this.down = false;
						if (this.delegateButtonDelegate != null)
						{
							this.delegateButtonDelegate.onButtonPressed(this.bid);
						}
					}
				}
			}
		}

		// Token: 0x06000645 RID: 1605 RVA: 0x000305D8 File Offset: 0x0002E7D8
		public override bool onTouchDownXY(float tx, float ty)
		{
			bool flag = base.onTouchDownXY(tx, ty);
			if (MathHelper.pointInRect(tx, ty, this.bungee.tail.pos.x - 35f, this.bungee.tail.pos.y - 15f, 70f, 70f))
			{
				this.fadeElement.setEnabled(true);
				this.down = true;
				this.dragStart = MathHelper.vect(tx, ty);
				this.tailStart = this.bungee.tail.pos;
				this.onTouchMoveXY(tx, ty);
				return true;
			}
			return flag;
		}

		// Token: 0x06000646 RID: 1606 RVA: 0x0003067C File Offset: 0x0002E87C
		public override bool onTouchUpXY(float tx, float ty)
		{
			bool flag = base.onTouchUpXY(tx, ty);
			if (this.down)
			{
				this.down = false;
				if (this.delegateButtonDelegate != null)
				{
					this.delegateButtonDelegate.onButtonPressed(this.bid);
				}
				return true;
			}
			return flag;
		}

		// Token: 0x06000647 RID: 1607 RVA: 0x000306C0 File Offset: 0x0002E8C0
		public override bool onTouchMoveXY(float tx, float ty)
		{
			bool flag = base.onTouchMoveXY(tx, ty);
			if (this.down)
			{
				float num = 1f;
				if (ty > FrameworkTypes.SCREEN_HEIGHT / 2f)
				{
					num /= 1.5f;
				}
				Vector vector = MathHelper.vectSub(MathHelper.vect(tx, ty), this.dragStart);
				vector = MathHelper.vectMult(vector, num);
				this.tailPos = MathHelper.vectAdd(this.tailStart, vector);
				this.dragStart = MathHelper.vect(tx, ty);
				this.tailStart = this.tailPos;
			}
			return flag;
		}

		// Token: 0x04000BAC RID: 2988
		public ButtonDelegate delegateButtonDelegate;

		// Token: 0x04000BAD RID: 2989
		public int bid;

		// Token: 0x04000BAE RID: 2990
		public Bungee bungee;

		// Token: 0x04000BAF RID: 2991
		public Processing fadeElement;

		// Token: 0x04000BB0 RID: 2992
		public bool down;

		// Token: 0x04000BB1 RID: 2993
		public Vector dragStart;

		// Token: 0x04000BB2 RID: 2994
		public Vector tailStart;

		// Token: 0x04000BB3 RID: 2995
		public Vector tailPos;
	}
}
