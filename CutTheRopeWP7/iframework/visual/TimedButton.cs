using System;

using ctr_wp7.ios;

namespace ctr_wp7.iframework.visual
{
    internal sealed class TimedButton : BaseElement
    {
        public static TimedButton createWithTextureUpDownID(Texture2D up, Texture2D down, int bID)
        {
            Image image = Image.Image_create(up);
            Image image2 = Image.Image_create(down);
            return new TimedButton().initWithUpElementDownElementandID(image, image2, bID);
        }

        public TimedButton initWithID(int n)
        {
            if (init() != null)
            {
                buttonID = n;
                state = TIMED_BUTTON.TIMED_BUTTON_UP;
                touchLeftInc = 0f;
                touchRightInc = 0f;
                touchTopInc = 0f;
                touchBottomInc = 0f;
                forcedTouchZone = new Rectangle(-1f, -1f, -1f, -1f);
            }
            return this;
        }

        public TimedButton initWithUpElementDownElementandID(BaseElement up, BaseElement down, int n)
        {
            if (initWithID(n) != null)
            {
                up.parentAnchor = down.parentAnchor = 9;
                _ = addChildwithID(up, 0);
                _ = addChildwithID(down, 1);
                setState(TIMED_BUTTON.TIMED_BUTTON_UP);
                Timeline timeline = new Timeline().initWithMaxKeyFramesOnTrack(8);
                timeline.addKeyFrame(KeyFrame.makeSingleAction(getChild(0), "ACTION_SET_VISIBLE", 1, 1, 0f));
                timeline.addKeyFrame(KeyFrame.makeSingleAction(getChild(1), "ACTION_SET_VISIBLE", 0, 0, 0f));
                timeline.addKeyFrame(KeyFrame.makeSingleAction(getChild(1), "ACTION_SET_VISIBLE", 1, 1, 0.1f));
                timeline.addKeyFrame(KeyFrame.makeSingleAction(getChild(0), "ACTION_SET_VISIBLE", 0, 0, 0f));
                timeline.addKeyFrame(KeyFrame.makeSingleAction(getChild(1), "ACTION_SET_VISIBLE", 0, 0, 0.2f));
                timeline.addKeyFrame(KeyFrame.makeSingleAction(getChild(0), "ACTION_SET_VISIBLE", 1, 1, 0f));
                timeline.addKeyFrame(KeyFrame.makeSingleAction(getChild(1), "ACTION_SET_VISIBLE", 1, 1, 0.1f));
                timeline.addKeyFrame(KeyFrame.makeSingleAction(getChild(0), "ACTION_SET_VISIBLE", 0, 0, 0f));
                addTimelinewithID(timeline, 0);
                timelinePlayed = false;
            }
            return this;
        }

        public void setTouchIncreaseLeftRightTopBottom(double l, double r, double t, double b)
        {
            setTouchIncreaseLeftRightTopBottom((float)l, (float)r, (float)t, (float)b);
        }

        public void setTouchIncreaseLeftRightTopBottom(float l, float r, float t, float b)
        {
            touchLeftInc = l;
            touchRightInc = r;
            touchTopInc = t;
            touchBottomInc = b;
        }

        public void forceTouchRect(Rectangle r)
        {
            forcedTouchZone = r;
        }

        public bool isInTouchZoneXYforTouchDown(float tx, float ty, bool td)
        {
            float num = td ? 0f : 15f;
            return forcedTouchZone.w != -1f
                ? pointInRect(tx, ty, drawX + forcedTouchZone.x - num, drawY + forcedTouchZone.y - num, forcedTouchZone.w + (num * 2f), forcedTouchZone.h + (num * 2f))
                : pointInRect(tx, ty, drawX - touchLeftInc - num, drawY - touchTopInc - num, width + (touchLeftInc + touchRightInc) + (num * 2f), height + (touchTopInc + touchBottomInc) + (num * 2f));
        }

        public void setState(TIMED_BUTTON s)
        {
            state = s;
            BaseElement child = getChild(0);
            BaseElement child2 = getChild(1);
            child.setEnabled(s == TIMED_BUTTON.TIMED_BUTTON_UP);
            child2.setEnabled(s == TIMED_BUTTON.TIMED_BUTTON_DOWN);
        }

        public override bool onTouchDownXY(float tx, float ty)
        {
            _ = base.onTouchDownXY(tx, ty);
            if (state == TIMED_BUTTON.TIMED_BUTTON_UP && isInTouchZoneXYforTouchDown(tx, ty, true))
            {
                setState(TIMED_BUTTON.TIMED_BUTTON_DOWN);
                time = timer;
                timelinePlayed = false;
                return true;
            }
            return false;
        }

        public override bool onTouchUpXY(float tx, float ty)
        {
            _ = base.onTouchUpXY(tx, ty);
            if (state == TIMED_BUTTON.TIMED_BUTTON_DOWN)
            {
                setState(TIMED_BUTTON.TIMED_BUTTON_UP);
                timelinePlayed = false;
                if (isInTouchZoneXYforTouchDown(tx, ty, false) && time <= 0f)
                {
                    delegateButtonDelegate?.onButtonPressed(buttonID);
                    Timeline currentTimeline = getCurrentTimeline();
                    if (currentTimeline != null)
                    {
                        stopCurrentTimeline();
                    }
                    time = -1f;
                    return true;
                }
                time = -1f;
            }
            return false;
        }

        public override bool onTouchMoveXY(float tx, float ty)
        {
            _ = base.onTouchMoveXY(tx, ty);
            if (state == TIMED_BUTTON.TIMED_BUTTON_DOWN)
            {
                if (isInTouchZoneXYforTouchDown(tx, ty, false))
                {
                    return true;
                }
                setState(TIMED_BUTTON.TIMED_BUTTON_UP);
            }
            return false;
        }

        public override int addChildwithID(BaseElement c, int i)
        {
            int num = base.addChildwithID(c, i);
            c.parentAnchor = 9;
            if (i == 1)
            {
                width = c.width;
                height = c.height;
                setState(TIMED_BUTTON.TIMED_BUTTON_UP);
            }
            return num;
        }

        public BaseElement createFromXML(XMLNode xml)
        {
            throw new NotImplementedException();
        }

        public override void update(float delta)
        {
            base.update(delta);
            if (time > 0f && state == TIMED_BUTTON.TIMED_BUTTON_DOWN)
            {
                time -= delta;
                if (time <= 0f && !timelinePlayed)
                {
                    playTimeline(0);
                    timelinePlayed = true;
                }
            }
        }

        private int buttonID;

        public float timer;

        private bool timelinePlayed;

        private float time;

        private TIMED_BUTTON state;

        public ButtonDelegate delegateButtonDelegate;

        private float touchLeftInc;

        private float touchRightInc;

        private float touchTopInc;

        private float touchBottomInc;

        private Rectangle forcedTouchZone;

        public enum TIMED_BUTTON
        {
            TIMED_BUTTON_UP,
            TIMED_BUTTON_DOWN
        }
    }
}
