using System;

using ctr_wp7.ios;

namespace ctr_wp7.iframework.visual
{
    // Token: 0x02000032 RID: 50
    internal abstract class FontGeneric : NSObject
    {
        // Token: 0x060001D9 RID: 473 RVA: 0x0000C968 File Offset: 0x0000AB68
        public virtual float stringWidth(NSString str)
        {
            float num = 0f;
            int num2 = str.length();
            char[] characters = str.getCharacters();
            float num3 = 0f;
            for (int i = 0; i < num2; i++)
            {
                num3 = this.getCharOffset(characters, i, num2);
                num += this.getCharWidth(characters[i]) + num3;
            }
            return num - num3;
        }

        // Token: 0x060001DA RID: 474
        public abstract void setCharOffsetLineOffsetSpaceWidth(float co, float lo, float sw);

        // Token: 0x060001DB RID: 475
        public abstract float fontHeight();

        // Token: 0x060001DC RID: 476
        public abstract bool canDraw(char c);

        // Token: 0x060001DD RID: 477
        public abstract float getCharWidth(char c);

        // Token: 0x060001DE RID: 478
        public abstract int getCharmapIndex(char c);

        // Token: 0x060001DF RID: 479
        public abstract int getCharQuad(char c);

        // Token: 0x060001E0 RID: 480
        public abstract float getCharOffset(char[] s, int c, int len);

        // Token: 0x060001E1 RID: 481 RVA: 0x0000C9BE File Offset: 0x0000ABBE
        public virtual float getLineOffset()
        {
            return this.lineOffset;
        }

        // Token: 0x060001E2 RID: 482 RVA: 0x0000C9C6 File Offset: 0x0000ABC6
        public virtual void notifyTextCreated(Text st)
        {
        }

        // Token: 0x060001E3 RID: 483 RVA: 0x0000C9C8 File Offset: 0x0000ABC8
        public virtual void notifyTextChanged(Text st)
        {
        }

        // Token: 0x060001E4 RID: 484 RVA: 0x0000C9CA File Offset: 0x0000ABCA
        public virtual void notifyTextDeleted(Text st)
        {
        }

        // Token: 0x060001E5 RID: 485
        public abstract int totalCharmaps();

        // Token: 0x060001E6 RID: 486
        public abstract Image getCharmap(int i);

        // Token: 0x040007E2 RID: 2018
        protected float charOffset;

        // Token: 0x040007E3 RID: 2019
        protected float lineOffset;

        // Token: 0x040007E4 RID: 2020
        protected float spaceWidth;
    }
}
