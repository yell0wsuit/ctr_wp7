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
                    this.tiles[i] = -1;
                }
                this.align = 18;
            }
            return this;
        }

        // Token: 0x060002E3 RID: 739 RVA: 0x00012AA8 File Offset: 0x00010CA8
        public override void draw()
        {
            this.preDraw();
            float h = this.texture.quadRects[this.tiles[0]].h;
            float h2 = this.texture.quadRects[this.tiles[2]].h;
            float num = (float)this.height - (h + h2);
            if (num >= 0f)
            {
                GLDrawer.drawImageQuad(this.texture, this.tiles[0], this.drawX + this.offsets[0], this.drawY);
                GLDrawer.drawImageTiledCool(this.texture, this.tiles[1], this.drawX + this.offsets[1], this.drawY + h, (float)this.width, num);
                GLDrawer.drawImageQuad(this.texture, this.tiles[2], this.drawX + this.offsets[2], this.drawY + h + num);
            }
            else
            {
                Rectangle rectangle = this.texture.quadRects[this.tiles[0]];
                Rectangle rectangle2 = this.texture.quadRects[this.tiles[2]];
                rectangle.h = Math.Min(rectangle.h, (float)this.height / 2f);
                rectangle2.h = Math.Min(rectangle2.h, (float)this.height - rectangle.h);
                rectangle2.y += this.texture.quadRects[this.tiles[2]].h - rectangle2.h;
                GLDrawer.drawImagePart(this.texture, rectangle, this.drawX + this.offsets[0], this.drawY);
                GLDrawer.drawImagePart(this.texture, rectangle2, this.drawX + this.offsets[2], this.drawY + rectangle.h);
            }
            this.postDraw();
        }

        // Token: 0x060002E4 RID: 740 RVA: 0x00012C9C File Offset: 0x00010E9C
        public virtual void setTileVerticallyTopCenterBottom(int t, int c, int b)
        {
            this.tiles[0] = t;
            this.tiles[1] = c;
            this.tiles[2] = b;
            float w = this.texture.quadRects[this.tiles[0]].w;
            float w2 = this.texture.quadRects[this.tiles[1]].w;
            float w3 = this.texture.quadRects[this.tiles[2]].w;
            if (w >= w2 && w >= w3)
            {
                this.width = (int)w;
            }
            else if (w2 >= w && w2 >= w3)
            {
                this.width = (int)w2;
            }
            else
            {
                this.width = (int)w3;
            }
            this.offsets[0] = ((float)this.width - w) / 2f;
            this.offsets[1] = ((float)this.width - w2) / 2f;
            this.offsets[2] = ((float)this.width - w3) / 2f;
        }

        // Token: 0x040008C0 RID: 2240
        public int[] tiles = new int[3];

        // Token: 0x040008C1 RID: 2241
        public float[] offsets = new float[3];

        // Token: 0x040008C2 RID: 2242
        public int align;
    }
}
