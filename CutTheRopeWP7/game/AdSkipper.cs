using ctr_wp7.ctr_original;
using ctr_wp7.iframework.core;
using ctr_wp7.iframework.visual;
using ctr_wp7.ios;

namespace ctr_wp7.game
{
    internal sealed class AdSkipper : BaseElement, ButtonDelegate
    {
        public override NSObject init()
        {
            if (base.init() != null)
            {
                timerNoDraw = 0f;
                active = false;
                skipper = null;
                skipAd = MenuController.createButtonWithTextIDDelegate(Application.getString(1310817), 0, this);
                skipAd.anchor = skipAd.parentAnchor = 34;
                skipAd.setEnabled(false);
                _ = addChild(skipAd);
                visible = false;
                anchor = parentAnchor = 34;
            }
            return this;
        }

        public void setJskipper(object jskipper)
        {
            freeJskipper();
            skipper = jskipper;
            active = true;
            skipAd.setEnabled(true);
        }

        public void freeJskipper()
        {
            if (skipper != null)
            {
                timerNoDraw = 0f;
                skipper = null;
                active = false;
                skipAd.setEnabled(false);
            }
        }

        public override void dealloc()
        {
            freeJskipper();
            base.dealloc();
        }

        public override void update(float delta)
        {
            base.update(delta);
            if (active)
            {
                timerNoDraw += delta;
            }
        }

        public void onButtonPressed(int n)
        {
            if (active)
            {
            }
        }

        public const int BUTTON_SKIP_AD = 0;

        private Button skipAd;

        private object skipper;

        public float timerNoDraw;

        public bool active;
    }
}
