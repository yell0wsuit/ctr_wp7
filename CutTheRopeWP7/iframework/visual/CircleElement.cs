using ctr_wp7.ios;

namespace ctr_wp7.iframework.visual
{
    internal sealed class CircleElement : BaseElement
    {
        public override NSObject init()
        {
            if (base.init() != null)
            {
                vertextCount = 32;
                solid = true;
            }
            return this;
        }

        public override void draw()
        {
            preDraw();
            OpenGL.glDisable(0);
            _ = MIN(width, height);
            OpenGL.glEnable(0);
            OpenGL.SetWhiteColor();
            postDraw();
        }

        public bool solid;

        public int vertextCount;
    }
}
