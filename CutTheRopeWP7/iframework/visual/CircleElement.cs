using System;
using ctr_wp7.iframework.helpers;
using ctr_wp7.ios;

namespace ctr_wp7.iframework.visual
{
	// Token: 0x02000039 RID: 57
	internal class CircleElement : BaseElement
	{
		// Token: 0x0600020B RID: 523 RVA: 0x0000DAE2 File Offset: 0x0000BCE2
		public override NSObject init()
		{
			if (base.init() != null)
			{
				this.vertextCount = 32;
				this.solid = true;
			}
			return this;
		}

		// Token: 0x0600020C RID: 524 RVA: 0x0000DAFC File Offset: 0x0000BCFC
		public override void draw()
		{
			base.preDraw();
			OpenGL.glDisable(0);
			MathHelper.MIN(this.width, this.height);
			bool flag = this.solid;
			OpenGL.glEnable(0);
			OpenGL.SetWhiteColor();
			base.postDraw();
		}

		// Token: 0x0400080D RID: 2061
		public bool solid;

		// Token: 0x0400080E RID: 2062
		public int vertextCount;
	}
}
