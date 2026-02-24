using System;
using System.Collections.Generic;

using ctr_wp7.iframework.core;
using ctr_wp7.ios;

namespace ctr_wp7.iframework.helpers
{
    internal class Mover : NSObject
    {
        public virtual Mover initWithPathCapacityMoveSpeedRotateSpeed(int l, float m_, float r_)
        {
            int num = (int)m_;
            int num2 = (int)r_;
            if (base.init() != null)
            {
                pathLen = 0;
                pathCapacity = l;
                rotateSpeed = num2;
                if (pathCapacity > 0)
                {
                    path = new Vector[pathCapacity];
                    for (int i = 0; i < path.Length; i++)
                    {
                        path[i] = default;
                    }
                    moveSpeed = new float[pathCapacity];
                    for (int j = 0; j < moveSpeed.Length; j++)
                    {
                        moveSpeed[j] = num;
                    }
                }
                paused = false;
            }
            return this;
        }

        public virtual void setMoveSpeed(float ms)
        {
            for (int i = 0; i < pathCapacity; i++)
            {
                moveSpeed[i] = ms;
            }
        }

        public virtual void setPathFromStringandStart(NSString p, Vector s)
        {
            if (p.characterAtIndex(0) == 'R')
            {
                bool flag = p.characterAtIndex(1) == 'C';
                NSString nsstring = p.substringFromIndex(2);
                int num = nsstring.intValue();
                int num2 = num / 2;
                float num3 = (float)(6.283185307179586 / num2);
                if (!flag)
                {
                    num3 = -num3;
                }
                float num4 = 0f;
                for (int i = 0; i < num2; i++)
                {
                    float num5 = s.x + (num * (float)Math.Cos((double)num4));
                    float num6 = s.y + (num * (float)Math.Sin((double)num4));
                    addPathPoint(vect(num5, num6));
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
                addPathPoint(vect(s.x + nsstring2.floatValue(), s.y + nsstring3.floatValue()));
            }
        }

        public virtual void addPathPoint(Vector v)
        {
            path[pathLen++] = v;
        }

        public virtual void start()
        {
            if (pathLen > 0)
            {
                pos = path[0];
                targetPoint = 1;
                calculateOffset();
            }
        }

        public virtual void pause()
        {
            paused = true;
        }

        public virtual void unpause()
        {
            paused = false;
        }

        public virtual void setRotateSpeed(float rs)
        {
            rotateSpeed = rs;
        }

        public virtual void jumpToPoint(int p)
        {
            targetPoint = p;
            pos = path[targetPoint];
            calculateOffset();
        }

        public virtual void calculateOffset()
        {
            Vector vector = path[targetPoint];
            offset = vectMult(vectNormalize(vectSub(vector, pos)), moveSpeed[targetPoint]);
        }

        public virtual void setMoveSpeedforPoint(float ms, int i)
        {
            moveSpeed[i] = ms;
        }

        public virtual void setMoveReverse(bool r)
        {
            reverse = r;
        }

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
                if (!vectEqual(pos, vector))
                {
                    float num = delta;
                    if (overrun != 0f)
                    {
                        num += overrun;
                        overrun = 0f;
                    }
                    pos = vectAdd(pos, vectMult(offset, num));
                    if (!sameSign(offset.x, vector.x - pos.x) || !sameSign(offset.y, vector.y - pos.y))
                    {
                        overrun = vectLength(vectSub(pos, vector));
                        float num2 = vectLength(offset);
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
                angle += rotateSpeed * delta;
            }
        }

        public override void dealloc()
        {
            path = null;
            moveSpeed = null;
            base.dealloc();
        }

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

        private float[] moveSpeed;

        private float rotateSpeed;

        public Vector[] path;

        public int pathLen;

        private int pathCapacity;

        public Vector pos;

        public double angle;

        private bool paused;

        public int targetPoint;

        private bool reverse;

        private float overrun;

        private Vector offset;
    }
}
