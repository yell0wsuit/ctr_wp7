using System;

using ctr_wp7.iframework.helpers;
using ctr_wp7.ios;

namespace ctr_wp7.iframework.visual
{
    // Token: 0x02000087 RID: 135
    internal class TimedButton : BaseElement
    {
        // Token: 0x060003E9 RID: 1001 RVA: 0x0001C1DC File Offset: 0x0001A3DC
        public static TimedButton createWithTextureUpDownID(Texture2D up, Texture2D down, int bID)
        {
            Image image = Image.Image_create(up);
            Image image2 = Image.Image_create(down);
            return new TimedButton().initWithUpElementDownElementandID(image, image2, bID);
        }

        // Token: 0x060003EA RID: 1002 RVA: 0x0001C204 File Offset: 0x0001A404
        public virtual TimedButton initWithID(int n)
        {
            if (base.init() != null)
            {
                this.buttonID = n;
                this.state = TimedButton.TIMED_BUTTON.TIMED_BUTTON_UP;
                this.touchLeftInc = 0f;
                this.touchRightInc = 0f;
                this.touchTopInc = 0f;
                this.touchBottomInc = 0f;
                this.forcedTouchZone = new Rectangle(-1f, -1f, -1f, -1f);
            }
            return this;
        }

        // Token: 0x060003EB RID: 1003 RVA: 0x0001C274 File Offset: 0x0001A474
        public virtual TimedButton initWithUpElementDownElementandID(BaseElement up, BaseElement down, int n)
        {
            if (this.initWithID(n) != null)
            {
                up.parentAnchor = (down.parentAnchor = 9);
                this.addChildwithID(up, 0);
                this.addChildwithID(down, 1);
                this.setState(TimedButton.TIMED_BUTTON.TIMED_BUTTON_UP);
                Timeline timeline = new Timeline().initWithMaxKeyFramesOnTrack(8);
                timeline.addKeyFrame(KeyFrame.makeSingleAction(this.getChild(0), "ACTION_SET_VISIBLE", 1, 1, 0f));
                timeline.addKeyFrame(KeyFrame.makeSingleAction(this.getChild(1), "ACTION_SET_VISIBLE", 0, 0, 0f));
                timeline.addKeyFrame(KeyFrame.makeSingleAction(this.getChild(1), "ACTION_SET_VISIBLE", 1, 1, 0.1f));
                timeline.addKeyFrame(KeyFrame.makeSingleAction(this.getChild(0), "ACTION_SET_VISIBLE", 0, 0, 0f));
                timeline.addKeyFrame(KeyFrame.makeSingleAction(this.getChild(1), "ACTION_SET_VISIBLE", 0, 0, 0.2f));
                timeline.addKeyFrame(KeyFrame.makeSingleAction(this.getChild(0), "ACTION_SET_VISIBLE", 1, 1, 0f));
                timeline.addKeyFrame(KeyFrame.makeSingleAction(this.getChild(1), "ACTION_SET_VISIBLE", 1, 1, 0.1f));
                timeline.addKeyFrame(KeyFrame.makeSingleAction(this.getChild(0), "ACTION_SET_VISIBLE", 0, 0, 0f));
                this.addTimelinewithID(timeline, 0);
                this.timelinePlayed = false;
            }
            return this;
        }

        // Token: 0x060003EC RID: 1004 RVA: 0x0001C3C3 File Offset: 0x0001A5C3
        public void setTouchIncreaseLeftRightTopBottom(double l, double r, double t, double b)
        {
            this.setTouchIncreaseLeftRightTopBottom((float)l, (float)r, (float)t, (float)b);
        }

        // Token: 0x060003ED RID: 1005 RVA: 0x0001C3D4 File Offset: 0x0001A5D4
        public virtual void setTouchIncreaseLeftRightTopBottom(float l, float r, float t, float b)
        {
            this.touchLeftInc = l;
            this.touchRightInc = r;
            this.touchTopInc = t;
            this.touchBottomInc = b;
        }

        // Token: 0x060003EE RID: 1006 RVA: 0x0001C3F3 File Offset: 0x0001A5F3
        public virtual void forceTouchRect(Rectangle r)
        {
            this.forcedTouchZone = r;
        }

        // Token: 0x060003EF RID: 1007 RVA: 0x0001C3FC File Offset: 0x0001A5FC
        public virtual bool isInTouchZoneXYforTouchDown(float tx, float ty, bool td)
        {
            float num = (td ? 0f : 15f);
            if (this.forcedTouchZone.w != -1f)
            {
                return MathHelper.pointInRect(tx, ty, this.drawX + this.forcedTouchZone.x - num, this.drawY + this.forcedTouchZone.y - num, this.forcedTouchZone.w + num * 2f, this.forcedTouchZone.h + num * 2f);
            }
            return MathHelper.pointInRect(tx, ty, this.drawX - this.touchLeftInc - num, this.drawY - this.touchTopInc - num, (float)this.width + (this.touchLeftInc + this.touchRightInc) + num * 2f, (float)this.height + (this.touchTopInc + this.touchBottomInc) + num * 2f);
        }

        // Token: 0x060003F0 RID: 1008 RVA: 0x0001C4E0 File Offset: 0x0001A6E0
        public virtual void setState(TimedButton.TIMED_BUTTON s)
        {
            this.state = s;
            BaseElement child = this.getChild(0);
            BaseElement child2 = this.getChild(1);
            child.setEnabled(s == TimedButton.TIMED_BUTTON.TIMED_BUTTON_UP);
            child2.setEnabled(s == TimedButton.TIMED_BUTTON.TIMED_BUTTON_DOWN);
        }

        // Token: 0x060003F1 RID: 1009 RVA: 0x0001C518 File Offset: 0x0001A718
        public override bool onTouchDownXY(float tx, float ty)
        {
            base.onTouchDownXY(tx, ty);
            if (this.state == TimedButton.TIMED_BUTTON.TIMED_BUTTON_UP && this.isInTouchZoneXYforTouchDown(tx, ty, true))
            {
                this.setState(TimedButton.TIMED_BUTTON.TIMED_BUTTON_DOWN);
                this.time = this.timer;
                this.timelinePlayed = false;
                return true;
            }
            return false;
        }

        // Token: 0x060003F2 RID: 1010 RVA: 0x0001C554 File Offset: 0x0001A754
        public override bool onTouchUpXY(float tx, float ty)
        {
            base.onTouchUpXY(tx, ty);
            if (this.state == TimedButton.TIMED_BUTTON.TIMED_BUTTON_DOWN)
            {
                this.setState(TimedButton.TIMED_BUTTON.TIMED_BUTTON_UP);
                this.timelinePlayed = false;
                if (this.isInTouchZoneXYforTouchDown(tx, ty, false) && this.time <= 0f)
                {
                    if (this.delegateButtonDelegate != null)
                    {
                        this.delegateButtonDelegate.onButtonPressed(this.buttonID);
                    }
                    Timeline currentTimeline = this.getCurrentTimeline();
                    if (currentTimeline != null)
                    {
                        this.stopCurrentTimeline();
                    }
                    this.time = -1f;
                    return true;
                }
                this.time = -1f;
            }
            return false;
        }

        // Token: 0x060003F3 RID: 1011 RVA: 0x0001C5DB File Offset: 0x0001A7DB
        public override bool onTouchMoveXY(float tx, float ty)
        {
            base.onTouchMoveXY(tx, ty);
            if (this.state == TimedButton.TIMED_BUTTON.TIMED_BUTTON_DOWN)
            {
                if (this.isInTouchZoneXYforTouchDown(tx, ty, false))
                {
                    return true;
                }
                this.setState(TimedButton.TIMED_BUTTON.TIMED_BUTTON_UP);
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
                this.width = c.width;
                this.height = c.height;
                this.setState(TimedButton.TIMED_BUTTON.TIMED_BUTTON_UP);
            }
            return num;
        }

        // Token: 0x060003F5 RID: 1013 RVA: 0x0001C64A File Offset: 0x0001A84A
        public virtual BaseElement createFromXML(XMLNode xml)
        {
            throw new NotImplementedException();
        }

        // Token: 0x060003F6 RID: 1014 RVA: 0x0001C654 File Offset: 0x0001A854
        public override void update(float delta)
        {
            base.update(delta);
            if (this.time > 0f && this.state == TimedButton.TIMED_BUTTON.TIMED_BUTTON_DOWN)
            {
                this.time -= delta;
                if (this.time <= 0f && !this.timelinePlayed)
                {
                    this.playTimeline(0);
                    this.timelinePlayed = true;
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
        private TimedButton.TIMED_BUTTON state;

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
