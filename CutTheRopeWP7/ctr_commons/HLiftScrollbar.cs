using ctr_wp7.iframework.core;
using ctr_wp7.iframework.visual;

namespace ctr_wp7.ctr_commons
{
    // Token: 0x02000117 RID: 279
    internal class HLiftScrollbar : Image
    {
        // Token: 0x06000871 RID: 2161 RVA: 0x0004BBD8 File Offset: 0x00049DD8
        public static HLiftScrollbar createWithResIDBackQuadLiftQuadLiftQuadPressed(int resID, int bq, int lq, int lqp)
        {
            return new HLiftScrollbar().initWithResIDBackQuadLiftQuadLiftQuadPressed(resID, bq, lq, lqp);
        }

        // Token: 0x06000872 RID: 2162 RVA: 0x0004BBE8 File Offset: 0x00049DE8
        public virtual HLiftScrollbar initWithResIDBackQuadLiftQuadLiftQuadPressed(int resID, int bq, int lq, int lqp)
        {
            if (base.initWithTexture(Application.getTexture(resID)) != null)
            {
                setDrawQuad(bq);
                Image image = Image_createWithResIDQuad(resID, lq);
                Image image2 = Image_createWithResIDQuad(resID, lqp);
                Vector relativeQuadOffset = getRelativeQuadOffset(resID, lq, lqp);
                image2.x += relativeQuadOffset.x;
                image2.y += relativeQuadOffset.y;
                lift = (Lift)new Lift().initWithUpElementDownElementandID(image, image2, 0);
                lift.parentAnchor = 17;
                lift.anchor = 18;
                lift.minX = 1f;
                lift.maxX = width - lift.minX;
                lift.liftDelegate = new Lift.PercentXY(percentXY);
                int num = 45;
                lift.setTouchIncreaseLeftRightTopBottom(num, num, -5f, 10f);
                _ = addChild(lift);
                spointsNum = 0;
                spoints = null;
                activeSpoint = 0;
            }
            return this;
        }

        // Token: 0x06000873 RID: 2163 RVA: 0x0004BD06 File Offset: 0x00049F06
        public virtual Vector getScrollPoint(int i)
        {
            return spoints[i];
        }

        // Token: 0x06000874 RID: 2164 RVA: 0x0004BD19 File Offset: 0x00049F19
        public virtual int getTotalScrollPoints()
        {
            return spointsNum;
        }

        // Token: 0x06000875 RID: 2165 RVA: 0x0004BD24 File Offset: 0x00049F24
        public virtual void updateActiveSpoint()
        {
            int i = 0;
            while (i < spointsNum)
            {
                if (lift.x <= spointsLimits[i].x)
                {
                    activeSpoint = limitPoints[i];
                    if (delegateLiftScrollbarDelegate != null)
                    {
                        delegateLiftScrollbarDelegate.changedActiveSpointFromTo(0, activeSpoint);
                        return;
                    }
                    break;
                }
                else
                {
                    i++;
                }
            }
        }

        // Token: 0x06000876 RID: 2166 RVA: 0x0004BD8C File Offset: 0x00049F8C
        public override void update(float delta)
        {
            base.update(delta);
            updateLift();
            for (int i = 0; i < spointsNum; i++)
            {
                if (lift.x <= spointsLimits[i].x)
                {
                    int num = limitPoints[i];
                    if (activeSpoint != num)
                    {
                        delegateLiftScrollbarDelegate?.changedActiveSpointFromTo(activeSpoint, num);
                        activeSpoint = num;
                    }
                    return;
                }
            }
            if (lift.x >= spointsLimits[spointsNum - 1].x && activeSpoint != limitPoints[spointsNum - 1])
            {
                delegateLiftScrollbarDelegate?.changedActiveSpointFromTo(activeSpoint, limitPoints[spointsNum - 1]);
                activeSpoint = limitPoints[spointsNum - 1];
            }
        }

        // Token: 0x06000877 RID: 2167 RVA: 0x0004BE82 File Offset: 0x0004A082
        public override void dealloc()
        {
            spoints = null;
            spointsLimits = null;
            limitPoints = null;
            container = null;
            delegateLiftScrollbarDelegate = null;
            base.dealloc();
        }

        // Token: 0x06000878 RID: 2168 RVA: 0x0004BEB0 File Offset: 0x0004A0B0
        public override bool onTouchDownXY(float tx, float ty)
        {
            return base.onTouchDownXY(tx, ty);
        }

        // Token: 0x06000879 RID: 2169 RVA: 0x0004BEC8 File Offset: 0x0004A0C8
        public override bool onTouchUpXY(float tx, float ty)
        {
            bool flag = base.onTouchUpXY(tx, ty);
            container.startMovingToSpointInDirection(vectZero);
            return flag;
        }

        // Token: 0x0600087A RID: 2170 RVA: 0x0004BEF0 File Offset: 0x0004A0F0
        public void percentXY(float px, float py)
        {
            Vector maxScroll = container.getMaxScroll();
            container.setScroll(vect(maxScroll.x * px, maxScroll.y * py));
        }

        // Token: 0x0600087B RID: 2171 RVA: 0x0004BF2C File Offset: 0x0004A12C
        public virtual void updateLift()
        {
            Vector scroll = container.getScroll();
            Vector maxScroll = container.getMaxScroll();
            float num = 0f;
            float num2 = 0f;
            if (maxScroll.x != 0f)
            {
                num = scroll.x / maxScroll.x;
            }
            if (maxScroll.y != 0f)
            {
                num2 = scroll.y / maxScroll.y;
            }
            lift.x = ((lift.maxX - lift.minX) * num) + lift.minX;
            lift.y = ((lift.maxY - lift.minY) * num2) + lift.minY;
        }

        // Token: 0x0600087C RID: 2172 RVA: 0x0004BFFC File Offset: 0x0004A1FC
        public virtual void calcScrollPoints()
        {
            Vector maxScroll = container.getMaxScroll();
            spointsNum = container.getTotalScrollPoints();
            spoints = null;
            spointsLimits = null;
            limitPoints = null;
            spoints = new Vector[spointsNum];
            spointsLimits = new Vector[spointsNum];
            limitPoints = new int[spointsNum];
            for (int i = 0; i < spointsNum; i++)
            {
                Vector vector = vectNeg(container.getScrollPoint(i));
                float num = 0f;
                float num2 = 0f;
                if (maxScroll.x != 0f)
                {
                    num = vector.x / maxScroll.x;
                }
                if (maxScroll.y != 0f)
                {
                    num2 = vector.y / maxScroll.y;
                }
                float num3 = ((lift.maxX - lift.minX) * num) + lift.minX;
                float num4 = ((lift.maxY - lift.minY) * num2) + lift.minY;
                spoints[i] = vect(num3, num4);
            }
            for (int j = 0; j < spointsNum; j++)
            {
                spointsLimits[j] = spoints[j];
                limitPoints[j] = j;
            }
            bool flag = true;
            while (flag)
            {
                flag = false;
                for (int k = 0; k < spointsNum - 1; k++)
                {
                    if (spointsLimits[k].x > spointsLimits[k + 1].x)
                    {
                        flag = true;
                        Vector vector2 = spointsLimits[k];
                        spointsLimits[k] = spointsLimits[k + 1];
                        spointsLimits[k + 1] = vector2;
                        int num5 = limitPoints[k];
                        limitPoints[k] = limitPoints[k + 1];
                        limitPoints[k + 1] = num5;
                    }
                }
            }
            for (int l = 0; l < spointsNum - 1; l++)
            {
                Vector vector3 = spointsLimits[l];
                Vector vector4 = spointsLimits[l + 1];
                Vector[] array = spointsLimits;
                int num6 = l;
                array[num6].x = array[num6].x + ((vector4.x - vector3.x) / 2f);
            }
        }

        // Token: 0x0600087D RID: 2173 RVA: 0x0004C2D3 File Offset: 0x0004A4D3
        public virtual void setContainer(ScrollableContainer c)
        {
            container = c;
            if (container != null)
            {
                calcScrollPoints();
                updateLift();
            }
        }

        // Token: 0x04000E0A RID: 3594
        public Vector[] spoints;

        // Token: 0x04000E0B RID: 3595
        public Vector[] spointsLimits;

        // Token: 0x04000E0C RID: 3596
        public int[] limitPoints;

        // Token: 0x04000E0D RID: 3597
        public int spointsNum;

        // Token: 0x04000E0E RID: 3598
        public int activeSpoint;

        // Token: 0x04000E0F RID: 3599
        public Lift lift;

        // Token: 0x04000E10 RID: 3600
        public ScrollableContainer container;

        // Token: 0x04000E11 RID: 3601
        public LiftScrollbarDelegate delegateLiftScrollbarDelegate;
    }
}
