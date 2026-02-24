using System;

namespace ctr_wp7.iframework.visual
{
    // Token: 0x02000061 RID: 97
    internal class VerticallyTiledImage : Image
    {
        // Token: 0x060002E2 RID: 738 RVA: 0x00012A74 File Offset: 0x00010C74
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

        // Token: 0x060002E3 RID: 739 RVA: 0x00012AA8 File Offset: 0x00010CA8
        public override void draw()
        {
            preDraw();
            float h = texture.quadRects[tiles[0]].h;
            float h2 = texture.quadRects[tiles[2]].h;
            float num = (float)height - (h + h2);
            if (num >= 0f)
            {
                GLDrawer.drawImageQuad(texture, tiles[0], drawX + offsets[0], drawY);
                GLDrawer.drawImageTiledCool(texture, tiles[1], drawX + offsets[1], drawY + h, (float)width, num);
                GLDrawer.drawImageQuad(texture, tiles[2], drawX + offsets[2], drawY + h + num);
            }
            else
            {
                Rectangle rectangle = texture.quadRects[tiles[0]];
                Rectangle rectangle2 = texture.quadRects[tiles[2]];
                rectangle.h = Math.Min(rectangle.h, (float)height / 2f);
                rectangle2.h = Math.Min(rectangle2.h, (float)height - rectangle.h);
                rectangle2.y += texture.quadRects[tiles[2]].h - rectangle2.h;
                GLDrawer.drawImagePart(texture, rectangle, drawX + offsets[0], drawY);
                GLDrawer.drawImagePart(texture, rectangle2, drawX + offsets[2], drawY + rectangle.h);
            }
            postDraw();
        }

        // Token: 0x060002E4 RID: 740 RVA: 0x00012C9C File Offset: 0x00010E9C
        public virtual void setTileVerticallyTopCenterBottom(int t, int c, int b)
        {
            tiles[0] = t;
            tiles[1] = c;
            tiles[2] = b;
            float w = texture.quadRects[tiles[0]].w;
            float w2 = texture.quadRects[tiles[1]].w;
            float w3 = texture.quadRects[tiles[2]].w;
            if (w >= w2 && w >= w3)
            {
                width = (int)w;
            }
            else if (w2 >= w && w2 >= w3)
            {
                width = (int)w2;
            }
            else
            {
                width = (int)w3;
            }
            offsets[0] = ((float)width - w) / 2f;
            offsets[1] = ((float)width - w2) / 2f;
            offsets[2] = ((float)width - w3) / 2f;
        }

        // Token: 0x040008C0 RID: 2240
        public int[] tiles = new int[3];

        // Token: 0x040008C1 RID: 2241
        public float[] offsets = new float[3];

        // Token: 0x040008C2 RID: 2242
        public int align;
    }
}
