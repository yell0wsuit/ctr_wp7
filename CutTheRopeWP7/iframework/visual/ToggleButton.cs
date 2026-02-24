namespace ctr_wp7.iframework.visual
{
    // Token: 0x02000048 RID: 72
    internal class ToggleButton : BaseElement, ButtonDelegate
    {
        // Token: 0x0600024B RID: 587 RVA: 0x0000F164 File Offset: 0x0000D364
        public virtual void onButtonPressed(int n)
        {
            switch (n)
            {
                case 0:
                case 1:
                    this.toggle();
                    break;
            }
            if (this.delegateButtonDelegate != null)
            {
                this.delegateButtonDelegate.onButtonPressed(this.buttonID);
            }
        }

        // Token: 0x0600024C RID: 588 RVA: 0x0000F1A4 File Offset: 0x0000D3A4
        public ToggleButton initWithUpElement1DownElement1UpElement2DownElement2andID(BaseElement u1, BaseElement d1, BaseElement u2, BaseElement d2, int bid)
        {
            if (base.init() != null)
            {
                this.buttonID = bid;
                this.b1 = new Button().initWithUpElementDownElementandID(u1, d1, 0);
                this.b2 = new Button().initWithUpElementDownElementandID(u2, d2, 1);
                this.b1.parentAnchor = (this.b2.parentAnchor = 9);
                this.width = this.b1.width;
                this.height = this.b1.height;
                this.addChildwithID(this.b1, 0);
                this.addChildwithID(this.b2, 1);
                this.b2.setEnabled(false);
                this.b1.delegateButtonDelegate = this;
                this.b2.delegateButtonDelegate = this;
            }
            return this;
        }

        // Token: 0x0600024D RID: 589 RVA: 0x0000F269 File Offset: 0x0000D469
        public void setTouchIncreaseLeftRightTopBottom(double l, double r, double t, double b)
        {
            this.setTouchIncreaseLeftRightTopBottom((float)l, (float)r, (float)t, (float)b);
        }

        // Token: 0x0600024E RID: 590 RVA: 0x0000F27A File Offset: 0x0000D47A
        public void setTouchIncreaseLeftRightTopBottom(float l, float r, float t, float b)
        {
            this.b1.setTouchIncreaseLeftRightTopBottom(l, r, t, b);
            this.b2.setTouchIncreaseLeftRightTopBottom(l, r, t, b);
        }

        // Token: 0x0600024F RID: 591 RVA: 0x0000F29C File Offset: 0x0000D49C
        public void toggle()
        {
            this.b1.setEnabled(!this.b1.isEnabled());
            this.b2.setEnabled(!this.b2.isEnabled());
        }

        // Token: 0x06000250 RID: 592 RVA: 0x0000F2D0 File Offset: 0x0000D4D0
        public bool on()
        {
            return this.b2.isEnabled();
        }

        // Token: 0x04000833 RID: 2099
        public ButtonDelegate delegateButtonDelegate;

        // Token: 0x04000834 RID: 2100
        private int buttonID;

        // Token: 0x04000835 RID: 2101
        private Button b1;

        // Token: 0x04000836 RID: 2102
        private Button b2;

        // Token: 0x02000049 RID: 73
        private enum TOGGLE_BUTTON
        {
            // Token: 0x04000838 RID: 2104
            TOGGLE_BUTTON_FACE1,
            // Token: 0x04000839 RID: 2105
            TOGGLE_BUTTON_FACE2
        }
    }
}
