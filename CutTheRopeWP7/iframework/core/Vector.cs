using Microsoft.Xna.Framework;

namespace ctr_wp7.iframework.core
{
    // Token: 0x02000081 RID: 129
    public struct Vector
    {
        // Token: 0x060003BD RID: 957 RVA: 0x00018081 File Offset: 0x00016281
        public Vector(Vector2 v)
        {
            x = v.X;
            y = v.Y;
        }

        // Token: 0x060003BE RID: 958 RVA: 0x0001809D File Offset: 0x0001629D
        public Vector(double xParam, double yParam)
        {
            x = (float)xParam;
            y = (float)yParam;
        }

        // Token: 0x060003BF RID: 959 RVA: 0x000180AF File Offset: 0x000162AF
        public Vector(float xParam, float yParam)
        {
            x = xParam;
            y = yParam;
        }

        // Token: 0x060003C0 RID: 960 RVA: 0x000180BF File Offset: 0x000162BF
        public readonly Vector2 toXNA()
        {
            return new Vector2(x, y);
        }

        // Token: 0x060003C1 RID: 961 RVA: 0x000180D4 File Offset: 0x000162D4
        public override readonly string ToString()
        {
            return string.Concat(new object[] { "Vector(x=", x, ",y=", y, ")" });
        }

        // Token: 0x04000942 RID: 2370
        public float x;

        // Token: 0x04000943 RID: 2371
        public float y;
    }
}
