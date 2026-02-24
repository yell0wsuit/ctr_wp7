using System;

using ctr_wp7.iframework.core;

namespace ctr_wp7.iframework.visual
{
    internal sealed class HorizontallyTiledImage : Image
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
            float w = texture.quadRects[tiles[0]].w;
            float w2 = texture.quadRects[tiles[2]].w;
            float num = width - (w + w2);
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
                rectangle.w = Math.Min(rectangle.w, width / 2f);
                rectangle2.w = Math.Min(rectangle2.w, width - rectangle.w);
                rectangle2.x += texture.quadRects[tiles[2]].w - rectangle2.w;
                GLDrawer.drawImagePart(texture, rectangle, drawX, drawY + offsets[0]);
                GLDrawer.drawImagePart(texture, rectangle2, drawX + rectangle.w, drawY + offsets[2]);
            }
            postDraw();
        }

        public void setTileHorizontallyLeftCenterRight(int l, int c, int r)
        {
            tiles[0] = l;
            tiles[1] = c;
            tiles[2] = r;
            float h = texture.quadRects[tiles[0]].h;
            float h2 = texture.quadRects[tiles[1]].h;
            float h3 = texture.quadRects[tiles[2]].h;
            height = h >= h2 && h >= h3 ? (int)h : h2 >= h && h2 >= h3 ? (int)h2 : (int)h3;
            offsets[0] = (height - h) / 2f;
            offsets[1] = (height - h2) / 2f;
            offsets[2] = (height - h3) / 2f;
        }

        public static HorizontallyTiledImage HorizontallyTiledImage_create(Texture2D t)
        {
            return (HorizontallyTiledImage)new HorizontallyTiledImage().initWithTexture(t);
        }

        public static HorizontallyTiledImage HorizontallyTiledImage_createWithResID(int r)
        {
            return HorizontallyTiledImage_create(Application.getTexture(r));
        }

        public static HorizontallyTiledImage HorizontallyTiledImage_createWithResIDQuad(int r, int q)
        {
            HorizontallyTiledImage horizontallyTiledImage = HorizontallyTiledImage_create(Application.getTexture(r));
            horizontallyTiledImage.setDrawQuad(q);
            return horizontallyTiledImage;
        }

        public int[] tiles = new int[3];

        public float[] offsets = new float[3];

        public int align;
    }
}
