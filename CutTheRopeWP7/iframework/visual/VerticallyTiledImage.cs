using System;

namespace ctr_wp7.iframework.visual
{
    internal sealed class VerticallyTiledImage : Image
    {
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

        public override void draw()
        {
            preDraw();
            float h = texture.quadRects[tiles[0]].h;
            float h2 = texture.quadRects[tiles[2]].h;
            float num = height - (h + h2);
            if (num >= 0f)
            {
                GLDrawer.drawImageQuad(texture, tiles[0], drawX + offsets[0], drawY);
                GLDrawer.drawImageTiledCool(texture, tiles[1], drawX + offsets[1], drawY + h, width, num);
                GLDrawer.drawImageQuad(texture, tiles[2], drawX + offsets[2], drawY + h + num);
            }
            else
            {
                Rectangle rectangle = texture.quadRects[tiles[0]];
                Rectangle rectangle2 = texture.quadRects[tiles[2]];
                rectangle.h = Math.Min(rectangle.h, height / 2f);
                rectangle2.h = Math.Min(rectangle2.h, height - rectangle.h);
                rectangle2.y += texture.quadRects[tiles[2]].h - rectangle2.h;
                GLDrawer.drawImagePart(texture, rectangle, drawX + offsets[0], drawY);
                GLDrawer.drawImagePart(texture, rectangle2, drawX + offsets[2], drawY + rectangle.h);
            }
            postDraw();
        }

        public void setTileVerticallyTopCenterBottom(int t, int c, int b)
        {
            tiles[0] = t;
            tiles[1] = c;
            tiles[2] = b;
            float w = texture.quadRects[tiles[0]].w;
            float w2 = texture.quadRects[tiles[1]].w;
            float w3 = texture.quadRects[tiles[2]].w;
            width = w >= w2 && w >= w3 ? (int)w : w2 >= w && w2 >= w3 ? (int)w2 : (int)w3;
            offsets[0] = (width - w) / 2f;
            offsets[1] = (width - w2) / 2f;
            offsets[2] = (width - w3) / 2f;
        }

        public int[] tiles = new int[3];

        public float[] offsets = new float[3];

        public int align;
    }
}
