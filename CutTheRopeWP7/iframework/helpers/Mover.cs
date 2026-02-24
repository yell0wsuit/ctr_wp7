using System;
using System.Collections.Generic;

using ctr_wp7.iframework.core;
using ctr_wp7.ios;

namespace ctr_wp7.iframework.helpers
{
    // Token: 0x02000093 RID: 147
    internal class Mover : NSObject
    {
        // Token: 0x0600045B RID: 1115 RVA: 0x0001E8CC File Offset: 0x0001CACC
        public virtual Mover initWithPathCapacityMoveSpeedRotateSpeed(int l, float m_, float r_)
        {
            int num = (int)m_;
            int num2 = (int)r_;
            if (base.init() != null)
            {
                pathLen = 0;
                pathCapacity = l;
                rotateSpeed = (float)num2;
                if (pathCapacity > 0)
                {
                    path = new Vector[pathCapacity];
                    for (int i = 0; i < path.Length; i++)
                    {
                        path[i] = default(Vector);
                    }
                    moveSpeed = new float[pathCapacity];
                    for (int j = 0; j < moveSpeed.Length; j++)
                    {
                        moveSpeed[j] = (float)num;
                    }
                }
                paused = false;
            }
            return this;
        }

        // Token: 0x0600045C RID: 1116 RVA: 0x0001E978 File Offset: 0x0001CB78
        public virtual void setMoveSpeed(float ms)
        {
            for (int i = 0; i < pathCapacity; i++)
            {
                moveSpeed[i] = ms;
            }
        }

        // Token: 0x0600045D RID: 1117 RVA: 0x0001E9A0 File Offset: 0x0001CBA0
        public virtual void setPathFromStringandStart(NSString p, Vector s)
        {
            if (p.characterAtIndex(0) == 'R')
            {
                bool flag = p.characterAtIndex(1) == 'C';
                NSString nsstring = p.substringFromIndex(2);
                int num = nsstring.intValue();
                int num2 = num / 2;
                float num3 = (float)(6.283185307179586 / (double)num2);
                if (!flag)
                {
                    num3 = -num3;
                }
                float num4 = 0f;
                for (int i = 0; i < num2; i++)
                {
                    float num5 = s.x + (float)num * (float)Math.Cos((double)num4);
                    float num6 = s.y + (float)num * (float)Math.Sin((double)num4);
                    addPathPoint(MathHelper.vect(num5, num6));
                    num4 += num3;
                }
                return;
            }
            addPathPoint(s);
            if (p.characterAtIndex(p.length() - 1) == ',')
            {
                p = p.substringToIndex(p.length() - 1);
            }
            List<NSString> list = p.componentsSeparatedByString(',');
            for (int j = 0; j < list.Count; j += 2)
            {
                NSString nsstring2 = list[j];
                NSString nsstring3 = list[j + 1];
                addPathPoint(MathHelper.vect(s.x + nsstring2.floatValue(), s.y + nsstring3.floatValue()));
            }
        }

        // Token: 0x0600045E RID: 1118 RVA: 0x0001EAD8 File Offset: 0x0001CCD8
        public virtual void addPathPoint(Vector v)
        {
            path[pathLen++] = v;
        }

        // Token: 0x0600045F RID: 1119 RVA: 0x0001EB07 File Offset: 0x0001CD07
        public virtual void start()
        {
            if (pathLen > 0)
            {
                pos = path[0];
                targetPoint = 1;
                calculateOffset();
            }
        }

        // Token: 0x06000460 RID: 1120 RVA: 0x0001EB36 File Offset: 0x0001CD36
        public virtual void pause()
        {
            paused = true;
        }

        // Token: 0x06000461 RID: 1121 RVA: 0x0001EB3F File Offset: 0x0001CD3F
        public virtual void unpause()
        {
            paused = false;
        }

        // Token: 0x06000462 RID: 1122 RVA: 0x0001EB48 File Offset: 0x0001CD48
        public virtual void setRotateSpeed(float rs)
        {
            rotateSpeed = rs;
        }

        // Token: 0x06000463 RID: 1123 RVA: 0x0001EB51 File Offset: 0x0001CD51
        public virtual void jumpToPoint(int p)
        {
            targetPoint = p;
            pos = path[targetPoint];
            calculateOffset();
        }

        // Token: 0x06000464 RID: 1124 RVA: 0x0001EB7C File Offset: 0x0001CD7C
        public virtual void calculateOffset()
        {
            Vector vector = path[targetPoint];
            offset = MathHelper.vectMult(MathHelper.vectNormalize(MathHelper.vectSub(vector, pos)), moveSpeed[targetPoint]);
        }

        // Token: 0x06000465 RID: 1125 RVA: 0x0001EBC9 File Offset: 0x0001CDC9
        public virtual void setMoveSpeedforPoint(float ms, int i)
        {
            moveSpeed[i] = ms;
        }

        // Token: 0x06000466 RID: 1126 RVA: 0x0001EBD4 File Offset: 0x0001CDD4
        public virtual void setMoveReverse(bool r)
        {
            reverse = r;
        }

        // Token: 0x06000467 RID: 1127 RVA: 0x0001EBE0 File Offset: 0x0001CDE0
        public virtual void update(float delta)
        {
            if (paused)
            {
                return;
            }
            if (pathLen > 0)
            {
                Vector vector = path[targetPoint];
                bool flag = false;
                if (!MathHelper.vectEqual(pos, vector))
                {
                    float num = delta;
                    if (overrun != 0f)
                    {
                        num += overrun;
                        overrun = 0f;
                    }
                    pos = MathHelper.vectAdd(pos, MathHelper.vectMult(offset, num));
                    if (!MathHelper.sameSign(offset.x, vector.x - pos.x) || !MathHelper.sameSign(offset.y, vector.y - pos.y))
                    {
                        overrun = MathHelper.vectLength(MathHelper.vectSub(pos, vector));
                        float num2 = MathHelper.vectLength(offset);
                        overrun /= num2;
                        pos = vector;
                        flag = true;
                    }
                }
                else
                {
                    flag = true;
                }
                if (flag)
                {
                    if (reverse)
                    {
                        targetPoint--;
                        if (targetPoint < 0)
                        {
                            targetPoint = pathLen - 1;
                        }
                    }
                    else
                    {
                        targetPoint++;
                        if (targetPoint >= pathLen)
                        {
                            targetPoint = 0;
                        }
                    }
                    calculateOffset();
                }
            }
            if (rotateSpeed != 0f)
            {
                angle += (double)(rotateSpeed * delta);
            }
        }

        // Token: 0x06000468 RID: 1128 RVA: 0x0001ED72 File Offset: 0x0001CF72
        public override void dealloc()
        {
            path = null;
            moveSpeed = null;
            base.dealloc();
        }

        // Token: 0x06000469 RID: 1129 RVA: 0x0001ED88 File Offset: 0x0001CF88
        public static bool moveVariableToTarget(ref float v, float t, float speed, float delta)
        {
            if (t != v)
            {
                if (t > v)
                {
                    v += speed * delta;
                    if (v > t)
                    {
                        v = t;
                    }
                }
                else
                {
                    v -= speed * delta;
                    if (v < t)
                    {
                        v = t;
                    }
                }
                if (t == v)
                {
                    return true;
                }
            }
            return false;
        }

        // Token: 0x040009A1 RID: 2465
        private float[] moveSpeed;

        // Token: 0x040009A2 RID: 2466
        private float rotateSpeed;

        // Token: 0x040009A3 RID: 2467
        public Vector[] path;

        // Token: 0x040009A4 RID: 2468
        public int pathLen;

        // Token: 0x040009A5 RID: 2469
        private int pathCapacity;

        // Token: 0x040009A6 RID: 2470
        public Vector pos;

        // Token: 0x040009A7 RID: 2471
        public double angle;

        // Token: 0x040009A8 RID: 2472
        private bool paused;

        // Token: 0x040009A9 RID: 2473
        public int targetPoint;

        // Token: 0x040009AA RID: 2474
        private bool reverse;

        // Token: 0x040009AB RID: 2475
        private float overrun;

        // Token: 0x040009AC RID: 2476
        private Vector offset;
    }
}
