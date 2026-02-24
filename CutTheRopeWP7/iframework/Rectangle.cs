namespace ctr_wp7.iframework
{
    // Token: 0x02000023 RID: 35
    internal struct Rectangle
    {
        // Token: 0x06000168 RID: 360 RVA: 0x0000AF7B File Offset: 0x0000917B
        public Rectangle(double xParam, double yParam, double width, double height)
        {
            this.x = (float)xParam;
            this.y = (float)yParam;
            this.w = (float)width;
            this.h = (float)height;
        }

        // Token: 0x06000169 RID: 361 RVA: 0x0000AF9E File Offset: 0x0000919E
        public Rectangle(float xParam, float yParam, float width, float height)
        {
            this.x = xParam;
            this.y = yParam;
            this.w = width;
            this.h = height;
        }

        // Token: 0x0600016A RID: 362 RVA: 0x0000AFBD File Offset: 0x000091BD
        public bool isValid()
        {
            return this.x != 0f || this.y != 0f || this.w != 0f || this.h != 0f;
        }

        // Token: 0x0400079B RID: 1947
        public float x;

        // Token: 0x0400079C RID: 1948
        public float y;

        // Token: 0x0400079D RID: 1949
        public float w;

        // Token: 0x0400079E RID: 1950
        public float h;
    }
}
