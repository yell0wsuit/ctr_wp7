using System;
using ctr_wp7.iframework.core;
using ctr_wp7.iframework.helpers;

namespace ctr_wp7.iframework.visual
{
	// Token: 0x020000CB RID: 203
	internal class TouchImage : Image
	{
		// Token: 0x060005E9 RID: 1513 RVA: 0x0002CB30 File Offset: 0x0002AD30
		public override bool onTouchDownXY(float tx, float ty)
		{
			base.onTouchDownXY(tx, ty);
			if (MathHelper.pointInRect(tx, ty, this.drawX, this.drawY, (float)this.width, (float)this.height))
			{
				if (this.delegateButtonDelegate != null)
				{
					this.delegateButtonDelegate.onButtonPressed(this.bid);
				}
				return true;
			}
			return false;
		}

		// Token: 0x060005EA RID: 1514 RVA: 0x0002CB85 File Offset: 0x0002AD85
		private static TouchImage TouchImage_create(Texture2D t)
		{
			return (TouchImage)new TouchImage().initWithTexture(t);
		}

		// Token: 0x060005EB RID: 1515 RVA: 0x0002CB98 File Offset: 0x0002AD98
		public static TouchImage TouchImage_createWithResIDQuad(int r, int q)
		{
			TouchImage touchImage = TouchImage.TouchImage_create(Application.getTexture(r));
			touchImage.setDrawQuad(q);
			return touchImage;
		}

		// Token: 0x04000B3E RID: 2878
		public int bid;

		// Token: 0x04000B3F RID: 2879
		public ButtonDelegate delegateButtonDelegate;
	}
}
