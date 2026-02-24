using System;
using ctr_wp7.iframework.core;
using ctr_wp7.iframework.helpers;

namespace ctr_wp7.iframework.visual
{
	// Token: 0x02000089 RID: 137
	internal class Scrollbar : BaseElement
	{
		// Token: 0x060003F8 RID: 1016 RVA: 0x0001C6B7 File Offset: 0x0001A8B7
		public override void update(float delta)
		{
			base.update(delta);
			if (this.delegateProvider != null)
			{
				this.delegateProvider(ref this.sp, ref this.mp, ref this.sc);
			}
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x0001C6E8 File Offset: 0x0001A8E8
		public override void draw()
		{
			base.preDraw();
			if (MathHelper.vectEqual(this.sp, MathHelper.vectUndefined) && this.delegateProvider != null)
			{
				this.delegateProvider(ref this.sp, ref this.mp, ref this.sc);
			}
			OpenGL.glDisable(0);
			bool flag = false;
			float num;
			float num2;
			float num3;
			float num5;
			if (this.vertical)
			{
				num = (float)this.width - 2f;
				num2 = 1f;
				num3 = (float)Math.Round(((double)this.height - 2.0) / (double)this.sc.y);
				float num4 = ((this.mp.y != 0f) ? (this.sp.y / this.mp.y) : 1f);
				num5 = (float)(1.0 + ((double)this.height - 2.0 - (double)num3) * (double)num4);
				if (num3 > (float)this.height)
				{
					flag = true;
				}
			}
			else
			{
				num3 = (float)this.height - 2f;
				num5 = 1f;
				num = (float)Math.Round(((double)this.width - 2.0) / (double)this.sc.x);
				float num6 = ((this.mp.x != 0f) ? (this.sp.x / this.mp.x) : 1f);
				num2 = (float)(1.0 + ((double)this.width - 2.0 - (double)num) * (double)num6);
				if (num > (float)this.width)
				{
					flag = true;
				}
			}
			if (!flag)
			{
				GLDrawer.drawSolidRectWOBorder(this.drawX, this.drawY, (float)this.width, (float)this.height, this.backColor);
				GLDrawer.drawSolidRectWOBorder(this.drawX + num2, this.drawY + num5, num, num3, this.scrollerColor);
			}
			OpenGL.glEnable(0);
			OpenGL.SetWhiteColor();
			base.postDraw();
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x0001C8E0 File Offset: 0x0001AAE0
		public virtual Scrollbar initWithWidthHeightVertical(float w, float h, bool v)
		{
			if (base.init() != null)
			{
				this.width = (int)w;
				this.height = (int)h;
				this.vertical = v;
				this.sp = MathHelper.vectUndefined;
				this.mp = MathHelper.vectUndefined;
				this.sc = MathHelper.vectUndefined;
				this.backColor = RGBAColor.MakeRGBA(1f, 1f, 1f, 0.5f);
				this.scrollerColor = RGBAColor.MakeRGBA(0f, 0f, 0f, 0.5f);
			}
			return this;
		}

		// Token: 0x04000961 RID: 2401
		public Vector sp;

		// Token: 0x04000962 RID: 2402
		public Vector mp;

		// Token: 0x04000963 RID: 2403
		public Vector sc;

		// Token: 0x04000964 RID: 2404
		public Scrollbar.ProvideScrollPosMaxScrollPosScrollCoeff delegateProvider;

		// Token: 0x04000965 RID: 2405
		public bool vertical;

		// Token: 0x04000966 RID: 2406
		public RGBAColor backColor;

		// Token: 0x04000967 RID: 2407
		public RGBAColor scrollerColor;

		// Token: 0x0200008A RID: 138
		// (Invoke) Token: 0x060003FD RID: 1021
		public delegate void ProvideScrollPosMaxScrollPosScrollCoeff(ref Vector sp, ref Vector mp, ref Vector sc);
	}
}
