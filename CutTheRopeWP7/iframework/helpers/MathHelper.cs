using System;

using ctr_wp7.ctr_original;
using ctr_wp7.iframework.core;
using ctr_wp7.ios;

namespace ctr_wp7.iframework.helpers
{
    // Token: 0x02000004 RID: 4
    internal class MathHelper : ResDataPhoneFull
    {
        // Token: 0x0600000B RID: 11 RVA: 0x000046F8 File Offset: 0x000028F8
        public static int MIN(int a, int b)
        {
            return Math.Min(a, b);
        }

        // Token: 0x0600000C RID: 12 RVA: 0x00004701 File Offset: 0x00002901
        public static float MIN(float a, float b)
        {
            return Math.Min(a, b);
        }

        // Token: 0x0600000D RID: 13 RVA: 0x0000470A File Offset: 0x0000290A
        public static float MIN(double a, double b)
        {
            return (float)Math.Min(a, b);
        }

        // Token: 0x0600000E RID: 14 RVA: 0x00004714 File Offset: 0x00002914
        public static int MAX(int a, int b)
        {
            return Math.Max(a, b);
        }

        // Token: 0x0600000F RID: 15 RVA: 0x0000471D File Offset: 0x0000291D
        public static float MAX(float a, float b)
        {
            return Math.Max(a, b);
        }

        // Token: 0x06000010 RID: 16 RVA: 0x00004726 File Offset: 0x00002926
        public static float MAX(double a, double b)
        {
            return (float)Math.Max(a, b);
        }

        // Token: 0x06000011 RID: 17 RVA: 0x00004730 File Offset: 0x00002930
        public static int RND(int n)
        {
            return RND_RANGE(0, n);
        }

        // Token: 0x06000012 RID: 18 RVA: 0x00004739 File Offset: 0x00002939
        public static int RND_RANGE(int n, int m)
        {
            return random_.Next(n, m + 1);
        }

        // Token: 0x06000013 RID: 19 RVA: 0x00004749 File Offset: 0x00002949
        public static uint arc4random()
        {
            return (uint)random_.Next(int.MinValue, int.MaxValue);
        }

        // Token: 0x17000001 RID: 1
        // (get) Token: 0x06000014 RID: 20 RVA: 0x0000475F File Offset: 0x0000295F
        public static float RND_MINUS1_1 => (float)((arc4random() / (double)ARC4RANDOM_MAX * 2.0) - 1.0);

        // Token: 0x17000002 RID: 2
        // (get) Token: 0x06000015 RID: 21 RVA: 0x00004784 File Offset: 0x00002984
        public static float RND_0_1 => (float)(arc4random() / (double)ARC4RANDOM_MAX);

        // Token: 0x06000016 RID: 22 RVA: 0x00004795 File Offset: 0x00002995
        public static float FIT_TO_BOUNDARIES(double V, double MINV, double MAXV)
        {
            return FIT_TO_BOUNDARIES((float)V, (float)MINV, (float)MAXV);
        }

        // Token: 0x06000017 RID: 23 RVA: 0x000047A2 File Offset: 0x000029A2
        public static float FIT_TO_BOUNDARIES(float V, float MINV, float MAXV)
        {
            return Math.Max(Math.Min(V, MAXV), MINV);
        }

        // Token: 0x06000018 RID: 24 RVA: 0x000047B1 File Offset: 0x000029B1
        public static float ceil(double value)
        {
            return (float)Math.Ceiling(value);
        }

        // Token: 0x06000019 RID: 25 RVA: 0x000047BA File Offset: 0x000029BA
        public static float round(double value)
        {
            return (float)Math.Round(value);
        }

        // Token: 0x0600001A RID: 26 RVA: 0x000047C3 File Offset: 0x000029C3
        public static float cosf(float x)
        {
            return (float)Math.Cos((double)x);
        }

        // Token: 0x0600001B RID: 27 RVA: 0x000047CD File Offset: 0x000029CD
        public static float sinf(float x)
        {
            return (float)Math.Sin((double)x);
        }

        // Token: 0x0600001C RID: 28 RVA: 0x000047D7 File Offset: 0x000029D7
        public static float tanf(float x)
        {
            return (float)Math.Tan((double)x);
        }

        // Token: 0x0600001D RID: 29 RVA: 0x000047E1 File Offset: 0x000029E1
        public static float acosf(float x)
        {
            return (float)Math.Acos((double)x);
        }

        // Token: 0x0600001E RID: 30 RVA: 0x000047EC File Offset: 0x000029EC
        public static void fmInit()
        {
            if (fmSins == null)
            {
                fmSins = new float[1024];
                for (int i = 0; i < 1024; i++)
                {
                    fmSins[i] = (float)Math.Sin(i * 2 * 3.141592653589793 / 1024.0);
                }
            }
            if (fmCoss == null)
            {
                fmCoss = new float[1024];
                for (int j = 0; j < 1024; j++)
                {
                    fmCoss[j] = (float)Math.Cos(j * 2 * 3.141592653589793 / 1024.0);
                }
            }
        }

        // Token: 0x0600001F RID: 31 RVA: 0x00004890 File Offset: 0x00002A90
        public static float fmSin(float angle)
        {
            int num = (int)((double)(angle * 1024f) / 3.141592653589793 / 2.0);
            num &= 1023;
            return fmSins[num];
        }

        // Token: 0x06000020 RID: 32 RVA: 0x000048CC File Offset: 0x00002ACC
        public static float fmCos(float angle)
        {
            int num = (int)((double)(angle * 1024f) / 3.141592653589793 / 2.0);
            num &= 1023;
            return fmCoss[num];
        }

        // Token: 0x06000021 RID: 33 RVA: 0x00004906 File Offset: 0x00002B06
        public static bool sameSign(float a, float b)
        {
            return (a >= 0f && b >= 0f) || (a < 0f && b < 0f);
        }

        // Token: 0x06000022 RID: 34 RVA: 0x0000492C File Offset: 0x00002B2C
        public static bool pointInRect(float x, float y, float checkX, float checkY, float checkWidth, float checkHeight)
        {
            return x >= checkX && x < checkX + checkWidth && y >= checkY && y < checkY + checkHeight;
        }

        // Token: 0x06000023 RID: 35 RVA: 0x00004946 File Offset: 0x00002B46
        public static bool rectInRect(float x1l, float y1t, float x1r, float y1b, float x2l, float y2t, float x2r, float y2b)
        {
            return x1l <= x2r && x1r >= x2l && y1t <= y2b && y1b >= y2t;
        }

        // Token: 0x06000024 RID: 36 RVA: 0x00004964 File Offset: 0x00002B64
        public static bool obbInOBB(Vector tl1, Vector tr1, Vector br1, Vector bl1, Vector tl2, Vector tr2, Vector br2, Vector bl2)
        {
            Vector[] array = new Vector[4];
            Vector[] array2 = new Vector[4];
            array[0] = tl1;
            array[1] = tr1;
            array[2] = br1;
            array[3] = bl1;
            array2[0] = tl2;
            array2[1] = tr2;
            array2[2] = br2;
            array2[3] = bl2;
            return overlaps1Way(array, array2) && overlaps1Way(array2, array);
        }

        // Token: 0x06000025 RID: 37 RVA: 0x000049FD File Offset: 0x00002BFD
        public static float DEGREES_TO_RADIANS(float D)
        {
            return (float)((double)D * 3.141592653589793 / 180.0);
        }

        // Token: 0x06000026 RID: 38 RVA: 0x00004A16 File Offset: 0x00002C16
        public static float RADIANS_TO_DEGREES(float R)
        {
            return (float)((double)(R * 180f) / 3.141592653589793);
        }

        // Token: 0x06000027 RID: 39 RVA: 0x00004A2C File Offset: 0x00002C2C
        private static bool overlaps1Way(Vector[] corner, Vector[] other)
        {
            Vector[] array = new Vector[2];
            float[] array2 = new float[2];
            array[0] = vectSub(corner[1], corner[0]);
            array[1] = vectSub(corner[3], corner[0]);
            for (int i = 0; i < 2; i++)
            {
                array[i] = vectDiv(array[i], vectLengthsq(array[i]));
                array2[i] = vectDot(corner[0], array[i]);
            }
            for (int j = 0; j < 2; j++)
            {
                float num = vectDot(other[0], array[j]);
                float num2 = num;
                float num3 = num;
                for (int k = 1; k < 4; k++)
                {
                    num = vectDot(other[k], array[j]);
                    if (num < num2)
                    {
                        num2 = num;
                    }
                    else if (num > num3)
                    {
                        num3 = num;
                    }
                }
                if (num2 > 1f + array2[j] || num3 < array2[j])
                {
                    return false;
                }
            }
            return true;
        }

        // Token: 0x06000028 RID: 40 RVA: 0x00004B8C File Offset: 0x00002D8C
        public static Rectangle rectInRectIntersection(Rectangle r1, Rectangle r2)
        {
            Rectangle rectangle = r2;
            rectangle.x = r2.x - r1.x;
            rectangle.y = r2.y - r1.y;
            if (rectangle.x < 0f)
            {
                rectangle.w += rectangle.x;
                rectangle.x = 0f;
            }
            if (rectangle.x + rectangle.w > r1.w)
            {
                rectangle.w = r1.w - rectangle.x;
            }
            if (rectangle.y < 0f)
            {
                rectangle.h += rectangle.y;
                rectangle.y = 0f;
            }
            if (rectangle.y + rectangle.h > r1.h)
            {
                rectangle.h = r1.h - rectangle.y;
            }
            return rectangle;
        }

        // Token: 0x06000029 RID: 41 RVA: 0x00004C84 File Offset: 0x00002E84
        public static float angleTo0_360(float angle)
        {
            float num = angle;
            while (Math.Abs(num) > 360f)
            {
                num -= (num > 0f) ? 360f : (-360f);
            }
            if (num < 0f)
            {
                num += 360f;
            }
            return num;
        }

        // Token: 0x0600002A RID: 42 RVA: 0x00004CCA File Offset: 0x00002ECA
        public static Vector vect(double x, double y)
        {
            return vect((float)x, (float)y);
        }

        // Token: 0x0600002B RID: 43 RVA: 0x00004CD5 File Offset: 0x00002ED5
        public static Vector vect(float x, float y)
        {
            return new Vector(x, y);
        }

        // Token: 0x0600002C RID: 44 RVA: 0x00004CDE File Offset: 0x00002EDE
        public static bool vectEqual(Vector v1, Vector v2)
        {
            return v1.x == v2.x && v1.y == v2.y;
        }

        // Token: 0x0600002D RID: 45 RVA: 0x00004D02 File Offset: 0x00002F02
        public static bool vectEqualApproximately(Vector v1, Vector v2, float Approximation)
        {
            return Math.Abs(v1.x - v2.x) <= Approximation && Math.Abs(v1.y - v2.y) <= Approximation;
        }

        // Token: 0x0600002E RID: 46 RVA: 0x00004D37 File Offset: 0x00002F37
        public static Vector vectAdd(Vector v1, Vector v2)
        {
            return vect(v1.x + v2.x, v1.y + v2.y);
        }

        // Token: 0x0600002F RID: 47 RVA: 0x00004D5C File Offset: 0x00002F5C
        public static void vectAdd2(ref Vector v1, Vector v2)
        {
            v1.x += v2.x;
            v1.y += v2.y;
        }

        // Token: 0x06000030 RID: 48 RVA: 0x00004D86 File Offset: 0x00002F86
        public static Vector vectNeg(Vector v)
        {
            return vect(-v.x, -v.y);
        }

        // Token: 0x06000031 RID: 49 RVA: 0x00004D9D File Offset: 0x00002F9D
        public static Vector vectSub(Vector v1, Vector v2)
        {
            return vect(v1.x - v2.x, v1.y - v2.y);
        }

        // Token: 0x06000032 RID: 50 RVA: 0x00004DC2 File Offset: 0x00002FC2
        public static void vectSub2(ref Vector v1, Vector v2)
        {
            v1.x -= v2.x;
            v1.y -= v2.y;
        }

        // Token: 0x06000033 RID: 51 RVA: 0x00004DEC File Offset: 0x00002FEC
        public static Vector vectMult(Vector v, double s)
        {
            return vectMult(v, (float)s);
        }

        // Token: 0x06000034 RID: 52 RVA: 0x00004DF6 File Offset: 0x00002FF6
        public static Vector vectMult(Vector v, float s)
        {
            return vect(v.x * s, v.y * s);
        }

        // Token: 0x06000035 RID: 53 RVA: 0x00004E0F File Offset: 0x0000300F
        public static Vector vectDiv(Vector v, float s)
        {
            return vect(v.x / s, v.y / s);
        }

        // Token: 0x06000036 RID: 54 RVA: 0x00004E28 File Offset: 0x00003028
        public static float vectDot(Vector v1, Vector v2)
        {
            return (v1.x * v2.x) + (v1.y * v2.y);
        }

        // Token: 0x06000037 RID: 55 RVA: 0x00004E49 File Offset: 0x00003049
        private static float vectCross(Vector v1, Vector v2)
        {
            return (v1.x * v2.y) - (v1.y * v2.x);
        }

        // Token: 0x06000038 RID: 56 RVA: 0x00004E6A File Offset: 0x0000306A
        public static Vector vectPerp(Vector v)
        {
            return vect(-v.y, v.x);
        }

        // Token: 0x06000039 RID: 57 RVA: 0x00004E80 File Offset: 0x00003080
        public static Vector vectRperp(Vector v)
        {
            return vect(v.y, -v.x);
        }

        // Token: 0x0600003A RID: 58 RVA: 0x00004E96 File Offset: 0x00003096
        private static Vector vectProject(Vector v1, Vector v2)
        {
            return vectMult(v2, vectDot(v1, v2) / vectDot(v2, v2));
        }

        // Token: 0x0600003B RID: 59 RVA: 0x00004EB0 File Offset: 0x000030B0
        private static Vector vectRotateByVector(Vector v1, Vector v2)
        {
            return vect((v1.x * v2.x) - (v1.y * v2.y), (v1.x * v2.y) + (v1.y * v2.x));
        }

        // Token: 0x0600003C RID: 60 RVA: 0x00004F00 File Offset: 0x00003100
        private static Vector vectUnrotateByVector(Vector v1, Vector v2)
        {
            return vect((v1.x * v2.x) + (v1.y * v2.y), (v1.y * v2.x) - (v1.x * v2.y));
        }

        // Token: 0x0600003D RID: 61 RVA: 0x00004F50 File Offset: 0x00003150
        public static float vectAngle(Vector v)
        {
            return (float)Math.Atan((double)(v.y / v.x));
        }

        // Token: 0x0600003E RID: 62 RVA: 0x00004F68 File Offset: 0x00003168
        public static float vectAngleNormalized(Vector v)
        {
            return (float)Math.Atan2(v.y, v.x);
        }

        // Token: 0x0600003F RID: 63 RVA: 0x00004F80 File Offset: 0x00003180
        public static float vectLength(Vector v)
        {
            return (float)Math.Sqrt((double)vectDot(v, v));
        }

        // Token: 0x06000040 RID: 64 RVA: 0x00004F90 File Offset: 0x00003190
        public static float vectLengthsq(Vector v)
        {
            return vectDot(v, v);
        }

        // Token: 0x06000041 RID: 65 RVA: 0x00004F99 File Offset: 0x00003199
        public static Vector vectNormalize(Vector v)
        {
            return vectMult(v, 1f / vectLength(v));
        }

        // Token: 0x06000042 RID: 66 RVA: 0x00004FAD File Offset: 0x000031AD
        public static Vector vectForAngle(float a)
        {
            return vect(fmCos(a), fmSin(a));
        }

        // Token: 0x06000043 RID: 67 RVA: 0x00004FC0 File Offset: 0x000031C0
        private static float vectToAngle(Vector v)
        {
            return (float)Math.Atan2(v.x, v.y);
        }

        // Token: 0x06000044 RID: 68 RVA: 0x00004FD8 File Offset: 0x000031D8
        public static float vectDistance(Vector v1, Vector v2)
        {
            Vector vector = vectSub(v1, v2);
            return vectLength(vector);
        }

        // Token: 0x06000045 RID: 69 RVA: 0x00004FF4 File Offset: 0x000031F4
        public static Vector vectRotate(Vector v, double rad)
        {
            float num = fmCos((float)rad);
            float num2 = fmSin((float)rad);
            float num3 = (v.x * num) - (v.y * num2);
            float num4 = (v.x * num2) + (v.y * num);
            return vect(num3, num4);
        }

        // Token: 0x06000046 RID: 70 RVA: 0x00005040 File Offset: 0x00003240
        public static Vector vectRotateAround(Vector v, double rad, float cx, float cy)
        {
            Vector vector = v;
            vector.x -= cx;
            vector.y -= cy;
            vector = vectRotate(vector, rad);
            vector.x += cx;
            vector.y += cy;
            return vector;
        }

        // Token: 0x06000047 RID: 71 RVA: 0x00005094 File Offset: 0x00003294
        private static Vector vectSidePerp(Vector v1, Vector v2)
        {
            Vector vector = vectRperp(vectSub(v2, v1));
            return vectNormalize(vector);
        }

        // Token: 0x06000048 RID: 72 RVA: 0x000050B4 File Offset: 0x000032B4
        private static int vcode(float x_min, float y_min, float x_max, float y_max, Vector p)
        {
            return ((p.x < x_min) ? 1 : 0) + ((p.x > x_max) ? 2 : 0) + ((p.y < y_min) ? 4 : 0) + ((p.y > y_max) ? 8 : 0);
        }

        // Token: 0x06000049 RID: 73 RVA: 0x000050F4 File Offset: 0x000032F4
        public static bool lineInRect(float x1, float y1, float x2, float y2, float rx, float ry, float w, float h)
        {
            VectorClass vectorClass = new(vect(x1, y1));
            VectorClass vectorClass2 = new(vect(x2, y2));
            float num = rx + w;
            float num2 = ry + h;
            int num3 = vcode(rx, ry, num, num2, vectorClass.v);
            int num4 = vcode(rx, ry, num, num2, vectorClass2.v);
            while (num3 != 0 || num4 != 0)
            {
                if ((num3 & num4) != 0)
                {
                    return false;
                }
                int num5;
                VectorClass vectorClass3;
                if (num3 != 0)
                {
                    num5 = num3;
                    vectorClass3 = vectorClass;
                }
                else
                {
                    num5 = num4;
                    vectorClass3 = vectorClass2;
                }
                if ((num5 & 1) != 0)
                {
                    VectorClass vectorClass4 = vectorClass3;
                    vectorClass4.v.y += (y1 - y2) * (rx - vectorClass3.v.x) / (x1 - x2);
                    vectorClass3.v.x = rx;
                }
                else if ((num5 & 2) != 0)
                {
                    VectorClass vectorClass5 = vectorClass3;
                    vectorClass5.v.y += (y1 - y2) * (num - vectorClass3.v.x) / (x1 - x2);
                    vectorClass3.v.x = num;
                }
                if ((num5 & 4) != 0)
                {
                    VectorClass vectorClass6 = vectorClass3;
                    vectorClass6.v.x += (x1 - x2) * (ry - vectorClass3.v.y) / (y1 - y2);
                    vectorClass3.v.y = ry;
                }
                else if ((num5 & 8) != 0)
                {
                    VectorClass vectorClass7 = vectorClass3;
                    vectorClass7.v.x += (x1 - x2) * (num2 - vectorClass3.v.y) / (y1 - y2);
                    vectorClass3.v.y = num2;
                }
                if (num5 == num3)
                {
                    num3 = vcode(rx, ry, num, num2, vectorClass.v);
                }
                else
                {
                    num4 = vcode(rx, ry, num, num2, vectorClass2.v);
                }
            }
            return true;
        }

        // Token: 0x0600004A RID: 74 RVA: 0x000052AC File Offset: 0x000034AC
        public static bool lineInLine(float x1, float y1, float x2, float y2, float x3, float y3, float x4, float y4)
        {
            Vector vector;
            vector.x = x3 - x1 + x4 - x2;
            vector.y = y3 - y1 + y4 - y2;
            Vector vector2;
            vector2.x = x2 - x1;
            vector2.y = y2 - y1;
            Vector vector3;
            vector3.x = x4 - x3;
            vector3.y = y4 - y3;
            float num = (vector2.y * vector3.x) - (vector3.y * vector2.x);
            float num2 = (vector3.x * vector.y) - (vector3.y * vector.x);
            float num3 = (vector2.x * vector.y) - (vector2.y * vector.x);
            return Math.Abs(num2) <= Math.Abs(num) && Math.Abs(num3) <= Math.Abs(num);
        }

        // Token: 0x0600004B RID: 75 RVA: 0x0000538C File Offset: 0x0000358C
        public static float FLOAT_RND_RANGE(int S, int F)
        {
            return RND_RANGE(S * 1000, F * 1000) / 1000f;
        }

        // Token: 0x0600004C RID: 76 RVA: 0x000053A8 File Offset: 0x000035A8
        public static NSString getMD5Str(NSString input)
        {
            return getMD5(input.getCharacters());
        }

        // Token: 0x0600004D RID: 77 RVA: 0x000053B8 File Offset: 0x000035B8
        public static NSString getMD5(char[] data)
        {
            byte[] array = new byte[data.Length * 2];
            for (int i = 0; i < data.Length; i++)
            {
                array[i * 2] = (byte)((data[i] & '\uff00') >> 8);
                array[(i * 2) + 1] = (byte)(data[i] & 'ÿ');
            }
            md5.md5_context md5_context = new();
            md5.md5_starts(ref md5_context);
            md5.md5_update(ref md5_context, array, (uint)array.Length);
            byte[] array2 = new byte[16];
            md5.md5_finish(ref md5_context, array2);
            char[] array3 = new char[32];
            int num = 0;
            for (int j = 0; j < 16; j++)
            {
                int num2 = array2[j];
                int num3 = (num2 >> 4) & 15;
                array3[num++] = (char)((num3 < 10) ? (48 + num3) : (97 + num3 - 10));
                num3 = num2 & 15;
                array3[num++] = (char)((num3 < 10) ? (48 + num3) : (97 + num3 - 10));
            }
            string text = new(array3);
            return new NSString(text);
        }

        // Token: 0x040006C3 RID: 1731
        private const int fmSinCosSize = 1024;

        // Token: 0x040006C4 RID: 1732
        public const double M_PI = 3.141592653589793;

        // Token: 0x040006C5 RID: 1733
        private const int COHEN_LEFT = 1;

        // Token: 0x040006C6 RID: 1734
        private const int COHEN_RIGHT = 2;

        // Token: 0x040006C7 RID: 1735
        private const int COHEN_BOT = 4;

        // Token: 0x040006C8 RID: 1736
        private const int COHEN_TOP = 8;

        // Token: 0x040006C9 RID: 1737
        private static readonly Random random_ = new();

        // Token: 0x040006CA RID: 1738
        private static readonly long ARC4RANDOM_MAX = 4294967296L;

        // Token: 0x040006CB RID: 1739
        private static float[] fmSins;

        // Token: 0x040006CC RID: 1740
        private static float[] fmCoss;

        // Token: 0x040006CD RID: 1741
        public static readonly Vector vectZero = new(0f, 0f);

        // Token: 0x040006CE RID: 1742
        public static readonly Vector vectUndefined = new(2.1474836E+09f, 2.1474836E+09f);
    }
}
