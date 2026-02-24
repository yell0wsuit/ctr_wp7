namespace ctr_wp7.iframework.visual
{
    internal sealed class ToggleButton : BaseElement, ButtonDelegate
    {
        public void onButtonPressed(int n)
        {
            switch (n)
            {
                case 0:
                case 1:
                    toggle();
                    break;
            }
            delegateButtonDelegate?.onButtonPressed(buttonID);
        }

        public ToggleButton initWithUpElement1DownElement1UpElement2DownElement2andID(BaseElement u1, BaseElement d1, BaseElement u2, BaseElement d2, int bid)
        {
            if (init() != null)
            {
                buttonID = bid;
                b1 = new Button().initWithUpElementDownElementandID(u1, d1, 0);
                b2 = new Button().initWithUpElementDownElementandID(u2, d2, 1);
                b1.parentAnchor = b2.parentAnchor = 9;
                width = b1.width;
                height = b1.height;
                _ = addChildwithID(b1, 0);
                _ = addChildwithID(b2, 1);
                b2.setEnabled(false);
                b1.delegateButtonDelegate = this;
                b2.delegateButtonDelegate = this;
            }
            return this;
        }

        public void setTouchIncreaseLeftRightTopBottom(double l, double r, double t, double b)
        {
            setTouchIncreaseLeftRightTopBottom((float)l, (float)r, (float)t, (float)b);
        }

        public void setTouchIncreaseLeftRightTopBottom(float l, float r, float t, float b)
        {
            b1.setTouchIncreaseLeftRightTopBottom(l, r, t, b);
            b2.setTouchIncreaseLeftRightTopBottom(l, r, t, b);
        }

        public void toggle()
        {
            b1.setEnabled(!b1.isEnabled());
            b2.setEnabled(!b2.isEnabled());
        }

        public bool on()
        {
            return b2.isEnabled();
        }

        public ButtonDelegate delegateButtonDelegate;

        private int buttonID;

        private Button b1;

        private Button b2;

        private enum TOGGLE_BUTTON
        {
            TOGGLE_BUTTON_FACE1,
            TOGGLE_BUTTON_FACE2
        }
    }
}
