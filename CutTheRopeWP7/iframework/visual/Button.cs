using System;
using ctr_wp7.iframework.helpers;
using ctr_wp7.ios;

namespace ctr_wp7.iframework.visual
{
	// Token: 0x02000057 RID: 87
	internal class Button : BaseElement
	{
		// Token: 0x0600029A RID: 666 RVA: 0x000109A0 File Offset: 0x0000EBA0
		public static Button createWithTextureUpDownID(Texture2D up, Texture2D down, int bID)
		{
			Image image = Image.Image_create(up);
			Image image2 = Image.Image_create(down);
			return new Button().initWithUpElementDownElementandID(image, image2, bID);
		}

		// Token: 0x0600029B RID: 667 RVA: 0x000109C8 File Offset: 0x0000EBC8
		public virtual Button initWithID(int n)
		{
			if (base.init() != null)
			{
				this.buttonID = n;
				this.state = Button.BUTTON_STATE.BUTTON_UP;
				this.touchLeftInc = 0f;
				this.touchRightInc = 0f;
				this.touchTopInc = 0f;
				this.touchBottomInc = 0f;
				this.forcedTouchZone = new Rectangle(-1f, -1f, -1f, -1f);
			}
			return this;
		}

		// Token: 0x0600029C RID: 668 RVA: 0x00010A38 File Offset: 0x0000EC38
		public virtual Button initWithUpElementDownElementandID(BaseElement up, BaseElement down, int n)
		{
			if (this.initWithID(n) != null)
			{
				up.parentAnchor = (down.parentAnchor = 9);
				this.addChildwithID(up, 0);
				this.addChildwithID(down, 1);
				this.setState(Button.BUTTON_STATE.BUTTON_UP);
			}
			return this;
		}

		// Token: 0x0600029D RID: 669 RVA: 0x00010A79 File Offset: 0x0000EC79
		public void setTouchIncreaseLeftRightTopBottom(double l, double r, double t, double b)
		{
			this.setTouchIncreaseLeftRightTopBottom((float)l, (float)r, (float)t, (float)b);
		}

		// Token: 0x0600029E RID: 670 RVA: 0x00010A8A File Offset: 0x0000EC8A
		public virtual void setTouchIncreaseLeftRightTopBottom(float l, float r, float t, float b)
		{
			this.touchLeftInc = l;
			this.touchRightInc = r;
			this.touchTopInc = t;
			this.touchBottomInc = b;
		}

		// Token: 0x0600029F RID: 671 RVA: 0x00010AA9 File Offset: 0x0000ECA9
		public virtual void forceTouchRect(Rectangle r)
		{
			this.forcedTouchZone = r;
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x00010AB4 File Offset: 0x0000ECB4
		public virtual bool isInTouchZoneXYforTouchDown(float tx, float ty, bool td)
		{
			float num = (td ? 0f : 15f);
			if (this.forcedTouchZone.w != -1f)
			{
				return MathHelper.pointInRect(tx, ty, this.drawX + this.forcedTouchZone.x - num - this.touchLeftInc, this.drawY + this.forcedTouchZone.y - num - this.touchTopInc, this.forcedTouchZone.w + num * 2f + (this.touchLeftInc + this.touchRightInc), this.forcedTouchZone.h + (this.touchTopInc + this.touchBottomInc) + num * 2f);
			}
			return MathHelper.pointInRect(tx, ty, this.drawX - this.touchLeftInc - num, this.drawY - this.touchTopInc - num, (float)this.width + (this.touchLeftInc + this.touchRightInc) + num * 2f, (float)this.height + (this.touchTopInc + this.touchBottomInc) + num * 2f);
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x00010BC8 File Offset: 0x0000EDC8
		public virtual void setState(Button.BUTTON_STATE s)
		{
			this.state = s;
			BaseElement child = this.getChild(0);
			BaseElement child2 = this.getChild(1);
			child.setEnabled(s == Button.BUTTON_STATE.BUTTON_UP);
			child2.setEnabled(s == Button.BUTTON_STATE.BUTTON_DOWN);
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x00010C00 File Offset: 0x0000EE00
		public override bool onTouchDownXY(float tx, float ty)
		{
			base.onTouchDownXY(tx, ty);
			if (this.state == Button.BUTTON_STATE.BUTTON_UP && this.isInTouchZoneXYforTouchDown(tx, ty, true))
			{
				this.setState(Button.BUTTON_STATE.BUTTON_DOWN);
				return true;
			}
			return false;
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x00010C28 File Offset: 0x0000EE28
		public override bool onTouchUpXY(float tx, float ty)
		{
			base.onTouchUpXY(tx, ty);
			if (this.state == Button.BUTTON_STATE.BUTTON_DOWN)
			{
				this.setState(Button.BUTTON_STATE.BUTTON_UP);
				if (this.isInTouchZoneXYforTouchDown(tx, ty, false))
				{
					if (this.delegateButtonDelegate != null)
					{
						this.delegateButtonDelegate.onButtonPressed(this.buttonID);
					}
					return true;
				}
			}
			return false;
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x00010C75 File Offset: 0x0000EE75
		public override bool onTouchMoveXY(float tx, float ty)
		{
			base.onTouchMoveXY(tx, ty);
			if (this.state == Button.BUTTON_STATE.BUTTON_DOWN)
			{
				if (this.isInTouchZoneXYforTouchDown(tx, ty, false))
				{
					return true;
				}
				this.setState(Button.BUTTON_STATE.BUTTON_UP);
			}
			return false;
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x00010CA0 File Offset: 0x0000EEA0
		public override int addChildwithID(BaseElement c, int i)
		{
			int num = base.addChildwithID(c, i);
			c.parentAnchor = 9;
			if (i == 1)
			{
				this.width = c.width;
				this.height = c.height;
				this.setState(Button.BUTTON_STATE.BUTTON_UP);
			}
			return num;
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x00010CE2 File Offset: 0x0000EEE2
		public virtual BaseElement createFromXML(XMLNode xml)
		{
			throw new NotImplementedException();
		}

		// Token: 0x04000899 RID: 2201
		public const float TOUCH_MOVE_AND_UP_ZONE_INCREASE = 15f;

		// Token: 0x0400089A RID: 2202
		public int buttonID;

		// Token: 0x0400089B RID: 2203
		public Button.BUTTON_STATE state;

		// Token: 0x0400089C RID: 2204
		public ButtonDelegate delegateButtonDelegate;

		// Token: 0x0400089D RID: 2205
		public float touchLeftInc;

		// Token: 0x0400089E RID: 2206
		public float touchRightInc;

		// Token: 0x0400089F RID: 2207
		public float touchTopInc;

		// Token: 0x040008A0 RID: 2208
		public float touchBottomInc;

		// Token: 0x040008A1 RID: 2209
		public Rectangle forcedTouchZone;

		// Token: 0x02000058 RID: 88
		public enum BUTTON_STATE
		{
			// Token: 0x040008A3 RID: 2211
			BUTTON_UP,
			// Token: 0x040008A4 RID: 2212
			BUTTON_DOWN
		}
	}
}
