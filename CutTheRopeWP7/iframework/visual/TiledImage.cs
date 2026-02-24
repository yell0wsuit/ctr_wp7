using ctr_wp7.iframework.core;

namespace ctr_wp7.iframework.visual
{
    internal sealed class TiledImage : Image
    {
        public void setTile(int t)
        {
            q = t;
        }

        public override void draw()
        {
            preDraw();
            GLDrawer.drawImageTiled(texture, q, drawX, drawY, width, height);
            postDraw();
        }

        private static TiledImage TiledImage_create(Texture2D t)
        {
            return (TiledImage)new TiledImage().initWithTexture(t);
        }

        private static TiledImage TiledImage_createWithResID(int r)
        {
            return TiledImage_create(Application.getTexture(r));
        }

        private static TiledImage TiledImage_createWithResIDQuad(int r, int q)
        {
            TiledImage tiledImage = TiledImage_createWithResID(r);
            tiledImage.setDrawQuad(q);
            return tiledImage;
        }

        private int q;
    }
}
