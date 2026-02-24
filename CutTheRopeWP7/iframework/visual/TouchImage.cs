using ctr_wp7.iframework.core;

namespace ctr_wp7.iframework.visual
{
    internal sealed class TouchImage : Image
    {
        public override bool onTouchDownXY(float tx, float ty)
        {
            _ = base.onTouchDownXY(tx, ty);
            if (pointInRect(tx, ty, drawX, drawY, width, height))
            {
                delegateButtonDelegate?.onButtonPressed(bid);
                return true;
            }
            return false;
        }

        private static TouchImage TouchImage_create(Texture2D t)
        {
            return (TouchImage)new TouchImage().initWithTexture(t);
        }

        public static TouchImage TouchImage_createWithResIDQuad(int r, int q)
        {
            TouchImage touchImage = TouchImage_create(Application.getTexture(r));
            touchImage.setDrawQuad(q);
            return touchImage;
        }

        public int bid;

        public ButtonDelegate delegateButtonDelegate;
    }
}
