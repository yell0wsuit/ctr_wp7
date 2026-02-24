using System.Collections.Generic;

using ctr_wp7.iframework.core;
using ctr_wp7.iframework.helpers;
using ctr_wp7.ios;

namespace ctr_wp7.iframework.visual
{
    // Token: 0x020000AF RID: 175
    internal class GLDrawer : NSObject
    {
        // Token: 0x060004CE RID: 1230 RVA: 0x00023FA7 File Offset: 0x000221A7
        public static void drawImage(Texture2D image, float x, float y)
        {
            Texture2D.drawAtPoint(image, MathHelper.vect(x, y));
        }

        // Token: 0x060004CF RID: 1231 RVA: 0x00023FB6 File Offset: 0x000221B6
        public static void drawImagePart(Texture2D image, Rectangle r, float x, float y)
        {
            Texture2D.drawRectAtPoint(image, r, MathHelper.vect(x, y));
        }

        // Token: 0x060004D0 RID: 1232 RVA: 0x00023FC6 File Offset: 0x000221C6
        public static void drawImageQuad(Texture2D image, int q, double x, double y)
        {
            GLDrawer.drawImageQuad(image, q, (float)x, (float)y);
        }

        // Token: 0x060004D1 RID: 1233 RVA: 0x00023FD3 File Offset: 0x000221D3
        public static void drawImageQuad(Texture2D image, int q, float x, float y)
        {
            if (q == -1)
            {
                GLDrawer.drawImage(image, x, y);
                return;
            }
            Texture2D.drawQuadAtPoint(image, q, MathHelper.vect(x, y));
        }

        // Token: 0x060004D2 RID: 1234 RVA: 0x00023FF0 File Offset: 0x000221F0
        public static void drawImageTiledCool(Texture2D image, int q, float x, float y, float width, float height)
        {
            float num = 0f;
            float num2 = 0f;
            float num3;
            float num4;
            if (q == -1)
            {
                num3 = (float)image._realWidth;
                num4 = (float)image._realHeight;
            }
            else
            {
                num = image.quadRects[q].x;
                num2 = image.quadRects[q].y;
                num3 = image.quadRects[q].w;
                num4 = image.quadRects[q].h;
            }
            for (float num5 = 0f; num5 < height; num5 += num4)
            {
                for (float num6 = 0f; num6 < width; num6 += num3)
                {
                    float num7 = width - num6;
                    if (num7 > num3)
                    {
                        num7 = num3;
                    }
                    float num8 = height - num5;
                    if (num8 > num4)
                    {
                        num8 = num4;
                    }
                    Rectangle rectangle = FrameworkTypes.MakeRectangle(num, num2, num7, num8);
                    GLDrawer.drawImagePart(image, rectangle, x + num6, y + num5);
                }
            }
        }

        // Token: 0x060004D3 RID: 1235 RVA: 0x000240CC File Offset: 0x000222CC
        public static void drawImageTiled(Texture2D image, int q, float x, float y, float width, float height)
        {
            if (FrameworkTypes.IS_WVGA)
            {
                GLDrawer.drawImageTiledCool(image, q, x, y, width, height);
                return;
            }
            float num = 0f;
            float num2 = 0f;
            float num3;
            float num4;
            if (q == -1)
            {
                num3 = (float)image._realWidth;
                num4 = (float)image._realHeight;
            }
            else
            {
                num = image.quadRects[q].x;
                num2 = image.quadRects[q].y;
                num3 = image.quadRects[q].w;
                num4 = image.quadRects[q].h;
            }
            if (width == num3 && height == num4)
            {
                GLDrawer.drawImageQuad(image, q, x, y);
                return;
            }
            int num5 = (int)MathHelper.ceil((double)(width / num3));
            int num6 = (int)MathHelper.ceil((double)(height / num4));
            int num7 = (int)width % (int)num3;
            int num8 = (int)height % (int)num4;
            int num9 = (int)((num7 == 0) ? num3 : ((float)num7));
            int num10 = (int)((num8 == 0) ? num4 : ((float)num8));
            int num11 = (int)x;
            int num12 = (int)y;
            for (int i = num6 - 1; i >= 0; i--)
            {
                num11 = (int)x;
                for (int j = num5 - 1; j >= 0; j--)
                {
                    if (j == 0 || i == 0)
                    {
                        Rectangle rectangle = FrameworkTypes.MakeRectangle(num, num2, (j == 0) ? ((float)num9) : num3, (i == 0) ? ((float)num10) : num4);
                        GLDrawer.drawImagePart(image, rectangle, (float)num11, (float)num12);
                    }
                    else
                    {
                        GLDrawer.drawImageQuad(image, q, (float)num11, (float)num12);
                    }
                    num11 += (int)num3;
                }
                num12 += (int)num4;
            }
        }

        // Token: 0x060004D4 RID: 1236 RVA: 0x00024232 File Offset: 0x00022432
        public static Quad2D getTextureCoordinates(Texture2D t, Rectangle r)
        {
            return Quad2D.MakeQuad2D(t._invWidth * r.x, t._invHeight * r.y, t._invWidth * r.w, t._invHeight * r.h);
        }

        // Token: 0x060004D5 RID: 1237 RVA: 0x00024274 File Offset: 0x00022474
        public static Vector calcPathBezier(Vector[] p, int count, float delta)
        {
            if (count > 2)
            {
                Vector[] array;
                if (!GLDrawer.VectorArray.TryGetValue(count - 1, out array))
                {
                    array = new Vector[count - 1];
                    GLDrawer.VectorArray.Add(count - 1, array);
                }
                for (int i = 0; i < count - 1; i++)
                {
                    array[i] = GLDrawer.calc2PointBezier(ref p[i], ref p[i + 1], delta);
                }
                return GLDrawer.calcPathBezier_2(array, count - 1, delta);
            }
            if (count == 2)
            {
                return GLDrawer.calc2PointBezier(ref p[0], ref p[1], delta);
            }
            return default(Vector);
        }

        // Token: 0x060004D6 RID: 1238 RVA: 0x0002430C File Offset: 0x0002250C
        public static Vector calcPathBezier_2(Vector[] p, int count, float delta)
        {
            if (count > 2)
            {
                for (int i = 0; i < count - 1; i++)
                {
                    p[i] = GLDrawer.calc2PointBezier(ref p[i], ref p[i + 1], delta);
                }
                return GLDrawer.calcPathBezier_2(p, count - 1, delta);
            }
            if (count == 2)
            {
                return GLDrawer.calc2PointBezier(ref p[0], ref p[1], delta);
            }
            return default(Vector);
        }

        // Token: 0x060004D7 RID: 1239 RVA: 0x0002437C File Offset: 0x0002257C
        public static Vector calc2PointBezier(ref Vector a, ref Vector b, float delta)
        {
            float num = 1f - delta;
            Vector vector;
            vector.x = a.x * num + b.x * delta;
            vector.y = a.y * num + b.y * delta;
            return vector;
        }

        // Token: 0x060004D8 RID: 1240 RVA: 0x000243C4 File Offset: 0x000225C4
        public static void calcCircle(float x, float y, float radius, int vertexCount, float[] glVertices)
        {
            float num = (float)(6.283185307179586 / (double)vertexCount);
            float num2 = 0f;
            for (int i = 0; i < vertexCount; i++)
            {
                glVertices[i * 2] = x + radius * MathHelper.cosf(num2);
                glVertices[i * 2 + 1] = y + radius * MathHelper.sinf(num2);
                num2 += num;
            }
        }

        // Token: 0x060004D9 RID: 1241 RVA: 0x00024418 File Offset: 0x00022618
        public static void drawCircleIntersection(float cx1, float cy1, float radius1, float cx2, float cy2, float radius2, int vertexCount, float width, RGBAColor fill)
        {
            float num = MathHelper.vectDistance(MathHelper.vect(cx1, cy1), MathHelper.vect(cx2, cy2));
            if (num >= radius1 + radius2 || radius1 >= num + radius2)
            {
                return;
            }
            float num2 = (radius1 * radius1 - radius2 * radius2 + num * num) / (2f * num);
            float num3 = num - num2;
            float num4 = MathHelper.acosf(num3 / radius2);
            float num5 = MathHelper.vectAngle(MathHelper.vectSub(MathHelper.vect(cx1, cy1), MathHelper.vect(cx2, cy2)));
            float num6 = num5 - num4;
            float num7 = num5 + num4;
            if (cx2 > cx1)
            {
                num6 += 3.1415927f;
                num7 += 3.1415927f;
            }
            GLDrawer.drawAntialiasedCurve2(cx2, cy2, radius2, num6, num7, vertexCount, width, 1f, fill);
        }

        // Token: 0x060004DA RID: 1242 RVA: 0x000244C8 File Offset: 0x000226C8
        public static void drawAntialiasedCurve2(float cx, float cy, float radius, float startAngle, float endAngle, int vertexCount, float width, float fadeWidth, RGBAColor fill)
        {
            float[] array = new float[(vertexCount - 1) * 12 + 4];
            float[] array2 = new float[vertexCount * 2];
            float[] array3 = new float[vertexCount * 2];
            float[] array4 = new float[vertexCount * 2];
            float[] array5 = new float[vertexCount * 2];
            RGBAColor[] array6 = new RGBAColor[(vertexCount - 1) * 6 + 2];
            GLDrawer.calcCurve(cx, cy, radius + fadeWidth, startAngle, endAngle, vertexCount, array2);
            GLDrawer.calcCurve(cx, cy, radius, startAngle, endAngle, vertexCount, array3);
            GLDrawer.calcCurve(cx, cy, radius - width, startAngle, endAngle, vertexCount, array4);
            GLDrawer.calcCurve(cx, cy, radius - width - fadeWidth, startAngle, endAngle, vertexCount, array5);
            array[0] = array2[0];
            array[1] = array2[1];
            array6[0] = RGBAColor.transparentRGBA;
            for (int i = 1; i < vertexCount; i += 2)
            {
                array[12 * i - 10] = array2[i * 2];
                array[12 * i - 9] = array2[i * 2 + 1];
                array[12 * i - 8] = array3[i * 2 - 2];
                array[12 * i - 7] = array3[i * 2 - 1];
                array[12 * i - 6] = array3[i * 2];
                array[12 * i - 5] = array3[i * 2 + 1];
                array[12 * i - 4] = array4[i * 2 - 2];
                array[12 * i - 3] = array4[i * 2 - 1];
                array[12 * i - 2] = array4[i * 2];
                array[12 * i - 1] = array4[i * 2 + 1];
                array[12 * i] = array5[i * 2 - 2];
                array[12 * i + 1] = array5[i * 2 - 1];
                array[12 * i + 2] = array5[i * 2 + 2];
                array[12 * i + 3] = array5[i * 2 + 3];
                array[12 * i + 4] = array4[i * 2];
                array[12 * i + 5] = array4[i * 2 + 1];
                array[12 * i + 6] = array4[i * 2 + 2];
                array[12 * i + 7] = array4[i * 2 + 3];
                array[12 * i + 8] = array3[i * 2];
                array[12 * i + 9] = array3[i * 2 + 1];
                array[12 * i + 10] = array3[i * 2 + 2];
                array[12 * i + 11] = array3[i * 2 + 3];
                array[12 * i + 12] = array2[i * 2];
                array[12 * i + 13] = array2[i * 2 + 1];
                array6[6 * i - 5] = RGBAColor.transparentRGBA;
                array6[6 * i - 4] = fill;
                array6[6 * i - 3] = fill;
                array6[6 * i - 2] = fill;
                array6[6 * i - 1] = fill;
                array6[6 * i] = RGBAColor.transparentRGBA;
                array6[6 * i + 1] = RGBAColor.transparentRGBA;
                array6[6 * i + 2] = fill;
                array6[6 * i + 3] = fill;
                array6[6 * i + 4] = fill;
                array6[6 * i + 5] = fill;
                array6[6 * i + 6] = RGBAColor.transparentRGBA;
            }
            array[(vertexCount - 1) * 12 + 2] = array2[vertexCount * 2 - 2];
            array[(vertexCount - 1) * 12 + 3] = array2[vertexCount * 2 - 1];
            array6[(vertexCount - 1) * 6 + 1] = RGBAColor.transparentRGBA;
            OpenGL.glColorPointer(4, 5, 0, array6);
            OpenGL.glDisableClientState(0);
            OpenGL.glEnableClientState(13);
            OpenGL.glVertexPointer(2, 5, 0, array);
            OpenGL.glDrawArrays(8, 0, (vertexCount - 1) * 6 + 2);
            OpenGL.glEnableClientState(0);
            OpenGL.glDisableClientState(13);
        }

        // Token: 0x060004DB RID: 1243 RVA: 0x000248A0 File Offset: 0x00022AA0
        private static void calcCurve(float cx, float cy, float radius, float startAngle, float endAngle, int vertexCount, float[] glVertices)
        {
            float num = (endAngle - startAngle) / (float)(vertexCount - 1);
            float num2 = MathHelper.tanf(num);
            float num3 = MathHelper.cosf(num);
            float num4 = radius * MathHelper.cosf(startAngle);
            float num5 = radius * MathHelper.sinf(startAngle);
            for (int i = 0; i < vertexCount; i++)
            {
                glVertices[i * 2] = num4 + cx;
                glVertices[i * 2 + 1] = num5 + cy;
                float num6 = -num5;
                float num7 = num4;
                num4 += num6 * num2;
                num5 += num7 * num2;
                num4 *= num3;
                num5 *= num3;
            }
        }

        // Token: 0x060004DC RID: 1244 RVA: 0x00024924 File Offset: 0x00022B24
        public static void drawAntialiasedLine(float x1, float y1, float x2, float y2, float size, RGBAColor color)
        {
            Vector vector = MathHelper.vect(x1, y1);
            Vector vector2 = MathHelper.vect(x2, y2);
            Vector vector3 = MathHelper.vectSub(vector2, vector);
            Vector vector4 = MathHelper.vectPerp(vector3);
            Vector vector5 = MathHelper.vectNormalize(vector4);
            vector4 = MathHelper.vectMult(vector5, size);
            Vector vector6 = MathHelper.vectNeg(vector4);
            Vector vector7 = MathHelper.vectAdd(vector4, vector3);
            Vector vector8 = MathHelper.vectAdd(MathHelper.vectNeg(vector4), vector3);
            MathHelper.vectAdd2(ref vector4, vector);
            MathHelper.vectAdd2(ref vector6, vector);
            MathHelper.vectAdd2(ref vector7, vector);
            MathHelper.vectAdd2(ref vector8, vector);
            Vector vector9 = MathHelper.vectSub(vector4, vector5);
            Vector vector10 = MathHelper.vectSub(vector7, vector5);
            Vector vector11 = MathHelper.vectAdd(vector6, vector5);
            Vector vector12 = MathHelper.vectAdd(vector8, vector5);
            GLDrawer.verts[0] = vector4.x;
            GLDrawer.verts[1] = vector4.y;
            GLDrawer.verts[2] = vector7.x;
            GLDrawer.verts[3] = vector7.y;
            GLDrawer.verts[4] = vector9.x;
            GLDrawer.verts[5] = vector9.y;
            GLDrawer.verts[6] = vector10.x;
            GLDrawer.verts[7] = vector10.y;
            GLDrawer.verts[8] = vector11.x;
            GLDrawer.verts[9] = vector11.y;
            GLDrawer.verts[10] = vector12.x;
            GLDrawer.verts[11] = vector12.y;
            GLDrawer.verts[12] = vector6.x;
            GLDrawer.verts[13] = vector6.y;
            GLDrawer.verts[14] = vector8.x;
            GLDrawer.verts[15] = vector8.y;
            RGBAColor[] array = GLDrawer.colors;
            int num = 2;
            RGBAColor[] array2 = GLDrawer.colors;
            int num2 = 3;
            RGBAColor[] array3 = GLDrawer.colors;
            int num3 = 4;
            GLDrawer.colors[5] = color;
            array[num] = (array2[num2] = (array3[num3] = color));
            OpenGL.glColorPointer_add(4, 5, 0, GLDrawer.colors);
            OpenGL.glVertexPointer_add(2, 5, 0, GLDrawer.verts);
        }

        // Token: 0x060004DD RID: 1245 RVA: 0x00024B20 File Offset: 0x00022D20
        public static void drawRect(float x, float y, float w, float h, RGBAColor color)
        {
            float[] array = new float[]
            {
                x,
                y,
                x + w,
                y,
                x,
                y + h,
                x + w,
                y + h
            };
            GLDrawer.drawPolygon(array, 4, color);
        }

        // Token: 0x060004DE RID: 1246 RVA: 0x00024B68 File Offset: 0x00022D68
        public static void drawSolidRect(float x, float y, float w, float h, RGBAColor border, RGBAColor fill)
        {
            float[] array = new float[]
            {
                x,
                y,
                x + w,
                y,
                x,
                y + h,
                x + w,
                y + h
            };
            GLDrawer.drawSolidPolygon(array, 4, border, fill);
        }

        // Token: 0x060004DF RID: 1247 RVA: 0x00024BB4 File Offset: 0x00022DB4
        public static void drawSolidRectWOBorder(float x, float y, float w, float h, RGBAColor fill)
        {
            float[] array = new float[]
            {
                x,
                y,
                x + w,
                y,
                x,
                y + h,
                x + w,
                y + h
            };
            OpenGL.glColor4f(fill.r, fill.g, fill.b, fill.a);
            OpenGL.glVertexPointer(2, 5, 0, array);
            OpenGL.glDrawArrays(8, 0, 4);
        }

        // Token: 0x060004E0 RID: 1248 RVA: 0x00024C24 File Offset: 0x00022E24
        public static void drawPolygon(float[] vertices, int vertexCount, RGBAColor color)
        {
            OpenGL.glColor4f(color.r, color.g, color.b, color.a);
            OpenGL.glVertexPointer(2, 5, 0, vertices);
            OpenGL.glDrawArrays(9, 0, vertexCount);
        }

        // Token: 0x060004E1 RID: 1249 RVA: 0x00024C5C File Offset: 0x00022E5C
        public static void drawSolidPolygon(float[] vertices, int vertexCount, RGBAColor border, RGBAColor fill)
        {
            OpenGL.glVertexPointer(2, 5, 0, vertices);
            OpenGL.glColor4f(fill.r, fill.g, fill.b, fill.a);
            OpenGL.glDrawArrays(8, 0, vertexCount);
            OpenGL.glColor4f(border.r, border.g, border.b, border.a);
            OpenGL.glDrawArrays(9, 0, vertexCount);
        }

        // Token: 0x060004E2 RID: 1250 RVA: 0x00024CC5 File Offset: 0x00022EC5
        public static void drawSolidPolygonWOBorder(float[] vertices, int vertexCount, RGBAColor fill)
        {
            OpenGL.glVertexPointer(2, 5, 0, vertices);
            OpenGL.glColor4f(fill.r, fill.g, fill.b, fill.a);
            OpenGL.glDrawArrays(8, 0, vertexCount);
        }

        // Token: 0x04000A25 RID: 2597
        private static Dictionary<int, Vector[]> VectorArray = new Dictionary<int, Vector[]>();

        // Token: 0x04000A26 RID: 2598
        private static RGBAColor[] colors = new RGBAColor[]
        {
            RGBAColor.transparentRGBA,
            RGBAColor.transparentRGBA,
            RGBAColor.transparentRGBA,
            RGBAColor.transparentRGBA,
            RGBAColor.transparentRGBA,
            RGBAColor.transparentRGBA,
            RGBAColor.transparentRGBA,
            RGBAColor.transparentRGBA
        };

        // Token: 0x04000A27 RID: 2599
        private static float[] verts = new float[16];
    }
}
