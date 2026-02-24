using System;

using ctr_wp7.iframework.visual;

namespace ctr_wp7.ctr_commons
{
    // Token: 0x02000115 RID: 277
    internal class Lift : Button
    {
        // Token: 0x06000868 RID: 2152 RVA: 0x0004BA4B File Offset: 0x00049C4B
        public override bool onTouchDownXY(float tx, float ty)
        {
            this.startX = tx - this.x;
            this.startY = ty - this.y;
            return base.onTouchDownXY(tx, ty);
        }

        // Token: 0x06000869 RID: 2153 RVA: 0x0004BA71 File Offset: 0x00049C71
        public override bool onTouchUpXY(float tx, float ty)
        {
            this.startX = 0f;
            this.startY = 0f;
            return base.onTouchUpXY(tx, ty);
        }

        // Token: 0x0600086A RID: 2154 RVA: 0x0004BA94 File Offset: 0x00049C94
        public override bool onTouchMoveXY(float tx, float ty)
        {
            if (tx == -10000f && ty == -10000f)
            {
                return false;
            }
            if (this.state == Button.BUTTON_STATE.BUTTON_DOWN)
            {
                this.x = Math.Max(Math.Min(tx - this.startX, this.maxX), this.minX);
                this.y = Math.Max(Math.Min(ty - this.startY, this.maxY), this.minY);
                if (this.maxX != 0f)
                {
                    float num = (this.x - this.minX) / (this.maxX - this.minX);
                    if (num != this.xPercent)
                    {
                        this.xPercent = num;
                        if (this.liftDelegate != null)
                        {
                            this.liftDelegate(this.xPercent, this.yPercent);
                        }
                    }
                }
                if (this.maxY != 0f)
                {
                    float num2 = (this.y - this.minY) / (this.maxY - this.minY);
                    if (num2 != this.yPercent)
                    {
                        this.yPercent = num2;
                        if (this.liftDelegate != null)
                        {
                            this.liftDelegate(this.xPercent, this.yPercent);
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
            this.liftDelegate = null;
            base.dealloc();
        }

        // Token: 0x04000E01 RID: 3585
        public float startX;

        // Token: 0x04000E02 RID: 3586
        public float startY;

        // Token: 0x04000E03 RID: 3587
        public Lift.PercentXY liftDelegate;

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
