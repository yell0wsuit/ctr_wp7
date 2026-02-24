using System;

using ctr_wp7.iframework.core;

namespace ctr_wp7.iframework.visual
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
                    tiles[i] = -1;
                }
                align = 18;
            }
            return this;
        }

        // Token: 0x060002DC RID: 732 RVA: 0x00012710 File Offset: 0x00010910
        public override void draw()
        {
            preDraw();
            float w = texture.quadRects[tiles[0]].w;
            float w2 = texture.quadRects[tiles[2]].w;
            float num = (float)width - (w + w2);
            if (num >= 0f)
            {
                GLDrawer.drawImageQuad(texture, tiles[0], drawX, drawY + offsets[0]);
                GLDrawer.drawImageTiledCool(texture, tiles[1], drawX + w, drawY + offsets[1], num, texture.quadRects[tiles[1]].h);
                GLDrawer.drawImageQuad(texture, tiles[2], drawX + w + num, drawY + offsets[2]);
            }
            else
            {
                Rectangle rectangle = texture.quadRects[tiles[0]];
                Rectangle rectangle2 = texture.quadRects[tiles[2]];
                rectangle.w = Math.Min(rectangle.w, (float)width / 2f);
                rectangle2.w = Math.Min(rectangle2.w, (float)width - rectangle.w);
                rectangle2.x += texture.quadRects[tiles[2]].w - rectangle2.w;
                GLDrawer.drawImagePart(texture, rectangle, drawX, drawY + offsets[0]);
                GLDrawer.drawImagePart(texture, rectangle2, drawX + rectangle.w, drawY + offsets[2]);
            }
            postDraw();
        }

        // Token: 0x060002DD RID: 733 RVA: 0x0001291C File Offset: 0x00010B1C
        public virtual void setTileHorizontallyLeftCenterRight(int l, int c, int r)
        {
            tiles[0] = l;
            tiles[1] = c;
            tiles[2] = r;
            float h = texture.quadRects[tiles[0]].h;
            float h2 = texture.quadRects[tiles[1]].h;
            float h3 = texture.quadRects[tiles[2]].h;
            if (h >= h2 && h >= h3)
            {
                height = (int)h;
            }
            else if (h2 >= h && h2 >= h3)
            {
                height = (int)h2;
            }
            else
            {
                height = (int)h3;
            }
            offsets[0] = ((float)height - h) / 2f;
            offsets[1] = ((float)height - h2) / 2f;
            offsets[2] = ((float)height - h3) / 2f;
        }

        // Token: 0x060002DE RID: 734 RVA: 0x00012A0F File Offset: 0x00010C0F
        public static HorizontallyTiledImage HorizontallyTiledImage_create(Texture2D t)
        {
            return (HorizontallyTiledImage)new HorizontallyTiledImage().initWithTexture(t);
        }

        // Token: 0x060002DF RID: 735 RVA: 0x00012A21 File Offset: 0x00010C21
        public static HorizontallyTiledImage HorizontallyTiledImage_createWithResID(int r)
        {
            return HorizontallyTiledImage_create(Application.getTexture(r));
        }

        // Token: 0x060002E0 RID: 736 RVA: 0x00012A30 File Offset: 0x00010C30
        public static HorizontallyTiledImage HorizontallyTiledImage_createWithResIDQuad(int r, int q)
        {
            HorizontallyTiledImage horizontallyTiledImage = HorizontallyTiledImage_create(Application.getTexture(r));
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
