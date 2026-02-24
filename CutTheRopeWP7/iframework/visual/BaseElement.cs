using System;
using System.Collections.Generic;
using System.Linq;

using ctr_wp7.ios;

namespace ctr_wp7.iframework.visual
{
    // Token: 0x02000009 RID: 9
    internal class BaseElement : NSObject
    {
        // Token: 0x06000079 RID: 121 RVA: 0x00005A52 File Offset: 0x00003C52
        public bool AnchorHas(int f)
        {
            return ((int)this.anchor & f) != 0;
        }

        // Token: 0x0600007A RID: 122 RVA: 0x00005A62 File Offset: 0x00003C62
        public bool ParentAnchorHas(int f)
        {
            return ((int)this.parentAnchor & f) != 0;
        }

        // Token: 0x17000003 RID: 3
        // (get) Token: 0x0600007B RID: 123 RVA: 0x00005A72 File Offset: 0x00003C72
        public bool HasParent
        {
            get
            {
                return this.parent != null;
            }
        }

        // Token: 0x0600007C RID: 124 RVA: 0x00005A80 File Offset: 0x00003C80
        public static void calculateTopLeft(BaseElement e)
        {
            float num = (e.HasParent ? e.parent.drawX : 0f);
            float num2 = (e.HasParent ? e.parent.drawY : 0f);
            int num3 = (e.HasParent ? e.parent.width : 0);
            int num4 = (e.HasParent ? e.parent.height : 0);
            if (e.parentAnchor != -1)
            {
                if ((e.parentAnchor & 1) != 0)
                {
                    e.drawX = num + e.x;
                }
                else if ((e.parentAnchor & 2) != 0)
                {
                    e.drawX = num + e.x + (float)(num3 >> 1);
                }
                else if ((e.parentAnchor & 4) != 0)
                {
                    e.drawX = num + e.x + (float)num3;
                }
                if ((e.parentAnchor & 8) != 0)
                {
                    e.drawY = num2 + e.y;
                }
                else if ((e.parentAnchor & 16) != 0)
                {
                    e.drawY = num2 + e.y + (float)(num4 >> 1);
                }
                else if ((e.parentAnchor & 32) != 0)
                {
                    e.drawY = num2 + e.y + (float)num4;
                }
            }
            else
            {
                e.drawX = e.x;
                e.drawY = e.y;
            }
            if ((e.anchor & 8) == 0)
            {
                if ((e.anchor & 16) != 0)
                {
                    e.drawY -= (float)(e.height >> 1);
                }
                else if ((e.anchor & 32) != 0)
                {
                    e.drawY -= (float)e.height;
                }
            }
            if ((e.anchor & 1) == 0)
            {
                if ((e.anchor & 2) != 0)
                {
                    e.drawX -= (float)(e.width >> 1);
                    return;
                }
                if ((e.anchor & 4) != 0)
                {
                    e.drawX -= (float)e.width;
                }
            }
        }

        // Token: 0x0600007D RID: 125 RVA: 0x00005C58 File Offset: 0x00003E58
        protected static void restoreTransformations(BaseElement t)
        {
            if (t.pushM || (double)t.rotation != 0.0 || (double)t.scaleX != 1.0 || (double)t.scaleY != 1.0 || (double)t.translateX != 0.0 || (double)t.translateY != 0.0)
            {
                OpenGL.glPopMatrix();
                t.pushM = false;
            }
        }

        // Token: 0x0600007E RID: 126 RVA: 0x00005CD3 File Offset: 0x00003ED3
        protected static void restoreColor(BaseElement t)
        {
            if (!RGBAColor.RGBAEqual(t.color, RGBAColor.solidOpaqueRGBA))
            {
                OpenGL.SetWhiteColor();
            }
        }

        // Token: 0x0600007F RID: 127 RVA: 0x00005CEC File Offset: 0x00003EEC
        public override NSObject init()
        {
            this.visible = true;
            this.touchable = true;
            this.updateable = true;
            this.name = null;
            this.x = 0f;
            this.y = 0f;
            this.drawX = 0f;
            this.drawY = 0f;
            this.width = 0;
            this.height = 0;
            this.rotation = 0f;
            this.rotationCenterX = 0f;
            this.rotationCenterY = 0f;
            this.scaleX = 1f;
            this.scaleY = 1f;
            this.color = RGBAColor.solidOpaqueRGBA;
            this.translateX = 0f;
            this.translateY = 0f;
            this.parentAnchor = -1;
            this.parent = null;
            this.anchor = 9;
            this.childs = new Dictionary<int, BaseElement>();
            this.timelines = new Dictionary<int, Timeline>();
            this.currentTimeline = null;
            this.currentTimelineIndex = -1;
            this.passTransformationsToChilds = true;
            this.passColorToChilds = true;
            this.passTouchEventsToAllChilds = false;
            this.blendingMode = -1;
            return this;
        }

        // Token: 0x06000080 RID: 128 RVA: 0x00005E00 File Offset: 0x00004000
        public virtual void preDraw()
        {
            BaseElement.calculateTopLeft(this);
            bool flag = (double)this.scaleX != 1.0 || (double)this.scaleY != 1.0;
            bool flag2 = (double)this.rotation != 0.0;
            bool flag3 = (double)this.translateX != 0.0 || (double)this.translateY != 0.0;
            if (flag || flag2 || flag3)
            {
                OpenGL.glPushMatrix();
                this.pushM = true;
                if (flag || flag2)
                {
                    float num = this.drawX + (float)(this.width >> 1) + this.rotationCenterX;
                    float num2 = this.drawY + (float)(this.height >> 1) + this.rotationCenterY;
                    OpenGL.glTranslatef(num, num2, 0f);
                    if (flag2)
                    {
                        OpenGL.glRotatef(this.rotation, 0f, 0f, 1f);
                    }
                    if (flag)
                    {
                        OpenGL.glScalef(this.scaleX, this.scaleY, 1f);
                    }
                    OpenGL.glTranslatef(-num, -num2, 0f);
                }
                if (flag3)
                {
                    OpenGL.glTranslatef(this.translateX, this.translateY, 0f);
                }
            }
            if (!RGBAColor.RGBAEqual(this.color, RGBAColor.solidOpaqueRGBA))
            {
                OpenGL.glColor4f(1f, 1f, 1f, this.color.a);
            }
            if (this.blendingMode != -1)
            {
                switch (this.blendingMode)
                {
                    case 0:
                        OpenGL.glBlendFunc(BlendingFactor.GL_SRC_ALPHA, BlendingFactor.GL_ONE_MINUS_SRC_ALPHA);
                        return;
                    case 1:
                        OpenGL.glBlendFunc(BlendingFactor.GL_ONE, BlendingFactor.GL_ONE_MINUS_SRC_ALPHA);
                        return;
                    case 2:
                        OpenGL.glBlendFunc(BlendingFactor.GL_SRC_ALPHA, BlendingFactor.GL_ONE);
                        break;
                    default:
                        return;
                }
            }
        }

        // Token: 0x06000081 RID: 129 RVA: 0x00005FB9 File Offset: 0x000041B9
        public virtual void draw()
        {
            this.preDraw();
            this.postDraw();
        }

        // Token: 0x06000082 RID: 130 RVA: 0x00005FC8 File Offset: 0x000041C8
        public virtual void postDraw()
        {
            if (!this.passTransformationsToChilds)
            {
                BaseElement.restoreTransformations(this);
            }
            if (!this.passColorToChilds)
            {
                BaseElement.restoreColor(this);
            }
            int i = 0;
            int num = 0;
            while (i < this.childs.Count)
            {
                BaseElement baseElement;
                bool flag = this.childs.TryGetValue(num, out baseElement);
                if (flag)
                {
                    if (baseElement != null && baseElement.visible)
                    {
                        baseElement.draw();
                    }
                    i++;
                }
                num++;
            }
            if (this.passTransformationsToChilds)
            {
                BaseElement.restoreTransformations(this);
            }
            if (this.passColorToChilds)
            {
                BaseElement.restoreColor(this);
            }
        }

        // Token: 0x06000083 RID: 131 RVA: 0x0000604C File Offset: 0x0000424C
        public virtual void update(float delta)
        {
            int i = 0;
            int num = 0;
            while (i < this.childs.Count)
            {
                BaseElement baseElement;
                bool flag = this.childs.TryGetValue(num, out baseElement);
                if (flag)
                {
                    if (baseElement != null && baseElement.updateable)
                    {
                        baseElement.update(delta);
                    }
                    i++;
                }
                num++;
            }
            if (this.currentTimeline != null)
            {
                Timeline.updateTimeline(this.currentTimeline, delta);
            }
        }

        // Token: 0x06000084 RID: 132 RVA: 0x000060AD File Offset: 0x000042AD
        public BaseElement getChildWithName(NSString n)
        {
            return this.getChildWithName(n.ToString());
        }

        // Token: 0x06000085 RID: 133 RVA: 0x000060BC File Offset: 0x000042BC
        public BaseElement getChildWithName(string n)
        {
            foreach (KeyValuePair<int, BaseElement> keyValuePair in this.childs)
            {
                BaseElement value = keyValuePair.Value;
                if (value != null)
                {
                    if (value.name != null && value.name.isEqualToString(n))
                    {
                        return value;
                    }
                    BaseElement childWithName = value.getChildWithName(n);
                    if (childWithName != null)
                    {
                        return childWithName;
                    }
                }
            }
            return null;
        }

        // Token: 0x06000086 RID: 134 RVA: 0x00006140 File Offset: 0x00004340
        public void setSizeToChildsBounds()
        {
            BaseElement.calculateTopLeft(this);
            float num = this.drawX;
            float num2 = this.drawY;
            float num3 = this.drawX + (float)this.width;
            float num4 = this.drawY + (float)this.height;
            foreach (KeyValuePair<int, BaseElement> keyValuePair in this.childs)
            {
                BaseElement value = keyValuePair.Value;
                if (value != null)
                {
                    BaseElement.calculateTopLeft(value);
                    if (value.drawX < num)
                    {
                        num = value.drawX;
                    }
                    if (value.drawY < num2)
                    {
                        num2 = value.drawY;
                    }
                    if (value.drawX + (float)value.width > num3)
                    {
                        num3 = value.drawX + (float)value.width;
                    }
                    if (value.drawX + (float)value.height > num4)
                    {
                        num4 = value.drawY + (float)value.height;
                    }
                }
            }
            this.width = (int)(num3 - num);
            this.height = (int)(num4 - num2);
        }

        // Token: 0x06000087 RID: 135 RVA: 0x0000625C File Offset: 0x0000445C
        public virtual bool handleAction(ActionData a)
        {
            if (a.actionName == "ACTION_SET_VISIBLE")
            {
                this.visible = a.actionSubParam != 0;
            }
            else if (a.actionName == "ACTION_SET_UPDATEABLE")
            {
                this.updateable = a.actionSubParam != 0;
            }
            else if (a.actionName == "ACTION_SET_TOUCHABLE")
            {
                this.touchable = a.actionSubParam != 0;
            }
            else if (a.actionName == "ACTION_PLAY_TIMELINE")
            {
                this.playTimeline(a.actionSubParam);
            }
            else if (a.actionName == "ACTION_PAUSE_TIMELINE")
            {
                this.pauseCurrentTimeline();
            }
            else if (a.actionName == "ACTION_STOP_TIMELINE")
            {
                this.stopCurrentTimeline();
            }
            else
            {
                if (!(a.actionName == "ACTION_JUMP_TO_TIMELINE_FRAME"))
                {
                    return false;
                }
                this.getCurrentTimeline().jumpToTrackKeyFrame(a.actionParam, a.actionSubParam);
            }
            return true;
        }

        // Token: 0x06000088 RID: 136 RVA: 0x00006368 File Offset: 0x00004568
        private BaseElement createFromXML(XMLNode xml)
        {
            return new BaseElement();
        }

        // Token: 0x06000089 RID: 137 RVA: 0x0000637C File Offset: 0x0000457C
        private int parseAlignmentString(NSString s)
        {
            int num = 0;
            if (s.rangeOfString("LEFT").length > 0U)
            {
                num = 1;
            }
            else if (s.rangeOfString("HCENTER").length > 0U || s.isEqualToString("CENTER"))
            {
                num = 2;
            }
            else if (s.rangeOfString("RIGHT").length > 0U)
            {
                num = 4;
            }
            if (s.rangeOfString("TOP").length > 0U)
            {
                num |= 8;
            }
            else if (s.rangeOfString("VCENTER").length > 0U || s.isEqualToString("CENTER"))
            {
                num |= 16;
            }
            else if (s.rangeOfString("BOTTOM").length > 0U)
            {
                num |= 32;
            }
            return num;
        }

        // Token: 0x0600008A RID: 138 RVA: 0x00006434 File Offset: 0x00004634
        public virtual int addChild(BaseElement c)
        {
            return this.addChildwithID(c, -1);
        }

        // Token: 0x0600008B RID: 139 RVA: 0x00006440 File Offset: 0x00004640
        public virtual int addChildwithID(BaseElement c, int i)
        {
            c.parent = this;
            BaseElement baseElement2;
            if (i == -1)
            {
                i = 0;
                bool flag = false;
                BaseElement baseElement;
                while (this.childs.TryGetValue(i, out baseElement))
                {
                    if (baseElement == null)
                    {
                        this.childs[i] = c;
                        flag = true;
                        break;
                    }
                    i++;
                }
                if (!flag)
                {
                    this.childs.Add(i, c);
                }
            }
            else if (this.childs.TryGetValue(i, out baseElement2))
            {
                if (baseElement2 != c)
                {
                    baseElement2.dealloc();
                }
                this.childs[i] = c;
            }
            else
            {
                this.childs.Add(i, c);
            }
            return i;
        }

        // Token: 0x0600008C RID: 140 RVA: 0x000064D4 File Offset: 0x000046D4
        public virtual void removeChildWithID(int i)
        {
            BaseElement baseElement = null;
            if (this.childs.TryGetValue(i, out baseElement))
            {
                if (baseElement != null)
                {
                    baseElement.parent = null;
                }
                this.childs.Remove(i);
            }
        }

        // Token: 0x0600008D RID: 141 RVA: 0x0000650A File Offset: 0x0000470A
        public void removeAllChilds()
        {
            this.childs.Clear();
        }

        // Token: 0x0600008E RID: 142 RVA: 0x00006518 File Offset: 0x00004718
        public virtual void removeChild(BaseElement c)
        {
            foreach (KeyValuePair<int, BaseElement> keyValuePair in this.childs)
            {
                if (c.Equals(keyValuePair.Value))
                {
                    this.childs.Remove(keyValuePair.Key);
                    break;
                }
            }
        }

        // Token: 0x0600008F RID: 143 RVA: 0x00006588 File Offset: 0x00004788
        public virtual BaseElement getChild(int i)
        {
            BaseElement baseElement = null;
            this.childs.TryGetValue(i, out baseElement);
            return baseElement;
        }

        // Token: 0x06000090 RID: 144 RVA: 0x000065A8 File Offset: 0x000047A8
        public virtual int getChildId(BaseElement c)
        {
            int num = -1;
            foreach (KeyValuePair<int, BaseElement> keyValuePair in this.childs)
            {
                if (c.Equals(keyValuePair.Value))
                {
                    num = keyValuePair.Key;
                    break;
                }
            }
            return num;
        }

        // Token: 0x06000091 RID: 145 RVA: 0x00006610 File Offset: 0x00004810
        public virtual int childsCount()
        {
            return this.childs.Count;
        }

        // Token: 0x06000092 RID: 146 RVA: 0x0000661D File Offset: 0x0000481D
        public virtual Dictionary<int, BaseElement> getChilds()
        {
            return this.childs;
        }

        // Token: 0x06000093 RID: 147 RVA: 0x00006628 File Offset: 0x00004828
        public virtual int addTimeline(Timeline t)
        {
            int count = this.timelines.Count;
            this.addTimelinewithID(t, count);
            return count;
        }

        // Token: 0x06000094 RID: 148 RVA: 0x0000664A File Offset: 0x0000484A
        public virtual void addTimelinewithID(Timeline t, int i)
        {
            t.element = this;
            this.timelines[i] = t;
        }

        // Token: 0x06000095 RID: 149 RVA: 0x00006660 File Offset: 0x00004860
        public virtual void removeTimeline(int i)
        {
            if (this.currentTimelineIndex == i)
            {
                this.stopCurrentTimeline();
            }
            this.timelines.Remove(i);
        }

        // Token: 0x06000096 RID: 150 RVA: 0x00006680 File Offset: 0x00004880
        public virtual void playTimeline(int t)
        {
            Timeline timeline = null;
            this.timelines.TryGetValue(t, out timeline);
            if (timeline == null)
            {
                return;
            }
            if (this.currentTimeline != null && this.currentTimeline.state != Timeline.TimelineState.TIMELINE_STOPPED)
            {
                this.currentTimeline.stopTimeline();
            }
            this.currentTimelineIndex = t;
            this.currentTimeline = timeline;
            this.currentTimeline.playTimeline();
        }

        // Token: 0x06000097 RID: 151 RVA: 0x000066DB File Offset: 0x000048DB
        public virtual void _playTimeline(NSObject obj)
        {
            this.playTimeline(((NSInt)obj).intValue());
        }

        // Token: 0x06000098 RID: 152 RVA: 0x000066EE File Offset: 0x000048EE
        public virtual void pauseCurrentTimeline()
        {
            this.currentTimeline.pauseTimeline();
        }

        // Token: 0x06000099 RID: 153 RVA: 0x000066FB File Offset: 0x000048FB
        public virtual void stopCurrentTimeline()
        {
            this.currentTimeline.stopTimeline();
            this.currentTimeline = null;
            this.currentTimelineIndex = -1;
        }

        // Token: 0x0600009A RID: 154 RVA: 0x00006716 File Offset: 0x00004916
        public virtual Timeline getCurrentTimeline()
        {
            return this.currentTimeline;
        }

        // Token: 0x0600009B RID: 155 RVA: 0x0000671E File Offset: 0x0000491E
        public int getCurrentTimelineIndex()
        {
            return this.currentTimelineIndex;
        }

        // Token: 0x0600009C RID: 156 RVA: 0x00006728 File Offset: 0x00004928
        public virtual Timeline getTimeline(int n)
        {
            Timeline timeline = null;
            this.timelines.TryGetValue(n, out timeline);
            return timeline;
        }

        // Token: 0x0600009D RID: 157 RVA: 0x00006747 File Offset: 0x00004947
        public virtual bool hasTimeline(int n)
        {
            return n >= 0 && n < this.timelines.Count && this.timelines[n] != null;
        }

        // Token: 0x0600009E RID: 158 RVA: 0x00006770 File Offset: 0x00004970
        public virtual bool onTouchDownXY(float tx, float ty)
        {
            bool flag = false;
            foreach (KeyValuePair<int, BaseElement> keyValuePair in Enumerable.Reverse<KeyValuePair<int, BaseElement>>(this.childs))
            {
                BaseElement value = keyValuePair.Value;
                if (value != null && value.touchable && value.onTouchDownXY(tx, ty) && !flag)
                {
                    flag = true;
                    if (!this.passTouchEventsToAllChilds)
                    {
                        return flag;
                    }
                }
            }
            return flag;
        }

        // Token: 0x0600009F RID: 159 RVA: 0x000067F4 File Offset: 0x000049F4
        public virtual bool onTouchUpXY(float tx, float ty)
        {
            bool flag = false;
            foreach (KeyValuePair<int, BaseElement> keyValuePair in Enumerable.Reverse<KeyValuePair<int, BaseElement>>(this.childs))
            {
                BaseElement value = keyValuePair.Value;
                if (value != null && value.touchable && value.onTouchUpXY(tx, ty) && !flag)
                {
                    flag = true;
                    if (!this.passTouchEventsToAllChilds)
                    {
                        return flag;
                    }
                }
            }
            return flag;
        }

        // Token: 0x060000A0 RID: 160 RVA: 0x00006878 File Offset: 0x00004A78
        public virtual bool onTouchMoveXY(float tx, float ty)
        {
            bool flag = false;
            foreach (KeyValuePair<int, BaseElement> keyValuePair in Enumerable.Reverse<KeyValuePair<int, BaseElement>>(this.childs))
            {
                BaseElement value = keyValuePair.Value;
                if (value != null && value.touchable && value.onTouchMoveXY(tx, ty) && !flag)
                {
                    flag = true;
                    if (!this.passTouchEventsToAllChilds)
                    {
                        return flag;
                    }
                }
            }
            return flag;
        }

        // Token: 0x060000A1 RID: 161 RVA: 0x000068FC File Offset: 0x00004AFC
        public void setEnabled(bool e)
        {
            this.visible = e;
            this.touchable = e;
            this.updateable = e;
        }

        // Token: 0x060000A2 RID: 162 RVA: 0x00006913 File Offset: 0x00004B13
        public bool isEnabled()
        {
            return this.visible && this.touchable && this.updateable;
        }

        // Token: 0x060000A3 RID: 163 RVA: 0x0000692D File Offset: 0x00004B2D
        public void setName(string n)
        {
            NSObject.NSREL(this.name);
            this.name = new NSString(n);
        }

        // Token: 0x060000A4 RID: 164 RVA: 0x00006946 File Offset: 0x00004B46
        public void setName(NSString n)
        {
            NSObject.NSREL(this.name);
            this.name = n;
        }

        // Token: 0x060000A5 RID: 165 RVA: 0x0000695C File Offset: 0x00004B5C
        public virtual void show()
        {
            foreach (KeyValuePair<int, BaseElement> keyValuePair in this.childs)
            {
                BaseElement value = keyValuePair.Value;
                if (value != null && value.visible)
                {
                    value.show();
                }
            }
        }

        // Token: 0x060000A6 RID: 166 RVA: 0x000069C4 File Offset: 0x00004BC4
        public virtual void hide()
        {
            foreach (KeyValuePair<int, BaseElement> keyValuePair in this.childs)
            {
                BaseElement value = keyValuePair.Value;
                if (value != null && value.visible)
                {
                    value.hide();
                }
            }
        }

        // Token: 0x060000A7 RID: 167 RVA: 0x00006A2C File Offset: 0x00004C2C
        public override void dealloc()
        {
            this.childs.Clear();
            this.childs = null;
            this.timelines.Clear();
            this.timelines = null;
            NSObject.NSREL(this.name);
            base.dealloc();
        }

        // Token: 0x040006F8 RID: 1784
        public const string ACTION_SET_VISIBLE = "ACTION_SET_VISIBLE";

        // Token: 0x040006F9 RID: 1785
        public const string ACTION_SET_TOUCHABLE = "ACTION_SET_TOUCHABLE";

        // Token: 0x040006FA RID: 1786
        public const string ACTION_SET_UPDATEABLE = "ACTION_SET_UPDATEABLE";

        // Token: 0x040006FB RID: 1787
        public const string ACTION_PLAY_TIMELINE = "ACTION_PLAY_TIMELINE";

        // Token: 0x040006FC RID: 1788
        public const string ACTION_PAUSE_TIMELINE = "ACTION_PAUSE_TIMELINE";

        // Token: 0x040006FD RID: 1789
        public const string ACTION_STOP_TIMELINE = "ACTION_STOP_TIMELINE";

        // Token: 0x040006FE RID: 1790
        public const string ACTION_JUMP_TO_TIMELINE_FRAME = "ACTION_JUMP_TO_TIMELINE_FRAME";

        // Token: 0x040006FF RID: 1791
        private bool pushM;

        // Token: 0x04000700 RID: 1792
        public bool visible;

        // Token: 0x04000701 RID: 1793
        public bool touchable;

        // Token: 0x04000702 RID: 1794
        public bool updateable;

        // Token: 0x04000703 RID: 1795
        private NSString name;

        // Token: 0x04000704 RID: 1796
        public float x;

        // Token: 0x04000705 RID: 1797
        public float y;

        // Token: 0x04000706 RID: 1798
        public float drawX;

        // Token: 0x04000707 RID: 1799
        public float drawY;

        // Token: 0x04000708 RID: 1800
        public int width;

        // Token: 0x04000709 RID: 1801
        public int height;

        // Token: 0x0400070A RID: 1802
        public float rotation;

        // Token: 0x0400070B RID: 1803
        public float rotationCenterX;

        // Token: 0x0400070C RID: 1804
        public float rotationCenterY;

        // Token: 0x0400070D RID: 1805
        public float scaleX;

        // Token: 0x0400070E RID: 1806
        public float scaleY;

        // Token: 0x0400070F RID: 1807
        public RGBAColor color;

        // Token: 0x04000710 RID: 1808
        private float translateX;

        // Token: 0x04000711 RID: 1809
        private float translateY;

        // Token: 0x04000712 RID: 1810
        public sbyte anchor;

        // Token: 0x04000713 RID: 1811
        public sbyte parentAnchor;

        // Token: 0x04000714 RID: 1812
        public bool passTransformationsToChilds;

        // Token: 0x04000715 RID: 1813
        public bool passColorToChilds;

        // Token: 0x04000716 RID: 1814
        private bool passTouchEventsToAllChilds;

        // Token: 0x04000717 RID: 1815
        public int blendingMode;

        // Token: 0x04000718 RID: 1816
        public BaseElement parent;

        // Token: 0x04000719 RID: 1817
        protected Dictionary<int, BaseElement> childs;

        // Token: 0x0400071A RID: 1818
        protected Dictionary<int, Timeline> timelines;

        // Token: 0x0400071B RID: 1819
        private int currentTimelineIndex;

        // Token: 0x0400071C RID: 1820
        private Timeline currentTimeline;
    }
}
