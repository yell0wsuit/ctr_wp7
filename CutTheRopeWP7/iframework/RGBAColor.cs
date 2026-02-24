using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;

namespace ctr_wp7.iframework
{
    // Token: 0x02000026 RID: 38
    public struct RGBAColor
    {
        // Token: 0x06000172 RID: 370 RVA: 0x0000B27C File Offset: 0x0000947C
        public Color toXNA()
        {
            Color color = default(Color);
            int num = (int)(this.r * 255f);
            int num2 = (int)(this.g * 255f);
            int num3 = (int)(this.b * 255f);
            int num4 = (int)(this.a * 255f);
            color.R = (byte)((num < 0) ? 0 : ((num > 255) ? 255 : num));
            color.G = (byte)((num2 < 0) ? 0 : ((num2 > 255) ? 255 : num2));
            color.B = (byte)((num3 < 0) ? 0 : ((num3 > 255) ? 255 : num3));
            color.A = (byte)((num4 < 0) ? 0 : ((num4 > 255) ? 255 : num4));
            return color;
        }

        // Token: 0x06000173 RID: 371 RVA: 0x0000B34A File Offset: 0x0000954A
        public static RGBAColor MakeRGBA(double r, double g, double b, double a)
        {
            return RGBAColor.MakeRGBA((float)r, (float)g, (float)b, (float)a);
        }

        // Token: 0x06000174 RID: 372 RVA: 0x0000B35C File Offset: 0x0000955C
        public static RGBAColor MakeRGBA(float r, float g, float b, float a)
        {
            RGBAColor rgbacolor = new RGBAColor(r, g, b, a);
            return rgbacolor;
        }

        // Token: 0x06000175 RID: 373 RVA: 0x0000B378 File Offset: 0x00009578
        public static bool RGBAEqual(RGBAColor a, RGBAColor b)
        {
            return a.r == b.r && a.g == b.g && a.b == b.b && a.a == b.a;
        }

        // Token: 0x06000176 RID: 374 RVA: 0x0000B3C7 File Offset: 0x000095C7
        public RGBAColor(double _r, double _g, double _b, double _a)
        {
            this.r = (float)_r;
            this.g = (float)_g;
            this.b = (float)_b;
            this.a = (float)_a;
        }

        // Token: 0x06000177 RID: 375 RVA: 0x0000B3EA File Offset: 0x000095EA
        public RGBAColor(float _r, float _g, float _b, float _a)
        {
            this.r = _r;
            this.g = _g;
            this.b = _b;
            this.a = _a;
        }

        // Token: 0x06000178 RID: 376 RVA: 0x0000B409 File Offset: 0x00009609
        private void init(float _r, float _g, float _b, float _a)
        {
            this.r = _r;
            this.g = _g;
            this.b = _b;
            this.a = _a;
        }

        // Token: 0x06000179 RID: 377 RVA: 0x0000B428 File Offset: 0x00009628
        public float[] toFloatArray()
        {
            return new float[] { this.r, this.g, this.b, this.a };
        }

        // Token: 0x0600017A RID: 378 RVA: 0x0000B464 File Offset: 0x00009664
        public static float[] toFloatArray(RGBAColor[] colors)
        {
            List<float> list = new List<float>();
            for (int i = 0; i < colors.Length; i++)
            {
                list.AddRange(colors[i].toFloatArray());
            }
            return list.ToArray();
        }

        // Token: 0x040007B4 RID: 1972
        public static readonly RGBAColor transparentRGBA = new RGBAColor(0f, 0f, 0f, 0f);

        // Token: 0x040007B5 RID: 1973
        public static readonly RGBAColor solidOpaqueRGBA = new RGBAColor(1f, 1f, 1f, 1f);

        // Token: 0x040007B6 RID: 1974
        public static readonly RGBAColor redRGBA = new RGBAColor(1.0, 0.0, 0.0, 1.0);

        // Token: 0x040007B7 RID: 1975
        public static readonly RGBAColor blueRGBA = new RGBAColor(0.0, 0.0, 1.0, 1.0);

        // Token: 0x040007B8 RID: 1976
        public static readonly RGBAColor greenRGBA = new RGBAColor(0.0, 1.0, 0.0, 1.0);

        // Token: 0x040007B9 RID: 1977
        public static readonly RGBAColor blackRGBA = new RGBAColor(0.0, 0.0, 0.0, 1.0);

        // Token: 0x040007BA RID: 1978
        public static readonly RGBAColor whiteRGBA = new RGBAColor(1.0, 1.0, 1.0, 1.0);

        // Token: 0x040007BB RID: 1979
        public float r;

        // Token: 0x040007BC RID: 1980
        public float g;

        // Token: 0x040007BD RID: 1981
        public float b;

        // Token: 0x040007BE RID: 1982
        public float a;
    }
}
