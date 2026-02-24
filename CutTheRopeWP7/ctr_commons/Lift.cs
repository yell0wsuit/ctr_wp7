using System;

using ctr_wp7.iframework.visual;

namespace ctr_wp7.ctr_commons
{
    // Token: 0x02000115 RID: 277
    internal sealed class Lift : Button
    {
        // Token: 0x06000868 RID: 2152 RVA: 0x0004BA4B File Offset: 0x00049C4B
        public override bool onTouchDownXY(float tx, float ty)
        {
            startX = tx - x;
            startY = ty - y;
            return base.onTouchDownXY(tx, ty);
        }

        // Token: 0x06000869 RID: 2153 RVA: 0x0004BA71 File Offset: 0x00049C71
        public override bool onTouchUpXY(float tx, float ty)
        {
            startX = 0f;
            startY = 0f;
            return base.onTouchUpXY(tx, ty);
        }

        // Token: 0x0600086A RID: 2154 RVA: 0x0004BA94 File Offset: 0x00049C94
        public override bool onTouchMoveXY(float tx, float ty)
        {
            if (tx == -10000f && ty == -10000f)
            {
                return false;
            }
            if (state == BUTTON_STATE.BUTTON_DOWN)
            {
                x = Math.Max(Math.Min(tx - startX, maxX), minX);
                y = Math.Max(Math.Min(ty - startY, maxY), minY);
                if (maxX != 0f)
                {
                    float num = (x - minX) / (maxX - minX);
                    if (num != xPercent)
                    {
                        xPercent = num;
                        if (liftDelegate != null)
                        {
                            liftDelegate(xPercent, yPercent);
                        }
                    }
                }
                if (maxY != 0f)
                {
                    float num2 = (y - minY) / (maxY - minY);
                    if (num2 != yPercent)
                    {
                        yPercent = num2;
                        if (liftDelegate != null)
                        {
                            liftDelegate(xPercent, yPercent);
                        }
                    }
                }
                return true;
            }
            return base.onTouchMoveXY(tx, ty);
        }

        // Token: 0x0600086B RID: 2155 RVA: 0x0004BBC1 File Offset: 0x00049DC1
        public override void dealloc()
        {
            liftDelegate = null;
            base.dealloc();
        }

        // Token: 0x04000E01 RID: 3585
        public float startX;

        // Token: 0x04000E02 RID: 3586
        public float startY;

        // Token: 0x04000E03 RID: 3587
        public PercentXY liftDelegate;

        // Token: 0x04000E04 RID: 3588
        public float minX;

        // Token: 0x04000E05 RID: 3589
        public float maxX;

        // Token: 0x04000E06 RID: 3590
        public float minY;

        // Token: 0x04000E07 RID: 3591
        public float maxY;

        // Token: 0x04000E08 RID: 3592
        public float xPercent;

        // Token: 0x04000E09 RID: 3593
        public float yPercent;

        // Token: 0x02000116 RID: 278
        // (Invoke) Token: 0x0600086E RID: 2158
        public delegate void PercentXY(float px, float py);
    }
}
