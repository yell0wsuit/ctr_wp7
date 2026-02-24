namespace ctr_wp7.iframework
{
    internal struct Quad2D(float x, float y, float w, float h)
    {

        public readonly float[] toFloatArray()
        {
            return [tlX, tlY, trX, trY, blX, blY, brX, brY];
        }

        public static Quad2D MakeQuad2D(float x, float y, float w, float h)
        {
            Quad2D quad2D = new(x, y, w, h);
            return quad2D;
        }

        public float tlX = x;

        public float tlY = y;

        public float trX = x + w;

        public float trY = y;

        public float blX = x;

        public float blY = y + h;

        public float brX = x + w;

        public float brY = y + h;
    }
}
