namespace ctr_wp7.iframework
{
    // Token: 0x02000024 RID: 36
    internal struct Quad2D(float x, float y, float w, float h)
    {

        // Token: 0x0600016C RID: 364 RVA: 0x0000B048 File Offset: 0x00009248
        public float[] toFloatArray()
        {
            return [tlX, tlY, trX, trY, blX, blY, brX, brY];
        }

        // Token: 0x0600016D RID: 365 RVA: 0x0000B0A8 File Offset: 0x000092A8
        public static Quad2D MakeQuad2D(float x, float y, float w, float h)
        {
            Quad2D quad2D = new Quad2D(x, y, w, h);
            return quad2D;
        }

        // Token: 0x0400079F RID: 1951
        public float tlX = x;

        // Token: 0x040007A0 RID: 1952
        public float tlY = y;

        // Token: 0x040007A1 RID: 1953
        public float trX = x + w;

        // Token: 0x040007A2 RID: 1954
        public float trY = y;

        // Token: 0x040007A3 RID: 1955
        public float blX = x;

        // Token: 0x040007A4 RID: 1956
        public float blY = y + h;

        // Token: 0x040007A5 RID: 1957
        public float brX = x + w;

        // Token: 0x040007A6 RID: 1958
        public float brY = y + h;
    }
}
