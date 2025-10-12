using System;

namespace ctre_wp7.iframework.visual
{
	// Token: 0x02000092 RID: 146
	internal class HBox : BaseElement
	{
		// Token: 0x06000457 RID: 1111 RVA: 0x0001E7E4 File Offset: 0x0001C9E4
		public override int addChildwithID(BaseElement c, int i)
		{
			int num = base.addChildwithID(c, i);
			if (this.align == 8)
			{
				c.anchor = (c.parentAnchor = 9);
			}
			else if (this.align == 16)
			{
				c.anchor = (c.parentAnchor = 17);
			}
			else if (this.align == 32)
			{
				c.anchor = (c.parentAnchor = 33);
			}
			c.x = this.nextElementX;
			this.nextElementX += (float)c.width + this.offset;
			this.width = (int)(this.nextElementX - this.offset);
			return num;
		}

		// Token: 0x06000458 RID: 1112 RVA: 0x0001E88A File Offset: 0x0001CA8A
		public virtual HBox initWithOffsetAlignHeight(double of, int a, double h)
		{
			return this.initWithOffsetAlignHeight((float)of, a, (float)h);
		}

		// Token: 0x06000459 RID: 1113 RVA: 0x0001E897 File Offset: 0x0001CA97
		public virtual HBox initWithOffsetAlignHeight(float of, int a, float h)
		{
			if (base.init() != null)
			{
				this.offset = of;
				this.align = a;
				this.nextElementX = 0f;
				this.height = (int)h;
			}
			return this;
		}

		// Token: 0x0400099E RID: 2462
		public float offset;

		// Token: 0x0400099F RID: 2463
		public int align;

		// Token: 0x040009A0 RID: 2464
		public float nextElementX;
	}
}
