using ctr_wp7.ctr_original;
using ctr_wp7.iframework.core;
using ctr_wp7.iframework.visual;
using ctr_wp7.ios;

namespace ctr_wp7.game
{
    // Token: 0x020000F7 RID: 247
    internal sealed class AdSkipper : BaseElement, ButtonDelegate
    {
        // Token: 0x0600077F RID: 1919 RVA: 0x0003B8E0 File Offset: 0x00039AE0
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

        // Token: 0x06000780 RID: 1920 RVA: 0x0003B972 File Offset: 0x00039B72
        public void setJskipper(object jskipper)
        {
            freeJskipper();
            skipper = jskipper;
            active = true;
            skipAd.setEnabled(true);
        }

        // Token: 0x06000781 RID: 1921 RVA: 0x0003B994 File Offset: 0x00039B94
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

        // Token: 0x06000782 RID: 1922 RVA: 0x0003B9C3 File Offset: 0x00039BC3
        public override void dealloc()
        {
            freeJskipper();
            base.dealloc();
        }

        // Token: 0x06000783 RID: 1923 RVA: 0x0003B9D1 File Offset: 0x00039BD1
        public override void update(float delta)
        {
            base.update(delta);
            if (active)
            {
                timerNoDraw += delta;
            }
        }

        // Token: 0x06000784 RID: 1924 RVA: 0x0003B9F0 File Offset: 0x00039BF0
        public void onButtonPressed(int n)
        {
            if (active)
            {
            }
        }

        // Token: 0x04000CF5 RID: 3317
        public const int BUTTON_SKIP_AD = 0;

        // Token: 0x04000CF6 RID: 3318
        private Button skipAd;

        // Token: 0x04000CF7 RID: 3319
        private object skipper;

        // Token: 0x04000CF8 RID: 3320
        public float timerNoDraw;

        // Token: 0x04000CF9 RID: 3321
        public bool active;
    }
}
