using System;
using ctr_wp7.iframework.core;

namespace ctr_wp7.iframework.visual
{
	// Token: 0x02000062 RID: 98
	internal class TiledImage : Image
	{
		// Token: 0x060002E6 RID: 742 RVA: 0x00012DAF File Offset: 0x00010FAF
		public virtual void setTile(int t)
		{
			this.q = t;
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x00012DB8 File Offset: 0x00010FB8
		public override void draw()
		{
			this.preDraw();
			GLDrawer.drawImageTiled(this.texture, this.q, this.drawX, this.drawY, (float)this.width, (float)this.height);
			this.postDraw();
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x00012DF1 File Offset: 0x00010FF1
		private static TiledImage TiledImage_create(Texture2D t)
		{
			return (TiledImage)new TiledImage().initWithTexture(t);
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x00012E04 File Offset: 0x00011004
		private static TiledImage TiledImage_createWithResID(int r)
		{
			return TiledImage.TiledImage_create(Application.getTexture(r));
		}

		// Token: 0x060002EA RID: 746 RVA: 0x00012E20 File Offset: 0x00011020
		private static TiledImage TiledImage_createWithResIDQuad(int r, int q)
		{
			TiledImage tiledImage = TiledImage.TiledImage_createWithResID(r);
			tiledImage.setDrawQuad(q);
			return tiledImage;
		}

		// Token: 0x040008C3 RID: 2243
		private int q;
	}
}
