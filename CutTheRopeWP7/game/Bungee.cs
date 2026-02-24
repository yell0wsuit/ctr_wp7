using ctr_wp7.iframework;
using ctr_wp7.iframework.core;
using ctr_wp7.iframework.helpers;
using ctr_wp7.iframework.sfe;
using ctr_wp7.iframework.visual;
using ctr_wp7.ios;

namespace ctr_wp7.game
{
    // Token: 0x020000E0 RID: 224
    internal class Bungee : ConstraintSystem
    {
        // Token: 0x06000682 RID: 1666 RVA: 0x00031DF0 File Offset: 0x0002FFF0
        private void drawAntialiasedLineContinued(float x1, float y1, float x2, float y2, float size, RGBAColor color, ref float lx, ref float ly, ref float rx, ref float ry)
        {
            Vector vector = vect(x1, y1);
            Vector vector2 = vect(x2, y2);
            Vector vector3 = vectSub(vector2, vector);
            Vector vector4 = vectMult(vector3, ((double)color.a == 1.0) ? 1.02f : 1f);
            Vector vector5 = vectPerp(vector3);
            Vector vector6 = vectNormalize(vector5);
            vector5 = vectMult(vector6, size);
            Vector vector7 = vectNeg(vector5);
            Vector vector8 = vectAdd(vector5, vector3);
            Vector vector9 = vectAdd(vector7, vector3);
            vectAdd2(ref vector8, vector);
            vectAdd2(ref vector9, vector);
            Vector vector10 = vectAdd(vector5, vector4);
            Vector vector11 = vectAdd(vector7, vector4);
            if (lx == -1f)
            {
                vectAdd2(ref vector5, vector);
                vectAdd2(ref vector7, vector);
            }
            else
            {
                vector5 = vect(lx, ly);
                vector7 = vect(rx, ry);
            }
            vectAdd2(ref vector10, vector);
            vectAdd2(ref vector11, vector);
            lx = vector8.x;
            ly = vector8.y;
            rx = vector9.x;
            ry = vector9.y;
            Vector vector12 = vectSub(vector5, vector6);
            Vector vector13 = vectSub(vector10, vector6);
            Vector vector14 = vectAdd(vector7, vector6);
            Vector vector15 = vectAdd(vector11, vector6);
            cverts[0] = vector5.x;
            cverts[1] = vector5.y;
            cverts[2] = vector10.x;
            cverts[3] = vector10.y;
            cverts[4] = vector12.x;
            cverts[5] = vector12.y;
            cverts[6] = vector13.x;
            cverts[7] = vector13.y;
            cverts[8] = vector14.x;
            cverts[9] = vector14.y;
            cverts[10] = vector15.x;
            cverts[11] = vector15.y;
            cverts[12] = vector7.x;
            cverts[13] = vector7.y;
            cverts[14] = vector11.x;
            cverts[15] = vector11.y;
            ccolors[2] = ccolors[3] = ccolors[4] = ccolors[5] = color;
            OpenGL.glColorPointer_add(4, 5, 0, ccolors);
            OpenGL.glVertexPointer_add(2, 5, 0, cverts);
        }

        // Token: 0x06000683 RID: 1667 RVA: 0x00032098 File Offset: 0x00030298
        private void drawBungee(Bungee b, Vector[] pts, int count, int points)
        {
            float num = (b.cut == -1 || b.forceWhite) ? 1f : (b.cutTime / 1.95f);
            if (num == 0f)
            {
                return;
            }
            RGBAColor rgbacolor = RGBAColor.MakeRGBA(0f, 0f, 0f, num);
            RGBAColor rgbacolor2 = RGBAColor.MakeRGBA(0.6755555555555556, 0.44, 0.27555555555555555, (double)num);
            RGBAColor rgbacolor3 = RGBAColor.MakeRGBA(0.304, 0.198, 0.124, (double)num);
            RGBAColor rgbacolor4 = RGBAColor.MakeRGBA(0.475, 0.305, 0.185, (double)num);
            RGBAColor rgbacolor5 = RGBAColor.MakeRGBA(0.19, 0.122, 0.074, (double)num);
            RGBAColor rgbacolor6 = b.alternateColors ? rgbacolor : rgbacolor2;
            RGBAColor rgbacolor7 = b.alternateColors ? rgbacolor : rgbacolor3;
            float num2 = vectDistance(vect(pts[0].x, pts[0].y), vect(pts[1].x, pts[1].y));
            if ((double)num2 <= 30.3)
            {
                b.relaxed = 0;
            }
            else if ((double)num2 <= 31.0)
            {
                b.relaxed = 1;
            }
            else if ((double)num2 <= 34.0)
            {
                b.relaxed = 2;
            }
            else
            {
                b.relaxed = 3;
            }
            if ((double)num2 > 37.0 && !b.dontDrawRedStretch)
            {
                float num3 = num2 / 30f * 2f;
                rgbacolor5.r *= num3;
                rgbacolor7.r *= num3;
            }
            bool flag = false;
            int num4 = (count - 1) * points;
            float[] array = new float[num4 * 2];
            b.drawPtsCount = num4 * 2;
            float num5 = 1f / (float)num4;
            float num6 = 0f;
            int num7 = 0;
            int num8 = 0;
            int num9 = 0;
            RGBAColor rgbacolor8 = rgbacolor5;
            RGBAColor rgbacolor9 = rgbacolor7;
            float num10 = (rgbacolor4.r - rgbacolor5.r) / (float)(num4 - 1);
            float num11 = (rgbacolor4.g - rgbacolor5.g) / (float)(num4 - 1);
            float num12 = (rgbacolor4.b - rgbacolor5.b) / (float)(num4 - 1);
            float num13 = (rgbacolor6.r - rgbacolor7.r) / (float)(num4 - 1);
            float num14 = (rgbacolor6.g - rgbacolor7.g) / (float)(num4 - 1);
            float num15 = (rgbacolor6.b - rgbacolor7.b) / (float)(num4 - 1);
            float num16 = -1f;
            float num17 = -1f;
            float num18 = -1f;
            float num19 = -1f;
            OpenGL.glDisableClientState(0);
            OpenGL.glEnableClientState(13);
            for (; ; )
            {
                if ((double)num6 > 0.99)
                {
                    num6 = 1f;
                }
                if (count < 3)
                {
                    break;
                }
                Vector vector = GLDrawer.calcPathBezier(pts, count, num6);
                array[num7++] = vector.x;
                array[num7++] = vector.y;
                b.drawPts[num8++] = vector.x;
                b.drawPts[num8++] = vector.y;
                if (num7 >= 6 || (double)num6 >= 1.0)
                {
                    RGBAColor rgbacolor10;
                    if (b.forceWhite)
                    {
                        rgbacolor10 = RGBAColor.whiteRGBA;
                    }
                    else if (flag)
                    {
                        rgbacolor10 = rgbacolor8;
                    }
                    else
                    {
                        rgbacolor10 = rgbacolor9;
                    }
                    OpenGL.glColor4f(rgbacolor10.r, rgbacolor10.g, rgbacolor10.b, rgbacolor10.a);
                    int num20 = num7 >> 1;
                    OpenGL.glVertexPointer_setAdditive(2, 5, 0, 16 * (num20 - 1));
                    OpenGL.glColorPointer_setAdditive(8 * (num20 - 1));
                    for (int i = 0; i < num20 - 1; i++)
                    {
                        drawAntialiasedLineContinued(array[i * 2], array[i * 2 + 1], array[i * 2 + 2], array[i * 2 + 3], (float)b.width, rgbacolor10, ref num16, ref num17, ref num18, ref num19);
                    }
                    OpenGL.glDrawArrays(8, 0, 8);
                    array[0] = array[num7 - 2];
                    array[1] = array[num7 - 1];
                    num7 = 2;
                    flag = !flag;
                    num9++;
                    rgbacolor8.r += num10 * (float)(num20 - 1);
                    rgbacolor8.g += num11 * (float)(num20 - 1);
                    rgbacolor8.b += num12 * (float)(num20 - 1);
                    rgbacolor9.r += num13 * (float)(num20 - 1);
                    rgbacolor9.g += num14 * (float)(num20 - 1);
                    rgbacolor9.b += num15 * (float)(num20 - 1);
                }
                if ((double)num6 >= 1.0)
                {
                    break;
                }
                num6 += num5;
            }
            OpenGL.glEnableClientState(0);
            OpenGL.glDisableClientState(13);
        }

        // Token: 0x06000684 RID: 1668 RVA: 0x0003256C File Offset: 0x0003076C
        public virtual NSObject initWithHeadAtXYTailAtTXTYandLength(ConstraintedPoint h, float hx, float hy, ConstraintedPoint t, float tx, float ty, float len)
        {
            if (base.init() != null)
            {
                relaxationTimes = 30;
                lineWidth = 3f;
                width = 2;
                cut = -1;
                bungeeMode = 0;
                if (h != null)
                {
                    bungeeAnchor = h;
                }
                else
                {
                    bungeeAnchor = (ConstraintedPoint)new ConstraintedPoint().init();
                }
                if (t != null)
                {
                    tail = t;
                }
                else
                {
                    tail = (ConstraintedPoint)new ConstraintedPoint().init();
                }
                bungeeAnchor.setWeight(0.02f);
                bungeeAnchor.pos = vect(hx, hy);
                tail.pos = vect(tx, ty);
                tail.setWeight(1f);
                addPart(bungeeAnchor);
                addPart(tail);
                tail.addConstraintwithRestLengthofType(bungeeAnchor, 30f, Constraint.CONSTRAINT.CONSTRAINT_DISTANCE);
                Vector vector = vectSub(tail.pos, bungeeAnchor.pos);
                int num = (int)(len / 30f + 2f);
                vector = vectDiv(vector, (float)num);
                rollplacingWithOffset(len, vector);
                forceWhite = false;
                initialCandleAngle = -1f;
                chosenOne = false;
                hideTailParts = false;
                dontDrawRedStretch = false;
                alternateColors = false;
            }
            return this;
        }

        // Token: 0x06000685 RID: 1669 RVA: 0x000326D4 File Offset: 0x000308D4
        public virtual int getLength()
        {
            if (this == null)
            {
                return 0;
            }
            int num = 0;
            Vector vector = vectZero;
            int count = parts.Count;
            for (int i = 0; i < count; i++)
            {
                ConstraintedPoint constraintedPoint = parts[i];
                if (i > 0)
                {
                    num += (int)vectDistance(vector, constraintedPoint.pos);
                }
                vector = constraintedPoint.pos;
            }
            return num;
        }

        // Token: 0x06000686 RID: 1670 RVA: 0x00032734 File Offset: 0x00030934
        public virtual float rollBack(float amount)
        {
            float num = amount;
            ConstraintedPoint constraintedPoint = parts[parts.Count - 2];
            int num2 = (int)tail.restLengthFor(constraintedPoint);
            int num3 = parts.Count;
            while (num > 0f)
            {
                if (num >= 30f)
                {
                    ConstraintedPoint constraintedPoint2 = parts[num3 - 2];
                    ConstraintedPoint constraintedPoint3 = parts[num3 - 3];
                    tail.changeConstraintFromTowithRestLength(constraintedPoint2, constraintedPoint3, (float)num2);
                    parts.RemoveAt(parts.Count - 2);
                    num3--;
                    num -= 30f;
                }
                else
                {
                    int num4 = (int)((float)num2 - num);
                    if (num4 < 1)
                    {
                        num = 30f;
                        num2 = (int)(30f + (float)num4 + 1f);
                    }
                    else
                    {
                        ConstraintedPoint constraintedPoint4 = parts[num3 - 2];
                        tail.changeRestLengthToFor((float)num4, constraintedPoint4);
                        num = 0f;
                    }
                }
            }
            int count = tail.constraints.Count;
            for (int i = 0; i < count; i++)
            {
                Constraint constraint = tail.constraints[i];
                if (constraint != null && constraint.type == Constraint.CONSTRAINT.CONSTRAINT_NOT_MORE_THAN)
                {
                    constraint.restLength = (float)(num3 - 1) * 33f;
                }
            }
            return num;
        }

        // Token: 0x06000687 RID: 1671 RVA: 0x00032887 File Offset: 0x00030A87
        public virtual void roll(float rollLen)
        {
            rollplacingWithOffset(rollLen, vectZero);
        }

        // Token: 0x06000688 RID: 1672 RVA: 0x00032898 File Offset: 0x00030A98
        public virtual void rollplacingWithOffset(float rollLen, Vector off)
        {
            ConstraintedPoint constraintedPoint = parts[parts.Count - 2];
            int num = (int)tail.restLengthFor(constraintedPoint);
            while (rollLen > 0f)
            {
                if (rollLen >= 30f)
                {
                    ConstraintedPoint constraintedPoint2 = parts[parts.Count - 2];
                    ConstraintedPoint constraintedPoint3 = (ConstraintedPoint)new ConstraintedPoint().init();
                    constraintedPoint3.setWeight(0.02f);
                    constraintedPoint3.pos = vectAdd(constraintedPoint2.pos, off);
                    addPartAt(constraintedPoint3, parts.Count - 1);
                    tail.changeConstraintFromTowithRestLength(constraintedPoint2, constraintedPoint3, (float)num);
                    constraintedPoint3.addConstraintwithRestLengthofType(constraintedPoint2, 30f, Constraint.CONSTRAINT.CONSTRAINT_DISTANCE);
                    rollLen -= 30f;
                }
                else
                {
                    int num2 = (int)(rollLen + (float)num);
                    if ((float)num2 > 30f)
                    {
                        rollLen = 30f;
                        num = (int)((float)num2 - 30f);
                    }
                    else
                    {
                        ConstraintedPoint constraintedPoint4 = parts[parts.Count - 2];
                        tail.changeRestLengthToFor((float)num2, constraintedPoint4);
                        rollLen = 0f;
                    }
                }
            }
        }

        // Token: 0x06000689 RID: 1673 RVA: 0x000329C0 File Offset: 0x00030BC0
        public virtual void removePart(int part)
        {
            forceWhite = false;
            ConstraintedPoint constraintedPoint = parts[part];
            ConstraintedPoint constraintedPoint2;
            if (part + 1 < parts.Count)
            {
                constraintedPoint2 = parts[part + 1];
            }
            else
            {
                constraintedPoint2 = null;
            }
            if (constraintedPoint2 == null)
            {
                constraintedPoint.removeConstraints();
            }
            else
            {
                for (int i = 0; i < constraintedPoint2.constraints.Count; i++)
                {
                    Constraint constraint = constraintedPoint2.constraints[i];
                    if (constraint.cp == constraintedPoint)
                    {
                        _ = constraintedPoint2.constraints.Remove(constraint);
                        ConstraintedPoint constraintedPoint3 = (ConstraintedPoint)new ConstraintedPoint().init();
                        constraintedPoint3.setWeight(1E-05f);
                        constraintedPoint3.pos = constraintedPoint2.pos;
                        constraintedPoint3.prevPos = constraintedPoint2.prevPos;
                        addPartAt(constraintedPoint3, part + 1);
                        constraintedPoint3.addConstraintwithRestLengthofType(constraintedPoint, 30f, Constraint.CONSTRAINT.CONSTRAINT_DISTANCE);
                        break;
                    }
                }
            }
            for (int j = 0; j < parts.Count; j++)
            {
                ConstraintedPoint constraintedPoint4 = parts[j];
                if (constraintedPoint4 != tail)
                {
                    constraintedPoint4.setWeight(1E-05f);
                }
            }
        }

        // Token: 0x0600068A RID: 1674 RVA: 0x00032ADF File Offset: 0x00030CDF
        public virtual void setCut(int part)
        {
            cut = part;
            cutTime = 2f;
            forceWhite = true;
        }

        // Token: 0x0600068B RID: 1675 RVA: 0x00032AFC File Offset: 0x00030CFC
        public virtual void strengthen()
        {
            int count = parts.Count;
            for (int i = 0; i < count; i++)
            {
                ConstraintedPoint constraintedPoint = parts[i];
                if (constraintedPoint != null)
                {
                    if (bungeeAnchor.pin.x != -1f)
                    {
                        if (constraintedPoint != tail)
                        {
                            constraintedPoint.setWeight(0.5f);
                        }
                        if (i != 0)
                        {
                            constraintedPoint.addConstraintwithRestLengthofType(bungeeAnchor, (float)i * 33f, Constraint.CONSTRAINT.CONSTRAINT_NOT_MORE_THAN);
                        }
                    }
                    i++;
                }
            }
        }

        // Token: 0x0600068C RID: 1676 RVA: 0x00032B78 File Offset: 0x00030D78
        public override void update(float delta)
        {
            update(delta, 1f);
        }

        // Token: 0x0600068D RID: 1677 RVA: 0x00032B88 File Offset: 0x00030D88
        public virtual void update(float delta, float koeff)
        {
            if ((double)cutTime > 0.0)
            {
                _ = Mover.moveVariableToTarget(ref cutTime, 0f, 1f, delta);
                if (cutTime < 1.95f && forceWhite)
                {
                    removePart(cut);
                }
            }
            int count = parts.Count;
            for (int i = 0; i < count; i++)
            {
                ConstraintedPoint constraintedPoint = parts[i];
                if (constraintedPoint != tail)
                {
                    ConstraintedPoint.qcpupdate(constraintedPoint, delta, koeff);
                }
            }
            for (int j = 0; j < relaxationTimes; j++)
            {
                int count2 = parts.Count;
                for (int k = 0; k < count2; k++)
                {
                    ConstraintedPoint constraintedPoint2 = parts[k];
                    ConstraintedPoint.satisfyConstraints(constraintedPoint2);
                }
            }
        }

        // Token: 0x0600068E RID: 1678 RVA: 0x00032C5C File Offset: 0x00030E5C
        public override void draw()
        {
            int count = parts.Count;
            OpenGL.SetRopeColor();
            if (cut == -1)
            {
                Vector[] array = new Vector[count];
                for (int i = 0; i < count; i++)
                {
                    ConstraintedPoint constraintedPoint = parts[i];
                    array[i] = constraintedPoint.pos;
                }
                drawBungee(this, array, count, 3);
                return;
            }
            Vector[] array2 = new Vector[count];
            Vector[] array3 = new Vector[count];
            bool flag = false;
            int num = 0;
            for (int j = 0; j < count; j++)
            {
                ConstraintedPoint constraintedPoint2 = parts[j];
                bool flag2 = true;
                if (j > 0)
                {
                    ConstraintedPoint constraintedPoint3 = parts[j - 1];
                    if (!constraintedPoint2.hasConstraintTo(constraintedPoint3))
                    {
                        flag2 = false;
                    }
                }
                if (constraintedPoint2.pin.x == -1f && !flag2)
                {
                    flag = true;
                    array2[j] = constraintedPoint2.pos;
                }
                if (!flag)
                {
                    array2[j] = constraintedPoint2.pos;
                }
                else
                {
                    array3[num] = constraintedPoint2.pos;
                    num++;
                }
            }
            int num2 = count - num;
            if (num2 > 0)
            {
                drawBungee(this, array2, num2, 3);
            }
            if (num > 0 && !hideTailParts)
            {
                drawBungee(this, array3, num, 3);
            }
        }

        // Token: 0x0600068F RID: 1679 RVA: 0x00032DB7 File Offset: 0x00030FB7
        public override void dealloc()
        {
            base.dealloc();
        }

        // Token: 0x04000BF2 RID: 3058
        public const float BUNGEE_REST_LEN = 30f;

        // Token: 0x04000BF3 RID: 3059
        private const float ROLLBACK_K = 0.5f;

        // Token: 0x04000BF4 RID: 3060
        private const int BUNGEE_BEZIER_POINTS = 3;

        // Token: 0x04000BF5 RID: 3061
        public const int BUNGEE_RELAXION_TIMES = 30;

        // Token: 0x04000BF6 RID: 3062
        private const float MAX_BUNGEE_SEGMENTS = 20f;

        // Token: 0x04000BF7 RID: 3063
        private const float DEFAULT_PART_WEIGHT = 0.02f;

        // Token: 0x04000BF8 RID: 3064
        private const float STRENGTHENED_PART_WEIGHT = 0.5f;

        // Token: 0x04000BF9 RID: 3065
        private const float CUT_DISSAPPEAR_TIMEOUT = 2f;

        // Token: 0x04000BFA RID: 3066
        private const float WHITE_TIMEOUT = 0.05f;

        // Token: 0x04000BFB RID: 3067
        public ConstraintedPoint bungeeAnchor;

        // Token: 0x04000BFC RID: 3068
        public ConstraintedPoint tail;

        // Token: 0x04000BFD RID: 3069
        public int cut;

        // Token: 0x04000BFE RID: 3070
        public int relaxed;

        // Token: 0x04000BFF RID: 3071
        public float initialCandleAngle;

        // Token: 0x04000C00 RID: 3072
        public bool chosenOne;

        // Token: 0x04000C01 RID: 3073
        public int bungeeMode;

        // Token: 0x04000C02 RID: 3074
        public float partWeight;

        // Token: 0x04000C03 RID: 3075
        public bool forceWhite;

        // Token: 0x04000C04 RID: 3076
        public float cutTime;

        // Token: 0x04000C05 RID: 3077
        public float[] drawPts = new float[200];

        // Token: 0x04000C06 RID: 3078
        public int drawPtsCount;

        // Token: 0x04000C07 RID: 3079
        public float lineWidth;

        // Token: 0x04000C08 RID: 3080
        public int width;

        // Token: 0x04000C09 RID: 3081
        public bool hideTailParts;

        // Token: 0x04000C0A RID: 3082
        public bool dontDrawRedStretch;

        // Token: 0x04000C0B RID: 3083
        public bool alternateColors;

        // Token: 0x04000C0C RID: 3084
        private static RGBAColor[] ccolors =
        [
            RGBAColor.transparentRGBA,
            RGBAColor.transparentRGBA,
            RGBAColor.transparentRGBA,
            RGBAColor.transparentRGBA,
            RGBAColor.transparentRGBA,
            RGBAColor.transparentRGBA,
            RGBAColor.transparentRGBA,
            RGBAColor.transparentRGBA
        ];

        // Token: 0x04000C0D RID: 3085
        private float[] cverts = new float[16];

        // Token: 0x020000E1 RID: 225
        private enum BUNGEE_MODE
        {
            // Token: 0x04000C0F RID: 3087
            BUNGEE_MODE_NORMAL,
            // Token: 0x04000C10 RID: 3088
            BUNGEE_MODE_LOCKED
        }
    }
}
