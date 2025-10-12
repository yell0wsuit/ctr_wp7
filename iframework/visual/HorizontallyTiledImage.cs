using System;
using ctre_wp7.iframework.core;

namespace ctre_wp7.iframework.visual
{
	// Token: 0x02000060 RID: 96
	internal class HorizontallyTiledImage : Image
	{
		// Token: 0x060002DB RID: 731 RVA: 0x000126DC File Offset: 0x000108DC
		public override Image initWithTexture(Texture2D t)
		{
			if (base.initWithTexture(t) != null)
			{
				for (int i = 0; i < 3; i++)
				{
					this.tiles[i] = -1;
				}
				this.align = 18;
			}
			return this;
		}

		// Token: 0x060002DC RID: 732 RVA: 0x00012710 File Offset: 0x00010910
		public override void draw()
		{
			this.preDraw();
			float w = this.texture.quadRects[this.tiles[0]].w;
			float w2 = this.texture.quadRects[this.tiles[2]].w;
			float num = (float)this.width - (w + w2);
			if (num >= 0f)
			{
				GLDrawer.drawImageQuad(this.texture, this.tiles[0], this.drawX, this.drawY + this.offsets[0]);
				GLDrawer.drawImageTiledCool(this.texture, this.tiles[1], this.drawX + w, this.drawY + this.offsets[1], num, this.texture.quadRects[this.tiles[1]].h);
				GLDrawer.drawImageQuad(this.texture, this.tiles[2], this.drawX + w + num, this.drawY + this.offsets[2]);
			}
			else
			{
				Rectangle rectangle = this.texture.quadRects[this.tiles[0]];
				Rectangle rectangle2 = this.texture.quadRects[this.tiles[2]];
				rectangle.w = Math.Min(rectangle.w, (float)this.width / 2f);
				rectangle2.w = Math.Min(rectangle2.w, (float)this.width - rectangle.w);
				rectangle2.x += this.texture.quadRects[this.tiles[2]].w - rectangle2.w;
				GLDrawer.drawImagePart(this.texture, rectangle, this.drawX, this.drawY + this.offsets[0]);
				GLDrawer.drawImagePart(this.texture, rectangle2, this.drawX + rectangle.w, this.drawY + this.offsets[2]);
			}
			this.postDraw();
		}

		// Token: 0x060002DD RID: 733 RVA: 0x0001291C File Offset: 0x00010B1C
		public virtual void setTileHorizontallyLeftCenterRight(int l, int c, int r)
		{
			this.tiles[0] = l;
			this.tiles[1] = c;
			this.tiles[2] = r;
			float h = this.texture.quadRects[this.tiles[0]].h;
			float h2 = this.texture.quadRects[this.tiles[1]].h;
			float h3 = this.texture.quadRects[this.tiles[2]].h;
			if (h >= h2 && h >= h3)
			{
				this.height = (int)h;
			}
			else if (h2 >= h && h2 >= h3)
			{
				this.height = (int)h2;
			}
			else
			{
				this.height = (int)h3;
			}
			this.offsets[0] = ((float)this.height - h) / 2f;
			this.offsets[1] = ((float)this.height - h2) / 2f;
			this.offsets[2] = ((float)this.height - h3) / 2f;
		}

		// Token: 0x060002DE RID: 734 RVA: 0x00012A0F File Offset: 0x00010C0F
		public static HorizontallyTiledImage HorizontallyTiledImage_create(Texture2D t)
		{
			return (HorizontallyTiledImage)new HorizontallyTiledImage().initWithTexture(t);
		}

		// Token: 0x060002DF RID: 735 RVA: 0x00012A21 File Offset: 0x00010C21
		public static HorizontallyTiledImage HorizontallyTiledImage_createWithResID(int r)
		{
			return HorizontallyTiledImage.HorizontallyTiledImage_create(Application.getTexture(r));
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x00012A30 File Offset: 0x00010C30
		public static HorizontallyTiledImage HorizontallyTiledImage_createWithResIDQuad(int r, int q)
		{
			HorizontallyTiledImage horizontallyTiledImage = HorizontallyTiledImage.HorizontallyTiledImage_create(Application.getTexture(r));
			horizontallyTiledImage.setDrawQuad(q);
			return horizontallyTiledImage;
		}

		// Token: 0x040008BD RID: 2237
		public int[] tiles = new int[3];

		// Token: 0x040008BE RID: 2238
		public float[] offsets = new float[3];

		// Token: 0x040008BF RID: 2239
		public int align;
	}
}
