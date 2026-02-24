using System;

using ctr_wp7.iframework.core;

namespace ctr_wp7.iframework.visual
{
    // Token: 0x02000089 RID: 137
    internal class Scrollbar : BaseElement
    {
        // Token: 0x060003F8 RID: 1016 RVA: 0x0001C6B7 File Offset: 0x0001A8B7
        public override void update(float delta)
        {
            base.update(delta);
            if (delegateProvider != null)
            {
                delegateProvider(ref sp, ref mp, ref sc);
            }
        }

        // Token: 0x060003F9 RID: 1017 RVA: 0x0001C6E8 File Offset: 0x0001A8E8
        public override void draw()
        {
            base.preDraw();
            if (vectEqual(sp, vectUndefined) && delegateProvider != null)
            {
                delegateProvider(ref sp, ref mp, ref sc);
            }
            OpenGL.glDisable(0);
            bool flag = false;
            float num;
            float num2;
            float num3;
            float num5;
            if (vertical)
            {
                num = width - 2f;
                num2 = 1f;
                num3 = (float)Math.Round((height - 2.0) / sc.y);
                float num4 = (mp.y != 0f) ? (sp.y / mp.y) : 1f;
                num5 = (float)(1.0 + (height - 2.0 - (double)num3) * (double)num4);
                if (num3 > height)
                {
                    flag = true;
                }
            }
            else
            {
                num3 = height - 2f;
                num5 = 1f;
                num = (float)Math.Round((width - 2.0) / sc.x);
                float num6 = (mp.x != 0f) ? (sp.x / mp.x) : 1f;
                num2 = (float)(1.0 + (width - 2.0 - (double)num) * (double)num6);
                if (num > width)
                {
                    flag = true;
                }
            }
            if (!flag)
            {
                GLDrawer.drawSolidRectWOBorder(drawX, drawY, width, height, backColor);
                GLDrawer.drawSolidRectWOBorder(drawX + num2, drawY + num5, num, num3, scrollerColor);
            }
            OpenGL.glEnable(0);
            OpenGL.SetWhiteColor();
            base.postDraw();
        }

        // Token: 0x060003FA RID: 1018 RVA: 0x0001C8E0 File Offset: 0x0001AAE0
        public virtual Scrollbar initWithWidthHeightVertical(float w, float h, bool v)
        {
            if (base.init() != null)
            {
                width = (int)w;
                height = (int)h;
                vertical = v;
                sp = vectUndefined;
                mp = vectUndefined;
                sc = vectUndefined;
                backColor = RGBAColor.MakeRGBA(1f, 1f, 1f, 0.5f);
                scrollerColor = RGBAColor.MakeRGBA(0f, 0f, 0f, 0.5f);
            }
            return this;
        }

        // Token: 0x04000961 RID: 2401
        public Vector sp;

        // Token: 0x04000962 RID: 2402
        public Vector mp;

        // Token: 0x04000963 RID: 2403
        public Vector sc;

        // Token: 0x04000964 RID: 2404
        public ProvideScrollPosMaxScrollPosScrollCoeff delegateProvider;

        // Token: 0x04000965 RID: 2405
        public bool vertical;

        // Token: 0x04000966 RID: 2406
        public RGBAColor backColor;

        // Token: 0x04000967 RID: 2407
        public RGBAColor scrollerColor;

        // Token: 0x0200008A RID: 138
        // (Invoke) Token: 0x060003FD RID: 1021
        public delegate void ProvideScrollPosMaxScrollPosScrollCoeff(ref Vector sp, ref Vector mp, ref Vector sc);
    }
}
