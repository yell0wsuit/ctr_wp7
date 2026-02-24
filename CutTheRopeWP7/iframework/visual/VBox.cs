namespace ctr_wp7.iframework.visual
{
    // Token: 0x0200001B RID: 27
    internal sealed class VBox : BaseElement
    {
        // Token: 0x06000143 RID: 323 RVA: 0x0000A398 File Offset: 0x00008598
        public override int addChildwithID(BaseElement c, int i)
        {
            int num = base.addChildwithID(c, i);
            if (align == 1)
            {
                c.anchor = c.parentAnchor = 9;
            }
            else if (align == 4)
            {
                c.anchor = c.parentAnchor = 12;
            }
            else if (align == 2)
            {
                c.anchor = c.parentAnchor = 10;
            }
            c.y = nextElementY;
            nextElementY += c.height + offset;
            height = (int)(nextElementY - offset);
            return num;
        }

        // Token: 0x06000144 RID: 324 RVA: 0x0000A43C File Offset: 0x0000863C
        public VBox initWithOffsetAlignWidth(double of, int a, double w)
        {
            return initWithOffsetAlignWidth((float)of, a, (float)w);
        }

        // Token: 0x06000145 RID: 325 RVA: 0x0000A449 File Offset: 0x00008649
        public VBox initWithOffsetAlignWidth(float of, int a, float w)
        {
            if (init() != null)
            {
                offset = of;
                align = a;
                nextElementY = 0f;
                width = (int)w;
            }
            return this;
        }

        // Token: 0x0400076B RID: 1899
        public float offset;

        // Token: 0x0400076C RID: 1900
        public int align;

        // Token: 0x0400076D RID: 1901
        public float nextElementY;
    }
}
