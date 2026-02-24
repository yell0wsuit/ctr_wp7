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
            return ((int)anchor & f) != 0;
        }

        // Token: 0x0600007A RID: 122 RVA: 0x00005A62 File Offset: 0x00003C62
        public bool ParentAnchorHas(int f)
        {
            return ((int)parentAnchor & f) != 0;
        }

        // Token: 0x17000003 RID: 3
        // (get) Token: 0x0600007B RID: 123 RVA: 0x00005A72 File Offset: 0x00003C72
        public bool HasParent
        {
            get
            {
                return parent != null;
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
            visible = true;
            touchable = true;
            updateable = true;
            name = null;
            x = 0f;
            y = 0f;
            drawX = 0f;
            drawY = 0f;
            width = 0;
            height = 0;
            rotation = 0f;
            rotationCenterX = 0f;
            rotationCenterY = 0f;
            scaleX = 1f;
            scaleY = 1f;
            color = RGBAColor.solidOpaqueRGBA;
            translateX = 0f;
            translateY = 0f;
            parentAnchor = -1;
            parent = null;
            anchor = 9;
            childs = new Dictionary<int, BaseElement>();
            timelines = new Dictionary<int, Timeline>();
            currentTimeline = null;
            currentTimelineIndex = -1;
            passTransformationsToChilds = true;
            passColorToChilds = true;
            passTouchEventsToAllChilds = false;
            blendingMode = -1;
            return this;
        }

        // Token: 0x06000080 RID: 128 RVA: 0x00005E00 File Offset: 0x00004000
        public virtual void preDraw()
        {
            BaseElement.calculateTopLeft(this);
            bool flag = (double)scaleX != 1.0 || (double)scaleY != 1.0;
            bool flag2 = (double)rotation != 0.0;
            bool flag3 = (double)translateX != 0.0 || (double)translateY != 0.0;
            if (flag || flag2 || flag3)
            {
                OpenGL.glPushMatrix();
                pushM = true;
                if (flag || flag2)
                {
                    float num = drawX + (float)(width >> 1) + rotationCenterX;
                    float num2 = drawY + (float)(height >> 1) + rotationCenterY;
                    OpenGL.glTranslatef(num, num2, 0f);
                    if (flag2)
                    {
                        OpenGL.glRotatef(rotation, 0f, 0f, 1f);
                    }
                    if (flag)
                    {
                        OpenGL.glScalef(scaleX, scaleY, 1f);
                    }
                    OpenGL.glTranslatef(-num, -num2, 0f);
                }
                if (flag3)
                {
                    OpenGL.glTranslatef(translateX, translateY, 0f);
                }
            }
            if (!RGBAColor.RGBAEqual(color, RGBAColor.solidOpaqueRGBA))
            {
                OpenGL.glColor4f(1f, 1f, 1f, color.a);
            }
            if (blendingMode != -1)
            {
                switch (blendingMode)
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
            preDraw();
            postDraw();
        }

        // Token: 0x06000082 RID: 130 RVA: 0x00005FC8 File Offset: 0x000041C8
        public virtual void postDraw()
        {
            if (!passTransformationsToChilds)
            {
                BaseElement.restoreTransformations(this);
            }
            if (!passColorToChilds)
            {
                BaseElement.restoreColor(this);
            }
            int i = 0;
            int num = 0;
            while (i < childs.Count)
            {
                BaseElement baseElement;
                bool flag = childs.TryGetValue(num, out baseElement);
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
            if (passTransformationsToChilds)
            {
                BaseElement.restoreTransformations(this);
            }
            if (passColorToChilds)
            {
                BaseElement.restoreColor(this);
            }
        }

        // Token: 0x06000083 RID: 131 RVA: 0x0000604C File Offset: 0x0000424C
        public virtual void update(float delta)
        {
            int i = 0;
            int num = 0;
            while (i < childs.Count)
            {
                BaseElement baseElement;
                bool flag = childs.TryGetValue(num, out baseElement);
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
            if (currentTimeline != null)
            {
                Timeline.updateTimeline(currentTimeline, delta);
            }
        }

        // Token: 0x06000084 RID: 132 RVA: 0x000060AD File Offset: 0x000042AD
        public BaseElement getChildWithName(NSString n)
        {
            return getChildWithName(n.ToString());
        }

        // Token: 0x06000085 RID: 133 RVA: 0x000060BC File Offset: 0x000042BC
        public BaseElement getChildWithName(string n)
        {
            foreach (KeyValuePair<int, BaseElement> keyValuePair in childs)
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
            float num = drawX;
            float num2 = drawY;
            float num3 = drawX + (float)width;
            float num4 = drawY + (float)height;
            foreach (KeyValuePair<int, BaseElement> keyValuePair in childs)
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
            width = (int)(num3 - num);
            height = (int)(num4 - num2);
        }

        // Token: 0x06000087 RID: 135 RVA: 0x0000625C File Offset: 0x0000445C
        public virtual bool handleAction(ActionData a)
        {
            if (a.actionName == "ACTION_SET_VISIBLE")
            {
                visible = a.actionSubParam != 0;
            }
            else if (a.actionName == "ACTION_SET_UPDATEABLE")
            {
                updateable = a.actionSubParam != 0;
            }
            else if (a.actionName == "ACTION_SET_TOUCHABLE")
            {
                touchable = a.actionSubParam != 0;
            }
            else if (a.actionName == "ACTION_PLAY_TIMELINE")
            {
                playTimeline(a.actionSubParam);
            }
            else if (a.actionName == "ACTION_PAUSE_TIMELINE")
            {
                pauseCurrentTimeline();
            }
            else if (a.actionName == "ACTION_STOP_TIMELINE")
            {
                stopCurrentTimeline();
            }
            else
            {
                if (!(a.actionName == "ACTION_JUMP_TO_TIMELINE_FRAME"))
                {
                    return false;
                }
                getCurrentTimeline().jumpToTrackKeyFrame(a.actionParam, a.actionSubParam);
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
            return addChildwithID(c, -1);
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
                while (childs.TryGetValue(i, out baseElement))
                {
                    if (baseElement == null)
                    {
                        childs[i] = c;
                        flag = true;
                        break;
                    }
                    i++;
                }
                if (!flag)
                {
                    childs.Add(i, c);
                }
            }
            else if (childs.TryGetValue(i, out baseElement2))
            {
                if (baseElement2 != c)
                {
                    baseElement2.dealloc();
                }
                childs[i] = c;
            }
            else
            {
                childs.Add(i, c);
            }
            return i;
        }

        // Token: 0x0600008C RID: 140 RVA: 0x000064D4 File Offset: 0x000046D4
        public virtual void removeChildWithID(int i)
        {
            BaseElement baseElement = null;
            if (childs.TryGetValue(i, out baseElement))
            {
                if (baseElement != null)
                {
                    baseElement.parent = null;
                }
                childs.Remove(i);
            }
        }

        // Token: 0x0600008D RID: 141 RVA: 0x0000650A File Offset: 0x0000470A
        public void removeAllChilds()
        {
            childs.Clear();
        }

        // Token: 0x0600008E RID: 142 RVA: 0x00006518 File Offset: 0x00004718
        public virtual void removeChild(BaseElement c)
        {
            foreach (KeyValuePair<int, BaseElement> keyValuePair in childs)
            {
                if (c.Equals(keyValuePair.Value))
                {
                    childs.Remove(keyValuePair.Key);
                    break;
                }
            }
        }

        // Token: 0x0600008F RID: 143 RVA: 0x00006588 File Offset: 0x00004788
        public virtual BaseElement getChild(int i)
        {
            BaseElement baseElement = null;
            childs.TryGetValue(i, out baseElement);
            return baseElement;
        }

        // Token: 0x06000090 RID: 144 RVA: 0x000065A8 File Offset: 0x000047A8
        public virtual int getChildId(BaseElement c)
        {
            int num = -1;
            foreach (KeyValuePair<int, BaseElement> keyValuePair in childs)
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
            return childs.Count;
        }

        // Token: 0x06000092 RID: 146 RVA: 0x0000661D File Offset: 0x0000481D
        public virtual Dictionary<int, BaseElement> getChilds()
        {
            return childs;
        }

        // Token: 0x06000093 RID: 147 RVA: 0x00006628 File Offset: 0x00004828
        public virtual int addTimeline(Timeline t)
        {
            int count = timelines.Count;
            addTimelinewithID(t, count);
            return count;
        }

        // Token: 0x06000094 RID: 148 RVA: 0x0000664A File Offset: 0x0000484A
        public virtual void addTimelinewithID(Timeline t, int i)
        {
            t.element = this;
            timelines[i] = t;
        }

        // Token: 0x06000095 RID: 149 RVA: 0x00006660 File Offset: 0x00004860
        public virtual void removeTimeline(int i)
        {
            if (currentTimelineIndex == i)
            {
                stopCurrentTimeline();
            }
            timelines.Remove(i);
        }

        // Token: 0x06000096 RID: 150 RVA: 0x00006680 File Offset: 0x00004880
        public virtual void playTimeline(int t)
        {
            Timeline timeline = null;
            timelines.TryGetValue(t, out timeline);
            if (timeline == null)
            {
                return;
            }
            if (currentTimeline != null && currentTimeline.state != Timeline.TimelineState.TIMELINE_STOPPED)
            {
                currentTimeline.stopTimeline();
            }
            currentTimelineIndex = t;
            currentTimeline = timeline;
            currentTimeline.playTimeline();
        }

        // Token: 0x06000097 RID: 151 RVA: 0x000066DB File Offset: 0x000048DB
        public virtual void _playTimeline(NSObject obj)
        {
            playTimeline(((NSInt)obj).intValue());
        }

        // Token: 0x06000098 RID: 152 RVA: 0x000066EE File Offset: 0x000048EE
        public virtual void pauseCurrentTimeline()
        {
            currentTimeline.pauseTimeline();
        }

        // Token: 0x06000099 RID: 153 RVA: 0x000066FB File Offset: 0x000048FB
        public virtual void stopCurrentTimeline()
        {
            currentTimeline.stopTimeline();
            currentTimeline = null;
            currentTimelineIndex = -1;
        }

        // Token: 0x0600009A RID: 154 RVA: 0x00006716 File Offset: 0x00004916
        public virtual Timeline getCurrentTimeline()
        {
            return currentTimeline;
        }

        // Token: 0x0600009B RID: 155 RVA: 0x0000671E File Offset: 0x0000491E
        public int getCurrentTimelineIndex()
        {
            return currentTimelineIndex;
        }

        // Token: 0x0600009C RID: 156 RVA: 0x00006728 File Offset: 0x00004928
        public virtual Timeline getTimeline(int n)
        {
            Timeline timeline = null;
            timelines.TryGetValue(n, out timeline);
            return timeline;
        }

        // Token: 0x0600009D RID: 157 RVA: 0x00006747 File Offset: 0x00004947
        public virtual bool hasTimeline(int n)
        {
            return n >= 0 && n < timelines.Count && timelines[n] != null;
        }

        // Token: 0x0600009E RID: 158 RVA: 0x00006770 File Offset: 0x00004970
        public virtual bool onTouchDownXY(float tx, float ty)
        {
            bool flag = false;
            foreach (KeyValuePair<int, BaseElement> keyValuePair in Enumerable.Reverse<KeyValuePair<int, BaseElement>>(childs))
            {
                BaseElement value = keyValuePair.Value;
                if (value != null && value.touchable && value.onTouchDownXY(tx, ty) && !flag)
                {
                    flag = true;
                    if (!passTouchEventsToAllChilds)
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
            foreach (KeyValuePair<int, BaseElement> keyValuePair in Enumerable.Reverse<KeyValuePair<int, BaseElement>>(childs))
            {
                BaseElement value = keyValuePair.Value;
                if (value != null && value.touchable && value.onTouchUpXY(tx, ty) && !flag)
                {
                    flag = true;
                    if (!passTouchEventsToAllChilds)
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
            foreach (KeyValuePair<int, BaseElement> keyValuePair in Enumerable.Reverse<KeyValuePair<int, BaseElement>>(childs))
            {
                BaseElement value = keyValuePair.Value;
                if (value != null && value.touchable && value.onTouchMoveXY(tx, ty) && !flag)
                {
                    flag = true;
                    if (!passTouchEventsToAllChilds)
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
            visible = e;
            touchable = e;
            updateable = e;
        }

        // Token: 0x060000A2 RID: 162 RVA: 0x00006913 File Offset: 0x00004B13
        public bool isEnabled()
        {
            return visible && touchable && updateable;
        }

        // Token: 0x060000A3 RID: 163 RVA: 0x0000692D File Offset: 0x00004B2D
        public void setName(string n)
        {
            NSObject.NSREL(name);
            name = new NSString(n);
        }

        // Token: 0x060000A4 RID: 164 RVA: 0x00006946 File Offset: 0x00004B46
        public void setName(NSString n)
        {
            NSObject.NSREL(name);
            name = n;
        }

        // Token: 0x060000A5 RID: 165 RVA: 0x0000695C File Offset: 0x00004B5C
        public virtual void show()
        {
            foreach (KeyValuePair<int, BaseElement> keyValuePair in childs)
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
            foreach (KeyValuePair<int, BaseElement> keyValuePair in childs)
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
            childs.Clear();
            childs = null;
            timelines.Clear();
            timelines = null;
            NSObject.NSREL(name);
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
