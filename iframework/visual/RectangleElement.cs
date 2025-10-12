using System;
using ctre_wp7.ios;

namespace ctre_wp7.iframework.visual
{
	// Token: 0x02000038 RID: 56
	internal class RectangleElement : BaseElement
	{
		// Token: 0x06000208 RID: 520 RVA: 0x0000DA47 File Offset: 0x0000BC47
		public override NSObject init()
		{
			if (base.init() != null)
			{
				this.solid = true;
			}
			return this;
		}

		// Token: 0x06000209 RID: 521 RVA: 0x0000DA5C File Offset: 0x0000BC5C
		public override void draw()
		{
			base.preDraw();
			OpenGL.glDisable(0);
			if (this.solid)
			{
				GLDrawer.drawSolidRectWOBorder(this.drawX, this.drawY, (float)this.width, (float)this.height, this.color);
			}
			else
			{
				GLDrawer.drawRect(this.drawX, this.drawY, (float)this.width, (float)this.height, this.color);
			}
			OpenGL.glEnable(0);
			OpenGL.SetWhiteColor();
			base.postDraw();
		}

		// Token: 0x0400080C RID: 2060
		public bool solid;
	}
}
