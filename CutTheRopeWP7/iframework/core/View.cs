using ctr_wp7.iframework.visual;
using ctr_wp7.ios;

namespace ctr_wp7.iframework.core
{
    internal class View : BaseElement
    {
        public virtual NSObject initFullscreen()
        {
            if (base.init() != null)
            {
                width = (int)SCREEN_WIDTH;
                height = (int)SCREEN_HEIGHT;
            }
            return this;
        }

        public override NSObject init()
        {
            return initFullscreen();
        }

        public override void draw()
        {
            OpenGL.SetWhiteColor();
            OpenGL.glEnable(0);
            OpenGL.glEnable(1);
            OpenGL.glBlendFunc(BlendingFactor.GL_SRC_ALPHA, BlendingFactor.GL_ONE_MINUS_SRC_ALPHA);
            base.preDraw();
            base.postDraw();
            OpenGL.glDisable(0);
            OpenGL.glDisable(1);
        }
    }
}
