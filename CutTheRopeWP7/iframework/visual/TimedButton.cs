using System;

using ctr_wp7.ios;

namespace ctr_wp7.iframework.visual
{
    // Token: 0x02000087 RID: 135
    internal sealed class TimedButton : BaseElement
    {
        // Token: 0x060003E9 RID: 1001 RVA: 0x0001C1DC File Offset: 0x0001A3DC
        public static TimedButton createWithTextureUpDownID(Texture2D up, Texture2D down, int bID)
        {
            Image image = Image.Image_create(up);
            Image image2 = Image.Image_create(down);
            return new TimedButton().initWithUpElementDownElementandID(image, image2, bID);
        }

        // Token: 0x060003EA RID: 1002 RVA: 0x0001C204 File Offset: 0x0001A404
        public TimedButton initWithID(int n)
        {
            if (base.init() != null)
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

        // Token: 0x060003EB RID: 1003 RVA: 0x0001C274 File Offset: 0x0001A474
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

        // Token: 0x060003EC RID: 1004 RVA: 0x0001C3C3 File Offset: 0x0001A5C3
        public void setTouchIncreaseLeftRightTopBottom(double l, double r, double t, double b)
        {
            setTouchIncreaseLeftRightTopBottom((float)l, (float)r, (float)t, (float)b);
        }

        // Token: 0x060003ED RID: 1005 RVA: 0x0001C3D4 File Offset: 0x0001A5D4
        public void setTouchIncreaseLeftRightTopBottom(float l, float r, float t, float b)
        {
            touchLeftInc = l;
            touchRightInc = r;
            touchTopInc = t;
            touchBottomInc = b;
        }

        // Token: 0x060003EE RID: 1006 RVA: 0x0001C3F3 File Offset: 0x0001A5F3
        public void forceTouchRect(Rectangle r)
        {
            forcedTouchZone = r;
        }

        // Token: 0x060003EF RID: 1007 RVA: 0x0001C3FC File Offset: 0x0001A5FC
        public bool isInTouchZoneXYforTouchDown(float tx, float ty, bool td)
        {
            float num = td ? 0f : 15f;
            if (forcedTouchZone.w != -1f)
            {
                return pointInRect(tx, ty, drawX + forcedTouchZone.x - num, drawY + forcedTouchZone.y - num, forcedTouchZone.w + (num * 2f), forcedTouchZone.h + (num * 2f));
            }
            return pointInRect(tx, ty, drawX - touchLeftInc - num, drawY - touchTopInc - num, width + (touchLeftInc + touchRightInc) + (num * 2f), height + (touchTopInc + touchBottomInc) + (num * 2f));
        }

        // Token: 0x060003F0 RID: 1008 RVA: 0x0001C4E0 File Offset: 0x0001A6E0
        public void setState(TIMED_BUTTON s)
        {
            state = s;
            BaseElement child = getChild(0);
            BaseElement child2 = getChild(1);
            child.setEnabled(s == TIMED_BUTTON.TIMED_BUTTON_UP);
            child2.setEnabled(s == TIMED_BUTTON.TIMED_BUTTON_DOWN);
        }

        // Token: 0x060003F1 RID: 1009 RVA: 0x0001C518 File Offset: 0x0001A718
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

        // Token: 0x060003F2 RID: 1010 RVA: 0x0001C554 File Offset: 0x0001A754
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

        // Token: 0x060003F3 RID: 1011 RVA: 0x0001C5DB File Offset: 0x0001A7DB
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

        // Token: 0x060003F4 RID: 1012 RVA: 0x0001C608 File Offset: 0x0001A808
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

        // Token: 0x060003F5 RID: 1013 RVA: 0x0001C64A File Offset: 0x0001A84A
        public BaseElement createFromXML(XMLNode xml)
        {
            throw new NotImplementedException();
        }

        // Token: 0x060003F6 RID: 1014 RVA: 0x0001C654 File Offset: 0x0001A854
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

        // Token: 0x04000953 RID: 2387
        private int buttonID;

        // Token: 0x04000954 RID: 2388
        public float timer;

        // Token: 0x04000955 RID: 2389
        private bool timelinePlayed;

        // Token: 0x04000956 RID: 2390
        private float time;

        // Token: 0x04000957 RID: 2391
        private TIMED_BUTTON state;

        // Token: 0x04000958 RID: 2392
        public ButtonDelegate delegateButtonDelegate;

        // Token: 0x04000959 RID: 2393
        private float touchLeftInc;

        // Token: 0x0400095A RID: 2394
        private float touchRightInc;

        // Token: 0x0400095B RID: 2395
        private float touchTopInc;

        // Token: 0x0400095C RID: 2396
        private float touchBottomInc;

        // Token: 0x0400095D RID: 2397
        private Rectangle forcedTouchZone;

        // Token: 0x02000088 RID: 136
        public enum TIMED_BUTTON
        {
            // Token: 0x0400095F RID: 2399
            TIMED_BUTTON_UP,
            // Token: 0x04000960 RID: 2400
            TIMED_BUTTON_DOWN
        }
    }
}
