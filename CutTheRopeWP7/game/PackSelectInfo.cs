using ctr_wp7.iframework.visual;
using ctr_wp7.ios;

namespace ctr_wp7.game
{
    // Token: 0x02000110 RID: 272
    internal sealed class PackSelectInfo : NSObject
    {
        // Token: 0x0600084C RID: 2124 RVA: 0x0004A188 File Offset: 0x00048388
        public NSObject initWithSize(int pSize)
        {
            if (base.init() != null)
            {
                i = 0;
                size = pSize;
                nextpack = -1;
                content = new int[size];
                elements = new BaseElement[size];
            }
            return this;
        }

        // Token: 0x0600084D RID: 2125 RVA: 0x0004A1D8 File Offset: 0x000483D8
        public bool isBoxExist(int box)
        {
            for (int i = 0; i < size; i++)
            {
                if (content[i] == box)
                {
                    return true;
                }
            }
            return false;
        }

        // Token: 0x0600084E RID: 2126 RVA: 0x0004A204 File Offset: 0x00048404
        public int firstIndexOf(int box)
        {
            for (int i = 0; i < size; i++)
            {
                if (content[i] == box)
                {
                    return i;
                }
            }
            return 0;
        }

        // Token: 0x0600084F RID: 2127 RVA: 0x0004A230 File Offset: 0x00048430
        public int getFirstGameBox()
        {
            for (int i = 0; i < size; i++)
            {
                if (BoxFabric.isGameBox(content[i]))
                {
                    return i;
                }
            }
            return -1;
        }

        // Token: 0x06000850 RID: 2128 RVA: 0x0004A260 File Offset: 0x00048460
        public void add(int box)
        {
            content[i] = box;
            i++;
        }

        // Token: 0x06000851 RID: 2129 RVA: 0x0004A27E File Offset: 0x0004847E
        public override void dealloc()
        {
            content = null;
            elements = null;
            base.dealloc();
        }

        // Token: 0x04000DE6 RID: 3558
        protected int i;

        // Token: 0x04000DE7 RID: 3559
        public int size;

        // Token: 0x04000DE8 RID: 3560
        public int nextpack;

        // Token: 0x04000DE9 RID: 3561
        public int[] content;

        // Token: 0x04000DEA RID: 3562
        public BaseElement[] elements;
    }
}
