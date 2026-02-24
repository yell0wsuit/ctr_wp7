using System.Collections.Generic;

using Microsoft.Xna.Framework;

namespace ctr_wp7.iframework
{
    public struct RGBAColor
    {
        public readonly Color toXNA()
        {
            Color color = default;
            int num = (int)(r * 255f);
            int num2 = (int)(g * 255f);
            int num3 = (int)(b * 255f);
            int num4 = (int)(a * 255f);
            color.R = (byte)((num < 0) ? 0 : ((num > 255) ? 255 : num));
            color.G = (byte)((num2 < 0) ? 0 : ((num2 > 255) ? 255 : num2));
            color.B = (byte)((num3 < 0) ? 0 : ((num3 > 255) ? 255 : num3));
            color.A = (byte)((num4 < 0) ? 0 : ((num4 > 255) ? 255 : num4));
            return color;
        }

        public static RGBAColor MakeRGBA(double r, double g, double b, double a)
        {
            return MakeRGBA((float)r, (float)g, (float)b, (float)a);
        }

        public static RGBAColor MakeRGBA(float r, float g, float b, float a)
        {
            RGBAColor rgbacolor = new(r, g, b, a);
            return rgbacolor;
        }

        public static bool RGBAEqual(RGBAColor a, RGBAColor b)
        {
            return a.r == b.r && a.g == b.g && a.b == b.b && a.a == b.a;
        }

        public RGBAColor(double _r, double _g, double _b, double _a)
        {
            r = (float)_r;
            g = (float)_g;
            b = (float)_b;
            a = (float)_a;
        }

        public RGBAColor(float _r, float _g, float _b, float _a)
        {
            r = _r;
            g = _g;
            b = _b;
            a = _a;
        }

        private void init(float _r, float _g, float _b, float _a)
        {
            r = _r;
            g = _g;
            b = _b;
            a = _a;
        }

        public readonly float[] toFloatArray()
        {
            return [r, g, b, a];
        }

        public static float[] toFloatArray(RGBAColor[] colors)
        {
            List<float> list = [];
            for (int i = 0; i < colors.Length; i++)
            {
                list.AddRange(colors[i].toFloatArray());
            }
            return [.. list];
        }

        public static readonly RGBAColor transparentRGBA = new(0f, 0f, 0f, 0f);

        public static readonly RGBAColor solidOpaqueRGBA = new(1f, 1f, 1f, 1f);

        public static readonly RGBAColor redRGBA = new(1.0, 0.0, 0.0, 1.0);

        public static readonly RGBAColor blueRGBA = new(0.0, 0.0, 1.0, 1.0);

        public static readonly RGBAColor greenRGBA = new(0.0, 1.0, 0.0, 1.0);

        public static readonly RGBAColor blackRGBA = new(0.0, 0.0, 0.0, 1.0);

        public static readonly RGBAColor whiteRGBA = new(1.0, 1.0, 1.0, 1.0);

        public float r;

        public float g;

        public float b;

        public float a;
    }
}
