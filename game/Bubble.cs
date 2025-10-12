using System;
using ctre_wp7.iframework.core;
using ctre_wp7.iframework.helpers;
using ctre_wp7.iframework.visual;

namespace ctre_wp7.game
{
	// Token: 0x02000102 RID: 258
	internal class Bubble : GameObject
	{
		// Token: 0x060007DC RID: 2012 RVA: 0x0003D892 File Offset: 0x0003BA92
		public static Bubble Bubble_create(Texture2D t)
		{
			return (Bubble)new Bubble().initWithTexture(t);
		}

		// Token: 0x060007DD RID: 2013 RVA: 0x0003D8A4 File Offset: 0x0003BAA4
		public static Bubble Bubble_createWithResID(int r)
		{
			return Bubble.Bubble_create(Application.getTexture(r));
		}

		// Token: 0x060007DE RID: 2014 RVA: 0x0003D8B4 File Offset: 0x0003BAB4
		public static Bubble Bubble_createWithResIDQuad(int r, int q)
		{
			Bubble bubble = Bubble.Bubble_create(Application.getTexture(r));
			bubble.setDrawQuad(q);
			return bubble;
		}

		// Token: 0x060007DF RID: 2015 RVA: 0x0003D8D8 File Offset: 0x0003BAD8
		public override void draw()
		{
			base.preDraw();
			if (!this.withoutShadow)
			{
				if (this.quadToDraw == -1)
				{
					GLDrawer.drawImage(this.texture, this.drawX, this.drawY);
				}
				else
				{
					this.drawQuad(this.quadToDraw);
				}
			}
			base.postDraw();
		}

		// Token: 0x04000D28 RID: 3368
		public bool popped;

		// Token: 0x04000D29 RID: 3369
		public float initial_rotation;

		// Token: 0x04000D2A RID: 3370
		public float initial_x;

		// Token: 0x04000D2B RID: 3371
		public float initial_y;

		// Token: 0x04000D2C RID: 3372
		public RotatedCircle initial_rotatedCircle;

		// Token: 0x04000D2D RID: 3373
		public bool withoutShadow;
	}
}
