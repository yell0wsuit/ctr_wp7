using System;

using ctr_wp7.iframework.visual;
using ctr_wp7.ios;

namespace ctr_wp7.game
{
    // Token: 0x02000110 RID: 272
    internal class PackSelectInfo : NSObject
    {
        // Token: 0x0600084C RID: 2124 RVA: 0x0004A188 File Offset: 0x00048388
        public virtual NSObject initWithSize(int pSize)
        {
            if (base.init() != null)
            {
                this.i = 0;
                this.size = pSize;
                this.nextpack = -1;
                this.content = new int[this.size];
                this.elements = new BaseElement[this.size];
            }
            return this;
        }

        // Token: 0x0600084D RID: 2125 RVA: 0x0004A1D8 File Offset: 0x000483D8
        public virtual bool isBoxExist(int box)
        {
            for (int i = 0; i < this.size; i++)
            {
                if (this.content[i] == box)
                {
                    return true;
                }
            }
            return false;
        }

        // Token: 0x0600084E RID: 2126 RVA: 0x0004A204 File Offset: 0x00048404
        public virtual int firstIndexOf(int box)
        {
            for (int i = 0; i < this.size; i++)
            {
                if (this.content[i] == box)
                {
                    return i;
                }
            }
            return 0;
        }

        // Token: 0x0600084F RID: 2127 RVA: 0x0004A230 File Offset: 0x00048430
        public virtual int getFirstGameBox()
        {
            for (int i = 0; i < this.size; i++)
            {
                if (BoxFabric.isGameBox(this.content[i]))
                {
                    return i;
                }
            }
            return -1;
        }

        // Token: 0x06000850 RID: 2128 RVA: 0x0004A260 File Offset: 0x00048460
        public virtual void add(int box)
        {
            this.content[this.i] = box;
            this.i++;
        }

        // Token: 0x06000851 RID: 2129 RVA: 0x0004A27E File Offset: 0x0004847E
        public override void dealloc()
        {
            this.content = null;
            this.elements = null;
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
