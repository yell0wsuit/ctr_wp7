using Microsoft.Xna.Framework;

namespace ctr_wp7.iframework.core
{
    public struct Vector
    {
        public Vector(Vector2 v)
        {
            x = v.X;
            y = v.Y;
        }

        public Vector(double xParam, double yParam)
        {
            x = (float)xParam;
            y = (float)yParam;
        }

        public Vector(float xParam, float yParam)
        {
            x = xParam;
            y = yParam;
        }

        public readonly Vector2 toXNA()
        {
            return new Vector2(x, y);
        }

        public override readonly string ToString()
        {
            return string.Concat(new object[] { "Vector(x=", x, ",y=", y, ")" });
        }

        public float x;

        public float y;
    }
}
